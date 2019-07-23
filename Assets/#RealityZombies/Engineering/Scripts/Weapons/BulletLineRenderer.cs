using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLineRenderer : MonoBehaviour {


    // a ref to this clone's line renderer
    private LineRenderer lR;
    // should we destroy these game objects // if we dont we will turn it inactive
    private bool destroyLR = true;
    // spawn timer until innactive
    private float activeTime = 0.45f;

    // a start function for when the gameObject is spawned
    private void Start()
    {
        // call set inactive
        StartCoroutine(SetInactive());

    }// end of start function


    // a function to change the color of this line renderer object (called to from "Bullet.cs")
    public void ChangeColor(Color newStartColor, Color newEndColor)
    {
        // ref the line renderer
        lR = GetComponent<LineRenderer>();
        // change the color based on our new color
        lR.startColor = newStartColor;
        lR.endColor = newEndColor;

    }// end of change color function


    // the turn this off function
    IEnumerator SetInactive()
    {
        // wait for X seconds
        yield return new WaitForSeconds(activeTime);
        // if these are meant to be destroyed
        if(destroyLR == true)
        {
            // destroy it from the game
            Destroy(gameObject);
        }// end of is meant to be destroyed
        else
        // if it is not meant to be destroyed
        {
            // turn it off
            gameObject.SetActive(false);
        }// end of not meant to be destroyed
        
    }// end of set inactive


}// end of bullet line renderer script
