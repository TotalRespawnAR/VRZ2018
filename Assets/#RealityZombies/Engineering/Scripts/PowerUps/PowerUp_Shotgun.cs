using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp_Shotgun : MonoBehaviour
{

    /// <summary>
    ///  this script is to control the power up of shotgun
    /// </summary>


    // the number of shots we get
    private int shots = 6;



    // Update is called once per frame
    private void Update()
    {


        // if no shots left
        if (shots <= 0)
        {
            //no more shotgun
            Destroy(transform.parent.gameObject, 0);

        }// end of no shots



    }// end of update


    // a function to add a shotgun
    public void ApplyPowerUp(int shots)
    {
        // displaya message
        Debug.Log("adding shotgun with :" + shots + " shots");
        // add shotgun
        //*************************** HERE

    }// end of shotgun function

}// end of shotgun pwoer up script