using UnityEngine;

public class FlyBehavior : MonoBehaviour, IShootable
{

    public float FlyBehaviorRadius;
    public int NumberOfFlyPlanePoints;

    Vector3 LocalPlaneFLyPos;

    Vector3[] MyPoses;
    Transform PlayerTrans;
    int PosIndex = 0;
    Animator m_anim;
    Rigidbody rb;
    SphereCollider sc;
    public void ReleaseMe()
    {
        if (shot) return;
        if (Attacking) return;

        transform.parent = null;
        transform.LookAt(PlayerTrans);
        Attacking = true;

    }
    void Awake()
    {
        sc = GetComponent<SphereCollider>();
        rb = GetComponent<Rigidbody>();
        m_anim = GetComponent<Animator>();
        PlayerTrans = GameObject.FindGameObjectWithTag("AncTag").transform;
        if (NumberOfFlyPlanePoints <= 0)
        {
            NumberOfFlyPlanePoints = 6;
        }
        MyPoses = new Vector3[NumberOfFlyPlanePoints];

        for (int x = 0; x < NumberOfFlyPlanePoints; x++)
        {

            LocalPlaneFLyPos = new Vector3(Random.Range(-FlyBehaviorRadius, FlyBehaviorRadius), Random.Range(-FlyBehaviorRadius, FlyBehaviorRadius), transform.localPosition.z);
            PosIndex = x;
            MyPoses[PosIndex] = LocalPlaneFLyPos;
            //if (PosIndex == 0)
            //{
            //    Debug.DrawLine(this.transform.position, MyPoses[PosIndex], Color.black, 200f);
            //}
            //else if (PosIndex == 1)
            //{
            //    Debug.DrawLine(this.transform.position, MyPoses[PosIndex], Color.blue, 200f);
            //}
            //else if (PosIndex == 2)
            //{
            //    Debug.DrawLine(this.transform.position, MyPoses[PosIndex], Color.cyan, 200f);
            //}
            //else if (PosIndex == 3)
            //{
            //    Debug.DrawLine(this.transform.position, MyPoses[PosIndex], Color.gray, 200f);
            //}
            //else if (PosIndex == 4)
            //{
            //    Debug.DrawLine(this.transform.position, MyPoses[PosIndex], Color.green, 200f);
            //}
            //else if (PosIndex == 6)
            //{
            //    Debug.DrawLine(this.transform.position, MyPoses[PosIndex], Color.grey, 200f);
            //}
            //else if (PosIndex == 7)
            //{
            //    Debug.DrawLine(this.transform.position, MyPoses[PosIndex], Color.magenta, 200f);
            //}
            //else if (PosIndex == 8)
            //{
            //    Debug.DrawLine(this.transform.position, MyPoses[PosIndex], Color.red, 200f);
            //}
            //else if (PosIndex == 9)
            //{
            //    Debug.DrawLine(this.transform.position, MyPoses[PosIndex], Color.white, 200f);
            //}

            //else
            //    Debug.DrawLine(this.transform.position, MyPoses[PosIndex], Color.yellow, 200f);
        }

        MyPoses[PosIndex] = LocalPlaneFLyPos;

    }

    Vector3 GetNextPosRR()
    {
        PosIndex++;
        if (PosIndex >= MyPoses.Length) PosIndex = 0;

        return MyPoses[PosIndex];
    }

    public Transform NextPos;
    bool Attacking;
    bool Kamikazi;
    bool shot;
    private void Update()
    {
        print("NOOOOO");

        if (shot) return;
        if (!Attacking)
            transform.LookAt(PlayerTrans);
    }
    private void LateUpdate()
    {
        if (shot) return;
        if (!Attacking)
        {

            if (MyPoses[PosIndex] != null)
                transform.position = Vector3.Lerp(transform.position, MyPoses[PosIndex] + this.transform.parent.position, Time.deltaTime * 0.4f);

            if ((this.transform.localPosition - MyPoses[PosIndex]).sqrMagnitude < 0.9f * 0.9f)
            {
                GetNextPosRR();
            }
        }
        else
        {
            if (!Kamikazi)
            {
                transform.LookAt(PlayerTrans);
                transform.Translate(Vector3.forward * Time.deltaTime * 3.5f);
                if ((this.transform.position - PlayerTrans.position).sqrMagnitude < 1.2f * 1.2f)
                {
                    Kamikazi = true;
                }
            }
            else
            {

                transform.Translate(Vector3.forward * Time.deltaTime * 3f);
            }
        }
    }

    public void Shot(Bullet argBullet)
    {
        shot = true;
        m_anim.SetTrigger("FlyDeathTrig");
        if (this.transform.parent != null)
        {

            this.transform.parent = null;
        }
        rb.isKinematic = false;
        rb.useGravity = true;
        sc.isTrigger = true;

        Destroy(gameObject, 5f);
    }



    public void aimed(bool argTF)
    {
        Debug.Log("AIMED");
    }

    //  // will use a rand wp on row 0 , then row 1 then row 2 , then target player;
    //  public Renderer rend;
    //  int _Max_Row_beforeTargettingPlayer = 3;
    //  Transform CamTarget;
    //  float _speedMove = 0;
    //  float _speedRot = 0;
    //  float _midDistToWaypointTarget = 0;
    //  float _curDistTo_curTargetPos = 0;
    //  int FlyHealth;
    //  public GameObject ExplosionEffect;
    //  public GameObject HitEffect;
    //  float effectVanishDelay;
    //  GameObject FlyChild;
    // // WayPointsStruct _WpStruct;
    //  Vector3 _originalPlayerPos_V3;
    //  Vector3 _curPlayerPos_V3;
    //  Vector2 _curWaypoint_RC;
    //  Vector2 _nextWaypoint_RC;
    //  Transform _curWaypoint_TRAN;
    //  Vector3 _curTargetPos_V3;
    //  bool _hasIngagedPlayer = false;
    //  Quaternion _toPlayerLookRot;
    //  Vector3 _moveDir;
    //  int _CUR_ROW = 0;
    //  bool hasExploded = false;
    //  int RadIusEXPLOSION = 5;
    //  public bool IsFireBomb;
    //  bool _playerDead;
    //  float _playerYoffset = 0.0f;
    //  float DistToPlayerWhenStopIndicate;
    // public int _flyId=0;
    //  bool _stopIndicating=false;
    //  Renderer _renderer;
    //  bool wasOffScreen = false;
    //  bool EngagedKillTimerStarted = false;
    //  public void SetFlyID(int argid) { _flyId = argid; }
    //  public int GetFlyId()
    //  {
    //      return _flyId;
    //  }

    //  public void aimed()
    //  {
    //      throw new System.NotImplementedException();
    //  }


    //  public void Shot(Bullet argBullet)
    //  {
    //      // Debug.Log("FLyShot");
    //      FlyHealth -= argBullet.damage;
    //      if (FlyHealth <= 1)
    //      {
    //          KillFly();
    //          return;
    //      }


    //      GameObject obj = Instantiate(HitEffect, argBullet.hitInfo.point, Quaternion.FromToRotation(Vector3.forward, argBullet.hitInfo.normal), argBullet.hitInfo.collider.transform) as GameObject;
    //      KillTimer t = obj.AddComponent<KillTimer>();
    //      t.StartTimer(effectVanishDelay);


    //  }
    //  //public void SetWayPointstruct(WayPointsStruct argWP)
    //  //{
    //  //    _WpStruct = argWP;
    //  //}
    //  public void SetMoveToPlayerYOffset(int argFlyType)
    //  { //compensate for the current way Fly_1 and Fly2 are scled
    //      if (argFlyType == 1)
    //      {
    //          _playerYoffset = 0.8f;
    //      }
    //  }
    //  void KillFly()
    //  {
    //      _stopIndicating = true;
    //      GameManager.Instance.RemoveFlyid(_flyId);
    //      ZombieIndicatorManager.Instance.RemoveIndicateFlyID(_flyId);
    //      FlyChild.SetActive(false);
    //      GameObject obj = Instantiate(ExplosionEffect, transform.position, Quaternion.identity) as GameObject;
    //      obj.transform.parent = this.transform;
    //      if (IsFireBomb && !hasExploded)
    //      {
    //          FireBomb(GameManager.Instance.GEtAllives());
    //      }

    //      StartCoroutine(WaitOneSec());
    //  }


    //  IEnumerator WaitOneSec()
    //  {
    //      yield return new WaitForSeconds(0.1f);
    //      Destroy(this.gameObject);
    //  }
    //  //Transform DeepSearch(Transform parent, string val)
    //  //{
    //  //    foreach (Transform c in parent)
    //  //    {
    //  //        if (c.name == val) { return c; }
    //  //        var result = DeepSearch(c, val);
    //  //        if (result != null)
    //  //            return result;
    //  //    }
    //  //    return null;
    //  //}
    //  private void Awake()
    //  {
    //      //BodyDeci

    //     //DeepSearch(this.transform.GetChild(0), "BodyDeci");
    //    //  _renderer = DeepSearch(this.transform.GetChild(0), "BodyDeci").gameObject.GetComponent<Renderer>();
    //      _renderer = rend;
    //      if (_renderer == null)
    //      {
    //          Debug.Log("WArning! No Renderer found ");
    //      }
    //  }
    //  void Start()
    //  {
    //      FlyChild = this.gameObject.transform.GetChild(0).gameObject;
    //      FlyHealth = 10;
    //      CamTarget = GameObject.FindGameObjectWithTag("MainCamera").transform;
    //      _originalPlayerPos_V3 = CamTarget.position;
    //      transform.LookAt(_originalPlayerPos_V3);
    //     // gameObject.AddComponent<DestroyTimer>().timeInSeconds = 30;
    //      _speedMove = 3.5f;
    //      _speedRot = 3f;
    //      effectVanishDelay = 3.0f;
    //      _midDistToWaypointTarget = 0.5f;
    //      StartCoroutine(WaithalfSec());
    //      //Debug.Log("FLYID" + _flyId);
    //  }

    //  IEnumerator WaithalfSec()
    //  {
    //      yield return new WaitForSeconds(0.2f);
    //      int randCol = 3; //Random.Range(0, 6);
    //      Debug.Log("BROKEN");
    //      // _curWaypoint_TRAN = _WpStruct.GetWayPoint(new Vector2(_CUR_ROW, (float)randCol)).transform;
    //      _curTargetPos_V3 = _curWaypoint_TRAN.position + Vector3.up * 2;
    //      //  transform.LookAt(_WpStruct.GetWayPoint(new Vector2(0f,5f)).transform.position + Vector3.up*2);
    //  }

    //  // Update is called once per frame
    //  void Update()
    //  {
    //      if (GameManager.Instance.IsPlayerDead)
    //      {
    //          _speedMove = 0;
    //          _speedRot = 0;
    //          if (!_playerDead)
    //          {
    //              StartCoroutine(WaitOneSec());
    //              ZombieIndicatorManager.Instance.RemoveIndicateFlyID(_flyId);
    //              _playerDead = true;
    //          }
    //      }

    //      CheckOffScreen();


    //      if (_hasIngagedPlayer)
    //      {
    //          if (!_stopIndicating)
    //          {
    //              _stopIndicating = true;
    //              ZombieIndicatorManager.Instance.RemoveIndicateFlyID(_flyId);
    //          }
    //          transform.position += _moveDir * _speedMove * Time.deltaTime;
    //          Debug.Log("BROKEN");
    //          //transform.Translate(Vector3.forward * _speedMove * Time.deltaTime);
    //          transform.rotation = Quaternion.Slerp(transform.rotation, _toPlayerLookRot, Time.deltaTime * _speedRot * 2);

    //          if (Vector3.Distance(transform.position, CamTarget.position) < 0.8)
    //          {
    //              int Flytype = 0;
    //              if (IsFireBomb) Flytype = 2;
    //              else
    //                  Flytype = 1;
    //              //GameManager.Instance.UIFlySplat();
    //              Debug.Log("FLY smacked player in the head");

    //              KillFly();
    //          }

    //      }
    //      else

    //     if (_curWaypoint_TRAN != null)
    //      {
    //          _curDistTo_curTargetPos = Vector3.Distance(transform.position, _curTargetPos_V3);
    //          if (_curDistTo_curTargetPos < _midDistToWaypointTarget)
    //          {

    //              RequestNewWayPointTarget();
    //          }


    //          Quaternion targetRotation = Quaternion.LookRotation(_curTargetPos_V3 - transform.position);

    //          // Smoothly rotate towards the target point.

    //          transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _speedRot * Time.deltaTime);
    //          transform.Translate(Vector3.forward * _speedMove * Time.deltaTime);

    //      }




    //  }

    //  public void TellFlyItWasAvoided()
    //  {



    //  }

    //  void RequestNewWayPointTarget()
    //  {
    //      if (_hasIngagedPlayer) return;
    //      if (_CUR_ROW < _Max_Row_beforeTargettingPlayer)
    //      {
    //          int randCol = Random.Range(2, 4);
    //          Debug.Log("BROKEN");
    //         // _curWaypoint_TRAN = _WpStruct.GetWayPoint(new Vector2(++_CUR_ROW, (float)randCol)).transform;
    //          _curTargetPos_V3 = _curWaypoint_TRAN.position + Vector3.up * (2 - (float)_CUR_ROW / 10f);
    //      }
    //      else
    //      {
    //          if (!_hasIngagedPlayer)
    //          {
    //              _hasIngagedPlayer = true;
    //              _curWaypoint_TRAN = null;
    //              _toPlayerLookRot = Quaternion.LookRotation(CamTarget.position - this.transform.position);
    //              _moveDir = (new Vector3(CamTarget.position.x, CamTarget.position.y - _playerYoffset, CamTarget.position.z) - transform.position).normalized;


    //              if (!EngagedKillTimerStarted) {
    //                  StartCoroutine(EffetVanish());
    //                  EngagedKillTimerStarted = true;
    //              }
    //              //EngagedKillTimerStarted = true;
    //              //transform.LookAt(_curPlayerPos_V3);
    //          }
    //      }
    //  }
    //  public void FireBomb(List<GameObject> argEnemies)
    //  {
    //      if (hasExploded) return;

    //      List<GameObject> AllLiveZombies = new List<GameObject>();
    //      foreach (GameObject go in argEnemies)
    //      {
    //          AllLiveZombies.Add(go);
    //      }


    //      foreach (GameObject go in AllLiveZombies)
    //      {

    //          float thedist = Vector3.Distance(go.transform.position, this.transform.position);
    //          if (thedist < RadIusEXPLOSION)
    //          {

    //              go.GetComponent<ZombieBehavior>().DoBurnMe();
    //          }
    //      }

    //      hasExploded = true;
    //  }
    //  public void StopFly() {
    //      _speedMove = 0;
    //      _speedRot = 0;
    //  }

    // // private void OnCollisionEnter(Collision hit) { Debug.Log(gameObject.name + " just hit collision " + hit.gameObject.name); }
    ////  private void OnTriggerEnter(Collider hit) { Debug.Log(gameObject.name + " just hit trigger" + hit.name); }


    //  void CheckOffScreen()
    //  {
    //      if (_stopIndicating) { return; }
    //      if (_renderer == null) { Debug.Log("no rend " + gameObject.name + "_" +_flyId); return; }
    //      if (_renderer.IsVisibleFrom(Camera.main) && wasOffScreen)
    //      {
    //          wasOffScreen = false;
    //          // Debug.Log(_flyId + "- Visible");
    //          ZombieIndicatorManager.Instance.RemoveIndicateFlyID(_flyId);
    //      }
    //      else if (!_renderer.IsVisibleFrom(Camera.main) && !wasOffScreen)
    //      {
    //          wasOffScreen = true;
    //          //Debug.Log(_flyId + "- NO Visible");
    //          ZombieIndicatorManager.Instance.IndicateFlyID(_flyId);

    //      }
    //  }

    //  IEnumerator EngagedKillTimer() {
    //      yield return new WaitForSeconds(5);
    //      KillFly();
    //  }

    //  IEnumerator EffetVanish()
    //  {
    //      yield return new WaitForSeconds(effectVanishDelay);

    //  }
}
