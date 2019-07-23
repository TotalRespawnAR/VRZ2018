////#define ENABLE_DEBUGLOG
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EB_SeekTargetHurler : IEnemyBehaviorObj, IDisposable
{
    #region Private vars
    IEnemyEntityComp m_ieec;
    IEnemyMoverComp m_iMover;
    ModesEnum _modeenum;
    bool _sentTriggerPickupPart1;
    #endregion

    #region Constructor Destructor
    public EB_SeekTargetHurler(IEnemyEntityComp argIbhc)
    {
        _modeenum = ModesEnum.SEEKTARGET;
#if ENABLE_DEBUGLOG
        Debug.Log("Constructor "+ _modeenum.ToString());
#endif
        m_ieec = argIbhc;
        m_iMover = argIbhc.Get_Mover();
 

        StartBehavior();

    }

    ~EB_SeekTargetHurler()
    {
#if ENABLE_DEBUGLOG
        Debug.Log("Destructor" + _modeenum.ToString());
#endif
        Dispose();
    }
    #endregion
    public void Dispose()
    {
#if ENABLE_DEBUGLOG
        Debug.Log("Dispose" + _modeenum.ToString());
#endif
        GC.SuppressFinalize(this);
    }
    #region Interface


    public void StartBehavior()
    {
        // m_iMover.DoLevelAnim();
        Debug.Log("IMPLEMENTME");
    }
    public void RunBehavior()
    {
        if (_sentTriggerPickupPart1) return;

        if (m_iMover.ReachDestinationOffset(2.65f))
        {

#if ENABLE_DEBUGLOG
// Debug.Log("Neww Skseek");
#endif
            if (!_sentTriggerPickupPart1) {
                //   m_iMover.setAnimTrig(TriggersEnemyAnimator.trigPickUpP1);
                Debug.Log("IMPLEMENTME");
                _sentTriggerPickupPart1 = true;
            }
        }
        else
        {
            m_iMover.RunRotateTOTarg();
        }
    }
    public void EndBehavior()
    {
        Dispose();
    }


    public ModesEnum GET_MyModeEnum()
    {
        return _modeenum;
    }

    public void ReadBullet(Bullet abulletfromDamageBehComp)
    {
 
    }

    #endregion
    public void CompleteAnyAnim(int argCombatanim)
    {

    }
    #region Private Methods

    #endregion
}
