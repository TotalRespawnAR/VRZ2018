////#define ENABLE_DEBUGLOG
using System;
using UnityEngine;


public class EB_AxeQuestions : IEnemyBehaviorObj, IDisposable
{

    #region Private vars
    IEnemyEntityComp m_ieec;
    IEnemyMoverComp m_iMover;
    ModesEnum _modeenum;
    KNode _CurKnode;
    Rigidbody RBmagnet;
    int _numberofAxesTOThrow;
    int _axeTrhowIntervals;
    bool OneAxeThrown;

    float MaxTimePAss = 0f;
    float CurTime = 0f;
    float CurAxeThrowTimer = 0f;

    int _headShot_NonLethal = 50; //used to be 80
    int _bodyShot_NonLethal = 15;
    int _limbShot_NonLethal = 5;
    #endregion

    #region Constructor Destructor
    public EB_AxeQuestions(IEnemyEntityComp argIbhc, int argNumberOfQuestions, int argAxeInterval)
    {
        _numberofAxesTOThrow = argNumberOfQuestions;
        _axeTrhowIntervals = argAxeInterval;
        MaxTimePAss = _axeTrhowIntervals * (_numberofAxesTOThrow + 1);
        _modeenum = ModesEnum.HURLEOBJ;
#if ENABLE_DEBUGLOG
        Debug.Log("Constructor "+ _modeenum.ToString());
#endif
        m_ieec = argIbhc;
        m_iMover = argIbhc.Get_Mover();
        // _CurKnode = argIbhc.Get_CurAndOnlyKnode();

        StartBehavior();

    }

    ~EB_AxeQuestions()
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
        Debug.Log("Dispose()" + _modeenum.ToString());
#endif
        GC.SuppressFinalize(this);
    }
    #region Interface


    public void StartBehavior()
    {
        //  bool boolval = false;
#if ENABLE_DEBUGLOG
#endif
        m_ieec.Get_Animer().iSet_Animator_CanReactBool(false);
        m_ieec.Get_Animer().iSet_Animator_BlockingBool(false);
        m_ieec.SetTargPos(KnodeProvider.Instance.GEt_PlayerAlphaBravo().GetPos());
        m_ieec.Get_Animer().Do_IDLE_Anim();// m_iMover.setAnimType(EBSTATE.START);
    }
    public void RunBehavior()
    {
        m_iMover.RunRotateTOTarg();
        if (!OneAxeThrown)
        {
            // m_iMover.setAnimTrig(TriggersEnemyAnimator.trigThrowAxe);
            m_ieec.Get_Animer().Do_ThrowAxeaim();
            // Debug.Log("IMPLEMENTME");
            OneAxeThrown = true;
        }

        CurTime += Time.deltaTime;
        CurAxeThrowTimer += Time.deltaTime;
        if (CurAxeThrowTimer > _axeTrhowIntervals)
        {
            CurAxeThrowTimer = 0f;
            OneAxeThrown = false;
        }
        if (CurTime >= MaxTimePAss)
        {
            m_ieec.Trigger_EndBEhaviorTASK(EnemyTaskEneum.HurlingDone);
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
