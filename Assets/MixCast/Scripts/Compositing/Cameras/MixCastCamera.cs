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
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace BlueprintReality.MixCast
{
    public class MixCastCamera : CameraComponent
    {
        public static List<MixCastCamera> ActiveCameras { get; protected set; }
        public static MixCastCamera Current { get; protected set; } //Assigned to the MixCastCamera that is being processed between FrameStarted and FrameEnded

        public static MixCastCamera FindCamera(CameraConfigContext context)
        {
            for (int i = 0; i < ActiveCameras.Count; i++)
            {
                if (ActiveCameras[i].context.Data == context.Data)
                {
                    return ActiveCameras[i];
                }
            }
            return null;
        }

        static MixCastCamera()
        {
            ActiveCameras = new List<MixCastCamera>();
        }

        public static event System.Action<MixCastCamera> FrameStarted;
        public static event System.Action<MixCastCamera> FrameEnded;

        public static event System.Action<MixCastCamera> GameRenderStarted;
        public static event System.Action<MixCastCamera> GameRenderEnded;

        public Transform displayTransform;
        public Camera gameCamera;

        public Texture Output { get; protected set; }

        protected SetTransformFromCameraSettings setTransform;

        private int width;
        private int height;

        protected override void OnEnable()
        {
			if (gameCamera != null)
            {
                gameCamera.stereoTargetEye = StereoTargetEyeMask.None;
                gameCamera.enabled = false;
            }
            setTransform = gameCamera.GetComponentInParent<SetTransformFromCameraSettings>();

            base.OnEnable();

            HandleDataChanged();

            ActiveCameras.Add(this);
        }
        protected override void OnDisable()
        {
            ReleaseOutput();

            ActiveCameras.Remove(this);

            base.OnDisable();

            if (gameCamera != null)
                gameCamera.enabled = true;
		}

        protected virtual void LateUpdate()
        {
            if (context.Data == null)
                return;

            if (context.Data.deviceFoV > 0 && gameCamera != null)
                gameCamera.fieldOfView = context.Data.deviceFoV;

            if (!SdkCameraUtilities.IsCameraRecording(context.Data.id) && !SdkCameraUtilities.IsCameraStreaming(context.Data.id))
            {
                width = SdkCameraUtilities.GetConfiguredCameraRenderWidth(context.Data);
                height = SdkCameraUtilities.GetConfiguredCameraRenderHeight(context.Data);

                if (Output == null || width != Output.width || height != Output.height)
                {
                    ReleaseOutput();
                    BuildOutput();
                }
            }
        }

        protected virtual void BuildOutput()
        {
            Output = new RenderTexture(width, height, 24, RenderTextureFormat.ARGBFloat, RenderTextureReadWrite.Linear)
            {
                antiAliasing = CalculateAntiAliasingValue(),
                useMipMap = false,
#if UNITY_5_5_OR_NEWER
                autoGenerateMips = false,
#else
                generateMips = false,
#endif
            };

            if (gameCamera != null)
            {
                gameCamera.targetTexture = Output as RenderTexture;
                gameCamera.aspect = (float)width / height;
            }
        }

        protected virtual void ReleaseOutput()
        {
            if (Output != null)
            {
                (Output as RenderTexture).Release();
                Output = null;
                if (gameCamera != null)
                    gameCamera.targetTexture = null;
            }
        }

        protected int CalculateAntiAliasingValue()
        {
#if UNITY_5_6_OR_NEWER
            if (gameCamera != null && !gameCamera.allowMSAA)
                return 1;
#endif
            if (gameCamera.actualRenderingPath == RenderingPath.DeferredShading)
                return 1;

            if (MixCastSdkData.ProjectSettings.overrideQualitySettingsAA)
                return 1 << MixCastSdkData.ProjectSettings.overrideAntialiasingVal;    //{unity-antialiasing-units} === 2^{saved-units}
            else
                return Mathf.Max(QualitySettings.antiAliasing, 1);  //Disabled can equal 0 rather than 1
        }

        public virtual void RenderScene()
        {
            setTransform.UpdateTransform();
        }

#region Event Firing
        protected void RenderGameCamera( Camera cam, RenderTexture target )
		{
			if (target == null || target.width == 0 || target.height == 0)
				return;

            RenderTexture oldTarget = cam.targetTexture;
            if( GameRenderStarted != null)
                GameRenderStarted( this );

            cam.targetTexture = target;
            cam.aspect = (float)target.width / target.height;
            cam.Render();

            if (GameRenderEnded != null)
                GameRenderEnded(this);

            cam.targetTexture = oldTarget;
            if (oldTarget != null)
                cam.aspect = (float)oldTarget.width / oldTarget.height;
            else
                cam.ResetAspect();
        }

		protected void StartFrame()
        {
            Current = this;
            if (FrameStarted != null)
                FrameStarted(this);
        }

        protected void CompleteFrame()
        {
            Current = null;

            if (FrameEnded != null)
                FrameEnded(this);

            Graphics.SetRenderTarget(null);
        }
        #endregion
    }
}
#endif
