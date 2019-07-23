using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyDamageComp  {
    void TakeHit(Bullet bullet);
    void TakeHits(Bullet[] bullets);
}
