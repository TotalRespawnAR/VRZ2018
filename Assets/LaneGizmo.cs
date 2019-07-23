using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneGizmo : MonoBehaviour {
   

    void OnDrawGizmos()
    {

        Gizmos.color =   Color.yellow;
     
        Gizmos.DrawLine( this.transform.GetChild(0).position, this.transform.GetChild(1).position);
    }

}
