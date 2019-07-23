// @Author Jeffrey M. Paquette ©2016
//#define GUNTEST
using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour, IGun
{
    #region oldStuff

    public MagazineMNGR GetGunMagMngr() { return this._myMagazineMNGR; }
    public GunType gunType;
    public GunReloadState curGunState;
    public Transform barrelTran_ACTUALSHOOTER;


    public int GetCurGunIndex() { return (int)gunType; }
    public GunType GetGunType() { return gunType; }
    GunEffects _myGunEffect;
    GunAnimate _myGunAnimate;
    // ReloadMNGR _myGunReload; //GUNRELOAD_LINK_RELOAD_METER
    MagazineMNGR _myMagazineMNGR;
    public GunAttachementsManager GunAttachements;
    bool stopSpreading = true;
    float spreadAngle = 0.0f;
    float GLOBALANGLE = 0.0f;
    int reloadDelay = 0;


    //*************is for fire methed , it can stay in Gun.cs
    ////TimerBehavior reloadTimer;                  // object that keeps track of reload time
    TimerBehavior repeatTimer;                  // object that keeps track of repeat fire
    [Tooltip("This time is only used if the weapon is an Uzi")]
    float repeatTime;
    //*************is for fire methed , it can stay in Gun.cs                    

    void GunFlash_FIRE()
    {
        if (_myGunEffect != null)
        {
            _myGunEffect.FlashEffect();
            if (gunType == GunType.PISTOL || gunType == GunType.UZI || gunType == GunType.MG61 || gunType == GunType.P90)
            {
                _myGunEffect.CasingEjectEffect();
            }
        }

    }

    #endregion



    #region ListenersANdHAndler

    bool SlowTimeOn = false;
    void SetSlowTime()
    {
        SlowTimeOn = true;
    }
    void SetNormalTime()
    {
        SlowTimeOn = false;

    }
    public GunReloadState GetGunState() { return curGunState; }
    //GUNHELPER R calls this  or from StemhandCTRL buttons 
    public void FullReplacementOfMag(/*bool argDoAudio*/)
    {
        //  GameEventsManager.Instance.CallTutoPlayerReloaded();

        if (SlowTimeOn)
        {
            _myGunAnimate.SetSpeed(14.5f);
        }
        else
        {
            _myGunAnimate.SetSpeed(0.5f);
        }

        if (curGunState == GunReloadState.A_LOCKEDANDLOAEDED)
        {
            _myGunAnimate.Gunimate_OPENSLIDER();
            curGunState = GunReloadState.B_EJECTINGMAG;    //RRRRRRRRRRRRRRRRRRRRRRRRRRRRELOADstates--------------------------------------------------------------------
        }

        if (GameSettings.Instance.GAmeSessionType != ARZGameSessionType.SINGLE)
        {
            if (GameSettings.Instance.GameMode == ARZGameModes.GameLeft_Alpha)
            {
                PropServerMessageHandler.QueueAlphaSound("b");
            }
            else
            if (GameSettings.Instance.GameMode == ARZGameModes.GameRight_Bravo)
            {
                PropServerMessageHandler.QueueBravoSound("b");
            }
        }



        _myGunEffect.AUDIO_FullReload();
    }

    public void OnSlideOutAnimComplete()
    {
        StartCoroutine(InstanciateANewMagInDelay());
        //Debug.Log("slideout done");
        DropMagFromGun();
        // curGunState = GunReloadState.C_NOMAG;    //RRRRRRRRRRRRRRRRRRRRRRRRRRRRELOADstates--------------------------------------------------------------------
    }
    IEnumerator InstanciateANewMagInDelay()
    {
        yield return new WaitForSeconds(reloadDelay);
        GunInjstantiateMagANDSLIDEINanim();
        _myGunEffect.AUDIO_PushMagIn();
        // Debug.Log("NOW i got a mag");
    }


    public void OnMagSlideAnimCompleted()
    {
        // Debug.Log("slide in done");
        curGunState = GunReloadState.A_LOCKEDANDLOAEDED;    //RRRRRRRRRRRRRRRRRRRRRRRRRRRRELOADstates--------------------------------------------------------------------

    }



    #endregion


    bool IS_ShowcaseShooting = false;

    public void AUDIO_PopMagOut()
    {
        _myGunEffect.AUDIO_PopMagOut();
    }
    public void AUDIO_PopMagIn()
    {
        _myGunEffect.AUDIO_PushMagIn();
    }
    public void AUDIO_DryFire()
    {
        _myGunEffect.AUDIO_Dry();
    }
    public void CancelSound() { _myGunEffect.AUDIO_CancelALL(); }

    void Awake()
    {
        _myGunEffect = GetComponent<GunEffects>();

        _myGunAnimate = GetComponent<GunAnimate>();
        _myMagazineMNGR = GetComponent<MagazineMNGR>();//GUNRELOAD_LINK_RELOAD_METER
                                                       // GunAttachements = GetComponent<GunAttachementsManager>();


        repeatTimer = gameObject.AddComponent<TimerBehavior>();
        Calculate_RepeatTime();
        CalculatAUTOSHOOTTIMES();

        curGunState = GunReloadState.A_LOCKEDANDLOAEDED;
    }
    //  int isThisRemembered = 0;



    private void OnEnable()
    {
        GameEventsManager.OnSlowTimeOn += SetSlowTime;
        GameEventsManager.OnSlowTimeOff += SetNormalTime;

        if (!hazbeeninitialized)
        {
            return;
        }

        if (curGunState == GunReloadState.B_EJECTINGMAG)
        {
            _myGunAnimate.SetSpeed(0.8f); //a bit of a penalty for trying to switch gun right at the start of the reload expecting to come back and accelerate the process

            // if (curGunState == GunReloadState.A_LOCKEDANDLOAEDED)
            //  {
            _myGunAnimate.Gunimate_OPENSLIDER();
            curGunState = GunReloadState.B_EJECTINGMAG;    //RRRRRRRRRRRRRRRRRRRRRRRRRRRRELOADstates--------------------------------------------------------------------
                                                           //   }

            _myGunEffect.AUDIO_PopMagOut();
            return;
        }
        else
            if (curGunState == GunReloadState.C_NOMAG)
        {

            _myMagazineMNGR.InstantiatFunctionalMagObjOnBone();
            _myGunAnimate.Gunimate_SLIDEIN();// --> SLIDEIN -> CLOSESLIDER 
            _myGunEffect.AUDIO_PushMagIn();
            curGunState = GunReloadState.D_INSERTINGMAG;
            return;
        }
        else
            if (curGunState == GunReloadState.D_INSERTINGMAG)
        {
            curGunState = GunReloadState.A_LOCKEDANDLOAEDED;
            _myGunAnimate.Gunimate_SLIDEIN();
            _myGunEffect.AUDIO_PushMagIn();

            return;
        }

    }
    private void OnDisable()
    {
        GameEventsManager.OnSlowTimeOn += SetSlowTime;
        GameEventsManager.OnSlowTimeOff -= SetNormalTime;

        //   Debug.Log("buy bye" + gameObject.name);

    }
    bool hazbeeninitialized;
    void Start()
    {

        barrelTran_ACTUALSHOOTER = GunAttachements.BulletSpawnerMovableTransform;
        GUN_START_MAG_IN();
        hazbeeninitialized = true;

        UseScope(GunScopes.Lazor);
    }








    public void ResetGunReloadState() { GUN_START_MAG_IN(); }

    public void GUN_START_MAG_IN()
    {
        _myMagazineMNGR.InstantiateMagInPlace();
        curGunState = GunReloadState.A_LOCKEDANDLOAEDED;    //RRRRRRRRRRRRRRRRRRRRRRRRRRRRELOADstates-----------------------------------------------------------------------------------------------------------------------------------------
    }
    #region RealFireMEthod
    void FireGun()
    {
        if (GameManager.Instance == null) { return; }
        if (GameSettings.Instance == null) { return; }

        if (GameManager.Instance != null)
        {
            if (GameManager.Instance.IsPlayerDead || curGunState != GunReloadState.A_LOCKEDANDLOAEDED)
            {
                return;
            }

            //if (!_myMagazineMNGR.IsMagPlaced()) { Logger.Debug("no shot no mag in "); return; }

            GameEventsManager.Instance.Call_ShooterFired();
        }

        if (GameSettings.Instance.GAmeSessionType != ARZGameSessionType.SINGLE)
        {
            if (GameSettings.Instance.GameMode == ARZGameModes.GameLeft_Alpha)
            {
                PropServerMessageHandler.QueueAlphaSound("a");
                PropServerMessageHandler.QueueAlphaFlash();
            }
            else
                if (GameSettings.Instance.GameMode == ARZGameModes.GameRight_Bravo)
            {
                PropServerMessageHandler.QueueBravoSound("a");
                PropServerMessageHandler.QueueBravoFlash();
            }
        }
        //****************************************************************************************************************************************
        //gunhelper.DrawStatic(barrelTran.transform.position, barrelTran.transform.position + (barrelTran.transform.forward * -5));
        //****************************************************************************************************************************************

        Quaternion thenewrot = DoSpread(stopSpreading);
        // gunhelper.DrawStatic(barrelTran.transform.position, thenewrot  * Vector3.one *5);

        //****************************************************************************************************************************************
        //gunhelper.DrawStatic(barrelTran.transform.position, barrelTran.transform.position + (thenewrot*(barrelTran.transform.forward * 5)  ));
        //****************************************************************************************************************************************



        // repeat fire if gun type is uzi
        if (gunType == GunType.UZI || gunType == GunType.MG61 || gunType == GunType.P90)
        {
            _myGunAnimate.PlayFastest();
            stopSpreading = false;
            repeatTimer.StartTimer(repeatTime, GUN_FIRE, false); //destroy on complete = false
        }
        else
        {
            _myGunAnimate.PlayFast();
        }


        if (_myMagazineMNGR.CanDecrementCurMagBulletCount(SlowTimeOn))
        {
            //_myGunBools.BHazBullets = true;

            //         GameObject bulletobj = Instantiate(_myMagazineMNGR.GetChamberedBullet(), barrelTran_ACTUALSHOOTER.position, thenewrot);// barrelTran.rotation);
            //Bullet bullet = bulletobj.GetComponent<Bullet>();
            BulletsManager.Instance.InstantiateBullet(1, _myMagazineMNGR.GetChamberedBullet(), barrelTran_ACTUALSHOOTER.position, thenewrot);

            if (gunType != GunType.GRENADELAUNCHER)
            {

                if (gunType == GunType.SHOTGUN)
                {
                    for (int x = 0; x < 15; x++)
                    {

                        Quaternion thenewrot_SH = DoSpread(false);
                        BulletsManager.Instance.InstantiateBullet(1, _myMagazineMNGR.GetChamberedBullet(), barrelTran_ACTUALSHOOTER.position, thenewrot_SH);


                    }
                    GLOBALANGLE = 0;
                }
            }
            else
            {
                GameObject bulletobj = Instantiate(_myMagazineMNGR.GetChamberedBullet(), barrelTran_ACTUALSHOOTER.position, thenewrot);// barrelTran.rotation);
                                                                                                                                       //Bullet bullet = bulletobj.GetComponent<Bullet>();
                                                                                                                                       //Rigidbody bulletInstance = bulletobj.GetComponent<Rigidbody>();

                // bulletInstance.AddForce(barrelTran_ACTUALSHOOTER.forward * 1600); //ADDING FORWARD FORCE TO THE FLARE PROJECTILE

            }

            _myGunAnimate.Gunimate_FIRE();
            GunFlash_FIRE();
        }
        //after a bulet is fired


        CheckForEmptyMag_andMaybeReload(SlowTimeOn);

    }
    #endregion
    void FakeFireGun()
    {
        if (GameManager.Instance.IsPlayerDead || curGunState != GunReloadState.A_LOCKEDANDLOAEDED)
        {
            return;
        }

        GameEventsManager.Instance.Call_ShooterFired();//this time , sind we are in god mode, the enemy manager will not shourtcut iheardashot()

        // repeat fire if gun type is uzi
        if (gunType == GunType.UZI || gunType == GunType.MG61 || gunType == GunType.P90)
        {
            _myGunAnimate.PlayFastest();
        }
        else
        {
            _myGunAnimate.PlayFast();
        }


        if (_myMagazineMNGR.CanDecrementCurMagBulletCount(SlowTimeOn))
        {
            if (gunType == GunType.GRENADELAUNCHER)
            {
                GameObject bulletobj = Instantiate(_myMagazineMNGR.GetChamberedBullet(), barrelTran_ACTUALSHOOTER.position, barrelTran_ACTUALSHOOTER.rotation);
                Rigidbody bulletInstance = bulletobj.GetComponent<Rigidbody>();
                bulletInstance.AddForce(barrelTran_ACTUALSHOOTER.forward * 1500); //ADDING FORWARD FORCE TO THE FLARE PROJECTILE
            }

            _myGunAnimate.Gunimate_FIRE();
            GunFlash_FIRE();
        }
        //after a bulet is fired
        CheckForEmptyMag_andMaybeReload(SlowTimeOn);
    }


    public void GUN_FIRE()
    {
        //if (GameSettings.Instance.IsIsGodModeON)
        //{
        //    FakeFireGun();
        //}
        //else
        //{

        FireGun();
        //}
    }



    public void CheckForEmptyMag_andMaybeReload(bool argOverride)
    {
        if (argOverride)
        {
            return;
        }

        if (curGunState != GunReloadState.A_LOCKEDANDLOAEDED)
        {
            return;
        }

        if (!_myMagazineMNGR.IsThereBulletsInCurmag())
        {
            FullReplacementOfMag();
        }
    }






    Quaternion DoSpread(bool argstopspread)
    {
        CalculateGlobalAngle();



        spreadAngle = GLOBALANGLE;
        float RandAng = Random.Range(0.0f, spreadAngle);
        Vector3 FireDirection = transform.forward;
        Quaternion fireRotation = Quaternion.LookRotation(FireDirection);
        Quaternion randRot = Random.rotation;
        fireRotation = Quaternion.RotateTowards(fireRotation, randRot, RandAng);
        if (!argstopspread) { return fireRotation; }

        else
        { return barrelTran_ACTUALSHOOTER.rotation; }
    }

    void CalculateGlobalAngle()
    {
        switch (gunType)
        {
            case GunType.UZI:
                //GLOBALANGLE += 0.18f;
                GLOBALANGLE += 0.1f;
                if (GLOBALANGLE > 2) { GLOBALANGLE = 2; }
                break;
            case GunType.MG61:
                GLOBALANGLE += 0.1f;                               //< same max spread as uzi 10. but gets to that max slower 
                if (GLOBALANGLE > 2) { GLOBALANGLE = 2; }
                break;
            case GunType.P90:
                GLOBALANGLE += 0.08f;
                if (GLOBALANGLE > 2) { GLOBALANGLE = 2; }   //thight spread 
                break;
            case GunType.SHOTGUN:
                GLOBALANGLE += 0.6f;
                if (GLOBALANGLE > 14) { GLOBALANGLE = 14; }   //wide spread 
                break;
            default:
                GLOBALANGLE = 0f;
                break;
        }
    }
    /*     void CalculateGlobalAngle()
    {
        switch (gunType)
        {
            case GunType.UZI:
                GLOBALANGLE += 0.8f;
                if (GLOBALANGLE > 8) { GLOBALANGLE = 8; }
                break;
            case GunType.MG61:
                GLOBALANGLE += 0.8f;                               //< same max spread as uzi 10. but gets to that max slower 
                if (GLOBALANGLE > 10) { GLOBALANGLE = 10; }
                break;
            case GunType.P90:
                GLOBALANGLE += 0.2f;
                if (GLOBALANGLE > 5) { GLOBALANGLE = 5; }   //thight spread 
                break;
            case GunType.SHOTGUN:
                GLOBALANGLE += 0.6f;
                if (GLOBALANGLE > 14) { GLOBALANGLE = 14; }   //wide spread 
                break;
            default:
                GLOBALANGLE = 0f;
                break;
        }
    }*/

    public void GUN_STOP_FIRE()
    {
        // Logger.Debug(" gun stops fire");
        _myGunAnimate.PlayNormal();
        stopSpreading = true;
        GLOBALANGLE = 0.0f;
        spreadAngle = 0.0f;
        repeatTimer.StopTimer();
    }

    public GunType GunGetGunType()
    {
        throw new System.NotImplementedException();
    }


    //UNUSED 
    //********************************************
    //add rigidbody let gravity do its thing ---> triggers animation
    //********************************************
    public void MANUAL_EJECT_MAG_OUT()
    {
        if (GameManager.Instance != null)
        {
            if (GameManager.Instance.IsPlayerDead)
            {
                return;
            }
        }
        if (curGunState == GunReloadState.A_LOCKEDANDLOAEDED)
        {
            _myGunAnimate.Gunimate_OPENSLIDER();
            curGunState = GunReloadState.B_EJECTINGMAG;    //RRRRRRRRRRRRRRRRRRRRRRRRRRRRELOADstates--------------------------------------------------------------------





        }
    }

    public void GunInjstantiateMagANDSLIDEINanim()
    {
        if (GameManager.Instance != null)
        {
            if (GameManager.Instance.IsPlayerDead)
            {
                return;
            }
        }
        if (curGunState == GunReloadState.C_NOMAG)
        {
            _myMagazineMNGR.InstantiatFunctionalMagObjOnBone();
            _myGunAnimate.Gunimate_SLIDEIN();// --> SLIDEIN -> CLOSESLIDER
            curGunState = GunReloadState.D_INSERTINGMAG;
        }
    }

    public void DropMagFromGun()
    {
        if (GameManager.Instance != null)
        {
            if (GameManager.Instance.IsPlayerDead)
            {
                return;
            }
        }
        curGunState = GunReloadState.C_NOMAG;
        _myMagazineMNGR.MAGmngerDropRigidMAg();
    }



    //public void MagicReloadBulletCount()
    //{
    //    if (GameManager.Instance.IsPlayerDead) return;
    //    _myMagazineMNGR.Refill_CurMag();
    //    _myGunEffect.AUDIO_FullReload();
    //}


    #region LAzorHolo

    public void UseScope(GunScopes argGunscope)
    {
        switch (argGunscope)
        {
            case GunScopes.NONE:
                GunAttachements.Setup_IronSights();
                break;
            case GunScopes.Sniper:
                GunAttachements.Setup_Sniper();
                break;
            case GunScopes.Kin:
                GunAttachements.Setup_KinScope();
                break;
            case GunScopes.RedDot:
                GunAttachements.Setup_RedDot();
                break;
            case GunScopes.Lazor:
                GunAttachements.Setup_Lazor();
                break;

            default:
                GunAttachements.Setup_Lazor();
                break;
        }
    }

    int scopeIndex = 0;
    public void Temp_ScopeCycleForward()
    {
        scopeIndex++;
        if (scopeIndex > 4)
        {
            scopeIndex = 0;
        }

        UseScope((GunScopes)scopeIndex);
    }

    //bool _isUsing_Lazer = true;
    //bool _is_usingHLS = false;
    //bool _isUsing_SniperScope = false;

    //public void USE_HOLOSIGHT(bool argUSe)
    //{
    //    if (argUSe)
    //    {
    //        if (HOLOSIGHTOBJ != null)
    //        {
    //            HOLOSIGHTOBJ.SetActive(true);
    //        }

    //        if (!_isUsing_Lazer)
    //        {
    //            barrelTran_ACTUALSHOOTER.position = HLS_LOC.position;
    //        }
    //        else
    //        {
    //            barrelTran_ACTUALSHOOTER.position = LAser_LOC.position;
    //        }
    //        _is_usingHLS = true;
    //    }
    //    else
    //    {
    //        if (HOLOSIGHTOBJ != null)
    //        {
    //            HOLOSIGHTOBJ.SetActive(false);
    //        }
    //        if (!_isUsing_Lazer)
    //        {
    //            barrelTran_ACTUALSHOOTER.position = TUBE_LOC.position;
    //        }
    //        else
    //        {
    //            barrelTran_ACTUALSHOOTER.position = LAser_LOC.position;
    //        }

    //        _is_usingHLS = false;
    //    }
    //}
    //public void Use_SNIPERSCOPE(bool argUSe)
    //{
    //    if (argUSe)
    //    {
    //        if (SNIPERSCOPEOBJ != null)
    //        {
    //            SNIPERSCOPEOBJ.SetActive(true);
    //            // _myGunEffect.LAzerOFF();
    //        }

    //        //if (! _isUsing_SniperScope  )
    //        //{
    //        //    barrelTran_ACTUALSHOOTER.position = HLS_LOC.position;
    //        //}
    //        //else
    //        //{
    //        //    barrelTran_ACTUALSHOOTER.position = LAser_LOC.position;
    //        //}
    //        _isUsing_SniperScope = true;
    //    }
    //    else
    //    {
    //        if (SNIPERSCOPEOBJ != null)
    //        {
    //            SNIPERSCOPEOBJ.SetActive(false);
    //            // _myGunEffect.LAzerOn();

    //        }
    //        //if (!_isUsing_SniperScope)
    //        //{
    //        //    barrelTran_ACTUALSHOOTER.position = TUBE_LOC.position;
    //        //}
    //        //else
    //        //{
    //        //    barrelTran_ACTUALSHOOTER.position = LAser_LOC.position;
    //        //}

    //        _isUsing_SniperScope = false;
    //    }
    //}


    //public void USe_Lazer(bool argUseLazer)
    //{

    //    if (argUseLazer)
    //    {
    //        //  isThisRemembered++;
    //        _myGunEffect.LAzerOn();
    //        _isUsing_Lazer = true;
    //        barrelTran_ACTUALSHOOTER.position = LAser_LOC.position;
    //    }
    //    else
    //    {
    //        //  Debug.Log("remember this gun " + gameObject.name + " used lazor " + isThisRemembered + "  times");
    //        _myGunEffect.LAzerOFF();
    //        _isUsing_Lazer = false;
    //        if (_is_usingHLS)
    //        {
    //            barrelTran_ACTUALSHOOTER.position = HLS_LOC.position;
    //        }
    //        else
    //        {
    //            barrelTran_ACTUALSHOOTER.position = TUBE_LOC.position;
    //        }

    //    }
    //}


    public void StartStopAutoShootShowcase(bool argstartstop)
    {
        IS_ShowcaseShooting = argstartstop;
    }



    void Calculate_RepeatTime()
    {
        switch (gunType)
        {
            case GunType.UZI:
                repeatTime = 0.08f;
                break;
            case GunType.MG61:
                repeatTime = 0.07f;
                break;
            case GunType.P90:
                repeatTime = 0.05f;
                break;

            default:
                repeatTime = 0f;
                break;
        }

    }


    void CalculatAUTOSHOOTTIMES()
    {
        switch (gunType)
        {
            case GunType.UZI:
                Limit = shhottimeAUTO;
                break;
            case GunType.MG61:
                Limit = shhottimeAUTO;
                break;
            case GunType.P90:
                Limit = shhottimeAUTO;
                break;
            case GunType.SHOTGUN:
                Limit = shhottimeSHOTGUN;
                break;
            case GunType.MAGNUM:
                Limit = shhottimeCOLT;
                break;
            case GunType.PISTOL:
                Limit = shhottimePISTOL;
                break;
            case GunType.GRENADELAUNCHER:
                Limit = shhottimeSHOTGUN;
                break;
            case GunType.HELL:
                Limit = shhottimeSHOTGUN;
                break;
            default:
                Limit = shhottimeSHOTGUN;
                break;
        }
    }

    float curTime = 0.1f;
    float Limit;

    float shhottimeCOLT = 0.4f;
    float shhottimePISTOL = 0.3f;
    float shhottimeAUTO = 0.05f;
    float shhottimeSHOTGUN = 0.8f;

    #endregion
}


