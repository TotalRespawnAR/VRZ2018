//#define ENABLE_DEBUGLOG
using System;
using UnityEngine;
//does seek knode list and perform action whenreaching knode
public class EBD_SeekKnodes : EB_Base
{
    Action _startAction;
    IEnemyEntityComp m_ieec;
    int _knodeIdTOSeek;

    KNode _TargetKNode; //will have been already set by Axeman initbeh to one of the ids in its array of node id to jump to
    bool _targetIsLAstNode;
    bool _EndEncountered;
    float Offset0forseekknode_1forattack;
    bool Blockison;
    public EBD_SeekKnodes(IEnemyEntityComp argIentity, ModesEnum argmodeenum, bool argcanblock, float argMAxtime, Action argStartAction, Action argActionAtKnode, bool argDamageOn, int argknodeidToSeek, float argOffset) : base(argIentity, argmodeenum, argMAxtime, argStartAction, argDamageOn)
    {
        Blockison = argcanblock;
        if (Blockison)
            GameEventsManager.On_EnemyIsAimed += ToggleAimed;

        _knodeIdTOSeek = argknodeidToSeek;

        Offset0forseekknode_1forattack = argOffset;
        _startAction = argStartAction;
        m_ieec = argIentity;
        _targetIsLAstNode = false;
        _EndEncountered = false;
        _TargetKNode = KnodeProvider.Instance.GetNodeByID(_knodeIdTOSeek); // argIentity.Get_CurAndOnlyKnode();


    }
    void ToggleAimed(int id, bool onoff)
    {
        if (m_ieec.Get_ID() == id)
        {
            m_ieec.Get_Animer().iSet_Animator_BlockingBool(onoff);

        }
    }
    ~EBD_SeekKnodes()
    {
        base.Dispose();
    }
    public override void CompleteAnyAnim(int argCombatanim)
    {
        base.CompleteAnyAnim(argCombatanim);
    }

    public override void EndBehavior()
    {
        if (Blockison)
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
        //        if (_EndEncountered)
        //        {
        //#if ENABLE_DEBUGLOG
        //            Debug.Log("kseek must end");
        //#endif
        //            m_ieec.Trigger_EndBEhaviorTASK(EnemyTaskEneum.ReachedEndOfKnodes);
        //            return;
        //        }
        // else
        if (m_ieec.Get_Mover().ReachDestinationOffset(Offset0forseekknode_1forattack))
        {
            //if (_firstKnodeId < 0) { m_ieec.Trigger_EndBEhaviorTASK(EnemyTaskEneum.ReachedPlayer); }
            //else

            m_ieec.Trigger_EndBEhaviorTASK(EnemyTaskEneum.ReachedKnodeSaught);

#if ENABLE_DEBUGLOG
            Debug.Log("Neww Skseek");
#endif
        }
        else
        {
            m_ieec.Get_Mover().RunRotateTOTarg();
        }
    }

    public override void StartBehavior()
    {
        if (Blockison)
        {
            m_ieec.Get_Animer().iSet_Animator_CanReactBool(true);
        }
      
        m_ieec.Set_CurAndOnlyKnode(_TargetKNode);
        if (_startAction != null)
            _startAction();
        else Debug.Log("  action null");

        //if (_firstKnodeId < 0) return;
        //_targetIsLAstNode = _TargetKNode.MyRow >= _firstKnodeId;
        //if (_targetIsLAstNode)
        //{
        //    _EndEncountered = true;
        //    //  Debug.Log("  EndEncountered");
        //}





    }

    public override string ToString()
    {
        return GET_MyModeEnum().ToString(); // base.ToString();
    }
}
