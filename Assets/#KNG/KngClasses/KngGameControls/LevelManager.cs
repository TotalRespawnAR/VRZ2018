// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections;
using UnityEngine;


public class LevelManager : MonoBehaviour
{
    #region props
    public int LevelTosToUse;
    GameObject[] Levels_OBJS;
    public GameObject[] Level_1;
    public GameObject[] Level_2;
    public GameObject[] Level_3;
    public GameObject[] Level_4;
    public GameObject[] Level_5;
    public GameObject[] Level_6;
    public GameObject[] Level_7;
    public GameObject[] Level_8;
    GameObject _curLevelObjInstance;
    ILevel _curIlevel;
    ILevelProps _curLevelProps;
    int curLodedLevel_Num = 0;
    public int Get_Cure_LoadedLevel_Num() { return curLodedLevel_Num; }
    public string Get_Cure_LoadedLevel_NumPliusOne()
    {
        int outnum = curLodedLevel_Num + 1;
        return outnum.ToString();
    }
    ////WaveLevel Cur_WL;


    #endregion


    void Awake()
    {

        if (LevelTosToUse == 0)
        {
            Levels_OBJS = new GameObject[] { Level_2[0], Level_2[1], Level_2[2], Level_2[3], Level_2[4], Level_2[5], Level_2[6], Level_2[7] };

        }
        else
            if (LevelTosToUse == 1)
        {
            Levels_OBJS = new GameObject[] { Level_2[0], Level_1[1], Level_2[2], Level_3[2] };
        }
        else
            if (LevelTosToUse == 2)
        {
            Levels_OBJS = new GameObject[] { Level_3[0], Level_2[0], Level_1[0], Level_3[1], Level_1[1], Level_3[2] };
        }


        if (Levels_OBJS.Length == 0)
        {
            print("NO WAVES LOADED");
            return;
        }






        for (int storedLevelIndex = 0; storedLevelIndex < Levels_OBJS.Length; storedLevelIndex++)
        {
            SetWaveTimesByWaveIndex(storedLevelIndex);
            //........................................           Levels_OBJS[storedLevelIndex].GetComponent<WaveLevel>().Set_LevelNumber(storedLevelIndex + 1);
        }

        _curLevelObjInstance = Instantiate(Levels_OBJS[curLodedLevel_Num]);
        _curIlevel = _curLevelObjInstance.GetComponent<ILevel>();

    }
    private void Start()
    {

        //if (GameManager.Instance.pubHardCodedWaves)
        //{
        //   print("  WAVES hardcoded ");
        //for (int storedLevelIndex = 0; storedLevelIndex < Levels_OBJS.Length; storedLevelIndex++)
        //{
        //    SetWaveTimesByWaveIndex(storedLevelIndex);
        //    //........................................           Levels_OBJS[storedLevelIndex].GetComponent<WaveLevel>().Set_LevelNumber(storedLevelIndex + 1);
        //}
        // }

    }

    #region setWave from settings
    void SetWaveTimesByWaveIndex(int argWaveIndex)
    {




        Levels_OBJS[argWaveIndex].GetComponent<WaveLevel>().iSet_LevelNumber(argWaveIndex + 1);
        Levels_OBJS[argWaveIndex].GetComponent<WaveLevel>().iSet_LevelTime(GameSettings.Instance.GetHardCodedWaveTime(argWaveIndex));
        Levels_OBJS[argWaveIndex].GetComponent<WaveLevel>().iSet_LevelMaxEnemiesOnScreen(GameSettings.Instance.GetHardCodedMazOnScereen(argWaveIndex));
        Levels_OBJS[argWaveIndex].GetComponent<WaveLevel>().iSet_LevelMax_Gravers_OnScreen(GameSettings.Instance.GetHardCodedMax_Graves_OnScereen(argWaveIndex));
        Levels_OBJS[argWaveIndex].GetComponent<WaveLevel>().iSet_LevelMax_Sprinters_OnScreen(GameSettings.Instance.GetHardCodedMax_Sprinters_OnScereen(argWaveIndex));
        Levels_OBJS[argWaveIndex].GetComponent<WaveLevel>().iSet_LevelMax_Axers_OnScreen(GameSettings.Instance.GetHardCodedWave_MAx_Axemen(argWaveIndex));
        Levels_OBJS[argWaveIndex].GetComponent<WaveLevel>().iSet_LevelSeekSpeed(GameSettings.Instance.GetHardCodedWaveSpeed(argWaveIndex));
        Levels_OBJS[argWaveIndex].GetComponent<WaveLevel>().iSetLevelHP(GameSettings.Instance.GetHardCodedWaveHP(argWaveIndex));
        Levels_OBJS[argWaveIndex].GetComponent<WaveLevel>().iSetLevelHItstrength(GameSettings.Instance.GetHardCodedWaveHitStregths(argWaveIndex));
        Levels_OBJS[argWaveIndex].GetComponent<WaveLevel>().iSet_LevelGunType(GameSettings.Instance.GetHardCodedWaveMainGuns(argWaveIndex), GameSettings.Instance.GetHardCodedWaveSecondaryGuns(argWaveIndex));
        Levels_OBJS[argWaveIndex].GetComponent<WaveLevel>().iSet_listSs(GameSettings.Instance.GetHardCodedWave_Available_SPawnIds(argWaveIndex), GameSettings.Instance.GetHardCodedWave_Available_GraveIds(argWaveIndex));




        BasicLevelTest btw = Levels_OBJS[argWaveIndex].GetComponent<BasicLevelTest>();
        if (btw != null)
        {
            btw.HasBos = GameSettings.Instance.GetHardCodedHasBoss(argWaveIndex);
            btw.BossTime = GameSettings.Instance.GetHardCodedWaveBossTimes(argWaveIndex);
            btw.SpawnRate = GameSettings.Instance.GetHardCodedWave_Spawn_Rates(argWaveIndex);
            btw.GraveRate = GameSettings.Instance.GetHardCodedWave_Grave_Rates(argWaveIndex);
            btw.Time_StartGrave = GameSettings.Instance.GetHardCodedWave_Grave_TimeStart(argWaveIndex);
            btw.Time_IncreaseAgro = GameSettings.Instance.GetHardCodedWave_Speed_IncreaseStart(argWaveIndex);
            btw.Times_Place_Axeguy = GameSettings.Instance.GetHardCodedWave_AxeGue_Times(argWaveIndex);
            btw.Times_place_Sprinter = GameSettings.Instance.Get__SPRINTER_TIMES(argWaveIndex);
            btw.pickfrom = GameSettings.Instance.GetHardCodedWave_Enemy_Names(argWaveIndex);

        }
        else
        {
            Debug.LogError("this is not a basicwavelevel");
        }

        //


    }

    #endregion
    #region newResponsibilities


    private void OnEnable()
    {

    }
    private void OnDisable()
    {

    }
    //private void Awake()
    //{
    //    _batchCrafterInWM = GetComponent<EnemyBatchCrafter>();

    //}

    #endregion
    public void Start_LoadedLEvel_Bufftime_W_start_level_ONTIMER(float time)
    {
        if (GameManager.Instance.KngGameState == ARZState.ReachedAllowedTime) return;

        GameManager.Instance.KngGameState = ARZState.WaveBuffer;
        // TimerBehavior t = gameObject.AddComponent<TimerBehavior>();
        // t.StartTimer(time, WM_Start_CurLoadedLevel_Level);
        //WM_Start_CurLoadedLevel_Level();
        StartCoroutine(StartWaiveTimer(time));
    }

    IEnumerator StartWaiveTimer(float x)
    {
        yield return new WaitForSeconds(x);
        WM_Start_CurLoadedLevel_Level();
    }

    public void WM_Start_CurLoadedLevel_Level()
    {
        if (ElevatorController.Instance != null)
        {
            ElevatorController.Instance.OpenDoor();
        }
        else
        {
            Debug.Log("NoElevator");
        }


        _curIlevel.StartLevel_WAVEPLAY();
    }
    public void WM_ReloadLevel()
    {
        //  StemKitMNGR.CALL_ResetGunAndMeter();
        GameManager.Instance.ResetWave();
        WaveNotCompleted_ButRepop();
    }



    public void LevelCompleted_NextLevel()
    {
        if (GameManager.Instance.KngGameState == ARZState.ReachedAllowedTime) return;
        curLodedLevel_Num++;
        if (curLodedLevel_Num == Levels_OBJS.Length)
        {
            GameManager.Instance.HardStop();
            return;
        }

        if (ElevatorController.Instance != null)
        {
            ElevatorController.Instance.CloseDoor();
        }
        else
        {
            Debug.Log("NoElevator");
        }

        //.................................................       _curIlevel.CloseBotheDoors();
        Destroy(_curLevelObjInstance);  //12345

        _curLevelObjInstance = Instantiate(Levels_OBJS[curLodedLevel_Num]);
        _curIlevel = _curLevelObjInstance.GetComponent<ILevel>();
        ////Cur_WL = GetCurrWave();
        ///_curIlevel
        Start_LoadedLEvel_Bufftime_W_start_level_ONTIMER(GameSettings.Instance.GET_SHORTTIME_4seconds());
    }
    public void WaveNotCompleted_ButRepop()
    {
        if (GameManager.Instance.KngGameState == ARZState.ReachedAllowedTime) return;

        Destroy(_curLevelObjInstance);
        // do not increment
        _curLevelObjInstance = Instantiate(Levels_OBJS[curLodedLevel_Num]);
        _curIlevel = _curLevelObjInstance.GetComponent<ILevel>();
        ////Cur_WL = GetCurrWave();
        Start_LoadedLEvel_Bufftime_W_start_level_ONTIMER(GameSettings.Instance.GET_SHORTTIME_4seconds());
    }

    public void StopTheGame()
    {
        Destroy(_curLevelObjInstance);
    }

    public void PlayerDiedResetLevel()
    {
        Destroy(_curLevelObjInstance);

        //  StemKitMNGR.CALL_ResetGunAndMeter();


        _curLevelObjInstance = Instantiate(Levels_OBJS[curLodedLevel_Num]);
        _curIlevel = _curLevelObjInstance.GetComponent<ILevel>();
        ////Cur_WL = GetCurrWave();

        TimerBehavior t = gameObject.AddComponent<TimerBehavior>();
        t.StartTimer(GameSettings.Instance.GET_SHORTTIME_4seconds(), WM_ReloadLevel); //was long

    }

    //*************************************************Q


    //*************************************************Q


    public WaveLevel GetCurrWaveObj()
    {
        return _curLevelObjInstance.GetComponent<WaveLevel>();
    }

    public string GetWaveRomanNumeral()
    {
        string romanNumeral;

        switch (curLodedLevel_Num)
        {
            case 0:
                romanNumeral = "I";
                break;
            case 1:
                romanNumeral = "II";
                break;
            case 2:
                romanNumeral = "III";
                break;
            case 3:
                romanNumeral = "IV";
                break;
            case 4:
                romanNumeral = "V";
                break;
            case 5:
                romanNumeral = "VI";
                break;
            case 6:
                romanNumeral = "VII";
                break;
            case 7:
                romanNumeral = "VIII";
                break;
            case 8:
                romanNumeral = "IX";
                break;
            case 9:
                romanNumeral = "X";
                break;
            default:
                romanNumeral = "";
                break;
        }

        return romanNumeral;
    }

    public ILevel Get_Cur_iLEvel()
    {
        return _curIlevel;
    }

    public GunType GetLoadedWaveWepon()
    {
        return GunType.GRENADELAUNCHER;// _curIlevel.Get_GunType_M();
    }

}













//public void TheFirstWaveCanSafelyInitKNPandKMNGnodes(KNodeManager argKngmng) {

//    ////get the level to do this so that it can also initialize spawn lists 
//   // KnodeProvider.Instance.SetReadyForRequests(argKngmng);
//   // _curIlevel.FreeToInitNodes();
//}


//void SetWaveTimesByWaveIndex(GameObject LevelObjClone, int argZeroBasedWaveIndex)
//{
//    LevelObjClone.GetComponent<WaveLevel>().iSet_LevelNumber(argZeroBasedWaveIndex + 1);
//    LevelObjClone.GetComponent<WaveLevel>().iSet_LevelTime(GameSettings.Instance.GetHardCodedWaveTime(argZeroBasedWaveIndex));
//    LevelObjClone.GetComponent<WaveLevel>().iSet_LevelMaxEnemiesOnScreen(GameSettings.Instance.GetHardCodedMazOnScereen(argZeroBasedWaveIndex));
//    LevelObjClone.GetComponent<WaveLevel>().iSet_LevelMax_Gravers_OnScreen(GameSettings.Instance.GetHardCodedMax_Graves_OnScereen(argZeroBasedWaveIndex));
//    LevelObjClone.GetComponent<WaveLevel>().iSet_LevelMax_Sprinters_OnScreen(GameSettings.Instance.GetHardCodedMax_Sprinters_OnScereen(argZeroBasedWaveIndex));
//    LevelObjClone.GetComponent<WaveLevel>().iSet_LevelMax_Axers_OnScreen(GameSettings.Instance.GetHardCodedWave_MAx_Axemen(argZeroBasedWaveIndex));
//    LevelObjClone.GetComponent<WaveLevel>().iSet_LevelSeekSpeed(GameSettings.Instance.GetHardCodedWaveSpeed(argZeroBasedWaveIndex));
//    LevelObjClone.GetComponent<WaveLevel>().iSetLevelHP(GameSettings.Instance.GetHardCodedWaveHP(argZeroBasedWaveIndex));
//    LevelObjClone.GetComponent<WaveLevel>().iSetLevelHItstrength(GameSettings.Instance.GetHardCodedWaveHitStregths(argZeroBasedWaveIndex));
//    LevelObjClone.GetComponent<WaveLevel>().iSet_LevelGunType(GameSettings.Instance.GetHardCodedWaveMainGuns(argZeroBasedWaveIndex), GameSettings.Instance.GetHardCodedWaveSecondaryGuns(argZeroBasedWaveIndex));
//    LevelObjClone.GetComponent<WaveLevel>().iSet_listSs(GameSettings.Instance.GetHardCodedWave_Available_SPawnIds(argZeroBasedWaveIndex), GameSettings.Instance.GetHardCodedWave_Available_GraveIds(argZeroBasedWaveIndex));

//    BasicLevelTest btw = LevelObjClone.GetComponent<BasicLevelTest>();
//    if (btw != null)
//    {
//        btw.HasBos = GameSettings.Instance.GetHardCodedHasBoss(argZeroBasedWaveIndex);
//        btw.BossTime = GameSettings.Instance.GetHardCodedWaveBossTimes(argZeroBasedWaveIndex);
//        btw.SpawnRate = GameSettings.Instance.GetHardCodedWave_Spawn_Rates(argZeroBasedWaveIndex);
//        btw.GraveRate = GameSettings.Instance.GetHardCodedWave_Grave_Rates(argZeroBasedWaveIndex);
//        btw.Time_StartGrave = GameSettings.Instance.GetHardCodedWave_Grave_TimeStart(argZeroBasedWaveIndex);
//        btw.Time_IncreaseAgro = GameSettings.Instance.GetHardCodedWave_Speed_IncreaseStart(argZeroBasedWaveIndex);
//        btw.Times_Place_Axeguy = GameSettings.Instance.GetHardCodedWave_AxeGue_Times(argZeroBasedWaveIndex);
//        btw.Times_place_Sprinter = GameSettings.Instance.Get__SPRINTER_TIMES(argZeroBasedWaveIndex);
//        btw.pickfrom = GameSettings.Instance.GetHardCodedWave_Enemy_Names(argZeroBasedWaveIndex);

//    }
//    else
//    {
//        Debug.LogError("this is not a basicwavelevel");
//    }
//}
