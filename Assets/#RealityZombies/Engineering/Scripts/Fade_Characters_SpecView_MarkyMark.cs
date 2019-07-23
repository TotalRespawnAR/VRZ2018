using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade_Characters_SpecView_MarkyMark : MonoBehaviour {

    /// this script allows switching between zombies for an animation
    /// 

        // the different children
    public GameObject[] zombieNorm;
    public GameObject[] zombieHolo;
    public GameObject[] zombieDiss;

    public bool useAnimation = false;

    public GameObject hexShot;

    // start
    private void Start()
    {
        ShowDiss();

        // if using my animation 
        if(useAnimation == true)
        {
            StartCoroutine(MarketingAnim());
        }// end of using my anim
     
    }// end of start()

    // update
    private void Update()
    {
        // if press 1
        if (Input.GetKeyDown("1"))
        {
            ShowNorm();
        }// end of press 1

        // if press 2
        if (Input.GetKeyDown("2"))
        {
            ShowHolo();
        }// end of press 2

        // if press 3
        if (Input.GetKeyDown("3"))
        {
            ShowDiss();
        }// end of press 3

    }// end of update()


    // functions for each of the zombies
    //1 - show normal
    private void ShowNorm()
    {
        foreach (GameObject limb in zombieNorm)
        {
            limb.GetComponent<SkinnedMeshRenderer>().enabled = true;
        }
        foreach (GameObject limb in zombieHolo)
        {
            limb.GetComponent<SkinnedMeshRenderer>().enabled = false;
        }
        foreach (GameObject limb in zombieDiss)
        {
            limb.GetComponent<SkinnedMeshRenderer>().enabled = false;
        }

    }// end of show normal

    //2 - show hologram
    private void ShowHolo()
    {
        foreach (GameObject limb in zombieNorm)
        {
            limb.GetComponent<SkinnedMeshRenderer>().enabled = false;
        }
        foreach (GameObject limb in zombieHolo)
        {
            limb.GetComponent<SkinnedMeshRenderer>().enabled = true;
        }
        foreach (GameObject limb in zombieDiss)
        {
            limb.GetComponent<SkinnedMeshRenderer>().enabled = false;
        }
    }// end of show hologram

    //3 - show dissolve
    private void ShowDiss()
    {
        foreach (GameObject limb in zombieNorm)
        {
            limb.GetComponent<SkinnedMeshRenderer>().enabled = false;
        }
        foreach (GameObject limb in zombieHolo)
        {
            limb.GetComponent<SkinnedMeshRenderer>().enabled = false;
        }
        foreach (GameObject limb in zombieDiss)
        {
            limb.GetComponent<SkinnedMeshRenderer>().enabled = true;
        }
    }// end of show dissolve

    IEnumerator MarketingAnim()
    {
        // zombie enters and is in dissolve
        yield return new WaitForSeconds(2.5f);
        ShowHolo();
        // make it a solid hologram now
        yield return new WaitForSeconds(1.5f);
        ShowNorm();
        // start fading between hologram and real/normal
        yield return new WaitForSeconds(0.1f);
        ShowHolo();
        yield return new WaitForSeconds(0.1f);
        ShowNorm();
        yield return new WaitForSeconds(0.1f);
        ShowHolo();
        yield return new WaitForSeconds(0.1f);
        ShowNorm();
        yield return new WaitForSeconds(0.1f);
        ShowHolo();
        yield return new WaitForSeconds(0.1f);
        ShowNorm();
        // faster flashing
        yield return new WaitForSeconds(0.05f);
        ShowHolo();
        yield return new WaitForSeconds(0.05f);
        ShowNorm();
        yield return new WaitForSeconds(0.05f);
        ShowHolo();
        yield return new WaitForSeconds(0.05f);
        ShowNorm();
        yield return new WaitForSeconds(0.05f);
        ShowHolo();
        yield return new WaitForSeconds(0.05f);
        ShowNorm();

        // zombie is real soon
        yield return new WaitForSeconds(1.65f);

        // if the bullet shot is there && this is sarah
        if (hexShot != null && transform.name.Contains("Sarah"))
        {
            hexShot.SetActive(true);
        }// end of if bullet shot is there

        // zombie is real now
        yield return new WaitForSeconds(0.35f);

        // start fading back
        ShowHolo();
        yield return new WaitForSeconds(0.1f);
        ShowNorm();
        yield return new WaitForSeconds(0.1f);
        ShowHolo();
        yield return new WaitForSeconds(0.1f);
        ShowNorm();
        yield return new WaitForSeconds(0.1f);
        ShowHolo();       

        // dissolve out
        yield return new WaitForSeconds(0.5f);
        ShowDiss();

        // wait to display skeleton's bullet
        yield return new WaitForSeconds(0.25f);
        // if the bullet shot is there && this is skeleton
        if (hexShot != null && transform.name.Contains("Skeleton"))
        {
            hexShot.SetActive(true);
        }// end of if bullet shot is there


    }// end of marketing animation



}// end of fade characters script
