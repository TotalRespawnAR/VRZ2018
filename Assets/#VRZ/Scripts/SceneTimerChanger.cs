using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTimerChanger : MonoBehaviour
{
    public string ScenName;
    // Use this for initialization
    void Start()
    {
        StartCoroutine(StartThisShitIn3sec());
    }
    IEnumerator StartThisShitIn3sec()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(ScenName);

    }
}
