// @Author Nabil Lamriben ©2017
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerInfoEntry : MonoBehaviour
{

    public InputField TextBoxField;

    Data_PlayerInfo cur_data_PlayerInfo;

    Data_PlayerPoints CurPlayerPoints;

    Data_PlayerSession CurrDataSession;

    SessionDataManager _sessmngr;

    string StrEntered; //value will not be null. it comes from keyboard event

    bool hasbeenActivated;
    //// bool textwasentered;

    TimerBehavior t;

    public Text txt;
    //this object will be created at the beggining of each Game session. It is not persistant;
    //Todo: persistant score grabber needs not be persistant at all. in fact i can just use it here and create a playerpoints object  in ValueChanged
    void Start()
    {
        t = gameObject.AddComponent<TimerBehavior>();
        Init2();
        if (GameSettings.Instance.IsTestModeON)
        {
            StartCoroutine(FakeInput());
        }
        ResetActivateInputField();
    }



    IEnumerator FakeInput()
    {
        yield return new WaitForSeconds(5);
        FakeCatchSend();
    }


    void Init2()
    {

        TextBoxField.ActivateInputField();
        TextBoxField.onValueChanged.AddListener(delegate { CatchSend(); });

        _sessmngr = GetComponent<SessionDataManager>();

        if (PersistantScoreGrabber.Instance == null) { CurPlayerPoints = new Data_PlayerPoints(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1); }
        else
        {
            if (CurPlayerPoints == null)
            {
                CurPlayerPoints = PersistantScoreGrabber.Instance.Get_Data_Player();
            }
        }

        txt.text = "";

        if (CurPlayerPoints.pointslost > 0)
        {

            //"Please Keep your Headset on."
            //"Wait for Store Staff."
            txt.text = "             Please Keep your Headset on." + "\n" +
                        "             Wait for Store Staff." + "\n" +
                        "       Final Score______  " + CurPlayerPoints.score + "\n" +
                        "       Zombies Killed___  " + CurPlayerPoints.kills + "\n" +
                        "       Points Lost______  " + CurPlayerPoints.pointslost + "\n";
        }
        else
        {
            txt.text = " Final Score______ " + CurPlayerPoints.score + "\n" +
                       " Zombies Killed___ " + CurPlayerPoints.kills + "\n";
            //" Zombies Killed___ " + CurPlayerPoints.kills + "\n";

        }


        // " Accuracy = "+CalcAccuracy(CurPlayerPoints.totalshots, CurPlayerPoints.miss) + "\n"+
        // "Shots fired            = " + CurPlayerPoints.totalshots +   "   |   Shots missed =  " + CurPlayerPoints.miss + "   |   ACCURACY =  " + CurPlayerPoints.Accuracy + " \n" +
        //"Player head shots       = " + CurPlayerPoints.headshots +   "   |  HeadShot kills   " + CurPlayerPoints.headshotkills +   "  |  hs Acc   " + CurPlayerPoints.HeadshotAccuracy+ "\n"+
        //"Player body shots       = " + CurPlayerPoints.torssoshots + "   |  HeadShot kills   " + CurPlayerPoints.torssoshotkills + "  |  bs Acc " + CurPlayerPoints.TorssoshotAccuracy + "\n" +
        //"Player limb shots       = " + CurPlayerPoints.limbshots +   "   |  HeadShot kills   " + CurPlayerPoints.limbshotkills +   "  |  ls Acc " + CurPlayerPoints.LimbshotAccuracy + "\n" +


        //"Player wavessurvived   = " + CurPlayerPoints.wavessurvived + "\n" +
        //"Player points Lost     = " + CurPlayerPoints.pointslost + "\n" +
        //"Player deaths          = " + CurPlayerPoints.deaths + "\n" + "\n" +

        //"Player streakcount     = " + CurPlayerPoints.streakcount + "\n" +
        //"Player maxstreak       = " + CurPlayerPoints.maxstreak + "\n" +
        //"Player zombies killed  = " + CurPlayerPoints.kills + "\n" +

        //"Player numberofReloads = " + CurPlayerPoints.numberofReloads + "\n" +
        //"Player wavessurvived   = " + CurPlayerPoints.wavessurvived + "\n";
    }

    string CalcAccuracy(int allshots, int miss)
    {
        int landedcalced = allshots - miss;

        float fired = (float)allshots;
        float landed = (float)landedcalced;
        if (fired > 0)
        {
            float accuracy = (landed / fired) * 100;
            return accuracy.ToString("0.00") + " %";
        }



        return "0 %";


    }
    string charstr;
    public void detectPressedKeyOrButton()
    {
        foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(kcode))
            {
                charstr = kcode.ToString().ToLower();
                Logger.Debug(charstr);

                if (charstr.CompareTo("rightcontrol") == 0) { Logger.Debug("yo going to maine"); SceneManager.LoadScene("None"); }
            }

        }
    }

    // Invoked when the value of the text field changes.
    public void ValueChangeCheck()
    {

        string[] strarra = StrEntered.Split(',');
        if (strarra.Length == 4)
        {

            if (cur_data_PlayerInfo == null) cur_data_PlayerInfo = new Data_PlayerInfo();
            cur_data_PlayerInfo.PlayerFirstName = strarra[0];
            cur_data_PlayerInfo.PlayerLastName = strarra[1];
            cur_data_PlayerInfo.PlayerUserName = strarra[2];
            cur_data_PlayerInfo.PlayerEmail = strarra[3];

            // Logger.Debug("you  entered " + cur_data_PlayerInfo.ToString());
            if (CurPlayerPoints == null)
            {
                CurPlayerPoints = PersistantScoreGrabber.Instance.Get_Data_Player();
            }
            //Data_PlayerSession thisSession = new Data_PlayerSession(System.DateTime.Now, cur_data_PlayerInfo, CurPlayerPoints);

            Data_PlayerSession thisSession = null;
            if (GameSettings.Instance == null)
            {
                thisSession = new Data_PlayerSession(System.DateTime.Now, cur_data_PlayerInfo, CurPlayerPoints);
            }
            else
            {
                thisSession = new Data_PlayerSession(System.DateTime.Now, GameSettings.Instance.GameName, GameSettings.Instance.IsTournamentModeOn, GameSettings.Instance.GameVersion, cur_data_PlayerInfo, CurPlayerPoints);
            }

            _sessmngr.SaveSession_to_ALLSessions_AndSaveTOFile(thisSession);
            ////textwasentered = true;

            StartCoroutine(AUTOGOTOGAME());
        }
        else
        {
            Logger.Debug("inbvalid input , must re make inputfield active and start all over after deleting th einput text field");
            ResetInputFieldAndTxt();
            TextBoxField.ActivateInputField();
        }
    }

    void ResetInputFieldAndTxt()
    {
        TextBoxField.text = "";
        inputstring = "";
    }

    string inputstring;
    public void CatchSend()
    {

        // Logger.Debug("on text changed text entered = " + TextBoxField.text);
        inputstring = TextBoxField.text;
        //  txt.text = inputstring;
        //  Logger.Debug("input string " + inputstring);
        if (inputstring.Length > 0)
        {
            object lastchar = inputstring.ToCharArray().GetValue(inputstring.Length - 1);
            if (lastchar != null && lastchar is char)
            {
                char x = (char)lastchar;
                // Logger.Debug("last char is + " + x);
                if (x == '*')
                {
                    Escape();
                }
            }
        }
    }

    void FakeCatchSend()
    {
        int rnd = UnityEngine.Random.Range(0, 20000);


        inputstring = " Bob" + rnd.ToString() + "by, Ddue,k,BobbyATyahoo.com*";

        if (inputstring.Length > 0)
        {
            object lastchar = inputstring.ToCharArray().GetValue(inputstring.Length - 1);
            if (lastchar != null && lastchar is char)
            {
                char x = (char)lastchar;
                // Logger.Debug("last char is + " + x);
                if (x == '*')
                {
                    Escape();
                }
            }
        }
    }



    void Escape()
    {
        Logger.Debug("input string " + inputstring);
        string[] strarra = inputstring.Split(',');
        if (cur_data_PlayerInfo == null) cur_data_PlayerInfo = new Data_PlayerInfo();

        if (strarra.Length == 4)
        {
            cur_data_PlayerInfo.PlayerFirstName = strarra[0];
            cur_data_PlayerInfo.PlayerLastName = strarra[1];
            cur_data_PlayerInfo.PlayerUserName = strarra[2];
            cur_data_PlayerInfo.PlayerEmail = strarra[3];

            Logger.Debug("you  entered " + cur_data_PlayerInfo.ToString());

            if (PersistantScoreGrabber.Instance == null) { CurPlayerPoints = new Data_PlayerPoints(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1); }
            else
            {
                if (CurPlayerPoints == null)
                {
                    CurPlayerPoints = PersistantScoreGrabber.Instance.Get_Data_Player();
                }
            }
            Logger.Debug("the scores are " + CurPlayerPoints.ToString());


            //nabilio
            //Data_PlayerSession thisSession = new Data_PlayerSession(System.DateTime.Now, cur_data_PlayerInfo, CurPlayerPoints);

            //_sessmngr.SaveSession_to_ALLSessions_AndSaveTOFile(thisSession);
            //textwasentered = true;


            Data_PlayerSession thisSession = null;
            if (GameSettings.Instance == null)
            {
                thisSession = new Data_PlayerSession(System.DateTime.Now, cur_data_PlayerInfo, CurPlayerPoints);
            }
            else
            {
                thisSession = new Data_PlayerSession(System.DateTime.Now, GameSettings.Instance.GameName, GameSettings.Instance.GameVersion, cur_data_PlayerInfo, CurPlayerPoints);

            }

            _sessmngr.SaveSession_to_ALLSessions_AndSaveTOFile(thisSession);
            //// textwasentered = true;





            StartCoroutine(AUTOGOTOGAME());
        }
        else
        {
            ResetInputFieldAndTxt();
            TextBoxField.ActivateInputField();
        }
    }

    IEnumerator AUTOGOTOGAME()
    {
        Logger.Debug("going to autogame in 5 sec");
        yield return new WaitForSeconds(5);
        //SingletonKiller.Instance.KillShareAnchor();
        //SingletonKiller.Instance.KillConsole3D();
        // SingletonKiller.Instance.KillHeadShotSounds();
        // SceneManager.LoadScene("EditMapAUTO");
        CurPlayerPoints = null;
        SceneManager.LoadScene("None");
    }

    public void ResetActivateInputField()
    {
        // Debug.Log("con3d only refocusing input field");
        TextBoxField.ActivateInputField();
        t.StartTimer(1f, ResetActivateInputField, false);
    }


    //private void Update()
    //{

    // detectPressedKeyOrButton();
    //if (hasbeenActivated && !textwasentered)
    //{
    //    DoActivateTextField();
    //}
    //}

}
