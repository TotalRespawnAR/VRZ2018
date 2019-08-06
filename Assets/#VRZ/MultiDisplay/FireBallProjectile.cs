using System;
using System.Collections;
using UnityEngine;

public class FireBallProjectile : MonoBehaviour, IShootable
{
    private AudioSource MyAudiosource;
    public TriggersDamageEffects MyHitType;
    int _Max_Row_beforeTargettingPlayer = 3;
    Transform CamTarget;
    float _speedMove = 0;
    float _midDistToWaypointTarget = 0;
    float _curDistTo_curTargetPos = 0;
    int AXE_HP;
    public GameObject ExplosionEffect;
    public GameObject HitEffect;
    float DistToCamAtBirth;
    float DistTraveled;

    Vector3 _originalPlayerPos_V3;
    Vector3 _curPlayerPos_V3;

    Vector3 _curTargetPos_V3;

    Quaternion _toPlayerLookRot;
    Vector3 _moveDir;




    bool _playerDead;
    float _playerYoffset = 0.0f;
    float DistToPlayerWhenStopIndicate;
    public int _AxeId = 0;
    bool hasHitplayer = false;


    Vector3 BirthPos;

    GameManager gm;
    public void SetMoveToPlayerYOffset(int argFlyType)
    { //compensate for the current way Fly_1 and Fly2 are scled
        if (argFlyType == 1)
        {
            _playerYoffset = 0.8f;
        }
    }
    void KillFireBall()
    {

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

    }

    void Start()
    {
        BirthPos = transform.position;
        AXE_HP = 10;
        CamTarget = GameObject.FindGameObjectWithTag("Player").transform;
        _originalPlayerPos_V3 = CamTarget.position;
        DistToCamAtBirth = Vector3.Distance(BirthPos, _originalPlayerPos_V3);
        transform.LookAt(_originalPlayerPos_V3);
        _speedMove = 20.5f;
        _midDistToWaypointTarget = 0.5f;

        AudioLoopTIme(2.0f);
        // audioMnger.PlayEvent("_wushJointStereo");
        AudioLoopTIme(Pitch);
        Destroy(this, 10f);


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

        if (isReleased)
            MotionUpdate();

    }

    public void TellFireBallItPassedPlayer()
    {
        //caled from backwall
        PassedPLAye = true;
        if (!hasHitplayer)
        {

            KillFireBall();
        }


    }
    bool isReleased;
    public void ReleaseTheProjectile()
    {
        this.transform.parent = null;
        isReleased = true;
        transform.LookAt(_originalPlayerPos_V3);
    }
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

        }
        KillFireBall();

    }
    void MotionUpdate()
    {



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
            KillFireBall();
        }



    }
}
