//#define  ENABLE_DEBUGLOG
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyModeObj : IEnemyBehaviorObj, IDisposable
{

    IEnemyEntityComp m_ieec;
    IEnemyMoverComp m_Imover;
    ModesEnum _modeenum;

    public EnemyModeObj(IEnemyEntityComp argIentity, ModesEnum enumNameForthisobj)
    {
        _modeenum = enumNameForthisobj;

#if ENABLE_DEBUGLOG
        Debug.Log("Construct" + _modeenum.ToString());
#endif
        m_ieec = argIentity;
        m_Imover = argIentity.Get_Mover();
        StartBehavior();
    }

    ~EnemyModeObj()
    {
#if ENABLE_DEBUGLOG
        Debug.Log("Destructor" + _modeenum.ToString());
#endif
        Dispose();
    }



    public virtual void StartBehavior()
    {
        Debug.Log("pleaz override me ");
    }

    public void Dispose()
    {
#if ENABLE_DEBUGLOG
        Debug.Log("base.Dispose()"+ _modeenum);
#endif
        GC.SuppressFinalize(this);
    }

    public void EndBehavior()
    {
        Dispose();
    }

    public ModesEnum GET_MyModeEnum()
    {
        return _modeenum;
    }

    public virtual void ReadBullet(Bullet abulletfromDamageBehComp)
    {
        Debug.Log("pleaz override me ");
    }
    public void CompleteAnyAnim(int argCombatanim)
    {

    }
    public virtual void RunBehavior()
    {
       Debug.Log("pleaz override me ");
    }

}
