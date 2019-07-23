using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SelfKillIfGame : MonoBehaviour {



    void Update()
    {
        if (SceneManager.GetActiveScene().name == "KngGame")
        {
            Destroy(gameObject);
        }
    }
}
