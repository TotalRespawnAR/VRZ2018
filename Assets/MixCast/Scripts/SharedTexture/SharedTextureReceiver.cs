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

using BlueprintReality.MixCast;
using BlueprintReality.MixCast.Data;
using BlueprintReality.MixCast.SharedTexture;
using System;
using UnityEngine;

namespace BlueprintReality.MixCast
{
    [Serializable]
    public class SharedTextureReceiver : IDisposable
    {
        public string TextureId { get; protected set; }

        private Texture texture;
        public Texture Texture { get { return texture; } }

        private SharedTex textureInfo;

        public SharedTextureReceiver(string texId)
        {
            TextureId = texId;
            SharedTextureReceiving.TryRequest(TextureId, ref texture, ref textureInfo);
            SharedTextureSending.OnTextureSent += RefreshTextureFromEvent;
        }
        public void Dispose()
        {
            if (Texture != null)
                SharedTextureReceiving.TryRelease(ref texture);
            SharedTextureSending.OnTextureSent -= RefreshTextureFromEvent;
        }

        public void RefreshTextureInfo()
        {
            SharedTextureReceiving.TryRequest(TextureId, ref texture, ref textureInfo);
        }
        //Useful for streamlining event driven updates
        public void RefreshTextureFromEvent(string evTextureId, SharedTex evTextureInfo)
        {
            if (TextureId == evTextureId)
            {
                if (SharedTextureSending.TryGetLocalTexture(TextureId, ref texture))
                    return;
                SharedTextureReceiving.RespondToTextureUpdate(TextureId, evTextureInfo, ref texture);     //passing in new data
                textureInfo = evTextureInfo;
            }
        }

        public void RefreshTextureObject()
        {
            SharedTextureReceiving.RespondToTextureUpdate(TextureId, textureInfo, ref texture);           //pass in existing data
        }
    }
}
