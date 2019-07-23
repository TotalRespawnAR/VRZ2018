using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolsMNGR : MonoBehaviour {
    //gm.start() --> uses devroom to init and calls this
    public void INIT_PathDataForAllLAnes()
    {
        foreach (GameObject go in MyPAtrols)
        {
            go.GetComponent<PathData>().InitPathData();
        }
    }


    public List<GameObject> MyPAtrols;
 
    // Use this for initialization
    void Awake()
    {
        MyPAtrols = new List<GameObject>();

        for (int i = 0; i < transform.childCount ; i++)
        {
            MyPAtrols.Add(transform.GetChild(i).gameObject);
        }

    }

    public GameObject GetA_PAtroleLoopObject(int argLoopIndex)
    {
        if (argLoopIndex >= MyPAtrols.Count) {
            argLoopIndex = 0;
        }
        return MyPAtrols[argLoopIndex];
    }

}
