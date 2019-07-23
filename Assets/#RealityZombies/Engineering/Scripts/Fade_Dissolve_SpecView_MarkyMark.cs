using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade_Dissolve_SpecView_MarkyMark : MonoBehaviour {

    // a script to fade between dissolve and not dissolve
    // Noah 05/14/18

    // this obj renderer
    public Renderer rend;
    // start
    private void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material.shader = Shader.Find("RM/Holographic/LB Transparent CutOut");
    }// end of start()
    // Update
    private void Update()
    {
        float dissolve = Mathf.PingPong((Time.time*0.75f), 1.0F);        
        rend.material.SetFloat("_Dis", dissolve);

      //    [MaterialToggle] Dissolve("Dissolve Effect", Float) = 0
      //_ColorEm("Dissolve Color", Color) = (1,1,1,1)
      //_DissolveTex("Dissolve Texture (RGB)", 2D) = "white" {}
      //_Dis("Dissolve", Range(0.0, 1.0)) = 0
      //_EdjeSize("Edje Size", Range(0.0, 1.0)) = 0.02
      //_Emission("Emission", Range(0.0, 50)) = 0.4
    }// end of Update


	
}// end of fade dissolve script