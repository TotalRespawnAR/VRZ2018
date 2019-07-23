using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class callGamemanager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameEventsManager.Instance.Call_targExplode();

    }

    // Update is called once per frame
    void Update () {
		
	}
}
