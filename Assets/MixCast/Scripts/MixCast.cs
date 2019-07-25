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

#if  UNITY_STANDALONE_WIN
using BlueprintReality.MixCast.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BlueprintReality.MixCast
{
    public class MixCast
    {
        public static MixCastData Settings { get; protected set; }
        public static event Action OnSettingsReloaded;

        public static void LoadSettings(string dataJson)
        {
            Settings = dataJson != null ? JsonUtility.FromJson<MixCastData>(dataJson) : null;
            if (Settings == null)
                Settings = new MixCastData();

            if (OnSettingsReloaded != null)
                OnSettingsReloaded();
        }
    }
}
#endif
