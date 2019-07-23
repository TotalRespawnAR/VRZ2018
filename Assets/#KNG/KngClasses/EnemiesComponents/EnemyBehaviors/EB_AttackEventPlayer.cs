////#define ENABLE_DEBUGLOG
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this will be set to losten to mid attack animation event from enemy. a trigger for the attack anim will be set on a timer still
public class EB_AttackEventPlayer : IEnemyBehaviorObj, IDisposable
{

    #region Private vars
    IEnemyEntityComp m_ieec;
    IEnemyMoverComp m_iMover;
    ModesEnum _modeenum;
    float MaxTimePAss = 3f;
    float CurTime = 0f;
    int _headShot_NonLethal = 50; //used to be 80
    int _bodyShot_NonLethal = 15;
    int _limbShot_NonLethal = 5;
    float _attackTimer = 3f;
    bool timercanrun =false;
    #endregion

    #region Constructor Destructor
    public EB_AttackEventPlayer(IEnemyEntityComp argIbhc)
    {
        _modeenum = ModesEnum.ATTACK;
#if ENABLE_DEBUGLOG
        Debug.Log("Construct" + _modeenum.ToString());
#endif
        m_ieec = argIbhc;
        m_iMover = argIbhc.Get_Mover();
        StartBehavior();
    }

    ~EB_AttackEventPlayer()
    {
#if ENABLE_DEBUGLOG
        Debug.Log("Destructor" + _modeenum.ToString());
#endif
        Dispose();
    }
    #endregion

    #region IMode and Idisosable
    public void Dispose()
    {
#if ENABLE_DEBUGLOG
        Debug.Log("disposing ATTACK");
#endif
        GC.SuppressFinalize(this);
    }

    public void StartBehavior()
    {
        CurTime = _attackTimer;
        // _playerKnodePos = KnodeProvider.Instance.GEt_PlayerAlphaBravo().GetPos();
        //  m_ieec.SetTargPos(_playerKnodePos);
        m_ieec.Set_CurAndOnlyKnode(KnodeProvider.Instance.GEt_PlayerAlphaBravo());
     //  m_iMover.setAnimType(EBSTATE.REACHING__8);
        // m_iMover.setAnimTrig(TriggersEnemyAnimator.trigReachSwing);
        // m_ieec.DamagePlayer();
        Debug.Log("IMPLEMENTME");
    }
    public void RunBehavior()
    {
        m_iMover.RunRotateTOTarg();

        if (timercanrun) {

            CurTime -= Time.deltaTime;
            if (CurTime <= 0)
            {
                CurTime = _attackTimer;
#if ENABLE_DEBUGLOG
            Debug.Log("time to scratch");
#endif
                 //m_ieec.DamagePlayer()
                //m_iMover.setAnimType(EnemyAnimatorState.REACHING);

                timercanrun = false;
            }
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
        //        m_iMover.Get_Animer().DO_HEADSHOT_Anim();
        //        break;
        //    case BulletPointsType.Torso:
        //        Update_bodyPartShot_nonLEthalPoints(abulletfromDamageBehComp, _bodyShot_NonLethal);
        //        break;
        //    case BulletPointsType.Hips:
        //        Update_bodyPartShot_nonLEthalPoints(abulletfromDamageBehComp, _bodyShot_NonLethal);
        //        m_iMover.Get_Animer().Do_GUTSHOT_Anim();
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
        //0 was told reachswing was finished
        //1 was told apexwasreached
        if (argCombatanim == 0) {
#if ENABLE_DEBUGLOG
            Debug.Log("Yostart timer");
#endif
            //   m_iMover.setAnimTrig(TriggersEnemyAnimator.trigReachSwing);
            Debug.Log("IMPLEMENTME");
            timercanrun = true;

        } else
            if (argCombatanim==1) {
#if ENABLE_DEBUGLOG
            Debug.Log("BOOOOOOM onlyone");
#endif
        }
        else
            Debug.LogError("YO can only enter 0 or 1 ");
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
