using UnityEngine;

public class RzPlayerDamageComponent : MonoBehaviour, IRzPlayerDamageComp
{
    public int HP;
    public int NumberOfConcurentDamages;
    public float HitCooldown;
    public float healingScalar;//= 0.4f;

    RzPlayerComponent m_player;
    IPLayerDamageStartegie m_iDamageStartegy;
    GameObject CurDamageStartegieObject; //isneverchanged
    float CurHealthPercent_0_1 = 1f;
    float _curDamagetaken = 0f;
    float Cahsed_Initial_HP;
    PlayerHudEffectsManagerComponent m_HudEffectsMNG;


    float ZombieHIt_RecoveryTime;

    public void TakeEnemyHit(TriggersDamageEffects argTrigDamEff, float argFloatdamage)
    {
        //return;

        PlayHudEffect(argTrigDamEff);
        m_player.AudioPlayDamage();
        if (argTrigDamEff == TriggersDamageEffects.AxeHit)
        {
            //  PlayHudEffect(TriggersDamageEffects.GlassBreak1);
        }
        m_iDamageStartegy.IncurrDamage(argTrigDamEff, argFloatdamage);

    }
    //public void TakeEnemyHit(TriggersDamageEffects _argTrigDamEff, float argFloatdamage)
    //{

    //    //HitType argHt = HitType.None;
    //    //if (sev == swingEnumVal.rightArmSwipe)
    //    //{
    //    //    argHt = HitType.ZombieScratchLR;
    //    //}
    //    //else
    //    //        if (sev == swingEnumVal.rightarmOverheadDown)
    //    //{
    //    //    argHt = HitType.ZombieScratchRL;
    //    //}
    //    //else
    //    //if (sev == swingEnumVal.botharmsUpDown)
    //    //{
    //    //    argHt = HitType.ZobieScratch2handupdown;
    //    //}
    //    m_iDamageStartegy.IncurrDamage(_argTrigDamEff, argFloatdamage);
    //}


    private void Awake()
    {
        m_player = GetComponent<RzPlayerComponent>();
        m_HudEffectsMNG = GetComponent<PlayerHudEffectsManagerComponent>();

    }
    void Start()
    {
        Cahsed_Initial_HP = m_player.GEt_InitialHP();
        CurDamageStartegieObject = new GameObject();
        CurDamageStartegieObject.name = "TimerDamageStrategie";
        CurDamageStartegieObject.transform.parent = this.transform;
        CurDamageStartegieObject.AddComponent<PlayerDamageTimerStrategie>();
        m_iDamageStartegy = CurDamageStartegieObject.GetComponent<IPLayerDamageStartegie>();
        m_iDamageStartegy.InitHudEffects(m_HudEffectsMNG);

    }


    public float chp;

    public void RunUpdateDamage()
    {
        chp = m_player.PlayerCurHP;

        if ((int)m_player.PlayerCurHP >= (int)m_player.GEt_InitialHP())
        {
            m_player.PlayerCurHP = m_player.GEt_InitialHP();
            return;
        }
        m_player.PlayerCurHP += (healingScalar * Time.deltaTime);
        RzPlayerHealthTubeControllerComponent.Instance.Updated_Percent_PlayerHEalthBAr(m_player.GEt_HEalth_Percent_01());
    }

    public void PlayHudEffect(TriggersDamageEffects argDamageTrig)
    {
        //  Debug.Log("HUD trig" + argDamageTrig.ToString());
        m_HudEffectsMNG.Trig_DamageEffect(argDamageTrig);
    }


    public int Get_NumberofConcurrentDamages()
    {
        return NumberOfConcurentDamages;
    }

    public float Get_CooldownTime()
    {
        return HitCooldown;
    }

    public float Get_InitialHP()
    {
        return HP;
    }
}
