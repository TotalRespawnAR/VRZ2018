using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyRagdolComp  {

    void ToggleAllKinematics(bool argOnOff);
    void EnableColliders(bool argonoff);
    void EnablePreprocess(bool argonoff);
    void EnableAntiStratch(bool argtf);
    void PreyAddHeadHinjoint();
    void PreyDestroyOwnActiveHeadHinge();
    void PreyHOOKUP(Rigidbody rghook);

}
