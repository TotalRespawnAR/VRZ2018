using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeStrikerReceiver : MonoBehaviour {
    private void OnEnable()
    {
        GameEventsManager.StrikerShoot_Handeler += Shoot;
        GameEventsManager.StrikerDryFire_Handeler += DrySound;
        GameEventsManager.StrikerReload_Handeler += ReloadSound;
    }
    private void OnDisable()
    {
        GameEventsManager.StrikerShoot_Handeler -= Shoot;
        GameEventsManager.StrikerDryFire_Handeler -= DrySound;
        GameEventsManager.StrikerReload_Handeler -= ReloadSound;
    }

    void Shoot() { Debug.Log("Bang"); }
    void DrySound() { Debug.Log("dry"); }
    void ReloadSound() { Debug.Log("reload"); }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
