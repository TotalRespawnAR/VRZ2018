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
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace BlueprintReality.MixCast.SharedTexture
{
    public class SharedTextureReceiving
    {
        [DllImport("ExTexture")]
        private static extern void RegisterRemoteTexturePair(IntPtr texture, ulong sharedHandle);

        [DllImport("ExTexture")]
        private static extern void UnregisterRemoteTexturePair(IntPtr texture);

        private static SharpDX.Direct3D11.Device d3d_device = null;
        public static SharpDX.Direct3D11.Device D3D_Device
        {
            get
            {
                if (d3d_device == null)
                {
                    var smallTex = new Texture2D(2, 2);
                    using (var smallTexDX = new SharpDX.Direct3D11.Texture2D(smallTex.GetNativeTexturePtr()))
                    {
                        d3d_device = new SharpDX.Direct3D11.Device(smallTexDX.Device.NativePointer);
                    }
                    UnityEngine.Object.Destroy(smallTex);
                }
                return d3d_device;
            }
        }

        public static SharpDX.DXGI.Format GetSharpDXFormat(int format)
        {
            return (SharpDX.DXGI.Format)format;
        }

        private class SharpDXBundle
        {
            public SharpDX.Direct3D11.Texture2D dxTexture;
            public SharpDX.Direct3D11.ShaderResourceView dxSRV;
        }

        private static Dictionary<Texture, long> remoteTextureHandleTable = new Dictionary<Texture, long>();
        private static Dictionary<Texture, SharpDXBundle> remoteTextureTable = new Dictionary<Texture, SharpDXBundle>();

        public static bool TryRequest(string texId, ref Texture texture, ref SharedTex info, bool automaticallyCleanOldTexture = true)
        {
            SharedTexturePluginUpdater.EnsureExists();

            if (SharedTextureSending.TryGetLocalTexture(texId, ref texture))
                return true;

            info = null;
            bool result = UnityThriftMixCastClient.Get<SharedTextureCommunication.Client>().TrySharedTextureRequest(texId, out info);
            if( !result )
            {
                info = null;
                Debug.LogError("Fail to request the required shared texture");
            }
            return RespondToTextureUpdate(texId, info, ref texture, automaticallyCleanOldTexture);
        }

        public static bool RespondToTextureUpdate(string texId, SharedTex info, ref Texture texture, bool automaticallyCleanOldTexture = true)
        {
            if (info == null || info.Handle == 0 || info.Width == 0 || info.Height == 0 || info.Format == 0)
            {
                if (texture != null)
                {
                    remoteTextureHandleTable.Remove(texture);
                    if (automaticallyCleanOldTexture)
                    {
                        TryRelease(ref texture);
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }

            if (texture != null && texture.width == info.Width && texture.height == info.Height)
            {
                long oldHandle = remoteTextureHandleTable[texture];
                if (oldHandle == info.Handle)
                {
                    // Nothing change
                    return false;
                }
            }

            if (texture != null)
            {
                remoteTextureHandleTable.Remove(texture);
                if (automaticallyCleanOldTexture)
                {
                    TryRelease(ref texture);
                }
            }

            TextureFormat fmt = TextureFormat.RGBA32;
            if (CheckFormat(info.Format, ref fmt))
            {
                texture = new Texture2D(info.Width, info.Height, fmt, false, true);
                (texture as Texture2D).Apply();
                remoteTextureHandleTable.Add(texture, info.Handle);

                RegisterRemoteTexturePair(texture.GetNativeTexturePtr(), (ulong)info.Handle);

                return true;
            }
            else
            {
                var desc = new SharpDX.Direct3D11.Texture2DDescription();
                desc.Width = info.Width;
                desc.Height = info.Height;
                desc.MipLevels = 1;
                desc.ArraySize = 1;
                desc.BindFlags = SharpDX.Direct3D11.BindFlags.ShaderResource | SharpDX.Direct3D11.BindFlags.RenderTarget;
                desc.OptionFlags = SharpDX.Direct3D11.ResourceOptionFlags.None;
                desc.Usage = SharpDX.Direct3D11.ResourceUsage.Default;
                desc.Format = (SharpDX.DXGI.Format)info.Format;
                desc.SampleDescription = new SharpDX.DXGI.SampleDescription(1, 0);
                var TextureDX = new SharpDX.Direct3D11.Texture2D(D3D_Device, desc);
                var TextureSRV = new SharpDX.Direct3D11.ShaderResourceView(D3D_Device, TextureDX);
                texture = Texture2D.CreateExternalTexture(info.Width, info.Height, fmt, false, true, TextureSRV.NativePointer);
                remoteTextureHandleTable.Add(texture, info.Handle);

                RegisterRemoteTexturePair(TextureDX.NativePointer, (ulong)info.Handle);

                var sharpdx_bundle = new SharpDXBundle();
                sharpdx_bundle.dxTexture = TextureDX;
                sharpdx_bundle.dxSRV = TextureSRV;
                remoteTextureTable.Add(texture, sharpdx_bundle);
                return true;
            }
        }

        public static bool TryRelease(ref Texture requestedTexture)
        {
            if (requestedTexture == null)
            {
                Debug.LogError("Cannot release an empty texture");
                return false;
            }

            SharedTexturePluginUpdater.EnsureExists();

            UnregisterRemoteTexturePair(requestedTexture.GetNativeTexturePtr());

            if (remoteTextureTable.ContainsKey(requestedTexture))
            {
                try
                {
                    var bundle = remoteTextureTable[requestedTexture];
                    remoteTextureTable.Remove(requestedTexture);

                    if (requestedTexture != null)
                    {
                        MonoBehaviour.Destroy(requestedTexture);
                        requestedTexture = null;
                    }

                    if (bundle.dxSRV != null)
                    {
                        bundle.dxSRV.Dispose();
                        bundle.dxSRV = null;
                    }
                    if (bundle.dxTexture != null)
                    {
                        bundle.dxTexture.Dispose();
                        bundle.dxTexture = null;
                    }
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                    requestedTexture = null;
                    return false;
                }
                return true;
            }
            else
            {
                if (requestedTexture != null)
                {
                    MonoBehaviour.Destroy(requestedTexture);
                    requestedTexture = null;
                }
                return true;
            }
        }

        // Add this to behave similar to Capture
        public static bool CopyNativeTexture(Texture2D src, int srcFmt, ref Texture2D dst, ref int dstFmt)
        {
            if (src == null)
            {
                if (dst != null)
                    UnityEngine.Object.Destroy(dst);
                dst = null;
                dstFmt = 0;
                return false;
            }

            if (dst == null || src.width != dst.width || src.height != dst.height || srcFmt != dstFmt)
            {
                dstFmt = srcFmt;
                if (dst != null)
                    UnityEngine.Object.Destroy(dst);

                dst = new Texture2D(src.width, src.height, src.format, false, true);
                dst.Apply();
            }

            TextureFormat fmt = TextureFormat.RGBA32;
            if (CheckFormat(dstFmt, ref fmt))
            {
                Graphics.CopyTexture(src, dst);
            }
            else
            {
#if UNITY_2017_1_OR_NEWER
                Graphics.ConvertTexture(src, dst);
#else

                Debug.LogError("Graphics API is not supported on old Unity version (before 2017.1)");
                return false;
#endif
            }
            return true;
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
    }
}
#endif
