using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecGizmo : MonoBehaviour {

    void OnDrawGizmos()
    {

        Gizmos.color = Color.blue;

        Gizmos.DrawLine(this.transform.position, this.transform.GetChild(0).position);
    }
}
