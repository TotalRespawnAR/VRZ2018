using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEmpTagFinder : MonoBehaviour {
    GameObject[] allEnemietags;
	// Use this for initialization
	void Start () {
        GameObject[] allObjects =  UnityEngine.Object.FindObjectsOfType<GameObject>() ;
        foreach (GameObject go in allObjects)
        {

            DeepSearchRemoveLAyer(go.transform);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public Transform DeepSearchContain(Transform parent, int val)
    {
        // Debug.Log("on " + parent.gameObject.name);

        foreach (Transform c in parent)
        {
            if (c.gameObject.layer == val) {
                return c;
            }
            var result = DeepSearchContain(c, val);
            if (result != null)
                return result;
        }
        return null;
    }

    public void DeepSearchRemoveLAyer(Transform parent)
    {
        if (parent == null) return;
        foreach (Transform c in parent)
        {
            if (c.gameObject.layer == 9)
            {
                Debug.Log(c.gameObject.name + " enemylayer");

            }
           
            else { c.gameObject.layer = 0; }
            DeepSearchRemoveLAyer(c);
            //if (result != null)
            //    return DeepSearchRemoveLAyer(c, val); ;
        }
        return;
    }
}
