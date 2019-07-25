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

namespace BlueprintReality.MixCast
{
    public class SetRenderingForMixCast : MonoBehaviour
    {
        public List<Renderer> targets = new List<Renderer>();
        [UnityEngine.Serialization.FormerlySerializedAs("renderForMixCast")]
        public bool renderForMixedReality = false;
        public bool renderForThirdPerson = false;

        protected virtual void OnEnable()
        {
            MixCastCamera.GameRenderStarted += HandleMixCastRenderStarted;
            MixCastCamera.GameRenderEnded += HandleMixCastRenderEnded;

            if (targets.Count == 0)
                GetComponentsInChildren<Renderer>(targets);

            //Set targets to the desired state during standard Unity rendering (not MixCast)
            targets.ForEach(r => r.enabled = !(renderForMixedReality || renderForThirdPerson));
        }
        protected virtual void OnDisable()
        {
            MixCastCamera.GameRenderStarted -= HandleMixCastRenderStarted;
            MixCastCamera.GameRenderEnded -= HandleMixCastRenderEnded;
        }


        private void HandleMixCastRenderStarted(MixCastCamera cam)
        {
            bool enable = CameraHasInput(cam) ? renderForMixedReality : renderForThirdPerson;
            for (int i = 0; i < targets.Count; i++)
            {
                targets[i].enabled = enable;
            }
        }
        private void HandleMixCastRenderEnded(MixCastCamera cam)
        {
            bool enable = CameraHasInput(cam) ? renderForMixedReality : renderForThirdPerson;
            for (int i = 0; i < targets.Count; i++)
            {
                targets[i].enabled = !enable;
            }
        }

        protected bool CameraHasInput(MixCastCamera cam)
        {
            for (int i = 0; i < MixCastSdkData.Cameras.Count; i++)
            {
                if (MixCastSdkData.Cameras[i].Identifier == cam.context.Data.id)
                    return MixCastSdkData.Cameras[i].VideoInputIds != null && MixCastSdkData.Cameras[i].VideoInputIds.Count > 0;
            }
            return false;
        }


        private void Reset()
        {
            targets.Clear();
            GetComponentsInChildren<Renderer>(targets);
        }
    }
}
#endif
