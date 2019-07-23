using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor.Audio;
//Noah
public class PowerUp_SlowTime : MonoBehaviour {

    /// <summary>
    ///  this script is to control the power up of slowing time
    /// </summary>

 //   // a var to track % time slow
 //   private float timeSlow  = 0.2f;
 //   // the offset of time based on how much we slowed
 //   private float waitTime = 0;
 //   // audio clips
 //   public AudioClip start;
 //   public AudioClip end;
 //   // audioSource
 //   private AudioSource aS;
    
	//// Use this for initialization
	//private void Start () {
 //       // displaya message
 //       //Debug.Log("press 9 to slow time");
 //       Destroy(transform.parent.gameObject, 20);
 //       // get aduio source
 //       aS = GetComponent<AudioSource>();

 //   }// end of start
	
	//// Update is called once per frame
	//private void Update () {            

 //       // if  second real time has passed
 //       if(Time.time > waitTime)
 //       {          
 //           // turn time to normal
 //           Time.timeScale = 1.0f;
 //       }// end of if 1 sec has passed

 //   }// end of update


 //   // a function to slow time
 //   public void ApplyPowerUp(int seconds)
 //   {
 //       // displaya message
 //       Debug.Log("slowing time to :" + seconds + " seconds");
 //       // play sound       
 //       aS.PlayOneShot(start);  
 //       // convert the wait time
 //       waitTime = Time.time + (timeSlow * seconds);       
 //       // slow time to 20%
 //       Time.timeScale = timeSlow;


 //   }// end of slow time function

 //   // when it's destroyed
 //   private void OnDestroy()
 //   {
 //       Debug.Log("*& kill this obj and play sound**"); // this wont work because slow time is not 20 seconds its 12... use a timer then DUH noah
 //       aS.PlayOneShot(end);
 //   }// end of when destroyed

}// end of slow time script
