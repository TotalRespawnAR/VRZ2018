using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicLevelTest : WaveLevel
{
    private float eventTime;
    public int WaveLevelCurSec = 0;
    public int SpawnRate;
    public int GraveRate;
    public int BossTime;
    public int Time_IncreaseAgro;
    public int Time_StartGrave;

    bool Check_Time_SpawnStandard()
    {
        return (WaveLevelCurSec % SpawnRate == 0);

    }
    bool Check_Time_SpawnGraver()
    {
        return (WaveLevelCurSec % GraveRate == 0 && WaveLevelCurSec >= Time_StartGrave);
    }
    bool Check_Time_SpawnSprinter()
    {
        return (WaveLevelCurSec == Times_place_Sprinter[cur_SprinterTimeIndex]);
    }
    bool Check_Time_AXXXXER()
    {
        return (WaveLevelCurSec == Times_Place_Axeguy[cur_AxeManTimeIndex]);
    }
    //bool Check_Time_BossGraver()
    //{
    //    return (WaveLevelCurSec == BossTime);
    //}
    bool Check_Time_IncreaseAgro()
    {
        return (WaveLevelCurSec == Time_IncreaseAgro);
    }

    bool Check_Time_WaveEnd()
    {
        return (WaveLevelCurSec == Time_IncreaseAgro);
    }



    public bool HasBos;
    bool bosswasSpawned;
    bool SpeedWasincreased;
    // bool AgroTimeReached;
    int cur_SprinterTimeIndex = 0;
    int cur_AxeManTimeIndex = 0;
    public List<int> Times_place_Sprinter;
    public List<int> Times_Place_Axeguy;
    bool waveTimeReached;



    public List<KngEnemyName> pickfrom;
    KngEnemyName[] StandardWaveZombieNames;
    KngEnemyName[] GRaversWaveZombieNames = new KngEnemyName[] { KngEnemyName.MUMMY, KngEnemyName.SKELETON };
    // KngEnemyName[] AxeGuysZombieNames = new KngEnemyName[] { KngEnemyName.axvmanv2, KngEnemyName.BASIC };
    KngEnemyName[] SPrinters = new KngEnemyName[] { KngEnemyName.CHECKER, KngEnemyName.SWEATER, KngEnemyName.MEATHEADBLACK, KngEnemyName.SPINAL, KngEnemyName.PAUL };
    private Coroutine CounterCoroutingUp;



    #region MonoMethods
    void Awake()
    {
        Alive_AxeGuys = 0;
        Alive_Gravers = 0;
        Alive_Standards = 0;
        Alive_Sprinters = 0;
        StandardWaveZombieNames = pickfrom.ToArray();
#if ENABLE_DEBUGLOG
        Debug.Log("AwakeAwakeAwake!");
#endif
        eventTime = float.PositiveInfinity;
        if (SpawnRate < 2) { SpawnRate = 2; }
        if (GraveRate < 2) { SpawnRate = 2; }
        if (Get_LevelTime() < (SpawnRate * 2))
        {
            iSet_LevelTime(SpawnRate * 2);
        }

    }
    void OnEnable()
    {
        GameEventsManager.On_PredatorPointed += HEardPredatorPointed;
        GameEventsManager.On_WaveMiniBossDied += One_Boss_Died;
        GameEventsManager.On_WaveMiniAXMANDied += On_Axeman_Died;
        GameEventsManager.On_WaveGRAVERDied += On_Graver_Died;
        GameEventsManager.On_WaveStandardEnemyDied += On_Standard_Died;
        GameEventsManager.On_WaveSPrinterEnemyDied += On_Sprinter_Died;
        GameEventsManager.OnNoLiveZombiesLeft += HeardLastLiveEnemyDied;
    }
    void OnDisable()
    {
        GameEventsManager.OnNoLiveZombiesLeft -= HeardLastLiveEnemyDied;

        GameEventsManager.On_PredatorPointed -= HEardPredatorPointed;
        GameEventsManager.On_WaveMiniBossDied -= One_Boss_Died;
        GameEventsManager.On_WaveStandardEnemyDied -= On_Standard_Died;
        GameEventsManager.On_WaveSPrinterEnemyDied -= On_Sprinter_Died;
        GameEventsManager.On_WaveMiniAXMANDied -= On_Axeman_Died;
        GameEventsManager.On_WaveGRAVERDied -= On_Graver_Died;
    }


    protected override void Start()
    {
#if ENABLE_DEBUGLOG
        Debug.Log("BasicLevel ovr Wavelevel Start!");
#endif

    }
    #endregion

    public override void BaseStartLevel_WAVEPLAY()
    {

        //RzPlayerComponent.Instance.PlayHud_Debug_Vertical(" wave  " + Get_LevelNumber().ToString() + "   finaltime " + Get_LevelTime().ToString());

        //RzPlayerComponent.Instance.PlayHud_Debug_Vertical("HP  " + Get_LevelHP().ToString() + "   hit strength " + Get_LevelHItStrength().ToString());
        //RzPlayerComponent.Instance.PlayHud_Debug_Vertical("std ." + Get_LevelMax_Standard_OnScreen().ToString() + " grv ." + Get_LevelMax_Gravers_OnScreen().ToString() + " spr." + Get_LevelMax_Sprinters_OnScreen().ToString() + " Axe." + Get_LevelMax_Axers_OnScreen().ToString());

        CounterCoroutingUp = StartCoroutine(CountUp());
    }

    IEnumerator CountUp()
    {

        Debug.Log(Get_LevelTime() + " ist the final time");
        while (WaveLevelCurSec <= Get_LevelTime())
        {
            yield return new WaitForSeconds(1);
            WaveLevelCurSec++;

            RzPlayerComponent.Instance.PlayHud_Debug_Event(WaveLevelCurSec.ToString());
#if ENABLE_DEBUGLOG
            Debug.Log("sec=" + WaveLevelCurSec);
#endif

            if (HasBos)
            {

                if (!bosswasSpawned)
                {
                    DoBossWaves(Get_LevelNumber());

                }


            }
            DoOldWaveSpawn();

        }
        waveTimeReached = true;
        waitforallenemiestobedead_setStateOVERTIME();
    }



    void HeardLastLiveEnemyDied()
    {
        RzPlayerComponent.Instance.PlayHud_Debug_Vertical("NoEnemies live");
        if (waveTimeReached)
        {
            GameManager.Instance.GM_Handle_LoadNextLevel();
        }

    }
    void waitforallenemiestobedead_setStateOVERTIME()
    {
#if ENABLE_DEBUGLOG
        Debug.Log("Wave EndTime Reached");
#endif


        CounterCoroutingUp = null;
        waveTimeReached = true;
        GameManager.Instance.KngGameState = ARZState.WaveOverTime;
        GameManager.Instance.OvertimeThrownCheckIfEndNow();
    }

    #region boss wave and non boss wave spawn paterns
    //void DoNonBossWaves()
    //{


    //    if (WaveLevelCurSec % SpawnRate == 0)
    //    {
    //        Spwan_Standards_byMaxAlive();
    //    }

    //    if (WaveLevelCurSec % GraveRate == 0 && WaveLevelCurSec >= Time_StartGrave)
    //    {


    //        Spawn_Gravers_byMAxAlive();
    //    }

    //    if (WaveLevelCurSec == Times_place_Sprinter[cur_SprinterTimeIndex])
    //    {

    //        if (cur_SprinterTimeIndex < Times_place_Sprinter.Count - 1)
    //        {
    //            if (Times_place_Sprinter[cur_SprinterTimeIndex] >= Times_place_Sprinter[cur_SprinterTimeIndex + 1])
    //                Debug.LogError("Problem ");
    //        }

    //        if (cur_SprinterTimeIndex >= Times_place_Sprinter.Count)
    //        {
    //            cur_SprinterTimeIndex = 0;
    //        }
    //        Spawn_Sprinters_byMaxAlive();
    //        cur_SprinterTimeIndex++;

    //    }


    //    if (WaveLevelCurSec == Times_Place_Axeguy[cur_AxeManTimeIndex])
    //    {
    //        Spawn_AxeGuys_byMaxAlive();
    //        cur_AxeManTimeIndex++;
    //        if (cur_AxeManTimeIndex >= Times_Place_Axeguy.Count)
    //        {
    //            cur_AxeManTimeIndex = 0;
    //        }
    //    }

    //    if (WaveLevelCurSec == Time_IncreaseAgro)
    //    {
    //        //Debug.Log("AGROOO now at " + WaveLevelCurSec + " seconds");

    //        iIncrease_LevelSeekSpeed();

    //    }

    //}

    void DoOldWaveSpawn()
    {


        if (Check_Time_SpawnStandard()) { Spwan_Standards_byMaxAlive(); }

        if (Check_Time_SpawnGraver()) { Spawn_Gravers_byMAxAlive(); }

        if (Check_Time_SpawnSprinter())
        {
            Spawn_Sprinters_byMaxAlive(); cur_SprinterTimeIndex++; if (cur_SprinterTimeIndex >= Times_place_Sprinter.Count)
            {
                cur_SprinterTimeIndex = 0;
            }
        }

        if (Check_Time_AXXXXER())
        {
            Spawn_AxeGuys_byMaxAlive(); cur_AxeManTimeIndex++; if (cur_AxeManTimeIndex >= Times_Place_Axeguy.Count)
            {
                cur_AxeManTimeIndex = 0;
            }
        }

        if (Check_Time_IncreaseAgro())
        {

            if (!SpeedWasincreased)
            {
                iIncrease_LevelSeekSpeed();
                GameManager.Instance.ENEMYMNGER_getter().SetIncreaseagro();
                SpeedWasincreased = true;
            }
        }

    }

    void DoIncrementaLSpawn()
    {


        if (Check_Time_SpawnGraver()) { Spawn_Gravers_byMAxAlive(); }

        if (Check_Time_SpawnSprinter())
        {
            Spawn_Sprinters_byMaxAlive(); cur_SprinterTimeIndex++; if (cur_SprinterTimeIndex >= Times_place_Sprinter.Count)
            {
                cur_SprinterTimeIndex = 0;
            }
        }

        if (Check_Time_AXXXXER())
        {
            Spawn_AxeGuys_byMaxAlive(); cur_AxeManTimeIndex++; if (cur_AxeManTimeIndex >= Times_Place_Axeguy.Count)
            {
                cur_AxeManTimeIndex = 0;
            }
        }

        if (Check_Time_IncreaseAgro())
        {
            if (!SpeedWasincreased)
            {
                iIncrease_LevelSeekSpeed();
                GameManager.Instance.ENEMYMNGER_getter().SetIncreaseagro();
                SpeedWasincreased = true;
            }
        }



    }

    void DoBossWaves(int WaveNum)
    {

        if (WaveLevelCurSec == BossTime)
        {
            Do_SpawnPumpkinZombie();
        }
    }

    #endregion

    #region Spawn max by max alive

    int _curStandardIndex = 0;
    int Alive_Standards;
    void Spwan_Standards_byMaxAlive()
    {

        if (GameManager.Instance.KngGameState == ARZState.WavePlay)
        {
            if (Alive_Standards < Get_LevelMax_Standard_OnScreen())
            {
                int howmanyTOpickfrom = pickfrom.Count;
                if (_curStandardIndex >= howmanyTOpickfrom)
                {
                    _curStandardIndex = 0;
                }

                Data_Enemy DE_noTimeStamp_noID = BuildDE(ARZombieypes.STANDARD, pickfrom[_curStandardIndex], Get_LevelBaseSeekSpeed(), Get_LevelHP(), SelfRoundRobin_SpawnPoints());
                GameManager.Instance.Spawn_Enemy(DE_noTimeStamp_noID);
                _curStandardIndex++;
                Alive_Standards++;
            }
        }

    }
    public override void On_Standard_Died()
    {
        //Debug.Log("std died");
        Alive_Standards--;
    }


    int _curGraverIndex = 0;
    int Alive_Gravers;
    void Spawn_Gravers_byMAxAlive()
    {
        //  Debug.Log("Spawn_Gravers_byMAxAlive alive " + Alive_Gravers + " onscreennow " + Get_LevelMax_Gravers_OnScreen());

        if (Alive_Gravers <= Get_LevelMax_Gravers_OnScreen())
        {
            if (_curGraverIndex >= GRaversWaveZombieNames.Length)
            {
                _curGraverIndex = 0;
            }

            Data_Enemy DE_noTimeStamp_noID = BuildDE(ARZombieypes.GRAVESKELETON, GRaversWaveZombieNames[Alive_Gravers % GRaversWaveZombieNames.Length], Get_LevelBaseSeekSpeed(), Get_LevelHP(), SelfRoundRobin_GRavePoints());

            GameManager.Instance.Spawn_Enemy(DE_noTimeStamp_noID);

            //todo: link the durt to the graver so it can get destroyed along with grvaer
            GameManager.Instance.EffectDirt(DE_noTimeStamp_noID.SpawnKnode.GetPos());

            _curGraverIndex++;
            Alive_Gravers++;
        }

    }
    public override void On_Graver_Died()
    {
        // Debug.Log("GRaveDied");
        Alive_Gravers--;
    }

    int curSprinterIndex = 0;
    int Alive_Sprinters;
    void Spawn_Sprinters_byMaxAlive()
    {
        if (Alive_Sprinters < Get_LevelMax_Sprinters_OnScreen())
        {
            if (curSprinterIndex >= SPrinters.Length)
            {
                curSprinterIndex = 0;
            }

            Data_Enemy boosp = BuildDE(ARZombieypes.Sprinter, SPrinters[curSprinterIndex], SeekSpeed.sprint, Get_LevelHP(), SelfRoundRobin_SpawnPoints());
            GameManager.Instance.Spawn_Enemy(boosp);

            curSprinterIndex++;
            Alive_Sprinters++;
        }
    }
    public override void On_Sprinter_Died()
    {
        // Debug.Log("sprinter died");
        Alive_Sprinters--;
    }


    int Alive_AxeGuys;
    void Spawn_AxeGuys_byMaxAlive()
    {

        //Debug.Log(" max axe" + Get_LevelMax_Axers_OnScreen());
        if (Alive_AxeGuys < Get_LevelMax_Axers_OnScreen())
        {
            int axeguyHP = Get_LevelHP() * 2;
            Data_Enemy argAxeguyDE = BuildDE(ARZombieypes.AXEMAN, KngEnemyName.axvmanv2, SeekSpeed.walk, axeguyHP, SelfRoundRobin_SpawnPoints());
            GameManager.Instance.Spawn_Enemy(argAxeguyDE);
            Alive_AxeGuys++;
        }
    }
    public override void On_Axeman_Died()
    {
        Alive_AxeGuys--;
        // Debug.Log("Axeman died " + Alive_AxeGuys + "left");
    }




    //void Do_SpawnHolobonesBoss()
    //{
    //    if (!bosswasSpawned)
    //    {
    //        int bossHP = Get_LevelHP() * 10;
    //        Data_Enemy boosp = BuildDE(ARZombieypes.STD_BOSS, KngEnemyName.HOLOBONES, SeekSpeed.run, bossHP, KnodeProvider.Instance.GetNodeByID(3));


    //        GameManager.Instance.Spawn_Enemy(boosp);
    //        bosswasSpawned = true;
    //    }
    //}

    //void Do_SpawnPumpkinBOss()
    //{
    //    if (!bosswasSpawned)
    //    {
    //        int bossHP = Get_LevelHP() * 10;
    //        Data_Enemy boosp = BuildDE(ARZombieypes.STD_BOSS, KngEnemyName.PumpkinShort, SeekSpeed.run, bossHP, KnodeProvider.Instance.GetNodeByID(3));


    //        GameManager.Instance.Spawn_Enemy(boosp);
    //        bosswasSpawned = true;
    //    }
    //}

    void Do_SpawnPumpkinZombie()
    {
        if (!bosswasSpawned)
        {

            RzPlayerComponent.Instance.PlayHud_Debug_Vertical("zombiePump spawned");
            int bossHP = Get_LevelHP() * 10;
            Data_Enemy boosp = BuildDE(ARZombieypes.TankFighter, KngEnemyName.PumpkinShort, SeekSpeed.run, bossHP, KnodeProvider.Instance.GetNodeByID(3));


            GameManager.Instance.Spawn_Enemy(boosp);
            bosswasSpawned = true;
        }
    }

    public override void One_Boss_Died()
    {
        Debug.Log("boss died");
        bosswasSpawned = false;
    }




    #endregion


    Data_Enemy BuildDE(ARZombieypes argZtype, KngEnemyName argName, SeekSpeed argStartSeekSpeed, int StartHP, KNode argSpawn)
    {

        if (GameManager.Instance.Get_IsItHellTimeYet())
        {
            return new Data_Enemy(
              0,
              -1,
              argName
              ,
              argZtype,
              StartHP,
              Get_LevelHItStrength() + 1,
              SeekSpeed.sprint,

              new Vector3(0, 180, 0),
             argSpawn);

        }
        else
        {
            return new Data_Enemy(
           0,
           -1,
           argName
           ,
           argZtype,
           StartHP,
           Get_LevelHItStrength(),
           argStartSeekSpeed,

           new Vector3(0, 180, 0),
          argSpawn);

        }



    }








    void TrySpawnLevelSpeed(int argHowmany)
    {
        //first filter
        if (argHowmany >= Get_LevelMax_Standard_OnScreen()) { argHowmany = Get_LevelMax_Standard_OnScreen(); }
        //second filter
        if (argHowmany >= (Get_LevelMax_Standard_OnScreen() - Alive_Standards)) { argHowmany = (Get_LevelMax_Standard_OnScreen() - Alive_Standards); }

        for (int z = 0; z < argHowmany; z++)
        {
            Spwan_Standards_byMaxAlive();
        }
    }




    //void Try_AxeThrowerWalking()
    //{
    //    if (Alive_AxeGuys >= Get_LevelMax_Axers_OnScreen())
    //    {
    //        // Debug.Log("cannot spawn anymore");
    //        return;
    //    }

    //    Data_Enemy DE_noTimeStamp_noID = BuildDE(ARZombieypes.AXEMAN, KngEnemyName.axvmanv2, SeekSpeed.walk, Get_LevelHP(), SelfRoundRobin_SpawnPoints());

    //    GameManager.Instance.Spawn_Enemy(DE_noTimeStamp_noID);
    //    Alive_AxeGuys++;

    //}

    int curIndex_SpawnPoints = 0;
    KNode SelfRoundRobin_SpawnPoints()
    {
        curIndex_SpawnPoints++;
        if (curIndex_SpawnPoints >= Get_ListSpawnPointIds().Count)
        {
            curIndex_SpawnPoints = 0;
        }

        return KnodeProvider.Instance.GetNodeByID(Get_ListSpawnPointIds()[curIndex_SpawnPoints]);
    }
    int curIndex_GravePoints = 0;

    KNode SelfRoundRobin_GRavePoints()
    {
        // Debug.Log("doing G roundRob");
        curIndex_GravePoints++;
        if (curIndex_GravePoints >= Get_ListGravePointIds().Count)
        {
            curIndex_GravePoints = 0;
        }

        return KnodeProvider.Instance.GetNodeByID(Get_ListGravePointIds()[curIndex_GravePoints]);
    }
    int curIndexTunnelPoints = 0;
    KNode SelfRoundRobin_TunnelPoints()
    {
        curIndexTunnelPoints++;
        if (curIndexTunnelPoints >= Get_ListTunnelSpawnPointIds().Count)
        {
            curIndexTunnelPoints = 0;
        }

        return KnodeProvider.Instance.GetNodeByID(Get_ListTunnelSpawnPointIds()[curIndexTunnelPoints]);
    }




}
