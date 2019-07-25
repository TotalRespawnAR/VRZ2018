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
using BlueprintReality.MixCast.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlueprintReality.MixCast
{
    public static class SdkCameraUtilities
    {
        public static int GetConfiguredCameraRenderWidth(MixCastData.CameraCalibrationData cameraConfig)
        {
            switch (cameraConfig.outputResolution)
            {
                case MixCastData.OutputResolution.WindowSize:
                    return Screen.width;
                case MixCastData.OutputResolution.FullScreen:
                    return Screen.currentResolution.width;
                case MixCastData.OutputResolution.Preset:
                    return cameraConfig.outputWidth;
                case MixCastData.OutputResolution.Custom:
                    if (cameraConfig.outputWidth > 0)
                        return cameraConfig.outputWidth;
                    else if (cameraConfig.outputHeight > 0)
                        return (int)(((float)Screen.width * cameraConfig.outputHeight) / Screen.height);
                    else
                        return Screen.width;
            }
            return -1;
        }
        public static int GetConfiguredCameraRenderHeight(MixCastData.CameraCalibrationData cameraConfig)
        {
            switch (cameraConfig.outputResolution)
            {
                case MixCastData.OutputResolution.WindowSize:
                    return Screen.height;
                case MixCastData.OutputResolution.FullScreen:
                    return Screen.currentResolution.height;
                case MixCastData.OutputResolution.Preset:
                    return cameraConfig.outputHeight;
                case MixCastData.OutputResolution.Custom:
                    if (cameraConfig.outputHeight > 0)
                        return cameraConfig.outputHeight;
                    else if (cameraConfig.outputWidth > 0)
                        return (int)(((float)Screen.height * cameraConfig.outputWidth) / Screen.width);
                    else
                        return Screen.height;
            }
            return -1;
        }

        public static bool IsCameraRunningInRealtime(string cameraId)
        {
            return IsCameraDisplayingToDesktop(cameraId) || IsCameraRecording(cameraId) || IsCameraStreaming(cameraId);
        }
        static bool IsCameraDisplayingToDesktop(string cameraId)
        {
            for (int i = 0; i < MixCastSdkData.Desktop.DisplayingCameraIds.Count; i++)
                if (MixCastSdkData.Desktop.DisplayingCameraIds[i] == cameraId)
                    return true;

            return false;
        }
        public static bool IsCameraRecording(string cameraId)
        {
            for (int i = 0; i < MixCastSdkData.Cameras.Count; i++)
                if (MixCastSdkData.Cameras[i].Identifier == cameraId)
                    return MixCastSdkData.Cameras[i].VideoRecordingEnabled;

            return false;
        }
        public static bool IsCameraStreaming(string cameraId)
        {
            for (int i = 0; i < MixCastSdkData.Cameras.Count; i++)
                if (MixCastSdkData.Cameras[i].Identifier == cameraId)
                    return MixCastSdkData.Cameras[i].VideoStreamingEnabled;

            return false;
        }

        public static void GetCameraPose(MixCastData.CameraCalibrationData data, out Vector3 position, out Quaternion rotation)
        {
            if (data.wasTracked && GetCamerasTrackingObjectTransform(data, out position, out rotation))
            {
                position = position + rotation * data.trackedPosition;
                rotation = rotation * data.trackedRotation;
            }
            else
            {
                position = data.worldPosition;
                rotation = data.worldRotation;
            }
        }
        public static bool GetCamerasTrackingObjectTransform(MixCastData.CameraCalibrationData data, out Vector3 position, out Quaternion rotation)
        {
            if (!string.IsNullOrEmpty(data.trackedByDeviceId))
            {
                if (TrackedObjectManager.GetTransformById(data.trackedByDeviceId, out position, out rotation))
                {
                    return true;
                }
            }

            //Fall back to index if guid isn't found
            if (!string.IsNullOrEmpty(data.trackedByDeviceRole))
            {
                int roleIndex = 0;
                if (int.TryParse(data.trackedByDeviceRole, out roleIndex))
                {
                    Data.AssignedRole role = (Data.AssignedRole)roleIndex;
                    return TrackedObjectManager.GetTransformByRole(role, out position, out rotation);
                }
            }

            position = Vector3.zero;
            rotation = Quaternion.identity;
            return false;
        }

        public static float CalculatePlayerDistance(MixCastData.CameraCalibrationData camInfo)
        {
            int hmdCount = 0;
            Vector3 devicePosSum = Vector3.zero;
            for (int i = 0; i < MixCastSdkData.TrackedObjects.Count; i++)
            {
                var trackedObject = MixCastSdkData.TrackedObjects[i];

                if (!trackedObject.Connected || trackedObject.HideFromUser)
                    continue;

                Vector3 objectPosition;
                if (string.IsNullOrEmpty(camInfo.actorTrackingDeviceId))
                {
                    if (trackedObject.ObjectType != Data.ObjectType.HMD)
                        continue;

                    objectPosition = trackedObject.Position.unity;

                    if (trackedObject.Source == TrackingSource.OCULUS)
                        objectPosition += trackedObject.Rotation.unity * VRInfo.OCULUS_RIFT_HMD_TO_HEAD;
                    else if (trackedObject.Source == TrackingSource.OPENVR)
                        objectPosition += trackedObject.Rotation.unity * VRInfo.VIVE_HMD_TO_HEAD;
                }
                else
                {
                    if (trackedObject.Identifier != camInfo.actorTrackingDeviceId)
                        continue;

                    objectPosition = trackedObject.Position.unity;

                    if (trackedObject.ObjectType == ObjectType.HMD)
                    {
                        if (trackedObject.Source == TrackingSource.OCULUS)
                            objectPosition += trackedObject.Rotation.unity * VRInfo.OCULUS_RIFT_HMD_TO_HEAD;
                        else if (trackedObject.Source == TrackingSource.OPENVR)
                            objectPosition += trackedObject.Rotation.unity * VRInfo.VIVE_HMD_TO_HEAD;
                    }
                }

                devicePosSum += objectPosition;
                hmdCount++;
            }

            if (hmdCount == 0)
                return 0;

            devicePosSum /= hmdCount;

            Vector3 cameraPos;
            Quaternion cameraRot;
            SdkCameraUtilities.GetCameraPose(camInfo, out cameraPos, out cameraRot);
            Matrix4x4 camMat = Matrix4x4.TRS(cameraPos, cameraRot, Vector3.one);

            Vector3 playerPosInCameraSpace = camMat.inverse.MultiplyPoint(devicePosSum);
            return playerPosInCameraSpace.z;
        }
    }
}
#endif
