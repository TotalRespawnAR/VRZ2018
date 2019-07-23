using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnsMNGR : MonoBehaviour {

    public List<GameObject> MySpawns;
    public List<GameObject> MyPregameSpawns;
    public GameObject SingleSpawnForTesting;

    public int GetMregameSpawnCount() { return MyPregameSpawns.Count; }
    // Use this for initialization
    void Awake()
    {
        MySpawns = new List<GameObject>();
        MyPregameSpawns  = new List<GameObject>();
        for (int i = 0; i < transform.childCount-1; i++)
        {
            if (transform.GetChild(i).name.Contains("pregame"))
            {
                MyPregameSpawns.Add(transform.GetChild(i).gameObject);
            }
            else
            {
                MySpawns.Add(transform.GetChild(i).gameObject);
              }
        }

        SingleSpawnForTesting = transform.GetChild(transform.childCount - 1).gameObject;
    }

}
