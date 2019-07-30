//#define  ENABLE_DEBUGLOG
//#define ENABLE_KEYBORADINPUTS

//using HoloToolkit.Unity;
using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

[RequireComponent(typeof(EnemyMeshComponent))]
[RequireComponent(typeof(EnemyMoverComponent))]
[RequireComponent(typeof(EnemyTargetDebugger))]
[RequireComponent(typeof(EnemyRaggDollComponent))]
[RequireComponent(typeof(EnemyAudioManager))]


// Damage and Score shoud be able to register to the onbulletwoonaflicted in OnEnabe. so they need to be added in gamemanager.Dresscomp
//[RequireComponent(typeof(EnemyDamageComponent))]
//[RequireComponent(typeof(EnemyScoreComponent))]

/// use ArgsEvent for sending anything to unrequired components.
/// those will be added by GM.DressComponents(go) ,and each will link up to Ibeh on their own 
/// IEBCdoes not need to know about those 


public class MainEntityComponent : MonoBehaviour, IEnemyEntityComp
{
    #region  for things taht use root motion or not
    bool RootMotion = true; //nmyboxBehaviorWillnot
    #endregion
    #region PrivatVars DirrectLink
    CharacterController m_cc;
    IEnemyMoverComp m_IMoverComp; //should be the one requiering an Ianimatorcompo
    IEnemyMeshComp m_ImeshComp;
    IEnemyRagdolComp m_IraggComp;
    public int _Id;
    public KNode _CurAndOnlyKnode;
    public ARZombieypes _EnemyType;

    Transform m_HeadT;
    Transform m_ChestT;
    Transform m_HipsT;
    Transform m_RhandT;
    Transform m_LhandT;

    bool _disolveOUTstarted;
    bool _disolveINstarted;
    bool _originalBoolSkipAnimDeath;
    public bool _IamAxmanOrPreyOrBoss;

    IEnemyEntityComp m_prey;
    // public GameObject PrimitivePreyMagnet; //for hurlers
    GameObject PrimitivePreyMagnetCHILD;
    public Rigidbody RB_PrimitivePreyMagnetCHILD;
    public GameObject RocketObj;
    #endregion



    #region Props
    EnemyAnimatorV6Component myAnimatorObj;
    IEnemyBehaviorObj _curMODE;
    //  UAudioManager audioMnger;

    public IEnemyBehaviorObj CurMODE
    {
        get
        {
            return _curMODE;
        }

        set
        {
            _curMODE = value;
            Broadcast_Mode();
        }
    }
    public int _curHP;
    public int _originalHP;
    public int CurHP
    {
        get
        {
            return _curHP;
        }

        set
        {
            _curHP = value;
        }
    }

    int _maxHP;
    public int MaxHP
    {
        get
        {
            return _maxHP;
        }

        set
        {
            _maxHP = value;
        }
    }

    Vector3 _targetposition;
    public Vector3 Targetposition
    {
        get
        {
            return _targetposition;
        }

        set
        {
            _targetposition = value;
#if ENABLE_DEBUGLOG
            // Debug.Log("new target pos " + _targetposition);
#endif

        }
    }
    int _strength;
    KngEnemyName _myname;
    public Transform _MyLookatTarget;
    public Transform _MyHandTartget;

    public int STRENGTH
    {
        get
        {
            if (_strength == 0) { _strength = 1; }

            return _strength;
        }

        set
        {
            if (_strength == 0) { _strength = 1; }
            _strength = value;
        }
    }

    //this is initialized at InitBeh() that way i can keep 10 as a base value if the wave starts in sprint mode
    float _seekAudioRandomeLoopTime = 10f; //max or sprinters. sprinters do the crazy Infected garble on 10 second loop
    bool _seekAudioTriggered;
    #endregion
    //EBD_REACHGUNPLAYER
    public void Set_MyHandTarget(Transform argextratarget)
    {
        _MyHandTartget = argextratarget;
    }

    public Transform Get_MyhandTArget()
    {
        return _MyHandTartget;
    }
    //not yet .. maybe seek shold
    public void Set_MyLookatTarget(Transform argextratarget)
    {
        _MyLookatTarget = argextratarget;
    }

    public Transform Get_MyLookatTArget()
    {
        return _MyLookatTarget;
    }





    public virtual void StartEnemy()
    {
        ////Debug.Log("MUST OVERRIDE STARTENEMY");
        //if (GetMyType()==ARZombieypes.GRAVESKELETON)
        //{

        //    CurMODE = new EBD_StartTimer(this, ModesEnum.STARTIDLE, 3f, StartActionEmerge, false);
        //    CurMODE.StartBehavior();
        //}
        //else
        //{
        //    //CurMODE = new EB_Start(this);
        //    CurMODE = new EBD_StartTimer(this, ModesEnum.STARTGRAVE, 3f, StartActionIdle , false);
        //    CurMODE.StartBehavior();
        //}
    }

    #region PrivateMethods

    void InitShouldRagdollDeath()
    {
        if ((GetMyType() == ARZombieypes.AXEMAN) || (GetMyType() == ARZombieypes.PREYgrave) || (GetMyType() == ARZombieypes.GRAVESKELETON))
        {
            _IamAxmanOrPreyOrBoss = true; //well who needs to skip death with rag? looks like evryone
            _originalBoolSkipAnimDeath = _IamAxmanOrPreyOrBoss;

        }
        else
        {
            _IamAxmanOrPreyOrBoss = false;
            _originalBoolSkipAnimDeath = _IamAxmanOrPreyOrBoss;

        }
    }



    public string[] SplitCommand(string argcom)
    {

        return argcom.Split('_');
    }

    public bool StringMatch(string str, string str2)
    {
        if (str == null && str2 == null)
        {
            return true;
        }

        return string.CompareOrdinal(str, str2) == 0 ? true : false;
    }

    void Broadcast_Mode()
    {
        ArgsEnemyMode e = new ArgsEnemyMode(_curMODE.GET_MyModeEnum());
        if (OnModeUpdated != null)
        {
            OnModeUpdated(this, e);
        }
    }

    void Broadcast_BulletWoond(Bullet argBullet, int argPoints, int argNewHp)
    {
        if (argBullet.HitBoss)
        {

            PLAY_AUDIOBANK("_armording");
        }

        bool Killshot = (argNewHp <= 0);

        ArgsBulletWoond woond = new ArgsBulletWoond(
                    argBullet.MyType,
                   argBullet.hitInfo.point,
                   argBullet.hitInfo.normal,
                   argBullet.damage,
                   argBullet.BulletPointsType,
                   argPoints,
                   false,
                   argNewHp,
                   Killshot
                   );

        if (OnBulletWoundInflicted != null)
        {
            OnBulletWoundInflicted(this, woond);
        }
    }

    void DeathInformsEnemyManager_AndDetroy()
    {

        Trigger_EndBEhaviorTASK(EnemyTaskEneum.KillmeBeforeILayEggs);
        GameManager.Instance.Zombie_ID_Died(Get_ID());
        StartCoroutine(WaitTOBEdestroyed());
    }
    IEnumerator WaitTOBEdestroyed()
    {
        yield return new WaitForSeconds(5f);
        //audioMnger.StopAll();
        CurMODE.EndBehavior();
        DestroyImmediate(this.gameObject);
    }
    #endregion

    float _curTimeloop;

    #region MonoMethods
    bool targettingISRunning = false;
    bool untargettingISRunning = false;
    void Awake()
    {
        //m_cc = GetComponent<CharacterController>();

        ////m_IMoverComp = GetComponent<IEnemyMoverComp>();//requires
        //CurHP = 120;
    }

    void OnEnable()
    {
        GameEventsManager.OnSlowTimeOn += IsSlowTimeON;
        GameEventsManager.OnSlowTimeOff += IsSlowTimeOff;
    }

    void OnDisable()
    {
        GameEventsManager.OnSlowTimeOn -= IsSlowTimeON;
        GameEventsManager.OnSlowTimeOff -= IsSlowTimeOff;
    }

    void Start()
    {
        m_cc = GetComponent<CharacterController>();
        myAnimatorObj = GetComponent<EnemyAnimatorV6Component>();
        if (myAnimatorObj == null)
        {
            Debug.Log("no animobj");
        }


#if ENABLE_DEBUGLOG
        if (m_cc == null) { Debug.Log("EBC Didnot find charcterctonntroller!!!"); }
#endif
        float startDelay = 1f;
        StartEnemy();
    }

    bool hasGongged = false;
    void FixedUpdate()
    {
        //if (this.transform.position.y > 2)
        //{
        //    // Debug.LogError("2 hi");
        //    print("HHHIIII");
        //    if (!hasGongged)
        //    {
        //        hasGongged = true;
        //        PlayHeadShotSound.Instance.PlayGongSound();
        //    }
        //}

        if (_curMODE != null)
        {
            _curMODE.RunBehavior();
            if (targettingISRunning)
            {
                Get_Animer().TweenHandIk(2f, AgroUpDown.Up);
            }

            if (untargettingISRunning)
            {
                Get_Animer().TweenHandIk(2f, AgroUpDown.Down);
            }

            _curTimeloop += Time.deltaTime;

            if (_curTimeloop >= _seekAudioRandomeLoopTime)
            {
                //if (!_seekAudioTriggered) {

                //    _seekAudioTriggered = true;
                //}

                _curTimeloop = 0f;
                Play_AUDIO_by_SeekSatate();

            }

#if ENABLE_KEYBORADINPUTS
                        if (Input.GetKeyDown(KeyCode.Alpha1))
                        {
                DooRagDelay(0.0f);
                        }
                        if (Input.GetKeyDown(KeyCode.Alpha2))
                        {
                DooRagDelay(0.6f);

            }

#endif

            if (GetMyType() == ARZombieypes.TankFighter)
            {
                RzPlayerComponent.Instance.PlayHud_Debug_LiveUPdate(_ISAimedAt.ToString());
                if (_curMODE.GET_MyModeEnum() != ModesEnum.DEAD)
                {
                    float dis = Vector3.SqrMagnitude(transform.position - KnodeProvider.Instance.GEt_PlayerAlphaBravo().PosOfTrans) + 2f;
                    float clampedD = 1 - (Mathf.Clamp((dis / 1000), 0, 1));
                    RzPlayerComponent.Instance.ModulateThrillVolume(clampedD);
                }
                else
                {
                    RzPlayerComponent.Instance.ModulateThrillVolume(0);
                }
            }
        }


    }

    public void CompletedAnimationOrAction(string actionAnim)
    {
        throw new NotImplementedException();
    }











    #endregion


    #region InterfaceImplementation
    public event EventHandler<ArgsEnemyMode> OnModeUpdated;
    public event EventHandler<ArgsBulletWoond> OnBulletWoundInflicted;
    public IEnemyBehaviorObj Get_Cur_EB_Mode()
    {
        return this._curMODE;
    }
    public void Kill_CurrMODE()
    {

        if (Get_Cur_EB_Mode() != null)
        {
            Get_Cur_EB_Mode().EndBehavior();
        }
    }
    public void Set_EB_Mode(IEnemyBehaviorObj argModeObj) //called by curently loaded modeobject
    {
        if (_curMODE != null)
        {
            //_curMODE.DisposeObj();
            _curMODE = null;
        }
        CurMODE = argModeObj;
    }

    bool startedInjuredWalk;
    int leghitsToCrawl = 0;
    bool hasfllen; //this will be managed by animer if we want to get back up
    int curleghitcnt = 0;
    public void Set_HP(Bullet argBullet, int argPoints, int argnewHP)
    {




        Broadcast_BulletWoond(argBullet, argPoints, argnewHP);
        CurHP = argnewHP; //will call is dead right after woond was processed
        if (GetMyType() != ARZombieypes.TankFighter && GetMyType() != ARZombieypes.AXEMAN)
        {
            if ((float)CurHP < (((float)_originalHP) / 4f))
            {
                Get_Animer().iSet_Animator_WeaknessInt(3);
                Get_Animer().iSet_SeekSpeedNum((int)SeekSpeed.injuredwalk);
            }
            else
            if ((float)CurHP < (((float)_originalHP) / 2f))
            {
                Get_Animer().iSet_Animator_WeaknessInt(2);



            }
            else
            if ((float)CurHP < (((float)_originalHP * 2) / 3f)) { Get_Animer().iSet_Animator_WeaknessInt(1); }
            else
            {
                // Get_Animer().iSet_SeekSpeedNum(0);
                Get_Animer().iSet_Animator_WeaknessInt(0);
            }
        }

        if (argBullet.HitLegs)
        {
            //  Get_Animer().DO_DESpeed(); //wil only woork if we ar in seekspeed.sprint chek is done in animer
            curleghitcnt++;

            if (Get_Animer().iGet_SeekSpedNum() == 3)
            {
                if (!hasfllen)
                {
                    if (curleghitcnt >= leghitsToCrawl)
                    {
                        hasfllen = true;
                        // Debug.LogError("fall");
                        if (GetMyType() == ARZombieypes.STANDARD || GetMyType() == ARZombieypes.Sprinter)
                        {

                            if (!Get_Animer().iGet_CrawlingBool())
                            {
                                Get_Animer().iSet_Animator_CanReactBool(false);
                                Get_Animer().Do_WalkToFAllToCrawl();
                            }
                        }
                        else
                        {
                            if (!Get_Animer().iGet_CrawlingBool())
                            {
                                Get_Animer().DO_DESpeed();
                            }
                        }
                    }
                }
            }

        }

        //EBD will call DoGutSho or DoHEsadshot in animer
        //        if (argBullet.BulletPointsType == BulletPointsType.Head)
        //        {
        //#if ENABLE_DEBUGLOG
        //            Debug.Log("headshot");
        //#endif
        //        }
        //        else
        //                if (argBullet.BulletPointsType == BulletPointsType.Torso)
        //        {
        //#if ENABLE_DEBUGLOG
        //            Debug.Log("headshot");
        //#endif
        //        }
#if ENABLE_DEBUGLOG
        Debug.Log(" nw hp" + CurHP);
#endif
        if (CurHP <= 0)
        {
            //Set_Mode(new EB_Dead(this));
            HandleDeath();
        }
    }
    public int Get_HP()
    {
        return CurHP;
    }


    public int Get_OriginalHP()
    {
        return _maxHP;
    }
    public int Get_ID()
    {
        return this._Id;
    }
    public void Set_ID(int argId)
    {
        this._Id = argId;
    }

    public int Get_Strength()
    {
        return this._strength;
    }

    public void Set_Strengt(int argStren)
    {
        this._strength = argStren;
    }
    public EnemyAnimatorV6Component Get_Animer()
    {
        return this.myAnimatorObj;
    }

    EnemyAudioManager MyAudioManager;
    public void InitBehavior(Data_Enemy argData)
    {

        _Id = argData.ID;
        Set_CurAndOnlyKnode(argData.SpawnKnode);
        // _CurAndOnlyKnode = argData.SpawnKnode;

        m_IMoverComp = GetComponent<IEnemyMoverComp>();
        m_ImeshComp = GetComponent<IEnemyMeshComp>();
        m_IraggComp = GetComponent<IEnemyRagdolComp>();

        MyAudioManager = GetComponent<EnemyAudioManager>();
        //audioMnger = GetComponent<UAudioManager>();
#if ENABLE_DEBUGLOG
        if (m_IMoverComp == null) { Debug.Log("Start() EBEH NO IMOVER!!!"); } else { Debug.Log("Start() EBEH found Imover"); }
#endif

        m_IMoverComp.InitAnimator(argData.LevelSeekSpeed, RootMotion);

        _maxHP = argData.HitPoints;
        CurHP = argData.HitPoints;
        _originalHP = CurHP;
        _EnemyType = argData.Ztype_STD;
        _strength = argData.HitStrength;
        _myname = argData.ModelName;

        if (argData.LevelSeekSpeed == SeekSpeed.sprint)
        {
            _seekAudioRandomeLoopTime = 9.8f; // the audio clips for sprinting are 10 and 10.2 second slong. just crop them bothe equally
        }
        else
        {
            _seekAudioRandomeLoopTime = UnityEngine.Random.Range(2.5f, 5f);
        }
        _seekAudioTriggered = false;

        //who can block
        if (GameSettings.Instance.UseNab)
        {
            if (
         _myname == KngEnemyName.CHECKER ||
        // _myname == KngEnemyName.PAUL ||
        _myname == KngEnemyName.MEATHEADBLACK ||
        _myname == KngEnemyName.MEATHEADWHITE ||
        _myname == KngEnemyName.BIGMUTANT ||
        // _myname == KngEnemyName.KNIFEFIGHTER ||
        _myname == KngEnemyName.SARAHCONNOR ||
         _myname == KngEnemyName.PumpkinShort ||
          _myname == KngEnemyName.PUMPKIN ||
           _myname == KngEnemyName.MUTANTBLUE
        )
            {
                canBlockAimForOldOrGraver = true;
            }
            else
            {
                canBlockAimForOldOrGraver = false;
            }
        }
        else
        {
            if (_myname == KngEnemyName.PumpkinShort || _myname == KngEnemyName.PUMPKIN | _myname == KngEnemyName.MUTANTBLUE)
            {
                canBlockAimForOldOrGraver = true;
            }
            else
            {
                canBlockAimForOldOrGraver = false;
            }
        }






        InitShouldRagdollDeath();

    }
    public void SetSkipDeathAnimForRagAndFall_FORSLOWDEATH(bool argSkipRag)
    {
        if (argSkipRag == true) //slowtime is on 
        {
            _IamAxmanOrPreyOrBoss = false;
        }
        else
        {
            _IamAxmanOrPreyOrBoss = _originalBoolSkipAnimDeath;
        }
    }


    public virtual void OnAnimationEventSTR(string argstr)
    {

    }
    public virtual void OnActionAnimationEndEvent()
    {
        Debug.Log("override me and chose what to do depending on cur clipplying");
    }

    public void CompletedCombatAction(int argActionTypeNumber)
    {
        Get_Cur_EB_Mode().CompleteAnyAnim(argActionTypeNumber);
    }

    bool pausedAttackNoneAnimEventAttack = false;
    public void PauseEnemy(bool argOnOff)
    {
        if (Get_Animer() != null)
        {
            Get_Animer().iToggleAnimatorOffWithDelay(!argOnOff, 0.1f);
            pausedAttackNoneAnimEventAttack = argOnOff;
        }
        else
        {
            Debug.Log("problem on " + Get_ID());
        }
    }



    public void SetTargPos(Vector3 argTargPos)
    { //set by Seek in constructor

        Targetposition = argTargPos;
    }


    public Vector3 GetTargPos() { return Targetposition; }
    public Vector3 GetMyPos() { return this.transform.position; }

    public KNode Get_CurAndOnlyKnode()
    {
        return _CurAndOnlyKnode;
    }

    public void Set_CurAndOnlyKnode(KNode argNEKnode)
    {
        SetTargPos(argNEKnode.GetPos());
        _CurAndOnlyKnode = argNEKnode;
    }

    public void DamagePlayer(TriggersDamageEffects argTrigDamEff, int ArgDamagePoints)
    {
        if (pausedAttackNoneAnimEventAttack)
        {
#if ENABLE_DEBUGLOG
            Debug.Log(" cannot Damageplayer");
#endif
            return;
        }
        else
        {
#if ENABLE_DEBUGLOG
            Debug.Log("boom boom");
#endif
            // HitType hittype = HitType.ZombieScratch;//wil be managed by ienemyattackprops smallattack bigattack damageplayerhp
            RzPlayerComponent.Instance.DamageHumanPlayer(argTrigDamEff, ArgDamagePoints); // frr now , the animtaor does it 
        }
    }



    public void Set_mySpecialBonetrans(Transform arghead, Transform argChest, Transform argHips, Transform argRhand, Transform argLhand)
    {
        m_HeadT = arghead;
        m_ChestT = argChest;
        m_HipsT = argHips;
        m_RhandT = argRhand;
        m_LhandT = argLhand;
    }

    public Transform Get_MyHEADtrans() { return m_HeadT; }
    public Transform Get_my_RHandtrans() { return m_RhandT; }
    public Transform Get_my_LHandtrans() { return m_LhandT; }
    public Transform Get_MyCHESTtrans() { return m_ChestT; }
    public Transform GEt_MyHIpsTans() { return m_HipsT; }
    public Transform GEt_MyTransform() { return this.transform; }

    //public void RunDisolveToNothing() {
    //    m_ImeshComp.RunApplyDissovefactoactor();
    //}
    //public void TriggerStartDisolveOUT() { m_ImeshComp.MeshDisolveToNothing(); }

    public IEnemyMoverComp Get_Mover()
    {
        return this.m_IMoverComp;
    }

    public IEnemyRagdolComp Get_Ragger()
    {
        return this.m_IraggComp;
    }

    public IEnemyEntityComp Get_AccessToMyPrey() { return this.m_prey; }
    public IEnemyMeshComp GEt_MEsher() { return this.m_ImeshComp; }
    public void Set_MyPrey(IEnemyEntityComp argThispoorGuy) { m_prey = argThispoorGuy; }

    public virtual void HandleDeath()
    {
#if ENABLE_DEBUGLOG
        Debug.Log("Base HAndleDeath");
#endif

        DeathInformsEnemyManager_AndDetroy();
    }
    public bool IsAxemanPreyOrBoss() { return _IamAxmanOrPreyOrBoss; }
    public ARZombieypes GetMyType() { return _EnemyType; }
    //public void RaggPreyEntity()
    //{
    //    m_cc.enabled = false;
    //    m_IraggComp.ToggleAllKinematics(false, true, false);
    //    Get_Animer().iToggleAnimatorOffWithDelay(false, 0.2f);
    //}
    ////used in EB_GrabHurl
    //public void RaggPReyEntity(bool argEnableCC, bool argUsehighendrag, bool argEnableColliders)
    //{

    //    Get_Animer().iToggleAnimatorOffWithDelay(false, 0.2f);
    //    m_IraggComp.ToggleAllKinematics(argEnableCC,true,false);
    //    m_IraggComp.EnablePreprocess(argUsehighendrag);
    //    m_IraggComp.EnableColliders(argEnableColliders);
    //    m_cc.enabled = argEnableCC;
    //}



    public void CreatePrimitivePreyMagnet()
    {
        //if (PrimitivePreyMagnet != null) Destroy(PrimitivePreyMagnet);

        //PrimitivePreyMagnet = new GameObject();
        //PrimitivePreyMagnet.name = "PRimMagnetPArent";
        //PrimitivePreyMagnet.transform.parent = Get_my_RHandtrans();
        //PrimitivePreyMagnet.transform.localPosition = new Vector3(0, -0.18f, 0.05f);
        //PrimitivePreyMagnet.AddComponent<Rigidbody>();
        //Rigidbody PrimitivMagnetrigidbody = PrimitivePreyMagnet.GetComponent<Rigidbody>();
        //PrimitivMagnetrigidbody.useGravity = false;
        //PrimitivMagnetrigidbody.isKinematic = true;

        PrimitivePreyMagnetCHILD = new GameObject
        {
            name = "PrimitivePreyMagnetCHILD"
        };
        PrimitivePreyMagnetCHILD.transform.parent = m_prey.GEt_MyTransform();
        //Destroy(PrimitivePreyMagnetCHILD.GetComponent<SphereCollider>());
        //PrimitivePreyMagnetCHILD.transform.parent = PrimitivePreyMagnet.transform;
        // PrimitivePreyMagnetCHILD.transform.localPosition = new Vector3(0f, 0, 0);
        PrimitivePreyMagnetCHILD.transform.position = Get_AccessToMyPrey().Get_MyHEADtrans().position + new Vector3(0, 0.1f, 0);
        //PrimitivePreyMagnetCHILD.transform.localScale = new Vector3(sphererad, sphererad, sphererad);
        PrimitivePreyMagnetCHILD.AddComponent<Rigidbody>();
        Rigidbody rigidbody = PrimitivePreyMagnetCHILD.GetComponent<Rigidbody>();
        RB_PrimitivePreyMagnetCHILD = rigidbody;
        rigidbody.useGravity = false;
        rigidbody.isKinematic = true;
    }
    public GameObject GetMyPrimitiveMagnet()
    {
        return PrimitivePreyMagnetCHILD;
    }
    public void GivePtimitiveToPrey()
    {
        //RB_PrimitivePreyMagnetCHILD.transform.parent = m_prey.GEt_MyTransform();
        //RB_PrimitivePreyMagnetCHILD.transform.localPosition = new Vector3(0, 0, 0);
    }
    public void PredatorDoPrimitiveFollowHand()
    {
        RB_PrimitivePreyMagnetCHILD.MovePosition(Get_my_RHandtrans().position + new Vector3(-0.3f, 0f, -0.1f));
    }
    public void RunTaHandTargetting() { if (!targettingISRunning) { targettingISRunning = true; } untargettingISRunning = false; }
    public void RunHAndUNtargetting() { if (!untargettingISRunning) { untargettingISRunning = true; } targettingISRunning = false; }
    public void StopAllHandTargetting()
    {
        targettingISRunning = false;
        untargettingISRunning = false;
    }

    public void BuildRocket()
    {
        if (RocketObj != null)
        {
            Destroy(RocketObj);
        }
        RocketObj = new GameObject();
        RocketObj.name = "rocket";
        RocketObj.transform.position = Get_my_RHandtrans().position + new Vector3(0, 0f, 0);
    }

    public void Run_Rocket()
    {
        RocketObj.transform.LookAt(Camera.main.transform.position);
        RocketObj.transform.Translate(Vector3.forward * Time.deltaTime * 6f);
        RB_PrimitivePreyMagnetCHILD.MovePosition(RocketObj.transform.position);

    }

    public void Destroy_Prey_RocketObj_PrimitivMagnet()
    {
#if ENABLE_DEBUGLOG
#endif
        Debug.Log("call prey suicide");
        m_prey.KillYourselfandCeanitup();
        Destroy(PrimitivePreyMagnetCHILD);
        Destroy(RocketObj);
    }

    //called from meshcomp , after the whole disolv...just wait 4 seconds to make sure the disolve is done
    public void KillYourselfandCeanitup()
    {
        Debug.LogError("DO NOT USE , does not remove from enemy manager");
        // GameManager.Instance.Zombie_ID_Died(Get_ID());
        StartCoroutine(WaitTOBEdestroyed());
    }












    #endregion

    public virtual void Trigger_EndBEhaviorTASK(EnemyTaskEneum argTAskName) //called from Ianimator when onemerged happens
    {

        //CurMODE.FinishedTask(argTAskName);
        switch (argTAskName)
        {
            case EnemyTaskEneum.EndFirstAnimation:
                if (CurMODE.GET_MyModeEnum() == ModesEnum.STARTGRAVE || CurMODE.GET_MyModeEnum() == ModesEnum.STARTIDLE)
                {
                    CurMODE.EndBehavior();//anything to call the unsubscriber
#if ENABLE_DEBUGLOG
                    Debug.Log("->ovr KSEEK");
#endif

                    CurMODE = new EBD_Seek(this, ModesEnum.KSEEK, canBlockAimForOldOrGraver, 3f, StartActionSeek, true, 6, 1.2f);
                    CurMODE.StartBehavior();
                }
                break;
            case EnemyTaskEneum.ReachedKnodeSaught:
                if (CurMODE.GET_MyModeEnum() == ModesEnum.KSEEK)
                {
                    CurMODE.EndBehavior();
#if ENABLE_DEBUGLOG
                    Debug.Log("->->->ovr KSEEK");
#endif
                    CurMODE = new EBD_Seek(this, ModesEnum.KSEEK, canBlockAimForOldOrGraver, 3f, StartActionSeek, true, 6, 1.2f);
                    CurMODE.StartBehavior();
                }
                break;
            case EnemyTaskEneum.ReachedEndOfKnodes:
                if (CurMODE.GET_MyModeEnum() == ModesEnum.KSEEK)
                {
                    CurMODE.EndBehavior();
#if ENABLE_DEBUGLOG
                    Debug.Log("ovr->TARGETPLAYER");
#endif

                    CurMODE = new EBD_Seek(this, ModesEnum.TARGETPLAYER, canBlockAimForOldOrGraver, 3f, StartActionTargetPlayer, true, -1, 2.55f);
                    CurMODE.StartBehavior();
                }
                break;
            case EnemyTaskEneum.ReachedPlayer:

                if (CurMODE.GET_MyModeEnum() == ModesEnum.TARGETPLAYER)
                {
                    CurMODE.EndBehavior();
#if ENABLE_DEBUGLOG
                    Debug.Log("ovr ->ATTACKPlayer");
#endif

                    CurMODE = new EBD_AttackPlayer(this, ModesEnum.ATTACK, 3f, StartAction3Reaches, RepeatAttackAction, true, 0.5f);
                    // CurMODE = new EBD_REACHPLAYERGUN(this, ModesEnum.ATTACK, 3f, StartAction1ReachingForGun, WhatToDoWhenGrabbedGun, true);
                    CurMODE.StartBehavior();
                }
                break;
            case EnemyTaskEneum.SWING:

                if (CurMODE.GET_MyModeEnum() == ModesEnum.ATTACK)
                {
                    CurMODE.CompleteAnyAnim(0);
#if ENABLE_DEBUGLOG
                    Debug.Log("ovr ->ATTACKPlayer AGAIN");
#endif
                }

                break;
            case EnemyTaskEneum.KillmeBeforeILayEggs:
                if (CurMODE.GET_MyModeEnum() != ModesEnum.DEAD)
                {
                    CurMODE.EndBehavior();
#if ENABLE_DEBUGLOG
                    Debug.Log("ovr ->Dead ?? do i even c this");
#endif
                    CurMODE = new EBD_Death(this, ModesEnum.DEAD, 3f, StartActionDeath);
                    CurMODE.StartBehavior();
                }
                else
                {
#if ENABLE_DEBUGLOG
                    Debug.Log("ovr already dead");
#endif
                }
                break;
            default:
                break;
        }

    }


    #region ACTIONS
    public void StartActionIdle()
    {

        if (myAnimatorObj == null) { Debug.Log("no anim idle"); }
        myAnimatorObj.iSet_EBEstate((int)EBSTATE.START_eb0); myAnimatorObj.iSet_IsStartUnderground(false);
        if (GetMyType() == ARZombieypes.AXEMAN)
            return;

        Get_Animer().Do_LookPlayer(true);
    }

    public void StartActionEmerge()
    {

        //Debug.Log("start action graving");
        if (myAnimatorObj == null)
        {
            Debug.Log("no anim grave");
        }
        myAnimatorObj.Do_GRAVEEMERGE_Anim();
        //myAnimatorObj.iSet_EBEstate((int)EBSTATE.START_eb0); myAnimatorObj.iSet_IsStartUnderground(true);
    }

    public void StartActionSeek()
    {
#if ENABLE_DEBUGLOG
        //  Debug.LogError("start action seek");
#endif
        // Get_Animer().Do_LookPlayer(true);
        Get_Animer().iSet_Animator_CanReactBool(true);
        KNode tempfirstKnode = Get_CurAndOnlyKnode();
        KNode _TargetKNode = KnodeProvider.Instance.RequestNextKnode(tempfirstKnode);
        Set_CurAndOnlyKnode(_TargetKNode);//set curand only knode so that we can get it back .. When behavior makes new behavior passing its own ref to imove , the new behavior can only use the ref as long as the parent is alive ... beh should not be allowed to creat new behs 
        myAnimatorObj.Do_WalkRunSprintAcordingTocuragro();
    }

    //is like seek but walk to knode playerpos
    public void StartActionTargetPlayer()
    {
#if ENABLE_DEBUGLOG
        Debug.Log("start action target player");
#endif
        Set_CurAndOnlyKnode(KnodeProvider.Instance.GEt_PlayerAlphaBravo());
        KNode tempfirstKnode = Get_CurAndOnlyKnode();
        myAnimatorObj.Do_WalkRunSprintAcordingTocuragro();
    }

    int xreachnum = 1;
    public void StartAction3Reaches()
    {
#if ENABLE_DEBUGLOG
        Debug.Log("start action reach " + xreachnum);
#endif

        Set_CurAndOnlyKnode(KnodeProvider.Instance.GEt_PlayerAlphaBravo());//set curand only knode so that we can get it back .. When behavior makes new behavior passing its own ref to imove , the new behavior can only use the ref as long as the parent is alive ... beh should not be allowed to creat new behs 
        myAnimatorObj.iSet_Animator_BlockingBool(false);

        //myAnimatorObj.iSet_SWING_EnumVal(xreachnum);
        myAnimatorObj.iSet_EBEstate((int)EBSTATE.REACHING_eb3);
        myAnimatorObj.iTrigger_TrigEBstateTransition();
        myAnimatorObj.DoSwingAnim((TriggersDamageEffects.BearClawLR));

    }
    void StartAction1ReachingForGun()
    {

        Set_CurAndOnlyKnode(KnodeProvider.Instance.GEt_PlayerAlphaBravo());//set curand only knode so that we can get it back .. When behavior makes new behavior passing its own ref to imove , the new behavior can only use the ref as long as the parent is alive ... beh should not be allowed to creat new behs 


        myAnimatorObj.iSet_EBEstate((int)EBSTATE.REACHING_eb3);
        myAnimatorObj.iTrigger_TrigEBstateTransition();
        myAnimatorObj.DoSwingAnim(0);
        myAnimatorObj.StartReachPlayerGunIk();


    }

    int[] validattacks = new int[5] { 1, 2, 3, 5, 6 };
    public void RepeatAttackAction()
    {
        //
        //if (xreachnum > 6) xreachnum = 1;
#if ENABLE_DEBUGLOG
#endif
        Debug.Log("reboooom " + xreachnum + " " + ((TriggersDamageEffects)validattacks[xreachnum % validattacks.Length]).ToString());

        // myAnimatorObj.DoSwingAnim((TriggersDamageEffects)validattacks[xreachnum % validattacks.Length]);


        xreachnum++;

        if (xreachnum > 3)
        {
            xreachnum = 1;
        }

        if (xreachnum == 1) { myAnimatorObj.DoSwingAnim(TriggersDamageEffects.BearClawLR); }
        else
            if (xreachnum == 2) { myAnimatorObj.DoSwingAnim(TriggersDamageEffects.BearClawRL); }
        else
            if (xreachnum == 3) { myAnimatorObj.DoSwingAnim(TriggersDamageEffects.ScratchUpdown); }
        else
        { myAnimatorObj.DoSwingAnim(TriggersDamageEffects.BearClawRL); }


    }

    void WhatToDoWhenGrabbedGun() { }




    void IsSlowTimeON() { isSlowTimNow = true; }
    void IsSlowTimeOff() { isSlowTimNow = false; }

    bool isSlowTimNow;
    public void StartActionDeath()
    {
        // Get_Animer().Do_LookPlayer(false);
        GEt_MEsher().MeshDisolveToNothing();
        //Debug.Log("StartActionDeath");
        //if (GetMyType() == ARZombieypes.GRAVESKELETON)
        //{
        //    GameEventsManager.Instance.CAll_WaveGRAVERDied();
        //}
        if (isSlowTimNow)
        {
            Get_Animer().SetupSlowTimeDeath(true);
        }
        else
        {
            Get_Animer().SetupSlowTimeDeath(false);
        }
        Get_Animer().Do_LookPlayer(false);



        myAnimatorObj.iSet_EBEstate((int)EBSTATE.EBDEAD_eb4);
        myAnimatorObj.iTrigger_TrigEBstateTransition();
        //  audioMnger.StopAll();

        if (IsAxemanPreyOrBoss())
        {

            DooRagDelay(0f);
        }
        else
        {
            DooRagDelay(0.0f);
#if ENABLE_DEBUGLOG
            Debug.Log("i am Not ax or grave or boss  ");
#endif
        }
    }


    void DooRagDelay(float argDelay)
    {

        RaggEntityWithDelay(false, false, true, argDelay, false);


    }

    void DooDIeAnimation()
    {

        //  RaggEntityWithDelay(false, true, true, argDelay, false);


    }

    // void StartActionWait() { myAnimatorObj.iSet_EBEstate((int)EBSTATE.START_eb0); myAnimatorObj.iSet_IsStartUnderground(false); }
    #endregion


    //   public void RaggPreyEntity()
    //{
    //    m_cc.enabled = false;
    //    m_IraggComp.ToggleAllKinematics(false, true, false);
    //    Get_Animer().iToggleAnimatorOffWithDelay(false, 0.2f);
    //}
    //used in EB_GrabHurl
    public void RaggEntityWithDelay(bool argEnableCC, bool argUsehighendrag, bool argEnableColliders, float argDelay, bool argAntistretch)
    {
        StartCoroutine(JustWaitAbitTotirnoffAnimator(argEnableCC, argUsehighendrag, argEnableColliders, argDelay, argAntistretch));
        Get_Animer().iToggleAnimatorOffWithDelay(false, 0.2f);

    }
    IEnumerator JustWaitAbitTotirnoffAnimator(bool argEnableCC, bool argUsehighendrag, bool argEnableColliders, float argDelay, bool argAntistretch)
    {
        yield return new WaitForSecondsRealtime(argDelay);
        m_IraggComp.ToggleAllKinematics(argEnableCC);//,true,false);
        m_IraggComp.EnablePreprocess(argUsehighendrag);
        m_IraggComp.EnableColliders(argEnableColliders);
        m_cc.enabled = argEnableCC;
        m_IraggComp.EnableAntiStratch(argAntistretch);

    }



    public void PLAY_AUDIOBANK(string argUNDERSCOREDEventName)
    {
        MyAudioManager.PlayBodyEffects();
    }

    //only use while seeking. ebd_seek will call this on rand intervals
    public void Play_AUDIO_by_SeekSatate()
    {
        if (Get_Animer().iGet_SeekSpedNum() == 0)
        {
            PLAY_AUDIOBANK(EnemyAudioEvents._chaseMoan0.ToString());
        }
        else if (Get_Animer().iGet_SeekSpedNum() == 1)
        {
            PLAY_AUDIOBANK(EnemyAudioEvents._chaseMoan0.ToString());
        }
        else if (Get_Animer().iGet_SeekSpedNum() == 2)
        {
            PLAY_AUDIOBANK(EnemyAudioEvents._chaseMoan2.ToString());
        }
        else if (Get_Animer().iGet_SeekSpedNum() == 3) //must be played at 10 sec intervls
        {
            PLAY_AUDIOBANK(EnemyAudioEvents._chaseMoan3.ToString());
        }

    }

    public void ChangeAgroLevel(AgroUpDown argUpDown)
    {
        throw new NotImplementedException();
    }

    public void SPEEDUPENEMY_LIVE()
    {
        //   m_IMoverComp.ChangeBAseLeveAgro(argUpDown);
        // Debug.Log("IMPLEMENTME");

        //if (Get_Animer() != null)
        //{
        //    Get_Animer().SpeedMeUpLive();
        //}
        //else
        //{
        //    Debug.Log("lol no animer");
        //}
        if (CurMODE != null)
        {
            if (CurMODE.GET_MyModeEnum() != ModesEnum.DEAD)
            {
                if (CurMODE.GET_MyModeEnum() == ModesEnum.KSEEK || CurMODE.GET_MyModeEnum() == ModesEnum.SEEKTARGET)
                {
                    CurMODE.CompleteAnyAnim(999);
                }
            }
        }


    }


    #region AIMING
    bool canBlockAimForOldOrGraver;
    public virtual bool IcanBlockAim()
    {
        return canBlockAimForOldOrGraver;
    }



    bool _ISAimedAt;
    //bool _IsBlocking;
    bool _IsInvincible;
    //public void SetEnemyIsBlocking(bool argONOFf)
    //{
    //    _ISAimedAt = argONOFf;

    //}



    public void SetMainEnemyIsAmedAt(bool argOnOff)
    {

        //edb ,having gotten the togle signal from LAzor event, will set this. 
        _ISAimedAt = argOnOff;
        //now main entity decides what to do with this info
        if (Get_Animer().iGet_AllowBlocking())
        {
            Get_Animer().iSet_Animator_BlockingBool(_ISAimedAt);
        }
        else
        {
            Get_Animer().iSet_Animator_BlockingBool(false);
        }
    }

    public bool GetMainEnemyIsAimedAt()
    {
        return _ISAimedAt;
    }

    public void SetMainEnemyIsInvincible(bool argOnOff)
    {
        _IsInvincible = argOnOff;
    }

    public bool GetMainEnemyISInvisible()
    {
        return _IsInvincible;
    }

    #endregion
}
