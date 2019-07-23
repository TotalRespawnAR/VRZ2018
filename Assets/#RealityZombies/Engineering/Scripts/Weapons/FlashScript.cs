using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashScript : MonoBehaviour {

	private Transform _transform;
	public new Transform transform { get { if (_transform == null) _transform = GetComponent<Transform>(); return _transform; } }

    public GameObject ParticleSys1;
    public GameObject ParticleSys2;
 
     

    ParticleSystem ps1;
    ParticleSystem ps2;
    void Start () {
        ps1 = ParticleSys1.GetComponent<ParticleSystem>();
        ps2 = ParticleSys2.GetComponent<ParticleSystem>();
    }

    void playing()
    {
        ps1.Play();
        ps2.Play();
    }
    ////bool go = false;
    public void Flash() {
        //go = true;
        playing();
    }

    // void Update()
    //{
    //    if (go) { playing(); }

    //}



}
