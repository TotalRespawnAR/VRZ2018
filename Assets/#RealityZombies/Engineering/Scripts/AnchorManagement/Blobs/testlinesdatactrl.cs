
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class testlinesdatactrl : MonoBehaviour {
    DataTextSaverLoader dtsl;
    public TextMesh TextFromSer;
    // Use this for initialization
    void Awake () {
        dtsl = GetComponent<DataTextSaverLoader>();
    }
    string Sample ;
    string[] textlines;
    private void Start()
    {
        Sample = "nabil\n lamriben\n grade3";
        textlines = Sample.Split('\n');

        //StartCoroutine(Waitabit());
    }
    IEnumerator Waitabit()
    {
        // WriteTimestamp();
       // Saveit(textlines);
        yield return new WaitForSeconds(2);

        LoadIt();
    }

    void Saveit(string[] textlines) { TextFromSer.text = "saving \n"; dtsl.Save("MyBytesSaved", textlines); }
    void LoadIt() {
        TextFromSer.text = "leaded \n";
        IList<string> textlinesRead = dtsl.Load("MyBytesSaved");
        foreach (string str in textlinesRead) {
            TextFromSer.text += "\n";
            TextFromSer.text += str;
        }
    }

}
