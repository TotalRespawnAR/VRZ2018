using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandSetter : MonoBehaviour {

    public Text text;

  
     
	// Use this for initialization
	void Start () {

        DisplayText();


    }
    void DisplayText()
    {
        if (GameSettings.Instance.PlayerLeftyRight == ARZPlayerLeftyRighty.RightyPlayer)
        {
            text.text = "RIGHT handed";
        }

        else
        {
            text.text = "Lefty";
        }
    }

    public void ToggleHand()
    {
        if (GameSettings.Instance.PlayerLeftyRight == ARZPlayerLeftyRighty.RightyPlayer) {
            GameSettings.Instance.PlayerLeftyRight = ARZPlayerLeftyRighty.LeftyPlayer;
        }
        else
        {
            GameSettings.Instance.PlayerLeftyRight = ARZPlayerLeftyRighty.RightyPlayer;
        }

        //GameSettings.Instance.IsRightHandedPlayer = !GameSettings.Instance.IsRightHandedPlayer;
        DisplayText();
    }
}
    