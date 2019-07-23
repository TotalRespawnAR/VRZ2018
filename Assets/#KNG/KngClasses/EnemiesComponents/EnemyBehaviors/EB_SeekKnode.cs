#define ENABLE_DEBUGLOG
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EB_SeekKnode : IEnemyBehaviorObj, IDisposable
{
    #region Private vars
    IEnemyEntityComp m_ieec;
    IEnemyMoverComp m_iMover;
    ModesEnum _modeenum;
    int _headShot_NonLethal = 50; //used to be 80
    int _bodyShot_NonLethal = 15;
    int _limbShot_NonLethal = 5;
    KNode _CurKnode;
    KNode _TargetKNode;
    bool _targetIsLAstNode;
    bool _EndEncountered;
    #endregion

    #region Constructor Destructor
    public EB_SeekKnode(IEnemyEntityComp argIbhc)
    {
        _modeenum = ModesEnum.KSEEK;
#if ENABLE_DEBUGLOG
        Debug.Log("Constructor "+ _modeenum.ToString());
#endif
        m_ieec = argIbhc;
        m_iMover = argIbhc.Get_Mover();
        _CurKnode = argIbhc.Get_CurAndOnlyKnode();
        
        StartBehavior();

    }

    ~EB_SeekKnode()
    {
#if ENABLE_DEBUGLOG
        Debug.Log("Destructor" + _modeenum.ToString());
#endif
        Dispose();
    }
    #endregion
    public void Dispose()
    {
#if ENABLE_DEBUGLOG
        Debug.Log("disposing SeekKK");
#endif
        GC.SuppressFinalize(this);
    }
    #region Interface


    public void StartBehavior()
    {
       // m_iMover.Get_Animer().Set_IsRootMotion(false);

        KNode tempfirstKnode = _CurKnode;
        _TargetKNode = KnodeProvider.Instance.RequestNextKnode(tempfirstKnode);
        m_ieec.Set_CurAndOnlyKnode(_TargetKNode);//set curand only knode so that we can get it back .. When behavior makes new behavior passing its own ref to imove , the new behavior can only use the ref as long as the parent is alive ... beh should not be allowed to creat new behs 
        _targetIsLAstNode = _TargetKNode.MyRow >=6;
        if (_targetIsLAstNode)
        {
            _EndEncountered = true;
        }
        else
        {
            //   m_iMover.DoLevelAnim();
            Debug.Log("IMPLEMENTME");
        }
        
    }
    public void RunBehavior()
    {
//        if (_EndEncountered)
//        {
//#if ENABLE_DEBUGLOG
//            Debug.Log("kseek must end");
//#endif
//            m_ieec.Trigger_EndBEhaviorTASK(EnemyTaskEneum.ReachedEndOfKnodes);
//            return;
//        }
//        else
//        if (m_iMover.ReachedDestination())
//        {
//            m_ieec.Trigger_EndBEhaviorTASK(EnemyTaskEneum.ReachedKnodeSaught);         
//#if ENABLE_DEBUGLOG
//// Debug.Log("Neww Skseek");
//#endif
//        }
//        else
//        {
//            m_iMover.RunRotateTOTarg();
//        }
    }
    public void EndBehavior( )
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


}
