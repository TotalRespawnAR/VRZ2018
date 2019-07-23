// @Author Jeffrey M. Paquette ©2016

using UnityEngine;
using UnityEngine.SceneManagement; 
//using UnityEngine.VR.WSA.Persistence;
//using UnityEngine.VR.WSA;
using System.Collections;
using System.Collections.Generic;

public class LevelManagerArz : MonoBehaviour {



    public void GotoGameNoSetm() {GameSettings.Instance.GameMode = ARZGameModes.GameNoStem; LoadScene("Game"); }
    public void GoToGameRight() { GameSettings.Instance.GameMode = ARZGameModes.GameRight_Bravo; LoadScene("Game"); }
    public void GoToGameLeft() { GameSettings.Instance.GameMode = ARZGameModes.GameLeft_Alpha; LoadScene("Game"); }

    public void GoToDUALLeft() { GameSettings.Instance.GameMode = ARZGameModes.GameLeft_Alpha; LoadScene("RZGameUdp"); }
    public void GoToDUALLRight() { GameSettings.Instance.GameMode = ARZGameModes.GameRight_Bravo; LoadScene("RZGameUdp"); }

    public void GoToMainMenu() { GameSettings.Instance.GameMode = ARZGameModes.GameRight_Bravo; LoadScene("MainMenu"); }
    public void GoToUdpMainMenu() { GameSettings.Instance.GameMode = ARZGameModes.GameRight_Bravo; LoadScene("UdpMainMenu"); }


    public void GoToTCPclientScene(){  SceneManager.LoadScene("TcpResearch");}
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
