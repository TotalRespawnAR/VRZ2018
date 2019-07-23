using System;
using UnityEngine;

public class EBD_AttackPlayer : EB_Base
{

    Action _startAction;
    Action _attackAction;
    IEnemyEntityComp m_ieec;
    KNode _TargetKNode;

    float CurTime;
    float _attackTimer = 3f;
    bool timercanrun = false; //on finished animm , reset this
    public EBD_AttackPlayer(IEnemyEntityComp argIentity, ModesEnum argmodeenum, float argMAxtime, Action argStartAction, Action argAttackAction, bool argDamageOn, float attackRepeattime) : base(argIentity, argmodeenum, argMAxtime, argStartAction, argDamageOn)
    {
        if (attackRepeattime < 0.5f)
        {
            attackRepeattime = 0.5f;
        }
        _attackTimer = attackRepeattime;
        _startAction = argStartAction;
        _attackAction = argAttackAction;
        m_ieec = argIentity;

        _TargetKNode = argIentity.Get_CurAndOnlyKnode();

    }

    ~EBD_AttackPlayer()
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
        if (timercanrun)
        {

            CurTime -= Time.deltaTime;
            if (CurTime <= 0)
            {
                CurTime = _attackTimer;
#if ENABLE_DEBUGLOG
                Debug.Log("time to scratch");
#endif
                _attackAction();



                timercanrun = false;
            }
        }

    }

    public override void StartBehavior()
    {
        CurTime = _attackTimer;
        m_ieec.Set_CurAndOnlyKnode(KnodeProvider.Instance.GEt_PlayerAlphaBravo());


        if (_startAction != null)
            _startAction();
        else Debug.Log("  action null");

    }

    public override string ToString()
    {
        return GET_MyModeEnum().ToString(); // base.ToString();
    }
}
