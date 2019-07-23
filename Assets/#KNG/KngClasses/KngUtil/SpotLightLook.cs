using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotLightLook : MonoBehaviour {

    private void OnEnable()
    {
        GameEventsManager.On_SpotLightTarget += DoTrackTarget;
        GameEventsManager.On_SpotLightOnOff += DoTurnLightOnOfft;
    }

    private void OnDisable()
    {
        GameEventsManager.On_SpotLightTarget -= DoTrackTarget;
        GameEventsManager.On_SpotLightOnOff -= DoTurnLightOnOfft;
    }
    public Transform target;
    Transform curtarget;
    Transform defautTarget;
    public GameObject MyLightPannel;
    public int LightID;


    void DoTurnLightOnOfft(int id, bool argOnOff)
    {
        if (id != LightID) return;
        MyLightPannel.SetActive(argOnOff);
    }
    void DoTrackTarget(int id, Transform argTarg) {
        if (id != LightID) return;
        curtarget = argTarg;
    }
	// Use this for initialization
	void Start () {
        defautTarget = target;
        curtarget = defautTarget;
        MyLightPannel.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (curtarget != null)
            transform.LookAt(curtarget);
        else
            transform.LookAt(defautTarget);
    }
}
