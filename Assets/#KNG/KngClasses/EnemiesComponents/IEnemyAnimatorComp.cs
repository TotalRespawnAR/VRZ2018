public interface IEnemyAnimatorComp
{



    void iSet_EBEstate(int argebstate);
    void iGet_EBstate();

    void iSet_SeekSpeedNum(int seeknum);
    int iGet_SeekSpedNum();

    void iTrigger_trigReact();
    void iSet_ReactEnumVal(int x);
    void iGet_ReactEnumVal();



    //void iTrigger_trigHeadShot();
    //void iTrigger_trigHitGut();
    void iTrigger_trigPickUpP1();
    void iTrigger_trigPickUpP2();
    void iTrigger_trigHurling();
    void iTrigger_trigThrowAxe();
    void iTrigger_trigCombatMove();
    void iSet_IntCombatMoveType(int x);


    void iTrigger_trigReachSwing();

    void iSet_Level_Seekspeed(SeekSpeed arglevelagro);
    SeekSpeed iGetCurLevelSeekSpeed();


    void iSet_IsRootMotion(bool argSet_IsRootMotion);
    void iSet_IsCrouching(bool argSet_IsCrouching);
    void iSet_IsStartUnderground(bool argSet_StartUnderground);
    void iSet_Animator_WeaknessInt(int weaknesslevel);
    void iToggleAnimatorOffWithDelay(bool argOnOff, float argdelay);

    void iSet_Animator_CrawlingBool(bool argcrawl);
    bool iGet_CrawlingBool();


    void iSet_Animator_AllowBlocking(bool argTF);
    bool iGet_AllowBlocking();

    void iSet_Animator_BlockingBool(bool argTF);
    bool iGet_BlockingBool();


    void iSet_Animator_CanReactBool(bool argCanReact);
    bool iGet_CanReactBool();

    //void Set_curAnimatorState(EBSTATE argAnimState);
    //void Set_curAnimatorState(EBSTATE argAnimState, int argAnimType);
    //EBSTATE Get_curANimatorState();
    //void CompletedEmergedAnimation();
    //void CompletedFallLandAnimation();
    //void CompletedEatToIdleAnimation();
    //void CompletedPartialPickupAnimation();
    //void CompletedPickupAnimation();
    //void CompletedRaizPreyAnimation();
    //void CompletedLetgoThrow();
    //void CompletedReachLoop();
    //void CompletedApexSwingAttack();
    //void CompleteCombatAnim(int combatanimtype);
    //void Set_HeadTargetLEft();
    //void UnSet_HeadTarget();
    //void ReachForGrabTargetRightHand(bool argOnOff);
    //void StartFirstAnimationState();
    //void StartLEvelAnimState();
    //void TweenClampHEadIk(float coef);
    //void TweenIkTOAnim(float coef);
    //void TweenHandIk(float coef, AgroUpDown argUpDown);

    //void ChangeAnimatorLeveAgro(AgroUpDown argUpDown);
    //void Trigger_AnimState(TriggersEnemyAnimator argTrigname);

    //void BOOL_Animator(BoolsEnemyAnimator animator, bool argOnOff);
}
