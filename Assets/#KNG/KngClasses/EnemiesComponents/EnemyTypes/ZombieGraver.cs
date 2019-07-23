using UnityEngine;

public class ZombieGraver : MainEntityComponent
{
    public override void StartEnemy()
    {
        GEt_MEsher().ToggleExternalMesh_inTime(false, 0.2f);
        CurMODE = new EBD_StartTimer(this, ModesEnum.STARTIDLE, 3f, StartActionEmerge, true);
        CurMODE.StartBehavior();
    }



    public override void OnAnimationEventSTR(string arganimname)  // IS C_ +    EnemyTaskEneum.EMERGED, Event name = C_EMERGED 
    {
        ////argstr can start with Apex or A_ and Completed or C_
        ////throw new NotImplementedException();
        ///

        string[] thesplit = SplitCommand(arganimname);

        if (thesplit.Length > 2)
        {
            if (thesplit[0] == "C")
            {
                if (StringMatch(thesplit[1], EnemyTaskEneum.SWING.ToString()))
                {
                    Trigger_EndBEhaviorTASK(EnemyTaskEneum.SWING);
                }
            }
            else
            if (thesplit[0] == "A")
            {
                if (StringMatch(thesplit[1], EnemyTaskEneum.SWING.ToString()))
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
        else
            if (thesplit.Length == 2)
        {

            if (StringMatch(thesplit[1], EnemyTaskEneum.EMERGED.ToString()))
            {
                Trigger_EndBEhaviorTASK(EnemyTaskEneum.EndFirstAnimation);
            }
        }
        else
            Debug.LogError(arganimname + " anime event  must be greater than 3 blocks_ long");
    }

    public override void HandleDeath()
    {
        //  Debug.Log("OVR HandleDeath GRAVERS");
        Trigger_EndBEhaviorTASK(EnemyTaskEneum.KillmeBeforeILayEggs);
        //   KillYourselfandCeanitup();

        GameEventsManager.Instance.CAll_WaveGRAVERDied();
        base.HandleDeath();

    }

}

