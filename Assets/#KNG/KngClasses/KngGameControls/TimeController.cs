using System.Collections;
using UnityEngine;

public class TimeController : MonoBehaviour
{



    float MAXWAITTIME = 10f;

    bool _slowtimeOn = false;
    float __CURSLOWTIME = 0f;
    float __CURSLOWTIMEPERCENTAGE = 0f;


    public void ResetTimeScaleToNormal(bool argPlayAudio)
    {
        __CURSLOWTIME = 0f;
        __CURSLOWTIMEPERCENTAGE = 0f;
        Time.timeScale = 1.0f;
        GameEventsManager.Instance.Call_SlowTimeOff();
        if (argPlayAudio)
            PlayHeadShotSound.Instance.PlaySound_2d_SlowTimeOn();
    }
    public void ResetTimeScaleToNormal_waverestart()
    {
        __CURSLOWTIME = 0f;
        __CURSLOWTIMEPERCENTAGE = 0f;
        Time.timeScale = 1.0f;
        GameEventsManager.Instance.Call_SlowTimeOff();
    }

    public void InitialSlowTimeCall()
    {

        StartCoroutine(WaitForMidOfSLowTimeAudio());
    }
    void RunInitilSlowTime()
    {
        if (!_slowtimeOn)
        {
            _slowtimeOn = true;
            __CURSLOWTIME = 1.2f;
            Time.timeScale = 0.2f;
            GameEventsManager.Instance.Call_SlowTimeOn();
        }
        else
        {
            _slowtimeOn = true;
            __CURSLOWTIME += 0.1f;
        }
    }
    IEnumerator WaitForMidOfSLowTimeAudio()
    {

        PlayHeadShotSound.Instance.PlaySound_2d_SlowTimeOn();
        yield return new WaitForSecondsRealtime(0.7f);
        RunInitilSlowTime();
        // PlayHeadShotSound.Instance.PlayHeartBeat(1.6f);

    }



    public void UpdateSlowTime()
    {
        if (!_slowtimeOn)
        {
            return;
        }

        __CURSLOWTIME += 0.1f;
        if (__CURSLOWTIME >= MAXWAITTIME)
        {
            __CURSLOWTIME = MAXWAITTIME;
        }
    }

    bool TriggeredSuddenDeath;
    bool TriggeredEndGAme;
    bool TriggeredPlayerMustDie;

    public float SuddenDeath_Seconds;
    public float EndGAme_Seconds;
    public float PlayerMustDieTime;
    public float _curRealTimePlayTime;
    //  float _curTimeSinceSceneLoaded;
    int _curSecInt;
    int _lastSecInt;
    bool Only10SecLeft = false;
    bool hasSaidThisNumber = false;


    void RunGameTimers()
    {
        if (GameManager.Instance.KngGameState == ARZState.WavePlay)
        {
            _curRealTimePlayTime += Time.unscaledDeltaTime;

            // RzPlayerComponent.Instance.PlayHud_Debug_RealTime(((int)_curRealTimePlayTime).ToString());
        }

        //   _curTimeSinceSceneLoaded += Time.unscaledDeltaTime;

        if (!TriggeredSuddenDeath)
        {
            if (_curRealTimePlayTime > SuddenDeath_Seconds)
            {
                GameManager.Instance.SetSuddenDeathRaised();
                TriggeredSuddenDeath = true;
                RzPlayerComponent.Instance.PlayHud_Debug_Vertical("SUDDENDEATH");
            }
        }

        if (!TriggeredEndGAme)
        {
            if (_curRealTimePlayTime > EndGAme_Seconds)
            {
                GameManager.Instance.KngGameState = ARZState.ReachedAllowedTime;
                GameManager.Instance.HardStop();
                TriggeredEndGAme = true;
                RzPlayerComponent.Instance.PlayHud_Debug_Vertical("END GAME");
            }
        }

        if (!TriggeredPlayerMustDie)
        {
            if (_curRealTimePlayTime > PlayerMustDieTime)
            {
                PlayHeadShotSound.Instance.PlaySound_2d_PlayerMustDie();
                TriggeredPlayerMustDie = true;
                RzPlayerComponent.Instance.PlayHud_Debug_Vertical("PLAYER MUST DIE");
            }
        }


        if (_curRealTimePlayTime > EndGAme_Seconds - 10)
        {
            Only10SecLeft = true;
        }

        if (Only10SecLeft)
        {
            _curSecInt = (int)_curRealTimePlayTime;

            Ten_2_zero = (int)EndGAme_Seconds - _curSecInt;
            NewSecond(Ten_2_zero);



        }




    }

    int Ten_2_zero = 10;
    void NewSecond(int curSecond)
    {
        if (_lastSecInt != curSecond)
        {
            hasSaidThisNumber = false;
            _lastSecInt = curSecond;
            CountOnce(_lastSecInt);
        }
    }
    void CountOnce(int argNumToSay)
    {
        if (!hasSaidThisNumber)
        {

            GameEventsManager.Instance.call_CountDownAudioVideo(argNumToSay);
            hasSaidThisNumber = true;
        }

    }

    //void CHECK_PlayTimeEnded_and_countdown(int ArgTimeSincegamestart)
    //{
    //    //when to stop the game

    //    //if (GameSettings.Instance.IsTournamentModeOn)
    //    //{


    //    // wave time 20
    //    //arcadend 20
    //    //allowd =10(hard calc)
    //    // if   t  >= 10
    //    if (ArgTimeSincegamestart >= _gameLenggth_minus_10Sconds)
    //    {
    //        O_2_10 = SECONDS_GAMELENGTH - ArgTimeSincegamestart;
    //        //  Debug.Log(O_2_10);

    //        //CounterBAckFrom11=
    //        //if ()
    //        //startvoice 10 9 8 
    //        // GameManager.Instance.call_CountDownAudioVideo(numbertosay);
    //        if (STARTvoice_indBackCount)
    //        {
    //            //start decrementing 10
    //            numbertosay = O_2_10;

    //            NewSecond(numbertosay);

    //        }
    //    }

    //    if (ArgTimeSincegamestart >= SECONDS_GAMELENGTH)
    //    {
    //        timesUpThrown = true;
    //        if (_gameManager != null)
    //        {
    //            _gameManager.TimesUp();
    //            _gameManager.HardStop();
    //        }
    //        return;
    //    }
    //    //}
    //}

    private void FixedUpdate()
    {
        RunGameTimers();

        if (__CURSLOWTIME > 0f)
        {
            __CURSLOWTIME -= Time.unscaledDeltaTime;

        }
        else
        {
            _slowtimeOn = false;
            Time.timeScale = 1.0f;
            GameEventsManager.Instance.Call_SlowTimeOff();
            PlayHeadShotSound.Instance.StopHeartBeat(0.5f);
        }

        __CURSLOWTIMEPERCENTAGE = (__CURSLOWTIME / MAXWAITTIME);
        if (RzPlayerHealthTubeControllerComponent.Instance != null)
        {
            RzPlayerHealthTubeControllerComponent.Instance.Updated_Percent_TimeBAr(__CURSLOWTIMEPERCENTAGE);
        }
        else
        {
            Debug.Log("No PlayerHealthBAr Found");
        }

        //if (RzPlayerHealthTubeControllerComponent.Instance != null)
        //{
        //    RzPlayerHealthTubeControllerComponent.Instance.Updated_Percent_TimeBAr(__CURSLOWTIMEPERCENTAGE);
        //}
        //else
        //{
        //    Debug.Log("No PlayerHealthBAr Found");
        //}
        //if (true)
        //{
        //    Debug.Log(" DO Update Slow time tube");
        //}
    }
}
