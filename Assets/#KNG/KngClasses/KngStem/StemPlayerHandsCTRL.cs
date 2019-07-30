// @Author Nabil Lamriben ©2018
#define ENABLE_KEYBORADINPUTS
//using HoloToolkit.Unity;
using SixenseCore;
using UnityEngine;
using UnityEngine.SceneManagement;
//controls what hand is holding what and what happens on collisions  //NO animations are done via events , and initialized when han what to do with animations
public class StemPlayerHandsCTRL : MonoBehaviour, IPlayerHandsCTRL
{


    #region ependecies
    MainHandScript MAinHandScript;
    // OffHandScript OFFhandScript;
    #endregion

    #region PublicProps
    //  public TextMesh _meshGunStatus;
    #endregion

    #region PrivateProps
    ////GunType _CurWave_Guntype;
    ////Ammunition _CurWave_Ammotype; 
    public bool isPlayerAllowedToUseStemInput = false;
    int _curGunenumIndex = 1;
    //  int _latestUnlockedGunEnumIndex;
    //UAudioManager _uaudio;
    bool _trackersLocatedAndPlaced = false;
    bool _allowExtraButtonUsage = true;
    bool _hasSelectedSecondaryGgunRight = false;
    bool _hasSelectedMaingunLeft = false;
    bool _hasShotOnceAlready = false;
    float _StemTriggerPullTimer = 0.0f;

    private IGun _curEquippedIGun;
    //public IGun CurEquippedIGun
    //{
    //    get { return _curEquippedIGun; }
    //    set { _curEquippedIGun = value; }
    //}

    protected SixenseCore.TrackerVisual[] m_trackers;

    #endregion

    #region Events
    private void OnEnable()
    {
        //isPlayerAllowedToUseStemInput = false;
        //GameEventsManager.OnSTEMCONNECTED += Handle_StemConnected;
        //GameEventsManager.OnSTEM_DIS_CONNECTED += Handle_StemDisconnected; 
        //GameEventsManager.OnOffhandTouchedSMTHN += Handle_OffHandCollision;
        //GameEventsManager.OnOffhandTouchedGUNMAG += Handle_OffHandCollidesWithMag;
        StemKitMNGR.OnVibrate += Handle_Vibrate;
        GameEventsManager.OnGunSetChanged += Handle_GunSetChanged;
        //GameEventsManager.OnUICellFilled += Handle_Segment1Ended_AutoClearOffHand;
        // GameEventsManager.OnUpdateAvailableGUnIndex += Handle_UpdateLatestUnlockedGunEnumIndex;
        StemKitMNGR.OnStartFindingTrackers += Handle_TrackersLocatedAndPlaced;
        // GameEventsManager.OnToggleONOFFExtraButtons += Handle_ToggleAllowExtraButtons;
        GameEventsManager.OnToggleInputs += Handle_ToggleAllowStemInputs;

        GameEventsManager.OnUpdateCurIGun += Handle_SetCurEquippedGun;
        GameEventsManager.OnGamePaused += Handle_ToggleAllowStemInputs;
        GameEventsManager.OnGameContinue += Handle_ToggleAllowStemInputs;
        GameEventsManager.OnWaveStartedOrReset_DEO += SetLoadedLEvelRef;
        GameEventsManager.OnCAnShoot += SetAllowShoot;
        GameEventsManager.OnCAnSwitchWeapons += SetAllowSwitch;
        GameEventsManager.OnCAnReloadReload += SetAllowReload;

    }
    private void OnDisable()
    {
        //GameEventsManager.OnSTEMCONNECTED -= Handle_StemConnected;
        //GameEventsManager.OnSTEM_DIS_CONNECTED -= Handle_StemDisconnected;
        StemKitMNGR.OnVibrate -= Handle_Vibrate;
        GameEventsManager.OnGunSetChanged -= Handle_GunSetChanged;
        // GameEventsManager.OnUpdateAvailableGUnIndex -= Handle_UpdateLatestUnlockedGunEnumIndex;
        StemKitMNGR.OnStartFindingTrackers -= Handle_TrackersLocatedAndPlaced;
        // GameEventsManager.OnToggleONOFFExtraButtons -= Handle_ToggleAllowExtraButtons;
        GameEventsManager.OnToggleInputs -= Handle_ToggleAllowStemInputs;

        GameEventsManager.OnGamePaused -= Handle_ToggleAllowStemInputs;
        GameEventsManager.OnUpdateCurIGun -= Handle_SetCurEquippedGun;
        GameEventsManager.OnGameContinue -= Handle_ToggleAllowStemInputs;
        GameEventsManager.OnWaveStartedOrReset_DEO -= SetLoadedLEvelRef;
        GameEventsManager.OnCAnShoot -= SetAllowShoot;
        GameEventsManager.OnCAnSwitchWeapons -= SetAllowSwitch;
        GameEventsManager.OnCAnReloadReload -= SetAllowReload;
    }

    bool ALLOW_TriggerRead = true;
    bool ALLOW_ReloadRead = false;
    bool ALLOW_SwitchGunRead = false;

    void SetAllowShoot(bool argallow) { ALLOW_TriggerRead = argallow; }
    void SetAllowReload(bool argallow) { ALLOW_ReloadRead = argallow; }
    void SetAllowSwitch(bool argallow) { ALLOW_SwitchGunRead = argallow; }

    void CanShoot() { SetAllowShoot(true); }
    void CanReload() { SetAllowReload(true); }
    void CanSwitch() { SetAllowSwitch(true); }


    WaveLevel _localwave;
    bool SwitchTO3gunPos = false;
    void SetLoadedLEvelRef(WaveLevel argwl)
    {
        //isPlayerAllowedToUseStemInput = true;
        _localwave = argwl;
        SwitchTO3gunPos = true;
        PlayerHand_Main_gun();

    }
    #endregion

    #region EventsHandlers
    ////tracker will broadcast using GameEventsManager Event
    //void Handle_StemDisconnected(TrackerID argid) { Debug.Log("yo ," + argid.ToString() + "is off line");
    // //   DoGame = false; _meshConnectionStatus.text = " Disconnection Heard";
    //}

    //void Handle_StemConnected(TrackerID argid) { Debug.Log("yo ," + argid.ToString() + "is ONLINE");
    //   // DoGame = true; _meshConnectionStatus.text = " Connected Heard";
    //}



    //Gun Effects flash Calls GameEventsManager Broadcast 
    void Handle_Vibrate(int x, float zeroone)
    {
        if (GameSettings.Instance.IsAllowVibrate)
        {
            SixenseCore.TrackerVisual tracker = null;
            foreach (var t in m_trackers)
            {
                if (t.HasInput)
                {
                    tracker = t;
                    break;
                }
            }

            if (tracker == null)
            {
                return;
            }

            var controller = tracker.Input;
            controller.Vibrate(x, zeroone);
        }
    }

    //GameEventsManager.FullInit()-> SetGuns() 
    //GameManager.Setgun()  
    //this.SelectNextGun()
    //this.SelectPreviousGun()
    void Handle_GunSetChanged(GunType _arggunType)
    {

        if (_arggunType == GunType.SHOTGUN)
        {
            GameManager.Instance.ENEMYMNGER_getter().Un_Aim_All_Live_Enemies();
        }
        //Debug.Log("C H A N G E t0" + _arggunType.ToString());
        _hasSelectedSecondaryGgunRight = false;
        _hasSelectedMaingunLeft = false;
        Equips_GunInMainHand(_arggunType);
    }

    void StemPlayerHandCtrl_Handle_GunSetChanged(GunType _arggunType)
    {

        if (_arggunType == GunType.SHOTGUN)
        {
            GameManager.Instance.ENEMYMNGER_getter().Un_Aim_All_Live_Enemies();
        }
        //Debug.Log("C H A N G E t0" + _arggunType.ToString());
        _hasSelectedSecondaryGgunRight = false;
        _hasSelectedMaingunLeft = false;
        if (ViveGunBundle.Instance != null)
        {
            ViveGunBundle.Instance.SetMyCurrBunThing(_arggunType);
            ViveGunBundle.Instance.ShowMyCurrBunThing();
            //_AnmController.MainHandAnimateHoldGun(argguntype);

        }
    }


    //this.Handle_GunSetChanged(ttupe)->  this.Equips_GunInMainHand(type)  GunsBundles.Show/or/hideMyCurrBunThing() ->  Set_EquipedGunObject_Visible()
    void Handle_SetCurEquippedGun(IGun argIgun)
    {
        _curEquippedIGun = argIgun;
    }



    //GameEventsManager.Start() -> INitHandsBundles()
    void Handle_TrackersLocatedAndPlaced(bool argBool)
    {
        _trackersLocatedAndPlaced = argBool;
    }

    //void Handle_ToggleAllowExtraButtons(bool argOnOff)
    //{
    //    _allowExtraButtonUsage = argOnOff;
    //}

    void Handle_ToggleAllowStemInputs(bool argb)
    {
        //if (argb == true)
        //{
        //    //
        //}
        Debug.Log("togglke stem " + argb.ToString());
        isPlayerAllowedToUseStemInput = argb;
    }


    #endregion

    delegate void helpdebug(float xf, float yf);
    void PRINTER(float xf, float yf) { Debug.Log("Xval= " + xf + "  Yval" + yf); }
    LeftMidRight GunSlotPosition;

    public TextMesh VibrationReader;
    private void Start()
    {
        print("hannnnnnssxxxxxxx" + gameObject.name);
        GunSlotPosition = LeftMidRight.MID;
        isPlayerAllowedToUseStemInput = false; //<may need to change this back to false, thisis just for testing weapon swap
                                               // _uaudio = GetComponent<UAudioManager>();
                                               // _latestUnlockedGunEnumIndex = 8;
    }

    private void Update()
    {
#if UNITY_EDITOR

        Keyboardinput();
        //TextStatus();
#endif
        ReadStemInput();

    }

    public void InitPlayerHandScripts(MainHandScript argMAIN)
    {
        m_trackers = argMAIN.gameObject.GetComponentsInParent<SixenseCore.TrackerVisual>();
        MAinHandScript = argMAIN;
    }

    // tracking meter // evrytime i put a gun in my hand(bundle gun enable) -> I endup making gunbundles pass a reff to metergo to the gun activated
    void Equips_GunInMainHand(GunType gunType)
    {
        if (GameSettings.Instance.Controlertype != ARZControlerType.StemControlSystem)
        {
            return;
        }

        MAinHandScript.Equip_byGunType(gunType);
    }

    void Fires_EquippedGun()
    {
        if (_curEquippedIGun != null)
            _curEquippedIGun.GUN_FIRE();
    }
    void FireStops_EquippedGun()
    {
        if (_curEquippedIGun != null)
            _curEquippedIGun.GUN_STOP_FIRE();
    }
    void Reloads_EuippedGUn()
    {
        if (_curEquippedIGun != null)
            GameEventsManager.Instance.CallTutoPlayerreloaded();
        _curEquippedIGun.FullReplacementOfMag();

    }

    void Use_EquippedGunScope(GunScopes argGunScope)
    {
        _curEquippedIGun.UseScope(argGunScope);

    }


    #region StemKeyboardInputs

    void ReadJoystickHorizontalInput(Tracker ArgTracker)
    {
        //    -1                   -0.2             0             0.2                   1
        //           
        if (ArgTracker.JoystickX > 0.2f)
        {
            if (!_hasSelectedSecondaryGgunRight)
            {
                PlayerHand_Secondary_gun();
            }
        }
        if (ArgTracker.JoystickX < 0.2f)
        {
            _hasSelectedSecondaryGgunRight = false;
        }
        if (ArgTracker.JoystickX < -0.2f)
        {
            if (!_hasSelectedMaingunLeft)
            { PlayerHand_Main_gun(); }
        }
        if (ArgTracker.JoystickX > -0.2f)
        { _hasSelectedMaingunLeft = false; }

    }


    void ReadTriggetInput(Tracker ArgTracker)
    {

        if (ArgTracker.GetButton(Buttons.TRIGGER))
        {
            _StemTriggerPullTimer += Time.deltaTime;
            if (!_hasShotOnceAlready)
            {

                Fires_EquippedGun();
                FireStops_EquippedGun();
                _hasShotOnceAlready = true;
            }


            if (_StemTriggerPullTimer > 0.2f)
            {
                _StemTriggerPullTimer = 0.0f;
                if (_curEquippedIGun.GetCurGunIndex() == (int)GunType.UZI || _curEquippedIGun.GetCurGunIndex() == (int)GunType.MG61 || _curEquippedIGun.GetCurGunIndex() == (int)GunType.P90) { Fires_EquippedGun(); }

                //ItStopsShooting();
            }
        }
        if (ArgTracker.GetButtonUp(Buttons.TRIGGER))
        {
            _hasShotOnceAlready = false;
            _StemTriggerPullTimer = 0.0f;
            FireStops_EquippedGun();
        }

    }
    float timetoreload = 3f;
    float curcountingtime = 0f;

    //void OldReadStemInput()
    //{
    //    if (GameSettings.Instance.Controlertype != ARZControlerType.StemControlSystem)
    //    {
    //        return;
    //    }

    //    helpdebug hd = PRINTER;
    //    if (_trackersLocatedAndPlaced)
    //    {
    //        SixenseCore.TrackerVisual tracker = null;
    //        foreach (var t in m_trackers)
    //        {
    //            if (t.HasInput)
    //            {
    //                tracker = t;
    //                break;
    //            }
    //        }

    //        if (tracker == null)
    //        {
    //            return;
    //        }

    //        var controller = tracker.Input;



    //        if (controller.GetButtonUp(Buttons.X))
    //        {
    //            curcountingtime = 0f;

    //        }

    //        if (controller.GetButton(Buttons.X))
    //        {
    //            curcountingtime += Time.deltaTime;

    //        }

    //        if (curcountingtime > timetoreload)
    //        {

    //            SceneManager.LoadScene("KngSetupMenu");
    //        }



    //        if (isPlayerAllowedToUseStemInput)
    //        {
    //            if (ALLOW_SwitchGunRead)
    //            {
    //                ReadJoystickHorizontalInput(controller);
    //            }

    //            if (ALLOW_TriggerRead)
    //            {
    //                ReadTriggetInput(controller);
    //            }

    //            if (controller.GetAnyButtonDown() && !controller.GetButton(Buttons.TRIGGER) && !controller.GetButton(Buttons.BUMPER))
    //            {
    //                if (ALLOW_ReloadRead)
    //                {
    //                    Reloads_EuippedGUn();
    //                }
    //            }
    //            //controller.GetAnyButtonDown(

    //            //if (controller.GetButtonDown(Buttons.NEXT) ||
    //            //  controller.GetButtonDown(Buttons.PREV))
    //            //{
    //            //    Replace_LoadedMag();
    //            //}

    //            if (controller.GetButtonDown(Buttons.A))
    //            {
    //                CallSkipTutorial();
    //            }
    //            //if (controller.GetButtonDown(Buttons.BUMPER))
    //            //{
    //            //    IsUsingLazer = !IsUsingLazer;
    //            //    _curEquippedIGun.USe_Lazer(IsUsingLazer);
    //            //}
    //            if (controller.GetButtonDown(Buttons.BUMPER))
    //            {

    //                if (!GameSettings.Instance.IsCanToggleLazerOn)
    //                {
    //                    return;
    //                }

    //                SwitchScopesRoundRobbin();

    //            }


    //            if (controller.GetButtonDown(Buttons.Y))
    //            {
    //                SwitchNonOcclusion();
    //            }
    //            if (controller.GetButtonDown(Buttons.B))
    //            {
    //                SwitchOcclusion();
    //            }
    //        }


    //    }

    //}
    void ReadStemInput()
    {
        if (GameSettings.Instance.Controlertype != ARZControlerType.StemControlSystem)
        {
            return;
        }

        helpdebug hd = PRINTER;
        if (_trackersLocatedAndPlaced)
        {
            SixenseCore.TrackerVisual tracker = null;
            foreach (var t in m_trackers)
            {
                if (t.HasInput)
                {
                    tracker = t;
                    break;
                }
            }

            if (tracker == null)
            {
                return;
            }

            var controller = tracker.Input;



            if (controller.GetButtonUp(Buttons.X))
            {
                curcountingtime = 0f;

            }

            if (controller.GetButton(Buttons.X))
            {
                curcountingtime += Time.deltaTime;

            }

            if (curcountingtime > timetoreload)
            {

                SceneManager.LoadScene("KngSetupMenu");
            }



            if (isPlayerAllowedToUseStemInput)
            {

                if (controller.GetButton(Buttons.BUMPER))
                {

                    print("holdingbumb");

                    if (controller.GetButtonDown(Buttons.PREV))
                    {
                        print("PREV");


                    }

                }


                if (ALLOW_SwitchGunRead)
                {
                    ReadJoystickHorizontalInput(controller);
                }

                if (ALLOW_TriggerRead)
                {
                    ReadTriggetInput(controller);
                }

                if (controller.GetAnyButtonDown() && !controller.GetButton(Buttons.TRIGGER) && !controller.GetButton(Buttons.BUMPER))
                {
                    if (ALLOW_ReloadRead)
                    {
                        Reloads_EuippedGUn();
                    }
                }
                //controller.GetAnyButtonDown(

                //if (controller.GetButtonDown(Buttons.NEXT) ||
                //  controller.GetButtonDown(Buttons.PREV))
                //{
                //    Replace_LoadedMag();
                //}

                if (controller.GetButtonDown(Buttons.A))
                {
                    CallSkipTutorial();
                }





                if (controller.GetButtonDown(Buttons.BUMPER))
                {

                    //if (!GameSettings.Instance.IsCanToggleLazerOn)
                    //{
                    //    return;
                    //}

                    SwitchScopesRoundRobbin();

                }


                if (controller.GetButtonDown(Buttons.Y))
                {
                    SwitchNonOcclusion();
                }
                if (controller.GetButtonDown(Buttons.B))
                {
                    SwitchOcclusion();
                }
            }


        }

    }

    int LaserHoloNone;
    void SwitchScopesRoundRobbin()
    {
        LaserHoloNone++;
        if (LaserHoloNone > 4)
        {
            LaserHoloNone = 0;
        }

        Use_EquippedGunScope((GunScopes)LaserHoloNone);
    }



    //int LaserHoloNone = 0;

    //bool IsUsingHLS = true;
    //bool IsUsingLazer = true;
    //bool ToggleSnioerScoepe = false;
    //void TurnOnSniperScope(bool argOnOff) { _curEquippedIGun.Use_SNIPERSCOPE(argOnOff); }
    //put this in update for reload state debugging
    void TextStatus()
    {
        if (_curEquippedIGun != null)
        {
            // string magstat = _curEquippedIGun.GetGunMagMngr().IsMagPlaced() == true ? " ON" : "___";
            //  string bulletstat = _curEquippedIGun.GetGunMagMngr().IsThereBulletsInCurmag() == true ? " ON" : "___";


            string mystatusline = "";
            mystatusline += "STATE :" + _curEquippedIGun.GetGunState().ToString() + "\n";
            //mystatusline += "MagazineIN :" + magstat + "\n";
            //mystatusline += "has bullets:" + bulletstat + "\n";

            mystatusline += "\n";
            mystatusline += "cur gun index from gun" + _curEquippedIGun.GetCurGunIndex();
            mystatusline += "cur gun index" + _curGunenumIndex;
            //string mystatusline= " MGin " + _IseeThisGun.GEtGunBools().BmagIn.ToString() + "| bull " + _IseeThisGun.GEtGunBools().BHazBullets.ToString() + "| accepnew " + _IseeThisGun.GEtGunBools().CanAcceptNewClip.ToString()+ "| reloa? " + _IseeThisGun.GEtGunBools().BisReloading.ToString();
            //_meshGunStatus.text = mystatusline;
        }
    }


    GunType PregameGun1 = GunType.PISTOL;
    GunType PregameGun2 = GunType.SHOTGUN;


    void PlayerHand_Secondary_gun()
    {
        if (GameManager.Instance.KngGameState == ARZState.Pregame)
        {
            GameEventsManager.Instance.CallTutoPlayerChangedWeapon();
            Pregame_NEXT_GunSelection();
        }
        else
        {
            NEXT_GunSelection();
        }
    }
    void PlayerHand_Main_gun()
    {
        if (GameManager.Instance.KngGameState == ARZState.Pregame)
        {
            GameEventsManager.Instance.CallTutoPlayerChangedWeapon();
            Pregame_PREV_GunSelection();
        }
        else
        {
            PREV_GunSelection();
        }
    }

    void Pregame_NEXT_GunSelection()
    {
        if (_curEquippedIGun.GetGunType() == PregameGun1)
        {
            GameEventsManager.Call_GunSetChangeTo(PregameGun2);
        }
    }
    void Pregame_PREV_GunSelection()
    {
        if (_curEquippedIGun.GetGunType() == PregameGun2)
        {
            GameEventsManager.Call_GunSetChangeTo(PregameGun1);
        }
    }

    void NEXT_GunSelection()
    {
        if (_localwave == null)
        {
            GunSlotPosition = LeftMidRight.MID;
            GameEventsManager.Call_GunSetChangeTo(GunType.PISTOL);
            _hasSelectedSecondaryGgunRight = true;
            return;
        }

        //       if (_curEquippedIGun.GUN_GET_BOOLS().ThisGunIsReloading) { _curEquippedIGun.MagicReloadBulletCount(); }
        if (_hasSelectedSecondaryGgunRight)
        {
            return;
        }

        if (GunSlotPosition == LeftMidRight.MID)
        {
            GameEventsManager.Call_GunSetChangeTo(_localwave.Get_LevelGunType(LeftMidRight.RIGHT));
            GunSlotPosition = LeftMidRight.RIGHT;
            _hasSelectedSecondaryGgunRight = true;

        }

    }
    void PREV_GunSelection()
    {
        //if (_curEquippedIGun.GUN_GET_BOOLS().ThisGunIsReloading) { _curEquippedIGun.MagicReloadBulletCount(); }
        if (_localwave == null)
        {
            GunSlotPosition = LeftMidRight.MID;
            GameEventsManager.Call_GunSetChangeTo(GunType.PISTOL);
            _hasSelectedMaingunLeft = true;
            return;
        }
        if (_hasSelectedMaingunLeft)
        {
            return;
        }

        if (GunSlotPosition == LeftMidRight.RIGHT)
        {
            GameEventsManager.Call_GunSetChangeTo(_localwave.Get_LevelGunType(LeftMidRight.MID));
            GunSlotPosition = LeftMidRight.MID;
            _hasSelectedMaingunLeft = true;

        }
    }

    void CallSkipTutorial() { GameEventsManager.Instance.CAll_SKIPtuto(); }

    bool FiveorSix;
    int One234 = 0;
    void SwitchNonOcclusion()
    {
        FiveorSix = !FiveorSix;
        if (FiveorSix)
        {
            GameEventsManager.Instance.CAll_Matchange(5);
        }
        else
        {
            GameEventsManager.Instance.CAll_Matchange(6);
        }
    }

    void SwitchOcclusion()
    {
        One234++;
        if (One234 > 4) One234 = 0;
        GameEventsManager.Instance.CAll_Matchange(One234);

    }
    void Keyboardinput()
    {
#if ENABLE_KEYBORADINPUTS
        if (isPlayerAllowedToUseStemInput)
        {
            if (Input.GetKeyUp(KeyCode.LeftBracket)) { if (ALLOW_SwitchGunRead) { PlayerHand_Main_gun(); } }
            if (Input.GetKeyUp(KeyCode.RightBracket)) { if (ALLOW_SwitchGunRead) { PlayerHand_Secondary_gun(); } }

            if (Input.GetKeyDown(KeyCode.R)) { if (ALLOW_ReloadRead) { Reloads_EuippedGUn(); } }
            if (Input.GetKeyDown(KeyCode.LeftControl)) { if (ALLOW_TriggerRead) { Fires_EquippedGun(); } }
            if (Input.GetKeyUp(KeyCode.LeftControl)) { if (ALLOW_TriggerRead) { FireStops_EquippedGun(); } }
            if (Input.GetKeyDown(KeyCode.X)) { CallSkipTutorial(); }

            //if (Input.GetKeyDown(KeyCode.Backspace))
            //{

            //}

            //if (Input.GetKeyUp(KeyCode.Alpha5))
            //{
            //    SwitchNonOcclusion();
            //}


            //if (Input.GetKeyUp(KeyCode.Alpha0))
            //{
            //    SwitchOcclusion();
            //}

            KEYBOARD_PERIFFERALS();
        }
#endif
    }


    //void KB_next_Ctrl() { if (SwitchTO3gunPos) { KEYBOARDSelectNextGun_new(); } else { KEYBOARDSelectNextGun(); } }
    //void KB_prev_Ctrl() { if (SwitchTO3gunPos) { KEYBOARDSelectPreviousGun_new(); } else { KEYBOARDSelectPreviousGun(); } }


    //void KEYBOARDSelectNextGun()
    //{
    //    _curGunenumIndex = _curEquippedIGun.GetCurGunIndex();
    //    _curGunenumIndex++;
    //    //if (_curGunenumIndex > _latestUnlockedGunEnumIndex)
    //    //{
    //    //    _curGunenumIndex = _latestUnlockedGunEnumIndex;
    //    //};
    //    GameEventsManager.Call_GunSetChangeTo((GunType)_curGunenumIndex);
    //}
    //void KEYBOARDSelectPreviousGun()
    //{

    //    _curGunenumIndex = _curEquippedIGun.GetCurGunIndex();
    //    _curGunenumIndex--;
    //    if (_curGunenumIndex < 1) { _curGunenumIndex = 1; };
    //    GameEventsManager.Call_GunSetChangeTo((GunType)_curGunenumIndex);
    //}


    void KEYBOARDSelectNextGun_new()
    {
        if (_localwave == null)
        {
            GunSlotPosition = LeftMidRight.MID;
            GameEventsManager.Call_GunSetChangeTo(GunType.PISTOL);
            _hasSelectedSecondaryGgunRight = true;
            return;
        }

        if (GunSlotPosition == LeftMidRight.MID)
        {
            // GameEventsManager.Call_GunSetChangeTo(_localwave.Get_GunType_R());
            GunSlotPosition = LeftMidRight.RIGHT;
        }
        else
         if (GunSlotPosition == LeftMidRight.LEFT)
        {
            //   GameEventsManager.Call_GunSetChangeTo(_localwave.Get_GunType_M());
            GunSlotPosition = LeftMidRight.MID;
        }
    }
    void KEYBOARDSelectPreviousGun_new()
    {
        if (_localwave == null)
        {
            GunSlotPosition = LeftMidRight.MID;
            GameEventsManager.Call_GunSetChangeTo(GunType.PISTOL);
            _hasSelectedMaingunLeft = true;
            return;
        }
        if (GunSlotPosition == LeftMidRight.RIGHT)
        {
            //    GameEventsManager.Call_GunSetChangeTo(_localwave.Get_GunType_M());
            GunSlotPosition = LeftMidRight.MID;
        }
    }

    void KEYBOARD_PERIFFERALS()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            SwitchScopesRoundRobbin();
        }
        //    if (Input.GetKeyDown(KeyCode.F))
        //{
        //    IsUsingHLS = !IsUsingHLS;
        //    _curEquippedIGun.USE_HOLOSIGHT(IsUsingHLS);
        //}
        //if (Input.GetKeyDown(KeyCode.G))
        //{
        //    IsUsingLazer = !IsUsingLazer;
        //    _curEquippedIGun.USe_Lazer(IsUsingLazer);
        //}
    }




    void STM_next_Ctrl()
    {
        if (SwitchTO3gunPos)
        {
            SelectNextGun_NEw();
        }
        else
        {
            SelectNextGun();
        }
    }
    void STM_prev_Ctrl()
    {
        if (SwitchTO3gunPos)
        {
            SelectPreviousGun_New();
        }
        else
        {
            SelectPreviousGun();

        }
    }
    void SelectNextGun()
    {
        //    if (_curEquippedIGun.GUN_GET_BOOLS().ThisGunIsReloading) { _curEquippedIGun.MagicReloadBulletCount(); }
        if (_hasSelectedSecondaryGgunRight)
        {
            return;
        }

        _curGunenumIndex = _curEquippedIGun.GetCurGunIndex();
        _curGunenumIndex++;
        //if (_curGunenumIndex > _latestUnlockedGunEnumIndex)
        //{
        //    _curGunenumIndex = _latestUnlockedGunEnumIndex;
        //};
        GameEventsManager.Call_GunSetChangeTo((GunType)_curGunenumIndex);
        _hasSelectedSecondaryGgunRight = true;
    }
    void SelectPreviousGun()
    {
        if (_localwave == null)
        {
            GunSlotPosition = LeftMidRight.MID;
            GameEventsManager.Call_GunSetChangeTo(GunType.PISTOL);
            _hasSelectedMaingunLeft = true;
            return;
        }

        //       if (_curEquippedIGun.GUN_GET_BOOLS().ThisGunIsReloading) { _curEquippedIGun.MagicReloadBulletCount(); }
        if (_hasSelectedMaingunLeft)
        {
            return;
        }

        _curGunenumIndex = _curEquippedIGun.GetCurGunIndex();
        _curGunenumIndex--;
        if (_curGunenumIndex < 1) { _curGunenumIndex = 1; };
        GameEventsManager.Call_GunSetChangeTo((GunType)_curGunenumIndex);
        _hasSelectedMaingunLeft = true;
    }
    void SelectNextGun_NEw()
    {
        if (_localwave == null)
        {
            GunSlotPosition = LeftMidRight.MID;
            GameEventsManager.Call_GunSetChangeTo(GunType.PISTOL);
            _hasSelectedSecondaryGgunRight = true;
            return;
        }

        //       if (_curEquippedIGun.GUN_GET_BOOLS().ThisGunIsReloading) { _curEquippedIGun.MagicReloadBulletCount(); }
        if (_hasSelectedSecondaryGgunRight)
        {
            return;
        }

        if (GunSlotPosition == LeftMidRight.MID)
        {
            //       GameEventsManager.Call_GunSetChangeTo(_localwave.Get_GunType_R());
            GunSlotPosition = LeftMidRight.RIGHT;
            _hasSelectedSecondaryGgunRight = true;

        }
        else
         if (GunSlotPosition == LeftMidRight.LEFT)
        {
            //          GameEventsManager.Call_GunSetChangeTo(_localwave.Get_GunType_M());
            GunSlotPosition = LeftMidRight.MID;
            _hasSelectedSecondaryGgunRight = true;

        }
    }
    void SelectPreviousGun_New()
    {
        //        if (_curEquippedIGun.GUN_GET_BOOLS().ThisGunIsReloading) { _curEquippedIGun.MagicReloadBulletCount(); }
        if (_hasSelectedSecondaryGgunRight)
        {
            return;
        }

        if (GunSlotPosition == LeftMidRight.RIGHT)
        {
            //       GameEventsManager.Call_GunSetChangeTo(_localwave.Get_GunType_M());
            GunSlotPosition = LeftMidRight.MID;
            _hasSelectedSecondaryGgunRight = true;

        }
    }


    #endregion


}



