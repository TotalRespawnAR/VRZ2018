using UnityEngine;

public class ViveGunBundle : MonoBehaviour, IBundle
{
    public static ViveGunBundle Instance = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        M1911 = Instantiate(gunM1911, this.TheCtrl.transform.position, this.TheCtrl.transform.rotation);
        M1911.name = "Gun_M1911";
        M1911.transform.parent = this.TheCtrl.transform;

        Mac11 = Instantiate(gunMac11, this.TheCtrl.transform.position, this.TheCtrl.transform.rotation);
        Mac11.name = "Gun_Mac11";
        Mac11.transform.parent = this.TheCtrl.transform;

        Colt = Instantiate(gunColt, this.TheCtrl.transform.position, this.TheCtrl.transform.rotation);
        Colt.name = "Gun_Colt";
        Colt.transform.parent = this.TheCtrl.transform;

        Shotgun = Instantiate(gunShotgun, this.TheCtrl.transform.position, this.TheCtrl.transform.rotation);
        Shotgun.name = "Gun_Shotgun";
        Shotgun.transform.parent = this.TheCtrl.transform;



        P90 = Instantiate(gunP90, this.TheCtrl.transform.position, this.TheCtrl.transform.rotation);
        P90.name = "Gun_P90";
        P90.transform.parent = this.TheCtrl.transform;


        MG61 = Instantiate(gunMG61, this.TheCtrl.transform.position, this.TheCtrl.transform.rotation);
        MG61.name = "Gun_MG61";
        MG61.transform.parent = this.TheCtrl.transform;

        Hell = Instantiate(gunHell, this.TheCtrl.transform.position, this.TheCtrl.transform.rotation);
        Hell.name = "Gun_Hell";
        Hell.transform.parent = this.TheCtrl.transform;

        FlareG = Instantiate(gunFlareG, this.TheCtrl.transform.position, this.TheCtrl.transform.rotation);
        FlareG.name = "Gun_FlareG";
        FlareG.transform.parent = this.TheCtrl.transform;


        HideAllMyThings();
        //SetMyCurrBunThing(GunType.P90);
        //ShowMyCurrBunThing();

        isPlayerAllwedTOUseController = false; //<may need to change this back to false, thisis just for testing weapon swap
                                               // _uaudio = GetComponent<UAudioManager>();
                                               // _latestUnlockedGunEnumIndex = 8;


        //   Use_EquippedGunScope(GunScopes.Lazor);
    }



    public GameObject gunM1911;
    public GameObject gunMac11;
    public GameObject gunColt;
    public GameObject gunShotgun;
    public GameObject gunP90;
    public GameObject gunMG61;
    public GameObject gunHell;
    public GameObject gunFlareG;
    public GameObject TheCtrl;


    GameObject M1911;
    GameObject Mac11;
    GameObject Colt;
    GameObject Shotgun;
    GameObject P90;
    GameObject MG61;
    GameObject Hell;
    GameObject FlareG;


    bool _isEquipedGunVisible;
    GameObject _curGunObject;
    IGun CurGunScript;

    public IGun GetActiveGunScript() { return CurGunScript; }



    #region InterfaceRegion

    public bool IsMyThingShowing()
    {
        if (_curGunObject == null) { return false; }
        if (!_isEquipedGunVisible) { return false; }
        return true;
    }

    public void HideAllMyThings()
    {
        M1911.SetActive(false);
        Colt.SetActive(false);
        Mac11.SetActive(false);
        Shotgun.SetActive(false);
        P90.SetActive(false);
        MG61.SetActive(false);
        Hell.SetActive(false);
        FlareG.SetActive(false);
    }


    // tracking meter //stemplayerctrl.ItPutsGunInHand or maginhand ->  handscript.ANYHAD_EQUIP  
    public void SetMyCurrBunThing(GunType argGuntype)
    {
        //unequip previous weapon
        if (CurGunScript != null)
        {
            CurGunScript.CancelSound();
            _curGunObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("tried to equip but no weapon was found");
        }
        switch (argGuntype)
        {
            case GunType.PISTOL:
                _curGunObject = M1911;
                break;
            case GunType.MAGNUM:
                _curGunObject = Colt;
                break;
            case GunType.UZI:
                _curGunObject = Mac11;
                break;
            case GunType.SHOTGUN:
                _curGunObject = Shotgun;
                break;
            case GunType.P90:
                _curGunObject = P90;
                break;
            case GunType.MG61:
                _curGunObject = MG61;
                break;
            case GunType.HELL:
                _curGunObject = Hell;
                break;
            case GunType.GRENADELAUNCHER:
                _curGunObject = FlareG;
                break;

        }
        // _curGunObject.GetComponent<Gun>().Gun_MAgic_MAg_Refill();

        if (_curGunObject != null)
        {
            CurGunScript = _curGunObject.transform.GetChild(0).gameObject.GetComponent<IGun>();
            //if (CurGunScript.GUN_GET_BOOLS().ThisGunIsReloading) {
            //    CurGunScript.FullReplacementOfMag_NoSOund();
            //};
        }
        else
        {
            Debug.LogWarning("no curr weapon Onject ");
        }
    }


    public void ShowMyCurrBunThing()
    {
        _isEquipedGunVisible = true;
        if (_curGunObject != null)
        {
            _curGunObject.SetActive(true);
        }
        else { Debug.LogWarning("no curr weapon Onject "); }
    }

    public void HideMyCurrBunThing()
    {
        Set_EquipedGunObject_Visible(false);
    }


    #endregion


    public void Set_EquipedGunObject_Visible(bool argVisible)
    {        // make equipped clip active
        _isEquipedGunVisible = argVisible;
        if (_curGunObject != null)
        {
            _curGunObject.SetActive(argVisible);

            GameEventsManager.Call_SetCurIgunTo(CurGunScript);
        }
        else { Debug.LogWarning("no curr weapon Onject "); }
    }





    #region ependecies
    //MainHandScript MAinHandScript;
    // OffHandScript OFFhandScript;
    #endregion

    #region PublicProps
    //  public TextMesh _meshGunStatus;
    #endregion

    #region PrivateProps
    ////GunType _CurWave_Guntype;
    ////Ammunition _CurWave_Ammotype; 
    public bool isPlayerAllwedTOUseController = false;
    int _curGunenumIndex = 1;
    //  int _latestUnlockedGunEnumIndex;
    //UAudioManager _uaudio;
    bool _trackersLocatedAndPlaced = false;
    bool _allowExtraButtonUsage = true;
    public bool _hasSelectedSecondaryGgunRight = true;
    public bool _hasSelectedMaingunLeft = true;
    bool _hasShotOnceAlready = false;
    float _StemTriggerPullTimer = 0.0f;


    //public IGun CurEquippedIGun
    //{
    //    get { return _curEquippedIGun; }
    //    set { _curEquippedIGun = value; }
    //}


    #endregion

    #region Events
    private void OnEnable()
    {


        GameEventsManager.OnUpdateCurIGun += Handle_SetCurEquippedGun;
        GameEventsManager.OnGunSetChanged += Handle_GunSetChanged;
        GameEventsManager.OnToggleInputs += Handle_ToggleAllowStemInputs;

        GameEventsManager.OnGamePaused += Handle_ToggleAllowStemInputs;
        GameEventsManager.OnGameContinue += Handle_ToggleAllowStemInputs;
        GameEventsManager.OnWaveStartedOrReset_DEO += SetLoadedLEvelRef;
        GameEventsManager.OnCAnShoot += SetAllowShoot;
        GameEventsManager.OnCAnSwitchWeapons += SetAllowSwitch;
        GameEventsManager.OnCAnReloadReload += SetAllowReload;

    }
    private void OnDisable()
    {
        GameEventsManager.OnGunSetChanged -= Handle_GunSetChanged;
        GameEventsManager.OnUpdateCurIGun -= Handle_SetCurEquippedGun;
        GameEventsManager.OnToggleInputs -= Handle_ToggleAllowStemInputs;

        GameEventsManager.OnGamePaused -= Handle_ToggleAllowStemInputs;
        GameEventsManager.OnGameContinue -= Handle_ToggleAllowStemInputs;

        GameEventsManager.OnWaveStartedOrReset_DEO -= SetLoadedLEvelRef;
        GameEventsManager.OnCAnShoot -= SetAllowShoot;
        GameEventsManager.OnCAnSwitchWeapons -= SetAllowSwitch;
        GameEventsManager.OnCAnReloadReload -= SetAllowReload;
    }

    bool ALLOW_TriggerRead = true;
    bool ALLOW_ReloadRead = false;
    bool ALLOW_SwitchGunRead = false;

    #region GameEvents
    void SetLoadedLEvelRef(WaveLevel argwl)
    {
        _localwave = argwl;
        SwitchTO3gunPos = true;

        PlayerHand_Main_gun();

    }
    void SetAllowShoot(bool argallow) { ALLOW_TriggerRead = argallow; }
    void SetAllowSwitch(bool argallow) { ALLOW_SwitchGunRead = argallow; }
    void SetAllowReload(bool argallow) { ALLOW_ReloadRead = argallow; }
    #endregion



    WaveLevel _localwave;
    bool SwitchTO3gunPos = false;
    #endregion

    #region EventsHandlers

    void Handle_GunSetChanged(GunType _arggunType)
    {

        if (_arggunType == GunType.SHOTGUN)
        {
            GameManager.Instance.ENEMYMNGER_getter().Un_Aim_All_Live_Enemies();
        }
        //Debug.Log("C H A N G E t0" + _arggunType.ToString());
        _hasSelectedSecondaryGgunRight = true;
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
        //  _hasSelectedSecondaryGgunRight = false;
        // _hasSelectedMaingunLeft = false;
        //if (ViveGunBundle.Instance != null)
        //{
        //    ViveGunBundle.Instance.SetMyCurrBunThing(_arggunType);
        //    ViveGunBundle.Instance.ShowMyCurrBunThing();
        //    //_AnmController.MainHandAnimateHoldGun(argguntype);

        //}
        HideAllMyThings();
        SetMyCurrBunThing(_arggunType);
        ShowMyCurrBunThing();

    }

    //this.Handle_GunSetChanged(ttupe)->  this.Equips_GunInMainHand(type)  GunsBundles.Show/or/hideMyCurrBunThing() ->  Set_EquipedGunObject_Visible()
    void Handle_SetCurEquippedGun(IGun argIgun)
    {
        CurGunScript = argIgun;
    }


    void Handle_ToggleAllowStemInputs(bool argb)
    {

        isPlayerAllwedTOUseController = argb;
    }


    #endregion

    //  LeftMidRight GunSlotPosition;




    // tracking meter // evrytime i put a gun in my hand(bundle gun enable) -> I endup making gunbundles pass a reff to metergo to the gun activated
    void Equips_GunInMainHand(GunType gunType)
    {
        if (GameSettings.Instance.Controlertype != ARZControlerType.StemControlSystem)
        {
            return;
        }
        StemPlayerHandCtrl_Handle_GunSetChanged(gunType);

    }

    void Fires_EquippedGun()
    {
        if (CurGunScript != null)
        {
            CurGunScript.GUN_FIRE();
        }
    }
    void FireStops_EquippedGun()
    {
        if (CurGunScript != null)
        {
            CurGunScript.GUN_STOP_FIRE();
        }
    }
    void Reloads_EuippedGUn()
    {
        if (CurGunScript != null)
        {
            GameEventsManager.Instance.CallTutoPlayerreloaded();
        }

        CurGunScript.FullReplacementOfMag();

    }

    void Use_EquippedGunScope(GunScopes argGunScope)
    {

        if (CurGunScript != null)
        {
            CurGunScript.UseScope(argGunScope);
        }

    }


    #region StemKeyboardInputs


    int LaserHoloNone = 0;
    public void SwitchScopesRoundRobbin()
    {
        LaserHoloNone++;
        if (LaserHoloNone > 4)
        {
            LaserHoloNone = 0;
        }

        Use_EquippedGunScope((GunScopes)LaserHoloNone);
    }




    GunType PregameGun1 = GunType.P90;
    GunType PregameGun2 = GunType.SHOTGUN;


    public void PlayerHand_Secondary_gun()
    {
        if (GameManager.Instance.KngGameState == ARZState.Pregame)
        {
            return;
            //    //GameEventsManager.Instance.CallTutoPlayerChangedWeapon();
            //    Pregame_NEXT_GunSelection();
        }
        //else
        //{
        //    NEXT_GunSelection();
        //}
        Equips_GunInMainHand(_localwave.Get_LevelGunType(LeftMidRight.MID));
    }
    public void PlayerHand_Main_gun()
    {
        if (GameManager.Instance.KngGameState == ARZState.Pregame)
        {
            return;
            //GameEventsManager.Instance.CallTutoPlayerChangedWeapon();
            //  Pregame_PREV_GunSelection();
        }
        //else
        //{
        //    PREV_GunSelection();
        //}

        Equips_GunInMainHand(_localwave.Get_LevelGunType(LeftMidRight.RIGHT));
    }


    //void NEXT_GunSelection()
    //{
    //    if (_localwave == null)
    //    {
    //        GunSlotPosition = LeftMidRight.MID;
    //        GameEventsManager.Call_GunSetChangeTo(GunType.PISTOL);
    //        _hasSelectedSecondaryGgunRight = true;
    //        return;
    //    }

    //    //       if (_curEquippedIGun.GUN_GET_BOOLS().ThisGunIsReloading) { _curEquippedIGun.MagicReloadBulletCount(); }
    //    if (_hasSelectedSecondaryGgunRight)
    //    {
    //        return;
    //    }

    //    if (GunSlotPosition == LeftMidRight.MID)
    //    {
    //        GameEventsManager.Call_GunSetChangeTo(_localwave.Get_LevelGunType(LeftMidRight.RIGHT));
    //        GunSlotPosition = LeftMidRight.RIGHT;
    //        _hasSelectedSecondaryGgunRight = true;

    //    }

    //}
    //void PREV_GunSelection()
    //{
    //    //if (_curEquippedIGun.GUN_GET_BOOLS().ThisGunIsReloading) { _curEquippedIGun.MagicReloadBulletCount(); }
    //    if (_localwave == null)
    //    {
    //        GunSlotPosition = LeftMidRight.MID;
    //        GameEventsManager.Call_GunSetChangeTo(GunType.PISTOL);
    //        _hasSelectedMaingunLeft = true;
    //        return;
    //    }
    //    if (_hasSelectedMaingunLeft)
    //    {
    //        return;
    //    }

    //    if (GunSlotPosition == LeftMidRight.RIGHT)
    //    {
    //        GameEventsManager.Call_GunSetChangeTo(_localwave.Get_LevelGunType(LeftMidRight.MID));
    //        GunSlotPosition = LeftMidRight.MID;
    //        _hasSelectedMaingunLeft = true;

    //    }
    //}


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




    //void STM_next_Ctrl()
    //{
    //    if (SwitchTO3gunPos)
    //    {
    //        SelectNextGun_NEw();
    //    }
    //    else
    //    {
    //        SelectNextGun();
    //    }
    //}
    //void STM_prev_Ctrl()
    //{
    //    if (SwitchTO3gunPos)
    //    {
    //        SelectPreviousGun_New();
    //    }
    //    else
    //    {
    //        SelectPreviousGun();

    //    }
    //}
    //void SelectNextGun()
    //{
    //    //    if (_curEquippedIGun.GUN_GET_BOOLS().ThisGunIsReloading) { _curEquippedIGun.MagicReloadBulletCount(); }
    //    if (_hasSelectedSecondaryGgunRight)
    //    {
    //        return;
    //    }

    //    _curGunenumIndex = CurGunScript.GetCurGunIndex();
    //    _curGunenumIndex++;
    //    //if (_curGunenumIndex > _latestUnlockedGunEnumIndex)
    //    //{
    //    //    _curGunenumIndex = _latestUnlockedGunEnumIndex;
    //    //};
    //    GameEventsManager.Call_GunSetChangeTo((GunType)_curGunenumIndex);
    //    _hasSelectedSecondaryGgunRight = true;
    //}
    //void SelectPreviousGun()
    //{
    //    if (_localwave == null)
    //    {
    //        GunSlotPosition = LeftMidRight.MID;
    //        GameEventsManager.Call_GunSetChangeTo(GunType.PISTOL);
    //        _hasSelectedMaingunLeft = true;
    //        return;
    //    }

    //    //       if (_curEquippedIGun.GUN_GET_BOOLS().ThisGunIsReloading) { _curEquippedIGun.MagicReloadBulletCount(); }
    //    if (_hasSelectedMaingunLeft)
    //    {
    //        return;
    //    }

    //    _curGunenumIndex = CurGunScript.GetCurGunIndex();
    //    _curGunenumIndex--;
    //    if (_curGunenumIndex < 1) { _curGunenumIndex = 1; };
    //    GameEventsManager.Call_GunSetChangeTo((GunType)_curGunenumIndex);
    //    _hasSelectedMaingunLeft = true;
    //}
    //void SelectNextGun_NEw()
    //{
    //    if (_localwave == null)
    //    {
    //        GunSlotPosition = LeftMidRight.MID;
    //        GameEventsManager.Call_GunSetChangeTo(GunType.PISTOL);
    //        _hasSelectedSecondaryGgunRight = true;
    //        return;
    //    }

    //    if (_hasSelectedSecondaryGgunRight)
    //    {
    //        return;
    //    }

    //    if (GunSlotPosition == LeftMidRight.MID)
    //    {
    //        GunSlotPosition = LeftMidRight.RIGHT;
    //        _hasSelectedSecondaryGgunRight = true;

    //    }
    //    else
    //     if (GunSlotPosition == LeftMidRight.LEFT)
    //    {
    //        GunSlotPosition = LeftMidRight.MID;
    //        _hasSelectedSecondaryGgunRight = true;

    //    }
    //}
    //void SelectPreviousGun_New()
    //{
    //    if (_hasSelectedSecondaryGgunRight)
    //    {
    //        return;
    //    }

    //    if (GunSlotPosition == LeftMidRight.RIGHT)
    //    {
    //        GunSlotPosition = LeftMidRight.MID;
    //        _hasSelectedSecondaryGgunRight = true;

    //    }
    //}


    #endregion



}
