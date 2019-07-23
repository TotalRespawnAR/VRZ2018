//#define  ENABLE_DEBUGLOG
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EB_Dead : IEnemyBehaviorObj, IDisposable
{
    IEnemyEntityComp m_ieec;
    IEnemyRagdolComp m_irag;
    IEnemyMoverComp m_imov;
    ModesEnum _modeenum;
    float MaxTimePAss = 3f;
    float CurTime = 0f;
    bool _skipanim;
    bool Originsal_skipanim;
    public EB_Dead(IEnemyEntityComp argIbhc )
    {
        _modeenum = ModesEnum.DEAD;
#if ENABLE_DEBUGLOG
        Debug.Log("Construct" + _modeenum.ToString());
#endif
        m_ieec = argIbhc;
        m_imov = argIbhc.Get_Mover();
        m_irag = argIbhc.Get_Ragger();
        _skipanim = argIbhc.IsAxemanPreyOrBoss();
        StartBehavior();
    }

    ~EB_Dead()
    {
#if ENABLE_DEBUGLOG
        Debug.Log("Destructor" + _modeenum.ToString());
#endif
        Dispose();
    }
    public void Dispose()
    {
#if ENABLE_DEBUGLOG
        Debug.Log("disposing DEAD");
#endif
        GC.SuppressFinalize(this);
    }


    #region Interface
    public void StartBehavior()
    {
         TriggerStartDisolveOUT();
        if (!_skipanim)
        {
            //  m_imov.setAnimType(EBSTATE.EBDEAD_4);
            Debug.Log("IMPLEMENTME");
        }
        else
        {
            //  m_imov.setAnimType(EBSTATE.EBDEAD_4);
            Debug.Log("IMPLEMENTME");
           // m_irag.ToggleAllKinematics(false,false,false);//oldscool
            Debug.Log("ImplementME");
            //   m_imov.Get_Animer().ToggleAnimatorOffWithDelay(false);
        }
    }

    public void RunBehavior()
    {
        RunDisolveToNothing();
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

      void RunDisolveToNothing()
    {
       // m_ImeshComp.RunApplyDissovefactoactor();
    }
      void TriggerStartDisolveOUT() {
       // m_ImeshComp.MeshDisolveToNothing();
    }

    #endregion
}
