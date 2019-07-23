using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistoUdpContainer : MonoBehaviour {
    public static PersistoUdpContainer Instance = null;
    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            Instance = this;
        }
        else
            Destroy(gameObject);
    }

}
