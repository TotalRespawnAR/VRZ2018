#define DISSOLVETEST
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ZombieRagDoll : MonoBehaviour {
    /*

    public GameObject ZombieRootArmature;

    ARZRagdollState _stateRaggdoll = ARZRagdollState.ANIMATED;
    //How long do we blend when transitioning from ragdolled to animated
    float ragdollToMecanimBlendTime = 0.5f;
    float mecanimToGetUpTransitionTime = 0.05f;
    //A helper variable to store the time when we transitioned from ragdolled to blendToAnim state
    float ragdollingEndTime = -100;

    List<ZombieBodyBoneParts> zombieBodyParts = new List<ZombieBodyBoneParts>();
    Vector3 ragdolledHipPosition, ragdolledHeadPosition, ragdolledFeetPosition;




    #region dependencies
    ZombieBehavior _ZBEH;
    ZombieAnimState _zAnim;
    ZombieCollisionCTRL _zColli;
    #endregion

    #region Init
    private void Awake()
    {
        _ZBEH = GetComponent<ZombieBehavior>();
        _zAnim = GetComponent<ZombieAnimState>();
        _zColli = GetComponent<ZombieCollisionCTRL>();
    }
    void Start()
    {
        BuildBodyparts_BonesOnly();
        //no need to make not kinematic here
    }
    IEnumerator ragin3() {
        yield return new WaitForSeconds(3);
        TestRagDoll();

    }
    #endregion
    #region PrivateMethods
    //should this only be bones? or everything?
    void BuildBodyparts_EVERYTHING()
    {    //Find all the transforms in the character, assuming that this script is attached to the root
        Component[] components = GetComponentsInChildren(typeof(Transform));

        //For each of the transforms, create a BodyPart instance and store the transform 
        foreach (Component c in components)
        {
            ZombieBodyBoneParts bodyPart = new ZombieBodyBoneParts();
            bodyPart.transform = c as Transform;
            zombieBodyParts.Add(bodyPart);
            //Debug.Log(bodyPart.transform.name);
        }
    }
    //usig bones in armature only
    void BuildBodyparts_BonesOnly()
    {    
        
        
        Component[] components = ZombieRootArmature.GetComponentsInChildren(typeof(Transform));

        //For each of the transforms, create a BodyPart instance and store the transform 
        foreach (Component c in components)
        {
            ZombieBodyBoneParts bodyPart = new ZombieBodyBoneParts();
            bodyPart.transform = c as Transform;
            zombieBodyParts.Add(bodyPart);
            // Debug.Log(bodyPart.transform.name);
        }
    }

    #endregion

    public bool Ragdolled
    {
        get
        {
            return _stateRaggdoll != ARZRagdollState.ANIMATED;
        }
        set
        {
            if (value == true)
            {
                if (_stateRaggdoll == ARZRagdollState.ANIMATED)
                {
                    //Transition from animated to ragdolled
                    _zColli.Make_NOT_Kinematic(); //allow the ragdoll RigidBodies to react to the environment
                    _zAnim.AnimatorsDissable(); //disable animation
                    _stateRaggdoll = ARZRagdollState.RAGGEDOLLED;
                }
            }
            else
            {
                if (_stateRaggdoll == ARZRagdollState.RAGGEDOLLED)
                {
                    //Transition from ragdolled to animated through the blendToAnim state
                    _zColli.Make_Kinematic(); //disable gravity etc.
                    ragdollingEndTime = Time.time; //store the state change time
                    _zAnim.AnimatorsEnable(); //enable animation
                    _stateRaggdoll = ARZRagdollState.BLENDTOANIMATED;

                   // Store the ragdolled position for blending
                    foreach (ZombieBodyBoneParts b in zombieBodyParts)
                        {
                            b.cashedPos = b.transform.position - new Vector3(0f, 1.5f, 0f); ;
                            b.cashedRot = b.transform.rotation;
                        }

                    ////Remember some key positions
                    
                    ragdolledFeetPosition = GameManager.Instance.GetArenaLocationAsTransform().position - new Vector3(0f, 0.5f, 0f); // this.transform.position; //0.5f * (anim.GetBoneTransform(HumanBodyBones.LeftToes).position + anim.GetBoneTransform(HumanBodyBones.RightToes).position);
                    ragdolledHeadPosition = _zAnim.Get_HeadBone().position - new Vector3(0f, 1.5f, 0f);//anim.GetBoneTransform(HumanBodyBones.Head).position;
                    ragdolledHipPosition = _zAnim.Get_HipBone().position -new Vector3(0f, 1.5f, 0f); // anim.GetBoneTransform(HumanBodyBones.Hips).position;

                    //Initiate the get up animation
                    _zAnim.Do_GetUpRoutine();
                } //if (state==RagdollState.ragdolled)
            }   //if value==false	
        } //set
    }


    void LateUpdate()
    {

        _zAnim.Do_SetGetupBools_False();

        //Blending from ragdoll back to animated
        if (_stateRaggdoll == ARZRagdollState.BLENDTOANIMATED)
        {
            if (Time.time <= ragdollingEndTime + mecanimToGetUpTransitionTime)
            {
                //If we are waiting for Mecanim to start playing the get up animations, update the root of the mecanim
                //character to the best match with the ragdoll
                Vector3 animatedToRagdolled = ragdolledHipPosition - _zAnim.Get_HipBone().position - new Vector3(0f, 1.5f, 0f); ;
                Vector3 newRootPosition = transform.position + animatedToRagdolled - new Vector3(0f, 1.5f, 0f); ;

                //Now cast a ray from the computed position downwards and find the highest hit that does not belong to the character 
                RaycastHit[] hits = Physics.RaycastAll(new Ray(newRootPosition, Vector3.down));
                newRootPosition.y = 0;
                foreach (RaycastHit hit in hits)
                {
                    if (!hit.transform.IsChildOf(transform))
                    {
                        newRootPosition.y = Mathf.Max(newRootPosition.y, hit.point.y);
                    }
                }
                transform.position = newRootPosition - new Vector3(0f, 1.5f, 0f); ;

                //Get body orientation in ground plane for both the ragdolled pose and the animated get up pose
                Vector3 ragdolledDirection = ragdolledHeadPosition - ragdolledFeetPosition;
                ragdolledDirection.y = 0;

                Vector3 meanFeetPosition = _zAnim.Get_MeanFeet() - new Vector3(0f,1.5f,0f);
                Vector3 animatedDirection = _zAnim.Get_Anim_Direction();
                animatedDirection.y = 0;

                //Try to match the rotations. Note that we can only rotate around Y axis, as the animated characted must stay upright,
                //hence setting the y components of the vectors to zero. 
                transform.rotation *= Quaternion.FromToRotation(animatedDirection.normalized, ragdolledDirection.normalized);
            }
            //compute the ragdoll blend amount in the range 0...1
            float ragdollBlendAmount = 1.0f - (Time.time - ragdollingEndTime - mecanimToGetUpTransitionTime) / ragdollToMecanimBlendTime;
            ragdollBlendAmount = Mathf.Clamp01(ragdollBlendAmount);

            //In LateUpdate(), Mecanim has already updated the body pose according to the animations. 
            //To enable smooth transitioning from a ragdoll to animation, we lerp the position of the hips 
            //and slerp all the rotations towards the ones stored when ending the ragdolling
            foreach (ZombieBodyBoneParts b in zombieBodyParts)
            {
                if (b.transform != transform)
                { //this if is to prevent us from modifying the root of the character, only the actual body parts
                  //position is only interpolated for the hips
                    if (b.transform == _zAnim.Get_HipBone())
                        b.transform.position = Vector3.Lerp(b.transform.position, b.cashedPos, ragdollBlendAmount);
                    //rotation is interpolated for all body parts
                    b.transform.rotation = Quaternion.Slerp(b.transform.rotation, b.cashedRot, ragdollBlendAmount);
                }
            }

            //if the ragdoll blend amount has decreased to zero, move to animated state
            if (ragdollBlendAmount == 0)
            {
                _stateRaggdoll = ARZRagdollState.ANIMATED;
                return;
            }
        }
    }


    //IMPACT
    float impactEndTime = 0;
    Rigidbody impactTarget = null;
    Vector3 impact;

    IEnumerator UnRagin5sec()
    {
        yield return new WaitForSeconds(5);

        //if(_ZBEH.CurZombieState != EBSTATE.DEAD && _ZBEH.CurZombieState != EBSTATE.MELTING)
          //  this.Ragdolled = false;
    }

    void Update()
    {
        //if left mouse button clicked
        //if (Input.GetMouseButtonDown(0))
        //{
        //    if (this.Ragdolled == true) return;

        //    //Get a ray going from the camera through the mouse cursor
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //    //check if the ray hits a physic collider
        //    RaycastHit hit; //a local variable that will receive the hit info from the Raycast call below
        //    if (Physics.Raycast(ray, out hit))
        //    {
        //        //check if the raycast target has a rigid body (belongs to the ragdoll)
        //        if (hit.rigidbody != null)
        //        {
        //           this.Ragdolled = true;

        //            //set the impact target to whatever the ray hit
        //            impactTarget = hit.rigidbody;
        //            //Debug.Log(_ZBEH.GetID() + " waz pushed");
        //            //impact direction also according to the ray
        //            impact = ray.direction * 0.2f;

        //            //the impact will be reapplied for the next 250ms
        //            //to make the connected objects follow even though the simulated body joints
        //            //might stretch
        //            impactEndTime = Time.time + 0.25f;
        //            StartCoroutine(UnRagin5sec());

        //        }
        //    }
        //}

   

        //Pressing space makes the character get up, assuming that the character root has
        //a RagdollHelper script
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    Ragdolled = false;
        //}

        //Check if we need to apply an impact
        if (Time.time < impactEndTime)
        {
            impactTarget.AddForce(impact, ForceMode.VelocityChange);
        }
    }

    public void TestRagDoll() {

        this.Ragdolled = true;

        //set the impact target to whatever the ray hit
        //Debug.Log(_ZBEH.GetID() + " waz pushed");
        //impact direction also according to the ray

        //the impact will be reapplied for the next 250ms
        //to make the connected objects follow even though the simulated body joints
        //might stretch

    }
    public void PUSH_DOLL( Bullet argbullet, Rigidbody rbOfHit) {
           if (this.Ragdolled == true) return;

            //Get a ray going from the camera through the mouse cursor
           // Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //check if the ray hits a physic collider
           
                //check if the raycast target has a rigid body (belongs to the ragdoll)
                
                    this.Ragdolled = true;

                    //set the impact target to whatever the ray hit
                    impactTarget = rbOfHit;
                    //Debug.Log(_ZBEH.GetID() + " waz pushed");
                    //impact direction also according to the ray
                    impact = argbullet.hitInfo.normal * -8.2f;

                    //the impact will be reapplied for the next 250ms
                    //to make the connected objects follow even though the simulated body joints
                    //might stretch
                    impactEndTime = Time.time + 0.2f;
                    StartCoroutine(UnRagin5sec());
    }
    public void Grenae_DOLL(Transform argFromGrenade)
    {


        if (this.Ragdolled == true) return;

        Transform HipTrans = _zAnim.Get_HipBone();

        Rigidbody rbOfHit = HipTrans.gameObject.GetComponent<Rigidbody>();

 

        this.Ragdolled = true;

        //set the impact target to whatever the ray hit
        impactTarget = rbOfHit;

        Vector3 Directoin = (argFromGrenade.position - HipTrans.position).normalized;

        impact = Directoin * -10f;

        //the impact will be reapplied for the next 250ms
        //to make the connected objects follow even though the simulated body joints
        //might stretch
        impactEndTime = Time.time + 0.2f;
       // StartCoroutine(UnRagin5sec());
    }
    */
}
