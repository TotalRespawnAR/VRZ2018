using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveLevelScripted : WaveLevel
{

    private float eventTime;
    int WaveLevelCurSec = 0;
    public int SpawnRate;
    public int GraveRate;

    public int Time_StartGrave;
    public int Time_IncreaseAgro;
    bool waveTimeReached;
    public bool ReplenishAllSpawnsOnAllDied;
    protected override void Start()
    {

        StartScriptedWave();
        base.Start();
    }
    public override void BaseStartLevel_WAVEPLAY()
    {
        Debug.Log("start scripted wave");
        CounterCoroutingUp = StartCoroutine(CountUp());
       
    }
    KngEnemyName[] LowLevelZobies = new KngEnemyName[] { KngEnemyName.BASIC, KngEnemyName.HOODIE, KngEnemyName.SHITFACE, KngEnemyName.SPINAL, KngEnemyName.SWEATER, KngEnemyName.HILLBILLY };

    KngEnemyName[] PreyGravers = new KngEnemyName[] { KngEnemyName.MUMMY, KngEnemyName.SKELETON};

    KngEnemyName[] PredatorBoss = new KngEnemyName[] { KngEnemyName.PUMPKIN, KngEnemyName.PARASITE };
    private Coroutine CounterCoroutingUp;
    bool startlock;
    void StartScriptedWave() {
        if (startlock) return;
        Invoke("Spawn1PredatorBoss", 1);
        Invoke("Spawn1PreyGraver", 1);
       // Invoke("Spawn6LowLevelGrunts", 4);
        startlock = true;
        // Spawn6LowLevelGrunts();



    }

    bool SCRIPTISON;




    void Spawn1PreyGraver()
    {
        SCRIPTISON = true;
        EBSTATE BaseAnimationForLevel;
   
        for (int z = 0; z < 1; z++)
        {

            Data_Enemy EP1 = new Data_Enemy(
                0,
                -1,
                PreyGravers[0],
                    ARZombieypes.PREYgrave,
            Get_LevelHP(),
             Get_LevelHItStrength(),
             Get_LevelBaseSeekSpeed(),
             new Vector3(0, 180, 0),
                SelfRoundRobin_GRavePoints());

            GameManager.Instance.Spawn_Enemy(EP1);
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
    void Spawn1PredatorBoss() {

   
        Data_Enemy EP1 = new Data_Enemy(
                       0,
                -1,
                PreyGravers[0],
                    ARZombieypes.PREDATOR,
            Get_LevelHP(),
             Get_LevelHItStrength(),
             Get_LevelBaseSeekSpeed(),
             new Vector3(0, 180, 0),
              SelfRoundRobin_SpawnPoints());

        GameManager.Instance.Spawn_Enemy(EP1);
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
                ARZombieypes.STANDARD,
                    Get_LevelHP(),
             Get_LevelHItStrength(),
             Get_LevelBaseSeekSpeed(),
                new Vector3(0, 180, 0),
                SelfRoundRobin_GRavePoints());

            GameManager.Instance.Spawn_Enemy(DE_noTimeStamp_noID);
        }
    }
    void TrySpawnOnAllSpawnUnderMAxEnemiesOnScren(EBSTATE argBaseAnimationForLevel, bool overridebaseanim, SeekSpeed seekspeed)
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

    void SpawnPumbkinPredator() { }
    bool once = false;

   public  override void HEardPredatorPointed()
    {
        SCRIPTISON = false;
        if (once) return;
        // TrySpawnOnAllSpawnUnderMAxEnemiesOnScren(EBSTATE.SEEKING_eb1, true);
        Debug.Log("IMPLEMENTME");
        // Spawn6LowLevelGrunts", 5);
        //base.HEardPredatorPointed();
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
            //    TrySpawnOnAllSpawnUnderMAxEnemiesOnScren(EBSTATE.CHASING, true);
            //}else
            //if (WaveLevelCurSec % GraveRate == 0)
            //{
            //    TrySpawnOnAllSpawnUnderMAxEnemiesOnScren(EBSTATE.CHASING, true);
            //}
            Debug.Log("IMPLEMENTME");
        }

        waitforallenemiestobedead_setStateOVERTIME();
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
//void Spawn6LowLevelGruntsNOSTOP()
//{

//    if (once) return;


//    EnemyAnimatorState BaseAnimationForLevel;
//    if (Get_LevelBaseAgro() == AgroLevel.High) { BaseAnimationForLevel = EnemyAnimatorState.HYPERCHASING; }
//    else
//    if (Get_LevelBaseAgro() == AgroLevel.High) { BaseAnimationForLevel = EnemyAnimatorState.CHASING; }
//    else
//    { BaseAnimationForLevel = EnemyAnimatorState.WALKING; }
//    if (once) { BaseAnimationForLevel = EnemyAnimatorState.HYPERCHASING; }

//    for (int z = 0; z < 8; z++)
//    {

//        Data_Enemy EP1 = new Data_Enemy(
//            0,
//            -1,
//            LowLevelZobies[z % LowLevelZobies.Length],
//            Get_LevelHP(),
//            ARZombieypes.STANDARD,
//            EnemyAnimatorState.IDLE,
//            BaseAnimationForLevel,
//            new Vector3(0, 180, 0),
//             SelfRoundRobin_SpawnPoints());

//        GameManager.Instance.Spawn_Enemy(EP1);
//    }
//}
//void Spawn6LowLevelGrunts() {
//    if (STOPSPAWNING) { return; }



//    EnemyAnimatorState BaseAnimationForLevel;
//    if (Get_LevelBaseAgro() == AgroLevel.High) { BaseAnimationForLevel = EnemyAnimatorState.HYPERCHASING; }
//    else
//    if (Get_LevelBaseAgro() == AgroLevel.High) { BaseAnimationForLevel = EnemyAnimatorState.CHASING; }
//    else
//    { BaseAnimationForLevel = EnemyAnimatorState.WALKING; }
//    if (once) { BaseAnimationForLevel = EnemyAnimatorState.HYPERCHASING; }

//    for (int z = 0; z < 8; z++) {

//        Data_Enemy EP1 = new Data_Enemy(
//            0,
//            -1,
//            LowLevelZobies[z% LowLevelZobies.Length],
//            Get_LevelHP(),
//            ARZombieypes.STANDARD,
//            EnemyAnimatorState.IDLE,
//            BaseAnimationForLevel,
//            new Vector3(0, 180, 0),
//             SelfRoundRobin_SpawnPoints());

//        GameManager.Instance.Spawn_Enemy(EP1);
//    }
//}
//void SpawnEnemytypes(int number, ARZombieypes argtype, EnemyAnimatorState firstanim )
//{

//    EnemyAnimatorState BaseAnimationForLevel;
//    if (Get_LevelBaseAgro() == AgroLevel.High) { BaseAnimationForLevel = EnemyAnimatorState.HYPERCHASING; }
//    else
//    if (Get_LevelBaseAgro() == AgroLevel.High) { BaseAnimationForLevel = EnemyAnimatorState.CHASING; }
//    else
//    { BaseAnimationForLevel = EnemyAnimatorState.WALKING; }


//    if (once) { BaseAnimationForLevel = EnemyAnimatorState.HYPERCHASING; }
//    KngEnemyName[] temp;
//    if (argtype == ARZombieypes.PREDATOR)
//    {
//        temp = PredatorBoss;
//        Data_Enemy EP1 = new Data_Enemy(
//          0,
//          -1,
//          PredatorBoss[0],
//          Get_LevelHP(),
//          argtype,
//          EnemyAnimatorState.IDLE,
//          BaseAnimationForLevel,
//          new Vector3(0, 180, 0),
//           SelfRoundRobin_SpawnPoints());

//        GameManager.Instance.Spawn_Enemy(EP1);
//    }
//    else if (argtype == ARZombieypes.PREYgrave)
//    {
//        temp = PreyGravers;
//        for (int z = 0; z < number; z++)
//        {

//            Data_Enemy EP1 = new Data_Enemy(
//                0,
//                -1,
//                temp[z% temp.Length],
//                Get_LevelHP(),
//                argtype,
//                firstanim,
//                BaseAnimationForLevel,
//                new Vector3(0, 180, 0),
//                SelfRoundRobin_GRavePoints()); //------------action needed

//            GameManager.Instance.Spawn_Enemy(EP1);
//        }
//    }
//    else
//    {
//        temp = LowLevelZobies;
//        for (int z = 0; z < number; z++)
//        {

//            Data_Enemy EP1 = new Data_Enemy(
//                0,
//                -1,
//                temp[z % temp.Length],
//                Get_LevelHP(),
//                argtype,
//                firstanim,
//                BaseAnimationForLevel,
//                new Vector3(0, 180, 0),
//                SelfRoundRobin_SpawnPoints()); //------------action needed

//            GameManager.Instance.Spawn_Enemy(EP1);
//        }
//    }


//}