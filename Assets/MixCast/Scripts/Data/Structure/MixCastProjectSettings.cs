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

using UnityEngine;

namespace BlueprintReality.MixCast
{
    public class MixCastProjectSettings : ScriptableObject
#if UNITY_EDITOR
        , ISerializationCallbackReceiver
#endif
    {
        //Generated
        [SerializeField]
        private string projectId;
        public string ProjectID { get { return projectId; } }

        //Rendering
        [Tooltip("If this is set, MixCast will render the scene through the provided Camera rather than a default. Can't be modified at runtime.")]
        public GameObject layerCamPrefab;
        [Tooltip("If this is set, MixCast will render the composited image through the provided Camera for final processing. Can't be modified at runtime.")]
        public GameObject finalPassCamPrefab;

        //Transparency
        [Tooltip("This signals whether your transparent materials write Premultiplied Alpha to the alpha channel. This results in more accurate compositing of virtual objects in the foreground")]
        public bool usingPMA = false;
        [Tooltip("If this is set, MixCast will use the alpha channel before it is modified by (most) Image Effects, rather than the final values after rendering completes")]
        public bool grabUnfilteredAlpha = true;

        //Quality
        [Tooltip("This signals whether to take the Anti-aliasing parameters you've configured in the Quality section of your Unity Project Settings, or to use custom parameters. Can't be modified at runtime.")]
        public bool overrideQualitySettingsAA = true;
        [Tooltip("The Anti-aliasing factor to use in MixCast scene rendering. Can't be modified at runtime.")]
        public int overrideAntialiasingVal = 0;

        //Effects
        [Tooltip("This value tells MixCast which Light components should apply to the player (using their Culling Mask field)")]
        public string subjectLayerName = "Default";
        [Tooltip("This signals whether to only apply virtual lighting from Lights with the MixCastLight component attached (better performance but manual) or to scan the scene for Lights automatically")]
        public bool specifyLightsManually = false;
        [Tooltip("This value allows you to tweak the strength of the effect that Directional Lights have on the user in Mixed Reality")]
        public float directionalLightPower = 1f;
        [Tooltip("This value allows you to tweak the strength of the effect that Point Lights have on the user in Mixed Reality")]
        public float pointLightPower = 1f;
        [Tooltip("This signals whether Point lights should illuminate the user when they are on the 'far' side of the user feed (and in range)")]
        public bool includeBacklighting = true;

        //Editor
        [Tooltip("This signals whether MixCast should only be activatable in Standalone builds when the exe is run with the command line argument '-mixcast'")]
        public bool requireCommandLineArg = false;
        [Tooltip("This signals whether MixCast should be activatable when running the application within the Unity Editor")]
        public bool enableMixCastInEditor = true;
        [Tooltip("This signals whether MixCast should display the user feed in the Scene View when MixCast is active")]
        public bool displaySubjectInScene = false;
        public bool applySdkFlagsAutomatically = true;

        public MixCastProjectSettings() : base()
        {
            ValidateId();
        }
        void ValidateId()
        {
            if (string.IsNullOrEmpty(projectId))
                projectId = System.Guid.NewGuid().ToString();
        }

#if UNITY_EDITOR
        public void OnBeforeSerialize()
        {
            ValidateId();
        }
        public void OnAfterDeserialize()
        {
            ValidateId();
        }
#endif
    }
}
