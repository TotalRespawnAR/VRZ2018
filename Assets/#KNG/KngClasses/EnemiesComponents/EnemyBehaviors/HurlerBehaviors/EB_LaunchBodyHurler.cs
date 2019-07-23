//#define ENABLE_DEBUGLOG
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EB_LaunchBodyHurler : IEnemyBehaviorObj, IDisposable
{
    #region Private vars
    IEnemyEntityComp m_ieec;
    IEnemyMoverComp m_iMover;
    ModesEnum _modeenum;
    Transform _PreyHead;
    Transform _preyChest;
    IEnemyRagdolComp m_Irag;
    IEnemyGenericAnimatorComp m_ianim;
    float MaxTimePAss = 4f;
    float CurTime = 0f;
    bool _sentmessage;
    #endregion

    #region Constructor Destructor
    public EB_LaunchBodyHurler(IEnemyEntityComp argIbhc)
    {
        _modeenum = ModesEnum.PROJECTILE;
#if ENABLE_DEBUGLOG
        Debug.Log("Constructor "+ _modeenum.ToString());
#endif
        m_ieec = argIbhc;
        m_iMover = argIbhc.Get_Mover();
       // m_ianim = m_iMover.Get_Animer();
        m_Irag = argIbhc.Get_Ragger();

        StartBehavior();

    }

    ~EB_LaunchBodyHurler()
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
        Debug.Log("Dispose()" + _modeenum.ToString());
#endif
        GC.SuppressFinalize(this);
    }
#region Interface


    public void StartBehavior()
    {
#if ENABLE_DEBUGLOG
#endif
        m_ieec.Get_AccessToMyPrey().Get_Ragger().PreyDestroyOwnActiveHeadHinge();
       // m_ieec.BuildRocket();
    }
    public void RunBehavior()
    {
        //m_ieec.Run_Rocket();
        if (_sentmessage) return;
        CurTime += Time.deltaTime;
        if (CurTime > MaxTimePAss)
        {
#if ENABLE_DEBUGLOG
#endif
            m_ieec.Destroy_Prey_RocketObj_PrimitivMagnet();
             m_ieec.Trigger_EndBEhaviorTASK(EnemyTaskEneum.HurlingDone);
            _sentmessage = true;
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
    public void CompleteAnyAnim(int argCombatanim)
    {

    }
    #endregion

    #region Private Methods

    #endregion
}
