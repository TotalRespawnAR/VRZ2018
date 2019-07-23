////#define ENABLE_DEBUGLOG
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EB_PointHurler : IEnemyBehaviorObj, IDisposable
{
    #region Private vars
    IEnemyEntityComp m_ieec;
    IEnemyMoverComp m_iMover;
    ModesEnum _modeenum;
    KNode _CurKnode;
    float MaxTimePAss = 2f;
    float CurTime = 0f;
    bool _sentmessage;
    #endregion

    #region Constructor Destructor
    public EB_PointHurler(IEnemyEntityComp argIbhc)
    {
        _modeenum = ModesEnum.POINTFINGER;
#if ENABLE_DEBUGLOG
        Debug.Log("Constructor "+ _modeenum.ToString());
#endif
        m_ieec = argIbhc;
        m_iMover = argIbhc.Get_Mover();
        _CurKnode = argIbhc.Get_CurAndOnlyKnode();

        StartBehavior();

    }

    ~EB_PointHurler()
    {
#if ENABLE_DEBUGLOG
#endif
        Debug.Log("Destructor" + _modeenum.ToString());
        Dispose();
    }
    #endregion
    public void Dispose()
    {
        Debug.Log("disposing SeekKK");
        GC.SuppressFinalize(this);
    }
    #region Interface


    public void StartBehavior()
    {
        //  m_iMover.setAnimTrig(TriggersEnemyAnimator.trigPointing);
        Debug.Log("IMPLEMENTME");
        GameEventsManager.Instance.CallPredatorPointed();
    }
    public void RunBehavior()
    {

        if (_sentmessage) return;
        CurTime += Time.deltaTime;
        if (CurTime > MaxTimePAss)
        {
            m_ieec.Trigger_EndBEhaviorTASK(EnemyTaskEneum.Pointing);
            _sentmessage = true;
        }

  
          m_iMover.RunRotateTOTarg();
       
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
