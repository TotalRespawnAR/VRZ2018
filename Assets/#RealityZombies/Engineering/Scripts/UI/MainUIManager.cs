using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class MainUIManager : MonoBehaviour {
    /// <summary>
    ///  this is to show/hide the setup menue.. it is never used 
    /// </summary>
    KeywordRecognizer keywordRecognizer = null;
    Dictionary<string, System.Action> keywords2 = new Dictionary<string, System.Action>();

    public GameObject FullMenu;

    public GameObject CurSor;

    public GameObject InputMNGR;

    public void SHOWME() {
        FullMenu.SetActive(true);
        InputMNGR.SetActive(true);
        CurSor.SetActive(true);
    }
    public void HIDEME() {
        FullMenu.SetActive(false);
        InputMNGR.SetActive(false);
        CurSor.SetActive(false);
    }
	// Use this for initialization
	void Start () {
        keywords2.Add("Show Me", () =>
        {
            SHOWME();

        });

        keywords2.Add("Hide Me", () =>
        {
            HIDEME();

        });
        if (keywordRecognizer == null)
            keywordRecognizer = new KeywordRecognizer(keywords2.Keys.ToArray());

        // Register a callback for the KeywordRecognizer and start recognizing!
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();
    }



    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;
        if (keywords2.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();
        }
    }
    ////bool togonof=false;


    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            HIDEME();
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            SHOWME();
        }
    }
}
