//using HoloToolkit.Unity;
using System;
using System.Collections;
using UnityEngine;

public class EnemySpinnerProjectile : MonoBehaviour, IShootable
{
    // public UAudioManager audioMnger;
    private AudioSource MyAudiosource;
    public bool RotateOnforGyleY;

    public TriggersDamageEffects MyHitType;
    public void aimed(bool argTF)
    {
        if (argTF)
            Debug.Log("AIMMED");
        else
            Debug.Log("notaimed");
        // throw new NotImplementedException();
    }

    public void Shot(Bullet argBullet)
    {
        //   Debug.Log("ouch");
        if (HitEffect != null)
        {
            GameObject obj = Instantiate(HitEffect, transform.position, Quaternion.identity) as GameObject;
            obj.AddComponent<KillTimer>().StartTimer(2f);
            //  PlayHeadShotSound.Instance.PlaySplatSound("_MetalDing");
        }
        KillAxe();

    }

    //private void OnEnable()
    //{
    //    GameEventsManager.OnTakeHit += GotHitasInteractiveTag;
    //}
    //private void OnDisable()
    //{
    //    GameEventsManager.OnTakeHit -= GotHitasInteractiveTag;

    //}

    private void GotHitasInteractiveTag(Bullet argBullet, int srgId)
    {

        //throw new NotImplementedException();
    }

    // will use a rand wp on row 0 , then row 1 then row 2 , then target player;
    public Renderer rend;
    int _Max_Row_beforeTargettingPlayer = 3;
    Transform CamTarget;
    float _speedMove = 0;
    float _speedRot = 0;
    float _midDistToWaypointTarget = 0;
    float _curDistTo_curTargetPos = 0;
    int AXE_HP;
    public GameObject ExplosionEffect;
    public GameObject HitEffect;
    // bool _hasIngagedPlayer;

    float DistToCamAtBirth;
    float DistTraveled;

    GameObject TheSpinnigAXE;
    Vector3 _originalPlayerPos_V3;
    Vector3 _curPlayerPos_V3;


    Vector3 _curTargetPos_V3;

    Quaternion _toPlayerLookRot;
    Vector3 _moveDir;




    bool _playerDead;
    float _playerYoffset = 0.0f;
    float DistToPlayerWhenStopIndicate;
    public int _AxeId = 0;
    bool _stopIndicating = false;
    Renderer _renderer;
    bool wasOffScreen = false;
    bool hasHitplayer = false;
    Vector3 RotationAmount = new Vector3(1800f, 0, 0);

    //bool EngagedKillTimerStarted = false;
    public void SetAxeID(int argid) { _AxeId = argid; }
    public int GetAxeId()
    {
        return _AxeId;
    }
    Vector3 BirthPos;

    GameManager gm;
    ZombieIndicatorManager zindicator;
    public void SetMoveToPlayerYOffset(int argFlyType)
    { //compensate for the current way Fly_1 and Fly2 are scled
        if (argFlyType == 1)
        {
            _playerYoffset = 0.8f;
        }
    }
    void KillAxe()
    {

        _stopIndicating = true;
        if (gm != null)
        {
            gm.RemoveAxeid(_AxeId);
        }
        //if (zindicator != null)
        //{
        //    zindicator.RemoveIndicateEnemyProjectileID(_AxeId);
        //}
        GameManager.Instance.ENEMYMNGER_getter().RemoveEnemyProjectile(this._AxeId);

        TheSpinnigAXE.SetActive(false);


        StartCoroutine(WaitPointOneSecToDestroy());
    }


    IEnumerator WaitPointOneSecToDestroy()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(this.gameObject);
    }

    private void Awake()
    {
        MyAudiosource = GetComponent<AudioSource>();
        gm = GameManager.Instance;
        SetAxeID(GameManager.Instance.ENEMYMNGER_getter().GetNewAxeID());
        zindicator = ZombieIndicatorManager.Instance;
        _renderer = rend;
        if (_renderer == null)
        {
            Debug.Log("WArning! No Renderer found ");
        }

        if (!RotateOnforGyleY)
        {
            RotationAmount = new Vector3(1800f, 0, 0);
        }
        else
        {
            RotationAmount = new Vector3(0, 1800f, 0);
        }
    }

    void Start()
    {
        BirthPos = transform.position;
        TheSpinnigAXE = this.gameObject.transform.GetChild(0).gameObject;
        AXE_HP = 10;
        CamTarget = GameObject.FindGameObjectWithTag("MainCamera").transform;
        _originalPlayerPos_V3 = CamTarget.position;
        DistToCamAtBirth = Vector3.Distance(BirthPos, _originalPlayerPos_V3);
        transform.LookAt(_originalPlayerPos_V3);
        // gameObject.AddComponent<DestroyTimer>().timeInSeconds = 30;
        _speedMove = 7.5f;
        _speedRot = 1f;
        _midDistToWaypointTarget = 0.5f;

        AudioLoopTIme(2.0f);
        // audioMnger.PlayEvent("_wushJointStereo");
        AudioLoopTIme(Pitch);


        GameManager.Instance.ENEMYMNGER_getter().AddLiveEnemyProjectile(this.gameObject);


    }


    bool PassedPLAye = false;


    void AudioLoopTIme(float lt)
    {
        //    audioMnger.SetPitch("_wushJointStereo", lt);
        MyAudiosource.pitch = lt;
    }

    public float Pitch = 2.0f;
    // Update is called once per frame
    void Update()
    {
        MotionUpdate();
        TheSpinnigAXE.transform.Rotate(RotationAmount * Time.deltaTime);



        //audioMnger.PlayEvent("_wushJointStereo");

    }

    public void TellAxeItPassedPlayer()
    { //caled from backwall

        PassedPLAye = true;
        if (!hasHitplayer)
        {
            GameManager.Instance.TryCallSlowTime();
            GameEventsManager.Instance.CallTutoPlayerAoidedAxe();
            KillAxe();
        }


    }
    //bool CheckIfPAssedPlayer()
    //{
    //    DistTraveled = Vector3.Distance(transform.position, _originalPlayerPos_V3);
    //    if (Math.Abs(DistTraveled) > (Math.Abs(DistToCamAtBirth) + 0.4f)) return true;
    //    return false;
    //}

    void MotionUpdate()
    {
        if (PassedPLAye) return;

        else
        {
            //CheckOffScreen();
            transform.Translate(Vector3.forward * Time.deltaTime * _speedMove);

            if (Math.Abs(Vector3.Distance(transform.position, CamTarget.position)) < 0.25f)
            {
                if (gm != null)
                {
                    //  gm.UIAxeSplit(); if (!hasHitplayer) { 
                    if (!hasHitplayer)
                    {
                        RzPlayerComponent.Instance.DamageHumanPlayer(MyHitType, 3);
                        // Debug.Log("AXE smacked player in the head");
                        hasHitplayer = true;
                    }
                }
                KillAxe();
            }

        }

    }



    //private void OnCollisionEnter(Collision hit) { Debug.Log(gameObject.name + " just hit collision " + hit.gameObject.name); }
    //private void OnTriggerEnter(Collider hit) { Debug.Log(gameObject.name + " just hit trigger" + hit.name); }


    void CheckOffScreen()
    {
        if (_stopIndicating) { return; }
        if (_renderer == null) { Debug.Log("no rend " + gameObject.name + "_" + _AxeId); return; }
        if (_renderer.IsVisibleFrom(Camera.main) && wasOffScreen)
        {
            wasOffScreen = false;
            // Debug.Log(_flyId + "- Visible");
            if (zindicator != null)
            {
                zindicator.RemoveIndicateEnemyProjectileID(_AxeId);
            }
        }
        else if (!_renderer.IsVisibleFrom(Camera.main) && !wasOffScreen)
        {
            wasOffScreen = true;
            //Debug.Log(_flyId + "- NO Visible");
            if (zindicator != null)
            {
                zindicator.IndicateEnemyProjectileID(_AxeId);
            }
        }
    }


}







//void shittyupdate() {
//    // Debug.DrawRay(transform.position, transform.forward * 4, Color.red);
//    if (gm != null)
//    {
//        if (gm.IsPlayerDead)
//        {
//            //_speedMove = 0;
//            //_speedRot = 0;
//            //if (!_playerDead)
//            //{
//            //    StartCoroutine(WaitPointOneSecToDestroy());
//            //    if (zindicator != null)
//            //    {
//            //        zindicator.RemoveIndicateFlyID(_AxeId);
//            //    }
//            //    _playerDead = true;
//            //    Destroy(this.gameObject);
//            //    return;
//            //}
//        }
//    }

//    _curTargetPos_V3 = CamTarget.position;
//    _curDistTo_curTargetPos = Vector3.Distance(transform.position, _curTargetPos_V3);
//    CheckOffScreen();
//    // //if (!_hasIngagedPlayer) {
//    // //    _toPlayerLookRot = Quaternion.LookRotation(CamTarget.position - this.transform.position);
//    // //    _moveDir = (new Vector3(CamTarget.position.x, CamTarget.position.y - _playerYoffset, CamTarget.position.z) - transform.position).normalized;


//    // //    if (Vector3.Distance(transform.position, _curTargetPos_V3) < 2.8)
//    // //    {
//    // //        _hasIngagedPlayer = true;
//    // //    }


//    // //    Quaternion targetRotation = Quaternion.LookRotation(_curTargetPos_V3 - transform.position);

//    // //    // Smoothly rotate towards the target point.
//    // //    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _speedRot * Time.deltaTime);
//    // //    transform.Translate(Vector3.forward * _speedMove * Time.deltaTime);


//    // //}

//    // else
//    //// if (_hasIngagedPlayer)
//    // {
//    //     if (!_stopIndicating)
//    //     {
//    //         _stopIndicating = true;
//    //         if (zindicator != null)
//    //         {
//    //             zindicator.RemoveIndicateFlyID(_AxeId);
//    //         }
//    //     }

//    //     transform.position += _moveDir * _speedMove * Time.deltaTime;
//    //     //transform.Translate(Vector3.forward * _speedMove * Time.deltaTime);
//    //     //transform.rotation = Quaternion.Slerp(transform.rotation, _toPlayerLookRot, Time.deltaTime * _speedRot * 2);

//    //     if (Vector3.Distance(transform.position, CamTarget.position) < 0.3f)
//    //     {
//    //         if (gm != null)
//    //         {
//    //         gm.HurtPlayerFly();
//    //         }
//    //         KillAxe();
//    //     }
//    //     else if (Vector3.Distance(transform.position, CamTarget.position) > 2.8f)
//    //     {
//    //         GameManager.Instance.TryCallSlowTime();
//    //         KillAxe();
//    //     }
//    //     // transform.LookAt(_curPlayerPos_V3 - transform.position*3);
//    //     //Quaternion targetRotation = Quaternion.LookRotation(_curPlayerPos_V3 - transform.position);

//    //     //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _speedRot/2 * Time.deltaTime );
//    // }





//}    //void shittyupdate() {
//    // Debug.DrawRay(transform.position, transform.forward * 4, Color.red);
//    if (gm != null)
//    {
//        if (gm.IsPlayerDead)
//        {
//            //_speedMove = 0;
//            //_speedRot = 0;
//            //if (!_playerDead)
//            //{
//            //    StartCoroutine(WaitPointOneSecToDestroy());
//            //    if (zindicator != null)
//            //    {
//            //        zindicator.RemoveIndicateFlyID(_AxeId);
//            //    }
//            //    _playerDead = true;
//            //    Destroy(this.gameObject);
//            //    return;
//            //}
//        }
//    }

//    _curTargetPos_V3 = CamTarget.position;
//    _curDistTo_curTargetPos = Vector3.Distance(transform.position, _curTargetPos_V3);
//    CheckOffScreen();
//    // //if (!_hasIngagedPlayer) {
//    // //    _toPlayerLookRot = Quaternion.LookRotation(CamTarget.position - this.transform.position);
//    // //    _moveDir = (new Vector3(CamTarget.position.x, CamTarget.position.y - _playerYoffset, CamTarget.position.z) - transform.position).normalized;


//    // //    if (Vector3.Distance(transform.position, _curTargetPos_V3) < 2.8)
//    // //    {
//    // //        _hasIngagedPlayer = true;
//    // //    }


//    // //    Quaternion targetRotation = Quaternion.LookRotation(_curTargetPos_V3 - transform.position);

//    // //    // Smoothly rotate towards the target point.
//    // //    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _speedRot * Time.deltaTime);
//    // //    transform.Translate(Vector3.forward * _speedMove * Time.deltaTime);


//    // //}

//    // else
//    //// if (_hasIngagedPlayer)
//    // {
//    //     if (!_stopIndicating)
//    //     {
//    //         _stopIndicating = true;
//    //         if (zindicator != null)
//    //         {
//    //             zindicator.RemoveIndicateFlyID(_AxeId);
//    //         }
//    //     }

//    //     transform.position += _moveDir * _speedMove * Time.deltaTime;
//    //     //transform.Translate(Vector3.forward * _speedMove * Time.deltaTime);
//    //     //transform.rotation = Quaternion.Slerp(transform.rotation, _toPlayerLookRot, Time.deltaTime * _speedRot * 2);

//    //     if (Vector3.Distance(transform.position, CamTarget.position) < 0.3f)
//    //     {
//    //         if (gm != null)
//    //         {
//    //         gm.HurtPlayerFly();
//    //         }
//    //         KillAxe();
//    //     }
//    //     else if (Vector3.Distance(transform.position, CamTarget.position) > 2.8f)
//    //     {
//    //         GameManager.Instance.TryCallSlowTime();
//    //         KillAxe();
//    //     }
//    //     // transform.LookAt(_curPlayerPos_V3 - transform.position*3);
//    //     //Quaternion targetRotation = Quaternion.LookRotation(_curPlayerPos_V3 - transform.position);

//    //     //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _speedRot/2 * Time.deltaTime );
//    // }





//}