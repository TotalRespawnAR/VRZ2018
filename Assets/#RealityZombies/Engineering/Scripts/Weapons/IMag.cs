using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMag  {

      bool TryDecrementBulletCount(bool argslowtimeon);
    GameObject GetBulletFromMag();
    int GetBulletsCount_inMag();
    void Refill();
    void InitCanPlayCollisionSound();
    void DissableMAgRendered();
}
