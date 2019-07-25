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
using UnityEngine;
using UnityEngine.Rendering;

namespace BlueprintReality.MixCast
{
    public class QuadrantMixCastCamera : MixCastCamera
    {
        public const string INSERT_DEPTH_SHADER = "Hidden/MixCast/InsertInputDepth";
        public const string ALPHA_OUT_SHADER = "Hidden/BPR/AlphaOut";
        public const string COPY_ALPHA_CHANNEL_SHADER = "Hidden/BPR/AlphaTransfer";

        public Color clearColor = new Color(0, 0, 0, 0);

        private Material straightBlitMat;
        private Material alphaOutBlit;

        private Material postBlit;
        private RenderTexture quadrantTex;
        private RenderTexture fpTex;
        private RenderTexture lastFrameAlpha;

        CommandBuffer captureFP;

        private CommandBuffer insertInputDepthCmd;
        private Material insertInputDepthMat;
        private SharedTextureReceiver inputDepthReceiver;

        private CommandBuffer grabAlphaCommand;
        private Material copyAlphaMat;

        protected void Awake()
        {
            straightBlitMat = new Material(Shader.Find("Unlit/Texture"));

            alphaOutBlit = new Material(Shader.Find(ALPHA_OUT_SHADER));
            postBlit = new Material(Shader.Find("Hidden/BPR/AlphaWrite"));

            insertInputDepthMat = new Material(Shader.Find(INSERT_DEPTH_SHADER));
            insertInputDepthCmd = new CommandBuffer() { name = "Insert Input Depth" };

            grabAlphaCommand = new CommandBuffer();
            grabAlphaCommand.name = "Get Correct Alpha";
            copyAlphaMat = new Material(Shader.Find(COPY_ALPHA_CHANNEL_SHADER));
        }

        protected override void HandleDataChanged()
        {
            base.HandleDataChanged();
            UnregisterDepthReceiver();
            RegisterDepthReceiver();
        }

        void RegisterDepthReceiver()
        {
            if (context.Data != null)
            {
                inputDepthReceiver = new SharedTextureReceiver(context.Data.id + "_input_depth");
                Service_SDK_Handler.OnExternalTextureUpdated += inputDepthReceiver.RefreshTextureFromEvent;
            }
        }
        void UnregisterDepthReceiver()
        {
            if (inputDepthReceiver != null)
            {
                Service_SDK_Handler.OnExternalTextureUpdated -= inputDepthReceiver.RefreshTextureFromEvent;
                inputDepthReceiver.Dispose();
                inputDepthReceiver = null;
            }
        }

        protected override void BuildOutput()
        {
            base.BuildOutput();

            quadrantTex = new RenderTexture(Mathf.CeilToInt(Output.width * 0.5f), Mathf.CeilToInt(Output.height * 0.5f), 24, (Output as RenderTexture).format);

            //Capture the headset camera's output
            fpTex = new RenderTexture(quadrantTex.width, quadrantTex.height, 0);
            captureFP = new CommandBuffer();
            captureFP.Blit(BuiltinRenderTextureType.CameraTarget, fpTex);
            Camera.main.AddCommandBuffer(CameraEvent.AfterEverything, captureFP);

            if (MixCastSdkData.ProjectSettings.grabUnfilteredAlpha)
            {
                lastFrameAlpha = new RenderTexture(Output.width, Output.height, 0)
                {
                    useMipMap = false,
#if UNITY_5_5_OR_NEWER
                    autoGenerateMips = false,
#else
                    generateMips = false,
#endif
                };
                lastFrameAlpha.Create();
                grabAlphaCommand.Blit(BuiltinRenderTextureType.CurrentActive, lastFrameAlpha);
            }
        }
        protected override void ReleaseOutput()
        {
            if (fpTex != null)
            {
                if( Camera.main != null )
                    Camera.main.RemoveCommandBuffer(CameraEvent.AfterEverything, captureFP);
                fpTex.Release();
                fpTex = null;
                captureFP.Release();
                captureFP = null;
            }

            if ( quadrantTex != null )
            {
                quadrantTex.Release();
                quadrantTex = null;
            }

            if (lastFrameAlpha != null)
            {
                lastFrameAlpha.Release();
                lastFrameAlpha = null;
                grabAlphaCommand.Clear();
            }

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

            GL.LoadIdentity();
            ClearBuffer();

            //Reusing RT for background and foreground, so perform blit right after render
            RenderBackground();
            BlitBackground();

            RenderForeground();
            BlitForeground();

            //BlitFirstPerson();

            Graphics.Blit(null, postBlit);  //clear alpha
            CompleteFrame();
        }

        void ClearBuffer()
        {
            Graphics.SetRenderTarget(Output as RenderTexture);
            GL.Clear(true, true, clearColor);
        }
        void RenderBackground()
        {
            RenderGameCamera(gameCamera, quadrantTex);
        }
        void RenderForeground()
        {
            //Foreground is empty if no cutout is required
            if (inputDepthReceiver.Texture == null)
            {
                Graphics.SetRenderTarget(quadrantTex);
                GL.Clear(true, true, new Color(0, 0, 0, 0));
                return;
            }

            CameraClearFlags oldFlags = gameCamera.clearFlags;
            gameCamera.clearFlags = CameraClearFlags.Depth;

            insertInputDepthCmd.Clear();
            insertInputDepthCmd.Blit(inputDepthReceiver.Texture, BuiltinRenderTextureType.CameraTarget, insertInputDepthMat);
            gameCamera.AddCommandBuffer(CameraEvent.BeforeForwardOpaque, insertInputDepthCmd);

            if (MixCastSdkData.ProjectSettings.grabUnfilteredAlpha)
                gameCamera.AddCommandBuffer(CameraEvent.BeforeImageEffects, grabAlphaCommand);  //Instruction to copy out the state of the RenderTexture before Image Effects are applied

            RenderGameCamera(gameCamera, quadrantTex);

            if (MixCastSdkData.ProjectSettings.grabUnfilteredAlpha)
            {
                gameCamera.RemoveCommandBuffer(CameraEvent.BeforeImageEffects, grabAlphaCommand);
                Graphics.Blit(lastFrameAlpha, quadrantTex, copyAlphaMat);      //Overwrite the potentially broken post-effects alpha channel with the pre-effect copy
            }

            gameCamera.RemoveCommandBuffer(CameraEvent.BeforeForwardOpaque, insertInputDepthCmd);

            gameCamera.clearFlags = oldFlags;
        }
        void BlitBackground()
        {
            bool oldSRGB = GL.sRGBWrite;
            GL.sRGBWrite = true;
            Graphics.SetRenderTarget(Output as RenderTexture);
            Graphics.DrawTexture(new Rect(0, Screen.height * 0.5f, Screen.width * 0.5f, Screen.height * 0.5f), quadrantTex, straightBlitMat);
            GL.sRGBWrite = oldSRGB;
        }
        void BlitForeground()
        {
            bool oldSRGB = GL.sRGBWrite;
            GL.sRGBWrite = true;
            Graphics.SetRenderTarget(Output as RenderTexture);
            Graphics.DrawTexture(new Rect(0, 0, Screen.width * 0.5f, Screen.height * 0.5f), quadrantTex, straightBlitMat);
            Graphics.DrawTexture(new Rect(Screen.width * 0.5f, 0, Screen.width * 0.5f, Screen.height * 0.5f), quadrantTex, alphaOutBlit);
            GL.sRGBWrite = oldSRGB;
        }

        void BlitFirstPerson()
        {
            Graphics.SetRenderTarget(Output as RenderTexture);
            Graphics.DrawTexture(new Rect(Screen.width * 0.5f, Screen.height * 0.5f, Screen.width * 0.5f, Screen.height * 0.5f), fpTex);
        }
    }
}
#endif
