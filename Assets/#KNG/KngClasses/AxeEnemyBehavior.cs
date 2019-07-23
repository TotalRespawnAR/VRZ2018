//#define ENABLE_KEYBORADINPUTS

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeEnemyBehavior : MonoBehaviour , IZBehavior
{

    public delegate void ZombieHPChanged(int argUpdatedHPleft);
    public event ZombieHPChanged OnZombieHPChanged;

    public void Call_ZombieHpChanged(int argNewZhp)
    {
     
        if (OnZombieHPChanged != null)
        {

            OnZombieHPChanged(argNewZhp);
        }
    }

    public int AxeGuyId;
    public GameObject Axe;

    public Transform here1;
    public Transform here2;

    public Animator anim;
    public int Health;
    public GameObject HitEffect;
    public Transform HeadTrans;
    public int AxeState0Idle1Walk2Run3DeadTrigThrow5Pause5;

    bool wasAlreadyHitByShotgun = false;
    bool AlreadyGotHeadShotted=false;
    public IZdamage IDammage;
    private void OnEnable()
    {
        GameEventsManager.OnTakeHit += TakeHit;
        // GameVoiceCommands.OnGamePaused += PauseZombieAnimation;
        //  GameVoiceCommands.OnGameContinue += ContinueZombieAnimation;
    }

    private void OnDisable()
    {
        GameEventsManager.OnTakeHit -= TakeHit;
        //   GameVoiceCommands.OnGamePaused -= PauseZombieAnimation;
        //  GameVoiceCommands.OnGameContinue -= ContinueZombieAnimation;

    }
    int axeid = 0;

    private void Awake()
    {
        IDammage = GetComponent<IZdamage>();
       // SetHP(200);
        
    }

    void Start() {
        anim.speed = 1;
       
            StartCoroutine(StartIdleFor1Sec());
 

        }

#if ENABLE_KEYBORADINPUTS
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            GameObject ax = Instantiate(Axe, here1.position, Quaternion.identity);
            ax.GetComponent<AxeBehavior>().SetAxeID(axeid);
            axeid++;
        }

        if (Input.GetKeyDown(KeyCode.Delete))
        {
            if (IndicatorAlreadyExists(2))
                RemoveIndicateZombieID(2);
        }
    }
#endif
    void TakeHit(Bullet argBullet, int argId)
    {
        //Debug.Log("axe heard takehit event");
        if (argId != AxeGuyId) return;

         
            if (argBullet.MyType == GunType.SHOTGUN)
            {
                if (!wasAlreadyHitByShotgun)
                {
                    RaggFORCEandkillme(argBullet);
                    wasAlreadyHitByShotgun = true;
                }

            }
            else
            {
                //  AnalyzetheHitTakenReacts(argBullet);
                if (IDammage != null)
                IDammage.TakeHit_fromZbehavior(argBullet);
            }
 
       

    }

    public void SetID(int argId)
    {
        AxeGuyId = argId;
    }

    public void SetID_HP_TYPE(int argId, int argHitpoints, Transform _argAlphaBravo, bool isPat, ARZombieypes argZtype)
    {
        AxeGuyId = argId;
        
        IDammage.SetHP(argHitpoints);
    }

    public void SetID_HP_TYPE(int argId, int argHitpoints, bool isPat, ARZombieypes argZtype)
    {
        AxeGuyId = argId;
   
        IDammage.SetHP(argHitpoints);
    }

    public int GetZombieID()
    {
        return AxeGuyId;
    }

    public void Zbeh_PauseZombieAnimation()
    {
        AxeState0Idle1Walk2Run3DeadTrigThrow5Pause5 = 5; //pauseing
        anim.speed = 0;

    }

    public Transform GetZombieHeadTrans()
    {
        return HeadTrans;
    }

    public void AgroMe()
    {
      //  Debug.LogWarning("AgroMe not implementer");
    }

    public void SpeedMeUp()
    {
      //  Debug.LogWarning("speedup not implementer");
    }

    public void IncreaseAgro()
    {
       // Debug.LogWarning("IncreaseAgro not implementer");
    }

    public void FakeBullet_and_ZbehaviorHook()
    {
       // Debug.LogWarning("FakeBullet_and_ZbehaviorHook not implementer");
    }

    public void HasLineOfSight(bool argCanSeeyou)
    {
      //  Debug.LogWarning("Not implemented");
    }

    public void GrenageMe(Transform argGrenade)
    {
       /// Debug.LogWarning("Not implemented");
    }

    public void JUST_LandedFromDROPPING()
    {
       // Debug.LogWarning("Not implemented");
    }

    public void SetHP(int value)
    {
      
        IDammage.SetHP(value);
    }

    public void Raggme()
    {
       // Debug.Log("Not implemented");
    }

    public void RaggmeANDkillMe()
    {
      //  Debug.LogWarning("Not implemented");
    }

    public void RaggFORCEandkillme(Bullet argBulet)
    {
       // Debug.LogWarning("Not implemented");
    }

    public void Flip_Just_got_HeadShotted()
    {
       // Debug.LogWarning("axe guy head shotted");
    }

    public bool Get_Did_alreadyGotHeadshotted()
    {
        return AlreadyGotHeadShotted;
    }

    public void ReceiveHeadBulletCheat(Bullet argBullet)
    {
       // Debug.LogWarning("Not implemented");
    }

    public void Melt()
    {
      //  Debug.Log("Not implemented");
    }

    public void DoBurnMe()
    {
       // Debug.Log("Not implemented");
      //  throw new System.NotImplementedException();
    }

    public void Call_ZombieDied()
    {
        gameObject.AddComponent<KillTimer>().StartTimer(2);
        if (GameManager.Instance != null)
        {
           // Debug.Log("axe id" + AxeGuyId + "died");
            GameManager.Instance.ZombieAXEMAN_ID_Died(AxeGuyId);
            ZombieIndicatorManager.Instance.RemoveIndicateAXEZombieID(AxeGuyId);
        }
    }
    //public void Call_AXeZombieDied()
    //{
    //    gameObject.AddComponent<KillTimer>().StartTimer(2);
    //    if (GameManager.Instance != null)
    //    {
    //        Debug.Log("axe id" + AxeGuyId + "died");
    //        GameManager.Instance.ZombieAXEMAN_ID_Died(AxeGuyId);
    //        ZombieIndicatorManager.Instance.RemoveIndicateAXEZombieID(AxeGuyId);
    //    }
    //}

    //ZombieAXEMAN_ID_Died

    public void UpdateCurAnimState(int argState) {
        anim.SetInteger("state", argState);
        AxeState0Idle1Walk2Run3DeadTrigThrow5Pause5 = argState;
    }

    public void TriggetThrowAxe()
    {
        anim.SetTrigger(TriggersEnemyAnimator.trigThrowAxe.ToString());

    }

    IEnumerator StartIdleFor1Sec()
    {
        yield return new WaitForSeconds(1);

        if (GameManager.Instance.KngGameState == ARZState.Pregame)
        {
            UpdateCurAnimState(0);//stay idle
        }
        else
        UpdateCurAnimState(1);
    }

    public void DO_AnimeThrowApex() {
        if (GameManager.Instance.IsPlayerDead) return;
        GameObject ax = Instantiate(Axe, here1.position, Quaternion.identity);
        ax.GetComponent<EnemySpinnerProjectile>().SetAxeID(axeid);
        axeid++;


    }
}
