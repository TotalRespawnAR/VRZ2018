using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGizmo : MonoBehaviour {

    void OnDrawGizmos()
    {

        Gizmos.color = Color.red;

        Gizmos.DrawLine(this.transform.position, this.transform.GetChild(0).position);
    }
}
