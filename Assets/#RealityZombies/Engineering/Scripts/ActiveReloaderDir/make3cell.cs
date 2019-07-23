using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class make3cell : MonoBehaviour {

    public GameObject MyactiveReloader;
    ////activereloadUIctrl scr;

    void MakeOne()
    {
        GameObject go = Instantiate(MyactiveReloader, this.transform.position, this.transform.rotation);
        go.GetComponent<activereloadUIctrl>().SetStartCellIndex(1);
    }
 
}
