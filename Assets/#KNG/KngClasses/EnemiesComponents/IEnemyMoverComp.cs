using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyMoverComp  {
    void InitAnimator(SeekSpeed argseekspeed, bool argRootMotion);
    void RunRotateTOTarg();
   // void Task_MoveUPDATEToPos(Vector3 argTargPos);
    // void setAnimType(EBSTATE argAnimType);
   // void setAnimType(EBSTATE argAnimType, int animint);
   // void setAnimTrig(TriggersEnemyAnimator argTrigType);
   // void DoFirstAnim();
   // void DoLevelAnim();
    bool ReachedDestination();
    bool ReachDestinationOffset(float argdistreach);
    bool ReachedZOffset();

    void SetRotateSpeed(SeekSpeed argseekspeed);
    //  void ChangeBAseLeveAgro(AgroUpDown argUpDown);

    //IEnemyGenericAnimatorComp Get_Animer();

 //   void SideStep(LeftMidRight argleftRight);
}
