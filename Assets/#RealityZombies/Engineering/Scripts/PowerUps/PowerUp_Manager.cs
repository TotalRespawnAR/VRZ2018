using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Noah
public class PowerUp_Manager : MonoBehaviour
{

    ///// this script will control all the power ups
    ///// 

    //// 0 = 2x = PowerUp_DoublePoints
    //// 1 = Time Slow  = PowerUp_SlowTime
    //// 2 = Shotgun = PowerUp_Shotgun
    //public int powerUpNum = -1;

    //// move forward timer
    //private float moveForwardTimer;

    //// the player/target
    //private GameObject player;
    //private Transform lookAt;

    //// how fast the powerup moves to the player
    //private int powerUpSpeed = 2;

    //// when this obj starts in scene
    //private void Start()
    //{
    //    // remove this within 2 minutes
    //    Destroy(gameObject, 120);
    //    // find main cam/player
    //    player = GameObject.Find("Main Camera");
    //    // if still no cam
    //    if(player == null)
    //    {
    //        player = GameObject.Find("HLCAM_Player");
    //    }// end of no main camera for player

    //    // look at it and then reset
    //    lookAt = player.transform;
    //    transform.LookAt(lookAt);
    //    transform.LookAt(null);

    //    // set the timer
    //    moveForwardTimer = Time.time + 60;


    //    // for testing
    //    //powerUpNum = 1;


    //    // for each child this obj has
    //    foreach (Transform child in transform)
    //    {
    //        // if it's not the right power up, deactivate it
    //        if (!child.name.Contains(powerUpNum.ToString()))
    //        {
    //            // turn it off
    //            child.gameObject.SetActive(false);
    //        }// end of no the right power up

    //    }// end of for each child in power ups

    //}// end of start

    //// update
    //private void Update()
    //{
    //    // if theres time to move forward
    //    if (Time.time < moveForwardTimer)
    //    {
    //        transform.Translate(Vector3.forward * powerUpSpeed * Time.deltaTime);
    //    }// end of time to move forward

    //}// end of Update

    //// when we collide with something
    //private void OnTriggerEnter(Collider trig)
    //{
    //    //print("i trigged with layer: " + trig.gameObject.layer + " : " + trig.transform.name);

    //    // if the layer was player
    //    if (trig.gameObject.tag == "PowerUp_Reciever") // 11 = player layer
    //    {
    //        // for each child this obj has
    //        foreach (Transform child in transform)
    //        {

    //            // if it's on
    //            if (child.gameObject.activeSelf == true)
    //            {
    //                // find which power up

    //                // if double points
    //                if (powerUpNum == 0 && child.gameObject.activeSelf == true)
    //                {
    //                    // get it boi
    //                    child.GetComponent<PowerUp_DoublePoints>().ApplyPowerUp(20);
    //                    // turn of this powerup from being activated again
    //                    powerUpNum = -1;
    //                    // turn of the child still active (visual choice)
    //                    child.gameObject.SetActive(false);
    //                }// end of 2x points

    //                // if slow time
    //                if (powerUpNum == 1 && child.gameObject.activeSelf == true)
    //                {
    //                    // get it boi
    //                    child.GetComponent<PowerUp_SlowTime>().ApplyPowerUp(12);
    //                    // turn of this powerup from being activated again
    //                    powerUpNum = -1;                        
    //                    // for each child this obj has
    //                    foreach (Transform kid in child.transform)
    //                    {
    //                        // turn of the child still active (visual choice)
    //                        //child.gameObject.SetActive(false);
    //                        GetComponent<MeshRenderer>().enabled = false;
    //                    }
    //                }// end of slow time

    //                // if shotgun
    //                if (powerUpNum == 2 && child.gameObject.activeSelf == true)
    //                {
    //                    // get it boi
    //                    child.GetComponent<PowerUp_Shotgun>().ApplyPowerUp(6);
    //                    // turn of this powerup from being activated again
    //                    powerUpNum = -1;
    //                    // turn of the child still active (visual choice)
    //                    child.gameObject.SetActive(false);
    //                }// end of shotgun

    //            }// end of if it is active




    //        }// end of for each child loop

    //        // turn off this collider
    //        GetComponent<BoxCollider>().enabled = false;

    //    }// end of player layer        

    //}// end of on trigger enter

}// end of power ups manager script
