// @Author Nabil Lamriben ©2018
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetControle : MonoBehaviour {

    // Use this for initialization
    public GameObject PointsOBJ;
    // Use this for initialization

    public GameObject EXPLOSION;

 
    public void MakeInvisible()
    {
        AmIOn = false;
        StartCoroutine(Makinivizinseconds());
        StartCoroutine(Witonsect());
        GetComponent<TargetEffect>().DISOLVEME();
    }

    IEnumerator Witonsect() {
        yield return new WaitForSeconds(1);
        MeshRenderer mr = GetComponent<MeshRenderer>();
        mr.enabled = false;
    }
    IEnumerator Makinivizinseconds() {

        yield return new WaitForSeconds(3);
        this.gameObject.SetActive(false);
    }
    public bool GetAmIOn() { return AmIOn; }
    bool AmIOn = true;

    public void FadeTarget() {

    }

    IEnumerator DoExplodeIn30Seconds() {
        autoExplodOn = true;
        yield return new WaitForSeconds(GameSettings.Instance.targetwaitTime);
        DOExplosion();
    }

    IEnumerator DoExplode_AUTO_NOSHOTSNEEDED()
    {
        autoExplodOn = true;
        yield return new WaitForSeconds(10f); //at least 10 placeroom in 2 wait3   set gun in 3 
        DOExplosion();
    }


    private void Start()
    {
       // StartCoroutine(DoExplode_AUTO_NOSHOTSNEEDED());
    }


    void DOExplosion() {
        MakeInvisible();
        Instantiate(EXPLOSION, transform.localPosition, Quaternion.identity);

     //   GameManagerOld.Instance.CheckWav1Started(); //TARGET explode
    }

 

    bool autoExplodOn = false;
    public void TakeHit(Bullet bullet) {
        regularhits++;
      //  Debug.Log("hit");
        if (bullet == null) return;
        int p =CalPointsForHere(bullet.hitInfo);
        string pstr = p.ToString();
        GameObject go= Instantiate(PointsOBJ, transform.position + (Vector3.up /2f ), Quaternion.identity) as GameObject;
        go.GetComponent<StreakText>().SetTextbox(pstr);
        Destroy(go, 3.0f);

        // deffer starting game to start button
        //if (bullseye == 3) DOExplosion();

        if (regularhits == 1) {
            if (autoExplodOn) {
                return;
            }
            StartCoroutine(DoExplodeIn30Seconds());
        }
         
    }

    int bullseye = 0;
    int regularhits = 0;

    public int CalPointsForHere(RaycastHit hitInfo)
    {
        float magnitude = Vector3.Magnitude(transform.position - hitInfo.point);
       // Debug.Log(magnitude);
        int points = 0;
        if (magnitude < 0.07f) { points = 10; bullseye++;  }
        else
        if (magnitude > 0.07f && magnitude < 0.14f) { points = 9; bullseye++; }
        else
        if (magnitude > 0.14f && magnitude < 0.23f) { points = 8; bullseye++;}
        else
        if (magnitude > 0.23f && magnitude < 0.29f) { points = 7; }
        else
        if (magnitude > 0.29f && magnitude < 0.39f) { points = 6; }
        else
        if (magnitude >0.39f && magnitude < 0.50f) { points = 5; }
        else
        if (magnitude > 0.50f ) { points = 0; }

         
       

            return points;
    }
}
