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
#if UNITY_2018_1_OR_NEWER
using UnityEngine.Experimental.Rendering;
#endif

namespace BlueprintReality.MixCast {
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
	public class InputFeedProjection : CameraComponent
    {
        public class FramePlayerData
        {
            public float playerDist;
        }

        public const string ANTI_ALIASING_SHADER_KEYWORD = "AA_ON";
        private const float NEAR_PLANE_PADDING = 0.01f; //Projection can only get as close to the camera as 101% of the near clip plane

        public static List<InputFeedProjection> ActiveProjections { get; protected set; }
        static InputFeedProjection()
        {
            ActiveProjections = new List<InputFeedProjection>();
        }

        public string textureProperty = "_MainTex";

        public MeshFilter Filter { get; protected set; }
        public MeshRenderer MeshRenderer { get; protected set; }
        public Material ProjectionMaterial { get; protected set; }

        public SharedTextureReceiver TextureReceiver { get; protected set; }
        public Texture InputTexture { get { return TextureReceiver != null ? TextureReceiver.Texture : null; } }
        public bool CanRender { get { return InputTexture != null; } }

        public event System.Action OnPreRender;

        protected FrameDelayQueue<FramePlayerData> frames;
        private Mesh projectionMesh;
        private List<Vector3> vertBuffer = new List<Vector3>();

        private void Awake()
        {
            Filter = GetComponent<MeshFilter>();
            MeshRenderer = GetComponent<MeshRenderer>();
            ProjectionMaterial = MeshRenderer.material;
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            frames = new FrameDelayQueue<FramePlayerData>(() => { return new FramePlayerData(); });

            InitializeMesh();

            Camera.onPreCull += HandleRenderStarted;
#if UNITY_2018_1_OR_NEWER
            RenderPipeline.beginCameraRendering += HandleRenderStarted;
#endif


#if UNITY_EDITOR
            InvokeProtector.InvokeRepeating(RefreshSceneCameras, 0, 1);
#endif

            ActiveProjections.Add(this);

            HandleDataChanged();
        }
        protected override void OnDisable()
        {
            ActiveProjections.Remove(this);

#if UNITY_2018_1_OR_NEWER
            RenderPipeline.beginCameraRendering -= HandleRenderStarted;
#endif
            Camera.onPreCull -= HandleRenderStarted;

            if (TextureReceiver != null)
            {
                Service_SDK_Handler.OnExternalTextureUpdated -= TextureReceiver.RefreshTextureFromEvent;
                TextureReceiver.Dispose();
                TextureReceiver = null;
            }

            Filter.sharedMesh = null;

#if UNITY_EDITOR
            InvokeProtector.CancelInvokeRepeating(RefreshSceneCameras);
#endif

            frames = null;

            base.OnDisable();
        }

        protected override void HandleDataChanged()
        {
            base.HandleDataChanged();
            if (TextureReceiver != null)
            {
                Service_SDK_Handler.OnExternalTextureUpdated -= TextureReceiver.RefreshTextureFromEvent;
                TextureReceiver.Dispose();
                TextureReceiver = null;
            }
            if (context.Data != null)
            {
                TextureReceiver = new SharedTextureReceiver(context.Data.id + "_input_rgb");
                Service_SDK_Handler.OnExternalTextureUpdated += TextureReceiver.RefreshTextureFromEvent;
            }
        }

        private int lastRefreshFrame;
        public void HandleRenderStarted(Camera cam)
        {
            if (context.Data == null )
                return;

            bool shouldRenderForCam = ShouldRenderForCamera(cam);
            if( !shouldRenderForCam )
            {
                MeshRenderer.enabled = false;
                return;
            }

            if (lastRefreshFrame < Time.frameCount)
            {
                StoreCurrentFramePlayerData();
                //TextureReceiver.RefreshTextureHandleManually();
                lastRefreshFrame = Time.frameCount;
            }
            if (InputTexture == null)
                return;

            frames.delayDuration = context.Data.bufferTime;
            frames.Update();
            
            SetMaterialProperties();
            UpdateMesh();

            MeshRenderer.enabled = true;

            if (OnPreRender != null)
                OnPreRender();
        }

        bool ShouldRenderForCamera(Camera cam)
        {
#if UNITY_EDITOR
            for (int i = 0; i < cachedSceneCameras.Count; i++)
                if (cam == cachedSceneCameras[i])
                    return MixCastSdkData.ProjectSettings.displaySubjectInScene;
#endif

            if (MixCastCamera.Current != null)
            {
                if (MixCastCamera.Current.context.Data == context.Data)
                {
                    if (MixCastCamera.Current is BufferedMixCastCamera)
                        return (MixCastCamera.Current as BufferedMixCastCamera).RenderingInputFeed;
                    else if (MixCastCamera.Current is QuadrantMixCastCamera)
                        return false;
                    else
                        return true;
                }
                else
                    return context.Data.projectionData.displayToOtherCams;
            }

            if (cam.stereoTargetEye != StereoTargetEyeMask.None)
            {
                return context.Data.projectionData.displayToHeadset;
            }

            return false;
        }
#if UNITY_EDITOR
        private List<Camera> cachedSceneCameras = new List<Camera>();
        void RefreshSceneCameras()
        {
            cachedSceneCameras.Clear();
            cachedSceneCameras.AddRange(UnityEditor.SceneView.GetAllSceneCameras());
        }
#endif

        void StoreCurrentFramePlayerData()
        {
            FramePlayerData newFrame = frames.GetNewFrame().Data;

            MixCastCamera cam = MixCastCamera.FindCamera(context);
            if (cam != null && cam.gameCamera != null)
                newFrame.playerDist = cam.gameCamera.transform.TransformVector(Vector3.forward).magnitude * SdkCameraUtilities.CalculatePlayerDistance(cam.context.Data); //Scale distance by camera scale
        }
        void SetMaterialProperties()
        {
            ProjectionMaterial.SetTexture(textureProperty, InputTexture);

            if (context.Data.antiAliasSegmentationResults)
                ProjectionMaterial.EnableKeyword(ANTI_ALIASING_SHADER_KEYWORD);
            else
                ProjectionMaterial.DisableKeyword(ANTI_ALIASING_SHADER_KEYWORD);

            float outputAspect = (float)context.Data.outputWidth / context.Data.outputHeight;
            float sourceAspect = (float)InputTexture.width / InputTexture.height;

            Vector4 texTransform = new Vector4(1, 1, 0, 0);
            texTransform.x = outputAspect / sourceAspect;
            texTransform.z = 0.5f * (1f - texTransform.x);
            ProjectionMaterial.SetVector("_MainTex_Transform", texTransform);

            FramePlayerData oldFrameData = frames.OldestFrameData;
            if (oldFrameData != null)
                ProjectionMaterial.SetFloat("_PlayerDist", oldFrameData.playerDist);
        }
#region Mesh Manipulation
        void InitializeMesh()
        {
            if (projectionMesh != null)
                Destroy(projectionMesh);

            projectionMesh = new Mesh();
            projectionMesh.MarkDynamic();

            vertBuffer = new List<Vector3>();
            for (int i = 0; i < 4; i++)
                vertBuffer.Add(Vector3.zero);
            projectionMesh.SetVertices(vertBuffer);

            List<Vector2> uvs = new List<Vector2>();
            uvs.Add(new Vector2(0, 1));
            uvs.Add(new Vector2(1, 1));
            uvs.Add(new Vector2(1, 0));
            uvs.Add(new Vector2(0, 0));
            projectionMesh.SetUVs(0, uvs);

            projectionMesh.SetIndices(new int[] { 0, 1, 2, 3 }, MeshTopology.Quads, 0);

            GetComponent<MeshFilter>().sharedMesh = projectionMesh;
        }
        void UpdateMesh()
        {
            MixCastCamera cam = MixCastCamera.FindCamera(context);
            if (cam == null)
                return;

            float playerDist = Mathf.Max(cam.gameCamera.nearClipPlane * (1f + NEAR_PLANE_PADDING), SdkCameraUtilities.CalculatePlayerDistance(cam.context.Data));
            float playerQuadHalfHeight = playerDist * Mathf.Tan(context.Data.deviceFoV * 0.5f * Mathf.Deg2Rad);
            float playerQuadHalfWidth = playerQuadHalfHeight * InputTexture.width / InputTexture.height;

            vertBuffer[0] = new Vector3(-playerQuadHalfWidth, playerQuadHalfHeight, playerDist);
            vertBuffer[1] = new Vector3(playerQuadHalfWidth, playerQuadHalfHeight, playerDist);
            vertBuffer[2] = new Vector3(playerQuadHalfWidth, -playerQuadHalfHeight, playerDist);
            vertBuffer[3] = new Vector3(-playerQuadHalfWidth, -playerQuadHalfHeight, playerDist);

            projectionMesh.SetVertices(vertBuffer);
            projectionMesh.RecalculateBounds();
            projectionMesh.UploadMeshData(false);
        }
#endregion
    }
}
#endif
