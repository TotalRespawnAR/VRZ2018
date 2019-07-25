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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlueprintReality.MixCast.Data
{
    public static class MixCastSdkData
    {
        private const string PROJECT_SETTINGS_FILENAME = "MixCast_ProjectSettings";

        //Build-time
        public static MixCastProjectSettings ProjectSettings { get; private set; }

        //Runtime 
        public static ExperienceMetadata ExperienceInfo { get; set; }

        public static List<TrackedObject> TrackedObjects { get; set; }
        public static TrackedObject GetTrackedObjectWithId(string id)
        {
            for (int i = 0; i < TrackedObjects.Count; i++)
                if (TrackedObjects[i].Identifier == id)
                    return TrackedObjects[i];
            return null;
        }

        public static Desktop Desktop { get; set; }
        public static List<Viewfinder> Viewfinders { get; set; }

        public static List<VirtualCamera> Cameras { get; set; }
        public static VirtualCamera GetCameraWithId(string id)
        {
            for (int i = 0; i < Cameras.Count; i++)
                if (Cameras[i].Identifier == id)
                    return Cameras[i];
            return null;
        }

        static MixCastSdkData()
        {
            ExperienceInfo = new ExperienceMetadata();

            TrackedObjects = new List<TrackedObject>();
            Desktop = new Desktop()
            {
                MaxDisplayingCameras = 1,
                DisplayingCameraIds = new List<string>(),
                UiZoomFactor = 1
            };
            Viewfinders = new List<Viewfinder>();
            Cameras = new List<VirtualCamera>();

            ProjectSettings = Resources.Load<MixCastProjectSettings>(PROJECT_SETTINGS_FILENAME);
        }
    }
}
#endif
