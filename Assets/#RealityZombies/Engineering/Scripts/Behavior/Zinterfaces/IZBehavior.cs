// @Author Nabil Lamriben ©2018
using UnityEngine;

public interface IZBehavior  {

    void SetID(int argId);
    //zombiespawningmanager ultimately for REQZ place idle 
    void SetID_HP_TYPE(int argId, int argHitpoints, Transform _argAlphaBravo, bool isPat, ARZombieypes argZtype);

    //zSpawnManager
    void SetID_HP_TYPE(int argId, int argHitpoints, bool isPat, ARZombieypes argZtype);

    int GetZombieID();

    void Zbeh_PauseZombieAnimation();

    Transform GetZombieHeadTrans();

    void AgroMe();

    void SpeedMeUp();

    void IncreaseAgro();

    void FakeBullet_and_ZbehaviorHook();

    void HasLineOfSight(bool argCanSeeyou);

    void GrenageMe(Transform argGrenade);

    void JUST_LandedFromDROPPING();

    void SetHP(int value);

    void Raggme();

    void RaggmeANDkillMe();

    void RaggFORCEandkillme(Bullet argBulet);

    void Flip_Just_got_HeadShotted();

    bool Get_Did_alreadyGotHeadshotted();

    void ReceiveHeadBulletCheat(Bullet argBullet);

    void Melt();

    void DoBurnMe();

    void Call_ZombieDied();
    void Call_ZombieHpChanged(int argNewZhp);
}
