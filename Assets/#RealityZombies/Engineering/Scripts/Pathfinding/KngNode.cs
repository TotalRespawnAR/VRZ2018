using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KngNode : MonoBehaviour {

    public bool IsOpen = true;
    public int Row;
    public int Col;
    public Vector3 pos;

    public List<KngNode> LateralNodes = new List<KngNode>();
    public List<KngNode> NextNodes=new List<KngNode>();
    public List<KngNode> PrevNodes = new List<KngNode>();

    // Use this for initialization
    void Start () {
        pos = this.gameObject.transform.position;
	}
    public int DistToCol;//only used to sord neighbors by closest to this
    public void OrderNextNodsAround(int argCol) {
        foreach (KngNode n in NextNodes) {
            n.DistToCol = Math.Abs(n.Col - argCol);
        }
        NextNodes.Sort((x, y) => x.DistToCol.CompareTo(Math.Abs(y.DistToCol) ));
    }
    public KngNode BestNextNodeForYouHere()
    {
        KngNode Bestnode = null;
        for (int neighborIndex = 0; neighborIndex <  NextNodes.Count; neighborIndex++)
        {
            if ( NextNodes[neighborIndex].IsOpen)
            {
                Bestnode = NextNodes[neighborIndex];
                break;
            }
        }
        //could be null, make sure ther IS a path
        return Bestnode;
    }

    public KngNode BestFinalWayPointForYou()
    {
        KngNode Bestnode = null;
        for (int neighborIndex = 0; neighborIndex < NextNodes.Count; neighborIndex++)
        {
            if (NextNodes[neighborIndex].IsOpen)
            {
                Bestnode = NextNodes[neighborIndex];
                break;
            }
        }
        //could be null, make sure ther IS a path
        return Bestnode;
    }

}
