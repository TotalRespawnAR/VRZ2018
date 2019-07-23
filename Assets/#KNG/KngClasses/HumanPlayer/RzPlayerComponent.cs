#define ENABLE_LOGS
using UnityEngine;

public class RzPlayerComponent : MonoBehaviour
{
    PlayerHudEffectsManagerComponent m_HudEffectsMNGR_JUSTFORDEBUGGING;
    public static RzPlayerComponent Instance = null;
    ThrillAudioCTRL myTHRILLManger;


    #region MonoMethods
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //  if (NumberOfConcurentDamages > 4) NumberOfConcurentDamages = 4;

        }
        else
            Destroy(gameObject);
    }
    void OnEnable()
    {
        ip_dammage_Comp = GetComponent<IRzPlayerDamageComp>();
        m_HudEffectsMNGR_JUSTFORDEBUGGING = GetComponent<PlayerHudEffectsManagerComponent>();
        _initialHP = ip_dammage_Comp.Get_InitialHP();
        _playerCurHP = _initialHP;
        myTHRILLManger = GetComponent<ThrillAudioCTRL>();
    }
    void OnDisable()
    {

    }
    Transform MyHand;
    void Start()
    {
        if (ip_dammage_Comp == null) { Debug.LogError("no idamageComp"); }
        // MyHand = GameManager.Instance.Get_SceneObjectsManager().InSceneStemBase.GetComponent<StemKitMNGR>().GetPlayerMainHandTrans();
    }
    public void SetMyHandPLZ(Transform arghanttrans) { MyHand = arghanttrans; }
    private void Update()
    {
        ip_dammage_Comp.RunUpdateDamage();
        //if (GameManager.Instance != null)
        //    PlayHud_Debug_LiveUPdate("state= " + GameManager.Instance.KngGameState.ToString());

    }
    #endregion
    public void AudioPlayDamage() { PlayHeadShotSound.Instance.PlayDamage(); }
    public void ModulateThrillVolume(float ZeroTo1) { myTHRILLManger.SetvolumeTHrill(ZeroTo1); }
    #region Vars
    IRzPlayerDamageComp ip_dammage_Comp;

    float _playerCurHP;
    float _initialHP;
    public float PlayerCurHP
    {
        get
        {
            return _playerCurHP;
        }

        set
        {
            _playerCurHP = value;

            //Mathf.Clamp(_playerCurHP, 0f, _initialHP);
            if (_playerCurHP <= 0)
            {
                HumanPlayerCurHP_isZERO();
                _playerCurHP = 0f;
            }
        }
    }

    public IRzPlayerDamageComp DamageComp
    {
        get
        {

            return ip_dammage_Comp;

        }

        private set
        {
            ip_dammage_Comp = value;
        }
    }

    public float GEt_HEalth_Percent_01()
    {
        return PlayerCurHP / _initialHP;
    }
    public float GEt_InitialHP()
    {
        return _initialHP;
    }
    #endregion
    #region PrivateMEthids
    void HumanPlayerCurHP_isZERO()
    {
        if (isDEAD) return;

        Debug.Log("HP " + _playerCurHP); //0
        GameManager.Instance.PlayerDied_GameManager();
        ip_dammage_Comp.PlayHudEffect(TriggersDamageEffects.BloodCurtain);
        isDEAD = true;


    }
    void CalcBloodDencityEffects()
    {



        //if (PlayerCurHP > 0 && PlayerCurHP <= _initialHP / 4f)
        //{
        //    ip_dammage_Comp.PlayHudEffect(TriggersDamageEffects.BloodDencity4);
        //}
        //else
        //if (PlayerCurHP > _initialHP / 4f && PlayerCurHP <= _initialHP / 2f)
        //{
        //    ip_dammage_Comp.PlayHudEffect(TriggersDamageEffects.BloodDencity3);
        //}
        //else
        //if (PlayerCurHP > (_initialHP / 2f) && PlayerCurHP > (_initialHP * 3f / 4f))
        //{
        //    ip_dammage_Comp.PlayHudEffect(TriggersDamageEffects.BloodDencity2);
        //}
        //else
        //if (PlayerCurHP > (_initialHP * 3f / 4f) && PlayerCurHP < _initialHP)
        //{
        //    ip_dammage_Comp.PlayHudEffect(TriggersDamageEffects.BloodDencity1);
        //}

    }
    #endregion

    #region publicmethods
    public Transform Get_PlayerHeadCamTrans() { return this.transform; }
    public void DamageHumanPlayer(TriggersDamageEffects argTrigDamEff, int argdamagevalue)
    {

        ip_dammage_Comp.TakeEnemyHit(argTrigDamEff, argdamagevalue);
        CalcBloodDencityEffects();
    }

    bool isDEAD;
    //wvae should do that , or waeve manager 
    public bool IsPlayerdead() { return isDEAD; }
    public void ResetRZplayer()
    {
        PlayerCurHP = _initialHP;
        isDEAD = false;
        //  RzPlayerHealthTubeControllerComponent.Instance.ResetBArs();
    }

    public Transform GetPlayerHANDTarnsForGUnGrab()
    {
        return MyHand;
    }
    #endregion

    public void PlayHud_Debug_RealTime(string argStr)
    {
        // m_HudEffectsMNGR_JUSTFORDEBUGGING.UpdateRealTime(argStr);
    }

    public void PlayHud_Debug_LiveUPdate(string argStr)
    {
        //if (GameManager.Instance.TESTON)
        //  m_HudEffectsMNGR_JUSTFORDEBUGGING.UpdateLive(argStr);
    }
    public void PlayHud_Debug_Event(string argStr)
    {
        // if (GameManager.Instance.TESTON)
        //  m_HudEffectsMNGR_JUSTFORDEBUGGING.UpdateEvent(argStr);
    }
    public void PlayHud_Debug_Vertical(string argStr)
    {
        // if (GameManager.Instance.TESTON)
        // m_HudEffectsMNGR_JUSTFORDEBUGGING.UpdateVert(argStr);
    }




}
