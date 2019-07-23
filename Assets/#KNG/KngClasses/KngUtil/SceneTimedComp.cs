using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTimedComp : MonoBehaviour {

    public string NextSceneName;
    public int Buffertime;
    
     
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    IEnumerator BufferWait() {
        yield return new WaitForSeconds(Buffertime);
        SceneManager.LoadScene(NextSceneName); 
    }
}
