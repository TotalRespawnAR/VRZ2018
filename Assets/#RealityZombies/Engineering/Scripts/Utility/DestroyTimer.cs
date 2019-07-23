// @Author Jeffrey M. Paquette ©2016

using UnityEngine;
using System.Collections;

public class DestroyTimer : MonoBehaviour {

    public float timeInSeconds = 10;
    // Use this for initialization
    public bool hasPArent = false;
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        timeInSeconds -= Time.deltaTime;
        if (timeInSeconds <= 0.0f)
        {
            if(hasPArent)
            Destroy(this.transform.parent.gameObject);
            else
                Destroy(this.gameObject);
        }
    }
}
