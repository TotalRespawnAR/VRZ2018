////#define  ENABLE_DEBUGLOG
using System.Collections;
using UnityEngine;

public class EnemyAnimatorV6Component : MonoBehaviour, IEnemyAnimatorComp
{
    IEnemyEntityComp m_ieec;
    EBSTATE cur_ebstate;
    SeekSpeed cur_seekspeed;
    int cur_weaknessLevel;  //for walking injured and triggeranim headshot gutshot goes to top or bot layer
    float cur_Multiplyer;
    int cur_deathType;    //works with trigEBstateTransition and EBSTATE =4
    int cur_deathDir;     //works with trigEBstateTransition and EBSTATE =4
    int CombatMoveType; //works wth trigcombatmove
    bool isCrouching;

    bool startUnderground;
    bool amCrawlingnow;
    int cur_ReachSwingType;
    Animator m_animator;
    AnimatorClipInfo[] m_AnimatorClipInfo;
    public string CurClipName()
    {


        //Get the animator clip information from the Animator Controller
        m_AnimatorClipInfo = m_animator.GetCurrentAnimatorClipInfo(0);
        //Output the name of the starting clip
        // Debug.Log("Starting clip : " + m_AnimatorClipInfo[0].clip);
        return m_AnimatorClipInfo[0].clip.name;
    }
    private void InitAnimatorProps()
    {
        cur_ebstate = EBSTATE.START_eb0;
        cur_seekspeed = SeekSpeed.walk;
        cur_weaknessLevel = 0;  //for walking injured and triggeranim headshot gutshot goes to top or bot layer
        cur_Multiplyer = 1f;
        cur_deathType = 0;    //works with trigEBstateTransition and EBSTATE =4
        cur_deathDir = 0;     //works with trigEBstateTransition and EBSTATE =4
        CombatMoveType = 0; // 0 is combat idle works wth trigcombatmove
        isCrouching = false;
        _isBlocking = false;
        startUnderground = false;
        amCrawlingnow = false;
        m_animator = GetComponent<Animator>();
        cur_ReachSwingType = 1;
    }

    private void Awake()
    {
        InitAnimatorProps();
    }


    public void iSet_EBEstate(int argebstate)
    {
        m_animator.SetInteger(EBSTATE_PropertyNames.IntEBSTATE.ToString(), argebstate);
    }

    public void iGet_EBstate()
    {
        Debug.Log("implementme");
    }

    public void iSet_SeekSpeedNum(int seeknum)
    {
        cur_seekspeed = (SeekSpeed)seeknum;
        m_animator.SetInteger(EBSTATE_PropertyNames.IntSEEKSPEED.ToString(), seeknum);
    }

    public int iGet_SeekSpedNum()
    {
        return m_animator.GetInteger(EBSTATE_PropertyNames.IntSEEKSPEED.ToString());
    }

    public void iTrigger_trigReact()
    {
        m_animator.ResetTrigger(EBSTATE_PropertyNames.TrigReact.ToString());
        m_animator.SetTrigger(EBSTATE_PropertyNames.TrigReact.ToString());
    }


    public void SetupSlowTimeDeath(bool argonoff)
    {
        if (argonoff)
        {
            m_animator.SetInteger(EBSTATE_PropertyNames.IntDeathType.ToString(), 1);
        }
        else
        {
            m_animator.SetInteger(EBSTATE_PropertyNames.IntDeathType.ToString(), 0);
        }
    }

    public void iTrigger_TrigEBstateTransition()
    {

        m_animator.ResetTrigger(EBSTATE_PropertyNames.TrigEBstateTransition.ToString());
        m_animator.SetTrigger(EBSTATE_PropertyNames.TrigEBstateTransition.ToString());
    }

    public void iSet_ReactEnumVal(int x)
    {
        m_animator.SetInteger(EBSTATE_PropertyNames.IntReactEnumVal.ToString(), x);
    }

    public void iGet_ReactEnumVal()
    {
        Debug.Log("implementme");
    }



    public void iSet_SWING_EnumVal(int x)
    {
        m_animator.SetInteger(EBSTATE_PropertyNames.IntSwingEnumVal.ToString(), x);
    }

    public void iGetSWINGEnumVal()
    {
        Debug.Log("implementme");
    }
    //    public void iTrigger_trigHeadShot()
    //    {
    //Debug.Log("implementme");
    //    }

    //    public void iTrigger_trigHitGut()
    //    {
    //Debug.Log("implementme");
    //    }

    public void iTrigger_trigPickUpP1()
    {
        m_animator.ResetTrigger(EBSTATE_PropertyNames.TrigPickUpP1.ToString());
        m_animator.SetTrigger(EBSTATE_PropertyNames.TrigPickUpP1.ToString());
    }

    public void iTrigger_trigPickUpP2()
    {
        m_animator.ResetTrigger(EBSTATE_PropertyNames.TrigPickUpP2.ToString());
        m_animator.SetTrigger(EBSTATE_PropertyNames.TrigPickUpP2.ToString());
    }

    public void iTrigger_trigHurling()
    {
        m_animator.ResetTrigger(EBSTATE_PropertyNames.TrigHurling.ToString());
        m_animator.SetTrigger(EBSTATE_PropertyNames.TrigHurling.ToString());
    }

    public void iTrigger_trigThrowAxe()
    {
        m_animator.ResetTrigger(EBSTATE_PropertyNames.TrigThrowAxe.ToString());
        m_animator.SetTrigger(EBSTATE_PropertyNames.TrigThrowAxe.ToString());
    }

    public void iTrigger_trigCombatMove()
    {
        m_animator.ResetTrigger(EBSTATE_PropertyNames.TrigCombatMove.ToString());
        m_animator.SetTrigger(EBSTATE_PropertyNames.TrigCombatMove.ToString());
    }

    public void iSet_IntCombatMoveType(int x)
    {
        m_animator.SetInteger(EBSTATE_PropertyNames.IntCombatMoveType.ToString(), x);
    }

    public void iTrigger_trigReachSwing()
    {
        m_animator.ResetTrigger(EBSTATE_PropertyNames.TrigReachSwing.ToString());
        m_animator.SetTrigger(EBSTATE_PropertyNames.TrigReachSwing.ToString());
    }


    //or any type of speed change while enemy is already moving
    public void iSet_Level_Seekspeed(SeekSpeed argLevelseekspeed)
    {

        cur_seekspeed = (SeekSpeed)argLevelseekspeed;
        iSet_SeekSpeedNum((int)cur_seekspeed);
    }

    public SeekSpeed iGetCurLevelSeekSpeed()
    {
        return cur_seekspeed;
    }



    public void iSet_IsCrouching(bool argSet_IsCrouching)
    {
        m_animator.SetBool(EBSTATE_PropertyNames.BoolCrouching.ToString(), argSet_IsCrouching);
    }




    public void iSet_IsMirror(bool argismir)
    {
        Debug.LogError("only for the single anim animator and overriders");
        m_animator.SetBool(EBSTATE_PropertyNames.BoolisMir.ToString(), argismir);
    }


    public void iSet_IsRootMotion(bool argSet_IsRootMotion)
    {
        m_animator.applyRootMotion = argSet_IsRootMotion;
    }
    public void iSet_IsStartUnderground(bool argSet_StartUnderground)
    {
        startUnderground = argSet_StartUnderground;
        m_animator.SetBool(EBSTATE_PropertyNames.BoolStartUnderground.ToString(), argSet_StartUnderground);
    }

    #region AIMING
    bool _isBlocking;
    public void iSet_Animator_BlockingBool(bool argTF)
    {
        if (m_animator != null) //causing issues when player dies and laz0r ctrl still haz a zombie in cash
        {
            _isBlocking = argTF;
            m_animator.SetBool(EBSTATE_PropertyNames.BoolIsBlocking.ToString(), argTF);
        }
    }
    public bool iGet_BlockingBool() { return _isBlocking; }

    bool _isAllowBlock;
    public void iSet_Animator_AllowBlocking(bool argTF)
    {
        _isAllowBlock = argTF;
        //if (!_isAllowBlock) { iSet_isBlocking(_isAllowBlock); /* false*/ }
        m_animator.SetBool(EBSTATE_PropertyNames.BoolAllowBlocking.ToString(), _isAllowBlock);
    }
    public bool iGet_AllowBlocking()
    {
        return _isAllowBlock;
    }
    bool _isCanReact;
    public void iSet_Animator_CanReactBool(bool argCanReact)
    {
        _isCanReact = argCanReact;
        //if (!_isCanReact)
        //{
        //    iSet_isBlocking(_isCanReact) /* false*/;
        //}
        m_animator.SetBool(EBSTATE_PropertyNames.BoolCanRect.ToString(), _isCanReact);
    }
    public bool iGet_CanReactBool()
    {
        return _isCanReact;
    }
    #endregion

    public void iSet_Animator_WeaknessInt(int weaknesslevel)
    {
        cur_weaknessLevel = weaknesslevel;
        m_animator.SetInteger(EBSTATE_PropertyNames.IntWeakness.ToString(), weaknesslevel);
    }

    public void iToggleAnimatorOffWithDelay(bool argIwantAnimatorOn, float delay)
    {
        if (argIwantAnimatorOn)
        {
            m_animator.enabled = argIwantAnimatorOn;
        }
        else
        {
            StartCoroutine(JustWaitAbitTotirnoffAnimator(delay));
        }
    }

    public void iSet_Animator_CrawlingBool(bool argcrawl)
    {
        iSet_Animator_BlockingBool(false);
        m_animator.SetBool(EBSTATE_PropertyNames.BoolCrawl.ToString(), argcrawl);
        amCrawlingnow = argcrawl;
    }
    public bool iGet_CrawlingBool()
    {
        return amCrawlingnow;
    }

    IEnumerator JustWaitAbitTotirnoffAnimator(float argdelay)
    {
        yield return new WaitForSecondsRealtime(argdelay);
#if ENABLE_DEBUGLOG
        Debug.Log("animer stopped");
#endif
        m_animator.enabled = false;
    }

    public void Do_IDLE_Anim()
    {
        iSet_IsStartUnderground(false);
        iSet_EBEstate((int)EBSTATE.START_eb0);

    }

    public void Do_GRAVEEMERGE_Anim()
    {
        iSet_IsStartUnderground(true);
        iSet_EBEstate((int)EBSTATE.START_eb0);
    }

    void Do_WALKInured_Anim()
    {
        m_ieec.Get_Mover().SetRotateSpeed(SeekSpeed.injuredwalk);
        iSet_SeekSpeedNum((int)SeekSpeed.injuredwalk);
        iSet_EBEstate((int)EBSTATE.SEEKING_eb1);
    }

    void Do_WALK_Anim()
    {
        m_ieec.Get_Mover().SetRotateSpeed(SeekSpeed.walk);
        iSet_SeekSpeedNum((int)SeekSpeed.walk);
        iSet_EBEstate((int)EBSTATE.SEEKING_eb1);
    }

    void Do_RUN_Anim()
    {
        m_ieec.Get_Mover().SetRotateSpeed(SeekSpeed.run);
        iSet_SeekSpeedNum((int)SeekSpeed.run);
        iSet_EBEstate((int)EBSTATE.SEEKING_eb1);
    }

    void Do_SPRINT_Anim()
    {
        m_ieec.Get_Mover().SetRotateSpeed(SeekSpeed.sprint);
        iSet_SeekSpeedNum((int)SeekSpeed.sprint);
        iSet_EBEstate((int)EBSTATE.SEEKING_eb1);
    }

    public void Do_BossWALK_Anim()
    {
        Debug.Log("BossWalkAnim");
        m_animator.SetInteger(EBSTATE_PropertyNames.IntWalkType.ToString(), -1);
        m_ieec.Get_Mover().SetRotateSpeed(SeekSpeed.walk);
        iSet_SeekSpeedNum((int)SeekSpeed.walk);
        iSet_EBEstate((int)EBSTATE.SEEKING_eb1);
        //iSet
    }


    public void Do_AxeRUN_Anim()
    {
        // Debug.Log("axmanrun");
        m_animator.SetInteger(EBSTATE_PropertyNames.IntWalkType.ToString(), -1);
        m_ieec.Get_Mover().SetRotateSpeed(SeekSpeed.sprint);
        iSet_SeekSpeedNum((int)SeekSpeed.sprint);
        iSet_EBEstate((int)EBSTATE.SEEKING_eb1);
        //iSet
    }
    public void Do_AxmanRoll_anim()
    {

    }

    public void Do_ThrowAxeaim()
    {
        iSet_Animator_CanReactBool(false);
        iTrigger_trigThrowAxe();
    }

    public void Do_WalkRunSprintAcordingTocuragro()
    {
        if (cur_seekspeed == SeekSpeed.walk)
        {
            Do_WALK_Anim();
        }
        else
        if (cur_seekspeed == SeekSpeed.run)
        {
            Do_RUN_Anim();
        }
        else
        if (cur_seekspeed == SeekSpeed.sprint)
        {
            Do_SPRINT_Anim();
        }
        if (cur_seekspeed == SeekSpeed.injuredwalk)
        {
            Do_WALKInured_Anim();
        }

    }


    public void Do_COMBATCROUCH_Anim(bool argcrouch)
    {
        iSet_IsCrouching(true);
        m_animator.ResetTrigger(EBSTATE_PropertyNames.TrigCombatMove.ToString());
        m_animator.SetTrigger(EBSTATE_PropertyNames.TrigCombatMove.ToString());
        iSet_EBEstate((int)EBSTATE.COMBAT_eb2);
    }

    public void Do_BLOCK_Anim(bool argBlock)
    {
        Debug.Log("implementme");
    }

    public void DO_COMBATPUNCH_Anim()
    {
        Debug.Log("implementme");
    }

    public void DO_COMBATwalkLEFT_Anim()
    {
        Debug.Log("implementme");
    }

    public void DO_COMBATwalkRIGHT_Anim()
    {
        Debug.Log("implementme");
    }

    public void DO_COMBATwalkFORWARD_Anim()
    {
        Debug.Log("implementme");
    }

    public void DO_COMBATwalkBACK_Anim()
    {
        Debug.Log("implementme");
    }

    public void DO_SpringRoll_Anim()
    {
        Debug.Log("implementme");
    }

    public void DO_HEADSHOT_Anim()
    {
        //  print("hdsotM");
        iSet_ReactEnumVal((int)ReactEnumVal.Headshot);
        iTrigger_trigReact();
    }

    public void Do_GUTSHOT_Anim()
    {
        // print("hutshot");
        iSet_ReactEnumVal((int)ReactEnumVal.Gutshot);
        iTrigger_trigReact();
    }

    public void DO_HEADSHOT_L_Anim()
    {
        // print("hdsotL");
        iSet_ReactEnumVal((int)ReactEnumVal.headknockLeft);
        iTrigger_trigReact();
    }

    public void DO_HEADSHOT_R_Anim()
    {
        //print("hdshotR");
        iSet_ReactEnumVal((int)ReactEnumVal.headknockRight);
        iTrigger_trigReact();
    }

    public void DO_GUTSHOT_L_Anim()
    {
        // print("hutshotL");
        iSet_ReactEnumVal((int)ReactEnumVal.shoulderknockLeft);
        iTrigger_trigReact();
    }

    public void DO_GUTSHOT_R_Anim()
    {
        //  print("hutshotR");
        iSet_ReactEnumVal((int)ReactEnumVal.shoulderknockRight);
        iTrigger_trigReact();
    }

    public void DO_POINTING_Anim()
    {
        Debug.Log("implementme");
    }

    public void DO_PICKUP1_ANIM()
    {
        iTrigger_trigPickUpP1();
    }

    public void DO_PICKUP2_Anim()
    {
        iTrigger_trigPickUpP2();
    }

    public void DoSwingAnim(TriggersDamageEffects argSwigType)
    {
        iSet_IsCrouching(false); //just in case
        iSet_SWING_EnumVal((int)argSwigType);
        iSet_EBEstate((int)EBSTATE.REACHING_eb3);
        iTrigger_trigReachSwing();


        if (argSwigType == TriggersDamageEffects.BearClawLR || argSwigType == TriggersDamageEffects.BearClawRL)
        {
            m_ieec.PLAY_AUDIOBANK(EnemyAudioEvents._reachGrunt2.ToString());
        }
        else
        if (argSwigType == TriggersDamageEffects.ScratchLR || argSwigType == TriggersDamageEffects.ScratchRL)
        {
            m_ieec.PLAY_AUDIOBANK(EnemyAudioEvents._reachGrunt3.ToString());
        }
        else
        if (argSwigType == TriggersDamageEffects.Scratch2XUpdpwn || argSwigType == TriggersDamageEffects.ScratchUpdown)
        {
            m_ieec.PLAY_AUDIOBANK(EnemyAudioEvents._reachGrunt4.ToString());
        }




    }



    float zero2one = 0.0f;


    public void TweenHandIk(float coef, AgroUpDown argUpDown)
    {
        if (argUpDown == AgroUpDown.Down)
        {
            float i = Time.deltaTime / coef;
            zero2one -= i;
            if (zero2one <= 0) { zero2one = 0; m_ieec.StopAllHandTargetting(); }

        }
        if (argUpDown == AgroUpDown.Up)
        {
            float i = Time.deltaTime / coef;
            zero2one += i;
        }
        if (zero2one >= 1) { zero2one = 1; m_ieec.StopAllHandTargetting(); }
    }

    public void Do_WalkToFAllToCrawl()
    {
        iSet_Animator_CrawlingBool(true);
        // iSet_SeekSpeedNum(-1);
    }
    public void DO_DESpeed()
    {
        if (cur_seekspeed == SeekSpeed.sprint)
        {
            if (!iGet_CrawlingBool())
            {
                iSet_Level_Seekspeed(SeekSpeed.run);
            }
        }

        else
        {
            Debug.Log("cannot deagro");
        }
    }

    void Start()
    {

        m_ieec = GetComponent<IEnemyEntityComp>();
        m_ieec.Set_mySpecialBonetrans(
            m_animator.GetBoneTransform(HumanBodyBones.Head)
            , m_animator.GetBoneTransform(HumanBodyBones.Chest)
            , m_animator.GetBoneTransform(HumanBodyBones.Hips)
            , m_animator.GetBoneTransform(HumanBodyBones.RightHand)
            , m_animator.GetBoneTransform(HumanBodyBones.LeftHand)
            );

        InitWalkType();


#if ENABLE_DEBUGLOG
        if (m_ieec == null) { Debug.Log("Start() animv6 NO IEBC!!!"); } else { Debug.Log("Start() animv6 found ibec"); }
#endif


    }

    void InitWalkType()
    {
        //only for single anim animator walk typp 0 is injured 1 is normal
        //m_animator.SetInteger(EBSTATE_PropertyNames.IntWalkType.ToString(), 1);
        if (m_ieec.GetMyType() != ARZombieypes.STD_BOSS)
        {
            int tempwalktype = 0;// m_ieec.Get_ID() % 15;
            m_animator.SetInteger(EBSTATE_PropertyNames.IntWalkType.ToString(), tempwalktype);
        }
        else
        {
            int tempwalktype = -1;
            m_animator.SetInteger(EBSTATE_PropertyNames.IntWalkType.ToString(), tempwalktype);
        }

    }
    bool _isLookPayerOn;
    public void Do_LookPlayer(bool OnOff)
    {
        if (OnOff)
        {
            //  Debug.Log("started Lokking");
        }
        else
        {

            // Debug.Log("ended Lokking");
        }
        _isLookPayerOn = OnOff;
    }
    bool _isREachPlayerGun;
    public void StartReachPlayerGunIk()
    {
        Debug.Log("startedReaching");
        _isREachPlayerGun = true;
    }

    private void OnAnimatorIK()
    {
        // if (m_ieec.GetMyType() == ARZombieypes.PREDATOR)
        if (_isLookPayerOn)
        {
           // Debug.Log(zero2one);
            m_animator.SetLookAtWeight(0.95f);
            m_animator.SetLookAtPosition(RzPlayerComponent.Instance.Get_PlayerHeadCamTrans().position);
        }
        else
        {
            m_animator.SetLookAtWeight(0.0f);
        }

        //if (_isREachPlayerGun)
        //{
        //    m_animator.SetLookAtWeight(0.8f);
        //    m_animator.SetLookAtPosition(RzPlayerComponent.Instance.GetPlayerHANDTarnsForGUnGrab().position);  //Camera.main.transform.position
        //                                                                                                       //m_animator.SetLookAtPosition(Camera.main.transform.position);  
        //    m_animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0.9f);
        //    m_animator.SetIKPosition(AvatarIKGoal.RightHand, m_ieec.Get_MyhandTArget().position);
        //}
        //else
        //{
        //    m_animator.SetLookAtWeight(0.0f);
        //}


        //if (_isLookGrabTargetOn)
        //{
        //    if (m_ieec.Get_AccessToMyPrey() != null)
        //    {
        //        m_animator.SetLookAtWeight(0.8f);
        //        m_animator.SetLookAtPosition(m_ieec.GetTargPos());
        //    }
        //}
        //else
        //{
        //    m_animator.SetLookAtWeight(0.0f);
        //}

        //if (m_ieec.Get_AccessToMyPrey() != null)
        //{
        //    m_animator.SetIKPositionWeight(AvatarIKGoal.RightHand, zero2one);
        //    m_animator.SetIKPosition(AvatarIKGoal.RightHand, m_ieec.GetTargPos());
        //}
    }


    //agro live zombvies
    public void SpeedMeUpLive()
    {

        if (cur_seekspeed == SeekSpeed.walk)
        {
            cur_seekspeed = SeekSpeed.run;
            Debug.Log("animto walk to run " + m_ieec.Get_ID());
        }
        else

         if (cur_seekspeed == SeekSpeed.run)
        {

            cur_seekspeed = SeekSpeed.sprint;
            Debug.Log("wave agro up to sprint");
        }

        Do_WalkRunSprintAcordingTocuragro();

    }



    /*
    #region Private vars
    public EBSTATE _levelBaseAnimState;
    public EBSTATE _firstAnimState;
    public EBSTATE _curEnmyAnimState;
    public EBSTATE _previousEnmyAnimState;
    public EBSTATE _cashed_PreRag_EnmyAnimState;
    public EBSTATE _cashed_PreFall_EnmyAnimState;
    public EBSTATE _cashed_PreSpecialEvent_EnmyAnimState;

    AgroLevel _baseAgroLevel;


    int _deathType_012 = 2;
    int _reachType_0ik_1swipe_2ovrhed_3botharms = 1;             
                                           //  int _chaseType_01234 = 0;           //0 1 2 3 4   //BOSS 5
    int _hyperchaseType_01234567 = 0;        //0 1 2
    int _idleType_012 = 0;
    int _fallDirection_DEFAULT = 3; //0 1 2
    int _fallDirection_0123 = 3;        //0forward 1 right 2 3
                                        //   int _walkType_012345 = 0;

    int _chaseType_01234 = 0;
    int _walkType_012345 = 0;

    int _CombatMoveType = 0;  //start with idelcombat 0-> idlecombat, 1->walkleft, 2->walkright, 3-> walkback 4-> walkforward 5-> attack1 6->attack2

    bool _isLookPayerOn;
    bool _isRightHandReachOn;
    bool _isLookGrabTargetOn;

    float BlendHyperC = 0.0f;
    #endregion

    #region MonoMethods
    void Awake()
    {


    }



    void OnEnable()
    {
        GameEventsManager.OnSlowTimeOn += SetSlowTimeDeathDirectionto4;
        GameEventsManager.OnSlowTimeOff += SetSlowTimeDeathDirectiontoDefault;
    }
    void OnDisable()
    {
        GameEventsManager.OnSlowTimeOn -= SetSlowTimeDeathDirectionto4;
        GameEventsManager.OnSlowTimeOff -= SetSlowTimeDeathDirectiontoDefault;

    }



    #endregion
    #region Interface

    public void Set_Level_Agro(AgroLevel arglevelagro)
    {
        _baseAgroLevel = arglevelagro;
    }


    public void Set_First_AnimatorState(EBSTATE argFirstAnimatorState)
    {
#if ENABLE_DEBUGLOG
        Debug.Log("First anim set!!!");
#endif
        _firstAnimState = argFirstAnimatorState;
        _curEnmyAnimState = _firstAnimState;
        _previousEnmyAnimState = _curEnmyAnimState;

    }
    public void Set_IsRootMotion(bool argapplyRootMotion)
    {
        m_animator = GetComponent<Animator>();
        if (m_animator == null)
        {
            m_animator = GetComponentInChildren<Animator>(); //axe guyy
        }

        m_animator.applyRootMotion = argapplyRootMotion;
    }
    public void set_WeaknessInt(int argWeaknesslevel) { m_animator.SetInteger("Weakness", argWeaknesslevel); }

    public void ToggleAnimatorOffWithDelay(bool argOnOff)
    {
        if (argOnOff)
        {
            m_animator.enabled = argOnOff;
        }
        else
        {
            StartCoroutine(JustWaitAbit());
        }
    }
    IEnumerator JustWaitAbit()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        m_animator.enabled = false;
    }

    public void Set_curAnimatorState(EBSTATE argAnimState)
    {
#if ENABLE_DEBUGLOG
        Debug.Log(" -> IANIM SetInteger." + argAnimState.ToString());
#endif

        _previousEnmyAnimState = _curEnmyAnimState;
        _curEnmyAnimState = argAnimState;

        //if (argAnimState == EnemyAnimatorState.REACHING)
        //{
        //    _reachType_012345678 = Random.Range(0, 5);
        //    m_animator.SetInteger("reachType", _reachType_012345678);
        //}

        m_animator.SetFloat("BlendHyperC", BlendHyperC);
        m_animator.SetInteger("state", (int)argAnimState);
    }

    public void Set_curAnimatorState(EBSTATE argAnimState, int argAnimTypebOSSwALKoNLYfORnOW)
    {
        _previousEnmyAnimState = _curEnmyAnimState;
        _curEnmyAnimState = argAnimState;


        if (argAnimState == EBSTATE.WALKING)
        {
            if (m_ieec.GetMyType() == ARZombieypes.STD_BOSS)
            {

                m_animator.SetInteger("walkType", argAnimTypebOSSwALKoNLYfORnOW);
                m_animator.SetInteger("state", (int)argAnimState);
                return;
            }
        }

        else
        if (argAnimState == EBSTATE.COMBATSTATE__31)
        {
            if (m_ieec.GetMyType() == ARZombieypes.STD_BOSS)
            {

                m_animator.SetInteger("CombatMoveType", argAnimTypebOSSwALKoNLYfORnOW);
                m_animator.SetInteger("state", (int)argAnimState);///YPPPPPPPPPPPPPPPp
               // yo this is why it shits i set combat state and state ... need to separate those in ianimcompo

                m_animator.SetTrigger(TriggersEnemyAnimator.trigCombatMove.ToString());
                return;
            }
        }

        m_animator.SetFloat("BlendHyperC", BlendHyperC);
        m_animator.SetInteger("state", (int)argAnimState);
    }


    public void CompletedEmergedAnimation()
    {
        m_ieec.Trigger_EndBEhaviorTASK(EnemyTaskEneum.EndFirstAnimation);
    }

    public void CompletedFallLandAnimation()
    {
        m_ieec.Trigger_EndBEhaviorTASK(EnemyTaskEneum.EndFirstAnimation);
    }

    public void CompletedEatToIdleAnimation()
    {
        m_ieec.Trigger_EndBEhaviorTASK(EnemyTaskEneum.EndFirstAnimation);
    }


    public void CompletedReachLoop()
    {
        m_ieec.CompletedCombatAction(0);
    }

    public void CompletedApexSwingAttack()
    {
      //  m_ieec.DamagePlayer();  //do the ui and dammage at apex of swig 
        // 
        if (_reachType_0ik_1swipe_2ovrhed_3botharms == 0) {
            //Do this once... well that animation has no event on it , so it never reaches endof loop , and the EB is never told to reset attack timer lol

        }
        else if (_reachType_0ik_1swipe_2ovrhed_3botharms == 1)
        {
            RzPlayerComponent.Instance.DamageHumanPlayer( HitType.ZombieScratchLR, m_ieec.Get_Strength());
        }
        else if (_reachType_0ik_1swipe_2ovrhed_3botharms == 2)
        {
            RzPlayerComponent.Instance.DamageHumanPlayer(HitType.ZombieScratchRL, m_ieec.Get_Strength());
        }
        else if (_reachType_0ik_1swipe_2ovrhed_3botharms == 3)
        {
            RzPlayerComponent.Instance.DamageHumanPlayer(HitType.ZobieScratch2handupdown, m_ieec.Get_Strength());
            //Do this once... well that animation has no event on it , so it never reaches endof loop , and the EB is never told to reset attack timer lol
        }
    }

    //just use CompletedLetgoThrow()  and Rocketlaunch event 
    //public void CompletedAxeThrowApexAnimation()
    //{
    //    m_ieec.Trigger_EndBehavior(EnemyTaskEneum.TriggerThrow);
    //}

    public void StartFirstAnimationState()
    {
#if ENABLE_DEBUGLOG
        Debug.Log("animating first anim");
#endif
        Set_curAnimatorState(_firstAnimState);
    }

    public void StartLEvelAnimState()
    {
#if ENABLE_DEBUGLOG
       Debug.Log("animating LEVEL anim");
#endif

        Set_curAnimatorState(EBSTATE.s);
    }


    public void ChangeAnimatorLeveAgro(AgroUpDown argUpDown)
    {
        if ((argUpDown == AgroUpDown.Up) && (_baseAgroLevel == AgroLevel.Low)) { _baseAgroLevel = AgroLevel.Med; }
        else
        if ((argUpDown == AgroUpDown.Up) && (_baseAgroLevel == AgroLevel.Med)) { _baseAgroLevel = AgroLevel.High; }
        else
        if ((argUpDown == AgroUpDown.Down) && (_baseAgroLevel == AgroLevel.High)) { _baseAgroLevel = AgroLevel.Med; }
        else
        if ((argUpDown == AgroUpDown.Down) && (_baseAgroLevel == AgroLevel.Med)) { _baseAgroLevel = AgroLevel.Low; }
        UPdateCuranimation();
    }

    public void Trigger_AnimState(TriggersEnemyAnimator argTrigname)
    {
#if ENABLE_DEBUGLOG
        Debug.Log("triggering " + argTrigname.ToString());
#endif
        m_animator.SetTrigger(argTrigname.ToString());
    }

    public void CompletedPartialPickupAnimation()
    {
        // 
        bool boolval = true;
#if ENABLE_DEBUGLOG
        Debug.Log(" Ianim.CompletedAnimP1 -> Ianim.ReachGrab( " + boolval.ToString() + ")");
#endif
        m_ieec.RunTaHandTargetting();
        // ReachForGrabTargetRightHand(boolval);
    }

    public void CompletedPickupAnimation()
    {
        Debug.Log("deletelog CompletedPickupAnimation");
        // bool boolval = false ; //if has not been set before
#if ENABLE_DEBUGLOG
        Debug.Log(" Ianim.CompletedAnimP1 -> Ianim.ReachGrab( " + boolval.ToString() + ")");
#endif
        //  ReachForGrabTargetRightHand(boolval);
        m_ieec.Trigger_EndBEhaviorTASK(EnemyTaskEneum.HandReachedGrabbTarget);
    }

    public void CompletedRaizPreyAnimation()
    {
        //m_ieec.Trigger_EndBehavior(EnemyTaskEneum.CompletedRaizedPrey);
        Trigger_AnimState(TriggersEnemyAnimator.trigHurling);
    }

    public void CompletedLetgoThrow()
    {
        //  Debug.Log("deletelog CompletedLetgoThrow");

        m_ieec.Trigger_EndBEhaviorTASK(EnemyTaskEneum.LaunchRocket);
    }


    //Animation Event handler
    public void CompleteCombatAnim(int combatanimtype) {

            Debug.Log("finished " + combatanimtype);
        m_ieec.CompletedCombatAction(combatanimtype);
    }



    public void ReachForGrabTargetRightHand(bool argOnOff)
    {

#if ENABLE_DEBUGLOG
#endif
        Debug.Log("-> Ianim.ReachGrab( " + argOnOff.ToString() + ")");
        //_isRightHandReachOn = argOnOff;
    }

    public void TweenClampHEadIk(float coef)
    {


    }

    public void TweenIkTOAnim(float coef) { }


    public AgroLevel GetCurAgroLevel() { return _baseAgroLevel; }

    public void BOOL_Animator(BoolsEnemyAnimator argBoolsanimator, bool argOnOff)
    {
        LastIsBlockingState = IsBlocking;
        if (LastIsBlockingState == argOnOff)
        {
            //samestate
        }
        else
        {
            IsBlocking = argOnOff;
            LastIsBlockingState = IsBlocking;
            m_animator.SetBool(argBoolsanimator.ToString(), IsBlocking);

            if(IsBlocking)  Set_HeadTargetLEft();
            else
            UnSet_HeadTarget();
        }
    }
    #endregion

    void DoPumpkinBossAnimatorPropsOverride()
    {
        _chaseType_01234 = 0;
        _walkType_012345 = 0;
        _hyperchaseType_01234567 = 4;
        _idleType_012 = 0;
        _reachType_0ik_1swipe_2ovrhed_3botharms = 1;
        _deathType_012 = Random.Range(0, 3);
        _fallDirection_DEFAULT = Random.Range(0, 4);
        _fallDirection_0123 = _fallDirection_DEFAULT;

    }

    void SetSlowTimeDeathDirectionto4() { _fallDirection_0123 = 4; m_ieec.SetSkipDeathAnimForRagAndFall_FORSLOWDEATH(true); m_animator.SetInteger("fallDirection", _fallDirection_0123); }
    void SetSlowTimeDeathDirectiontoDefault() { _fallDirection_0123 = _fallDirection_DEFAULT; m_ieec.SetSkipDeathAnimForRagAndFall_FORSLOWDEATH(false); m_animator.SetInteger("fallDirection", _fallDirection_0123); }

    void UPdateCuranimation()
    {
        //_baseAgroLevel   

        if ((_curEnmyAnimState == EBSTATE.WALKING) || (_curEnmyAnimState == EBSTATE.CHASING) || (_curEnmyAnimState == EBSTATE.HYPERCHASING))
        {
            if (_baseAgroLevel == AgroLevel.High) { Set_curAnimatorState(EBSTATE.HYPERCHASING); }
            else
            if (_baseAgroLevel == AgroLevel.Med) { Set_curAnimatorState(EBSTATE.CHASING); }
            else
            if (_baseAgroLevel == AgroLevel.Low) { Set_curAnimatorState(EBSTATE.WALKING); }
        }
    }

    float HEadIkweigth;
    float thevar;

    bool IsMoovingHeadTo0;
    bool IsMoovingHeadTo1;

    enum LookingState
    {
        Aimated,
        StaringAtPlayer,
        LooSide,
        ReachingOut
    }

    LookingState Curlooking;


    Vector3 LookV3Pos;
    public void Set_HeadTargetLEft()
    {
        _isLookPayerOn = true;
        HEadIkweigth = 0f;

    }

    public void UnSet_HeadTarget()
    {
        _isLookPayerOn = false;
        HEadIkweigth = 1f;
        LookV3Pos = new Vector3(0, 0, 0);
    }

    private void OnAnimatorIK(int layerIndex)
    {

        LookV3Pos = new Vector3(m_animator.GetBoneTransform(HumanBodyBones.Hips).position.x - 1f, m_animator.GetBoneTransform(HumanBodyBones.Hips).position.y + 1f, m_animator.GetBoneTransform(HumanBodyBones.Hips).position.z - 1f);

        m_animator.SetLookAtPosition(LookV3Pos);
        if (_isLookPayerOn)
        {

            thevar = Mathf.Clamp(HEadIkweigth += (Time.deltaTime * 0.5f), 0f, 0.8f);
            m_animator.SetLookAtWeight(thevar);


        }
        else
        {
            thevar = Mathf.Clamp(HEadIkweigth -= (Time.deltaTime * 0.5f), 0f, 0.8f);
            m_animator.SetLookAtWeight(thevar);


        }


    }



    private void OldOnAnimatorIK()
    {
        if (m_ieec.GetMyType() == ARZombieypes.PREDATOR)
            //   Debug.Log(zero2one);
            if (_isLookPayerOn)
            {
                m_animator.SetLookAtWeight(0.8f);
                m_animator.SetLookAtPosition(Camera.main.transform.position);
            }
            else
            {
                m_animator.SetLookAtWeight(0.0f);
            }

        if (_isLookGrabTargetOn)
        {
            if (m_ieec.Get_AccessToMyPrey() != null)
            {
                m_animator.SetLookAtWeight(0.8f);
                m_animator.SetLookAtPosition(m_ieec.GetTargPos());
            }
        }
        else
        {
            m_animator.SetLookAtWeight(0.0f);
        }

        if (m_ieec.Get_AccessToMyPrey() != null)
        {
            m_animator.SetIKPositionWeight(AvatarIKGoal.RightHand, zero2one);
            m_animator.SetIKPosition(AvatarIKGoal.RightHand, m_ieec.GetTargPos());
        }
    }

    bool IsBlocking = false;
    bool LastIsBlockingState = false;
    void Update()
    {

        //if (Input.GetKeyDown(KeyCode.Y)) { Set_HeadTargetLEft(); }
        //if (Input.GetKeyDown(KeyCode.U)) { UnSet_HeadTarget(); }
        //if (Input.GetKeyDown(KeyCode.Alpha0)) {
        //    Debug.Log("set EBSTATE 0");
        //    m_animator.SetInteger(EBSTATE_PropertyNames.EBSTATE.ToString(),(int) EBSTATE.EBSTART_0); }
        //if (Input.GetKeyDown(KeyCode.Alpha1)) {
        //    Debug.Log("set EBSTATE 1");
        //    m_animator.SetInteger(EBSTATE_PropertyNames.EBSTATE.ToString(), (int)EBSTATE.EBSEEK_1); }

        //if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    Debug.Log("set seeksped 0");
        //    m_animator.SetInteger(EBSTATE_PropertyNames.Weakness.ToString(), 0);
        //}

        //if (Input.GetKeyDown(KeyCode.Alpha3))
        //{
        //    Debug.Log("set seeksped 1");
        //    m_animator.SetInteger(EBSTATE_PropertyNames.Weakness.ToString(), 1);
        //}
        //EBSTATE_INTS


        //if (Input.GetKeyDown(KeyCode.Alpha5))
        //{
        //    Debug.Log("set hedshot 2");
        //    m_animator.SetInteger(EBSTATE_PropertyNames.TriggeredAnimType.ToString(), (int)TriggeredAnimTypes.headknock);
        //    m_animator.ResetTrigger(EBSTATE_PropertyNames.trigTriggeredAnim.ToString());
        //    m_animator.SetTrigger(EBSTATE_PropertyNames.trigTriggeredAnim.ToString());
        //}

        //if (Input.GetKeyDown(KeyCode.Alpha6))
        //{
        //    Debug.Log("set pushback 4");
        //    m_animator.SetInteger(EBSTATE_PropertyNames.TriggeredAnimType.ToString(), (int)TriggeredAnimTypes.headknock);

        //    m_animator.ResetTrigger(EBSTATE_PropertyNames.trigTriggeredAnim.ToString());
        //    m_animator.SetTrigger(EBSTATE_PropertyNames.trigTriggeredAnim.ToString());

        //}

        Debug.DrawLine(transform.position, LookV3Pos, Color.yellow);  
    }

    void Anim(string PropertyName, int propvalue, string AnimTrigger, int PreRequiredIntval) {
        m_animator.SetInteger(EBSTATE_PropertyNames.TriggeredAnimType.ToString(), (int)TriggeredAnimTypes.headknock);
    }

    public EBSTATE Get_curANimatorState()
    {
Debug.Log("implementme");
    }

    public void Set_trigReactionType(int x)
    {
        m_animator.SetInteger("TriggeredAnimTypes", x);
    }
    */

}
