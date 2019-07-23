////#define  ENABLE_DEBUGLOG
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EB_StartGraveNoDeath : IEnemyBehaviorObj, IDisposable
{
    IEnemyEntityComp m_ieec;
    IEnemyMoverComp m_Imover;
    ModesEnum _modeenum;
 
    public EB_StartGraveNoDeath(IEnemyEntityComp argIentity)
    {
        _modeenum = ModesEnum.STARTGRAVENODEATH;
#if ENABLE_DEBUGLOG
        Debug.Log("Construct" + _modeenum.ToString());
#endif
        m_ieec = argIentity;
        m_Imover = argIentity.Get_Mover();
        StartBehavior();
    }

    ~EB_StartGraveNoDeath()
    {
#if ENABLE_DEBUGLOG
        Debug.Log("Destructor" + _modeenum.ToString());
#endif
        Dispose();
    }

    public void Dispose()
    {
#if ENABLE_KEYBORADINPUTS
        Debug.Log("disposing STartGRAVEnoDeath");
#endif
        GC.SuppressFinalize(this);
    }


#region Interface

    public void StartBehavior()
    {
        // m_Imover.DoFirstAnim();//should be graveanim , but make sure the ardData enemy de has the zombieType properly set , else just check for the first animation
        Debug.Log("IMPLEMENTME");
    }

    public void RunBehavior()
    {

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