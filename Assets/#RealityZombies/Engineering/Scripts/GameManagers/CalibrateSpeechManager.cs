// @Author Jeffrey M. Paquette ©2016

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows.Speech;

public class CalibrateSpeechManager : MonoBehaviour
{

    KeywordRecognizer keywordRecognizer = null;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

    //void EditorKeyboardInputs()
    //{



    //    if (Input.GetKeyDown(KeyCode.M)) { SceneManager.LoadScene("SetupMenu"); }

    //}
    //void Update()
    //{
    //    EditorKeyboardInputs();
    //}

    // Use this for initialization
    void Start()
    {
        keywords.Add("Exit Calibration", () =>
        {
            //load main menu scene
            SceneManager.LoadScene("SetupMenu");
        });

        // Tell the KeywordRecognizer about our keywords.
        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());

        // Register a callback for the KeywordRecognizer and start recognizing!
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();
    }

    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;
        if (keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();
        }
    }
    private void OnDestroy()
    {
        if (keywordRecognizer != null)
        {
            keywordRecognizer.Stop();
            keywordRecognizer.OnPhraseRecognized -= KeywordRecognizer_OnPhraseRecognized;
            keywordRecognizer.Dispose();
        }
    }
}
