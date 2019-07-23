using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPointsManager : MonoBehaviour {

    //gm.start() --> uses devroom to init and calls this
    public void INIT_PathDataForAll_DROPS(Transform _argAlfaBravoHotspot)
    {
        foreach (GameObject go in DropPoints)
        {
            go.GetComponent<PathData>().InitPathData(_argAlfaBravoHotspot.position);
        }
    }


    public List<GameObject> DropPoints;
    // Use this for initialization
    void Start()
    {
        DropPoints = new List<GameObject>();

 
        for (int i = 0; i < transform.childCount; i++)
        {
            DropPoints.Add(transform.GetChild(i).gameObject);
        }
    }

    public void DropZombie()
    {
        int RandGrave = Random.Range(0, transform.childCount);

        //hmmmmmm maybe i should do this un the morning ... who should call this . 
        // i dont want this class to use factory ... 
        // who should use this , it will only be used in pre game 
    }
}

