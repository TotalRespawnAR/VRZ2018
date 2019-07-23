using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour {

    public Text AlphaBravo;
    public Toggle blood;
    public Toggle holo;
    public Toggle rightHanded;
    public Toggle tournament;
    public Toggle reloadModeEasy;
    public Toggle defaultScanMesh;
    public Toggle useStem;
    public Toggle useStiker;
    public Toggle testMode;
    public InputField gameLength;
    public Toggle playSingle;
    public Toggle playUdp;
    public Toggle playMultiplayerCoop;
    public InputField oppenentIP;
    public InputField serverIP;

    public Text Placeholder_GameLength;
    public Text Placeholder_OpponentIP;
    public Text Placeholder_ServerIP;

    private void OnEnable()
    {
        

    }

    private void OnDisable()
    {
        
    }
    public void Start()
    {

        Debug.Log("I am On --------------" + gameObject.name);
        //if (GameSettings.Instance.GameMode == ARZGameModes.GameLeft) { AlphaBravo.text = "alpha"; }
        //else
        // if (GameSettings.Instance.GameMode == ARZGameModes.GameRight) { AlphaBravo.text = "bravo"; }
        //else
        //{
        //    AlphaBravo.text = "NA!!";
        //}

        //blood.isOn = GameSettings.Instance.IsBloodOn;
        //rightHanded.isOn = GameSettings.Instance.IsRightHandedPlayer;
        //tournament.isOn = GameSettings.Instance.IsTournamentModeOn;
        //reloadModeEasy.isOn = (GameSettings.Instance.ReloadDifficulty == ARZReloadLevel.EASY) ? true : false;
        //defaultScanMesh.isOn = GameSettings.Instance.IsUseDefaultScanMesh;
        //useStem.isOn = GameSettings.Instance.IsUseStemSystem;
        //useStiker.isOn = GameSettings.Instance.IsUseStrikerVr;
        //playSingle.isOn = GameSettings.Instance.IsSinglePlayer;
        //playUdp.isOn = GameSettings.Instance.IsUdpPlayer;
        //playMultiplayerCoop.isOn = GameSettings.Instance.IsMultiPlayer;
        //testMode.isOn = GameSettings.Instance.IsTestModeON;
        //Placeholder_GameLength.text = GameSettings.Instance.GlobalGameMasterTime.ToString();// ?  "240" : "120";
        //holo.isOn = GameSettings.Instance.IsUseHololens;

    }


    public void Set_Alpha() {

        GameSettings.Instance.GameMode = ARZGameModes.GameLeft_Alpha;
 
    }
    public void Set_Bravo() {

        GameSettings.Instance.GameMode = ARZGameModes.GameRight_Bravo; 
    }

    public void Set_TestMode()
    {
        if (testMode.isOn) { GameSettings.Instance.IsTestModeON=true; }else
         GameSettings.Instance.IsTestModeON=false;
    }

    public void Set_RightHanded()
    {
        if (rightHanded.isOn)
        {
            GameSettings.Instance.PlayerLeftyRight = ARZPlayerLeftyRighty.RightyPlayer;
        }
        else
        {
            GameSettings.Instance.PlayerLeftyRight = ARZPlayerLeftyRighty.LeftyPlayer;
        }
    }

    public void Set_Blood()
    {
        if (blood.isOn) { GameSettings.Instance.IsBloodOn = true; }
        else
            GameSettings.Instance.IsBloodOn = false;
    }


    public void Set_Tournament()
    {
        if (tournament.isOn) { GameSettings.Instance.IsTournamentModeOn = true; }
        else
            GameSettings.Instance.IsTournamentModeOn = false;
    }



    public void GoTOCalibtaion() {
        if (GameSettings.Instance.GameMode == ARZGameModes.GameLeft_Alpha)
        SceneManager.LoadScene("CalibrateLeft");
        else
        SceneManager.LoadScene("CalibrateRight");

    }

    public void DoSetGameLength(Text argTxtbox) {
        Debug.Log("enetered + " + argTxtbox.text);

        int val = int.Parse(argTxtbox.text);
        if (val > 60 && val < 300) {
            float timefloat = (float)val;
            GameSettings.Instance.Set_GlobalTimer(timefloat);
            Placeholder_GameLength.text = GameSettings.Instance.Global_Time_Apocalypse_GameEnds_600s_10m.ToString();
        }
    }

    
}
