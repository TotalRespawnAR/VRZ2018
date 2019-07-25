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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace BlueprintReality.MixCast
{
    public class SharedTexturePluginUpdater : MonoBehaviour
    {
        private static bool created = false;
        public static void EnsureExists()
        {
            if (!created)
            {
                new GameObject().AddComponent<SharedTexturePluginUpdater>();
                created = true;
            }
        }

        private void Awake()
        {
            name = GetType().Name;
            hideFlags = HideFlags.HideAndDontSave;
            DontDestroyOnLoad(gameObject);
            StartCoroutine("CallPluginAtEndOfFrames");
        }

        private IEnumerator CallPluginAtEndOfFrames()
        {
            YieldInstruction wait = new WaitForEndOfFrame();
            while (true)
            {
                // Wait until all frame rendering is done
                yield return wait;

                GL.IssuePluginEvent(GetRenderEventFunc(), 1);
            }
        }

        [DllImport("ExTexture")]
        private static extern IntPtr GetRenderEventFunc();
    }
}
