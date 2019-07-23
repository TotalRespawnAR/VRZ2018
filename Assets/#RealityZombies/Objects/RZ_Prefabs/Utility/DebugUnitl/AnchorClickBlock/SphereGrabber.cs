using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereGrabber : MonoBehaviour {

    // Use this for initialization

    GameObject GrabbedObject;
    public void SetGrabbedObject(GameObject argGo) { }
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnCollisionEnter(Collision collision)
    {
        // Debug.Log("Detected Collision with " + collision.gameObject.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        //  Debug.Log("Detected Trigger enter with " + other.gameObject.name);
    }
}
