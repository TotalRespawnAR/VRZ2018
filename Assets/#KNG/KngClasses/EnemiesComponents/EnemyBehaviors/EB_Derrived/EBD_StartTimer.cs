using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBD_StartTimer : EB_Base
{

    Action _startAction;
    IEnemyEntityComp m_ieec;
    public EBD_StartTimer(IEnemyEntityComp argIentity, ModesEnum argmodeenum, float argMAxtime, Action argStartAction, bool argdamangeon) : base(argIentity, argmodeenum, argMAxtime, argStartAction, argdamangeon)
    {
        _startAction = argStartAction;
        m_ieec = argIentity;
    }

     ~EBD_StartTimer()
    {
#if ENABLE_DEBUGLOG
        Debug.Log("ebd ~ first");
#endif
        base.Dispose();
    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override void ReadBullet(Bullet abulletfromDamageBehComp)
    {


       // Debug.Log("ebd_ReadingBullet ");
        base.ReadBullet(abulletfromDamageBehComp);
    }

    public override void RunBehavior()
    {
        if(m_ieec.GetMyType()!= ARZombieypes.GRAVESKELETON)
        base.RunTimer();

    }

    public override void StartBehavior()
    {
        //base.StartBehavior();
        if (_startAction != null)
            _startAction();
        else Debug.Log("  action null");
    }

    public override string ToString()
    {
        return GET_MyModeEnum().ToString(); // base.ToString();
    }

 
}
