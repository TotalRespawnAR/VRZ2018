// @Author Jeffrey M. Paquette ©2016

using UnityEngine;
using System.Collections;


public class Ammo : MonoBehaviour {

    [Tooltip("How many mags of ammo does this object hold? (0 for infinite)")]
    public int count = 1;

    [Tooltip("Is this ammo box unlimited?")]
    public bool unlimited = false;

    //[Tooltip("If unlimited, the amount of time it remains unlimited.")]
    //public float unlimitedTime;

    ////private float oneMinLeft;


    ////private bool countdownStarted = false;

 

    public void Take()
    {
        if (unlimited)
            return;

        count--;
        if (count < 1)
        {
            Destroy(gameObject);
        }
    }

    public void MakeLimited()
    {
        unlimited = false;
    }


 
}
