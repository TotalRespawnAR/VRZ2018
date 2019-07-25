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
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlueprintReality.MixCast
{
	public class SetLightsForProjection : MonoBehaviour
    {
        public const int DIR_LIGHT_ARRAY_MAX = 10;
        public const int POINT_LIGHT_ARRAY_MAX = 100;
        public const int SPOT_LIGHT_ARRAY_MAX = 100;

        public class FrameLightingData
        { 
            public int directionalLightCount;
            public Vector4[] directionalLightDirections = new Vector4[DIR_LIGHT_ARRAY_MAX];
            public Vector4[] directionalLightColors = new Vector4[DIR_LIGHT_ARRAY_MAX];

            public int pointLightCount;
            public Vector4[] pointLightPositions = new Vector4[POINT_LIGHT_ARRAY_MAX];
            public Vector4[] pointLightColors = new Vector4[POINT_LIGHT_ARRAY_MAX];

            //public int spotLightCount;
            //public Vector4[] spotLightPositions = new Vector4[SPOT_LIGHT_ARRAY_MAX];
            //public Vector4[] spotLightDirections = new Vector4[SPOT_LIGHT_ARRAY_MAX];
            //public Vector4[] spotLightColors = new Vector4[SPOT_LIGHT_ARRAY_MAX];
        }

        private const string KEYWORD_LIGHTING_FLAT = "LIGHTING_FLAT";

        protected const string DIR_LIGHT_ARRAY_SIZE = "_DirLightCount";
        protected const string DIR_LIGHT_DIRECTIONS = "_DirLightDirections";
        protected const string DIR_LIGHT_COLORS = "_DirLightColors";

        protected const string POINT_LIGHT_ARRAY_SIZE = "_PointLightCount";
        protected const string POINT_LIGHT_POSITIONS_AND_RANGE = "_PointLightPositions";
        protected const string POINT_LIGHT_COLORS = "_PointLightColors";

        //protected const string SPOT_LIGHT_ARRAY_SIZE = "_SpotLightCount";
        //protected const string SPOT_LIGHT_POSITION_AND_RANGE = "_SpotLightPositions";
        //protected const string SPOT_LIGHT_DIRECTIONS_AND_ANGLE = "_SpotLightDirections";
        //protected const string SPOT_LIGHT_COLORS = "_SpotLightColors";

        public InputFeedProjection projection;

        private int layerNum;
        private FrameDelayQueue<FrameLightingData> frames;

        private void OnEnable()
        {
            layerNum = LayerMask.NameToLayer(MixCastSdkData.ProjectSettings.subjectLayerName);
            frames = new FrameDelayQueue<FrameLightingData>(() => {
                return new FrameLightingData();
            });

            projection.OnPreRender += HandleRenderStarted;
        }

        private void OnDisable()
        {
            frames.Dispose();
            frames = null;

            projection.OnPreRender -= HandleRenderStarted;

            projection.ProjectionMaterial.DisableKeyword(KEYWORD_LIGHTING_FLAT);
        }

        private void HandleRenderStarted()
        {
            frames.delayDuration = projection.context.Data.bufferTime;
            frames.Update();

            Material projectionMat = projection.ProjectionMaterial;

            if (projection.context.Data.lightingData.effectAmount > 0)
            {
                StoreCurrentFrameLightingData();

                projectionMat.EnableKeyword(KEYWORD_LIGHTING_FLAT);
                SetMaterialProperties(frames.OldestFrameData, projection.ProjectionMaterial);
            }
            else
                projectionMat.DisableKeyword(KEYWORD_LIGHTING_FLAT);          
        }

        void StoreCurrentFrameLightingData()
        {
            MixCastCamera cam = MixCastCamera.FindCamera(projection.context);
            if (cam == null || cam.gameCamera == null)
                return;

            FrameLightingData lightingData = frames.GetNewFrame().Data;

            float playerDist = SdkCameraUtilities.CalculatePlayerDistance(projection.context.Data);

            lightingData.directionalLightCount = 0;
            if (MixCastSdkData.ProjectSettings.specifyLightsManually)
            {
                foreach( Light light in MixCastLight.ActiveDirectionalLights )
                {
                    if( (light.cullingMask & (1 << layerNum)) > 0 && LightIsAffectingPlayer(light, cam.gameCamera, playerDist))
                    {
                        lightingData.directionalLightDirections[lightingData.directionalLightCount] = light.transform.forward;
                        lightingData.directionalLightColors[lightingData.directionalLightCount] = light.color * light.intensity * MixCastSdkData.ProjectSettings.directionalLightPower * 0.5f;
                        lightingData.directionalLightCount++;

                        if (lightingData.directionalLightCount == DIR_LIGHT_ARRAY_MAX)
                            break;
                    }
                }
            }
            else
            {
                var directionalLights = Light.GetLights(LightType.Directional, layerNum);
                for (int i = 0; i < directionalLights.Length && lightingData.directionalLightCount < DIR_LIGHT_ARRAY_MAX; i++)
                {
                    if (LightIsAffectingPlayer(directionalLights[i], cam.gameCamera, playerDist))
                    {
                        lightingData.directionalLightDirections[lightingData.directionalLightCount] = directionalLights[i].transform.forward;
                        lightingData.directionalLightColors[lightingData.directionalLightCount] = directionalLights[i].color * directionalLights[i].intensity * MixCastSdkData.ProjectSettings.directionalLightPower * 0.5f;
                        lightingData.directionalLightCount++;
                    }
                }
            }


            lightingData.pointLightCount = 0;
            if (MixCastSdkData.ProjectSettings.specifyLightsManually)
            {
                foreach (Light light in MixCastLight.ActivePointLights)
                {
                    if ((light.cullingMask & (1 << layerNum)) > 0 && LightIsAffectingPlayer(light, cam.gameCamera, playerDist))
                    {
                        lightingData.pointLightPositions[lightingData.pointLightCount] = light.transform.position;
                        lightingData.pointLightPositions[lightingData.pointLightCount].w = light.range;
                        lightingData.pointLightColors[lightingData.pointLightCount] = light.color * light.intensity * MixCastSdkData.ProjectSettings.pointLightPower * 0.5f;
                        lightingData.pointLightCount++;

                        if (lightingData.pointLightCount == POINT_LIGHT_ARRAY_MAX)
                            break;
                    }
                }
            }
            else
            {
                var pointLights = Light.GetLights(LightType.Point, layerNum);
                for (int i = 0; i < pointLights.Length && lightingData.pointLightCount < POINT_LIGHT_ARRAY_MAX; i++)
                {
                    if (LightIsAffectingPlayer(pointLights[i], cam.gameCamera, playerDist))
                    {
                        lightingData.pointLightPositions[lightingData.pointLightCount] = pointLights[i].transform.position;
                        lightingData.pointLightPositions[lightingData.pointLightCount].w = pointLights[i].range;
                        lightingData.pointLightColors[lightingData.pointLightCount] = pointLights[i].color * pointLights[i].intensity * MixCastSdkData.ProjectSettings.pointLightPower * 0.5f;
                        lightingData.pointLightCount++;
                    }
                }
            }

            //foundLights = Light.GetLights(LightType.Spot, layerNum);
            //lightingData.spotLightCount = 0;
            //for (int i = 0; i < foundLights.Length && lightingData.spotLightCount < SPOT_LIGHT_ARRAY_MAX; i++)
            //{
            //    if (LightIsAffectingPlayer(foundLights[i], cam.gameCamera, playerDist))
            //    {
            //        lightingData.spotLightPositions[lightingData.spotLightCount] = foundLights[i].transform.position;
            //        lightingData.spotLightPositions[lightingData.spotLightCount].w = foundLights[i].range;
            //        lightingData.spotLightDirections[lightingData.spotLightCount] = foundLights[i].transform.forward;
            //        lightingData.spotLightDirections[lightingData.spotLightCount].w = foundLights[i].spotAngle * Mathf.Deg2Rad * 0.5f;
            //        lightingData.spotLightColors[lightingData.spotLightCount] = foundLights[i].color * foundLights[i].intensity * MixCastSdkData.ProjectSettings.spotLightPower * 0.5f;
            //        lightingData.spotLightCount++;
            //    }
            //}
        }

        bool LightIsAffectingPlayer(Light l, Camera cam, float playerDist)
        {
            switch(l.type)
            {
                case LightType.Directional:
                    return true;

                case LightType.Point:
                    float dot = Vector3.Dot(cam.transform.forward, l.transform.position - cam.transform.position);

                    if (Mathf.Abs(dot - playerDist) > l.range)
                        return false;
                    if (playerDist < dot && !MixCastSdkData.ProjectSettings.includeBacklighting)
                        return false;

                    return true;

                case LightType.Spot:
                    float coneSideLength = l.range / Mathf.Cos(l.spotAngle * Mathf.Deg2Rad);

                    float dot2 = Vector3.Dot(cam.transform.forward, l.transform.position - cam.transform.position);
                    if (Mathf.Abs(dot2 - playerDist) > coneSideLength)
                        return false;
                    if (playerDist < dot2 && !MixCastSdkData.ProjectSettings.includeBacklighting)
                        return false;
                    return true;

                default:
                    return false;
            }
        }

        void SetMaterialProperties(FrameLightingData oldFrame, Material mat)
        {
            mat.SetFloat("_PlayerLightingFactor", projection.context.Data.lightingData.effectAmount);
            mat.SetFloat("_PlayerLightingBase", projection.context.Data.lightingData.baseLighting);
            mat.SetFloat("_PlayerLightingPower", projection.context.Data.lightingData.powerMultiplier);

            mat.SetInt(DIR_LIGHT_ARRAY_SIZE, oldFrame.directionalLightCount);
            mat.SetVectorArray(DIR_LIGHT_DIRECTIONS, oldFrame.directionalLightDirections);
            mat.SetVectorArray(DIR_LIGHT_COLORS, oldFrame.directionalLightColors);

            mat.SetInt(POINT_LIGHT_ARRAY_SIZE, oldFrame.pointLightCount);
            mat.SetVectorArray(POINT_LIGHT_POSITIONS_AND_RANGE, oldFrame.pointLightPositions);
            mat.SetVectorArray(POINT_LIGHT_COLORS, oldFrame.pointLightColors);

            //mat.SetInt(SPOT_LIGHT_ARRAY_SIZE, oldFrame.spotLightCount);
            //mat.SetVectorArray(SPOT_LIGHT_POSITION_AND_RANGE, oldFrame.spotLightPositions);
            //mat.SetVectorArray(SPOT_LIGHT_DIRECTIONS_AND_ANGLE, oldFrame.spotLightDirections);
            //mat.SetVectorArray(SPOT_LIGHT_COLORS, oldFrame.spotLightColors);
        }
    }
}
#endif
