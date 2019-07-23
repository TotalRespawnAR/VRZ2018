// @Author Jeffrey M. Paquette ©2016


public interface IGun
{


    GunReloadState GetGunState();
    GunType GetGunType();
    // GunState GetGunState();
    void MANUAL_EJECT_MAG_OUT();

    // GunBools GUN_GET_BOOLS();


    void GUN_FIRE();
    // method to stop firing weapon (only implemented on automatic weapons)
    void GUN_STOP_FIRE();




    void CancelSound();
    void GUN_START_MAG_IN();

    void FullReplacementOfMag();
    //  void FullReplacementOfMag_NoSOund();

    void GunInjstantiateMagANDSLIDEINanim();
    void AUDIO_PopMagIn();
    //  void MagicReloadBulletCount();
    void DropMagFromGun();

    //void GUN_LINK_RELOAD_METER(GameObject go);

    // void PopAnActiveReload();

    MagazineMNGR GetGunMagMngr();



    int GetCurGunIndex();
    void ResetGunReloadState();




    void StartStopAutoShootShowcase(bool argstartstop);

    void UseScope(GunScopes argGunscope);
    void Temp_ScopeCycleForward();
    //no neeed for AUDIO_Fire sincs that gets triggered when a bullet is fired and a flash effect is instantiated 
    //void AUDIO_PopMagOut();
    //void AUDIO_PushMagIn();
    //void AUDIO_Chamber();
    //void AUDIO_Dry();
    //void AUDIO_FullReload();
}
