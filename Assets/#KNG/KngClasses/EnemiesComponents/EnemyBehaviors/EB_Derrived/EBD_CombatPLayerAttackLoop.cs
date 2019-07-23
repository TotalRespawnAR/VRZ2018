using System;
using UnityEngine;

public class EBD_CombatPLayerAttackLoop : EB_Base
{
    Action _startAction;
    IEnemyEntityComp m_ieec;
    IEnemyMoverComp m_imover;
    KNode _TargetKNode;

    float DelayFullBlockDamage = 0.5f;
    float CurDelay;
    bool AimedAttWasSet;
    bool isInPlayerAttackRange;
    // bool hasPunched = false;
    bool hasPunchedInitialHit = false;
    int _localListOfMoves = 0;
    CombatActions[] Combatm0ves;
    CombatActions[] Combatm1ves;
    CombatActions[] Combatm2ves;
    CombatActions[] Combatm3ves;
    int curCombatMoveIndex = -1;
    bool CrouchTog = false;
    float _minDistRoPlayer;
    public EBD_CombatPLayerAttackLoop(IEnemyEntityComp argIentity, ModesEnum argmodeenum, float argMAxtime, Action argStartAction, bool argDamageOn, float minDistRoPlayer, int CombatMoves) : base(argIentity, argmodeenum, argMAxtime, argStartAction, argDamageOn)
    {
        _minDistRoPlayer = minDistRoPlayer;
        _startAction = argStartAction;
        m_ieec = argIentity;
        m_imover = m_ieec.Get_Mover();
        _localListOfMoves = CombatMoves;

        m_ieec.Set_CurAndOnlyKnode(KnodeProvider.Instance.GEt_PlayerAlphaBravo());
        _TargetKNode = argIentity.Get_CurAndOnlyKnode();
        //  GameEventsManager.On_EnemyIsAimed += ToggleAimed;





        //Combatm0ves = new CombatActions[] {


        //    CombatActions.walkbackwards, //to get away from player and set hasingaed to false when walk back finishes
        //    CombatActions.walkleft,
        //    CombatActions.walkbackwards,
        //    CombatActions.walkright,
        //   CombatActions.walkloop ,



        //};

    }

    ~EBD_CombatPLayerAttackLoop()
    {
        base.Dispose();
    }
    public override void CompleteAnyAnim(int argCombatanim)
    {
        // Debug.Log("completed  " + ((CombatActions)argCombatanim).ToString());

        if (argCombatanim == (int)CombatActions.righthook)
        {
            curCombatMoveIndex = 0;
            // m_ieec.Get_Animer().iSet_CanReact(true);
            // isInPlayerAttackRange = true; //still true we want to move away from payer now
        }
        else
            if (argCombatanim == (int)CombatActions.tauntchest)
        {
            //CAnDetectAimedAt = true;
            m_ieec.Get_Animer().iSet_Animator_AllowBlocking(true);
            m_ieec.Get_Animer().iSet_Animator_CanReactBool(true);
            curCombatMoveIndex++;
        }
        else

            curCombatMoveIndex++;
        // Debug.Log("started  " + Combatm0ves[curCombatMoveIndex].ToString());

        if (curCombatMoveIndex >= Combatm0ves.Length)
        {
            curCombatMoveIndex = 0;
            // Debug.Log("  loop  ");
        }




        if (Combatm0ves[curCombatMoveIndex % Combatm0ves.Length] == CombatActions.crouch)
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
        if (Combatm0ves[curCombatMoveIndex % Combatm0ves.Length] == CombatActions.uncrouch)
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

        AnimateCombatAction((CombatActions)Combatm0ves[curCombatMoveIndex % Combatm0ves.Length]);



        //if (curCombatMoveIndex >= Combatm0ves.Length) { curCombatMoveIndex = 0; Debug.Log("  loop  "); }

        //  Debug.Log("started  " + Combatm0ves[curCombatMoveIndex].ToString());
        // base.CompleteAnyAnim(argCombatanim);
    }

    public override void EndBehavior()
    {
        //  GameEventsManager.On_EnemyIsAimed -= ToggleAimed;
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
        if (m_imover.ReachDestinationOffset(_minDistRoPlayer))
        {
            isInPlayerAttackRange = true;


        }
        else
        {
            isInPlayerAttackRange = false;

        }



        if (isInPlayerAttackRange)
        {
            //m_ieec.Get_Animer().iSet_CanReact(true);

            if (!hasPunchedInitialHit)
            {
                AnimateCombatAction(CombatActions.righthook);
                hasPunchedInitialHit = true;
            }
            //else
            //{
            //    if (!hasPunched) AnimateCombatAction(CombatActions.righthook);

            //}

        }
        //  Debug.Log("pump is attackrange " + isInPlayerAttackRange.ToString());
    }

    public override void StartBehavior()
    {


        Combatm1ves = new CombatActions[] {
             CombatActions.walkbackwards,  //can block can react
            CombatActions.tauntchest,     //no block no react
            CombatActions.walkforward,     //can block can react
             CombatActions.righthook,     //no block no react
        };

        Combatm2ves = new CombatActions[] {
                CombatActions.walkbackwards,  //can block can react
            CombatActions.tauntchest,     //no block no react
            CombatActions.walkforward,     //can block can react
             CombatActions.righthook,     //no block no react
        };


        //Combatm3ves = new CombatActions[] {
        //    CombatActions.walkbackwards,
        //    CombatActions.walkbackwards,
        //    CombatActions.punchattack1,
        //    CombatActions.walkright,
        //    CombatActions.walkleft,
        //    CombatActions.walkleft,
        //    CombatActions.walkright,
        //    CombatActions.walkleft,
        //    CombatActions.walkloop,


        //};
        Combatm3ves = new CombatActions[] {
            CombatActions.walkbackwards,  //can block can react
            CombatActions.tauntchest,     //no block no react
            CombatActions.walkforward,     //can block can react
             CombatActions.righthook,     //no block no react
          //  CombatActions.walkbackwards,
          ////  CombatActions.walkleft,
          //   CombatActions.tauntchest,
          //  CombatActions.walkloop,
          //      CombatActions.walkbackwards,
          ////  CombatActions.walkright,
          //   CombatActions.tauntchest,
          //  CombatActions.walkloop,
        };

        //if (_localListOfMoves <= 1)
        //{
        //    Combatm0ves = new CombatActions[Combatm1ves.Length];

        //    for (int i = 0; i < Combatm1ves.Length; i++) { Combatm0ves[i] = Combatm1ves[i]; }

        //}
        //else
        //    if (_localListOfMoves == 2)
        //{
        //    Combatm0ves = new CombatActions[Combatm2ves.Length];

        //    for (int i = 0; i < Combatm2ves.Length; i++) { Combatm0ves[i] = Combatm2ves[i]; }
        //}
        //else

        //{
        //    Combatm0ves = new CombatActions[Combatm3ves.Length];

        //    for (int i = 0; i < Combatm3ves.Length; i++) { Combatm0ves[i] = Combatm3ves[i]; }
        //}

        Combatm0ves = new CombatActions[Combatm3ves.Length];

        for (int i = 0; i < Combatm3ves.Length; i++) { Combatm0ves[i] = Combatm3ves[i]; }


        if (_startAction != null)
            _startAction();
        else Debug.Log("  action null");





    }

    public override string ToString()
    {
        return GET_MyModeEnum().ToString(); // base.ToString();
    }


    void AnimateCombatAction(CombatActions argcombataction)
    {
        //  Debug.Log("started  " + argcombataction.ToString());

        //m_ieec.Get_Animer().iSet_Animator_AllowBlocking(true);
        //m_ieec.Get_Animer().iSet_Animator_CanReactBool(true);
        //hasPunched = false;
        if (argcombataction == CombatActions.punchattack1)
        {

            //   hasPunched = true;
        }
        else
           if (argcombataction == CombatActions.righthook)
        {
            m_ieec.Get_Animer().iSet_Animator_AllowBlocking(false);
            if (m_ieec.Get_Animer().iGet_BlockingBool()) { m_ieec.Get_Animer().iSet_Animator_BlockingBool(false); }
            m_ieec.Get_Animer().iSet_Animator_CanReactBool(false);
            //m_ieec.Get_Animer().iSet_Animator_AllowBlocking(false);
            //m_ieec.Get_Animer().iSet_Animator_CanReactBool(false);
            //hasPunched = true;

        }
        else if (argcombataction == CombatActions.melepunch)
        {
            //m_ieec.Get_Animer().iSet_Animator_AllowBlocking(false);
            //m_ieec.Get_Animer().iSet_Animator_CanReactBool(false);
            //  hasPunched = true;

        }


        else if (argcombataction == CombatActions.walkbackwards)
        {
            m_ieec.Get_Animer().iSet_Animator_AllowBlocking(true);

            m_ieec.Get_Animer().iSet_Animator_CanReactBool(true);

        }

        else if (argcombataction == CombatActions.walkforward)
        {

            m_ieec.Get_Animer().iSet_Animator_AllowBlocking(true);

            m_ieec.Get_Animer().iSet_Animator_CanReactBool(true);

        }
        else if (argcombataction == CombatActions.tauntchest)
        {

            m_ieec.Get_Animer().iSet_Animator_AllowBlocking(false);
            if (m_ieec.Get_Animer().iGet_BlockingBool()) { m_ieec.Get_Animer().iSet_Animator_BlockingBool(false); }
            m_ieec.Get_Animer().iSet_Animator_CanReactBool(false);


        }


        m_ieec.Get_Animer().iSet_IntCombatMoveType((int)argcombataction);
        m_ieec.Get_Animer().iTrigger_trigCombatMove();
    }


    #region AIMING
    // bool CAnDetectAimedAt = true;


    //no , jtst use ieee tself dum dum 
    //void ToggleAimed(int id, bool onoff)
    //{
    //    //if (CAnDetectAimedAt)
    //    //{

    //    if (m_ieec.Get_ID() == id)
    //    {
    //        string str = "aimed " + onoff.ToString() + "";

    //        if (m_ieec.Get_Animer().iGet_AllowBlocking())
    //        {
    //            str += " blk ";
    //            m_ieec.Get_Animer().iSet_Animator_BlockingBool(onoff);
    //        }
    //        else
    //        {
    //            m_ieec.Get_Animer().iSet_Animator_BlockingBool(false);
    //            str += " not blk";
    //        }

    //    }

    //    //}
    //    //else
    //    //{
    //    //    if (m_ieec.Get_ID() == id)
    //    //    {
    //    //        m_ieec.Get_Animer().iSet_Animator_BlockingBool(false);

    //    //    }
    //    //}

    //}
    #endregion


}
