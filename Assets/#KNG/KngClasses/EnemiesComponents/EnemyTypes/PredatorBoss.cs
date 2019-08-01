using UnityEngine;

public class PredatorBoss : MainEntityComponent
{

    public override void StartEnemy()
    {
        CurMODE = new EB_StartHurler(this);
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
                    Debug.Log("->ovr EB_SeekTargetHurler ");
                    CurMODE = new EB_SeekTargetHurler(this);
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

            case EnemyTaskEneum.HandReachedGrabbTarget:
                if (CurMODE.GET_MyModeEnum() == ModesEnum.SEEKTARGET)
                {
                    CurMODE.EndBehavior();//anything to call the unsubscriber
                    Debug.Log("->ovr EB_GrabHurlerPrey predator");
                    CurMODE = new EB_GrabHurlerPrey(this);
                }
                break;



            case EnemyTaskEneum.GrabbedEnemy:
                if (CurMODE.GET_MyModeEnum() == ModesEnum.GRABBB)
                {
                    CurMODE.EndBehavior();//anything to call the unsubscriber
                    Debug.Log("->ovr EB_HurleObjHurler predator");
                    CurMODE = new EB_HurleObjHurler(this);
                }
                break;

            case EnemyTaskEneum.LaunchRocket:
                if (CurMODE.GET_MyModeEnum() == ModesEnum.HURLEOBJ)
                {
                    CurMODE.EndBehavior();//anything to call the unsubscriber
                    Debug.Log("->ovr EB_LaunchBodyHurler predator");
                    CurMODE = new EB_LaunchBodyHurler(this);
                }
                break;


            case EnemyTaskEneum.HurlingDone:
                if (CurMODE.GET_MyModeEnum() == ModesEnum.PROJECTILE)
                {
                    CurMODE.EndBehavior();//anything to call the unsubscriber
                    Debug.Log("->ovr EB_PointHurler predator");
                    CurMODE = new EB_PointHurler(this);
                }
                break;
            case EnemyTaskEneum.Pointing:
                if (CurMODE.GET_MyModeEnum() == ModesEnum.POINTFINGER)
                {
                    CurMODE.EndBehavior();//anything to call the unsubscriber
                    Debug.Log("->ovr EB_TargetPlayer predator");
                    CurMODE = new EB_TargetPlayer(this);
                }
                break;

            case EnemyTaskEneum.KillmeBeforeILayEggs:
                if (CurMODE.GET_MyModeEnum() != ModesEnum.DEAD)
                {
                    CurMODE.EndBehavior();
                    Debug.Log("ovr ->EB_DeadPrey");
                    CurMODE = new EB_DeadPrey(this);
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

    public override void HandleDeath()
    {
        Debug.Log("OVR HandleDeath");
        Trigger_EndBEhaviorTASK(EnemyTaskEneum.KillmeBeforeILayEggs);
        KillYourselfandCeanitup();
        //GameEventsManager.Instance.CallPreyIsReadyForPickup(this);
        //  StartCoroutine(WaitThenSignalPreyReady(4));
    }

    //IEnumerator WaitThenSignalPreyReady(float argtime)
    //{
    //    yield return new WaitForSeconds(argtime);
    //    Debug.Log("prey ready");
    //    GameEventsManager.Instance.CallPreyIsReadyForPickup(this);
    //}


}
