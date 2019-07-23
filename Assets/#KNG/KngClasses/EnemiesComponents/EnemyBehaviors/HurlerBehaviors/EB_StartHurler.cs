////#define  ENABLE_DEBUGLOG
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EB_StartHurler : IEnemyBehaviorObj, IDisposable
{
    IEnemyEntityComp m_ieec;
    IEnemyMoverComp m_imover;
    ModesEnum _modeenum;
    float MaxTimePAss = 3f;
    float CurTime = 0f;
    public EB_StartHurler(IEnemyEntityComp argIbhc)
    {
        _modeenum = ModesEnum.STARTIDLE;
#if ENABLE_DEBUGLOG
        Debug.Log("Construct" + _modeenum.ToString());
#endif
        m_ieec = argIbhc;
        m_imover = argIbhc.Get_Mover();
        StartBehavior();
        GameEventsManager.On_PreyIsReady += PreyIsReady;
    }
    ~EB_StartHurler()
    {
#if ENABLE_DEBUGLOG
        Debug.Log("Destructor" + _modeenum.ToString());
#endif
        Dispose();
    }

    void PreyIsReady(IEnemyEntityComp argIPreEntity) {

        //Debug.Log("must go to you! ");
        m_ieec.Set_MyPrey(argIPreEntity);
        m_ieec.SetTargPos(argIPreEntity.Get_MyHEADtrans().position);
       m_ieec.Trigger_EndBEhaviorTASK(EnemyTaskEneum.EndFirstAnimation);
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

    }

    public void EndBehavior()
    {
        GameEventsManager.On_PreyIsReady -= PreyIsReady;
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
