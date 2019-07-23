using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using SysDiag = System.Diagnostics;
using System.Linq;
#if !UNITY_EDITOR && UNITY_METRO
using System.Threading.Tasks;
using Windows.Storage;
#endif
public class LoggerToFile : MonoBehaviour {




    //public string output = "";
    //public string stack = "";
    void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }
    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }
    void HandleLog(string logString, string stackTrace, LogType type)
    {

        if (!GameSettings.Instance.IsLoggerOn) return; //quick way to toggle error logging  
        if (type == LogType.Error || type == LogType.Exception)
        {

            var logEntry = string.Format("\n +++++++++++++++++++++++++++++++++   \n {0} {1} \n {2}\n  {3} \n {4} ", DateTime.Now, type, logString, stackTrace, "+++++++++++++++++++++++++++++++++");
            string fullpath = ArzDirPath + "/" + "ARZ_logErrors.txt";

            File.AppendAllText(fullpath, logEntry);
        }
        else
               if (type == LogType.Log || type == LogType.Warning)
        {

            var logEntry = string.Format("\n ------------------------------------ \n {0} {1} \n {2}\n  {3} \n ", DateTime.Now, type, logString, "------------------------------------");
            string fullpath = ArzDirPath + "/" + "ARZ_logErrors.txt";

            File.AppendAllText(fullpath, logEntry);
        }
    }

    public static LoggerToFile Instance = null;

    private void Awake()
    {
      
        string fullpath = ArzDirPath + "/" + "ARZ_logErrors.txt";
 
        if (Instance == null)
        {
            Debug.Log("fileWriter On ");
            DontDestroyOnLoad(this.gameObject);
            Instance = this;
        }
        else
            Destroy(gameObject);
    }




    //private StreamWriter _writer;

    string ArzDirPath
    {
        get
        {
#if !UNITY_EDITOR && UNITY_METRO
                    return ApplicationData.Current.RoamingFolder.Path;
#else
            return Application.persistentDataPath;
#endif
        }
    }
 

}
