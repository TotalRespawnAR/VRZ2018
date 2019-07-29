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

#if UNITY_STANDALONE_WIN && MIXCAST_LWRP
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Experimental.Rendering.LightweightPipeline;
using UnityEngine.Rendering;

namespace BlueprintReality.MixCast
{
    public class InsertCmdBufferBehaviour : MonoBehaviour
    {
        public delegate void ExecuteFunc(ScriptableRenderContext context);

        public string PassTag { get; protected set; }
        public ExecuteFunc Execute { get; protected set; }

        private Dictionary<int, GenericRenderPassImpl> impls = new Dictionary<int, GenericRenderPassImpl>();

        public void Create(string passTag, ExecuteFunc execute)
        {
            Execute = execute;
            PassTag = passTag;
        }

        protected ScriptableRenderPass GetPass(RenderTargetHandle colorHandle, RenderTargetHandle? depthHandle)
        {
            if (!impls.ContainsKey(colorHandle.id))
                impls[colorHandle.id] = new GenericRenderPassImpl(this, colorHandle, depthHandle);
            return impls[colorHandle.id];
        }
    }

    public class GenericRenderPassImpl : ScriptableRenderPass
    {
        private InsertCmdBufferBehaviour m_behaviour;
        private RenderTargetHandle? m_colorHandle, m_depthHandle;

        public GenericRenderPassImpl(InsertCmdBufferBehaviour behaviour, RenderTargetHandle colorHandle, RenderTargetHandle? depthHandle)
        {
            m_behaviour = behaviour;
            m_colorHandle = colorHandle;
            m_depthHandle = depthHandle;
        }

        public override void Execute(ScriptableRenderer renderer, ScriptableRenderContext context, ref RenderingData renderingData)
        {
            if (!m_behaviour.enabled)
                return;

            CommandBuffer cmd = CommandBufferPool.Get(m_behaviour.PassTag);

            if (m_colorHandle.HasValue)
            {
                if (m_depthHandle.HasValue)
                    cmd.SetRenderTarget(m_colorHandle.Value.Identifier(), m_depthHandle.Value.Identifier());
                else
                    cmd.SetRenderTarget(m_colorHandle.Value.Identifier());
            }
            else if (m_depthHandle.HasValue)
                cmd.SetRenderTarget(m_depthHandle.Value.Identifier());

            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);

            m_behaviour.Execute(context);
        }
    }
}
#endif
