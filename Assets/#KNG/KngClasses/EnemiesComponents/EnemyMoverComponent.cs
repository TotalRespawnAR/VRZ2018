////#define  ENABLE_DEBUGLOG
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 [RequireComponent(typeof(EnemyAnimatorV6Component))]
public class EnemyMoverComponent : MonoBehaviour, IEnemyMoverComp
{
    #region Dependencies privatvars
    IEnemyEntityComp m_ieec;
    IEnemyAnimatorComp m_IAnimatorComp;
    float DistToTarget;
    float ZDistTTareget;
    float MaxdistToTarget = 1f;
    float Max_Z_DistToPlayer = 2.8f;
    float BAseRoatateSpeedSLow = 1.1f;
    float BAseRoatateSpeedFast = 5.1f;
    float rotspeedRadPerSec = 1.1f;//0.8f;
    #endregion

   
    #region MonoMethods
    void Awake()
    {
     
    }
    void OnEnable()
    {
    }
    void OnDisable()
    {

    }

    void Start()
    {
   
        m_ieec = GetComponent<IEnemyEntityComp>();
        m_IAnimatorComp = GetComponent<IEnemyAnimatorComp>();
    }
    #endregion
    #region InterfaceImplementation
    public void InitAnimator(SeekSpeed argSeekSpeed, bool argRootMotion)
    {

        m_IAnimatorComp = GetComponent<IEnemyAnimatorComp>();

        m_IAnimatorComp.iSet_IsRootMotion(argRootMotion);
       m_IAnimatorComp.iSet_Level_Seekspeed(argSeekSpeed);
    }
    public void RunRotateTOTarg() {
        RotateToward(m_ieec.GetTargPos());
    }
    // public void Task_MoveUPDATEToPos(Vector3 argTargPos) { RotateToward(argTargPos); }

    public bool ReachedDestination()
    {
        DistToTarget = Vector3.Distance(m_ieec.GetMyPos(), new Vector3(m_ieec.GetTargPos().x, m_ieec.GetMyPos().y, m_ieec.GetTargPos().z));
        return (DistToTarget < MaxdistToTarget);
    }

    public bool ReachedZOffset()
    {
        ZDistTTareget = Vector3.Distance(m_ieec.GetMyPos(), new Vector3(m_ieec.GetMyPos().x, m_ieec.GetMyPos().y, m_ieec.GetTargPos().z));
        return (ZDistTTareget < Max_Z_DistToPlayer);
    }

 
 
  

    public IEnemyAnimatorComp Get_Animer()
    {
        return m_IAnimatorComp;
    }

    public bool ReachDestinationOffset(float argdistreach)
    {
        return CheckDistance(argdistreach);
    }
    public void SetRotateSpeed(SeekSpeed argseekspeed) {
        if (argseekspeed == SeekSpeed.sprint)
        {
            rotspeedRadPerSec = BAseRoatateSpeedFast;
        }
        else
            rotspeedRadPerSec = BAseRoatateSpeedSLow ;
    }

    #endregion
    #region PrivateMeth
    void RotateToward(Vector3 argTargPos)
    {
         
        
        Vector3 VectorToloolkAtt = new Vector3(argTargPos.x, transform.position.y, argTargPos.z);

        Vector3 targetDir = VectorToloolkAtt - transform.position;
        float step = rotspeedRadPerSec * Time.deltaTime;

        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
        Debug.DrawRay(transform.position, newDir * 4, Color.red);
        transform.rotation = Quaternion.LookRotation(newDir);
    }

    bool CheckDistance()
    {
        DistToTarget = Vector3.Distance(m_ieec.GetMyPos(), new Vector3(m_ieec.GetTargPos().x, m_ieec.GetMyPos().y, m_ieec.GetTargPos().z));
#if ENABLE_DEBUGLOG
        // Debug.Log("regular dist "+ DistToTarget);
#endif
        if (DistToTarget < MaxdistToTarget)
        {
#if ENABLE_DEBUGLOG
         Debug.Log("Mover less tha dist 1");
#endif
            return true;
        }
        else
        {
#if ENABLE_DEBUGLOG
       //     Debug.Log("mover dist > 1");
#endif
            return false;
        }
    }
    bool Check_Z_Distance()
    {

        DistToTarget = Vector3.Distance(m_ieec.GetMyPos(), new Vector3(m_ieec.GetMyPos().x, m_ieec.GetMyPos().y, m_ieec.GetTargPos().z));
#if ENABLE_DEBUGLOG
        // Debug.Log("z offset "+DistToTarget);
        Debug.Log("z offset " + DistToTarget);
#endif
        if (DistToTarget < Max_Z_DistToPlayer)
        {
             
#if ENABLE_DEBUGLOG
            //    Debug.Log("mover dist < 2.2");
#endif
                return true;
             
        }
        else
        {
           
#if ENABLE_DEBUGLOG
               Debug.Log("mover dist > 2.2");
#endif
                return false;
            
        }

        #endregion
    }
    float Dist2Targ;
    bool CheckDistance(float argto)
    {
        Dist2Targ = Vector3.Distance(m_ieec.GetMyPos(), new Vector3(m_ieec.GetTargPos().x, m_ieec.GetMyPos().y, m_ieec.GetTargPos().z));
#if ENABLE_DEBUGLOG
        // Debug.Log("regular dist "+ DistToTarget);
#endif
        if (Dist2Targ < argto)
        {
#if ENABLE_DEBUGLOG
         Debug.Log("Mover less tha dist 1");
#endif
            return true;
        }
        else
        {
#if ENABLE_DEBUGLOG
       //     Debug.Log("mover dist > 1");
#endif
            return false;
        }
    }


}

