using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KngSpawnPointsCTRL : MonoBehaviour {

    [SerializeField]
    public List<List<Vector3>> PathsFromHere;
    public string GivenName;
    public string Birthname;
    public int ColumnNumber;
    public KngNode ImmediatNextNode;
    private void Start()
    {
        Birthname = this.gameObject.name;
    }
    public void SetPathsFromHere(List<List<Vector3>> argPathsFromHere, string argGiveenName) {
        GivenName = argGiveenName;
        PathsFromHere = argPathsFromHere;
    }
    public List<Vector3> GetPathsFromHere(int arg012)
    {
        //int pathsCount = PathsFromHere.Count;
        // Debug.Log(Birthname + " " + GivenName + " -> from " + arg012);
        // Debug.Log(" out of "+PathsFromHere.Count + "paths \n -------------");
        //if (arg012 >= PathsFromHere.Count)
        //    arg012--;

        arg012 %= PathsFromHere.Count;

        if (arg012 < 0)
            arg012 = 0;

        if (arg012 >= PathsFromHere.Count) arg012 = PathsFromHere.Count - 1;

        return PathsFromHere[arg012];
    }
 
}
