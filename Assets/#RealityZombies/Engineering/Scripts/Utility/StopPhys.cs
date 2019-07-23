using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopPhys : MonoBehaviour {

    //private void OnTriggerEnter(Collider other)
    //{
    //    Logger.Debug("splat");
    //   GetComponent<Rigidbody>().isKinematic = true;
    //    GetComponent<Rigidbody>().useGravity = false;
    //}


        //needs to go to Zombie Collision  
    void OnCollisionEnter(Collision collision)
    {
        // Logger.Debug("splat");
        if (collision.gameObject.tag == "SpatialMesh")
        {
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<Rigidbody>().useGravity = false;
        }

        // Logger.Debug("splat");
        //foreach (ContactPoint contact in collision.contacts)
        //{
        //    Debug.DrawRay(contact.point, contact.normal, Color.white);
        //}


    }
}
