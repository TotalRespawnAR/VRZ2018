using UnityEngine;
using UnityEngine.SceneManagement;

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

    float Line1 = 10f;
    float Line2 = 30f;
    float Line3 = 50f;
    float Line4 = 70f;
    float Line5 = 90f;
    float Line6 = 110f;
    float Line7 = 130f;
    float Line8 = 150f;
    float Line9 = 170f;
    float Line10 = 190f;

    float Xplace1 = 10f;
    float Xplace2 = 110f;
    float Xplace3 = 210f;
    float Xplace4 = 310f;
    float Xplace5 = 410f;
    Rect ToggleVive;
    Rect ToggleNab;
    Rect ToggleElevator;
    Rect ToggleTestmode;

    Rect ToggleBabyGun;
    Rect TogglePlayer2;
    Rect ToggleScopes;
    Rect ToggleShowPlayer1;

    Rect GameTimeLable;
    Rect GameTimeBox;

    Rect SuddTimeLable;
    Rect SudTimeBox;

    Rect AltGame;
    Rect UseAxe2;
    Rect RoomRevers;
    Rect UseXboxCTRL;
    Rect UseCamShake;
    Rect UseFlies;
    Rect UseFlies2;
    Rect UseFireSky;
    Rect UseSingleButtonReload;
    Rect ButtonNext;

    private void OnEnable()
    {

        ToggleVive = new Rect(Xplace1, Line1, TextBoxDimentionsWidth, ToggleDimentions);
        ToggleNab = new Rect(Xplace2, Line1, TextBoxDimentionsWidth, ToggleDimentions);
        ToggleElevator = new Rect(Xplace3, Line1, TextBoxDimentionsWidth, ToggleDimentions);
        ToggleTestmode = new Rect(Xplace4, Line1, TextBoxDimentionsWidth, ToggleDimentions);


        ToggleBabyGun = new Rect(Xplace1, Line2, TextBoxDimentionsWidth, ToggleDimentions);
        TogglePlayer2 = new Rect(Xplace2, Line2, TextBoxDimentionsWidth, ToggleDimentions);
        ToggleScopes = new Rect(Xplace3, Line2, TextBoxDimentionsWidth, ToggleDimentions);
        ToggleShowPlayer1 = new Rect(Xplace4, Line2, TextBoxDimentionsWidth, ToggleDimentions);


        GameTimeLable = new Rect(Xplace1, Line3, TextBoxDimentionsWidth, TextBoxDimentionsHeight);
        GameTimeBox = new Rect(Xplace2, Line3, TextBoxDimentionsWidth, TextBoxDimentionsHeight);

        SuddTimeLable = new Rect(Xplace4, Line3, TextBoxDimentionsWidth, TextBoxDimentionsHeight);
        SudTimeBox = new Rect(Xplace5, Line3, TextBoxDimentionsWidth, TextBoxDimentionsHeight);


        AltGame = new Rect(Xplace1, Line4, TextBoxDimentionsWidth, TextBoxDimentionsHeight);
        UseAxe2 = new Rect(Xplace2, Line4, TextBoxDimentionsWidth, TextBoxDimentionsHeight);
        RoomRevers = new Rect(Xplace3, Line4, TextBoxDimentionsWidth, TextBoxDimentionsHeight);
        UseXboxCTRL = new Rect(Xplace4, Line4, TextBoxDimentionsWidth, TextBoxDimentionsHeight);
        UseCamShake = new Rect(Xplace5, Line4, TextBoxDimentionsWidth, TextBoxDimentionsHeight);
        UseFlies = new Rect(Xplace1, Line5, TextBoxDimentionsWidth, TextBoxDimentionsHeight);
        UseFlies2 = new Rect(Xplace2, Line5, TextBoxDimentionsWidth, TextBoxDimentionsHeight);
        UseFireSky = new Rect(Xplace3, Line5, TextBoxDimentionsWidth, TextBoxDimentionsHeight);
        UseSingleButtonReload = new Rect(Xplace4, Line5, TextBoxDimentionsWidth, TextBoxDimentionsHeight);
        ButtonNext = new Rect(Xplace4, Line8, TextBoxDimentionsWidth, ToggleDimentions);
    }

    private void OnDisable()
    {

    }


    private void OnGUI()
    {

        GameSettings.Instance.UseVive = GUI.Toggle(ToggleVive, GameSettings.Instance.UseVive, "useVive");
        GameSettings.Instance.UseNab = GUI.Toggle(ToggleNab, GameSettings.Instance.UseNab, "useNab");

        GameSettings.Instance.UseElevator = GUI.Toggle(ToggleElevator, GameSettings.Instance.UseElevator, "elevator");
        GameSettings.Instance.UseTestmode = GUI.Toggle(ToggleTestmode, GameSettings.Instance.UseTestmode, "testmode");
        GameSettings.Instance.UseBabyGun = GUI.Toggle(ToggleBabyGun, GameSettings.Instance.UseBabyGun, "babygun");
        GameSettings.Instance.UsePlayer2 = GUI.Toggle(TogglePlayer2, GameSettings.Instance.UsePlayer2, "player2");
        GameSettings.Instance.UseScopes = GUI.Toggle(ToggleScopes, GameSettings.Instance.UseScopes, "scopes");
        GameSettings.Instance.UseShowPlayer = GUI.Toggle(ToggleShowPlayer1, GameSettings.Instance.UseShowPlayer, "showP1");
        GameSettings.Instance.Set_GlobalTimer(GUI.TextField(GameTimeBox, GameSettings.Instance.Global_Time_Apocalypse_GameEnds_600s_10m.ToString()));
        GUI.TextArea(GameTimeLable, "gameTime");
        GameSettings.Instance.Set_SuddenDeathTimer(GUI.TextField(SudTimeBox, GameSettings.Instance.Global_Time_SuddenDeath_300s_5min.ToString()));
        GUI.TextArea(SuddTimeLable, "SuddenDeath");

        GameSettings.Instance.UseAltGame = GUI.Toggle(AltGame, GameSettings.Instance.UseAltGame, "alt");
        GameSettings.Instance.UseAxe2 = GUI.Toggle(UseAxe2, GameSettings.Instance.UseAxe2, "axeeffects");
        GameSettings.Instance.UseRoomFlip = GUI.Toggle(RoomRevers, GameSettings.Instance.UseRoomFlip, "roomFlip");

        GameSettings.Instance.UseCamShake = GUI.Toggle(UseCamShake, GameSettings.Instance.UseCamShake, "camShake");
        GameSettings.Instance.UseXboxCTRL = GUI.Toggle(UseXboxCTRL, GameSettings.Instance.UseXboxCTRL, "xbxCtrl");
        GameSettings.Instance.UseFlies = GUI.Toggle(UseFlies, GameSettings.Instance.UseFlies, "swarmOn");
        GameSettings.Instance.UseFirSky = GUI.Toggle(UseFireSky, GameSettings.Instance.UseFirSky, "fireSky");
        GameSettings.Instance.UseFlies2 = GUI.Toggle(UseFlies2, GameSettings.Instance.UseFirSky, "swarm");

        GameSettings.Instance.ISimpleGunSwapn = GUI.Toggle(UseSingleButtonReload, GameSettings.Instance.ISimpleGunSwapn, "1 butt Rel");

        if (GUI.Button(ButtonNext, "playerEntry"))
        {
            GoToScene();
        }
    }
    void GoToScene()
    {

        SceneManager.LoadScene("PlayerEntry");
    }

}
