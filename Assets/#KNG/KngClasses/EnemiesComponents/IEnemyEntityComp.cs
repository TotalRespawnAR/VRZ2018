using System;
using UnityEngine;

public interface IEnemyEntityComp
{
    event EventHandler<ArgsEnemyMode> OnModeUpdated;
    event EventHandler<ArgsBulletWoond> OnBulletWoundInflicted;
    int Get_ID();
    void Set_ID(int argId);

    IEnemyBehaviorObj Get_Cur_EB_Mode(); //for damage to send bullet to mode 
    void Set_EB_Mode(IEnemyBehaviorObj argModeObj);

    int Get_HP();
    void Set_HP(Bullet argBullet, int argPoints, int argnewHP);
    int Get_OriginalHP();
    int Get_Strength();
    void Set_Strengt(int argStren);

    void InitBehavior(Data_Enemy argData);

    void CompletedAnimationOrAction(string actionAnim);
    //this is it the one and ony listener for anim evnets
    void OnAnimationEventSTR(string str);
    void OnActionAnimationEndEvent();
    void CompletedCombatAction(int CombatActionMnumber);

    void Kill_CurrMODE();


    void Trigger_EndBEhaviorTASK(EnemyTaskEneum argTAskName);

    void SetTargPos(Vector3 argTargpos);
    Vector3 GetTargPos();
    Vector3 GetMyPos();

    KNode Get_CurAndOnlyKnode();
    void Set_CurAndOnlyKnode(KNode argNEKnode);

    void DamagePlayer(TriggersDamageEffects sev, int damagepoints);
    void ChangeAgroLevel(AgroUpDown argUpDown);

    void Set_mySpecialBonetrans(Transform argHead, Transform argChest, Transform argHips, Transform argRhand, Transform argLhand);

    void SPEEDUPENEMY_LIVE();
    Transform Get_MyHEADtrans();
    Transform Get_my_RHandtrans();
    Transform Get_my_LHandtrans();
    Transform Get_MyCHESTtrans();
    Transform GEt_MyHIpsTans();
    Transform GEt_MyTransform();

    void Set_MyHandTarget(Transform argextratarget);
    Transform Get_MyhandTArget();
    void Set_MyLookatTarget(Transform argextratarget);
    Transform Get_MyLookatTArget();

    //void RunDisolveToNothing();
    //void TriggerStartDisolveOUT();

    IEnemyMoverComp Get_Mover();
    IEnemyRagdolComp Get_Ragger();
    IEnemyEntityComp Get_AccessToMyPrey();
    IEnemyMeshComp GEt_MEsher();
    void Shutthefuckup();

    void Set_MyPrey(IEnemyEntityComp argThispoorGuy);

    void HandleDeath();
    bool IsAxemanPreyOrBoss();
    void SetSkipDeathAnimForRagAndFall_FORSLOWDEATH(bool argSkipRag);
    ARZombieypes GetMyType();
    // void RaggPreyEntity(); //means kill char contoller as well
    void RaggEntityWithDelay(bool argkillCC, bool argUsehighendrag, bool argturnoffCollidersaswell, float argDelay, bool argANtiStretchTf);
    void CreatePrimitivePreyMagnet();
    GameObject GetMyPrimitiveMagnet();
    void GivePtimitiveToPrey();
    void PredatorDoPrimitiveFollowHand();
    void RunTaHandTargetting();
    void RunHAndUNtargetting();
    void StopAllHandTargetting();
    void BuildRocket();
    void Run_Rocket();
    void Destroy_Prey_RocketObj_PrimitivMagnet();
    void DoSetMyAssOnFire();
    void KillYourselfandCeanitup();
    void StickFireOnMe();
    void PauseEnemy(bool argonoff);



    EnemyAnimatorV6Component Get_Animer();

    void PLAY_AUDIOBANK(AudioClipType argCliptype);

    void SetMainEnemyIsAmedAt(bool argOnOff);
    bool GetMainEnemyIsAimedAt();

    void SetMainEnemyIsInvincible(bool argOnOff);
    bool GetMainEnemyISInvisible();
}
