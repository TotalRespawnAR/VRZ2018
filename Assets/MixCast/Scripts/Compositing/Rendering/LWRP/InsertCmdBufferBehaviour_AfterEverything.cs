#if UNITY_STANDALONE_WIN && MIXCAST_LWRP
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Experimental.Rendering.LightweightPipeline;

namespace BlueprintReality.MixCast
{
    public class InsertCmdBufferBehaviour_AfterEverything : InsertCmdBufferBehaviour, IAfterRender
    {
        public ScriptableRenderPass GetPassToEnqueue()
        {
            return GetPass(RenderTargetHandle.CameraTarget, null);
        }
    }
}
#endif