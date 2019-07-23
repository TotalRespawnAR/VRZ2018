using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullRoomEsCtrl : MonoBehaviour {

    public GameObject Kingston;
    public GameObject Cambridge;

    private void Start()
    {
        if (GameSettings.Instance.IsKingstonOn)
        {
            Kingston.SetActive(true);
            Cambridge.SetActive(false);
        }
        else
        {
            Kingston.SetActive(false);
            Cambridge.SetActive(true);
        }
    }

}
