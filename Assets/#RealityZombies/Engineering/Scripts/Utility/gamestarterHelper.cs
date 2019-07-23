// @Author Nabil Lamriben ©2017
// @Author Jeffrey M. Paquette ©2016
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows.Speech;



public class gamestarterHelper : MonoBehaviour {

    KeywordRecognizer keywordRecognizer = null;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

    public LevelManagerArz LevelMNGR;

    public TextMesh BloodTextMesh;
    public TextMesh GameLengthTextMesh;
    public TextMesh BulletPathTextMesh;
    public TextMesh SecurityOnTextMesh;
    public TextMesh StaticHitPointsOnTextMesh;
    public TextMesh TestModeOnTextMesh;
    public TextMesh ActiveReloadOnTextMesh;
    public TextMesh AllowVibratedOnTextMesh;
    public TextMesh ZombieRootMotionOnTextMesh;
    public TextMesh IsTournamentModeOnTextMesh;


    string StartGameLeftstr = "Start Game Left";
    string StartGameRightstr = "Start Game Right";
    string StartGameNoStemstr = "Start Game No Stem";  
    string MainMenustr ="Main Menu";
    string ToggleBloodEffectstr= "Toggle Blood Effect";
    // string ToggleBulletPathstr= "Toggle Bullet Path";
    string ToggleSecuritystr="Toggle Security" ;
   // string Togglestatichitpointstr = "Toggle static hit point";
    string ToggleTestModerstr = "Toggle Test Mode";
    string ToggleAllowVibratestr = "Toggle Allow Vibration";

    string SetActiveReloadLevel_Easy_str = "Set Reload Level Easy";
    string SetActiveReloadLevel_MEDIUM_str = "Set Reload Level Medium";
    string SetActiveReloadLevel_HARD_str = "Set Reload Level Hard"; //
    string SetActiveReloadLevel_Noob_str = "Set Reload Level New be"; //
   // string ToggleZombieRootMotionstr = "Toggle Zombie Root Motion";
    string ToggleIsTournamentModeOnstr = "Toggle Tournament Mode";
    // Use this for initialization
    void Start()
    {
        Debug.Log("BBBCVVVVVVOOOOIIIIICE");

        keywords.Add(StartGameLeftstr, () =>
        {
            GOTO_SetGameLeft();

        });

        keywords.Add(StartGameRightstr, () =>
        {
            GOTO_SetGameRight();

        });

        keywords.Add(StartGameNoStemstr , () =>
        {
            GOTO_SetNostem();
        });

        keywords.Add(MainMenustr , () =>
        {
            GoToMain();
        });

        keywords.Add(ToggleBloodEffectstr, () =>
        {
            ToggleBlood();
        });

    


        //keywords.Add(ToggleBulletPathstr, () =>
        //{
        //    ToggleBulletPath();
        //});

        keywords.Add(ToggleSecuritystr, () =>
        {
            ToggleSecurity();
        });

        keywords.Add(ToggleTestModerstr, () =>
        {
            ToggleTestMode();
        });

        keywords.Add(SetActiveReloadLevel_Easy_str, () =>
        {
            SetActiveReloadLevel(ARZReloadLevel.EASY);
        });

        keywords.Add(SetActiveReloadLevel_MEDIUM_str, () =>
        {
            SetActiveReloadLevel(ARZReloadLevel.MEDIUM);
        });

        keywords.Add(SetActiveReloadLevel_HARD_str, () =>
        {
            SetActiveReloadLevel(ARZReloadLevel.HARD);
        });

        keywords.Add(SetActiveReloadLevel_Noob_str, () =>
        {
            SetActiveReloadLevel(ARZReloadLevel.NOOB);
        });

        keywords.Add(ToggleAllowVibratestr, () =>
        {
            ToggleAllowVibrate();
        });


        //keywords.Add(Togglestatichitpointstr, () =>
        //{
        //    ToggleStaticHitPoints();
        //});

        //keywords.Add(ToggleZombieRootMotionstr, () =>
        //{
        //    ToggleZombieRootMotion();
        //});

        keywords.Add(ToggleIsTournamentModeOnstr, () =>
        {
            ToggleIsTournamentMode();
        });

        //
        // Tell the KeywordRecognizer about our keywords.
        if (keywordRecognizer==null)
        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());

        // Register a callback for the KeywordRecognizer and start recognizing!
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();
    }

    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;
        if (keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();
        }
    }




    ////private string IsTournamentModeOnstr;

    void keyboardInputs()
    {
        //if (Input.GetKeyDown(KeyCode.M)) { GoToMain(); }
        //if (Input.GetKeyDown(KeyCode.Q)) { GotoScan(); }
        //if (Input.GetKeyDown(KeyCode.E)) { GoToEdit(); }

        //if (Input.GetKeyDown(KeyCode.C)) { GoToCalLeft();  }
        //if (Input.GetKeyDown(KeyCode.V)) { GoToCalRight(); }

        //if (Input.GetKeyDown(KeyCode.LeftArrow)) { GOTO_SetGameLeft(); }
        //if (Input.GetKeyDown(KeyCode.DownArrow)) { GOTO_SetNostem(); }
        //if (Input.GetKeyDown(KeyCode.RightArrow)) { GOTO_SetGameRight(); }

        //if (Input.GetKeyDown(KeyCode.Alpha1)) { ToggleBlood(); }
        //if (Input.GetKeyDown(KeyCode.Alpha2)) { ToggleGameLength(); }
        //if (Input.GetKeyDown(KeyCode.Alpha3)) { ToggleBulletPath(); }
        //if (Input.GetKeyDown(KeyCode.Alpha4)) { ToggleSecurity(); }
        //if (Input.GetKeyDown(KeyCode.Alpha5)) { ToggleStaticHitPoints(); }
        //if (Input.GetKeyDown(KeyCode.Alpha6)) { ToggleTestMode(); }
        //if (Input.GetKeyDown(KeyCode.Alpha7)) { ToggleAllowVibrate(); }
        //if (Input.GetKeyDown(KeyCode.Alpha8)) { SetActiveReloadLevel(ARZReloadLevel.EASY); }
        //if (Input.GetKeyDown(KeyCode.Alpha9)) { SetActiveReloadLevel(ARZReloadLevel.MEDIUM); }
        //if (Input.GetKeyDown(KeyCode.Alpha0)) { SetActiveReloadLevel(ARZReloadLevel.HARD); }

        //if (Input.GetKeyDown(KeyCode.Q)) { ToggleZombieRootMotion(); }  

    }

    void GoToMain() { LevelMNGR.LoadScene("MainMenu"); }
    void GotoScan() { LevelMNGR.LoadScene("ScanRoom"); }
    void GoToEdit() { LevelMNGR.LoadScene("RZEditMap"); }

    void GoToCalLeft() { LevelMNGR.LoadScene("CalibrateLeft"); }
    void GoToCalRight() { LevelMNGR.LoadScene("CalibrateRight"); }


    public  void GOTO_SetGameLeft() { LevelMNGR.GoToGameLeft(); }

    public void GOTO_SetGameRight() { LevelMNGR.GoToGameRight(); }

    public void GOTO_SetNostem() { LevelMNGR.GotoGameNoSetm(); }


 

    public void ToggleBlood() {
        if (GameSettings.Instance)
        GameSettings.Instance.IsBloodOn = !GameSettings.Instance.IsBloodOn;
        else
            Debug.LogError("Must have a gamesettings");
    }

    public void ToggleBulletPath() {
        //if (GameSettings.Instance)
        //    GameSettings.Instance.IsHideBulletsPaths = !GameSettings.Instance.IsHideBulletsPaths;
        //else
            Debug.LogError("is no worki");
    }

    public void ToggleSecurity()
    {
        if (GameSettings.Instance)
            GameSettings.Instance.IsSecurityOn = !GameSettings.Instance.IsSecurityOn;
        else
            Debug.LogError("Must have a gamesettings");
    }

    //public void ToggleStaticHitPoints() {
    //    if (GameSettings.Instance)
    //        GameSettings.Instance.IsStaticHitPointsON = !GameSettings.Instance.IsStaticHitPointsON;
    //    else
    //        Debug.LogError("Must have a gamesettings");     
    //}

    public void ToggleTestMode()
    {
        if (GameSettings.Instance)
            GameSettings.Instance.IsTestModeON = !GameSettings.Instance.IsTestModeON;
        else
            Debug.LogError("Must have a gamesettings");
    }


    public void SetActiveReloadLevel( ARZReloadLevel argReloadLevel)
    {
        if (GameSettings.Instance)
            GameSettings.Instance.ReloadDifficulty = argReloadLevel;
        else
            Debug.LogError("Must have a gamesettings");
    }

    public void ToggleAllowVibrate()
    {
        if (GameSettings.Instance)
            GameSettings.Instance.IsAllowVibrate = !GameSettings.Instance.IsAllowVibrate;
        else
            Debug.LogError("Must have a gamesettings");
    }



    //public void ToggleZombieRootMotion()
    //{
    //    if (GameSettings.Instance)
    //        GameSettings.Instance.IsZombieRootMotionOn = !GameSettings.Instance.IsZombieRootMotionOn;
    //    else
    //        Debug.LogError("Must have a gamesettings");
    //}

    public void ToggleIsTournamentMode()
    {
        if (GameSettings.Instance)
            GameSettings.Instance.IsTournamentModeOn = !GameSettings.Instance.IsTournamentModeOn;
        else
            Debug.LogError("Must have a gamesettings");
    }

    void Update () {
        keyboardInputs();


        if (GameSettings.Instance) {
            if(GameSettings.Instance.IsBloodOn)BloodTextMesh.text = "("+ ToggleBloodEffectstr + ") : On";
            else
                BloodTextMesh.text = "(" + ToggleBloodEffectstr + ") : Off";

        

            //if (GameSettings.Instance.IsHideBulletsPaths)BulletPathTextMesh.text = "(" + ToggleBulletPathstr + ") : Off";
            //else
            //    BulletPathTextMesh.text = "(" + ToggleBulletPathstr + ") : On";


            if (GameSettings.Instance.IsSecurityOn) SecurityOnTextMesh.text = "(" + ToggleSecuritystr + ") : On";
            else
                SecurityOnTextMesh.text = "(" + ToggleSecuritystr + ") : Off";


            //if (GameSettings.Instance.IsStaticHitPointsON) StaticHitPointsOnTextMesh.text = "(" + Togglestatichitpointstr + "): On";
            //else
            //    StaticHitPointsOnTextMesh.text = "(" + Togglestatichitpointstr + ") : Off";

            if (GameSettings.Instance.IsTestModeON) TestModeOnTextMesh.text = "(" + ToggleTestModerstr + "): On";
            else
                TestModeOnTextMesh.text = "(" + ToggleTestModerstr + ") : Off";

            if (GameSettings.Instance.ReloadDifficulty == ARZReloadLevel.EASY) ActiveReloadOnTextMesh.text = "(Set Reload Level  easy/medium/hard/New be )  \n Cur reload mode : EASY ";
            else
            if (GameSettings.Instance.ReloadDifficulty == ARZReloadLevel.MEDIUM) ActiveReloadOnTextMesh.text = "(Set Reload Level  easy/medium/hard/New be )  \n Cur reload mode : MEDIUM ";
            else
            if (GameSettings.Instance.ReloadDifficulty == ARZReloadLevel.HARD) ActiveReloadOnTextMesh.text = "(Set Reload Level  easy/medium/hard/New be )  \n Cur reload mode : HARD ";
            else
            if (GameSettings.Instance.ReloadDifficulty == ARZReloadLevel.NOOB) ActiveReloadOnTextMesh.text = "(Set Reload Level  easy/medium/hard/New be )  \n Cur reload mode : Noob ";



            //not yet implemented
            if (GameSettings.Instance.IsAllowVibrate) AllowVibratedOnTextMesh.text = "(" + ToggleAllowVibratestr + "): On";
            else
                AllowVibratedOnTextMesh.text = "(" + ToggleAllowVibratestr + ") : Off";


            //if (GameSettings.Instance.IsZombieRootMotionOn) ZombieRootMotionOnTextMesh.text = "(" + ToggleZombieRootMotionstr + "): On";
            //else
            //    ZombieRootMotionOnTextMesh.text = "(" + ToggleZombieRootMotionstr + ") : Off";


            if (GameSettings.Instance.IsTournamentModeOn) IsTournamentModeOnTextMesh.text = "(" + ToggleIsTournamentModeOnstr + "): On";
            else
                IsTournamentModeOnTextMesh.text = "(" + ToggleIsTournamentModeOnstr + ") : Off";


        }
        else
            Debug.LogError("Must have a gamesettings");
    }
}
