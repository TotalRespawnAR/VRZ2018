//using HoloToolkit.Unity;
using UnityEngine;

public class ZombieAxeMan : MainEntityComponent
{
    //UAudioManager audioMnger;
    private GameObject InHandAxe;
    int[] _4_knodesToJumpTo;

    int _curKNodetojumpto_INDEX;
    int nodeToJumpToPTR;
    int NumberOfAttackPointsBeforeRussingPlayer;
    int curAttackpoint;

    bool PatroleWasSet = false;

    public override void SpetialEnemyIINIT()
    {
        Init_PatrolNodes();
    }
    void Init_PatrolNodes()
    {
        if (PatroleWasSet)
        {
            return;
        }

        if (Get_CurAndOnlyKnode().KnokeID == 1 || Get_CurAndOnlyKnode().KnokeID == 9 ||
            Get_CurAndOnlyKnode().KnokeID == 4 || Get_CurAndOnlyKnode().KnokeID == 7 ||
                Get_CurAndOnlyKnode().KnokeID == 12 || Get_CurAndOnlyKnode().KnokeID == 15)
        {
            _4_knodesToJumpTo = GameSettings.Instance.Get_Patrole(FarMidNearNone.FAR);
        }
        else
        {
            _4_knodesToJumpTo = GameSettings.Instance.Get_Patrole(FarMidNearNone.MID);
        }


        nodeToJumpToPTR = Random.Range(0, _4_knodesToJumpTo.Length);//the increment prtr to ensure a clock wise position
        nodeToJumpToPTR++;
        if (nodeToJumpToPTR >= _4_knodesToJumpTo.Length)
        {
            nodeToJumpToPTR = 0;
        }
        PatroleWasSet = true;
    }


    // 16    18 19 20 21    23
    // 24    26 27 28 29    31
    // 32    34 35 36 37    38
    // 40 41 42 43 44 45 46 47
    // 48 49 50 51 52 53 54 55
    public override void StartEnemy()
    {


        //audioMnger = GetComponent<UAudioManager>();
        GEt_MEsher().ToggleExternalMesh_inTime(true, 0.2f);
        //if (Get_ID() % 3 == 0)
        //{
        //    _4_knodesToJumpTo = new int[] { 33, 37, 40 };
        //}
        //else
        //if (Get_ID() % 3 == 1)
        //{
        //    _4_knodesToJumpTo = new int[] { 34, 27, 39 };
        //}
        //else
        //{
        //    _4_knodesToJumpTo = new int[] { 50, 45, 40 };
        //}
        _curKNodetojumpto_INDEX = 0;
        NumberOfAttackPointsBeforeRussingPlayer = 3;
        curAttackpoint = 0;


        InHandAxe = Get_my_RHandtrans().GetChild(0).GetChild(0).gameObject;// Instantiate(GameManager.Instance.GetaStaticAxe(), Get_my_RHandtrans().position, Quaternion.Euler(Get_my_RHandtrans().localRotation.eulerAngles));
                                                                           // InHandAxe.transform.parent = Get_my_RHandtrans();
        CurMODE = new EBD_StartTimer(this, ModesEnum.STARTIDLE, 3f, StartActionIdle, false);
        InHandAxe.transform.GetChild(0).gameObject.SetActive(false);
        CurMODE.StartBehavior();
        InHandAxe.SetActive(false);
    }

    public override void OnAnimationEventSTR(string arganimname)
    {
        //string arganimname = Get_Animer().CurClipName();
        //  Debug.Log("event " + arganimname);
        string[] thesplit = SplitCommand(arganimname);

        if (thesplit.Length > 2)
        {
            if (thesplit[0] == "C")
            {
                Process_C_AnimEvents(thesplit);
            }
            else
                    if (thesplit[0] == "A")
            {
                Process_A_AnimEvents(thesplit);
            }
        }

    }
    void Process_C_AnimEvents(string[] thesplit)
    {
        if (thesplit[1] == "CMBT")
        {
            if (StringMatch(thesplit[2], CombatActions.idle.ToString()))
            {
                CurMODE.CompleteAnyAnim((int)CombatActions.idle); //but it doesn't matter what int i pass right now , just makes the ebd move the index of actions forward
            }
            else if (StringMatch(thesplit[2], CombatActions.walkbackwards.ToString()))
            {
                CurMODE.CompleteAnyAnim((int)CombatActions.walkbackwards);
            }
            else if (StringMatch(thesplit[2], CombatActions.walkforward.ToString()))
            {
                CurMODE.CompleteAnyAnim((int)CombatActions.walkforward);
            }
            else if (StringMatch(thesplit[2], CombatActions.walkleft.ToString()))
            {
                CurMODE.CompleteAnyAnim((int)CombatActions.walkleft);
            }
            else if (StringMatch(thesplit[2], CombatActions.walkright.ToString()))
            {
                CurMODE.CompleteAnyAnim((int)CombatActions.walkright);
            }
            //else if (StringMatch(thesplit[2], CombatActions.punchattack1.ToString()))
            //{
            //    CurMODE.CompleteAnyAnim((int)CombatActions.punchattack1);
            //}
            //else if (StringMatch(thesplit[2], CombatActions.hadooken.ToString()))
            //{
            //    Trigger_EndBEhaviorTASK(EnemyTaskEneum.LaunchRocket);
            //}
            else
            {
                Debug.Log("no matches");
            }
        }

        else

         if (StringMatch(thesplit[1], EnemyTaskEneum.SWING.ToString()))
        {
            Trigger_EndBEhaviorTASK(EnemyTaskEneum.SWING);

        }
    }
    void Process_A_AnimEvents(string[] thesplit)
    {
        //Debug.Log(thesplit[0]+" " + thesplit[1] + " "+ thesplit[2]);

        if (thesplit[1] == "AXXX")
        {
            if (StringMatch(thesplit[2], AxmanActions.Throw.ToString()))
            {
                Trigger_EndBEhaviorTASK(EnemyTaskEneum.LaunchRocket);
            }
            else
          if (StringMatch(thesplit[2], AxmanActions.Grabbed.ToString()))
            {


                Trigger_EndBEhaviorTASK(EnemyTaskEneum.HandReachedGrabbTarget);
            }



            else
            {
                Debug.Log("no matches");
            }
        }
        else
                if (StringMatch(thesplit[1], "MOVE"))
        {
            if (StringMatch(thesplit[2], "uncrawl"))
            {
                Get_Animer().iSet_Animator_CrawlingBool(false);
            }

        }

        else if (StringMatch(thesplit[1], EnemyTaskEneum.SWING.ToString()))
        {
            if (StringMatch(thesplit[2], TriggersDamageEffects.BearClawLR.ToString()))
            {
                DamagePlayer(TriggersDamageEffects.BearClawLR, Get_Strength());
            }
            else

            if (StringMatch(thesplit[2], TriggersDamageEffects.BearClawRL.ToString()))
            {
                DamagePlayer(TriggersDamageEffects.BearClawRL, Get_Strength());
            }
            else
                if (StringMatch(thesplit[2], TriggersDamageEffects.AxeHit.ToString()))
            {
                DamagePlayer(TriggersDamageEffects.AxeHit, Get_Strength());
            }
            else

            if (StringMatch(thesplit[2], TriggersDamageEffects.GuyleSlash.ToString()))
            {
                DamagePlayer(TriggersDamageEffects.GuyleSlash, Get_Strength());
            }
            else
                       if (StringMatch(thesplit[2], TriggersDamageEffects.ScratchUpdown.ToString()))
            {
                DamagePlayer(TriggersDamageEffects.ScratchUpdown, Get_Strength());
            }
            else

                   if (StringMatch(thesplit[2], TriggersDamageEffects.Scratch2XUpdpwn.ToString()))
            {
                DamagePlayer(TriggersDamageEffects.Scratch2XUpdpwn, Get_Strength());
            }
            else

                   if (StringMatch(thesplit[2], TriggersDamageEffects.RightHookLR.ToString()))
            {
                DamagePlayer(TriggersDamageEffects.RightHookLR, Get_Strength());
            }
            else
                       if (StringMatch(thesplit[2], TriggersDamageEffects.MelePunchRL.ToString()))
            {
                DamagePlayer(TriggersDamageEffects.MelePunchRL, Get_Strength());
            }
            else
                       if (StringMatch(thesplit[2], TriggersDamageEffects.ScratchLR.ToString()))
            {
                DamagePlayer(TriggersDamageEffects.ScratchLR, Get_Strength());
            }
            else
                       if (StringMatch(thesplit[2], TriggersDamageEffects.ScratchRL.ToString()))
            {
                DamagePlayer(TriggersDamageEffects.ScratchRL, Get_Strength());
            }


        }
    }
    public override void Trigger_EndBEhaviorTASK(EnemyTaskEneum argTAskName) //called from Ianimator when onemerged happens
    {
        //CurMODE.FinishedTask(argTAskName);
        switch (argTAskName)
        {
            case EnemyTaskEneum.EndFirstAnimation:
                if (CurMODE.GET_MyModeEnum() == ModesEnum.STARTIDLE)
                {
                    CurMODE.EndBehavior();//anything to call the unsubscriber
#if ENABLE_DEBUGLOG
                    Debug.Log("->ovr EB_SeekAxe");
#endif
                    nodeToJumpToPTR++;
                    if (nodeToJumpToPTR >= _4_knodesToJumpTo.Length)
                    {
                        nodeToJumpToPTR = 0;
                    }

                    CurMODE.EndBehavior();
                    CurMODE = new EBD_SeekKnodes(this, ModesEnum.KSEEK, false, 3f, StartActionAxeManSeek, ActionAxeManRoll, true, _4_knodesToJumpTo[nodeToJumpToPTR], 0.6f); //try 0.5f
                    CurMODE.StartBehavior();
                }
                break;

            case EnemyTaskEneum.ReachedKnodeSaught:
                if (CurMODE.GET_MyModeEnum() == ModesEnum.KSEEK)
                {
                    curAttackpoint++;
                    if (curAttackpoint <= NumberOfAttackPointsBeforeRussingPlayer)
                    {
#if ENABLE_DEBUGLOG
                        Debug.Log("->ovr EB_AXEqiestinos ");
#endif
                        transform.LookAt(Get_CurAndOnlyKnode().GetPos());
                        // Get_Mover().setAnimTrig(TriggersEnemyAnimator.trigSpringRoll);
                        //  Debug.Log("IMPLEMENTME");
                        //transform.LookAt(KnodeProvider.Instance.GEt_PlayerAlphaBravo().GetPos());

                        //Get_Animer().iSet_CanReact(false);
                        CurMODE.EndBehavior();//anything to call the unsubscriber
                        CurMODE = new EB_AxeQuestions(this, 2, 6); //throw 3 ases , 6 seconds appart
                    }
                    else
                    {

                        CurMODE.EndBehavior();//anything to call the unsubscriber
                        CurMODE = new EBD_Seek(this, ModesEnum.TARGETPLAYER, false, 3f, StartActionTargetPlayer, true, -1, 2.6f);
                        CurMODE.StartBehavior();
                    }
                }
                break;
            case EnemyTaskEneum.ReachedPlayer:

                if (CurMODE.GET_MyModeEnum() == ModesEnum.TARGETPLAYER)
                {
                    CurMODE.EndBehavior();
#if ENABLE_DEBUGLOG
                    Debug.Log("ovr ->ATTACKPlayer");
#endif

                    CurMODE = new EBD_AttackPlayer(this, ModesEnum.ATTACK, 3f, StartAction3Reaches, RepeatAttackAction, true, 0.5f);
                    //  CurMODE = new EBD_REACHPLAYERGUN(this, ModesEnum.ATTACK, 3f, StartAction1ReachingForGun, WhatToDoWhenGrabbedGun, true);
                    CurMODE.StartBehavior();
                }
                break;
            case EnemyTaskEneum.Burning:

                if (CurMODE.GET_MyModeEnum() == ModesEnum.KSEEK ||
                    CurMODE.GET_MyModeEnum() == ModesEnum.SEEKTARGET ||
                    CurMODE.GET_MyModeEnum() == ModesEnum.TARGETPLAYER ||
                    CurMODE.GET_MyModeEnum() == ModesEnum.ATTACK)
                {

                    CurMODE.EndBehavior();
#if ENABLE_DEBUGLOG
                    Debug.Log("ovr ->Dead ?? do i even c this");
#endif
                    CurMODE = new EBD_Burn(this, ModesEnum.BURNING, 10f, StartBurnAction);
                    CurMODE.StartBehavior();
                }
                else
                {
                    //Debug.Log("ovr already dead");


                }


                break;

            case EnemyTaskEneum.HurlingDone:
                if (CurMODE.GET_MyModeEnum() == ModesEnum.HURLEOBJ) //axequestion is a hurl
                {
                    CurMODE.EndBehavior();//anything to call the unsubscriber
#if ENABLE_DEBUGLOG
  Debug.Log("->ovr EB_SeekAxe");
#endif
                    nodeToJumpToPTR++;
                    if (nodeToJumpToPTR >= _4_knodesToJumpTo.Length)
                    {
                        nodeToJumpToPTR = 0;
                    }

                    CurMODE.EndBehavior();
                    CurMODE = new EBD_SeekKnodes(this, ModesEnum.KSEEK, true, 3f, StartActionAxeManSeek, ActionAxeManRoll, true, _4_knodesToJumpTo[nodeToJumpToPTR], 1f); //try 0.5f
                    CurMODE.StartBehavior();
                }
                break;

            case EnemyTaskEneum.HandReachedGrabbTarget:
                if (CurMODE.GET_MyModeEnum() == ModesEnum.HURLEOBJ) //axequestion is a hurl
                {
                    // Debug.Log("grabbed axe");
                    InHandAxe.transform.GetChild(0).gameObject.SetActive(true);
                    InHandAxe.SetActive(true);
                }
                break;

            case EnemyTaskEneum.LaunchRocket:
                if (CurMODE.GET_MyModeEnum() == ModesEnum.HURLEOBJ) //axequestion is a hurl
                {
#if ENABLE_DEBUGLOG
                    Debug.Log("axe thrown");
#endif
                    InHandAxe.transform.GetChild(0).gameObject.SetActive(false);
                    InHandAxe.SetActive(false);
                    SpawnActiveAxeinMyHand();
                    Get_Animer().iSet_Animator_CanReactBool(true);
                }
                break;

            case EnemyTaskEneum.KillmeBeforeILayEggs:
                if (CurMODE.GET_MyModeEnum() != ModesEnum.DEAD)
                {
                    CurMODE.EndBehavior();
#if ENABLE_DEBUGLOG
                    Debug.Log("ovr ->Dead");
#endif
                    CurMODE = new EBD_Death(this, ModesEnum.DEAD, 3f, StartActionDeath);
                    CurMODE.StartBehavior();
                }
                else
                {
#if ENABLE_DEBUGLOG
                    Debug.Log("ovr already dead");
#endif
                }
                break;


            default:
                break;
        }

    }


    void SpawnActiveAxeinMyHand()
    {
        //todo : register the axe in enemies manager , map zid to axe id  we can disable the axe when thrower dies .. ad rigidbody  gravity andwait destroy 
        Instantiate(GameManager.Instance.GetaDynamicAxe(), Get_my_RHandtrans().position, Quaternion.identity);
    }


    public override void HandleDeath()
    {
        // Debug.Log("OVR HandleDeath");
        Trigger_EndBEhaviorTASK(EnemyTaskEneum.KillmeBeforeILayEggs);
        //   KillYourselfandCeanitup();

        GameEventsManager.Instance.CAll_WaveAXMANDied();
        base.HandleDeath();

    }

    public void StartActionAxeManSeek()
    {
#if ENABLE_DEBUGLOG
        Debug.LogError("start action seek");
#endif

        // Set_CurAndOnlyKnode(_TargetKNode);//set curand only knode so that we can get it back .. When behavior makes new behavior passing its own ref to imove , the new behavior can only use the ref as long as the parent is alive ... beh should not be allowed to creat new behs 
        Get_Animer().Do_AxeRUN_Anim();
    }

    public void ActionAxeManRoll()
    {
#if ENABLE_DEBUGLOG
        Debug.LogError("start action seek");
#endif
        //KNode tempfirstKnode = Get_CurAndOnlyKnode();
        // KNode _TargetKNode = KnodeProvider.Instance.RequestNextKnode(tempfirstKnode);
        // Set_CurAndOnlyKnode(_TargetKNode);//set curand only knode so that we can get it back .. When behavior makes new behavior passing its own ref to imove , the new behavior can only use the ref as long as the parent is alive ... beh should not be allowed to creat new behs 
        Get_Animer().Do_AxmanRoll_anim();
    }


    public void ActionAxeManThrowAxe()
    {
#if ENABLE_DEBUGLOG
        Debug.LogError("start action seek");
#endif
        //KNode tempfirstKnode = Get_CurAndOnlyKnode();
        // KNode _TargetKNode = KnodeProvider.Instance.RequestNextKnode(tempfirstKnode);
        // Set_CurAndOnlyKnode(_TargetKNode);//set curand only knode so that we can get it back .. When behavior makes new behavior passing its own ref to imove , the new behavior can only use the ref as long as the parent is alive ... beh should not be allowed to creat new behs 
        Get_Animer().Do_ThrowAxeaim();
    }
}
