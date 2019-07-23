////#define ENABLE_DEBUGLOG
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EB_SeekKnodeAxeman : IEnemyBehaviorObj, IDisposable
{


    #region Private vars
    IEnemyEntityComp m_ieec;
    IEnemyMoverComp m_iMover;
    ModesEnum _modeenum;
    KNode _TargetKNode;
    int _targNodeID;
    int _headShot_NonLethal = 50; //used to be 80
    int _bodyShot_NonLethal = 15;
    int _limbShot_NonLethal = 5;
    #endregion

    #region Constructor Destructor
    public EB_SeekKnodeAxeman(IEnemyEntityComp argIbhc, int _argNodeID)
    {
        _targNodeID = _argNodeID;
           _modeenum = ModesEnum.KSEEK;
#if ENABLE_DEBUGLOG
        Debug.Log("Constructor "+ _modeenum.ToString());
#endif
        m_ieec = argIbhc;
        m_iMover = argIbhc.Get_Mover();
        StartBehavior();
    }

    ~EB_SeekKnodeAxeman()
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
        Debug.Log("Dispose" + _modeenum.ToString());
#endif
        GC.SuppressFinalize(this);
    }
    #region Interface


    public void StartBehavior()
    {
        _TargetKNode = KnodeProvider.Instance.GetNodeByID(_targNodeID);
        m_ieec.Set_CurAndOnlyKnode(_TargetKNode);//set curand only knode         
       // m_iMover.DoLevelAnim();
        Debug.Log("IMPLEMENTME");
    }
    public void RunBehavior()
    {
           m_iMover.RunRotateTOTarg();

        if (m_iMover.ReachDestinationOffset(0.5f))
        {

            Debug.Log("Neww Skseek DEEEEST REASCHEDHDEHHDE");
#if ENABLE_DEBUGLOG
#endif
            // m_iMover.setAnimTrig(TriggersEnemyAnimator.trigSpringRoll);
            m_ieec.Trigger_EndBEhaviorTASK(EnemyTaskEneum.ReachedKnodeSaught);
        }
       
        
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
        switch (abulletfromDamageBehComp.BulletPointsType)
        {
            case BulletPointsType.Head:
                Update_bodyPartShot_nonLEthalPoints(abulletfromDamageBehComp, _headShot_NonLethal);
                break;
            case BulletPointsType.Torso:
                Update_bodyPartShot_nonLEthalPoints(abulletfromDamageBehComp, _bodyShot_NonLethal);
                break;
            case BulletPointsType.Limbs:
                Update_bodyPartShot_nonLEthalPoints(abulletfromDamageBehComp, _limbShot_NonLethal);
                break;
            default:
                // UpdateBloodandScore_LimbShot(bullet);
                break;
        }
    }

    #endregion

    #region Private Methods
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
    public void CompleteAnyAnim(int argCombatanim)
    {

    }
    #endregion

}
