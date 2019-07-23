using System.Collections;
using UnityEngine;

public class WaveLevelPumPkinBoss : WaveLevel
{

    private float eventTime;
    int WaveLevelCurSec = 0;
    public int SpawnRate;
    public int GraveRate;


    public int Time_StartGrave;
    public int Time_IncreaseAgro;

    public int BossHP;
    public int BossSpawnNodeId;

    bool waveTimeReached;
    public bool ReplenishAllSpawnsOnAllDied;
    bool SCRIPTISON;
    protected override void Start()
    {
        StartScriptedWave();
        base.Start();
    }
    public override void BaseStartLevel_WAVEPLAY()
    {
        Debug.Log("start scripted wave BOSS ");
        CounterCoroutingUp = StartCoroutine(CountUp());
        if (startlock) return;
        SpawnPumpkinBoss();
        startlock = true;

    }
    KngEnemyName[] LowLevelZobies = new KngEnemyName[] { KngEnemyName.BASIC, KngEnemyName.HOODIE, KngEnemyName.SHITFACE, KngEnemyName.SPINAL, KngEnemyName.SWEATER, KngEnemyName.HILLBILLY };

    KngEnemyName[] PreyGravers = new KngEnemyName[] { KngEnemyName.MUMMY, KngEnemyName.SKELETON };

    KngEnemyName[] PredatorBoss = new KngEnemyName[] { KngEnemyName.PUMPKIN, KngEnemyName.PARASITE };
    private Coroutine CounterCoroutingUp;
    bool startlock;
    void StartScriptedWave()
    {

    }



    void SpawnPumpkinBoss()
    {
        if (BossHP < 10) BossHP = 100;
        if (BossSpawnNodeId < 2 || BossSpawnNodeId > 4) { BossSpawnNodeId = 3; }


        Data_Enemy PumpkinBoss_ED = new Data_Enemy(
               0,
               -1,
               PredatorBoss[0],
               ARZombieypes.STD_BOSS,
               BossHP,
               3,
               Get_LevelBaseSeekSpeed(),
               new Vector3(0, 180, 0),
              GEtNodeByDirrectID(BossSpawnNodeId));

        GameManager.Instance.Spawn_Enemy(PumpkinBoss_ED);
    }


    void TrySpawnGraveOnAllGravesUnderMAxEnemiesOnScren()
    {

        EBSTATE BaseAnimationForLevel;


        for (int z = 0; z < 3; z++)
        {
            if (GameManager.Instance.GEtAllives().Count >= Get_LevelMax_Standard_OnScreen())
            {

                break;
            }

            Data_Enemy DE_noTimeStamp_noID = new Data_Enemy(
                0,
                -1,
                PreyGravers[z % PreyGravers.Length],
                ARZombieypes.PREYgrave,
                Get_LevelHP(),
                Get_LevelHItStrength(),
                Get_LevelBaseSeekSpeed(),
                new Vector3(0, 180, 0),
                SelfRoundRobin_GRavePoints());

            GameManager.Instance.Spawn_Enemy(DE_noTimeStamp_noID);
        }
    }
    void TrySpawnOnAllSpawnUnderMAxEnemiesOnScren(EBSTATE argBaseAnimationForLevel, bool overridebaseanim)
    {
        if (SCRIPTISON) return;
        EBSTATE BaseAnimationForLevel;


        if (overridebaseanim) BaseAnimationForLevel = argBaseAnimationForLevel;
        for (int z = 0; z < Get_ListSpawnPointIds().Count; z++)
        {
            if (GameManager.Instance.GEtAllives().Count >= Get_LevelMax_Standard_OnScreen())
            {

                break;
            }

            Data_Enemy DE_noTimeStamp_noID = new Data_Enemy(
                0,
                -1,
                LowLevelZobies[z % LowLevelZobies.Length],
                ARZombieypes.STANDARD,
                Get_LevelHP(),
                Get_LevelHItStrength(),
                Get_LevelBaseSeekSpeed(),
                new Vector3(0, 180, 0),
                SelfRoundRobin_SpawnPoints());

            GameManager.Instance.Spawn_Enemy(DE_noTimeStamp_noID);
        }
    }

    bool once = false;

    public override void One_Boss_Died()
    {
        SCRIPTISON = false;
        if (once) return;
        // TrySpawnOnAllSpawnUnderMAxEnemiesOnScren(EnemyAnimatorState.HYPERCHASING, true);
        once = true;
    }
    IEnumerator CountUp()
    {

        while (WaveLevelCurSec <= Get_LevelTime())
        {
            yield return new WaitForSeconds(1);
            WaveLevelCurSec++;
#if ENABLE_DEBUGLOG
            Debug.Log("sec=" + WaveLevelCurSec);
            Debug.Log("WaveLevelCurSec" + WaveLevelCurSec);
#endif

            //if (WaveLevelCurSec % SpawnRate == 0)
            //{
            //    TrySpawnOnAllSpawnUnderMAxEnemiesOnScren(EnemyAnimatorState.CHASING, true);
            //}
            //else
            //if (WaveLevelCurSec % GraveRate == 0)
            //{
            //    TrySpawnOnAllSpawnUnderMAxEnemiesOnScren(EnemyAnimatorState.CHASING, true);
            //}
        }

        waitforallenemiestobedead_setStateOVERTIME();
    }

    KNode GEtNodeByDirrectID(int NodeID)
    {
        return KnodeProvider.Instance.GetNodeByID(NodeID);
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
}
