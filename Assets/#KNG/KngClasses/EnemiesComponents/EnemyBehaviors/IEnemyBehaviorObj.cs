using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyBehaviorObj
{
    ModesEnum GET_MyModeEnum();
    void StartBehavior();
    void RunBehavior();
    void EndBehavior();

    void ReadBullet(Bullet abulletfromDamageBehComp);
    void CompleteAnyAnim(int argCombatanim);
    
}
