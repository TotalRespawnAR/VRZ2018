using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StemMainHandCollision : MonoBehaviour {
    private void OnTriggerEnter(Collider other)
    {
       // Logger.Debug("MAINtouch!! " + this.gameObject.name + "->OntrigEnt->" + other.gameObject.name);
        //StemKitManager.MAINHandTouchedThisThing(other.gameObject.tag);
    }
}
