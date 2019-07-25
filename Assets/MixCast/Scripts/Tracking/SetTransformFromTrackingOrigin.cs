/**********************************************************************************
* Blueprint Reality Inc. CONFIDENTIAL
* 2019 Blueprint Reality Inc.
* All Rights Reserved.
*
* NOTICE:  All information contained herein is, and remains, the property of
* Blueprint Reality Inc. and its suppliers, if any.  The intellectual and
* technical concepts contained herein are proprietary to Blueprint Reality Inc.
* and its suppliers and may be covered by Patents, pending patents, and are
* protected by trade secret or copyright law.
*
* Dissemination of this information or reproduction of this material is strictly
* forbidden unless prior written permission is obtained from Blueprint Reality Inc.
***********************************************************************************/

#if UNITY_STANDALONE_WIN
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if MIXCAST_STEAMVR
using Valve.VR;
#endif
#if UNITY_2017_2_OR_NEWER
using UnityEngine.XR;
using Device = UnityEngine.XR.XRDevice;
using UnityXrTracking = UnityEngine.XR.InputTracking;
using Node = UnityEngine.XR.XRNode;
using NodeState = UnityEngine.XR.XRNodeState;
using UnityEngine.Experimental.Rendering;
#else
using UnityEngine.VR;
using Device = UnityEngine.VR.VRDevice;
using UnityXrTracking = UnityEngine.VR.InputTracking;
using Node = UnityEngine.VR.VRNode;
#if UNITY_2017_1_OR_NEWER
using NodeState = UnityEngine.VR.VRNodeState;
#endif
#endif

namespace BlueprintReality.MixCast.Tracking
{
    public class SetTransformFromTrackingOrigin : MonoBehaviour
    {
        private int lastRenderedFrameCount = -1;

        private Camera hmdCamera;

        private Vector3 trackingSpaceOffsetPos = Vector3.zero;
        private Quaternion trackingSpaceOffsetRot = Quaternion.identity;

#if UNITY_2017_1_OR_NEWER
        protected List<NodeState> trackingNodeStates = new List<NodeState>();
#endif

        private void OnEnable()
        {
            Camera.onPreRender += ApplyPoses;
#if UNITY_2018_1_OR_NEWER
            RenderPipeline.beginCameraRendering += ApplyPoses;
#endif
        }
        private void OnDisable()
        {
#if UNITY_2018_1_OR_NEWER
            RenderPipeline.beginCameraRendering -= ApplyPoses;
#endif
            Camera.onPreRender -= ApplyPoses;
        }

        void ApplyPoses(Camera cam)
        {
            if (MixCast.Settings == null)
                return;

            if (lastRenderedFrameCount == Time.renderedFrameCount)
                return;

            lastRenderedFrameCount = Time.renderedFrameCount;
            UpdateTrackingSpaceOffset();
            UpdateTransform();
        }

        void Update()
        {
            PollEngineNodes();
        }

        void PollEngineNodes()
        {
#if UNITY_2017_1_OR_NEWER
            InputTracking.GetNodeStates(trackingNodeStates);
#endif
        }

        void UpdateTransform()
        {
            if (!Device.isPresent)
            {
                ApplyTransformFromFirstRoom();
                return;
            }

            if (hmdCamera == null || !hmdCamera.isActiveAndEnabled)
            {
                hmdCamera = VRInfo.FindHMDCamera();
            }

            if (hmdCamera == null)
            {
                ApplyTransformFromFirstRoom();
                return;
            }

            float playerScale = 1;
            if (hmdCamera.transform.parent != null)
            {
                playerScale = hmdCamera.transform.parent.TransformVector(Vector3.forward).magnitude;
            }

            Vector3 mixcastTrackingOriginPos;
            Quaternion mixcastTrackingOriginRot;
            GetEngineOrigin(playerScale, out mixcastTrackingOriginPos, out mixcastTrackingOriginRot);

            ApplyTrackingSpaceCompensationToOrigin(playerScale, ref mixcastTrackingOriginPos, ref mixcastTrackingOriginRot);

#if UNITY_2017_1_OR_NEWER
            if( VRInfo.IsVRSystemOculus() )
            {
                NodeState? refNode = TrackingSpaceOrigin.GetReferenceSensorNode(trackingNodeStates);
                if (refNode.HasValue)
                {
                    Vector3 spaceOffsetPos = Vector3.zero;
                    Quaternion spaceOffsetRot = Quaternion.identity;
                    TrackingSpaceOrigin.CorrectWorldPose(refNode.Value, ref spaceOffsetPos, ref spaceOffsetRot);
                    mixcastTrackingOriginPos += mixcastTrackingOriginRot * spaceOffsetPos;
                    mixcastTrackingOriginRot *= spaceOffsetRot;
                }
            }
#endif

            transform.position = mixcastTrackingOriginPos;
            transform.rotation = mixcastTrackingOriginRot;
            transform.localScale = Vector3.one * playerScale;
        }

        void ApplyTransformFromFirstRoom()
        {
            if (MixCastRoomBehaviour.ActiveRoomBehaviours.Count > 0)
            {
                transform.position = MixCastRoomBehaviour.ActiveRoomBehaviours[0].transform.position;
                transform.rotation = MixCastRoomBehaviour.ActiveRoomBehaviours[0].transform.rotation;
                transform.localScale = Vector3.one * MixCastRoomBehaviour.ActiveRoomBehaviours[0].transform.TransformVector(Vector3.forward).magnitude;
            }
        }

        void GetEngineOrigin(float playerScale, out Vector3 engineOriginPos, out Quaternion engineOriginRot)
        {
            Vector3 hmdLocalPos = UnityXrTracking.GetLocalPosition(Node.CenterEye);
            Quaternion hmdLocalRot = UnityXrTracking.GetLocalRotation(Node.CenterEye);

            engineOriginRot = hmdCamera.transform.rotation * Quaternion.Inverse(hmdLocalRot);
            engineOriginPos = hmdCamera.transform.position - playerScale * (engineOriginRot * hmdLocalPos);
        }

        void ApplyTrackingSpaceCompensationToOrigin(float playerScale, ref Vector3 pos, ref Quaternion rot)
        {
            rot = rot * Quaternion.Inverse(trackingSpaceOffsetRot);
            pos = pos - playerScale * (rot * trackingSpaceOffsetPos);
        }

#if MIXCAST_STEAMVR
        TrackedDevicePose_t[] standingPoses = new TrackedDevicePose_t[OpenVR.k_unMaxTrackedDeviceCount];
        TrackedDevicePose_t[] seatedPoses = new TrackedDevicePose_t[OpenVR.k_unMaxTrackedDeviceCount];
#endif

        void UpdateTrackingSpaceOffset()
        {
#if MIXCAST_STEAMVR
            if (VRInfo.IsDeviceOpenVR())
            {
                if (OpenVR.Compositor.GetTrackingSpace() == ETrackingUniverseOrigin.TrackingUniverseSeated)
                {
                    OpenVR.System.GetDeviceToAbsoluteTrackingPose(ETrackingUniverseOrigin.TrackingUniverseStanding, 0, standingPoses);
                    OpenVR.System.GetDeviceToAbsoluteTrackingPose(ETrackingUniverseOrigin.TrackingUniverseSeated, 0, seatedPoses);

                    for( int i = 0; i < standingPoses.Length; i++ )
                    {
                        if (standingPoses[i].bPoseIsValid && seatedPoses[i].bPoseIsValid)
                        {
                            SteamVR_Utils.RigidTransform standingHmdTransform = new SteamVR_Utils.RigidTransform(standingPoses[i].mDeviceToAbsoluteTracking);
                            SteamVR_Utils.RigidTransform seatedHmdTransform = new SteamVR_Utils.RigidTransform(seatedPoses[i].mDeviceToAbsoluteTracking);

                            SteamVR_Utils.RigidTransform sittingToStanding = SteamVR_Utils.RigidTransform.identity;
                            sittingToStanding.Multiply(standingHmdTransform, seatedHmdTransform.GetInverse());

                            if( Vector3.SqrMagnitude(trackingSpaceOffsetPos - sittingToStanding.pos) > 0.001f || Quaternion.Angle(trackingSpaceOffsetRot, sittingToStanding.rot) > 0.1f )
                            {
                                Debug.Log("Seated mode compensation \n" +
                                    "Pos: " + sittingToStanding.pos + "\n" +
                                    "Rot: " + sittingToStanding.rot);
                            }
                            trackingSpaceOffsetPos = sittingToStanding.pos;
                            trackingSpaceOffsetRot = sittingToStanding.rot;

                            break;
                        }
                    }
                }
                else
                {
                    trackingSpaceOffsetPos = Vector3.zero;
                    trackingSpaceOffsetRot = Quaternion.identity;
                }
            }
#else
            trackingSpaceOffsetPos = Vector3.zero;
            trackingSpaceOffsetRot = Quaternion.identity;
#endif
        }
    }
}
#endif
