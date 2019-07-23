//#define ENABLE_KEYBORADINPUTS
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour {

 
    public Text TimerTxt_RunningTime;
    public Text TimerTxt_PlayTime;
    public Text CurWaveLBL_curwave;
    public Text TimerTxt_CurWaveTime;
    public Text TimerTxt_sinceSuddenDeathTime;

    private void Start()
    {
        Debug.Log("game timer on " + gameObject.name);
    }

    /*
    bool _isPaused = false;
    bool timesUpThrown=false;
    bool _experienceStarted = false;
    bool _suddenDeathCAlled = false;

    float _curGameTimer = 0f;
    int _Seconds_curGsmeTimer = 0;

    //Will BE OVERRRIDEN 
    int _curWaveLevel_Number = 1;
    int _curTicker = 0;
    //not using wave time here cuz this is just a straight up time . completely encapsulated
    float _cruWaveTimer = 0f;
    int _Seconds_curWaveTimer = 0;
    ////int REVERSE_WAVESE = 0;

    float _ActualPlayTime = 0f;
    int _Seconds_ActualPlayTime = 0;

    float _TimeSinceSuddenDeath = 0f;
    int _Seconds__TimeSinceSuddenDeath = 0;

    float GAMELENGTH = 0f;
    int SECONDS_GAMELENGTH = 0;

    //private float _Time_TournamentNonKidgame_END;
    //private float _Time_KidsMode_END;
    //private float _Time_SuddenDeathTriggerSTART;

    //private int __Seconds_TournamentNONkid_EndTime;
    //private int __Seconds_Arcade_EndTime;

    private  int _gameLenggth_minus_10Sconds;
     private int _SuddenDeath_TriggerTime_Seconds;
    
    GameManager _gameManager;
#region INitEvents

    private void OnEnable()
    {
        Debug.Log("Do I work0 " + gameObject.name);
        GameEventsManager.OnGamePaused += PauseCountDownCounter;
        GameEventsManager.OnGameContinue += ContinueCountDownCounter;
        GameEventsManager.OnRzExperienceStarted  += StartTimers;
        GameEventsManager.OnWaveStartedOrReset += StartWaveTimer;
        GameEventsManager.OnWaveStartedOrReset_DEO += WaveLevelStartedOrReloaded;
    }

    private void OnDisable()
    {

        GameEventsManager.OnGamePaused -= PauseCountDownCounter;
        GameEventsManager.OnGameContinue -= ContinueCountDownCounter;
        GameEventsManager.OnRzExperienceStarted -= StartTimers;
        GameEventsManager.OnWaveStartedOrReset -= StartWaveTimer;
        GameEventsManager.OnWaveStartedOrReset_DEO -= WaveLevelStartedOrReloaded;

    }
     private void PauseCountDownCounter(bool argtruefalse) { _isPaused = true; }
    private void ContinueCountDownCounter(bool argtruefalse) { _isPaused = false; }
    private void StartTimers(){ _experienceStarted = true; }
    WaveLevel _curWaveLevel;
    ScriptWaveScenario _curWaveScenario;
    int _EndofThisWave;
    //Wavemanager will raize event 
    //or just dirrect ref for now
    int specialZombiePTR=0;
    void WaveLevelStartedOrReloaded(WaveLevel  argwaveLevel) {
        //CancelInvoke();
        _curWaveLevel = argwaveLevel;
        _cruWaveTimer = 0f;
        _Seconds_curWaveTimer = 0;
        //_curWaveLevel_Number = argwaveLevel.Get_LevelNumber();
        //_curWaveScenario = argwaveLevel.GetScenarioObject();
        //_curTicker = argwaveLevel.TimeCoolDown_wl;
        //_EndofThisWave = argwaveLevel.Get_LevelTime();
      //  int thislevelsEDOlistLength = _curWaveScenario.EDOList.Count;
       // mySpecialEnemies = new Data_Enemy[thislevelsEDOlistLength];

        //for (int x = 0; x < thislevelsEDOlistLength; x++) {
        //    mySpecialEnemies[x] =new Data_Enemy( _curWaveScenario.EDOList[x].TimeStamp , _curWaveScenario.EDOList[x].ModelName, _curWaveScenario.EDOList[x].HitPoints, _curWaveScenario.EDOList[x].Ztype_STD, _curWaveScenario.EDOList[x].InitialState, _curWaveScenario.EDOList[x].InitialState);
        //}
        specialZombiePTR = 0;
        //oldSec = -1;

        LevelAgroed_1 = false;
    }

    //int oldSec = -1;
    //void CheckSpecialZtimePTRplusplus(int thistime) {
    //    if (_gameManager.KngGameState!= ARZState.WavePlay) return;

    //    //Debug.Log("sec " + thistime);
    //    if (oldSec == thistime) return;

    //    if (mySpecialEnemies[specialZombiePTR].TimeStamp == thistime) {
           
    //        //Debug.Log("sec " + thistime);
    //        GameEventsManager.Instance.Call_AzzRoZQ(mySpecialEnemies[specialZombiePTR]);
    //        specialZombiePTR++;
    //        if (specialZombiePTR >= mySpecialEnemies.Length) specialZombiePTR = 0;
          
    //    }

    //    oldSec = thistime;
    //}


    //void CheckTimeOnCurPointedSpecialEnemyInSpecialList(int argsec) {
    //    //if (_curWaveLevel.NoTicks())
    //    //{

    //    //    return;
    //    //}

    //    // Debug.Log("Mahdude at  "+ specialZombiePTR+" t=" + mySpecialEnemies[specialZombiePTR].TimeStamp + " " + argsec);
    //    if (mySpecialEnemies[specialZombiePTR].TimeStamp == argsec)
    //    {

    //        //   Debug.Log("sec " + argsec);
    //        GameEventsManager.Instance.Call_AzzRoZQ(mySpecialEnemies[specialZombiePTR]);
    //        specialZombiePTR++;
    //        if (specialZombiePTR >= mySpecialEnemies.Length) specialZombiePTR = 0;

    //    }
    //}



 
  //  public void Set_CureWave_Scenario(ScriptWaveScenario argcurWaveScenario) { _curWaveScenario = argcurWaveScenario; }
   // Data_Enemy[] mySpecialEnemies;

    //Deprecating 
    private void StartWaveTimer(int argWaveNumber) {
        _cruWaveTimer = 0f;
        _Seconds_curWaveTimer = 0;
        _curWaveLevel_Number = argWaveNumber;
    }
#endregion
    // Use this for initialization



    void RunDeltaTimer_convertToSeconds() {
        _curGameTimer += Time.deltaTime;
        _Seconds_curGsmeTimer = (int)_curGameTimer;
        if (_experienceStarted) {
            if (!_isPaused && !timesUpThrown &&                 
                !_gameManager.IsPlayerDead)
            {
                if (_gameManager.KngGameState == ARZState.WavePlay  ||
                    _gameManager.KngGameState == ARZState.WaveOverTime ||
                    _gameManager.KngGameState == ARZState.ReachedAllowedTime)
                {
                    _cruWaveTimer += Time.deltaTime;
                    _Seconds_curWaveTimer = (int)_cruWaveTimer;

                    if (_suddenDeathCAlled)
                    {
                        _TimeSinceSuddenDeath += Time.deltaTime;
                        _Seconds__TimeSinceSuddenDeath = (int)_TimeSinceSuddenDeath;
                        Filter_SpawnSpecialZombies();
                    }

                    if (GameManager.Instance.KngGameState == ARZState.Pregame ||
                        GameManager.Instance.KngGameState == ARZState.WaveBuffer ||
                       _gameManager.IsPlayerDead)
                        return;
                    _ActualPlayTime += Time.deltaTime;
                    _Seconds_ActualPlayTime = (int)_ActualPlayTime;
                    
                }
            }
        }
    }
    string ConvertTo_MIN_SEC(int argSeconds) {
        if (argSeconds <= 0) { return "0:0"; }
        int t = argSeconds;
        int minutes = t / 60;
        int seconds = t % 60;
        string min = (minutes < 10) ? ("0" + minutes) : ("" + minutes);
        string sec = (seconds < 10) ? ("0" + seconds) : ("" + seconds);

        return min+":"+sec;
    }


    


     void InitTimesAndSeconds()
    {
     
        _isPaused = false;
        timesUpThrown = false;
        _experienceStarted = false;
        _suddenDeathCAlled = false;

        _curGameTimer = 0f;
        _Seconds_curGsmeTimer = 0;

        _curWaveLevel_Number = 1;
        _cruWaveTimer = 0f;
        _Seconds_curWaveTimer = 0;


        _ActualPlayTime = 0f;
        _Seconds_ActualPlayTime = 0;

        _TimeSinceSuddenDeath = 0f;
        _Seconds__TimeSinceSuddenDeath = 0;


        GAMELENGTH = 0f;
        SECONDS_GAMELENGTH = 0;

        if (GameSettings.Instance.IsTournamentModeOn)
        {
            GAMELENGTH = GameSettings.Instance.Global_Time_Apocalypse_GameEnds_600s_10m;
        }
        else
        {
            GAMELENGTH = GameSettings.Instance.Global_Time_Arcade_GameEnds_240s_4m;

        }
        SECONDS_GAMELENGTH = (int)GAMELENGTH;
        _gameLenggth_minus_10Sconds = SECONDS_GAMELENGTH - 10;
        _SuddenDeath_TriggerTime_Seconds =(int) GameSettings.Instance.Global_Time_SuddenDeath_300s_5min;
        LevelAgroed_1 = false;

    }

    void Start()
    {
        if (GameSettings.Instance != null)
        {
            _gameManager = GameManager.Instance;

            InitTimesAndSeconds();
            _MyLocalTickBools = new TickBools();
         }
       
    }
    TickBools _MyLocalTickBools;

    // Update is called once per frame
    void Update () {
        if (GameManager.Instance.KngGameState != ARZState.EndGame && _curWaveLevel!=null)
        {
            RunDeltaTimer_convertToSeconds();
            CHECK_PlayTimeEnded_and_countdown(_Seconds_ActualPlayTime);
            CHECK_TimeToCallSuddenDeath(_Seconds_ActualPlayTime);

            CHECK_LevelTicker();
        }
    //  CheckSpecialZtimePTRplusplus(_Seconds_curGsmeTimer);

// CHECK_LevelTicker();
#if ENABLE_KEYBORADINPUTS
         if (Input.GetKeyDown(KeyCode.Space)) { Debug.Log("i have " + _curWaveScenario.EDOList[0].ModelName.ToString() + " to spawn"); }
#endif
    }
    //LateUpdateOnlyToCheckForTimeandDisplay Seconds on ui
    void LateUpdate()
    {

        if (_gameManager.KngGameState != ARZState.EndGame && _curWaveLevel != null)
        {
            TimerTxt_RunningTime.text = ConvertTo_MIN_SEC(_Seconds_curGsmeTimer);
            CurWaveLBL_curwave.text = _curWaveLevel_Number.ToString();
            TimerTxt_CurWaveTime.text = ConvertTo_MIN_SEC(_Seconds_curWaveTimer);
            TimerTxt_PlayTime.text = ConvertTo_MIN_SEC(_Seconds_ActualPlayTime);
            TimerTxt_sinceSuddenDeathTime.text = ConvertTo_MIN_SEC(_Seconds__TimeSinceSuddenDeath);
        }

    }
    bool notSetPlayerToinvincible = true;
    int numbertosay = 10;//first tim not said
    int O_2_10=0;
    bool STARTvoice_indBackCount = false;
    void CHECK_PlayTimeEnded_and_countdown(int ArgTimeSincegamestart)
    {
        //when to stop the game

        //if (GameSettings.Instance.IsTournamentModeOn)
        //{


        // wave time 20
        //arcadend 20
        //allowd =10(hard calc)
    // if   t  >= 10
        if (ArgTimeSincegamestart >= _gameLenggth_minus_10Sconds) {
            O_2_10 = SECONDS_GAMELENGTH - ArgTimeSincegamestart;
          //  Debug.Log(O_2_10);
            if (notSetPlayerToinvincible) {
                _gameManager.isInvincible = true;
                STARTvoice_indBackCount = true;
                _gameManager.KngGameState = ARZState.ReachedAllowedTime;
                notSetPlayerToinvincible = false;
            }
            //CounterBAckFrom11=
            //if ()
            //startvoice 10 9 8 
            // GameManager.Instance.call_CountDownAudioVideo(numbertosay);
            if (STARTvoice_indBackCount) {
                //start decrementing 10
                numbertosay =  O_2_10;
                
                NewSecond(numbertosay);

            }
        }

            if (ArgTimeSincegamestart >= SECONDS_GAMELENGTH)
            {
                timesUpThrown = true;
                if (_gameManager != null)
                {
                    _gameManager.TimesUp();
                    _gameManager.HardStop();
                }
                return;
            }
        //}
    }

    bool hasSaidThisNumber = false;
    int oldSecond=11;
    
    void NewSecond(int curSecond) {
        if (oldSecond != curSecond) {
            hasSaidThisNumber = false;
            oldSecond = curSecond;
            CountOnce(oldSecond);
        }
    }
    void CountOnce(int argNumToSay)
    {
        if (!hasSaidThisNumber) {
         //   Debug.Log("saying " + argNumToSay);
            GameEventsManager.Instance.call_CountDownAudioVideo(argNumToSay);
            hasSaidThisNumber = true;
        }

    }

    void CHECK_TimeToCallSuddenDeath(int ArgTimeSincegamestart)
    {
        if (ArgTimeSincegamestart >= _SuddenDeath_TriggerTime_Seconds)
        {
            if (_suddenDeathCAlled != true)
            {
                if (_gameManager != null)
                {
                    GameEventsManager.Instance.Call_SuddenDeath();
                    _gameManager.SetSuddenDeathRaised();
                    _suddenDeathCAlled = true;
                }
            }
        }
    }

    bool LevelAgroed_1 = false;
    ////bool WITHIN_SECOND = true;
    ////bool HAsSentTickOnce = false;
    int thisSec = -1;
    void CHECK_LevelTicker() {
        if (_gameManager.IsPlayerDead) return;
        if (_gameManager.KngGameState==ARZState.WavePlay)
        {
            //
            //            if (thisSec == _Seconds_curWaveTimer) { return; }

            //            if (!LevelAgroed_1)
            //            {
            //                if (_Seconds_curWaveTimer > _curWaveLevel.TimeIncreaseAgro && _curWaveLevel.TimeIncreaseAgro>0) {
            //                    _curWaveLevel.Increase_Initial_State();

            //                    LevelAgroed_1 = true;
            //                }
            //            }
            //            //tickbool grave times
            //            if ( _Seconds_curWaveTimer >= _curWaveLevel.TimeGraveStart  
            //                && _Seconds_curWaveTimer < _curWaveLevel.TimeGraveEnds         
            //               )
            //            {
            //                if (_curWaveLevel.TimeGraveStart == 0) {
            //                    _MyLocalTickBools.ShouldGrave = false;
            //                }else
            //                _MyLocalTickBools.ShouldGrave = true;
            //            }
            //            else {
            //                _MyLocalTickBools.ShouldGrave = false;
            //            }


            //            if (_Seconds_curWaveTimer >= _curWaveLevel.TimeGasMAskStart
            //               && _Seconds_curWaveTimer < _curWaveLevel.TimeGasMAskEnds
            //              )
            //            {
            //                if (_curWaveLevel.TimeGasMAskStart == 0)
            //                {
            //                    _MyLocalTickBools.ShouldGassmask = false;
            //                }
            //                else
            //                    _MyLocalTickBools.ShouldGassmask = true;
            //            }
            //            else
            //            {
            //                _MyLocalTickBools.ShouldGassmask = false;
            //            }



            //            if (_Seconds_curWaveTimer >= _curWaveLevel.TimeBigMutantStart
            //               && _Seconds_curWaveTimer < _curWaveLevel.TimeBigMutantkEnds
            //              )
            //            {
            //                if (_curWaveLevel.TimeBigMutantStart == 0)
            //                {
            //                    _MyLocalTickBools.ShouldBigMutant = false;
            //                }
            //                else
            //                    _MyLocalTickBools.ShouldBigMutant = true;
            //            }
            //            else
            //            {
            //                _MyLocalTickBools.ShouldBigMutant = false;
            //            }


            //            if (_Seconds_curWaveTimer >= _curWaveLevel.TimeFighterStart
            //               && _Seconds_curWaveTimer < _curWaveLevel.TimeFighterEnds
            //              )
            //            {
            //                if (_curWaveLevel.TimeFighterStart == 0)
            //                {
            //                    _MyLocalTickBools.Shouldfighters = false;
            //                }
            //                else
            //                    _MyLocalTickBools.Shouldfighters = true;
            //            }
            //            else
            //            {
            //                _MyLocalTickBools.Shouldfighters = false;
            //            }


            //            if (_Seconds_curWaveTimer >= _curWaveLevel.TimeTeethStart
            //            && _Seconds_curWaveTimer < _curWaveLevel.TimeTeethEnds
            //           )
            //            {
            //                if (_curWaveLevel.TimeTeethStart == 0)
            //                {
            //                    _MyLocalTickBools.Shouldteeth = false;
            //                }
            //                else
            //                    _MyLocalTickBools.Shouldteeth = true;
            //            }
            //            else
            //            {
            //                _MyLocalTickBools.Shouldteeth = false;
            //            }

            //            if (_Seconds_curWaveTimer >= _curWaveLevel.TimePumpkinStart
            //         && _Seconds_curWaveTimer < _curWaveLevel.TimePumpkinEnds
            //        )
            //            {
            //                if (_curWaveLevel.TimePumpkinStart == 0)
            //                {
            //                    _MyLocalTickBools.Shouldpumpkins = false;
            //                }
            //                else
            //                    _MyLocalTickBools.Shouldpumpkins = true;
            //            }
            //            else
            //            {
            //                _MyLocalTickBools.Shouldpumpkins = false;
            //            }



            //            if (_Seconds_curWaveTimer >= _curWaveLevel.TimeClawStart
            //                && _Seconds_curWaveTimer < _curWaveLevel.TimeClawEnds
            //                )
            //            {
            //                if (_curWaveLevel.TimeClawStart == 0)
            //                {
            //                    _MyLocalTickBools.ShouldClaws = false;
            //                }
            //                else
            //                    _MyLocalTickBools.ShouldClaws = true;
            //            }
            //            else
            //            {
            //                _MyLocalTickBools.ShouldClaws = false;
            //            }





            //            CheckTimeOnCurPointedSpecialEnemyInSpecialList(_Seconds_curWaveTimer);

            //            if (_Seconds_curWaveTimer == _EndofThisWave)
            //            {
            //                _curWaveLevel.FinishLevel();
            //            }
            //            //Debug.Log("sec" + _Seconds_curWaveTimer);
            //            thisSec = _Seconds_curWaveTimer;
            //            if (_Seconds_curWaveTimer > 0 && _Seconds_curWaveTimer % _curWaveLevel.Get_TickTime() == 0)
            //            {
            //                _MyLocalTickBools.TickTime = _Seconds_curWaveTimer;
            //                GameEventsManager.Instance.CAll_LevelTicked(_MyLocalTickBools);
            //                ////HAsSentTickOnce = true;
            //            }
            //            if (_curWaveLevel.FlyPeriod < 1) return;
            //            if (_curWaveLevel.FlyPeriod < 2) _curWaveLevel.FlyPeriod = 2;
            //            if (_Seconds_curWaveTimer > 0 && _Seconds_curWaveTimer % _curWaveLevel.FlyPeriod == 0)
            //            {
            //                GameEventsManager.Instance.CAll_LevelFlyTicked();
            //            }
            //    
        }

    }

    void CHECKscenarioAndSendmaybe() {

    }


    int Time_Spawn_GasMask1 = 20; //at 20 sec signal spawngasmask zomb

    int Time_Spawn_GasMask2 = 40; //at 20 sec signal spawngasmask zomb

    int Time_Spawn_GiantMutanat1 = 60; //at 20 sec signal spawngasmask zomb

    int Time_Spawn_GasMask3 = 70; //at 20 sec signal spawngasmask zomb

    int Time_Spawn_GasMask4 = 80; //at 20 sec signal spawngasmask zomb

    int Time_Spawn_GiantMutanat2 = 90; //at 20 sec signal spawngasmask zomb

    int Time_Spawn_GiantMutanat3 = 95;
    int Time_Spawn_GiantMutanat4 = 100;
    int Time_Spawn_GiantMutanat5 = 105;
    int Time_Spawn_GiantMutanat6 = 110;
    int Time_Spawn_GiantMutanat7 = 115;
    int Time_Spawn_GiantMutanat8 = 120;
    int Time_Spawn_GiantMutanat9 = 125;
    int Time_Spawn_GiantMutanat10 = 130;
    int Time_Spawn_GiantMutanat11 = 135;
    int Time_Spawn_GiantMutanat12 = 140;


    void Filter_SpawnSpecialZombies()
    {
       // if (_Seconds__TimeSinceSuddenDeath == 5) { GameManager.Instance.Call_TimeTospawnSpcialZomb(14); }
       // if (_Seconds__TimeSinceSuddenDeath == 6) { GameManager.Instance.Call_TimeTospawnSpcialZomb(15); }
        if (_Seconds__TimeSinceSuddenDeath == Time_Spawn_GasMask1) { GameEventsManager.Instance.Call_TimeTospawnSpcialZomb(14); }
        if (_Seconds__TimeSinceSuddenDeath == Time_Spawn_GasMask2) { GameEventsManager.Instance.Call_TimeTospawnSpcialZomb(14); }
        if (_Seconds__TimeSinceSuddenDeath == Time_Spawn_GasMask3) { GameEventsManager.Instance.Call_TimeTospawnSpcialZomb(14); }
        if (_Seconds__TimeSinceSuddenDeath == Time_Spawn_GasMask4) { GameEventsManager.Instance.Call_TimeTospawnSpcialZomb(14); }

        if (_Seconds__TimeSinceSuddenDeath == Time_Spawn_GiantMutanat1) { GameEventsManager.Instance.Call_TimeTospawnSpcialZomb(15); }
        if (_Seconds__TimeSinceSuddenDeath == Time_Spawn_GiantMutanat2) { GameEventsManager.Instance.Call_TimeTospawnSpcialZomb(15); }
        if (_Seconds__TimeSinceSuddenDeath == Time_Spawn_GiantMutanat3) { GameEventsManager.Instance.Call_TimeTospawnSpcialZomb(15); }
        if (_Seconds__TimeSinceSuddenDeath == Time_Spawn_GiantMutanat4) { GameEventsManager.Instance.Call_TimeTospawnSpcialZomb(15); }
        if (_Seconds__TimeSinceSuddenDeath == Time_Spawn_GiantMutanat5) { GameEventsManager.Instance.Call_TimeTospawnSpcialZomb(15); }
        if (_Seconds__TimeSinceSuddenDeath == Time_Spawn_GiantMutanat6) { GameEventsManager.Instance.Call_TimeTospawnSpcialZomb(15); GameEventsManager.Instance.Call_TimeTospawnSpcialZomb(15); }
        if (_Seconds__TimeSinceSuddenDeath == Time_Spawn_GiantMutanat7) { GameEventsManager.Instance.Call_TimeTospawnSpcialZomb(15); GameEventsManager.Instance.Call_TimeTospawnSpcialZomb(14); }
        if (_Seconds__TimeSinceSuddenDeath == Time_Spawn_GiantMutanat8) { GameEventsManager.Instance.Call_TimeTospawnSpcialZomb(15); GameEventsManager.Instance.Call_TimeTospawnSpcialZomb(15); }
        if (_Seconds__TimeSinceSuddenDeath == Time_Spawn_GiantMutanat9) { GameEventsManager.Instance.Call_TimeTospawnSpcialZomb(15); GameEventsManager.Instance.Call_TimeTospawnSpcialZomb(14); }
        if (_Seconds__TimeSinceSuddenDeath == Time_Spawn_GiantMutanat10) { GameEventsManager.Instance.Call_TimeTospawnSpcialZomb(15); GameEventsManager.Instance.Call_TimeTospawnSpcialZomb(15); }
        if (_Seconds__TimeSinceSuddenDeath == Time_Spawn_GiantMutanat11) { GameEventsManager.Instance.Call_TimeTospawnSpcialZomb(15); GameEventsManager.Instance.Call_TimeTospawnSpcialZomb(15); }
        if (_Seconds__TimeSinceSuddenDeath == Time_Spawn_GiantMutanat12) { GameEventsManager.Instance.Call_TimeTospawnSpcialZomb(15); GameEventsManager.Instance.Call_TimeTospawnSpcialZomb(15); }

    }
    
     */
}
