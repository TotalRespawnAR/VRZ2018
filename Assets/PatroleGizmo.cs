using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatroleGizmo : MonoBehaviour {

    public List<Transform> PatrolePoints = new List<Transform>();
    public Color LineColor;
    void OnDrawGizmos()
    {
    
        Gizmos.color = Color.blue;
        for (int x = 0; x < PatrolePoints.Count-1; x++) {
            Gizmos.DrawLine(PatrolePoints[x].position, PatrolePoints[x + 1].position);
        }
        Gizmos.DrawLine(PatrolePoints[PatrolePoints.Count - 1].position, PatrolePoints[0].position);
    }

    private void Awake()
    {
        foreach (Transform child in transform)
        {
            PatrolePoints.Add(child);
        }
    }
    // Use this for initialization
    void Start () {
   



    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
