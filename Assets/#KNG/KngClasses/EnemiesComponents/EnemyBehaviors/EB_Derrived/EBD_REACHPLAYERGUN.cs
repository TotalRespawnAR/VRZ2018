
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//does seeking and walk to player 
public class EBD_REACHPLAYERGUN : EB_Base
{
    Action _startAction;
    Action _runningAction;
    IEnemyEntityComp m_ieec;
    KNode _TargetKNode;

    
    
    bool timercanrun = false; //on finished animm , reset this
    public EBD_REACHPLAYERGUN(IEnemyEntityComp argIentity, ModesEnum argmodeenum, float argMAxtime, Action argStartAction, Action argWhatToDoWhenGrabbedGun, bool argDamageOn ) : base(argIentity, argmodeenum, argMAxtime, argStartAction, argDamageOn)
    {
      
        _startAction = argStartAction;
        _runningAction = argWhatToDoWhenGrabbedGun;
         m_ieec = argIentity;

        _TargetKNode = argIentity.Get_CurAndOnlyKnode();

    }

    ~EBD_REACHPLAYERGUN()
    {
        base.Dispose();
    }
    public override void CompleteAnyAnim(int argCombatanim)
    {
        //base.CompleteAnyAnim(argCombatanim);
        timercanrun = true;
    }

    public override void EndBehavior()
    {
        base.EndBehavior();
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
        base.ReadBullet(abulletfromDamageBehComp);
    }

    public override void RunBehavior()
    {

        m_ieec.Get_Mover().RunRotateTOTarg();
     

    }

    public override void StartBehavior()
    {
        
        m_ieec.Set_MyHandTarget(RzPlayerComponent.Instance.GetPlayerHANDTarnsForGUnGrab());


        if (_startAction != null)
            _startAction();
        else Debug.Log("  action null");

    }

    public override string ToString()
    {
        return GET_MyModeEnum().ToString(); // base.ToString();
    }
}



