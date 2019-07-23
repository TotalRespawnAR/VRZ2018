using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testttttt : MonoBehaviour {

    public List<Rigidbody> RigidBodies = new List<Rigidbody>();
    public Collider[] BoneTransWithColliders;
    void Awake()
    {
        BoneTransWithColliders = GetComponentsInChildren<Collider>(); //Watch out , the first collider is the charcontoller, can keep ref here but dont use it here
        foreach (Collider c in BoneTransWithColliders)
        {

            Rigidbody rb = c.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                RigidBodies.Add(rb);
            }
        }
    }
    void Start () {
        addstupidscript();

    }

    void addstupidscript()
    {
        foreach (Rigidbody rb in RigidBodies)
        {
          //  if (!rb.gameObject.name.Contains("ip"))
                rb.gameObject.AddComponent<RagAntiStretch>();
        }
         
    }


    // Update is called once per frame
    void Update () {
        //Debug.Log("YIYIYIIY");
    }
}
