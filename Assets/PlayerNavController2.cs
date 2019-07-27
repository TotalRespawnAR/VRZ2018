using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
//using UnityStandardAssets.Characters.ThirdPerson;
public class PlayerNavController2 : MonoBehaviour
{

    public NavMeshAgent agent;
    public Camera cam;
    public MyThirdPersonCharacter character;
    public List<Rigidbody> RigidBodies = new List<Rigidbody>();
    public GameObject RootHipObj;
    public Transform KylesTrans;


    public bool DoSetup;
    private void Awake()
    {
        RigidBodies = RootHipObj.GetComponentsInChildren<Rigidbody>().ToList();
        KylesTrans = GameObject.FindGameObjectWithTag("Target").transform;
    }

    private void Start()
    {
        //rotation is done by animated character
        agent.updateRotation = false;
        //if (DoSetup)
        //{
        //    ToggleAllKinematics(false);
        //    ToggleAllGravity(false);
        //    ToggleAllRiGidBOdies(false);
        //}

    }

    bool makeRag = false;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace)) { makeRag = true; ToggleAllRiGidBOdies(true); ToggleAllKinematics(false); ToggleAllGravity(true); character.m_Animator.enabled = false; character.SetRag(makeRag); }


        if (makeRag)
        {
            print("makerag");
            character.Move(Vector3.zero, false, false);
            agent.isStopped = true;
            return;
        }

        agent.SetDestination(KylesTrans.position);

        if (Input.GetMouseButtonDown(0))
        {
            print("cliik");
            //take mouse pos , and makes a ray in the direction of the cam out
            //store the ray 
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            print("ray at " + ray.origin.ToString());
            //before shooting the ray, need object to hold the hit 

            RaycastHit hit;
            //shout out the ray we want to shoot 
            if (Physics.Raycast(ray, out hit))
            {

                agent.SetDestination(hit.point);
                print("hit at " + hit.point.ToString());

            }

        }
        if (agent.remainingDistance > agent.stoppingDistance && !makeRag)
        {

            character.Move(agent.desiredVelocity, false, false);
        }
        else //reached destination
        {
            character.Move(Vector3.zero, false, false);
        }


    }

    void ToggleAllKinematics(bool argbool)
    {

        foreach (Rigidbody rb in RigidBodies)
        {
            rb.isKinematic = argbool;
        }
    }

    void ToggleAllGravity(bool argbool)
    {

        foreach (Rigidbody rb in RigidBodies)
        {
            rb.useGravity = argbool;
        }
    }


    void ToggleAllRiGidBOdies(bool argbool)
    {

        foreach (Rigidbody rb in RigidBodies)
        {
            rb.gameObject.SetActive(argbool);
        }
    }
}
