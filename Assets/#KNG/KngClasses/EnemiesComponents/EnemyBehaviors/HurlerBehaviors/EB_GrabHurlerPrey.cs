////#define  ENABLE_DEBUGLOG
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EB_GrabHurlerPrey : IEnemyBehaviorObj, IDisposable
{
    IEnemyEntityComp m_ieec;
    IEnemyMoverComp m_imover;
    ModesEnum _modeenum;
    float MaxTimePAss = 3f;
    float CurTime = 0f;
    bool sentmessage;
    public EB_GrabHurlerPrey(IEnemyEntityComp argIbhc)
    {
        _modeenum = ModesEnum.GRABBB;
#if ENABLE_DEBUGLOG
        Debug.Log("Construct" + _modeenum.ToString());
#endif
        m_ieec = argIbhc;
        m_imover = argIbhc.Get_Mover();
        StartBehavior();
    }
    ~EB_GrabHurlerPrey()
    {
#if ENABLE_DEBUGLOG
        Debug.Log(" destruct" + _modeenum.ToString());
#endif
        Dispose();
    }


    public void Dispose()
    {
#if ENABLE_DEBUGLOG

        Debug.Log(" Dispose()" + _modeenum.ToString());
#endif

        GC.SuppressFinalize(this);
    }




    #region Interface

    public void StartBehavior()
    {
#if ENABLE_DEBUGLOG
        Debug.Log("trigpickup1" + _modeenum.ToString());
        Debug.Log("createprim" + _modeenum.ToString());
         
#endif


        m_ieec.CreatePrimitivePreyMagnet();
#if ENABLE_DEBUGLOG
         
        Debug.Log("givprimistoprey " + _modeenum.ToString());
#endif
        m_ieec.GivePtimitiveToPrey();
         
        m_ieec.Get_AccessToMyPrey().Get_Ragger().PreyAddHeadHinjoint();
        m_ieec.Get_AccessToMyPrey().Get_Ragger().PreyHOOKUP(m_ieec.GetMyPrimitiveMagnet().GetComponent<Rigidbody>());

       
    }

    public void RunBehavior()
    {
        if (sentmessage) return;
        m_ieec.Trigger_EndBEhaviorTASK(EnemyTaskEneum.GrabbedEnemy);
        // kinda weird , just use the cool raggentutycoordinator() that i ll just made  
        //m_ieec.Get_AccessToMyPrey().RaggPReyEntity(true,true,false);
        sentmessage = true;
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
}
