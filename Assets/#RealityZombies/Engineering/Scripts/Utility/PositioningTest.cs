using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositioningTest : MonoBehaviour {

    public GameObject RefToInst;

    // Use this for initialization
    void Start () {


    }
        //  GameObject TempGridmap = Instantiate(gridMap, gridPlaceHGolder.transform.localPosition, Quaternion.identity) as GameObject;
        //   Vector3 wprldpos = gridPlaceHGolder.transform.TransformPoint()

    void Dopop_localPosition()
    {
        GameObject gridPlaceHGolder = GameObject.FindGameObjectWithTag("GridMaker");//devRoomManager.devPAthfinder;
        GameObject TempGridmap = Instantiate(RefToInst, gridPlaceHGolder.transform.localPosition, Quaternion.identity) as GameObject;
    }

    //should work .
    void Dopop_Position()
    {
        GameObject gridPlaceHGolder = GameObject.FindGameObjectWithTag("GridMaker");
        GameObject TempGridmap = Instantiate(RefToInst, gridPlaceHGolder.transform.position, Quaternion.identity) as GameObject;
        TempGridmap.transform.rotation = gridPlaceHGolder.transform.rotation;
    }
 
}
