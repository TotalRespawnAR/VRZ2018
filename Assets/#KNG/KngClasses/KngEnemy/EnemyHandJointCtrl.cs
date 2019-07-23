using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandJointCtrl : MonoBehaviour {
    public GameObject EnemyToGRamOBj;
     // IEnemyBehavior EnemyToGrab;
    public Transform EnemyHsnddHingTrans;
    public Rigidbody MyHandHookCUBE;
    public Rigidbody MyHandHook;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
           // EnemyToGRamOBj.GetComponent<IEnemyBehavior>().Set_STATE(EnemyAnimatorState.RAGGED);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
           // EnemyToGRamOBj.GetComponent<IEnemyRagDoll>().GrabMe(MyHandHookCUBE);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
          //  EnemyToGRamOBj.GetComponent<IEnemyBehavior>().ReleaseMe();
        }
       // MyHandHookCUBE.MovePosition(EnemyHsnddHingTrans.position);

    }
}
