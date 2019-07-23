using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotspotHider : MonoBehaviour {

    public GameObject thecube;
    MeshRenderer Mr;
	void Start () {
        Mr = thecube.gameObject.GetComponent<MeshRenderer>();

        if (GameSettings.Instance!= null)
        {

            if (GameSettings.Instance.IsTestModeON)
            {
                Mr.enabled = true;
            }
            else {
                Mr.enabled = false;
            }
        }
	}
 
}
