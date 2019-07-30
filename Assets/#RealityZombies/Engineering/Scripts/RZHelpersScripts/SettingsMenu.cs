using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    //      y                                         .     y
    //      |                                         .     |                                         .
    //  ....x                                         . ....x                                         
    //      |-------------------|   ^                 .
    //      | <------width----->|   | height          .     | <------width----->|   | height          
    //      |___________________| ..v..........Xspace .     |___________________| ..v..........Xspace 
    //                         |            
    //                         | Yspace
    //                         |
    //      y                  |
    //      |                  |
    //  ....x                  |
    //      |---------------------|   ^
    //      | <------width------->|   | height
    //      |_____________________| ..v..........Xspace 
    //                         |            
    //                         | Yspace
    //                         |
    // Xspace
    float Xspace = 5f;
    float Yspace = 5f;
    float TextBoxDimentionsWidth = 100f; //=12 chars round to 10 
    float TextBoxDimentionsHeight = 20f;
    float ToggleDimentions = 20f;
    float PosX = 10;
    float PosY = 10;
    float curXpos = 0;
    float curYpos = 0;

    Rect ToggleVive;
    Rect ToggleNab;
    Rect GameTimeLable;
    Rect GameTimeBox;
    private void OnEnable()
    {
        curXpos = PosX;
        curYpos = PosY;
        ToggleVive = new Rect(curXpos, curYpos, TextBoxDimentionsWidth, ToggleDimentions);
        curXpos += (Xspace + TextBoxDimentionsWidth);
        ToggleNab = new Rect(curXpos, curYpos, TextBoxDimentionsWidth, ToggleDimentions);

        curXpos = PosX; //reset cursor 
        curYpos += (Yspace + TextBoxDimentionsHeight); //nextline

        GameTimeLable = new Rect(curXpos, curYpos, TextBoxDimentionsWidth, TextBoxDimentionsHeight);
        curXpos += (Xspace + TextBoxDimentionsWidth);
        GameTimeBox = new Rect(curXpos, curYpos, TextBoxDimentionsWidth, TextBoxDimentionsHeight);

        curXpos = PosX; //reset cursor 
        curYpos += (Yspace + TextBoxDimentionsHeight); //nextline

    }

    private void OnDisable()
    {

    }


    private void OnGUI()
    {

        GameSettings.Instance.UseVive = GUI.Toggle(ToggleVive, GameSettings.Instance.UseVive, "useVive");
        GameSettings.Instance.NABILSETTINGSON = GUI.Toggle(ToggleNab, GameSettings.Instance.NABILSETTINGSON, "useNab");
        //GameSettings.Instance.Global_Time_Apocalypse_GameEnds_600s_10m = 


        GameSettings.Instance.Set_GlobalTimer(GUI.TextField(GameTimeBox, GameSettings.Instance.Global_Time_Apocalypse_GameEnds_600s_10m.ToString()));
        GUI.TextArea(GameTimeLable, "gameTime");
    }

    void GuiDoToggle(string argTogleName, bool argInitialGuiVal, float argYOffset, float argXoffset)
    {

        GameSettings.Instance.UseVive = GUI.Toggle(ToggleVive, argInitialGuiVal, argTogleName);

    }












    void GuiDo()
    {
        if (!aTexture)
        {
            Debug.LogError("Please assign a texture in the inspector.");
            return;
        }

        toggleTxt = GUI.Toggle(new Rect(10, 10, 150, 30), toggleTxt, "12345678901234567890");
        toggleImg = GUI.Toggle(new Rect(10, 50, 50, 50), toggleImg, aTexture);
    }

    public Texture aTexture;

    private bool toggleTxt = false;
    private bool toggleImg = false;





}




































//public Text AlphaBravo;
//public Toggle blood;
//public Toggle holo;
//public Toggle rightHanded;
//public Toggle tournament;
//public Toggle reloadModeEasy;
//public Toggle defaultScanMesh;
//public Toggle useStem;
//public Toggle useStiker;
//public Toggle testMode;
//public InputField gameLength;
//public Toggle playSingle;
//public Toggle playUdp;
//public Toggle playMultiplayerCoop;
//public InputField oppenentIP;
//public InputField serverIP;

//public Text Placeholder_GameLength;
//public Text Placeholder_OpponentIP;
//public Text Placeholder_ServerIP;


//public void Start()
//{
//CalculatedLocalIP = ;
//  Debug.Log("I am On --------------" + gameObject.name);
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

//}



//[SerializeField]
//private string _uiRemote_IP_Alpha;
//[SerializeField]
//private string _uiRemote_IP_Bravo;
//[SerializeField]
//private string _uiRemote_IP_AudioServer;
//[SerializeField]
//private string _uiRemote_Port_Alpha;
//[SerializeField]
//private string _uiRemote_Port_Bravo;
//[SerializeField]
//private string _uiRemote_Port_AudioServer;
//[SerializeField]
//private string _uiLocal_IP;
//[SerializeField]
//private string _uiLocal_ListenPort;

//private bool toggleUpdate = false;
//private bool toggleFixed = false;
//private bool toggleLate = false;



//string btn_AlphaSet = "set A";
//string btn_BravoSet = "set B";
//string btn_LocalSet = "set me";
//string btn_AudioDerverSet = "set au";


//bool UdpSceneOn = false;
//string CalculatedLocalIP;


//private void OnGUI()
//{
//    GUI.TextArea(new Rect(10, 10, 100, 20), "yo");
//    GUI.Toggle(new Rect(20, 20, 40, 40), false, "useVive");
//    //_uiRemote_IP_Alpha = GUI.TextField(new Rect(10, 10, 200, 30), _uiRemote_IP_Alpha, 25);
//    //_uiRemote_IP_Bravo = GUI.TextField(new Rect(10, 50, 200, 30), _uiRemote_IP_Bravo, 25);
//    //_uiLocal_IP = GUI.TextField(new Rect(10, 90, 200, 30), CalculatedLocalIP, 25);
//    //_uiRemote_IP_AudioServer = GUI.TextField(new Rect(10, 130, 200, 30), _uiRemote_IP_AudioServer, 25);


//    //_uiRemote_Port_Alpha = GUI.TextField(new Rect(210, 10, 90, 30), _uiRemote_Port_Alpha, 25);
//    //_uiRemote_Port_Bravo = GUI.TextField(new Rect(210, 50, 90, 30), _uiRemote_Port_Bravo, 25);
//    //_uiLocal_ListenPort = GUI.TextField(new Rect(210, 90, 90, 30), _uiLocal_ListenPort, 25);
//    //_uiRemote_Port_AudioServer = GUI.TextField(new Rect(210, 130, 90, 30), _uiRemote_Port_AudioServer, 25);
//    ////_uiRemote_Port_AudioServer = GUI.TextField(new Rect(210, 130, 90, 30), CalculatedLocalIP, 25);//


//    //if (GUI.Button(new Rect(300, 10, 100, 30), btn_AlphaSet)) { Debug.Log(btn_AlphaSet); }
//    //if (GUI.Button(new Rect(300, 50, 100, 30), btn_BravoSet)) { Debug.Log(btn_BravoSet); }
//    //if (GUI.Button(new Rect(300, 90, 100, 30), btn_LocalSet)) { Debug.Log(btn_LocalSet); }
//    //if (GUI.Button(new Rect(300, 130, 100, 30), btn_AudioDerverSet))
//    //{

//    //}


//    //if (GUI.Button(new Rect(300, 170, 100, 30), "check"))
//    //{

//    //}
//    //if (GUI.Button(new Rect(400, 170, 100, 30), "setall"))
//    //{

//    //}


//    //if (GUI.Button(new Rect(10, 210, 100, 30), "start")) { }

//    //if (GUI.Button(new Rect(110, 210, 100, 30), "restart")) { }
//    //if (GUI.Button(new Rect(220, 210, 100, 30), "stop")) { }








//}
//public void Set_Alpha()
//{

//    GameSettings.Instance.GameMode = ARZGameModes.GameLeft_Alpha;

//}
//public void Set_Bravo()
//{

//    GameSettings.Instance.GameMode = ARZGameModes.GameRight_Bravo;
//}

//public void Set_TestMode()
//{
//    if (testMode.isOn) { GameSettings.Instance.IsTestModeON = true; }
//    else
//        GameSettings.Instance.IsTestModeON = false;
//}

//public void Set_RightHanded()
//{
//    if (rightHanded.isOn)
//    {
//        GameSettings.Instance.PlayerLeftyRight = ARZPlayerLeftyRighty.RightyPlayer;
//    }
//    else
//    {
//        GameSettings.Instance.PlayerLeftyRight = ARZPlayerLeftyRighty.LeftyPlayer;
//    }
//}

//public void Set_Blood()
//{
//    if (blood.isOn) { GameSettings.Instance.IsBloodOn = true; }
//    else
//        GameSettings.Instance.IsBloodOn = false;
//}


//public void Set_Tournament()
//{
//    if (tournament.isOn) { GameSettings.Instance.IsTournamentModeOn = true; }
//    else
//        GameSettings.Instance.IsTournamentModeOn = false;
//}



//public void GoTOCalibtaion()
//{
//    if (GameSettings.Instance.GameMode == ARZGameModes.GameLeft_Alpha)
//        SceneManager.LoadScene("CalibrateLeft");
//    else
//        SceneManager.LoadScene("CalibrateRight");

//}

//public void DoSetGameLength(Text argTxtbox)
//{
//    Debug.Log("enetered + " + argTxtbox.text);

//    int val = int.Parse(argTxtbox.text);
//    if (val > 60 && val < 300)
//    {
//        float timefloat = (float)val;
//        GameSettings.Instance.Set_GlobalTimer(timefloat);
//        Placeholder_GameLength.text = GameSettings.Instance.Global_Time_Apocalypse_GameEnds_600s_10m.ToString();
//    }
//}
