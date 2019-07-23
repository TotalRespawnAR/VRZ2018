using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravesManager : MonoBehaviour {

    //gm.start() --> uses devroom to init and calls this
    public void INIT_PathDataForAll_GRAVES(Transform _argAlfaBravoHotspot)
    {
        foreach (GameObject go in Graves)
        {
            go.GetComponent<PathData>().InitPathData(_argAlfaBravoHotspot.position);
        }
    }

    public List<GameObject> Graves;
	// Use this for initialization
	void Start () {
        Graves = new List<GameObject>();

        for (int i = 0; i < transform.childCount; i++) {
            Graves.Add(transform.GetChild(i).gameObject);
        }
    }

   public  Transform Get_GraveX(int x) {
        if (x >= Graves.Count) {
            x = 0;
        }

        return Graves[x].transform;
    }

}
