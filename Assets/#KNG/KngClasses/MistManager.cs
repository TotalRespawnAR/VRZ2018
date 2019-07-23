using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MistManager : MonoBehaviour {

    public GameObject Mist1;
    public GameObject Mist2;

    public void NOMists() { Mist1.SetActive(false); Mist2.SetActive(false); }
    public void YesMists() { Mist1.SetActive(true); Mist2.SetActive(false); }
}
