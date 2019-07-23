using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigation : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        StartCoroutine(StartInaFew());
    }


    IEnumerator StartInaFew()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("runthisCopySmallTerr");
    }
}
