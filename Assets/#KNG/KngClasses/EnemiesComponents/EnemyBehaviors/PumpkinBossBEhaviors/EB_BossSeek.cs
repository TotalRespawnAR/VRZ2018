////#define ENABLE_DEBUGLOG
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EB_BossSeek :  IEnemyBehaviorObj, IDisposable
{

    #region Private vars
    IEnemyEntityComp m_ieec;
    IEnemyMoverComp m_iMover;
    IEnemyAnimatorComp m_ianim;
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
    public EB_BossSeek(IEnemyEntityComp argIbhc)
    {
        _modeenum = ModesEnum.KSEEK;
#if ENABLE_DEBUGLOG
        Debug.Log("Constructor "+ _modeenum.ToString());
#endif
        m_ieec = argIbhc;
        m_iMover = argIbhc.Get_Mover();
        Debug.Log("ImplementME");
        //m_ianim = m_iMover.Get_Animer();
        _CurKnode = argIbhc.Get_CurAndOnlyKnode();
        GameEventsManager.On_EnemyIsAimed += ToggleAimed;
        StartBehavior();

    }

    ~EB_BossSeek()
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
//        KNode tempfirstKnode = _CurKnode;
//        _TargetKNode = KnodeProvider.Instance.RequestNextKnode(tempfirstKnode);
//        m_ieec.Set_CurAndOnlyKnode(_TargetKNode);//set curand only knode so that we can get it back .. When behavior makes new behavior passing its own ref to imove , the new behavior can only use the ref as long as the parent is alive ... beh should not be allowed to creat new behs 
//        _targetIsLAstNode = _TargetKNode.MyRow >= 5;
//        if (_targetIsLAstNode)
//        {
//            _EndEncountered = true;
//        }
//        else
//        {
//            // m_iMover.DoLevelAnim();
//            Debug.Log("IMPLEMENTME");
//        }

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

//            // CheckAimed();
           
 //        }
    }
    public void EndBehavior()
    {
        //GameEventsManager.On_EnemyIsAimed -= ToggleAimed;
        //Debug.Log("ImplementME");
        ////  m_ianim.BOOL_Animator(BoolsEnemyAnimator.IsBlocking, false);
        //m_ieec.AimEnemy(false);
        //Dispose();
    }


    public ModesEnum GET_MyModeEnum()
    {
        return _modeenum;
    }

    public void ReadBullet(Bullet abulletfromDamageBehComp)
    {
        Debug.Log("ImplementME");
        //switch (abulletfromDamageBehComp.BulletPointsType)
        //{
        //    case BulletPointsType.Head:
        //        Update_bodyPartShot_nonLEthalPoints(abulletfromDamageBehComp, _headShot_NonLethal);
        //        m_ianim.Trigger_AnimState(TriggersEnemyAnimator.trigHeadShot);
        //        break;
        //    case BulletPointsType.Torso:
        //        Update_bodyPartShot_nonLEthalPoints(abulletfromDamageBehComp, _bodyShot_NonLethal);
        //        m_ianim.Trigger_AnimState(TriggersEnemyAnimator.trigHitGut);
        //        break;
        //    case BulletPointsType.Limbs:
        //        Update_bodyPartShot_nonLEthalPoints(abulletfromDamageBehComp, _limbShot_NonLethal);
        //        break;
        //    default:
        //        // UpdateBloodandScore_LimbShot(bullet);
        //        break;
        //}
    }

    //void CompleteCombatAnim(int argCombatanim);
    public void CompleteAnyAnim(int argCombatanim)
    {
     
    }
    #endregion

    #region Private Methods

    void ToggleAimed(int id, bool onoff) {
        //if (m_ieec.Get_ID() == id)
        //{
        //    Debug.Log("ImplementME");
        //    m_ianim.BOOL_Animator(BoolsEnemyAnimator.IsBlocking, onoff);
        //    m_ieec.AimEnemy(onoff);
        //}
    }

    //void CheckAimed() {

    //    if (m_ieec.IsBEingAomedAt())
    //    {
    //        m_ianim.BOOL_Animator(BoolsEnemyAnimator.IsBlocking, true);
    //    }
    //    else
    //    {
    //        m_ianim.BOOL_Animator(BoolsEnemyAnimator.IsBlocking, false);
          
    //    }


    //}
    void Update_bodyPartShot_nonLEthalPoints(Bullet bullet, int PointsNonLethal)
    {
        int ExraLethalPoins = 0;
        int newHP = m_ieec.Get_HP();
        newHP -= bullet.damage;
        if (newHP < 0)
        {
            newHP = 0;
            ExraLethalPoins = PointsNonLethal;
        }
        m_ieec.Set_HP(bullet, PointsNonLethal + ExraLethalPoins, newHP);
    }
    #endregion
}
