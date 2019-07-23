
using UnityEngine;

public interface IZdamage
{


    bool BeenHitOnce();

    void SetHP(int value);

    void SetHP(int value, int BossHeadShots);

    void TakeHit_fromZbehavior(Bullet bullet);

    void DoBurnMe();

    void Fake_TakeSelfHit_fromZbehavior();

    void Kill();
    //for cheating from zombie beh
    void Rgister_HeadShot(Bullet bullet);
}
