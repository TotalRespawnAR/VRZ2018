using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToGame : MonoBehaviour
{
    public string SceneName;
    // Use this for initialization
    void Start()
    {
        StartCoroutine(StartInaFew());
    }


    IEnumerator StartInaFew()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneName);
    }
}
