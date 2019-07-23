////#define  ENABLE_DEBUGLOG
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EB_TargetPlayer : IEnemyBehaviorObj, IDisposable
{

    #region Private vars
    IEnemyEntityComp m_ieec;
    IEnemyMoverComp m_imover;
    ModesEnum _modeenum;
    int _headShot_NonLethal = 50; //used to be 80
    int _bodyShot_NonLethal = 15;
    int _limbShot_NonLethal = 5;

    KNode _TargetKNode;

    #endregion

    #region Constructor Destructor
    public EB_TargetPlayer(IEnemyEntityComp argIentity)
    {
        _modeenum = ModesEnum.TARGETPLAYER;
#if ENABLE_DEBUGLOG
        Debug.Log("Construct" + _modeenum.ToString());
#endif
        m_ieec = argIentity;
        m_imover = argIentity.Get_Mover();
        StartBehavior();

    }

    ~EB_TargetPlayer()
    {
#if ENABLE_DEBUGLOG
        Debug.Log("Destructor" + _modeenum.ToString());
#endif
        Dispose();
    }
    #endregion

    public void Dispose()
    {
        //Debug.Log("disposing TARGplayer");
        GC.SuppressFinalize(this);
    }

    #region Interface

    public void StartBehavior()
    {
        _TargetKNode = KnodeProvider.Instance.GEt_PlayerAlphaBravo();
        m_ieec.Set_CurAndOnlyKnode(_TargetKNode);
        //   m_imover.DoLevelAnim();
        Debug.Log("IMPLEMENTME");

    }
    public void RunBehavior()
    {
        if (m_imover.ReachedZOffset()) 
        {
            m_ieec.Trigger_EndBEhaviorTASK(EnemyTaskEneum.ReachedPlayer);

        }
        else
        {
            m_imover.RunRotateTOTarg();
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
        //switch (abulletfromDamageBehComp.BulletPointsType)
        //{
        //    case BulletPointsType.Head:
        //        Update_bodyPartShot_nonLEthalPoints(abulletfromDamageBehComp, _headShot_NonLethal);
        //        Debug.Log("hithead");

        //        if (abulletfromDamageBehComp.hitInfo.point.z < m_ieec.Get_MyHEADtrans().position.z)
        //        {
        //            if (abulletfromDamageBehComp.hitInfo.point.x < m_ieec.Get_MyHEADtrans().position.x)
        //            {
        //                m_ieec.Get_Animer().Set_ReactEnumVal((int)ReactEnumVal.headknockLeft);
        //                Debug.Log("REACT head L");
        //            }
        //            else
        //            {

        //                m_ieec.Get_Animer().Set_ReactEnumVal((int)ReactEnumVal.headknockRight);
        //                Debug.Log("REACT head R");
        //            }

        //        }
        //        else
        //        {
        //            m_imover.Get_Animer().Set_ReactEnumVal((int)ReactEnumVal.Headshot);
        //            Debug.Log("REACT HEAD");
        //        }

        //        m_imover.Get_Animer().Trigger_AnimState(TriggersEnemyAnimator.trigTriggeredAnim);


        //        break;
        //    case BulletPointsType.Torso:
        //        Update_bodyPartShot_nonLEthalPoints(abulletfromDamageBehComp, _bodyShot_NonLethal);
        //        Debug.Log("hittorso");


        //        if (abulletfromDamageBehComp.hitInfo.point.z < m_ieec.Get_MyCHESTtrans().position.z)
        //        {
        //            if (abulletfromDamageBehComp.hitInfo.point.x < m_ieec.Get_MyCHESTtrans().position.x)
        //            {
        //                m_imover.Get_Animer().Set_ReactEnumVal((int)ReactEnumVal.shoulderknockLeft);
        //                Debug.Log("REACT chest L");
        //            }
        //            else
        //            {

        //                m_imover.Get_Animer().Set_ReactEnumVal((int)ReactEnumVal.shoulderknockRight);
        //                Debug.Log("REACT chest R");
        //            }

        //        }
        //        else
        //        {
        //            m_imover.Get_Animer().Set_ReactEnumVal((int)ReactEnumVal.Gutshot);
        //            Debug.Log("REACT chest");
        //        }

        //        m_imover.Get_Animer().Trigger_AnimState(TriggersEnemyAnimator.trigTriggeredAnim);




        //        break;
        //    case BulletPointsType.Hips:
        //        Update_bodyPartShot_nonLEthalPoints(abulletfromDamageBehComp, _bodyShot_NonLethal);
        //        m_imover.Get_Animer().Set_ReactEnumVal((int)ReactEnumVal.hipknock);
        //        Debug.Log("REACT chest");
        //        m_imover.Get_Animer().Trigger_AnimState(TriggersEnemyAnimator.trigTriggeredAnim);
        //        break;
        //    case BulletPointsType.Limbs:
        //        Update_bodyPartShot_nonLEthalPoints(abulletfromDamageBehComp, _limbShot_NonLethal);
        //        break;
        //    default:
        //        // UpdateBloodandScore_LimbShot(bullet);
        //        break;
        //}
    }
    public void CompleteAnyAnim(int argCombatanim)
    {

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
    #endregion
}
