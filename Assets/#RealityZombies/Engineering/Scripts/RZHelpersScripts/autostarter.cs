//#define ENABLE_LOGS
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class autostarter : MonoBehaviour
{



    // Use this for initialization
    void Start()
    {
        StartCoroutine(StartThisShitIn3sec());
    }
    IEnumerator StartThisShitIn3sec()
    {
        yield return new WaitForSeconds(5f);
        // SceneManager.LoadScene("KngSetupMenu");
        SceneManager.LoadScene("runthis");

        //if (FindBinaryFile())
        //{
        //    Debug.Log("Bin file exists");
        //    SceneManager.LoadScene("KngSetupMenu");
        //}
        //else
        //    SceneManager.LoadScene("SceneAncPlace");
    }

    bool FindBinaryFile()
    {

        Debug.Log("path to wac= " + StaticHexLogger.DirTarget);
        if (File.Exists(StaticHexLogger.DirTarget + StaticHexLogger.FileName_BatchSaveBytesWACt))
            return true;
        return false;
    }



}
