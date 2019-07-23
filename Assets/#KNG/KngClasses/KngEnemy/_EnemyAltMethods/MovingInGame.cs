//#define ENABLE_LINEDEBUG
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingInGame :   MonoBehaviour, Imoveing
{
    #region Props
    public KNode _LastTargetNode;
    public KNode _CurTargetNode;
    public Vector3 _curTargetWayPoint;
    public float MaxdistToTarget = 0.6f;
    public float DistToNextWaypoint;
    CharacterController m_ctrt;
    float charCtrl_HyperSpeed = 0.04f;
    float charCtrl_ChaseSpeed = 0.02f;
    float charCtrl_WalkSpeed = 0.01f;
    float charCtrl_IdleSpeed = 0.0f;
    float characterControler_moveSpeed;
    bool isGrounded; //needs to start as true , beause for one second the zombie is instantiated above ground. zombie waits 2 secods to start reading groundedstate . at this point the dude is on the ground
    IEnemyBehavior m_EBEH;
    Transform M_tarns;
    #endregion

    public void Set_IEBEH(IEnemyBehavior _IEBEH, Transform argTrans ) {
        m_EBEH = _IEBEH;
        M_tarns = argTrans;
    }
    public void MOVE()
    {
        throw new System.NotImplementedException();
    }

    public void MOVE(bool argallow)
    {
        if (argallow) return;

        //if (_IEBEH.Get_STATE() == ZombieState.ENGAGING) {
        //    CheckDistToWaypoint(true);
        //}else
        CheckDistToWaypoint(); //not zdiff , 

        ToggleGrounded(isGrounded);

        RotateToward(_CurTargetNode.GetPos());
        DrawDebugNextTarget();
    }

    void DrawDebugNextTarget()
    {
#if ENABLE_LINEDEBUG
        LR.SetPosition(0, transform.position);
        LR.SetPosition(1, _CurTargetNode.GetPos());
#endif
    }

    void ToggleGrounded(bool last )
    {
        isGrounded = m_ctrt.isGrounded;
        if (last != isGrounded)
        {
            Debug.Log("was +" + last + " now" + isGrounded);
          //  m_EBEH.UpdateGrounded(isGrounded);

        }

    }


    public void Request_NextNode_setCurLastNext( )
    {
        if (_CurTargetNode.BestNextNode(KNodeNExtDir.ToPlayer, 0).IsFinal)
        {
            //MUST STOP MOVING OR SOMETHING
            //Debug.Log(_CurTargetNode.gameObject.name + "  My Target Is FINAL dont set next target transf");
          //  m_EBEH.Set_STATE(EnemyAnimatorState.Birthed);
            return;
        }
        else
        {
            _LastTargetNode = _CurTargetNode;
            _CurTargetNode = KnodeProvider.Instance.RequestNextKnode(_CurTargetNode);
            _curTargetWayPoint = _CurTargetNode.GetPos();
        }
    }
 
    void CheckDistToWaypoint()
    {

        if (GameManager.Instance.KngGameState == ARZState.Pregame) { Debug.Log("no nnnnneeeeexxxxt"); return; }//dont walk dont throw  this will cause enemybeh to stop updating the move function

        //if (argZdiff) {
        //    DistToNextWaypoint = Mathf.Abs(this.transform.position.z - _CurTargetNode.GetPos().z);
        //}
        //else
        //{
        DistToNextWaypoint = Vector3.Distance(this.M_tarns.position, new Vector3(_CurTargetNode.GetPos().x, this.M_tarns.position.y, _CurTargetNode.GetPos().z));
        // }
        if (DistToNextWaypoint < MaxdistToTarget)
        {
            Request_NextNode_setCurLastNext();
        }
    }

    void RotateToward(Vector3 argTargPos)
    {
        Vector3 VectorToloolkAtt = new Vector3(argTargPos.x, M_tarns.position.y, argTargPos.z);
        float rotspeedRadPerSec = 0.8f;
        Vector3 targetDir = VectorToloolkAtt - M_tarns.position;
        float step = rotspeedRadPerSec * Time.deltaTime;

        Vector3 newDir = Vector3.RotateTowards(M_tarns.forward, targetDir, step, 0.0f);
        Debug.DrawRay(M_tarns.position, newDir * 4, Color.red);
        M_tarns.rotation = Quaternion.LookRotation(newDir);
    }
}
