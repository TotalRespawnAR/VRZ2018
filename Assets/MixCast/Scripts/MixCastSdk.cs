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
using BlueprintReality.MixCast.Thrift;
using System;
using System.Collections;
using UnityEngine;

namespace BlueprintReality.MixCast
{
    public static class MixCastSdk
    {
        public const string VERSION_STRING = "2.2.3";

        public const string WEBSITE_URL = "https://mixcast.me/route.php?dest=mixcast";

        private static bool active = false;
        public static event Action OnActiveChanged;
        public static bool Active
        {
            get
            {
                return active;
            }
            set
            {
                if (value == active)
                    return;

                active = value;
                if (OnActiveChanged != null)
                    OnActiveChanged();
            }
        }

        public static event Action<string> OnEventSent;
        public static void SendCustomEvent(string eventId)
        {
            if (Active)
            {
                UnityThriftMixCastClient.Get<SDK_Service.Client>().TrySendExperienceEvent(eventId);
                if (OnEventSent != null)
                    OnEventSent(eventId);
            }
        }
        public static void SendCustomEventAfterDelay(string eventId, float delay)
        {
            if (MixCastSdkBehaviour.Instance != null)
                MixCastSdkBehaviour.Instance.StartCoroutine(DelayCall(() => SendCustomEvent(eventId), delay));
            else
                Debug.LogError("Can't make a call to the MixCast SDK with no MixCastSdkBehaviour component present!");
        }

        public static void TakeSnapshotFromCamera(VirtualCamera camera)
        {
            if (Active)
                UnityThriftMixCastClient.Get<SDK_Service.Client>().TryRequestTakeSnapshot(camera.Identifier);
            else
                Debug.LogError("MixCast isn't running!");
        }

#if UNITY_EDITOR
        [UnityEditor.MenuItem("MixCast/Go to Website", priority = 1)]
#endif
        public static void GoToWebsite()
        {
            Application.OpenURL(WEBSITE_URL);
        }

        //Returns true if compareTo is a later version than current
        public static bool IsVersionBLaterThanVersionA(string versionA, string versionB)
        {
            if (versionA == versionB)
                return false;

            string[] versionBNums = versionB.Split('.');
            string[] versionANums = versionA.Split('.');

            for (int i = 0; i < versionBNums.Length && i < versionANums.Length; i++)
            {
                try
                {
                    int versionBNum = int.Parse(versionBNums[i]);
                    int versionANum = int.Parse(versionANums[i]);

                    if (versionBNum > versionANum)
                        return true;
                    else if (versionBNum < versionANum)
                        return false;

                }
                catch (FormatException)
                {
                    Debug.LogError("version check failed due to invalid string format");
                    return false;
                }
            }

            if (versionBNums.Length > versionANums.Length)
                return true;

            return false;
        }

        private static IEnumerator DelayCall(Action call, float delay)
        {
            yield return new WaitForSeconds(delay);
            call();
        }
    }
}
#endif
