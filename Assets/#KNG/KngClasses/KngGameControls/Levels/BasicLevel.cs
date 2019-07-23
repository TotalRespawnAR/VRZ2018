//#define ENABLE_DEBUGLOG
using System.Collections;
using UnityEngine;
public class BasicLevel : WaveLevel
{
    private float eventTime;
    int WaveLevelCurSec = 0;
    public int SpawnRate;
    public int GraveRate;

    public int Time_StartGrave;
    public int Time_IncreaseAgro;
    bool waveTimeReached;
    public bool ReplenishAllSpawnsOnAllDied;

    KngEnemyName[] LowLevelZobies = new KngEnemyName[] { KngEnemyName.BASIC, KngEnemyName.HOODIE, KngEnemyName.SHITFACE, KngEnemyName.SPINAL, KngEnemyName.SWEATER, KngEnemyName.HILLBILLY };
    KngEnemyName[] GRavers = new KngEnemyName[] { KngEnemyName.MUMMY, KngEnemyName.SKELETON };
    KngEnemyName[] AxeGuys = new KngEnemyName[] { KngEnemyName.axvmanv2, KngEnemyName.BASIC };
    private Coroutine CounterCoroutingUp;



    #region MonoMethods
    void Awake()
    {
#if ENABLE_DEBUGLOG
        Debug.Log("AwakeAwakeAwake!");
#endif
        eventTime = float.PositiveInfinity;
        if (SpawnRate < 3) { SpawnRate = 3; }
        if (GraveRate < 3) { SpawnRate = 3; }
        if (Get_LevelTime() < (SpawnRate * 2))
        {
            iSet_LevelTime(SpawnRate * 2);
        }

    }
    void OnEnable()
    {
        GameEventsManager.OnNoLiveZombiesLeft += HeardLastLiveEnemyDied;
    }
    void OnDisable()
    {
        GameEventsManager.OnNoLiveZombiesLeft -= HeardLastLiveEnemyDied;
    }
    protected override void Start()
    {
#if ENABLE_DEBUGLOG
        Debug.Log("BasicLevel ovr Wavelevel Start!");
#endif
        //   StartTimer(3);
        //   StartCoroutine(CountUp());
    }
    #endregion

    public override void BaseStartLevel_WAVEPLAY()
    {
        CounterCoroutingUp = StartCoroutine(CountUp());
    }



    void HeardLastLiveEnemyDied()
    {
        if (waveTimeReached)
        {
            GameManager.Instance.GM_Handle_LoadNextLevel();
        }
        else
        {
            if (ReplenishAllSpawnsOnAllDied)
            {
                TrySpawnOnAllSpawnUnderMAxEnemiesOnScren();
            }
        }

    }
    void waitforallenemiestobedead_setStateOVERTIME()
    {
        Debug.Log("Wave EndTime Reached");
#if ENABLE_DEBUGLOG
#endif
        CounterCoroutingUp = null;
        waveTimeReached = true;
        GameManager.Instance.KngGameState = ARZState.WaveOverTime;
        GameManager.Instance.OvertimeThrownCheckIfEndNow();
    }

    IEnumerator CountUp()
    {

        while (WaveLevelCurSec <= Get_LevelTime())
        {
            yield return new WaitForSeconds(1);
            WaveLevelCurSec++;
#if ENABLE_DEBUGLOG
            Debug.Log("sec=" + WaveLevelCurSec);
#endif
            if (WaveLevelCurSec == 1)
            {
                TrySpawnOnAllSpawnUnderMAxEnemiesOnScren();
            }
            else
            if (WaveLevelCurSec % SpawnRate == 0)
            {

                TrySpawnOnAllSpawnUnderMAxEnemiesOnScren();
            }
            else
            if (WaveLevelCurSec % GraveRate == 0 && WaveLevelCurSec >= Time_StartGrave)
            {
                TrySpawnGraveOnAllGravesUnderMAxEnemiesOnScren();
            }
            //else
            //if (WaveLevelCurSec % SpawnRate == 0 ) {
            //    TrySpawnOnAllSpawnUnderMAxEnemiesOnScren();
            //}
        }

        waitforallenemiestobedead_setStateOVERTIME();
    }

    void TrySpawnOnAllSpawnUnderMAxEnemiesOnScren()
    {

        for (int z = 0; z < Get_ListSpawnPointIds().Count; z++)
        {
            if (GameManager.Instance.GEtAllives().Count >= Get_LevelMax_Standard_OnScreen())
            {
                break;
            }
            Data_Enemy DE_noTimeStamp_noID = BuildDE(LowLevelZobies[z % LowLevelZobies.Length], SelfRoundRobin_SpawnPoints());
            GameManager.Instance.Spawn_Enemy(DE_noTimeStamp_noID);
        }
    }
    Data_Enemy BuildDE(KngEnemyName argName, KNode argSpawn)
    {

        return new Data_Enemy(
             0,
             -1,
             argName,
             ARZombieypes.STANDARD,
             Get_LevelHP(),
             Get_LevelHItStrength(),
             Get_LevelBaseSeekSpeed(),
             new Vector3(0, 180, 0),
            argSpawn);

    }

    void TrySpawnGraveOnAllGravesUnderMAxEnemiesOnScren()
    {

        for (int z = 0; z < 3; z++)
        {
            if (GameManager.Instance.GEtAllives().Count >= Get_LevelMax_Standard_OnScreen())
            {

                break;
            }

            Data_Enemy DE_noTimeStamp_noID = BuildDE(GRavers[z % GRavers.Length], SelfRoundRobin_GRavePoints());

            GameManager.Instance.Spawn_Enemy(DE_noTimeStamp_noID);
        }
    }


    void TrySpawnAXETHROWERUnderMAxEnemiesOnScren()
    {


        if (GameManager.Instance.GEtAllives().Count >= Get_LevelMax_Standard_OnScreen())
        {

            return;
        }

        Data_Enemy DE_noTimeStamp_noID = BuildDE(AxeGuys[0], SelfRoundRobin_SpawnPoints());

        GameManager.Instance.Spawn_Enemy(DE_noTimeStamp_noID);

    }

    int curIndex_SpawnPoints = 0;
    KNode SelfRoundRobin_SpawnPoints()
    {
        curIndex_SpawnPoints++;
        if (curIndex_SpawnPoints >= Get_ListSpawnPointIds().Count) curIndex_SpawnPoints = 0;
        return KnodeProvider.Instance.GetNodeByID(Get_ListSpawnPointIds()[curIndex_SpawnPoints]);
    }
    int curIndex_GravePoints = 0;

    KNode SelfRoundRobin_GRavePoints()
    {
        curIndex_GravePoints++;
        if (curIndex_GravePoints >= Get_ListGravePointIds().Count) curIndex_GravePoints = 0;
        return KnodeProvider.Instance.GetNodeByID(Get_ListGravePointIds()[curIndex_GravePoints]);
    }
    int curIndexTunnelPoints = 0;
    KNode SelfRoundRobin_TunnelPoints()
    {
        curIndexTunnelPoints++;
        if (curIndexTunnelPoints >= Get_ListTunnelSpawnPointIds().Count) curIndexTunnelPoints = 0;
        return KnodeProvider.Instance.GetNodeByID(Get_ListTunnelSpawnPointIds()[curIndexTunnelPoints]);
    }
}

