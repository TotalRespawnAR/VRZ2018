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
using BlueprintReality.MixCast.Thrift;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace BlueprintReality.MixCast.SharedTexture
{
    public class SharedTextureSending : MonoBehaviour
    {
        static bool exists = false;
        private static void EnsureExists()
        {
            if (!exists)
            {
                new GameObject().AddComponent<SharedTextureSending>();
                exists = true;
            }
        }

        private class RegisteredLocalTexture
        {
            public Texture source;
            public Texture destination;
            public string texId;
        }

        private void Awake()
        {
            name = GetType().Name;
            gameObject.hideFlags = HideFlags.HideAndDontSave;
            DontDestroyOnLoad(gameObject);
            StartCoroutine("CallCopyLocalTextures");
        }

        private static Dictionary<string, RegisteredLocalTexture> registeredLocalTextures = new Dictionary<string, RegisteredLocalTexture>();
        public static event Action<string, SharedTex> OnTextureSent;

        private IEnumerator CallCopyLocalTextures()
        {
            while (true)
            {
                CopyLocalTextures();
                yield return null;
            }
        }

        private void CopyLocalTextures()
        {
            foreach (var label in registeredLocalTextures.Values)
            {
                if (label.source != null && label.destination != null)
                {
                    Graphics.CopyTexture(label.source, label.destination);
                }
            }
        }

        private static bool CheckFormat(int format, ref TextureFormat convFmt)
        {
            if (format == 28)
            {
                convFmt = TextureFormat.RGBA32;
                return true;
            }
            else if (format == 87)
            {
                convFmt = TextureFormat.BGRA32;
                return true;
            }
            else if (format == 56)
            {
                convFmt = TextureFormat.R16;
                return true;
            }
            else if (format == 2)
            {
                convFmt = TextureFormat.RGBAFloat;
                return true;
            }
            else if (format == 24)
            {
                //Debug.LogWarning("Unreal Back Buffer Format");
                convFmt = TextureFormat.RGBA32;
                return false;
            }
            else
            {
                // Add more pixel format support
                Debug.LogError("Unsupported Pixel Format:" + format + "!!!");
                convFmt = TextureFormat.RGBA32;
                return false;
            }
        }

        [DllImport("ExTexture")]
        private static extern IntPtr CreateSharedTexture(IntPtr texture, ref int width, ref int height, ref int format, ref ulong handle);

        private static SharedTex getSharedTex(Texture texture, out IntPtr srv)
        {
            srv = IntPtr.Zero;

            if (texture == null)
            {
                return null;
            }

            int width = 0;
            int height = 0;
            int format = 0;
            ulong handle = 0;

            try
            {
                srv = CreateSharedTexture(texture.GetNativeTexturePtr(), ref width, ref height, ref format, ref handle);
            }
            catch
            {
                Debug.LogError("Register Local Texture failed");
                return null;
            }
            if (srv == IntPtr.Zero || handle <= 0)
            {
                Debug.LogError("Register Local Texture failed");
                return null;
            }
            return new SharedTex((long)handle, width, height, format);
        }

        public static void RegisterLocal(string texId, Texture texture)
        {
            EnsureExists();
            SharedTexturePluginUpdater.EnsureExists();

            IntPtr srv = IntPtr.Zero;
            SharedTex tex = getSharedTex(texture, out srv);
            bool result = (tex != null) && (srv != IntPtr.Zero) && UnityThriftMixCastClient.Get<SharedTextureCommunication.Client>().TrySharedTextureNotify(texId, tex);
            if (result)
            {
                TextureFormat convFormat = TextureFormat.RGBA32;
                CheckFormat(tex.Format, ref convFormat);
                Texture newDst = Texture2D.CreateExternalTexture(tex.Width, tex.Height, convFormat, false, true, srv);
                RegisteredLocalTexture label = new RegisteredLocalTexture();
                label.source = texture;
                label.destination = newDst;
                label.texId = texId;
                registeredLocalTextures.Add(texId, label);
                if (OnTextureSent != null)
                    OnTextureSent(texId, tex);
            }
        }

        public static bool UnregisterLocal(string texId)
        {
            if (string.IsNullOrEmpty(texId))
                return false;

            EnsureExists();
            SharedTexturePluginUpdater.EnsureExists();

            if (registeredLocalTextures.ContainsKey(texId))
            {
                var label = registeredLocalTextures[texId];
                registeredLocalTextures.Remove(texId);
                SharedTex emptyTex = new SharedTex(0, 0, 0, 0);
                bool res = UnityThriftMixCastClient.Get<SharedTextureCommunication.Client>().TrySharedTextureNotify(label.texId, emptyTex);
                if (OnTextureSent != null)
                    OnTextureSent(texId, emptyTex);
                return res;
            }
            else
            {
                Debug.LogError("No Local Texture found for ID: " + texId);
                return false;
            }
        }

        public static bool TryGetLocalTexture(string texId, ref Texture texture)
        {
            if (!registeredLocalTextures.ContainsKey(texId))
                return false;
            texture = registeredLocalTextures[texId].source;
            return true;
        }
    }
}
#endif
