// @Author Nabil Lamriben ©2018
//#define ENABLE_LOGS

using System.Collections.Generic;
using UnityEngine;

public class ZombieCollisionCTRL : MonoBehaviour {

    /*
    
	#region dependencies
	Rigidbody _rb_onRootHips;
    Rigidbody[] _RBS;
    CharacterController _charCtrl;
    ZombieBehavior _ZBEH;
    ZombieAnimState _zanim;
	#endregion

	#region INITandListeners
	private void OnEnable()
    {
        _ZBEH = GetComponent<ZombieBehavior>();
        _ZBEH.OnZombieStateChanged += UpdateCollidersState;
    }

    private void OnDisable()
    {
        _ZBEH.OnZombieStateChanged -= UpdateCollidersState;
    }

    private void Awake()
    {
        _zanim = GetComponent<ZombieAnimState>();
        Transform hipboneForRagonly = _zanim.Get_HipBone();
        _charCtrl = GetComponent<CharacterController>();

        _RBS = GetComponentsInChildren<Rigidbody>();

        ShowAllMasses();
        if (hipboneForRagonly.gameObject.GetComponent<Rigidbody>() == null)
        {
            _rb_onRootHips = gameObject.GetComponent<Rigidbody>();

        }
        else
            _rb_onRootHips = _zanim.Get_HipBone().gameObject.GetComponent<Rigidbody>();


        //This is how i fixe the zombies walking into each other========================================================================================
        _rb_onRootHips.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        // _rb_onRootHips.isKinematic = false;
        //This is how i fixe the zombies walking into each other========================================================================================

        Make_Kinematic();

    }

    

    void ShowAllMasses()
    {
        if (_RBS == null || _RBS.Length == 0) return;

        foreach (Rigidbody _rb in _RBS)
        {
            _rb.mass = 10;
        }
    }
    #endregion

    #region PublicMethods
    
	public void MoveRigidBodyDown()
	{
        ClearRigidBodyConstraints();
        //rb.isKinematic = false;
        Make_Kinematic();
        gameObject.transform.Translate(Vector3.down * Time.deltaTime * 1.5f, Space.World);
        // foreach (Rigidbody rb in _RBS) {rb.MovePosition(transform.position + (Vector3.down * Time.deltaTime * 0.5f)); }
        //_rb_onRootHips.MovePosition(transform.position + (Vector3.down * Time.deltaTime * 0.5f));
    }

    void MakeALLRBS_Kinematic() {
        if (_RBS == null || _RBS.Length==0) return;
        foreach (Rigidbody _rb in _RBS) {
            _rb.isKinematic = true;
            _rb.useGravity = false;
            _rb.velocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;
        }
    }
    void MakeALLRBS_Not_Kinematic()
    {
        if (_RBS == null || _RBS.Length == 0) return;
        foreach (Rigidbody _rb in _RBS)
        {
            _rb.isKinematic = false;
            _rb.useGravity = true;
            _rb.velocity = Vector3.zero;
        }
    }
    public void Make_Kinematic()
    {
        MakeALLRBS_Kinematic();
    }

    public void Make_NOT_Kinematic()
    {
        MakeALLRBS_Not_Kinematic();
    }
    #endregion

    #region PrivateMethods
 

    void UpdateCollidersState(EBSTATE argNewZombieState)
    {
         switch (argNewZombieState)
        {
    //        case EnemyAnimatorState.IDLE:

    //        case EnemyAnimatorState.WALKING:
    //             break;
    //        case EnemyAnimatorState.CHASING:
    //             break;
    //        case EnemyAnimatorState.REACHING:
    //             break;
    //        case EnemyAnimatorState.DEAD:
    //           CharControllerEnable(false);
    //            DisableBoxColliders(this.transform);
    //           // FreezAllRigidBodyConstraints(); //<-----------------------could be the culprit . is it even needed
    //            break;
    //        case EnemyAnimatorState.PAUSED:
    //           CharControllerEnable(false);
    //            DisableBoxColliders(this.transform);
    //            FreezAllRigidBodyConstraints();
    //            break;
    //        case EnemyAnimatorState.MELTING:
				//ClearRigidBodyConstraints();
				//break;
 
        }

    }



    Transform DisableBoxColliders(Transform argTrans)
    {
        if (argTrans.gameObject.GetComponent<BoxCollider>())
            argTrans.gameObject.GetComponent<BoxCollider>().enabled = false;
        foreach (Transform c in argTrans)
        {
            var result = DisableBoxColliders(c);
            if (result != null)
            {
                return result;
            }
        }
        return null;
    }

    void CharControllerEnable(bool argOnOff)
    {
        if (_charCtrl == null) {
            Debug.Log( "!!!No char ctrl on "+ gameObject.name);
            return;
        }
        _charCtrl.enabled = argOnOff;
    }

    public void ClearRigidBodyConstraints() {
        if (_rb_onRootHips == null)
        {
            Debug.Log("!!!No rigidbody roothips on " + gameObject.name);
            return;
        }
        _rb_onRootHips.constraints = RigidbodyConstraints.None; }

    void FreezAllRigidBodyConstraints() {
        if (_rb_onRootHips != null)
        {
            _rb_onRootHips.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    void FreezSelectively()
    {
        _rb_onRootHips.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }
    #endregion

 

    public void DieRaggdole()
    {
        Make_NOT_Kinematic();
     }
    // Update is called once per frame
    
    //void Update()
    //{
 
    //    if (_ZBEH.CurZombieState == ZombieState.DROPPING) {
    //        if (_charCtrl.isGrounded)
    //        {
    //            _ZBEH.JUST_LandedFromDROPPING();
    //        }
    //    }
    //}

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.CompareTag("ZombieTag")) {

            if (hit.gameObject.GetComponent<IZeffects>().IsOnFire()) {
                Debug.Log(" Didthatwork?");
                _ZBEH.DoBurnMe();
            }
          //  Debug.Log("Zid" + _ZBEH.GetID() + "just hit " + hit.gameObject.GetComponent<ZombieEffects>());
        }
        
        //if (hit.transform.tag == "SomeTag")
        //{
        //    hit.transform.SendMessage("SomeFunction", SendMessageOptions.DontRequireReceiver);
        //}
    }


    //private void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log("hit " + other.gameObject.tag);
    //}
    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log("hitcoliz " + collision.gameObject.tag);

    //}
    */
}
