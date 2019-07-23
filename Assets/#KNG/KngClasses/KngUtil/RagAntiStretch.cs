using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagAntiStretch : MonoBehaviour {
    Vector3 startpos;
    //Rigidbody myrb;
    //private void Awake()
    //{
    //    myrb = GetComponent<Rigidbody>();
    //}

    //public void SetKINE(bool argKineTF) { myrb.isKinematic=argKineTF; }
    //public void SetUseGravity(bool argTF) { myrb.useGravity = argTF; }

    void Start () {
        startpos = transform.localPosition;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        transform.localPosition = startpos;
    }
}
