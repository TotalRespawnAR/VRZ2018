////#define ENABLE_DEBUGLOG
using System;
using UnityEngine;

public class EB_BossCombat : IEnemyBehaviorObj, IDisposable
{
    #region Private vars
    IEnemyEntityComp m_ieec;
    IEnemyMoverComp m_iMover;
    IEnemyAnimatorComp m_ianim;
    ModesEnum _modeenum;
    int _headShot_NonLethal = 10; //used to be 80
    int _bodyShot_NonLethal = 2;
    int _limbShot_NonLethal = 1;

    float DelayFullBlockDamage = 0.5f;
    float CurDelay;

    bool AimedAttWasSet;

    //int[] CombatMoves;
    CombatActions[] Combatm0ves;
    int curCombatMoveIndex = -1;
    bool CrouchTog = false;
    #endregion

    #region Constructor Destructor
    public EB_BossCombat(IEnemyEntityComp argIbhc)
    {
        GameEventsManager.On_EnemyIsAimed += ToggleAimed;
        //CombatMoves = new int[] { 1, 5, 2, 2, 5, 1, 5, 3, 5, 4, 1, 1, 5, 2, 3, 3, 5 };
        Combatm0ves = new CombatActions[] {
        CombatActions.idle,
        CombatActions.walkleft,
        CombatActions.idle,
        CombatActions.punchattack1,
        CombatActions.idle,
        CombatActions.walkright,
        CombatActions.idle,
        CombatActions.walkbackwards,
        CombatActions.idle,
        CombatActions.walkforward,
        CombatActions.idle,
        CombatActions.crouch,
        CombatActions.idle,
        CombatActions.walkleft,
         CombatActions.idle,
        CombatActions.walkright,
         CombatActions.idle,
        CombatActions.walkbackwards,
         CombatActions.idle,
        CombatActions.walkforward,
         CombatActions.idle,
        CombatActions.punchattack1,
         CombatActions.idle,
         CombatActions.uncrouch

        };
        _modeenum = ModesEnum.COMBAT;
#if ENABLE_DEBUGLOG
        Debug.Log("Constructor "+ _modeenum.ToString());
#endif
        m_ieec = argIbhc;
        m_iMover = argIbhc.Get_Mover();
        // m_ianim = m_iMover.Get_Animer();
        m_ieec.Set_CurAndOnlyKnode(KnodeProvider.Instance.GEt_PlayerAlphaBravo());
        StartBehavior();

    }

    ~EB_BossCombat()
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
        //  m_iMover.setAnimType(EBSTATE.COMBATSTATE__31, 5);
        Debug.Log("IMPLEMENTME");

    }
    public void RunBehavior()
    {
        m_iMover.RunRotateTOTarg();
    }
    public void EndBehavior()
    {
        GameEventsManager.On_EnemyIsAimed -= ToggleAimed;
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
        //        m_ianim.Trigger_AnimState(TriggersEnemyAnimator.trigHeadShot);
        //        break;
        //    case BulletPointsType.Torso:
        //        Update_bodyPartShot_nonLEthalPoints(abulletfromDamageBehComp, _bodyShot_NonLethal);
        //        m_ianim.Trigger_AnimState(TriggersEnemyAnimator.trigHitGut);
        //        break;
        //    case BulletPointsType.Limbs:
        //        Update_bodyPartShot_nonLEthalPoints(abulletfromDamageBehComp, _limbShot_NonLethal);
        //        m_ianim.Trigger_AnimState(TriggersEnemyAnimator.trigHitGut);
        //        break;
        //    default:
        //        // UpdateBloodandScore_LimbShot(bullet);
        //        break;
        //}
    }
    public void CompleteAnyAnim(int argCombatanim) //enemyanim gets the end of anim event ->  Enemyentity -> mucurMOD. CompleteCombatAnimd
    {
        Debug.Log("IMPLEMENTME");
        //curCombatMoveIndex++;
        //if (curCombatMoveIndex >= Combatm0ves.Length) { curCombatMoveIndex = 0; Debug.Log("  loop  "); }




        //    if (Combatm0ves[curCombatMoveIndex] == CombatActions.crouch)
        //    {

        //        m_ianim.BOOL_Animator(BoolsEnemyAnimator.crouching, true);
        //        Debug.Log("geddown " + Combatm0ves[curCombatMoveIndex].ToString());
        //        curCombatMoveIndex++;

        //    }
        //    else
        //    if (Combatm0ves[curCombatMoveIndex] == CombatActions.uncrouch)
        //    {

        //        m_ianim.BOOL_Animator(BoolsEnemyAnimator.crouching, false);
        //    Debug.Log("geddown " + Combatm0ves[curCombatMoveIndex].ToString());
        //        curCombatMoveIndex++;
        //    }


        //if (curCombatMoveIndex >= Combatm0ves.Length) { curCombatMoveIndex = 0; Debug.Log("  loop  ");  }
        //Debug.Log("IMPLEMENTME");
        //Debug.Log("started  " + (int)Combatm0ves[curCombatMoveIndex]);
    }

    #endregion

    #region Private Methods


    void ToggleAimed(int id, bool onoff)
    {
        if (m_ieec.Get_ID() == id)
        {
            Debug.Log("IMPLEMENTME");
            //m_ianim.BOOL_Animator(BoolsEnemyAnimator.IsBlocking, onoff);
            m_ieec.SetMainEnemyIsAmedAt(onoff);

        }
    }

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



    void WalkSideWaysToNodeWileFacingPlayer()
    {


    }
}
