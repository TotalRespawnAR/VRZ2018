////#define  ENABLE_DEBUGLOG
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EB_DeadPrey : IEnemyBehaviorObj, IDisposable
{
    IEnemyEntityComp m_ieec;
    IEnemyMoverComp m_imov;
    ModesEnum _modeenum;
    float MaxTimePAss = 3f;
    float CurTime = 0f;
    bool sentPreayReady;
    public EB_DeadPrey(IEnemyEntityComp argIbhc)
    {
        _modeenum = ModesEnum.DEAD;
#if ENABLE_DEBUGLOG
        Debug.Log("Construct" + _modeenum.ToString());
#endif
        m_ieec = argIbhc;
        m_imov = argIbhc.Get_Mover();
        StartBehavior();
    }

    ~EB_DeadPrey()
    {
#if ENABLE_DEBUGLOG
        Debug.Log("Destructor" + _modeenum.ToString() + "prey...");
#endif
        Dispose();
    }
    public void Dispose()
    {
#if ENABLE_DEBUGLOG

        Debug.Log("disposing DEADPRey");
#endif

        GC.SuppressFinalize(this);
    }


    #region Interface
    public void StartBehavior()
    {
#if ENABLE_DEBUGLOG

        Debug.Log("eb_deadprey startBeh -> Imoversetanim");
#endif

        //   m_imov.setAnimType(EBSTATE.EBDEAD_4);
        Debug.Log("IMPLEMENTME");
    }

    public void RunBehavior()
    {
        if (sentPreayReady) return;
        CurTime += Time.deltaTime;
        if (CurTime > MaxTimePAss)
        {
            if (!sentPreayReady) {
               
                GameEventsManager.Instance.CallPreyIsReadyForPickup(m_ieec);
                sentPreayReady = true;
            }
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
        //dont do anything with this
    }
    public void CompleteAnyAnim(int argCombatanim)
    {

    }
    #endregion
    //coroutine not possible here, and dont want to inherit mono on this object S
    //IEnumerator WaitThenSignalPreyReady(float argtime)
    //{
    //    yield return new WaitForSeconds(argtime);
    //    Debug.Log("prey ready");
    //    GameEventsManager.Instance.CallPreyIsReadyForPickup(this);
    //}

}
