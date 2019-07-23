//#define DISABLE_TICKBATCH
//#define ENABLE_KEYBORADINPUTS
////#define ENABLE_DEBUGLOG

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class EnemyBatchCrafter : MonoBehaviour {
    /*
    public KngEnemyName[] NamesForRoundRobbin_shouldBeShuffeldAnd4timesbigger;
    List<KngEnemyName> RRRinlistform;
    WaveLevel LoadedLevel;
    int RoundRobbinPTR = 0;
    int IcanMake_thisManyZombies;

    Queue<Data_Enemy> PriorityQueue;
    List<Data_Enemy> BATCH;
    int Total_Possible_ZonScreenForThisWaveLevel;
    int Total_Available_SpawnPointsForThisWaveLevel;

    bool everyother = false;
    int Evry4 = 0;

    SpawnTransformsController TRANSFORMctrl;
    //from wavemanager whne start or reload
    public void SetLoadedLEvelRef(WaveLevel argLoadedLevel) {

        if (argLoadedLevel == null)
        {
            return;
        }

        if (PriorityQueue == null) PriorityQueue = new Queue<Data_Enemy>();
        else
            PriorityQueue.Clear();
         RoundRobbinPTR = 0;
        LoadedLevel = argLoadedLevel;
        //how many names in this level
        int howManyZnamesInLevel = LoadedLevel.LevelZombies.Count;
      //  int howmanyAvailableSpawnsINLevel = LoadedLevel.Get_LevelMaxAvailableInitialSpawns();
        KngEnemyName[] temp = LoadedLevel.LevelZombies.ToArray();
        RRRinlistform = new List<KngEnemyName>();
        for (int t = 0; t < howManyZnamesInLevel; t++)
        {
            System.Random r = new System.Random();
            temp = temp.OrderBy(x => r.Next()).ToArray();
            RRRinlistform.AddRange(temp);
        }

        NamesForRoundRobbin_shouldBeShuffeldAnd4timesbigger = RRRinlistform.ToArray();

        Total_Possible_ZonScreenForThisWaveLevel = argLoadedLevel.Get_LevelMaxEnemiesOnScreen();
        Total_Available_SpawnPointsForThisWaveLevel = LoadedLevel.Get_LevelSpawnPointIds().Count;
       // Total_Available_SpawnPointsForThisWaveLevel = 8;
    }


    Data_Enemy Get_Priority() {
        return PriorityQueue.Dequeue();
    }

    void OnEnable()
    {
        GameEventsManager.OnLevelTicked += OnTick;
        GameEventsManager.OnLevelFlyTicked += OnflyTick;
        GameEventsManager.OnOtherHLSmallAttack += OnReceiveSmallAttack;
        GameEventsManager.OnWaveStartedOrReset_DEO += SetLoadedLEvelRef;
        GameEventsManager.OnaddToZQ += ADDtoQ_Formed_DE;
    }

    private void OnDisable()
    {
        GameEventsManager.OnLevelTicked -= OnTick;
        GameEventsManager.OnLevelFlyTicked -= OnflyTick;
        GameEventsManager.OnOtherHLSmallAttack -= OnReceiveSmallAttack;
        GameEventsManager.OnWaveStartedOrReset_DEO -= SetLoadedLEvelRef;
        GameEventsManager.OnaddToZQ -= ADDtoQ_Formed_DE;
    }
    private void Awake()
    {
        TRANSFORMctrl = GetComponent<SpawnTransformsController>();
    }

    bool Q_Has(KngEnemyName argName) {
        if (PriorityQueue.Any<Data_Enemy>(d => d.ModelName == argName))
        {
            return true;
        }
        else
            return false;
    }


    void OnTick(TickBools tb) {

#if DISABLE_TICKBATCH
        return;
#endif
        //if (LoadedLevel.hasMiniBoss)
        //{
        //    if (!LoadedLevel.MiniBossDied)
        //    {
        //        return;
        //    }
        //}


      
        if (LoadedLevel == null  ) { return; }
        if (LoadedLevel.NoTicks())
        {
            return;
        }


        if (GameManager.Instance.KngGameState != ARZState.WavePlay &&
           GameManager.Instance.KngGameState != ARZState.WaveOverTime)
        {
            return;
        }

        if (tb.ShouldGrave)
        {
#if ENABLE_DEBUGLOG
            Debug.Log("BT grave");
#endif
            //Evry4++;

            //everyother = !everyother;
            //if (Evry4 % 2 == 0 ) {
            //    if (Random.Range(1, 5) % 2 == 0)
            //    {
            //     Data_Enemy gassdata = new Data_Enemy(0, KngEnemyName.SKELETON,  1, ARZombieypes.GRAVER, EnemyAnimatorState.GRAVING,   LoadedLevel.Get_Initial_Zstate()); 
            //        ADDtoQ(gassdata);
            //    }
            //    else
            //    {
            //        Data_Enemy gassdata = new Data_Enemy(0, KngEnemyName.MUMMY, 1, ARZombieypes.GRAVER, EnemyAnimatorState.GRAVING,  LoadedLevel.Get_Initial_Zstate());
            //        ADDtoQ(gassdata);
            //    }
            //}
        }

        if (tb.ShouldGassmask)
        {
#if ENABLE_DEBUGLOG
            Debug.Log("BT gass");
#endif
            //if (!Q_Has(KngEnemyName.GASMASK))
            //{
            //    Data_Enemy gassdata = new Data_Enemy(1, KngEnemyName.GASMASK,1, ARZombieypes.STANDARD, LoadedLevel.Get_Initial_Zstate(), LoadedLevel.Get_Initial_Zstate());
            //    ADDtoQ(gassdata);
            //}

        }

        if (tb.ShouldBigMutant)
        {
#if ENABLE_DEBUGLOG
            Debug.Log("BT mutant");
#endif
            //if (!Q_Has(KngEnemyName.BIGMUTANT))
            //{
            //    Data_Enemy bigmut = new Data_Enemy(1, KngEnemyName.BIGMUTANT, 1, ARZombieypes.STANDARD, LoadedLevel.Get_Initial_Zstate(), LoadedLevel.Get_Initial_Zstate());
            //    ADDtoQ(bigmut);
            //}

        }
        if (tb.Shouldfighters)
        {
#if ENABLE_DEBUGLOG
            Debug.Log("BT fighter");
#endif
            //if (!Q_Has(KngEnemyName.KNIFEFIGHTER))
            //{
            //    Data_Enemy de = new Data_Enemy(1, KngEnemyName.KNIFEFIGHTER, 1, ARZombieypes.STANDARD, LoadedLevel.Get_Initial_Zstate(), LoadedLevel.Get_Initial_Zstate());
            //    ADDtoQ(de);
            //}
               
        }
        if (tb.Shouldteeth)
        {
#if ENABLE_DEBUGLOG
            Debug.Log("BT teeth");
#endif
            //if (!Q_Has(KngEnemyName.PARASITE))
            //{
            //    Data_Enemy de = new Data_Enemy(1, KngEnemyName.PARASITE, 1, ARZombieypes.STANDARD, LoadedLevel.Get_Initial_Zstate(), LoadedLevel.Get_Initial_Zstate());
            //    ADDtoQ(de);
            //}

        }
        if (tb.ShouldClaws)
        {

#if ENABLE_DEBUGLOG
            Debug.Log("BT claw");
#endif
            //if (!Q_Has(KngEnemyName.MUTANTBLUE))
            //{
            //    Data_Enemy de = new Data_Enemy(1, KngEnemyName.MUTANTBLUE, 1, ARZombieypes.STANDARD, LoadedLevel.Get_Initial_Zstate(), LoadedLevel.Get_Initial_Zstate());
            //    ADDtoQ(de);
            //}

        }

        //if (tb.Shouldpumpkins)
        //{
        //    if (!Q_Has(KngEnemyName.PUMPKIN ))
        //    {
        //        Data_Enemy de = new Data_Enemy(1, KngEnemyName.PUMPKIN, 1, ARZombieypes.STANDARD, LoadedLevel.Get_Initial_Zstate(), LoadedLevel.Get_Initial_Zstate());
        //        ADDtoQ(de);
        //    }
        //}

        List<Data_Enemy> batch = CraftBatch();

        int LiveZombies = GameManager.Instance.ENEMYMNGER_getter().liveenemies.Count;
      
        //  Debug.Log("max for level "+ Total_Possible_ZonScreenForThisWaveLevel + " but already alive " + LiveZombies);
        if (LiveZombies >= Total_Possible_ZonScreenForThisWaveLevel) return;
#if ENABLE_LOGS
        Debug.Log("Tick " + tb.TickTime);
#endif
        int allverts = 0;
      //  Debug.Log("batch size at  = " + LoadedLevel.name + "  is" + batch.Count);
        for (int i = 0; i < batch.Count; i++)
        {
          //  Debug.Log(batch[i].ModelName);
           allverts += GameSettings.Instance.GetEnemyModel_Verts_FullRez(batch[i].ModelName);
        }
#if ENABLE_LOGS
        Debug.Log("total verts for wave " + allverts );
#endif
        // TRANSFORMctrl.DoWorkWithThisBatch(CraftBatch());
    }

    List<Data_Enemy> CraftBatch()
    {
        BATCH = new List<Data_Enemy>();
        int LiveZombies = GameManager.Instance.ENEMYMNGER_getter().liveenemies.Count;

        IcanMake_thisManyZombies = Total_Possible_ZonScreenForThisWaveLevel - LiveZombies;
        // Live == max or More Live than Max /// thos shuld never happen 
        if (IcanMake_thisManyZombies <= 0) return BATCH; //will be null here 

        //ok so , we got a live zombie defficiency
        //let see how many spawns are available

        if (IcanMake_thisManyZombies > Total_Available_SpawnPointsForThisWaveLevel)
        {
            IcanMake_thisManyZombies = Total_Available_SpawnPointsForThisWaveLevel;
        }
        //IcanMake_thisManyZombies++;
        //now i can only make this many . at most one per available spawn point . if game is too slow , try using another queue in transformCTRL . 
        // the transfromCTRL wou;d get a lis of 10, but only have 3 available zombie, it will spawn all 3 , and wait half the time of a tick and spawn the rest 
        //  or
        // i could use the spawnNEAR to spawn the excess


        //check the priority que 


       // BATCH = new List<Data_Enemy>();
        //...............here pq
        Try_FillBatchWithPriorityQ();

        if (IcanMake_thisManyZombies > 0)
        {
            for (int i = 0; i < IcanMake_thisManyZombies; i++)
            {
                BATCH.Add(CraftaZombie_Data_Enemy(NamesForRoundRobbin_shouldBeShuffeldAnd4timesbigger[RoundRobbinPTR]));
                RoundRobbinPTR++;
                if (RoundRobbinPTR >= NamesForRoundRobbin_shouldBeShuffeldAnd4timesbigger.Length) RoundRobbinPTR = 0;
            }

        }

      //  foreach (Data_Enemy de in BATCH) {
            
      //  }

      ////  Data_Enemy EP1 = new Data_Enemy(0, 1, KngEnemyName.BASIC, 40, ARZombieypes.STANDARD, ZombieState.IDLE, new Vector3(0, 180, 0), GameManager.Instance.Get_SceneObjectsManager().KnodesMNGR.GetNode(50));
      //  Data_Enemy CmpletedNoID = BATCH[0];
      //  CmpletedNoID.InitialRotEuler = new Vector3(0, 180, 0);
      //  CmpletedNoID.SpawnKnode = KnodeProvider.Instance.Get_RoundRobin_Spawnpoint();


        foreach (Data_Enemy de in BATCH)
        {
            de.InitialRotEuler = new Vector3(0, 180, 0);
            if (de.InitialState == EnemyAnimatorState.GRAVING) {
                de.SpawnKnode = KnodeProvider.Instance.Get_RoundRobin_Gravepoint();
            }
            else
            {

            de.SpawnKnode = KnodeProvider.Instance.Get_RoundRobin_Spawnpoint();
            }
            GameManager.Instance.Spawn_Enemy(de);
        }

        // GameManager.Instance.Spawn_Enemy(BATCH[0]);
        return BATCH;
    }

    Data_Enemy CraftaZombie_Data_Enemy(KngEnemyName argnam)
    {

        int _mat = Random.Range(1, 4);
        int _hitP = LoadedLevel.Get_LevelHP();
        ARZombieypes _ztype = ARZombieypes.STANDARD; //should just be standard wor wavelevel zombies 
        EnemyAnimatorState _initState = LoadedLevel.Get_Level_AnimState(); //id;e walking
        ARZPowerups _powerupDrop = ARZPowerups.NONE;
        bool _isPAtrolling = false;
        Data_Enemy craftedData = new Data_Enemy(-1, argnam, _hitP, _ztype, _initState, _initState);
        return craftedData;
    }



    void OnflyTick() {
        Debug.Log("BROKEN");

        //TRANSFORMctrl.SpawnFly();
    }

    void OnReceiveSmallAttack() {
        Debug.Log("BROKEN");

        // TRANSFORMctrl.SpawnFly();
        // TRANSFORMctrl.SpawnAxeGuy();
    }
    void OnReceiveBigAttack()
    {
        Debug.Log("BROKEN");

        // TRANSFORMctrl.SpawnFly();//try fly 2 or axguy
        //  TRANSFORMctrl.SpawnAxeGuy();
    }
    void ADDtoQ_Formed_DE(Data_Enemy de) {
        if (PriorityQueue == null) PriorityQueue = new Queue<Data_Enemy>();
        PriorityQueue.Enqueue(de);
    }





    //int extra3gravers = 0;
    //void FAKE3zombies_cuzDissabeledTICKER() {
    //    for (int x = 0; x < extra3gravers ; x++){
    //        Data_Enemy fakegraver = new Data_Enemy(0, KngEnemyName.SKELETON, 10, ARZombieypes.GRAVER, EnemyAnimatorState.GRAVING, LoadedLevel.Get_Initial_Zstate());

    //        ADDtoQ(fakegraver);
    //    }
    //}
    //List<Data_Enemy> MAKEFAKE_BATCH() {
    //    BATCH = new List<Data_Enemy>();
    //    int LiveZombies = GameManager.Instance.ENEMYMNGER_getter().liveenemies.Count;
    //    IcanMake_thisManyZombies = Total_Possible_ZonScreenForThisWaveLevel - LiveZombies  ;
    //    // Live == max or More Live than Max /// thos shuld never happen 
    //    if (IcanMake_thisManyZombies <= 0) return  BATCH; //will be null here 
    //    if (IcanMake_thisManyZombies > Total_Available_SpawnPointsForThisWaveLevel)
    //    {
    //        IcanMake_thisManyZombies = Total_Available_SpawnPointsForThisWaveLevel;
    //    }
    //    FAKE3zombies_cuzDissabeledTICKER();//adds 3 gravers to priorityQ
    //    //...............here pq
    //   // Try_FillBatchWithPriorityQ();
    //    if (IcanMake_thisManyZombies > 0)
    //    {
    //        for (int i = 0; i < IcanMake_thisManyZombies; i++)
    //        {
    //            BATCH.Add(CraftaZombie_Data_Enemy(NamesForRoundRobbin_shouldBeShuffeldAnd4timesbigger[RoundRobbinPTR]));
    //            RoundRobbinPTR++;
    //            if (RoundRobbinPTR >= NamesForRoundRobbin_shouldBeShuffeldAnd4timesbigger.Length) RoundRobbinPTR = 0;
    //        }
    //    }

    //    foreach (Data_Enemy de in BATCH)
    //    {
    //        de.InitialRotEuler = new Vector3(0, 180, 0);
    //        //  de.SpawnKnode = KnodeProvider.Instance.Get_RoundRobin_TESTSpawnpoint();
    //        de.SpawnKnode = KnodeProvider.Instance.Get_RoundRobin_Spawnpoint();
    //        GameManager.Instance.Spawn_Enemy(de);
    //    }
    //    // Data_Enemy CmpletedNoID = BATCH[0];
    //    //CmpletedNoID.InitialRotEuler = new Vector3(0, 180, 0);
    //    //CmpletedNoID.SpawnKnode = KnodeProvider.Instance.Get_RoundRobin_Spawnpoint();



    //    // GameManager.Instance.Spawn_Enemy(BATCH[0]);
    //    return BATCH;
    //}




    void Try_FillBatchWithPriorityQ() {
        if (PriorityQueue == null) {
           // Debug.Log("PriorityQ is null");
            return; }
        if (PriorityQueue.Count == 0) {
            //Debug.Log("PriorityQ is empty");
            return;

        }
        int cashedPQcount = PriorityQueue.Count;
       // for (int x = 0; x < cashedPQcount; x++)
      //  {
            BATCH.Add(PriorityQueue.Dequeue());
            IcanMake_thisManyZombies--;
      //  }
    }

    //void Start () {

    //}

    //// Update is called once per frame

#if ENABLE_KEYBORADINPUTS
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MAKEFAKE_BATCH();
        }
      

       
    }
#endif
    //void printallnames()
    //{
    //    foreach (ARZnames n in NamesForRoundRobbin_shouldBeShuffeldAnd4timesbigger)
    //    {
    //        Debug.Log(n);
    //    }
    //}
    */
}
