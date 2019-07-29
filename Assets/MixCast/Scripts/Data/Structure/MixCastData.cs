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
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace BlueprintReality.MixCast
{
    [Serializable]
    public partial class MixCastData
    {
        public enum OutputMode
        {
            Immediate, Buffered, Quadrant
        }

        public enum OutputResolution
        {
            Preset,
            FullScreen,
            WindowSize,
            Custom
        }

        public enum LicenseType
        {
            Free, Arcade, Creator, Indie, Educator, Streamer, eSports, Film, Pro, Enterprise
        }

        static public event EventHandler OnWriteData;

        static public void WriteData()
        {
            if (OnWriteData != null)
            {
                OnWriteData(null, null);
            }
        }

        [Serializable]
        public partial class GlobalData
        {
            public int targetFramerate = 30;
            public int framesPerSpareRender = 3;    //1 spare render every 3 main renders

            public string rootOutputPath = "";
            public bool startAutomatically = true;

            public Vector3 standingPosToRaw = Vector3.zero;
            public Quaternion standingRotToRaw = Quaternion.identity;
        }

        [Serializable]
        public partial class CameraCalibrationData
        {
            //Camera parameters
            public string displayName;
            public const float DEFAULT_DEVICE_FOV = 45;
            public float deviceFoV = DEFAULT_DEVICE_FOV;

            //Placement data
            public Vector3 worldPosition = Vector3.zero;
            public Quaternion worldRotation = Quaternion.identity;

            public bool wasTracked;
            public string trackedByDevice;
            public string trackedByDeviceId;
            public string trackedByDeviceRole;
            public Vector3 trackedPosition = Vector3.zero;
            public Quaternion trackedRotation = Quaternion.identity;

            //Actor tracking data
            public bool actorWasTracked;
            public string actorTrackingDeviceId;

            //Motion data
            public float positionSmoothTime;
            public float rotationSmoothTime;

            public const int MAX_WIDTH_FREE = 1280;
            public const int MAX_HEIGHT_FREE = 720;

            //Output data
            public int outputWidth = 1920, outputHeight = 1080;
            public OutputMode outputMode = OutputMode.Buffered;
            public OutputResolution outputResolution = OutputResolution.Preset;

            //For buffered camera mode
            public const float DEFAULT_BUFFER_DELAY = 150f; //milliseconds
            public float bufferTime = DEFAULT_BUFFER_DELAY;
            public float maxBufferTime = 5000f;

            public bool isTransformConfigured = false;

            //Background removal data
            public SubjectBoxingData croppingData = new SubjectBoxingData();

            //Projection parameters
            public const bool DEFAULT_BG_REMOVAL_AA_ENABLED = true;
            public bool antiAliasSegmentationResults = DEFAULT_BG_REMOVAL_AA_ENABLED;
            public ProjectionData projectionData = new ProjectionData();

            //InScene display data
            public SceneDisplayData displayData = new SceneDisplayData();

            public LightingData lightingData = new LightingData();

            

            public string id;
            public bool deviceUseAutoFoV = false;
        }

        [Serializable]
        public class SubjectBoxingData
        {
            public bool active = false;

            public const float DEFAULT_HEADRADIUS = 0.25f;
            public float headRadius = 0.25f;
            public const float DEFAULT_HANDRADIUS = 0.25f;
            public float handRadius = 0.25f;
            public const float DEFAULT_BASERADIUS = 0.25f;
            public float baseRadius = 0.25f;
        }

        [Serializable]
        public class ProjectionData
        {
            public bool displayToHeadset = false;
            public bool displayToOtherCams = false;
        }

        [Serializable]
        public class SceneDisplayData
        {
            public enum PlacementMode
            {
                Camera, World, Headset
            }
            public PlacementMode mode = PlacementMode.Camera;

            public Vector3 position;
            public Quaternion rotation;

            public const float MAX_SCALE = 1.5f, MIN_SCALE = 0.25f;
            public float scale = 1f;
            public float alpha = 1f;
        }

        [Serializable]
        public class LightingData
        {
            public const float DEFAULT_EFFECT_AMOUNT = 0f;
            public const float DEFAULT_BASE_LIGHTING = 0.5f;
            public const float DEFAULT_POWER_MULTIPLIER = 1f;
            public const float DEFAULT_DIR_FACTOR = 1f;
            public const float DEFAULT_DIR_ADJUST = 0.5f;

            // Toggle to easily turn on/off ligting while preserving set values
            public bool isEnabled = DEFAULT_EFFECT_AMOUNT > 0;
            //Factor lerps from no lighting (0) to full lighting (1)
            public float effectAmount = DEFAULT_EFFECT_AMOUNT;
            //Adds a constant value to lighting to set a baseline amount of light
            public float baseLighting = DEFAULT_BASE_LIGHTING;
            //Multiplies the final lighting power
            public float powerMultiplier = DEFAULT_POWER_MULTIPLIER;

            //Determines how much directional attenuation applies
            public float lightingByDirection = DEFAULT_DIR_FACTOR;
            //Allows the user to customize at which slope lighting falls off
            public float dirLightingEnd = DEFAULT_DIR_ADJUST;
        }

        [Serializable]
        public class SensorPose
        {
            public Vector3 position = Vector3.zero;
            public Quaternion rotation = Quaternion.identity;
        }

        public GlobalData global = new GlobalData();
        public List<CameraCalibrationData> cameras = new List<CameraCalibrationData>();
        
        public SceneDisplayData sceneDisplay = new SceneDisplayData();

        public string sensorId;
        public int sensorIndex = -1;
        public SensorPose sensorPose = new SensorPose();

        public Vector3 cameraStartPosition;
        public Quaternion cameraStartRotation;

        public string sourceVersion = "";

        public string language = "";
        public string persistentDataPath;

        public CameraCalibrationData GetCameraByID(string id)
        {
            for (int i = 0; i < cameras.Count; i++)
                if (cameras[i] != null && cameras[i].id == id)
                    return cameras[i];
            return null;
        }
    }
}
#endif
