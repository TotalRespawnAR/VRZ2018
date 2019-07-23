using System;
using UnityEngine;
//does seeking and walk to player 
public class EBD_Seek : EB_Base
{
    Action _startAction;
    IEnemyEntityComp m_ieec;
    int _lastRow;
    KNode _TargetKNode;
    bool _targetIsLAstNode;
    bool _EndEncountered;
    float Offset0forseekknode_1forattack;
    bool Bolockison;

    ModesEnum deleteme;
    public EBD_Seek(IEnemyEntityComp argIentity, ModesEnum argmodeenum, bool argcanblock, float argMAxtime, Action argStartAction, bool argDamageOn, int lastRow, float argOffset) : base(argIentity, argmodeenum, argMAxtime, argStartAction, argDamageOn)
    {

        deleteme = argmodeenum;
        Bolockison = argcanblock;
        if (Bolockison)
            GameEventsManager.On_EnemyIsAimed += ToggleAimed;
        _lastRow = lastRow;
        Offset0forseekknode_1forattack = argOffset;
        _startAction = argStartAction;
        m_ieec = argIentity;
        _targetIsLAstNode = false;
        _EndEncountered = false;
        _TargetKNode = argIentity.Get_CurAndOnlyKnode();

    }
    void ToggleAimed(int id, bool onoff)
    {
        if (m_ieec.Get_ID() == id)
        {
            m_ieec.Get_Animer().iSet_Animator_BlockingBool(onoff);

        }
    }
    ~EBD_Seek()
    {
        base.Dispose();
    }
    public override void CompleteAnyAnim(int argCombatanim)
    {


        if (argCombatanim == 999)
        {
            m_ieec.Get_Animer().Do_WalkRunSprintAcordingTocuragro();
        }
        base.CompleteAnyAnim(argCombatanim);
    }

    public override void EndBehavior()
    {
        if (Bolockison)
            GameEventsManager.On_EnemyIsAimed -= ToggleAimed;

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

        if (_EndEncountered)
        {
#if ENABLE_DEBUGLOG
        Debug.Log("id " + m_ieec.Get_ID() + " is " + deleteme.ToString());
            Debug.Log("kseek must end");
#endif
            m_ieec.Get_Animer().iSet_Animator_CrawlingBool(false);
            m_ieec.Trigger_EndBEhaviorTASK(EnemyTaskEneum.ReachedEndOfKnodes);
            return;
        }
        else
      if (m_ieec.Get_Mover().ReachDestinationOffset(Offset0forseekknode_1forattack))
        {
            if (_lastRow < 0)
            {
                m_ieec.Trigger_EndBEhaviorTASK(EnemyTaskEneum.ReachedPlayer);
                // Debug.Log("id " + m_ieec.Get_ID() + " is " + deleteme.ToString() + "  reached player");
            }
            else

                m_ieec.Trigger_EndBEhaviorTASK(EnemyTaskEneum.ReachedKnodeSaught);

#if ENABLE_DEBUGLOG
// Debug.Log("Neww Skseek");
#endif
        }
        else
        {
            m_ieec.Get_Mover().RunRotateTOTarg();
        }
    }

    public override void StartBehavior()
    {
        if (_startAction != null)
            _startAction();
        else Debug.Log("  action null");



        if (_lastRow < 0) return;
        _targetIsLAstNode = _TargetKNode.MyRow >= _lastRow;
        if (_targetIsLAstNode)
        {
            _EndEncountered = true;
            //  Debug.Log("  EndEncountered");
        }


        //else
        //{
        //    //   m_iMover.DoLevelAnim();
        //    Debug.Log("IMPLEMENTME");
        //}


    }

    public override string ToString()
    {
        return GET_MyModeEnum().ToString(); // base.ToString();
    }
}



