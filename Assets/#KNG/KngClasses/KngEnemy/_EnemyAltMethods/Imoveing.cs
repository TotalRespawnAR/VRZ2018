using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Imoveing  {

    void MOVE();
    void MOVE(bool argallow);
    void Set_IEBEH(IEnemyBehavior _IEBEH, Transform argTrans);
}
