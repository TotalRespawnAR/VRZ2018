using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRaggDollComponent : MonoBehaviour , IEnemyRagdolComp
{

     
    //for getting state and call_Update_state
    IEnemyEntityComp m_Ieec;
    FixedJoint _preyOwnHeadHingJoint;
    public Collider[] BoneTransWithColliders;
    public List<Rigidbody> RigidBodies = new List<Rigidbody>();
    public List<RagAntiStretch> MyAntiStretches = new List<RagAntiStretch>();

    bool _isDead;

    #region MonoMethods
    void Awake()
    {
        BoneTransWithColliders = GetComponentsInChildren<Collider>(); //Watch out , the first collider is the charcontoller, can keep ref here but dont use it here

    }
    void OnEnable()
    {

    }
    void OnDisable()
    {

    }

    void Start()
    {

        foreach (Collider c in BoneTransWithColliders)
        {

            RagAntiStretch mar = c.gameObject.GetComponent<RagAntiStretch>();
            if (mar != null)
            {
                MyAntiStretches.Add(mar);
                mar.enabled = false;
            }

            Rigidbody rb = c.gameObject.GetComponent<Rigidbody>();
            if (rb!=null){
                RigidBodies.Add(rb);
            }
        }
        m_Ieec = GetComponent<IEnemyEntityComp>();

        ToggleAllKinematics(true);//just turn on kinemantics on start 

    }
    #endregion



    //void OnEnemyBulletWoond(object o, ArgsBulletWoond e)
    //{

    //}
    //the bool grounded is no longer used to trigger event -> ToggleAllKinematics(blah); 
    //that bool will be consumed by enemybeh to decide when to change states to ragged or ragged death. timers can be used that way enemy will not tart anim as soon as touched ground
    //Event Handeler ONLY! EnemyBehavior will update this


    //set true for animator to take controle 
    //must be true to let animator be able to controle the pos of each rigidbody , to allow ragdoll just turn kinematics offrigid bodies and disable animator
    //Kinematic off for rag to work
    //animator off 
    public void ToggleAllKinematics(bool argOnOff)
    {
        //Debug.Log("kinematicsOnOff " + argOnOff.ToString());

        foreach (Rigidbody rb in RigidBodies)
        {
            rb.isKinematic = argOnOff;
        }
    }
  
    public void EnablePreprocess(bool argonoff) {
        foreach (Rigidbody rb in RigidBodies)
        {
            rb.mass = 1;
            CharacterJoint cj = rb.gameObject.GetComponent<CharacterJoint>();
            if (cj)
            {
                cj.enablePreprocessing = argonoff;
            }
        }
    }

    public void EnableColliders(bool argonoff)
    {
        foreach (Collider colider in BoneTransWithColliders)
        {
            if (colider.gameObject.CompareTag("ZombieTag")) {
                continue;
            }


            colider.enabled = argonoff;
            
        }
    }

    public void  EnableAntiStratch(bool argtf) {
        foreach (RagAntiStretch ra in MyAntiStretches) {
            if (!ra.gameObject.name.Contains("ip"))
                ra.enabled = argtf;
        }
    }

    bool hasAlreadyAbbedStupiscriptToAllOrNotHip;

    public void PreyAddHeadHinjoint()
    {
        addstupidscript();
        preyCreateHeadHinge();
    }

    void addstupidscript() {
        foreach (Rigidbody rb in RigidBodies)
        {
            if(!rb.gameObject.name.Contains("ip"))
            rb.gameObject.AddComponent<RagAntiStretch>();
        }
        hasAlreadyAbbedStupiscriptToAllOrNotHip = true;
    }
   
    public void PreyHOOKUP(Rigidbody rghook)
    {
        _preyOwnHeadHingJoint.connectedBody = rghook;
    }

    void preyCreateHeadHinge() {

        PreyDestroyOwnActiveHeadHinge();
       
        _preyOwnHeadHingJoint = m_Ieec.Get_MyHEADtrans().gameObject.AddComponent<FixedJoint>();
        _preyOwnHeadHingJoint.anchor = new Vector3(0.1f, 0f, 0);
        _preyOwnHeadHingJoint.axis = new Vector3(0, 0, 0) ;// new Vector3(1f, 1f, 1f);
       // JointSpring hingeSpring = _preyOwnHeadHingJoint.;
        //hingeSpring.spring = 1;
        //hingeSpring.damper = 30;
        //hingeSpring.targetPosition = 0f;

    }

   public void PreyDestroyOwnActiveHeadHinge() {
        if (_preyOwnHeadHingJoint != null)
        {
            Destroy(_preyOwnHeadHingJoint);
        }
    }

        //void AddHingeJointToLimb(Rigidbody fromhookRB, Transform LimbToHinge)
        //{

        //    foreach (Rigidbody rb in RigidBodies)
        //    {
        //        rb.mass = 1;
        //        CharacterJoint cj = rb.gameObject.GetComponent<CharacterJoint>();
        //        if (cj)
        //        {
        //            cj.enablePreprocessing = true;
        //        }
        //    }

        //    //if (HeandTrans.GetComponent<HingeJoint>() != null)
        //    //{
        //    //    Destroy(HeandTrans.GetComponent<HingeJoint>());
        //    //}
        //    HingeJoint hj = LimbToHinge.gameObject.AddComponent<HingeJoint>();
        //    hj.anchor = new Vector3(0, 0f, 0);
        //    hj.axis = new Vector3(1f, 0f, 1f);
        //    JointSpring hingeSpring = hj.spring;
        //    hingeSpring.spring = 1;
        //    hingeSpring.damper = 30;
        //    hingeSpring.targetPosition = 0f;
        //    //  hj.useSpring = true;

        //    hj.connectedBody = fromhookRB;
        //}

        //public void GrabMe(Rigidbody fromhookRB, Transform arglimbtran)
        //{
        //    AddHingeJointToLimb(fromhookRB, arglimbtran);
        //}

        //public void LetMeGoo()
        //{
        //    RemoveHEadJoint();
        //}
        //void RemoveHEadJoint()
        //{
        //    foreach (Rigidbody rb in RigidBodies)
        //    {
        //        rb.mass = 1;
        //        CharacterJoint cj = rb.gameObject.GetComponent<CharacterJoint>();
        //        if (cj)
        //        {
        //            cj.enablePreprocessing = false;
        //        }
        //    }

        //    if (HeandTrans.GetComponent<HingeJoint>() != null)
        //    {
        //        Destroy(HeandTrans.GetComponent<HingeJoint>());
        //    }

        //    _IenemyBehavior.Set_STATE(ZombieState.IDLE);
        //}
    }
