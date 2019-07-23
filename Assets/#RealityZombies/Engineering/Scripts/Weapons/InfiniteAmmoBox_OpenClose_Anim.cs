using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteAmmoBox_OpenClose_Anim : MonoBehaviour
{

    /// this script is to contain a reference to the ammo box opening
    /// 

    // the top obj we want to open
    public GameObject ammoBoxTop;
    // can this box be opened
    private bool canThisOpen = false; 


    private void Start()
    {
        // send a message
        Logger.Debug(transform.name + " is in the scene for opening");
        // get the ammo box child
        //ammoBoxTop = transform.parent.GetChild(4).gameObject;
        // set the box to possible for opening\
        canThisOpen = true;
        StartCoroutine(OpenBox(-5));
    }


    // a function to open the ammo box
    IEnumerator OpenBox(int direction)
    {        
        yield return new WaitForSeconds(0.05f);
        ammoBoxTop.transform.Rotate(direction, 0, 0);
        yield return new WaitForSeconds(0.05f);
        ammoBoxTop.transform.Rotate(direction, 0, 0);
        yield return new WaitForSeconds(0.05f);
        ammoBoxTop.transform.Rotate(direction, 0, 0);
        yield return new WaitForSeconds(0.05f);
        ammoBoxTop.transform.Rotate(direction, 0, 0);
        yield return new WaitForSeconds(0.05f);
        ammoBoxTop.transform.Rotate(direction, 0, 0);
        yield return new WaitForSeconds(0.05f);
        ammoBoxTop.transform.Rotate(direction, 0, 0);
        yield return new WaitForSeconds(0.05f);
        ammoBoxTop.transform.Rotate(direction, 0, 0);
        yield return new WaitForSeconds(0.05f);
        ammoBoxTop.transform.Rotate(direction, 0, 0);
        yield return new WaitForSeconds(0.05f);
        ammoBoxTop.transform.Rotate(direction, 0, 0);
        yield return new WaitForSeconds(0.05f);
        ammoBoxTop.transform.Rotate(direction, 0, 0);
        yield return new WaitForSeconds(0.05f);
        ammoBoxTop.transform.Rotate(direction, 0, 0);
        yield return new WaitForSeconds(0.05f);
        ammoBoxTop.transform.Rotate(direction, 0, 0);
    }// end of open box top function




    private void OnTriggerEnter(Collider trig)
    {
        // send message
        Logger.Debug(trig.name + " has entered the ammo box trigger");

        // if the box can be opened
        if(canThisOpen == true)
        {
            // call function to open/close
            StartCoroutine(OpenBox(-5));
            // cant open until closed
            canThisOpen = false;
        }
       
    }

    private void OnTriggerExit(Collider trig)
    {
        // send message
        Logger.Debug(trig.name + " has exited the ammo box trigger");

        // if the box canot be opened
        if (canThisOpen == false)
        {
            // call function to open/close
            StartCoroutine(OpenBox(5));
            // can open again
            canThisOpen = true;
        }
    }

}// end of infinite ammobox anim script
