
using UnityEngine;

public interface IZeffects
{

    //void CheckOffScreen();
    void SetHealthBar(HealthBarCtrl argHB);
    void SetHealthBar(GameObject argHBObj);
    void SetHEalthBarHealthPoints(int argInitHP, int BossHeadshots);
    void SetHEalthBarHealthPoints(int argInitHP);
    void UpdatedHp(int argNEwHP, bool argBeenHitOnce);
    void HideBar();
    void SetFire();
    bool IsOnFire();
    void Boold_On_Head(Bullet bullet);
    void Blood_On_HEadCheat(Transform argHEadTRans);
    void Boold_On_Torso(Bullet bullet);
    void Boold_On_Pelvis(Bullet bullet);
    void Boold_On_Limb(Bullet bullet);
     
}
