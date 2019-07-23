using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieFreeLimbCleaner : MonoBehaviour {
    Renderer _ren;
	// Use this for initialization
	void Awake () {
        _ren = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void OnDestroy () {
        Logger.Debug("deleteing mat on" + this.gameObject.name);
        DestroyImmediate(_ren.material);
	}
}
