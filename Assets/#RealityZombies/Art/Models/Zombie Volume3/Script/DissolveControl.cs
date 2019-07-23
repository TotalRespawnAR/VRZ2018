using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveControl : MonoBehaviour {

	public float dissolveSpeed = 1f;
	private bool bDissolve = false;
	private Material mat;
	private float dissolveAmouht = 0f;

	// Use this for initialization
	void Start () 
	{
		mat = GetComponentInChildren<SkinnedMeshRenderer> ().material;
	}
	
	// Update is called once per frame
	void Update () 
	{
		Debug.Log (bDissolve);
		if (bDissolve == true && dissolveAmouht < 1f) {
			dissolveAmouht += Time.deltaTime * dissolveSpeed;
			mat.SetFloat ("_dissolveSlider", dissolveAmouht);
		} else
			Debug.Log ("done");
	}

	public void ResetToDefault()
	{
		bDissolve = false;
		mat.SetFloat("_dissolveSlider",0f);
		dissolveAmouht = 0f;
	}

	public void StartDissolve()
	{
		if(bDissolve == false)
			bDissolve = true;

	}
}
