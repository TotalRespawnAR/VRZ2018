//#define ENABLE_LOGS
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Autostarter : MonoBehaviour
{
 

    public string ScenName;
    // Use this for initialization
    void Start()
    {
        StartCoroutine(StartThisShitIn3sec());
    }
    IEnumerator StartThisShitIn3sec()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(ScenName);

    }

    //bool FindBinaryFile()
    //{

    //    Debug.Log("path to wac= " + StaticHexLogger.DirTarget);
    //    if (File.Exists(StaticHexLogger.DirTarget + StaticHexLogger.FileName_BatchSaveBytesWACt))
    //        return true;
    //    return false;
    //}



}
 

