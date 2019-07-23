using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp_DoublePoints : MonoBehaviour {

    /// <summary>
    ///  this script is to control the power up of doble points
    /// </summary>

    
    // the offset of time based on how much we double
    private float waitTime = 0;

    // Use this for initialization
    private void Start()
    {
        // displaya message
        //Debug.Log("press 9 to slow time");
        Destroy(transform.parent.gameObject, 30);

    }// end of start

    // Update is called once per frame
    private void Update()
    {


        // if  second real time has passed
        if (Time.time > waitTime)
        {
            // displaya message
            Debug.Log("double points is no longer active");
            // turn points to normal
            ScoreManager.doublePointsIsOn = 1;
        }// end of if 1 sec has passed



    }// end of update


    // a function to add dobule points
    public void ApplyPowerUp(int seconds)
    {
        // displaya message
        Debug.Log("adding double points for :" + seconds + " seconds");
        // convert the wait time
        waitTime = Time.time + seconds;
        // change to double points
        ScoreManager.doublePointsIsOn = 2;

    }// end of 2x points function

}// end of double pooints script
