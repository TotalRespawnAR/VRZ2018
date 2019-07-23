using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeGuyMover : MonoBehaviour {
    /*
    AxeEnemyBehavior _ABeh;
    public KngNode _LastTargetNode;
    public KngNode _CurTargetNode;
    public KngNode _NextTargetNode;
    public Vector3 _curTargetWayPoint;
    float MaxdistToTarget = 0.6f;
    float normalSpeed = 110f;
    public float ZombRotSpeed = 110f;
    public float movespeed = 0.8f;
    public int MaxSteps=1;
    public int CurStepSteps = 0;
    public bool HasReachedLastNode = false;
    Quaternion _toRotation;
    Quaternion oldRotation;
    public float DistToNextWaypoint;
    private void Awake()
    {
        _ABeh = GetComponent<AxeEnemyBehavior>();
        if(GameManager.Instance.KngGameState == ARZState.Pregame)
        {
            InvokeRepeating("LaunchAxeProjectile", 0.5f, 0.5f);

        }
    }
     Vector3 GET_CurTargetNodePos()
    {
        return _CurTargetNode.pos;
    }
    public void SET_MYDP(KngSpawnPointsCTRL TheSpawn)
    {
        _LastTargetNode = TheSpawn.ImmediatNextNode;
        _CurTargetNode = TheSpawn.ImmediatNextNode;
        _NextTargetNode = _CurTargetNode.BestNextNodeForYouHere();
       // _ZBEH.ALPHA_BRAVO_TARG = argFinalTarget_AB;// argwpd.GetHotSpot(0).transform;

        //Helpmegetalist(TheSpawn.GetPathsFromHere(argPathNumber));
        _curTargetWayPoint = GET_CurTargetNodePos();//GET_ActiveWaypoint_MOVER();
    }
    public void SET_MYDP(KngNode argWayPointSpawnPlace)
    {
        _LastTargetNode = argWayPointSpawnPlace;
        _CurTargetNode = argWayPointSpawnPlace;
        _NextTargetNode = _CurTargetNode.BestNextNodeForYouHere();
        // _ZBEH.ALPHA_BRAVO_TARG = argFinalTarget_AB;// argwpd.GetHotSpot(0).transform;

        //Helpmegetalist(TheSpawn.GetPathsFromHere(argPathNumber));
        _curTargetWayPoint = GET_CurTargetNodePos();//GET_ActiveWaypoint_MOVER();
    }
    void CheckDistToWaypoint()
    {
       if (GameManager.Instance.KngGameState== ARZState.Pregame ) { return; }//dont walk dont throw 
        DistToNextWaypoint = Vector3.Distance(this.transform.position, new Vector3(_CurTargetNode.pos.x, this.transform.position.y, _CurTargetNode.pos.z));
        if (DistToNextWaypoint < MaxdistToTarget)
        {
            CurStepSteps++;
            if (CurStepSteps < MaxSteps)
            {
                //Debug.Log(" NExtNode");
                _LastTargetNode = _CurTargetNode;
                _CurTargetNode = _NextTargetNode;
                _NextTargetNode = _CurTargetNode.BestNextNodeForYouHere();
                _curTargetWayPoint = _CurTargetNode.pos;
            }
            else {
                if (!HasReachedLastNode) HasReachedLastNode = true;
                _ABeh.UpdateCurAnimState(0);

                //run in arg1 seconds, every arg2 seconds
                InvokeRepeating("LaunchAxeProjectile", 0.5f, 0.5f);
            }
        }
    }

    void LaunchAxeProjectile() {
        _ABeh.TriggetThrowAxe();
    }
    // Use this for initialization



    void RotateToward(Vector3 argTargPos) {
        Vector3 VectorToloolkAtt = new Vector3(argTargPos.x, transform.position.y, argTargPos.z);
        float rotspeedRadPerSec=0.8f;
        Vector3 targetDir = VectorToloolkAtt - transform.position;
        float step = rotspeedRadPerSec * Time.deltaTime;

        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
        Debug.DrawRay(transform.position, newDir*4, Color.red);
        transform.rotation = Quaternion.LookRotation(newDir);
    }
	// Update is called once per frame
	void Update () {
        if (HasReachedLastNode) return;
        if (_ABeh.AxeState0Idle1Walk2Run3DeadTrigThrow5Pause5 == 3) return;
        if (_CurTargetNode == null) return; //when using the tuto, the point is arbitrary
       

        // oldRotation = transform.rotation;

        if (_ABeh.AxeState0Idle1Walk2Run3DeadTrigThrow5Pause5 == 4) //no such state, will make the anmator not do anything
        {
         //   transform.LookAt(new Vector3(GameManager.Instance.PlayerBEH.gameObject.transform.position.x, transform.position.y, GameManager.Instance.PlayerBEH.gameObject.transform.position.z));
        }
        CheckDistToWaypoint();
        if (_ABeh.AxeState0Idle1Walk2Run3DeadTrigThrow5Pause5 ==1  || _ABeh.AxeState0Idle1Walk2Run3DeadTrigThrow5Pause5 == 2)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * movespeed);
        }
        RotateToward(_CurTargetNode.pos);
 
    }

    */
}
