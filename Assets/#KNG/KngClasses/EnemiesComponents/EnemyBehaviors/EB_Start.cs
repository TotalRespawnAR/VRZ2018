#define  ENABLE_DEBUGLOG
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EB_Start : IEnemyBehaviorObj, IDisposable
{
    IEnemyEntityComp m_ieec;
    IEnemyMoverComp m_imover;
    ModesEnum _modeenum;
    float MaxTimePAss = 1f;
    float CurTime = 0f;
    public EB_Start(IEnemyEntityComp argIbhc )
    {
        _modeenum = ModesEnum.STARTIDLE;
#if ENABLE_DEBUGLOG
        Debug.Log("Construct" + _modeenum.ToString());
#endif
        m_ieec = argIbhc;
        m_imover = argIbhc.Get_Mover();
        StartBehavior();
        GameEventsManager.OnShooterFired += HEardIt;
    }
    public EB_Start(IEnemyEntityComp argIbhc, float argEndTime)
    {
        _modeenum = ModesEnum.STARTIDLE;
#if ENABLE_DEBUGLOG
        Debug.Log("Construct" + _modeenum.ToString());
#endif
        MaxTimePAss = argEndTime;
        m_ieec = argIbhc;
        m_imover = argIbhc.Get_Mover();
        StartBehavior();
        GameEventsManager.OnShooterFired += HEardIt;
    }
    ~EB_Start()
    {
#if ENABLE_DEBUGLOG
        Debug.Log("Destructor" + _modeenum.ToString());
#endif
        Dispose();
    }

    void HEardIt() {
        //Debug.Log("i hearda bang bang");
    }
    public void Dispose()
    {
#if ENABLE_DEBUGLOG
        Debug.Log("disposing START");
#endif
        GC.SuppressFinalize(this);
    }




    #region Interface

    public void StartBehavior()
    {
#if ENABLE_DEBUGLOG
        Debug.Log("herroo");
#endif
    }

    public void RunBehavior()
    {
        CurTime += Time.deltaTime;
        if (CurTime > MaxTimePAss) {
            m_ieec.Trigger_EndBEhaviorTASK(EnemyTaskEneum.EndFirstAnimation);
        }
    }

    public void EndBehavior()
    {
        GameEventsManager.OnShooterFired -= HEardIt;
#if ENABLE_DEBUGLOG
        Debug.Log("EB_start ws told to stop it ");
#endif
        Dispose();
    }

    public ModesEnum GET_MyModeEnum()
    {
        return _modeenum;
    }

    public void ReadBullet(Bullet abulletfromDamageBehComp)
    {
        //dont do anything with this
    }
    public void CompleteAnyAnim(int argCombatanim)
    {

    }
    #endregion
}
