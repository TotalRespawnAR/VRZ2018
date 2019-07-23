using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyBehavior : MonoBehaviour //, IShootable
{



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
