using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class TeaganServer : MonoBehaviour {
    public Text OutputResult;
    public Button Btn_GetThatInt;

    string TeaganServerIP = "http://192.168.1.8:27742/game";
    string SEND_TO_TeaganServerIP = "http://192.168.1.8:27742/game/send?message=hi%20dude";
    bool ServerIpIsValid;

    IEnumerator DoGetThatInt()
    {
        WWW www = new WWW(SEND_TO_TeaganServerIP);
        yield return www;
        string resStr = www.text;
        OutputResult.text = " the int is  " + resStr;
        int res = -11;
        int.TryParse(resStr, out res);
 
        if (res > 0)
        {

            ServerIpIsValid = true;
        }
        else
            OutputResult.text = "invalid server ip/port :" + TeaganServerIP + " !!!";
    }

    


    void OnGetThatInt() {

        StartCoroutine("DoGetThatInt");

    }
    //build this player
    void Start()
    {
        ServerIpIsValid = false;
        Btn_GetThatInt.onClick.AddListener(delegate { OnGetThatInt(); });
 
    }

}
