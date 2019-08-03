using System.Collections;
using UnityEngine;

public class PreyGraver : MainEntityComponent
{
    public override void StartEnemy()
    {
        GEt_MEsher().ToggleExternalMesh_inTime(false, 0.2f);
        CurMODE = new EB_StartGraveNoDeath(this);
        CurMODE.StartBehavior();
    }

    public override void Trigger_EndBEhaviorTASK(EnemyTaskEneum argTAskName) //called from Ianimator when onemerged happens
    {
        //CurMODE.FinishedTask(argTAskName);
        switch (argTAskName)
        {
            case EnemyTaskEneum.EndFirstAnimation:
                if (CurMODE.GET_MyModeEnum() == ModesEnum.STARTGRAVENODEATH)
                {
                    CurMODE.EndBehavior();//anything to call the unsubscriber
#if ENABLE_DEBUGLOG

                    Debug.Log("->ovr Wait prey");
#endif

                    CurMODE = new EB_Wait(this);
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
            case EnemyTaskEneum.KillmeBeforeILayEggs:
                if (CurMODE.GET_MyModeEnum() != ModesEnum.DEAD)
                {
                    CurMODE.EndBehavior();
#if ENABLE_DEBUGLOG

                    Debug.Log("ovr ->Dead");
#endif

                    CurMODE = new EB_DeadPrey(this);
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

    public override void HandleDeath()
    {
#if ENABLE_KEYBORADINPUTS

        Debug.Log("OVR HandleDeath");
#endif

        Trigger_EndBEhaviorTASK(EnemyTaskEneum.KillmeBeforeILayEggs);
        //GameEventsManager.Instance.CallPreyIsReadyForPickup(this);
        StartCoroutine(WaitThenSignalPreyReady(4));
    }

    IEnumerator WaitThenSignalPreyReady(float argtime)
    {
        yield return new WaitForSeconds(argtime);
#if ENABLE_KEYBORADINPUTS

        Debug.Log("prey ready");
#endif

        GameEventsManager.Instance.CallPreyIsReadyForPickup(this);
    }

}
