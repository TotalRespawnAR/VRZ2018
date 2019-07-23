// @Author Jeffrey M. Paquette ©2016
// @Author Nabil Lamriben ©2017
 
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * This standard wave spawns zombies for a certain amount of time, keeping no more
 * than x number of zombies on screen at any time. It uses y percent of all
 * spawn points. Bonus points are awarded for destroying more zombies than the alloted par
 */
public class WaveStandard : MonoBehaviour, IWave {

    public int[] indexesOfZOmbiesForWave;
    //public int[] indexesOfZOmbiesForWave= new int[]   { 1,4,9,11,13,20 }; //wave1
    //2 public int[] indexesOfZOmbiesForWave = new int[] { 1, 4, 9, 11, 13, 20 };
    //3 public int[] indexesOfZOmbiesForWave = new int[] { 1, 4, 9, 11, 13, 20 };
    //4 public int[] indexesOfZOmbiesForWave = new int[] { 1, 4, 9, 11, 13, 20 };

    //shoudl be done by wavenumber
    void SetZindeciesforWaveGun(GunType argWaveGun) {
        if (argWaveGun == GunType.MAGNUM)
        {
            indexesOfZOmbiesForWave = new int[] { 1, 1, 1, 1, 1, 8, 3, 10, 6, 20, 17 }; //wave1
        }
        else
        if (argWaveGun == GunType.PISTOL)
        {
            indexesOfZOmbiesForWave = new int[] { 2, 16, 12, 5, 4, 18, 9, 11, 6, 7, 3, 20 }; //wave2
        }
        else if (argWaveGun == GunType.UZI)
        {
            indexesOfZOmbiesForWave = new int[] { 1, 11, 5, 18, 8, 12, 17, 2, 7, 10, 20, 13, 3, 19, 6, 16, 4, 9 }; //waveall of em
        }
        else {
            indexesOfZOmbiesForWave = new int[] { 1,2,3,4,5,6,7,8,9,10,11,12,13,16,17,18,19,20 };
        }
    }

    //i get the next index here , then use that as an index in settings.variations[] 

    int curIndexInWAVE = -1;
    public int GetNExtIndex() {
        ++curIndexInWAVE;

        if (curIndexInWAVE >= indexesOfZOmbiesForWave.Length || curIndexInWAVE <=0 ) curIndexInWAVE = 0;

        return indexesOfZOmbiesForWave[curIndexInWAVE];
    }


    #region Public_props
    [Tooltip("Total time this wave will be active.")]
    // public float WaveTimerInMinutes;
    public float WavetimeinSeconds;

    [Tooltip("Number of living zombies allowed at any one time.")]
    public int maxZombiesOnScreen;

    [Tooltip("Percentage of spawn points used. Range 0.0 - 1.0")]
    public float percentageOfSpawns;

    [Tooltip("Buffer before the same spawn can respawn a zombie")]
    public float CoolDownTime = 3.5f;


    [Tooltip("Hitpoint range of zombies this wave.")]
    public int minZombieHP, maxZombieHP;

    public int UnlockSpawnAfterNumberOfKills = 1;

    ////private int _deadZombiesCount_for_unlocking_SP = 0;

    public GunType WaveGun;
    public int WaveNumber = 0;
    #endregion

    void OnEnable()
    {
        GameEventsManager.OnSuddenDeath += Run_SuddenDeathWave;
        GameEventsManager.OnTimeToSpawnSpecialZomb += SetisspawningSpecialZombie;
    }

    private void OnDisable()
    {
        GameEventsManager.OnSuddenDeath -= Run_SuddenDeathWave;
        GameEventsManager.OnTimeToSpawnSpecialZomb -= SetisspawningSpecialZombie;

    }


    ////bool _isSpawningSpetialZombie;
    //public void SetisspawningSpecialZombie()
    //{

    //        _isSpawningSpetialZombie = true;

    //}

    #region Private_props

    ////private List<SpawnPoint> _Available_placedSpawnPoints_accordngTOWavePercentOfSpawnUsed;     // reference to all spawn points used by this wave

    //Will get set to true while spawning a special z and back to false when done spawning
    //private bool _isSpawningSpetialZombie = false;

    ////private int maxSpawnPointIndex;                 // the index of the closest spawn used by this wave
    private int _curCountOfZOmbiesOnScree = 0;      // number of zombies living

    ////private bool waveTimeisUp = false;              // is the wave over?

    private bool OnCompleteNotCalledyet = true;


    #endregion

    public bool IsZombieCountOnScreenLow() {
        if (_curCountOfZOmbiesOnScree < maxZombiesOnScreen)
            return true;
        else
            return false;
    }


    //darren brown 

    void Start()
    {
     

        InitSpawnPoints();
        //_isSpawningSpetialZombie = false;

        if (CoolDownTime < 3.5) {
            CoolDownTime = 3.5f;
        }

        SetZindeciesforWaveGun(this.WaveGun);
        //debig  Logger.Debug("_isSpawningSpetialZombie " + _isSpawningSpetialZombie);
    }


    //void Update() {

        //if (OnCompleteNotCalledyet && GameManagerOld.Instance.curgamestate == ARZState.WaveEnded && _curCountOfZOmbiesOnScree <= 0) {
        //    WaveSTD_Completed_callGM_Handle_Pop_newPlusPLus();
        //    OnCompleteNotCalledyet = false;
        //}

   // }


    #region PublicMEthods

    //this was on a timer 5sec in wm the first time
    public void StartWave()
    {
        
       // GameManager.Instance.curgamestate = ARZState.WavePlay;
       //// StartCoroutine(ie_StartWaveTimer());
       // // FillAllSpawnpoints_onlyonStart();
       // StemKitMNGR.Call_GunSetChangeTo(WaveGun);
       // //  StemKitMNGR.CALL_UpdateAvailableGUnIndex((int)WaveGun); //<wave one , get gun 1 wave 2 get gun 12 wave 3 get123 ...
       // StemKitMNGR.CALL_UpdateAvailableGUnIndex(8);
       // StemKitMNGR.CALL_ResetGunAndMeter();
       // StemKitMNGR.CALL_ToggleStemInput(true); //don tt allow switching guns 

       // if (GameSettings.Instance.IsIncrementalSpawningOn) {
       //     //TurnOnFurthestSpawn_and_spawnZ();
       // }
       // else {
       //     FillAllSpawnpoints_onlyonStart();
       // }

    }

    void HEardSuddenDeath() { }


    public void WaveSTD_Completed_callGM_Handle_Pop_newPlusPLus()
    {
       //// ResetSpawnStates();
       // // tell the game manager that wave is completed
       // GameManager.Instance.GM_Handle_WaveCompleteByPoppingNUMplusplus();
    }

    //public void ResetSpawnStates() {
    //   _isSpawningSpetialZombie = false;

    //    if (_Available_placedSpawnPoints_accordngTOWavePercentOfSpawnUsed.Count > 0) {
    //        for (int i = 0; i < _Available_placedSpawnPoints_accordngTOWavePercentOfSpawnUsed.Count; i++)
    //        {
    //            _Available_placedSpawnPoints_accordngTOWavePercentOfSpawnUsed[i].ResetMe();
    //        }
    //    }
    //}

    //public void StopSpawnStates()
    //{
    //     _isSpawningSpetialZombie = false;

    //    if (_Available_placedSpawnPoints_accordngTOWavePercentOfSpawnUsed.Count > 0)
    //    {
    //        for (int i = 0; i < _Available_placedSpawnPoints_accordngTOWavePercentOfSpawnUsed.Count; i++)
    //        {
    //            _Available_placedSpawnPoints_accordngTOWavePercentOfSpawnUsed[i].StopMe();
    //        }
    //    }
    //}


    ////Queue<SpawnPoint> thereadyQFRomWvemanage;
    //public void Wave_isittime_and_canI_spawnNow()
    //{
    //    if (GameManager.Instance.curgamestate == ARZState.EndGame ||
    //        GameManager.Instance.curgamestate == ARZState.WaveBuffer ||
    //        GameManager.Instance.curgamestate == ARZState.WaveOverTime) return;

    //    if (_curCountOfZOmbiesOnScree >= maxZombiesOnScreen || waveTimeisUp || GameManager.Instance.IsPlayerDead)
    //        return;
    //    thereadyQFRomWvemanage = GameManager.Instance.GEtReadyQ();
    //    List<SpawnPoint> AvailableSpawnPointsNotCoolingDown = new List<SpawnPoint>();

    //    foreach (SpawnPoint spawnPoint in thereadyQFRomWvemanage)
    //    {
    //        if (!spawnPoint.IsCoolingDown)
    //        {
    //            AvailableSpawnPointsNotCoolingDown.Add(spawnPoint);
    //        }
    //    }

    //    if (AvailableSpawnPointsNotCoolingDown.Count == 0)
    //    {
    //        ResetSpawnStates();

    //        foreach (SpawnPoint spawnPoint in _Available_placedSpawnPoints_accordngTOWavePercentOfSpawnUsed)
    //        {
                
    //                AvailableSpawnPointsNotCoolingDown.Add(spawnPoint);
                 
    //        }
    //         // Logger.Debug("no cooled down sp fond");
    //      //  return;
    //    }

    //    int i = Random.Range(0, AvailableSpawnPointsNotCoolingDown.Count);
    //    Pass_CalculatedINdex_ofZombieTOspawn(AvailableSpawnPointsNotCoolingDown[i],0);
    //    AvailableSpawnPointsNotCoolingDown[i].StartCoolingDown();
    //}


    //1 caller WM when dequiing 
    public void SpawnOneOfMyZombiesAtSpawn(SpawnPoint argDeQuedSp)
    {

        ////these checks are old and may be redundent  only reduddnt one i see is the check for LowScreenZombieCount, it was already cheked by WM , and that how we got here 
        //if (GameManager.Instance.curgamestate == ARZState.EndGame ||
        //    GameManager.Instance.curgamestate == ARZState.WaveBuffer ||
        //    GameManager.Instance.curgamestate == ARZState.WaveOverTime || 
        //    GameManager.Instance.IsPlayerDead || 
        //    waveTimeisUp) return;

        ////        if (_curCountOfZOmbiesOnScree >= maxZombiesOnScreen || waveTimeisUp || GameManager.Instance.IsPlayerDead)
        

        //Pass_CalculatedINdex_ofZombieTOspawn(argDeQuedSp,0);
        ////ar gHere.StartCoolingDown(); // wavemanager asks the coolingdown 
    }


    public void WaveReload()
    {
       //// GameManager.Instance.Call_WaveStartedOrReset(WaveNumber);

       //// ResetSpawnStates();
       // if (GameSettings.Instance.IsIncrementalSpawningOn)
       // {
       //     //TurnOnFurthestSpawn_and_spawnZ();
       // }
       // else
       // {
       //     FillAllSpawnpoints_onlyonStart();
       // }
    }

 

    public void WaveUpdateKilledZombies_and_CheckIfISTImeToSpawnNew()
    {

      //  //Debug.Log("zkilled probe a spawn");
      //  _curCountOfZOmbiesOnScree--;
      //  _deadZombiesCount_for_unlocking_SP++;

      //  //DecideToUnlockNExtSPorNOt();

      //  if (_curCountOfZOmbiesOnScree <= 0 && GameManager.Instance.curgamestate == ARZState.WaveOverTime)
      //  {
      //      GameManager.Instance.curgamestate = ARZState.WaveEnded;
      //      return;
      //  }

      ////  Wave_isittime_and_canI_spawnNow();
    }

  

    public void OnGameOver()
    {
      //  ResetSpawnStates();
    }



    #endregion


    #region privateMethods

    void Run_SuddenDeathWave() {
     //    CoolDownTime = 2f;
     //   GameSettings.Instance.IsZombieCollisionModeOn = false;
     //   GameSettings.Instance.IsIncrementalSpawningOn = false;

     //   List<GameObject> PlacedSpawnPoints = GameManager.Instance.GetSpawnPoints();
     //   _Available_placedSpawnPoints_accordngTOWavePercentOfSpawnUsed = new List<SpawnPoint>();


     ////   maxZombiesOnScreen = 12;
     //   // calculate max spawn point index used by this array
        
     //       maxSpawnPointIndex = PlacedSpawnPoints.Count;
        

     //   for (int i = 0; i < maxSpawnPointIndex; i++)
     //   {
     //       SpawnPoint spawnPoint = PlacedSpawnPoints[i].GetComponent<SpawnPoint>();
     //       spawnPoint.Init_inactive_and_coolingdown(CoolDownTime);
     //       _Available_placedSpawnPoints_accordngTOWavePercentOfSpawnUsed.Add(spawnPoint);
     //   }

     //   ResetSpawnStates();
     //   FillAllSpawnpoints_onlyonStart();
    }

    //private IEnumerator ie_StartWaveTimer()
    //{
    //   // // yield return new WaitForSeconds(WaveTimerInMinutes * 60);
    //   // yield return new WaitForSeconds(WavetimeinSeconds);
    //   // waveTimeisUp = true;
    //   //// ResetSpawnStates();
    //   // if (_curCountOfZOmbiesOnScree > 0)
    //   // {
    //   //     GameManager.Instance.curgamestate = ARZState.WaveOverTime;

    //   // }
    //   // else
    //   // {
    //   //     GameManager.Instance.curgamestate = ARZState.WaveEnded;
    //   // }
    //}

    private void InitSpawnPoints()
    {
        ////ToDo: 
        //// please make sure to handle "NoSpawns Placed" 
 
        //List<GameObject> PlacedSpawnPoints_All_Of_them = GameManager.Instance.GetSpawnPoints();
        //_Available_placedSpawnPoints_accordngTOWavePercentOfSpawnUsed = new List<SpawnPoint>();


        //// calculate max spawn point index used by this array
        //if (percentageOfSpawns >= 0.0f && percentageOfSpawns <= 1.0f)
        //{
        //    maxSpawnPointIndex = (int)((PlacedSpawnPoints_All_Of_them.Count) * percentageOfSpawns);
        //    if (maxSpawnPointIndex == 0 && PlacedSpawnPoints_All_Of_them.Count > 0)
        //    {
        //        maxSpawnPointIndex = 1;
        //    }
        //}
        //else
        //{
        //    maxSpawnPointIndex = PlacedSpawnPoints_All_Of_them.Count;
        //}

        //for (int i = 0; i < maxSpawnPointIndex; i++)
        //{
        //    SpawnPoint spawnPoint = PlacedSpawnPoints_All_Of_them[i].GetComponent<SpawnPoint>();
        //    spawnPoint.Init_inactive_and_coolingdown(CoolDownTime);
        //    _Available_placedSpawnPoints_accordngTOWavePercentOfSpawnUsed.Add(spawnPoint);
        //}
    }

    ////int indexofLatestActivatedSpwn = 0;

    //void TurnOnFurthestSpawn_and_spawnZ() {
    //    indexofLatestActivatedSpwn = 0;
    //    _Available_placedSpawnPoints_accordngTOWavePercentOfSpawnUsed[indexofLatestActivatedSpwn].TurnOnAndStartCoolingdown();
    //    Pass_CalculatedINdex_ofZombieTOspawn(_Available_placedSpawnPoints_accordngTOWavePercentOfSpawnUsed[indexofLatestActivatedSpwn], 0);
    //}
    //void TurnOnNExtSpawn() {
    //    if (GameManager.Instance.curgamestate == ARZState.WaveOverTime || GameManager.Instance.curgamestate == ARZState.WaveEnded) return;
    //    indexofLatestActivatedSpwn++;
    //    if (indexofLatestActivatedSpwn >= _Available_placedSpawnPoints_accordngTOWavePercentOfSpawnUsed.Count) return;
    //    _Available_placedSpawnPoints_accordngTOWavePercentOfSpawnUsed[indexofLatestActivatedSpwn].TurnOnAndStartCoolingdown();
    //    Pass_CalculatedINdex_ofZombieTOspawn(_Available_placedSpawnPoints_accordngTOWavePercentOfSpawnUsed[indexofLatestActivatedSpwn], 0);
    //}

    //void DecideToUnlockNExtSPorNOt() {
    //    if (UnlockSpawnAfterNumberOfKills == 0) return;
    //    if (_deadZombiesCount_for_unlocking_SP % UnlockSpawnAfterNumberOfKills == 0) TurnOnNExtSpawn();
    //}

    

    ////int Material_Number = 1;
    ////int Zombie_Number = 1;

    void UpdateZombieResourceIndex(int argBossNumber)
    {
        //    if (argBossNumber==14) {
        //    Zombie_Number = 14; Material_Number = 1;
        //}
        //else
        //    if (argBossNumber == 15) {
        //    Zombie_Number = 15; Material_Number = 1;
        //}
        //else
        //if (argBossNumber == 0)
        //{
            
        //    Zombie_Number = GetNExtIndex();
        //    //    if (Zombie_Number <= 0 || Zombie_Number >= GameSettings.Instance.GetZombieVariationsAvailable().Length) {
        //    if (Zombie_Number <= 0 || Zombie_Number >= GameSettings.Instance.UNifiedVariations_123)
        //    {
        //        Zombie_Number = 1; //becoae the first available zombie number is 1 not 0, and the variations in gamesettings only have values for index 1 , not 0
        //    }
        //    // int randMAtNumber_1_12_123 = Random.Range(1 , GameSettings.Instance.GetZombieVariationsAvailable()[Zombie_Number] + 1); //plus 1 cuz randome.range is ceiling exclusive
        //    int randMAtNumber_1_12_123 = Random.Range(1, GameSettings.Instance.UNifiedVariations_123 + 1); //plus 1 cuz randome.range is ceiling exclusive
        //    Material_Number = randMAtNumber_1_12_123;

        //}
       
    }


    void SetisspawningSpecialZombie(int x) // 14= gas 15=bigbutatnt
    {
        //UpdateZombieResourceIndex(x);
        // _isSpawningSpetialZombie = true;

    }

    

    private void Pass_CalculatedINdex_ofZombieTOspawn(SpawnPoint spawnPoint,int argBOssNum)
    {
       // if (GameManager.Instance.curgamestate != ARZState.WavePlay) return;
       // if(!_isSpawningSpetialZombie)
       // UpdateZombieResourceIndex(argBOssNum);

       // int randZombieHP = Random.Range(minZombieHP, maxZombieHP);

       //GameManager.Instance.REQ_Z(spawnPoint.transform, Zombie_Number, Material_Number, randZombieHP, ARZombieypes.STANDARD, ZombieState.WALKING);

       // _curCountOfZOmbiesOnScree++;
       // if (_isSpawningSpetialZombie) _isSpawningSpetialZombie = false;
    }

    

    private void FillAllSpawnpoints_onlyonStart()
    {

        //Debug.Log("FILLIG ALLLLLLL SPs");
        //if (GameManager.Instance.curgamestate == ARZState.EndGame || GameManager.Instance.curgamestate == ARZState.WaveBuffer) return;

        //if (_curCountOfZOmbiesOnScree >= maxZombiesOnScreen || waveTimeisUp || GameManager.Instance.IsPlayerDead)
        //    return;

        //indexofLatestActivatedSpwn = 0;

        //for (int x=0; x<_Available_placedSpawnPoints_accordngTOWavePercentOfSpawnUsed.Count; x++)
        //{
        //    _Available_placedSpawnPoints_accordngTOWavePercentOfSpawnUsed[indexofLatestActivatedSpwn].TurnOnAndStartCoolingdown();
        //    Pass_CalculatedINdex_ofZombieTOspawn(_Available_placedSpawnPoints_accordngTOWavePercentOfSpawnUsed[indexofLatestActivatedSpwn],0); //0 is neither 14 nor 15 lol
        //    indexofLatestActivatedSpwn++;
        //}
    }

    public GunType GetWaveGunType()
    {
        return WaveGun;
    }

    public ScriptWaveScenario GetScenarioObject()
    {
        throw new System.NotImplementedException();
    }



    #endregion

}
