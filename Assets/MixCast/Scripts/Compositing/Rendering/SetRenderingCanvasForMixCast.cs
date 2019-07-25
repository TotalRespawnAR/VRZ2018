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
    [RequireComponent(typeof(CanvasGroup))]
    public class SetRenderingCanvasForMixCast : MonoBehaviour
    {
        public bool renderForMixedReality = false;
        public bool renderForThirdPerson = false;

        private CanvasGroup target;
        private float restoreAlpha;

        protected virtual void OnEnable()
        {
            MixCastCamera.GameRenderStarted += HandleMixCastRenderStarted;
            MixCastCamera.GameRenderEnded += HandleMixCastRenderEnded;

            target = GetComponent<CanvasGroup>();
        }
        protected virtual void OnDisable()
        {
            MixCastCamera.GameRenderStarted -= HandleMixCastRenderStarted;
            MixCastCamera.GameRenderEnded -= HandleMixCastRenderEnded;
        }


        private void HandleMixCastRenderStarted(MixCastCamera cam)
        {
            restoreAlpha = target.alpha;
            target.alpha = (CameraHasInput(cam) ? renderForMixedReality : renderForThirdPerson) ? 1 : 0;
        }
        private void HandleMixCastRenderEnded(MixCastCamera cam)
        {
            target.alpha = restoreAlpha;
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
    }
}
#endif
