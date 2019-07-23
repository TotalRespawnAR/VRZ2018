using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineChaining : MonoBehaviour {

	// Use this for initialization
	void Start () {
        // StartCoroutine(Func2());
        StartCoroutine(Chaining());
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void CountTo5() {
        for (int x = 0; x <6; x++) {
            Debug.Log("counts to 5 " + x);
        }
    }

    void CountTo_6_7_8()
    {
        for (int x = 6; x < 9; x++)
        {
            Debug.Log("counts 678 " + x);
        }
    }

    IEnumerator Func1()
    {
        CountTo5();
            yield return null;

    }

    IEnumerator Func2()
    {
        //wait for the completion of Func1
        yield return StartCoroutine(Func1());
        CountTo_6_7_8();

    }

    IEnumerator FuncA()
    {
        CountTo5();
        yield return null;

    }
    IEnumerator FuncB()
    {
        CountTo_6_7_8();
        yield return null;

    }

    IEnumerator Chaining()
    {
       
        yield return StartCoroutine(FuncA());
        yield return StartCoroutine(FuncB());


    }
}
