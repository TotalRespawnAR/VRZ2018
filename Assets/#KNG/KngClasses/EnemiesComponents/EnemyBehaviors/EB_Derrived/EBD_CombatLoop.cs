using System;
using UnityEngine;

public class EBD_CombatLoop : EB_Base
{
    Action _startAction;
    IEnemyEntityComp m_ieec;
    IEnemyMoverComp m_imover;
    KNode _TargetKNode;

    float DelayFullBlockDamage = 0.5f;
    float CurDelay;
    bool AimedAttWasSet;
    CombatActions[] Combatm0ves;
    CombatActions[] Combatm1ves;
    CombatActions[] Combatm2ves;
    CombatActions[] Combatm3ves;
    int curCombatMoveIndex = -1;
    bool CrouchTog = false;
    public EBD_CombatLoop(IEnemyEntityComp argIentity, ModesEnum argmodeenum, float argMAxtime, Action argStartAction, bool argDamageOn) : base(argIentity, argmodeenum, argMAxtime, argStartAction, argDamageOn)
    {

        _startAction = argStartAction;
        m_ieec = argIentity;
        m_imover = m_ieec.Get_Mover();


        m_ieec.Set_CurAndOnlyKnode(KnodeProvider.Instance.GEt_PlayerAlphaBravo());
        _TargetKNode = argIentity.Get_CurAndOnlyKnode();
        GameEventsManager.On_EnemyIsAimed += ToggleAimed;
        //CombatMoves = new int[] { 1, 5, 2, 2, 5, 1, 5, 3, 5, 4, 1, 1, 5, 2, 3, 3, 5 };



        Combatm1ves = new CombatActions[] {
            CombatActions.walkforward,
             CombatActions.punchattack1,
             CombatActions.walkbackwards,
             CombatActions.walkbackwards,
             CombatActions.walkloop,
             CombatActions.punchattack1,
             CombatActions.punchattack1,
              CombatActions.walkbackwards,
               CombatActions.walkbackwards,
                CombatActions.walkright,
                 CombatActions.walkloop,
                   CombatActions.punchattack1,
             CombatActions.punchattack1,
              CombatActions.walkbackwards,
             CombatActions.walkbackwards,
              CombatActions.walkright,
                      CombatActions.walkloop,
                   CombatActions.punchattack1,
             CombatActions.punchattack1,
              CombatActions.walkbackwards,
             CombatActions.walkbackwards,
        };

        Combatm2ves = new CombatActions[] {
              CombatActions.walkbackwards,
            CombatActions.walkleft,
             CombatActions.punchattack1,
            CombatActions.walkright,
             CombatActions.punchattack1,
             CombatActions.walkleft,
             CombatActions.punchattack1,
            //CombatActions.walkright,
           CombatActions.walkforward,

          CombatActions.crouch,

          CombatActions.walkbackwards,
           CombatActions.punchattack1,
            CombatActions.punchattack1,
            CombatActions.walkforward,
               CombatActions.punchattack1,
                  CombatActions.punchattack1,

              CombatActions.uncrouch,
             CombatActions.punchattack1,
              CombatActions.walkleft,
             CombatActions.punchattack1,
                CombatActions.punchattack1,
                   CombatActions.punchattack1,
              CombatActions.walkbackwards,
               CombatActions.punchattack1,
                  CombatActions.walkright,
             CombatActions.punchattack1,
             CombatActions.walkforward,
            CombatActions.walkright,
             CombatActions.punchattack1,
        };


        Combatm3ves = new CombatActions[] {
              CombatActions.walkbackwards,
            CombatActions.walkleft,
            CombatActions.tauntchest,
        CombatActions.walkright,
             CombatActions.walkbackwards,
             CombatActions.walkleft,
              CombatActions.walkbackwards,
             
            //CombatActions.walkright,
           CombatActions.walkloop,


        };
        Combatm0ves = Combatm3ves;

    }

    ~EBD_CombatLoop()
    {
        base.Dispose();
    }
    public override void CompleteAnyAnim(int argCombatanim)
    {

        curCombatMoveIndex++;
        // Debug.Log("started  " + Combatm0ves[curCombatMoveIndex].ToString());

        if (curCombatMoveIndex >= Combatm0ves.Length)
        {
            curCombatMoveIndex = 0;
            // Debug.Log("  loop  ");
        }




        if (Combatm0ves[curCombatMoveIndex] == CombatActions.crouch)
        {

            m_ieec.Get_Animer().iSet_IsCrouching(true);
            // Debug.Log("geddown " + Combatm0ves[curCombatMoveIndex].ToString());
            curCombatMoveIndex++;
            if (curCombatMoveIndex >= Combatm0ves.Length)
            {
                curCombatMoveIndex = 0;
                //   Debug.Log("  loop  ");
            }

        }
        else
        if (Combatm0ves[curCombatMoveIndex] == CombatActions.uncrouch)
        {

            m_ieec.Get_Animer().iSet_IsCrouching(false);
            //  Debug.Log("geddown " + Combatm0ves[curCombatMoveIndex].ToString());
            curCombatMoveIndex++;
            if (curCombatMoveIndex >= Combatm0ves.Length)
            {
                curCombatMoveIndex = 0;
                //Debug.Log("  loop  "); 
            }
        }

        AnimateCombatAction((CombatActions)Combatm0ves[curCombatMoveIndex]);



        //if (curCombatMoveIndex >= Combatm0ves.Length) { curCombatMoveIndex = 0; Debug.Log("  loop  "); }

        //  Debug.Log("started  " + Combatm0ves[curCombatMoveIndex].ToString());
        // base.CompleteAnyAnim(argCombatanim);
    }

    public override void EndBehavior()
    {
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
        m_imover.RunRotateTOTarg();

    }

    public override void StartBehavior()
    {
        if (_startAction != null)
            _startAction();
        else Debug.Log("  action null");





    }

    public override string ToString()
    {
        return GET_MyModeEnum().ToString(); // base.ToString();
    }

    void ToggleAimed(int id, bool onoff)
    {
        if (m_ieec.Get_ID() == id)
        {
            m_ieec.Get_Animer().iSet_Animator_BlockingBool(onoff);

        }
    }

    void AnimateCombatAction(CombatActions argcombataction)
    {
        // Debug.Log("started  " + argcombataction.ToString());

        if (argcombataction == CombatActions.punchattack1)
        {
            m_ieec.Get_Animer().iSet_Animator_CanReactBool(true);
        }
        m_ieec.Get_Animer().iSet_IntCombatMoveType((int)argcombataction);
        m_ieec.Get_Animer().iTrigger_trigCombatMove();
    }
}
