//using HoloToolkit.Unity;
using UnityEngine;

public class BossPumpKin : MainEntityComponent
{

    //UAudioManager audioMnger;

    public override void StartEnemy()
    {
        //audioMnger = GetComponent<UAudioManager>();
        GEt_MEsher().ToggleExternalMesh_inTime(true, 0.2f);
        //CurMODE = new EB_Start(this);
        Get_Animer().iSet_Animator_CanReactBool(true);
        CurMODE = new EBD_StartTimer(this, ModesEnum.STARTIDLE, 3f, StartActionIdle, false);
        CurMODE.StartBehavior();
    }

    public override void Trigger_EndBEhaviorTASK(EnemyTaskEneum argTAskName) //called from Ianimator when onemerged happens
    {
        //CurMODE.FinishedTask(argTAskName);
        switch (argTAskName)
        {
            case EnemyTaskEneum.EndFirstAnimation:
                if (CurMODE.GET_MyModeEnum() == ModesEnum.STARTIDLE)
                {
                    CurMODE.EndBehavior();
                    CurMODE = new EBD_Seek(this, ModesEnum.KSEEK, true, 3f, StartActionSeek, true, 5, 1f);
                    CurMODE.StartBehavior();
                }
                break;

            case EnemyTaskEneum.ReachedKnodeSaught:
                if (CurMODE.GET_MyModeEnum() == ModesEnum.KSEEK)
                {
                    CurMODE.EndBehavior();
#if ENABLE_DEBUGLOG
                    Debug.Log("->->->ovr KSEEK");
#endif
                    CurMODE = new EBD_Seek(this, ModesEnum.KSEEK, true, 3f, StartActionSeek, true, 5, 1f);
                    CurMODE.StartBehavior();
                }
                break;

            case EnemyTaskEneum.ReachedEndOfKnodes:
                if (CurMODE.GET_MyModeEnum() == ModesEnum.KSEEK)
                {
                    // Debug.Log("gotoCOMBAAAT");
                    CurMODE = new EBD_CombatLoop(this, ModesEnum.COMBAT, 3f, StartActionCombat, true);
                    CurMODE.StartBehavior();
                    //CurMODE.EndBehavior();
                    // CurMODE = new EB_BossCombat(this);
                }
                break;

            case EnemyTaskEneum.LaunchRocket:
                if (CurMODE.GET_MyModeEnum() == ModesEnum.COMBAT)
                {
                    // Debug.Log("Y U NO HADUKEN");
                    Instantiate(GameManager.Instance.GetaDynamicGuyle(), Get_my_RHandtrans().position, Quaternion.identity);

                }
                break;




            case EnemyTaskEneum.KillmeBeforeILayEggs:
                if (CurMODE.GET_MyModeEnum() != ModesEnum.DEAD)
                {
                    CurMODE.EndBehavior();
#if ENABLE_DEBUGLOG
                    Debug.Log("ovr ->Dead ?? do i even c this");
#endif
                    CurMODE = new EBD_Death(this, ModesEnum.DEAD, 3f, StartActionDeath);
                    CurMODE.StartBehavior();
                }
                else
                {
                    Debug.Log("ovr already dead");
                }
                break;
            default:
                break;
        }

    }


    public override void OnActionAnimationEndEvent()
    {
        Debug.LogError("should neve be called ");

    }
    public override void HandleDeath()
    {
        // Debug.Log("OVR HandleDeath");
        Trigger_EndBEhaviorTASK(EnemyTaskEneum.KillmeBeforeILayEggs);
        //   KillYourselfandCeanitup();

        GameEventsManager.Instance.CAll_WaveMiniBossDied();
        base.HandleDeath();

    }

    public void PlayAudioEventstr(string Argeventname)
    {

        if (StringMatch(Argeventname, "_thump")) { PlayAudioThump(); }
        if (StringMatch(Argeventname, "_attackGrunt")) { PlayAudioGrunt(); }
        if (StringMatch(Argeventname, "_headPain")) { PlayAudio_headPain(); }
        if (StringMatch(Argeventname, "_gutPain")) { PlayAudio_gutPain(); }


    }

    void PlayAudioThump()
    {
        //audioMnger.PlayEvent("_thump");
    }
    void PlayAudioGrunt()
    {
        // audioMnger.PlayEvent("_attackGrunt");
    }
    void PlayAudio_headPain()
    {
        //  audioMnger.PlayEvent("_headPain");
    }

    void PlayAudio_gutPain()
    {
        // audioMnger.PlayEvent("_gutPain");
    }
    public void StartActionCombat()
    {

        Get_Animer().iSet_EBEstate((int)EBSTATE.COMBAT_eb2);
        Get_Animer().iTrigger_TrigEBstateTransition();
        //ouch after this , we need to get back to combat state 2 in mecanim
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
                Debug.Log("no matches");
        }
    }



    void Process_A_AnimEvents(string[] thesplit)
    {
        //Debug.Log(thesplit[0]+" " + thesplit[1] + " "+ thesplit[2]);

        if (thesplit[1] == "CMBT")
        {
            if (StringMatch(thesplit[2], CombatActions.punchattack1.ToString()))
            {
                Trigger_EndBEhaviorTASK(EnemyTaskEneum.LaunchRocket);
            }



            else
                Debug.Log("no matches");
        }
        else
                if (StringMatch(thesplit[1], "MOVE"))
        {
            if (StringMatch(thesplit[2], "uncrawl"))
            {
                Get_Animer().iSet_Animator_CrawlingBool(false);
            }

        }


    }

}

