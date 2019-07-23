using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimTestCompo : MonoBehaviour {


    public int AnimationNumber;
    public int SpeedLevel;
    public TriggersTestAnimator trigname;

    public Animator MyAnimator;

        void Start () {
		
	}
	
	 
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space)) {
            MyAnimator.SetInteger("SpeedLevel", SpeedLevel);
            MyAnimator.SetTrigger(trigname.ToString());
        }
	}
}
