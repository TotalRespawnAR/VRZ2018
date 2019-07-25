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
using UnityEngine;
using UnityEngine.UI;

namespace BlueprintReality.MixCast
{
    public class SetScaleFromViewfinder: CameraComponent
    {
        public enum Mode
        {
            StretchWidth, StretchHeight
        }

        public Mode mode = Mode.StretchWidth;

        private MixCastCamera cam;

        protected override void OnEnable()
        {
            base.OnEnable();

            HandleDataChanged();
        }

        protected override void HandleDataChanged()
        {
            base.HandleDataChanged();

            LateUpdate();
        }

        private void LateUpdate()
        {
            if (cam == null || cam.context.Data != context.Data || !cam.isActiveAndEnabled)
                cam = MixCastCamera.FindCamera(context);

            if (cam != null && cam.Output != null)
            {
                Vector3 localScale = transform.localScale;
                if (mode == Mode.StretchWidth)
                    localScale.x = localScale.y * cam.Output.width / cam.Output.height;
                else
                    localScale.y = localScale.x * cam.Output.height / cam.Output.width;
                transform.localScale = localScale;
            }
        }
    }
}
#endif
