using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KngFloatingUI : MonoBehaviour {

    GameObject _chelper;
    private void Start()
    {
        _chelper = GameObject.FindGameObjectWithTag("CursorTag");
        if (_chelper == null)
        {
            Logger.Debug("XXXXXXXXXXX did not find Cursor in game");
        }
        else
        {
            _chelper.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// OnClicking KngFloatingUI."CALIB BUTTON" in SetupScene
    /// </summary>
    public void BTN_OpenCalibtaionScene()
    {
        if (GameSettings.Instance.GameMode == ARZGameModes.GameLeft_Alpha)
            SceneManager.LoadScene("CalibrateLeft");
        else
            SceneManager.LoadScene("CalibrateRight");
    }

    /// <summary>
    /// OnClicking KngFloatingUI."Setup Button" in SetupScene
    /// </summary>
    public void BTN_OpenKngSetupMenue()
    {
        SceneManager.LoadScene("KngSetupMenu");
    }


    /// <summary>
    /// OnClicking KngFloatingUI."Setup Button" in SetupScene
    /// </summary>
    public void BTN_OpenKngGame()
    {
        GameSettings.Instance.IsKidsModeOn = false;
        GameSettings.Instance.IsTournamentModeOn = true;
        SceneManager.LoadScene("KngGame");
    }

    public void BTN_KidsMode() {
        GameSettings.Instance.IsKidsModeOn = true;
        GameSettings.Instance.IsTournamentModeOn = false;
        SceneManager.LoadScene("KngGame");
    }

    /// <summary>
    /// OnClicking KngFloatingUI."place Button" in SetupScene
    /// </summary>
    public void BTN_OpenKngPaceRoom()
    {
        SceneManager.LoadScene("SceneAncPlace");
    }


    /// <summary>
    /// OnClicking KngFloatingUI."Load Button" in SetupScene
    /// </summary>
    public void BTN_OpenKngLoadRoom()
    {
        SceneManager.LoadScene("SceneAnchorsSeriDese");
    }


    public void BTN_OpenUdpSetupRoom()
    {
        SceneManager.LoadScene("udpPropSetup");
    }

    public void BTN_OpenUdpTestRoom()
    {
        SceneManager.LoadScene("kngUdpTest");
    }
}
