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
    //Place at the root of the camera hierarchy.
    public class BufferedMixCastCamera : MixCastCamera
    {
        public const string BLIT_BACKGROUND_SHADER = "Hidden/BPR/Background Blit";
        public const string BLIT_FOREGROUND_SHADER = "Hidden/BPR/Foreground Blit";
        public const string BLIT_BACKGROUND_PMA_SHADER = "Hidden/BPR/Background Blit PMA";
        public const string BLIT_FOREGROUND_PMA_SHADER = "Hidden/BPR/Foreground Blit PMA";
        public const string COPY_ALPHA_CHANNEL_SHADER = "Hidden/BPR/AlphaTransfer";

        [System.Serializable]
        public class GameFrame
        {
            public float playerDist;

            public RenderTexture foregroundBuffer;
            public RenderTexture backgroundBuffer;

            public void Release()
            {
                if (backgroundBuffer != null)
                    backgroundBuffer.Release();
                if (foregroundBuffer != null)
                    foregroundBuffer.Release();
                backgroundBuffer = null;
                foregroundBuffer = null;
            }
        }

        private FrameDelayQueue<GameFrame> frames;

        private RenderTexture sceneRenderTarget;
        private RenderTexture compositingRenderTarget;
        public RenderTexture LastFrameAlpha { get; protected set; }


        public bool RenderingInputFeed { get; protected set; }

        private Material blitBackgroundMat;
        private Material blitForegroundMat;

        private CommandBuffer grabAlphaCommand;
        private CommandBuffer replaceAlphaCommand;
        private Material copyAlphaMat;

        private List<Camera> backgroundCameraList = new List<Camera>();
        private List<Camera> foregroundCameraList = new List<Camera>();

        private Camera finalCam;
        private CommandBuffer finalizeFrameCommand;

#if MIXCAST_LWRP
        List<InsertCmdBufferBehaviour_AfterTransparent> foregroundAlphaGrabCmdInserters = new List<InsertCmdBufferBehaviour_AfterTransparent>();
        List<InsertCmdBufferBehaviour_AfterEverything> foregroundAlphaSetCmdInserters = new List<InsertCmdBufferBehaviour_AfterEverything>();
        InsertCmdBufferBehaviour_AfterTransparent finalPassCmdInserter;
#endif

        protected void Awake()
        {
            if (MixCastSdkData.ProjectSettings.usingPMA)
            {
                blitBackgroundMat = new Material(Shader.Find(BLIT_BACKGROUND_PMA_SHADER));
                blitForegroundMat = new Material(Shader.Find(BLIT_FOREGROUND_PMA_SHADER));
            }
            else
            {
                blitBackgroundMat = new Material(Shader.Find(BLIT_BACKGROUND_SHADER));
                blitForegroundMat = new Material(Shader.Find(BLIT_FOREGROUND_SHADER));
            }

            copyAlphaMat = new Material(Shader.Find(COPY_ALPHA_CHANNEL_SHADER));

            SpawnSceneLayerCameras();

            if (MixCastSdkData.ProjectSettings.finalPassCamPrefab != null)
            {
                finalCam = CreateFinalPassCam("Final Pass Camera");

                finalizeFrameCommand = new CommandBuffer() { name = "Finalize Frame" };
            }
        }
        void SpawnSceneLayerCameras()
        {
            if (MixCastSdkData.ProjectSettings.layerCamPrefab != null)
            {
                SpawnLayerCameraFromPrefab(false);
                SpawnLayerCameraFromPrefab(true);
            }
            else
            {
                SpawnLayerCameraFromScratch(false);
                SpawnLayerCameraFromScratch(true);
            }
        }
        void SpawnLayerCameraFromPrefab(bool isForeground)
        {
            GameObject spawnedCamObj = Instantiate(MixCastSdkData.ProjectSettings.layerCamPrefab);

            spawnedCamObj.name = isForeground ? "Foreground Camera" : "Background Camera";

            List<Camera> camList = isForeground ? foregroundCameraList : backgroundCameraList;
            camList.AddRange(spawnedCamObj.GetComponentsInChildren<Camera>(true));
            camList.Sort((x, y) => x.depth.CompareTo(y.depth));

            for (int i = 0; i < camList.Count; i++)
            {
                Camera newCam = camList[i];
                newCam.stereoTargetEye = StereoTargetEyeMask.None;
                newCam.enabled = false;
            }

            if (isForeground)
            {
                SetCameraParametersFromMainCamera[] copyParamsScript = spawnedCamObj.GetComponentsInChildren<SetCameraParametersFromMainCamera>(true);
                for (int i = 0; i < copyParamsScript.Length; i++)
                {
                    copyParamsScript[i].clippingPlanes = false;
                    copyParamsScript[i].clearSettings = false;
                }
            }

            spawnedCamObj.transform.SetParent(gameCamera.transform);
            spawnedCamObj.transform.localPosition = Vector3.zero;
            spawnedCamObj.transform.localRotation = Quaternion.identity;
            spawnedCamObj.transform.localScale = Vector3.one;
        }
        void SpawnLayerCameraFromScratch(bool isForeground)
        {
            GameObject newCamObj = new GameObject(isForeground ? "Foreground Camera" : "Background Camera")
            {
                hideFlags = HideFlags.HideAndDontSave
            };

            Camera newCam = newCamObj.AddComponent<Camera>();

            newCam.depth = isForeground ? 2 : 1;
            newCam.stereoTargetEye = StereoTargetEyeMask.None;
            newCam.enabled = false;

            SetCameraParametersFromMainCamera copyParamsScript = newCamObj.AddComponent<SetCameraParametersFromMainCamera>();

            if (isForeground)
            {
                copyParamsScript.clippingPlanes = false;
                copyParamsScript.clearSettings = false;
            }

            List<Camera> camList = isForeground ? foregroundCameraList : backgroundCameraList;
            camList.Add(newCam);

            newCamObj.transform.SetParent(gameCamera.transform);
            newCamObj.transform.localPosition = Vector3.zero;
            newCamObj.transform.localRotation = Quaternion.identity;
            newCamObj.transform.localScale = Vector3.one;
        }
        Camera CreateFinalPassCam(string camName)
        {
            GameObject newCamObj = Instantiate(MixCastSdkData.ProjectSettings.finalPassCamPrefab);
            newCamObj.name = camName;

            Camera newCam = newCamObj.GetComponent<Camera>();
            newCam.cullingMask = 0;
            newCam.clearFlags = CameraClearFlags.Nothing;
            newCam.farClipPlane = 1;
            newCam.nearClipPlane = 0.1f;
            newCam.renderingPath = RenderingPath.Forward;
            newCam.useOcclusionCulling = false;
            newCam.stereoTargetEye = StereoTargetEyeMask.None;
            newCam.enabled = false;

            newCamObj.transform.SetParent(gameCamera.transform);
            newCamObj.transform.localPosition = Vector3.zero;
            newCamObj.transform.localRotation = Quaternion.identity;
            newCamObj.transform.localScale = Vector3.one;

            return newCam;
        }

        protected override void OnEnable()
        {
            base.OnEnable();

#if MIXCAST_LWRP
            if (GraphicsSettings.renderPipelineAsset is UnityEngine.Experimental.Rendering.LightweightPipeline.LightweightRenderPipelineAsset)
            {
                if (finalCam != null)
                {
                    finalPassCmdInserter = finalCam.gameObject.AddComponent<InsertCmdBufferBehaviour_AfterTransparent>();
                    finalPassCmdInserter.Create("FinalMixCastPass", (c) =>
                    {
                        c.ExecuteCommandBuffer(finalizeFrameCommand);
                    });
                }
            }
#endif
        }
        protected override void OnDisable()
        {
#if MIXCAST_LWRP
            if (finalPassCmdInserter != null)
                Destroy(finalPassCmdInserter);
#endif
            base.OnDisable();
        }

        protected override void BuildOutput()
        {
            base.BuildOutput();

            frames = new FrameDelayQueue<GameFrame>(() =>
            {
                return new GameFrame();
            },
            (GameFrame f) =>
            {
                f.Release();
            });
            frames.delayDuration = context.Data.bufferTime;

            sceneRenderTarget = new RenderTexture(Output.width, Output.height, 24, RenderTextureFormat.ARGBFloat, RenderTextureReadWrite.Linear)
            {
                antiAliasing = CalculateAntiAliasingValue(),
                useMipMap = false,
#if UNITY_5_5_OR_NEWER
                autoGenerateMips = false,
#else
                generateMips = false,
#endif
            };

            if (MixCastSdkData.ProjectSettings.grabUnfilteredAlpha)
            {
                LastFrameAlpha = new RenderTexture(Output.width, Output.height, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear)
                {
                    useMipMap = false,
#if UNITY_5_5_OR_NEWER
                    autoGenerateMips = false,
#else
                    generateMips = false,
#endif
                };

                grabAlphaCommand = new CommandBuffer() { name = "Get Correct Alpha" };
                grabAlphaCommand.Blit(BuiltinRenderTextureType.CurrentActive, LastFrameAlpha);
                replaceAlphaCommand = new CommandBuffer() { name = "Set Correct Alpha" };
#if UNITY_5_5_OR_NEWER
                replaceAlphaCommand.Blit(LastFrameAlpha as RenderTexture, BuiltinRenderTextureType.CurrentActive, copyAlphaMat);
#else
                replaceAlphaCommand.Blit(LastFrameAlpha as Texture, BuiltinRenderTextureType.CurrentActive, copyAlphaMat);
#endif

                for (int i = 0; i < foregroundCameraList.Count; i++)
                {
                    foregroundCameraList[i].AddCommandBuffer(CameraEvent.AfterForwardAlpha, grabAlphaCommand);   //Instruction to copy out the state of the RenderTexture before Image Effects are applied
                    foregroundCameraList[i].AddCommandBuffer(CameraEvent.AfterImageEffects, replaceAlphaCommand);   //Instruction to paste in the state of the RenderTexture from before Image Effects are applied

#if MIXCAST_LWRP
                    if (GraphicsSettings.renderPipelineAsset is UnityEngine.Experimental.Rendering.LightweightPipeline.LightweightRenderPipelineAsset)
                    {
                        InsertCmdBufferBehaviour_AfterTransparent foregroundAlphaGrabCmdInserter = foregroundCameraList[i].gameObject.AddComponent<InsertCmdBufferBehaviour_AfterTransparent>();
                        foregroundAlphaGrabCmdInserter.Create("AfterTransparentMixCastPass", (c) =>
                        {
                            c.ExecuteCommandBuffer(grabAlphaCommand);
                        });
                        foregroundAlphaGrabCmdInserters.Add(foregroundAlphaGrabCmdInserter);

                        InsertCmdBufferBehaviour_AfterEverything foregroundAlphaSetCmdInserter = foregroundCameraList[i].gameObject.AddComponent<InsertCmdBufferBehaviour_AfterEverything>();
                        foregroundAlphaSetCmdInserter.Create("AfterEverythingMixCastPass", (c) =>
                        {
                            c.ExecuteCommandBuffer(replaceAlphaCommand);
                        });
                        foregroundAlphaSetCmdInserters.Add(foregroundAlphaSetCmdInserter);
                    }
#endif
                }
            }
            else
                LastFrameAlpha = sceneRenderTarget;


            compositingRenderTarget = new RenderTexture(Output.width, Output.height, 0, RenderTextureFormat.ARGBFloat, RenderTextureReadWrite.Linear)
            {
                useMipMap = false,
#if UNITY_5_5_OR_NEWER
                autoGenerateMips = false,
#else
                generateMips = false,
#endif
            };

            if (finalizeFrameCommand != null)
            {
#if UNITY_5_5_OR_NEWER
                finalizeFrameCommand.Blit(compositingRenderTarget as RenderTexture, BuiltinRenderTextureType.CurrentActive);
#else
                finalizeFrameCommand.Blit(compositingRenderTarget as Texture, BuiltinRenderTextureType.CurrentActive);
#endif
            }
        }
        protected override void ReleaseOutput()
        {
            if (frames != null)
            {
                frames.Dispose();
                frames = null;
            }

            if (LastFrameAlpha != null)
            {
                for (int i = 0; i < foregroundCameraList.Count; i++)
                {
                    foregroundCameraList[i].RemoveCommandBuffer(CameraEvent.AfterForwardAlpha, grabAlphaCommand);
                    foregroundCameraList[i].RemoveCommandBuffer(CameraEvent.AfterImageEffects, replaceAlphaCommand);
                }
#if MIXCAST_LWRP
                for( int i = 0; i < foregroundAlphaGrabCmdInserters.Count; i++ ) 
                {
                    Destroy(foregroundAlphaGrabCmdInserters[i]);    
                }
                for( int i = 0; i < foregroundAlphaSetCmdInserters.Count; i++ ) 
                {
                    Destroy(foregroundAlphaSetCmdInserters[i]);    
                }
#endif
                LastFrameAlpha.Release();
                LastFrameAlpha = null;
                grabAlphaCommand = null;
            }
            if (sceneRenderTarget != null)
            {
                sceneRenderTarget.Release();
                sceneRenderTarget = null;
            }
            if (compositingRenderTarget != null)
            {
                compositingRenderTarget.Release();
                compositingRenderTarget = null;
            }

            if (finalizeFrameCommand != null)
                finalizeFrameCommand.Clear();

            base.ReleaseOutput();
        }

        void OnGUI()
        {
            //magic declaration that causes rendering to work
        }


        public override void RenderScene()
        {
            base.RenderScene();

            StartFrame();

            frames.delayDuration = context.Data.bufferTime;
            frames.Update();

            GameFrame newFrame = PrepareNewFrame();

            newFrame.playerDist = SdkCameraUtilities.CalculatePlayerDistance(context.Data);  //Scale distance by camera scale
            RenderBackground(newFrame);
            RenderForeground(newFrame);

            GameFrame bufferedFrame = frames.OldestFrameData;

            GL.LoadIdentity();
            ClearBuffer();
            BlitBackground(bufferedFrame);

            RenderingInputFeed = true;
            for (int i = 0; i < InputFeedProjection.ActiveProjections.Count; i++)
            {
                if (InputFeedProjection.ActiveProjections[i].context.Data == context.Data)
                {
                    RenderInputProjection(InputFeedProjection.ActiveProjections[i]);
                }
            }
            RenderingInputFeed = false;

            BlitForeground(bufferedFrame);

            RenderFinalPass();

            CompleteFrame();
        }

        //Gets an empty frame from the pool and ensures its ready to be filled
        GameFrame PrepareNewFrame()
        {
            GameFrame newFrame = frames.GetNewFrame().Data;

            if (newFrame.backgroundBuffer != null && (newFrame.backgroundBuffer.width != Output.width || newFrame.backgroundBuffer.height != Output.height))
            {
                newFrame.backgroundBuffer.Release();
                newFrame.backgroundBuffer = null;
            }
            if (newFrame.backgroundBuffer == null)
            {
                newFrame.backgroundBuffer = new RenderTexture(Output.width, Output.height, 24, (Output as RenderTexture).format, (Output as RenderTexture).sRGB ? RenderTextureReadWrite.sRGB : RenderTextureReadWrite.Linear);
            }

            if (newFrame.foregroundBuffer != null && (newFrame.foregroundBuffer.width != Output.width || newFrame.foregroundBuffer.height != Output.height))
            {
                newFrame.foregroundBuffer.Release();
                newFrame.foregroundBuffer = null;
            }
            if (newFrame.foregroundBuffer == null)
            {
                newFrame.foregroundBuffer = new RenderTexture(Output.width, Output.height, 24, (Output as RenderTexture).format, (Output as RenderTexture).sRGB ? RenderTextureReadWrite.sRGB : RenderTextureReadWrite.Linear);
            }

            return newFrame;
        }

        Color clearCol = new Color(0, 0, 0, 0);
        void ClearBuffer()
        {
            Graphics.SetRenderTarget(compositingRenderTarget);
            GL.Clear(true, true, clearCol);
            //Graphics.SetRenderTarget(null);
        }

        void RenderBackground(GameFrame targetFrame)
        {
            for (int i = 0; i < backgroundCameraList.Count; i++)
            {
                if (!backgroundCameraList[i].gameObject.activeInHierarchy)
                    continue;

                if (!Mathf.Approximately(backgroundCameraList[i].fieldOfView, context.Data.deviceFoV))
                    backgroundCameraList[i].fieldOfView = context.Data.deviceFoV;

                RenderGameCamera(backgroundCameraList[i], sceneRenderTarget); //render to an anti-aliased buffer
            }
            Graphics.Blit(sceneRenderTarget, targetFrame.backgroundBuffer);  //then transfer to a regular one
        }

        void RequestRenderTexture(RenderTexture src, ref RenderTexture request)
        {
            if (request == null)
            {
                request = new RenderTexture(src.width, src.height, src.depth, src.format, src.sRGB ? RenderTextureReadWrite.sRGB : RenderTextureReadWrite.Linear);
            }
            else if (request.width != src.width || request.height != src.height)
            {
                request.Release();
                request = new RenderTexture(src.width, src.height, src.depth, src.format, src.sRGB ? RenderTextureReadWrite.sRGB : RenderTextureReadWrite.Linear);
            }
        }

        void BlitBackground(GameFrame frame)
        {
            RenderTexture src = frame.backgroundBuffer;

            bool oldSRGB = GL.sRGBWrite;
            GL.sRGBWrite = false;

            if (MixCastSdkData.ProjectSettings.usingPMA)
                blitBackgroundMat.SetTexture("_ForegroundTex", frame.foregroundBuffer);
            blitBackgroundMat.mainTexture = src;
            Graphics.Blit(src, compositingRenderTarget as RenderTexture, blitBackgroundMat);

            GL.sRGBWrite = oldSRGB;
        }

        void RenderForeground(GameFrame targetFrame)
        {
            if (targetFrame.playerDist > 0)
            {
                for (int i = 0; i < foregroundCameraList.Count; i++)
                {
                    if (!foregroundCameraList[i].gameObject.activeInHierarchy)
                        continue;

                    if (!Mathf.Approximately(foregroundCameraList[i].fieldOfView, context.Data.deviceFoV))
                        foregroundCameraList[i].fieldOfView = context.Data.deviceFoV;

                    if (i == 0)
                        foregroundCameraList[i].clearFlags = CameraClearFlags.Color;
                    else
                        foregroundCameraList[i].clearFlags = (foregroundCameraList[i].clearFlags != CameraClearFlags.Nothing) ? CameraClearFlags.Depth : CameraClearFlags.Nothing;
                    foregroundCameraList[i].backgroundColor = Color.clear;
                    foregroundCameraList[i].farClipPlane = Mathf.Max(foregroundCameraList[i].nearClipPlane + 0.001f, foregroundCameraList[i].transform.TransformVector(Vector3.forward).magnitude * targetFrame.playerDist);

                    RenderGameCamera(foregroundCameraList[i], sceneRenderTarget);
                }
                Graphics.Blit(sceneRenderTarget, targetFrame.foregroundBuffer);
            }
            else
            {
                Graphics.Blit(Texture2D.blackTexture, targetFrame.foregroundBuffer);
            }
        }
        void BlitForeground(GameFrame frame)
        {
            bool oldSRGB = GL.sRGBWrite;
            GL.sRGBWrite = true;

            blitForegroundMat.mainTexture = frame.foregroundBuffer;
            Graphics.Blit(frame.foregroundBuffer, compositingRenderTarget as RenderTexture, blitForegroundMat);

            GL.sRGBWrite = oldSRGB;
        }

        void RenderInputProjection(InputFeedProjection feedProjection)
        {
            if (!feedProjection.CanRender)
                return;

            feedProjection.HandleRenderStarted(gameCamera);
            Graphics.Blit(feedProjection.InputTexture, compositingRenderTarget as RenderTexture, feedProjection.ProjectionMaterial);
        }

        void RenderFinalPass()
        {
            if (finalCam != null)
            {
                finalCam.AddCommandBuffer(CameraEvent.BeforeImageEffectsOpaque, finalizeFrameCommand);

                finalCam.targetTexture = Output as RenderTexture;
                finalCam.aspect = (float)Output.width / Output.height;
                finalCam.Render();

                finalCam.RemoveCommandBuffer(CameraEvent.BeforeImageEffectsOpaque, finalizeFrameCommand);
            }
            else
            {
                Graphics.Blit(compositingRenderTarget, Output as RenderTexture);
            }
        }
    }
}
#endif
