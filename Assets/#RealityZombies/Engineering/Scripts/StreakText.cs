using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreakText : MonoBehaviour {

    public TextMesh tb;

    public void SetTextbox(string str) {
        tb.text = str;
    }

    public void SetColor(Color argColor) {
        tb.color = argColor;
        if (argColor == Color.green)
        {
            tb.fontStyle = FontStyle.Bold;
        }
        else
        {
            tb.fontStyle = FontStyle.Normal;
        }
    }

    public void SetFontSize(int argFsize) {
        tb.fontSize = argFsize;
    }
    
}
