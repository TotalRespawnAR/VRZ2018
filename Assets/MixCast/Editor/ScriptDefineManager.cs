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
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using BlueprintReality.MixCast.Data;
#if UNITY_2017_3_OR_NEWER
using UnityEditor.PackageManager.Requests;
#endif

//Class that handles enforcing the script defines on project necessary for appropriate SDK interaction
namespace BlueprintReality.MixCast
{
    [InitializeOnLoad]
    public class ScriptDefineManager
    {
        public class FileDrivenDefine
        {
            public string defineFlag;
            public string fileName;
            public string systemName;
        }

        public class PackageDrivenDefine
        {
            public string defineFlag;
            public string packageName;
            public string systemName;
            public string minVersion;
        }

        public static readonly FileDrivenDefine[] FILE_DEFINES = new FileDrivenDefine[]
        {
            new FileDrivenDefine()
            {
                defineFlag = "MIXCAST_STEAMVR",
                fileName = "SteamVR.cs",
                systemName = "SteamVR"
            },
            new FileDrivenDefine()
            {
                defineFlag = "MIXCAST_OCULUS",
                fileName = "OVRManager.cs",
                systemName = "Oculus"
            },
        };

        public static readonly PackageDrivenDefine[] PACKAGE_DEFINES = new PackageDrivenDefine[]
        {
            new PackageDrivenDefine()
            {
                defineFlag = "MIXCAST_LWRP",
                packageName = "com.unity.render-pipelines.lightweight",
                systemName = "Lightweight Render Pipeline",
                minVersion = "4.0.0"
            },
        };

#if UNITY_2017_3_OR_NEWER
        static ListRequest listPackageReq;
        static List<UnityEditor.PackageManager.PackageInfo> loadedPackages = new List<UnityEditor.PackageManager.PackageInfo>();
#endif

        static ScriptDefineManager()
        {
            EditorApplication.delayCall += Start;
            EditorApplication.update += Update;
        }

        static void Start()
        {
            EditorApplication.delayCall -= Start;

#if UNITY_2017_3_OR_NEWER
            listPackageReq = UnityEditor.PackageManager.Client.List(true);
#else
            if (MixCastSdkData.ProjectSettings != null && MixCastSdkData.ProjectSettings.applySdkFlagsAutomatically)
                EnforceAppropriateScriptDefines();
#endif
        }


        private static void Update()
        {
#if UNITY_2017_3_OR_NEWER
            if (listPackageReq != null && listPackageReq.IsCompleted)
            {
                loadedPackages.Clear();
                if (listPackageReq.Error == null)
                {
                    foreach (UnityEditor.PackageManager.PackageInfo package in listPackageReq.Result)
                    {
                        loadedPackages.Add(package);
                    }
                }

                listPackageReq = null;

                if (MixCastSdkData.ProjectSettings != null && MixCastSdkData.ProjectSettings.applySdkFlagsAutomatically)
                    EnforceAppropriateScriptDefines();
            }
#endif
        }

        public static bool EnforceAppropriateScriptDefines()
        {
            string defineStr = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildPipeline.GetBuildTargetGroup(EditorUserBuildSettings.activeBuildTarget));

            List<string> defineList = new List<string>();
            if (!string.IsNullOrEmpty(defineStr))
                defineList.AddRange(defineStr.Split(';'));

            bool anyChanges = false;
            for (int i = 0; i < FILE_DEFINES.Length; i++)
            {
                FileDrivenDefine define = FILE_DEFINES[i];
                bool changed = EnforceFileDefineAutomatically(define.defineFlag, define.fileName, defineList);
                if (changed && defineList.Contains(define.defineFlag))
                    Debug.Log("Enabled " + define.systemName + " support for MixCast");
                anyChanges |= changed;
            }
#if UNITY_2017_3_OR_NEWER
            for (int i = 0; i < PACKAGE_DEFINES.Length; i++)
            {
                PackageDrivenDefine define = PACKAGE_DEFINES[i];
                bool changed = EnforcePackageDefineAutomatically(define.defineFlag, define.packageName, defineList, define.minVersion);
                if (changed && defineList.Contains(define.defineFlag))
                    Debug.Log("Enabled " + define.systemName + " support for MixCast");
                anyChanges |= changed;
            }
#endif

            if (anyChanges)
            {
                defineStr = string.Join(";", defineList.ToArray());
                PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildPipeline.GetBuildTargetGroup(EditorUserBuildSettings.activeBuildTarget), defineStr);
            }

            return anyChanges;
        }

        //Returns true if the define list has been modified
        public static bool EnforceFileDefineAutomatically(string libraryFlag, string libraryIdentifier, List<string> currentDefines)
        {
            bool libraryFound = Directory.GetFiles(Application.dataPath, libraryIdentifier, SearchOption.AllDirectories).Length > 0;
            bool modifying = currentDefines.Contains(libraryFlag) != libraryFound;
            if (modifying)
            {
                if (libraryFound)
                    currentDefines.Add(libraryFlag);
                else
                    currentDefines.Remove(libraryFlag);
            }
            return modifying;
        }
#if UNITY_2017_3_OR_NEWER
        public static bool EnforcePackageDefineAutomatically(string defineFlag, string checkPackageId, List<string> currentDefines, string minVersion = null)
        {
            
            UnityEditor.PackageManager.PackageInfo info = loadedPackages.Find(i => i.name == checkPackageId);
            bool packageFound = info != null && (minVersion == null || !MixCastSdk.IsVersionBLaterThanVersionA(info.version.Replace("-preview",""), minVersion));
            
            bool modifying = currentDefines.Contains(defineFlag) != packageFound;
            if (modifying)
            {
                if (packageFound)
                    currentDefines.Add(defineFlag);
                else
                    currentDefines.Remove(defineFlag);
            }
            return modifying;
        }
#endif
        public static bool IsDefineEnabled(string flag)
        {
            string defineStr = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildPipeline.GetBuildTargetGroup(EditorUserBuildSettings.activeBuildTarget));
            List<string> defineList = new List<string>(defineStr.Split(';'));
            return defineList.Contains(flag);
        }
        public static bool TryEnableDefine(FileDrivenDefine define)
        {
            if (IsDefineEnabled(define.defineFlag))
                return true;
            bool libraryFound = Directory.GetFiles(Application.dataPath, define.fileName, SearchOption.AllDirectories).Length > 0;
            if (!libraryFound)
                return false;
            BuildTargetGroup buildTarget = BuildPipeline.GetBuildTargetGroup(EditorUserBuildSettings.activeBuildTarget);
            string defineStr = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTarget);
            defineStr += ";" + define.defineFlag;
            PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTarget, defineStr);
            return true;
        }
        public static void DisableDefine(FileDrivenDefine define)
        {
            if (!IsDefineEnabled(define.defineFlag))
                return;
            BuildTargetGroup buildTarget = BuildPipeline.GetBuildTargetGroup(EditorUserBuildSettings.activeBuildTarget);
            string defineStr = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTarget);
            List<string> defineList = new List<string>(defineStr.Split(';'));
            defineList.Remove(define.defineFlag);
            defineStr = string.Join(";", defineList.ToArray());
            PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTarget, defineStr);
        }
    }
}
#endif
