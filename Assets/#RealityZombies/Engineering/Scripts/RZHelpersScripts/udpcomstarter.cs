using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class udpcomstarter : MonoBehaviour {

    public GameObject CommWalki;

	// Use this for initialization
	void Start () {
        StartCoroutine(TurnOnComIn10Sec());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    IEnumerator TurnOnComIn10Sec() {
        yield return new WaitForSeconds(10f);

        CommWalki.SetActive(true);
    }
}
