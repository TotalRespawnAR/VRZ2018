using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StemTutoController : MonoBehaviour {
    public GameObject ObjTriger;
    public GameObject ObjReload;
    public GameObject ObjJoy;
    public TextMesh MAinText;

    public void UpdateMainTExt(string argTXT) {
        MAinText.text = "";
        MAinText.text = argTXT;
    }
    public void ShowObjTrigger() {
        ObjTriger.SetActive(true);
        ObjReload.SetActive(false);
        ObjJoy.SetActive(false);
    }
    public void ShowObjReload() {
        ObjTriger.SetActive(false);
        ObjReload.SetActive(true);
        ObjJoy.SetActive(false);
    }
    public void ShowObjJoy()
    {
        ObjTriger.SetActive(false);
        ObjReload.SetActive(false);
        ObjJoy.SetActive(true);
    }
    public void HideAll() {
        ObjTriger.SetActive(false);
        ObjReload.SetActive(false);
        ObjJoy.SetActive(false);
    }
    // Use this for initialization
    //void Start () {
    //    HideAll();
    //}
	
	// Update is called once per frame
	//void Update () {
		
	//}
}
