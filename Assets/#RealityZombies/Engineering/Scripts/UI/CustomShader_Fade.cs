using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomShader_Fade : MonoBehaviour {


    /// <summary>
    ///  this script is meant to fade on custom shaders by storing original color, and changing until black
    /// </summary>
    /// 

    // a var to store current color of obj
    private Color mySavedColor;
    // the color black
    private Color black;
    // this obj renderer
    private Renderer rend;
    // color to ref/change
    private Color changeColor;
    // timers for changing fades
    private float lifeTimer;
    private float timeAlive = 1;
    // time it takes to fade
    private float fadeTime = 0.01f;

    // start
    private void Start()
    {
        // get rend
        rend = GetComponent<Renderer>();
       // rend.material.shader = Shader.Find("_Jimmy/Hologram03");
        // declare colors
        black = new Color(0, 0, 0, 1);
        mySavedColor = rend.material.color;
        changeColor = mySavedColor;

        // start it boi
        changeColor = black;
        lifeTimer = Time.time;      

    }// end of Start()

    // update
    private void Update()
    {
        if(Time.time < lifeTimer + 0.2f)
        {
            changeColor = black;
        }

        if (Time.time > lifeTimer + 0.2f && Time.time < lifeTimer + 1.0f)
        {
            FadeToColor();
        }

        if (Time.time > lifeTimer + timeAlive)
        {
            FadeToBlack();
        }

        // assign color
        rend.material.color = changeColor;
    }// end of Update()


    // a function to fade from black to stored color
    private void FadeToColor()
    {
        // if r = less than
        if (rend.material.color.r < mySavedColor.r)
        {            
            // increase         
            // wait
            StartCoroutine(Wait(fadeTime, new Color(changeColor.r += Time.deltaTime, changeColor.g, changeColor.b, changeColor.a), "color"));
        }// end of r less than

        // if g = less than
        if (rend.material.color.g < mySavedColor.g)
        {
            // increase        
            // wait
            StartCoroutine(Wait(fadeTime, new Color(changeColor.r, changeColor.g += Time.deltaTime, changeColor.b, changeColor.a), "color"));
        }// end of g less than

        // if b = less than
        if (rend.material.color.b < mySavedColor.b)
        {
            // increase        
            // wait
            StartCoroutine(Wait(fadeTime, new Color(changeColor.r, changeColor.g, changeColor.b += Time.deltaTime, changeColor.a), "color"));
        }// end of b less than

        // if a = more than
        if (rend.material.color.a > mySavedColor.a)
        {
            // decrease       
            // wait
            StartCoroutine(Wait(fadeTime, new Color(changeColor.r, changeColor.g, changeColor.b, changeColor.a -= Time.deltaTime), "color"));
        }// end of a more than

        //print("2color: " + rend.material.color);
    }// end of fadetoColor


    // a function to fade from stored color to black
    private void FadeToBlack()
    {
        // if r = more than
        if (rend.material.color.r > black.r)
        {            
            // decrease       
            // wait
            StartCoroutine(Wait(fadeTime, new Color(changeColor.r -= Time.deltaTime, changeColor.g, changeColor.b, changeColor.a), "black"));
        }// end of r more than

        // if g = more than
        if (rend.material.color.g > black.g)
        {
            // decrease       
            // wait
            StartCoroutine(Wait(fadeTime, new Color(changeColor.r, changeColor.g -= Time.deltaTime, changeColor.b, changeColor.a), "black"));
        }// end of g more than

        // if b = more than
        if (rend.material.color.b > black.b)
        {
            // decrease       
            // wait
            StartCoroutine(Wait(fadeTime, new Color(changeColor.r, changeColor.g, changeColor.b -= Time.deltaTime, changeColor.a), "black"));
        }// end of b more than

        // if a = less than
        if (rend.material.color.a < black.a)
        {
            // increase       
            // wait
            StartCoroutine(Wait(fadeTime, new Color(changeColor.r, changeColor.g, changeColor.b, changeColor.a += Time.deltaTime), "black"));
        }// end of a less than than

        //print("3color: " + rend.material.color);
    }// end of fadetoblack()

    // wait for fade anim
    IEnumerator Wait(float howLong, Color newColor, string colorOrBlack)
    {
        // if black
        if(colorOrBlack == "black")
        {
            rend.material.color = newColor;
            yield return new WaitForSeconds(howLong);
            FadeToBlack();
        }// end of "black"

        // if color
        if (colorOrBlack == "color")
        {
            rend.material.color = newColor;
            yield return new WaitForSeconds(howLong);
            FadeToColor();
        }// end of "color"

    }// end of wait to fade

}// end of custom shader fade script
