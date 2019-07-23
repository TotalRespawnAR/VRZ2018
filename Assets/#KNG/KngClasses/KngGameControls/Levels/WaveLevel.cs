////#define ENABLE_DEBUGLOG
//#define ENABLE_DEBUGLOG
using System.Collections.Generic;
using UnityEngine;

public class WaveLevel : MonoBehaviour, ILevel, ILevelProps
{
    #region public vars
    public bool LevelTickerOverride;
    public int LevelTime;
    public int LevelNumber;
    public int LevelHP;
    public int LevelHitStrength;
    public int LevelMaxEnemiesOnScreen;
    public int LevelMax_gravers_OnScreen;
    public int LevelMax_Sprinters_OnScreen;
    public int LevelMax_axedudes_OnScreen;


    public SeekSpeed LevelBaseSeekSpeed;
    public GunType LevelGun_MAIN;
    public GunType LevelGun_SCONDARY;
    public List<int> ListSpawnPointIds;
    public List<int> ListGravePointIds;
    public List<int> ListTunnelSpawnPointIds;
    #endregion

    #region Iprops
    public void Set_TickerOverride(bool argonoff)
    {
        LevelTickerOverride = argonoff;
    }

    public bool Get_TickerOverride()
    {
        return LevelTickerOverride;
    }

    //GameSettings for now
    public void iSet_LevelTime(int t)
    {
        LevelTime = t;
    }
    public int Get_LevelTime()
    {
        return LevelTime;
    }
    //GameSettings for now
    public void iSet_LevelNumber(int num)
    {
        LevelNumber = num;
    }
    public int Get_LevelNumber()
    {
        return LevelNumber;
    }
    //GameSettings for now
    public void iSetLevelHP(int x)
    {
        LevelHP = x;
    }
    public int Get_LevelHP()
    {
        return LevelHP;
    }
    //GameSettings for now
    public void iSetLevelHItstrength(int x)
    {
        LevelHitStrength = x;
    }
    public int Get_LevelHItStrength()
    {
        return LevelHitStrength;
    }
    //GameSettings for now
    public void iSet_LevelMaxEnemiesOnScreen(int maxenemies)
    {
        LevelMaxEnemiesOnScreen = maxenemies;
    }
    public int Get_LevelMax_Standard_OnScreen()
    {
        return LevelMaxEnemiesOnScreen;
    }
    //GameSettings for now
    public int Get_LevelMax_Gravers_OnScreen()
    {
        return LevelMax_gravers_OnScreen;
    }

    public void iSet_LevelMax_Gravers_OnScreen(int maxenemies)
    {
        LevelMax_gravers_OnScreen = maxenemies;
    }
    //GameSettings for now
    public int Get_LevelMax_Sprinters_OnScreen()
    {
        return LevelMax_Sprinters_OnScreen;
    }

    public void iSet_LevelMax_Sprinters_OnScreen(int maxenemies)
    {
        LevelMax_Sprinters_OnScreen = maxenemies;
    }
    //GameSettings for now
    public int Get_LevelMax_Axers_OnScreen()
    {
        return LevelMax_axedudes_OnScreen;
    }

    public void iSet_LevelMax_Axers_OnScreen(int maxenemies)
    {
        LevelMax_axedudes_OnScreen = maxenemies;
    }




    //GameSettings for now
    public void iSet_LevelSeekSpeed(SeekSpeed argseekspeed)
    {
        LevelBaseSeekSpeed = argseekspeed;
    }
    public SeekSpeed Get_LevelBaseSeekSpeed()
    {
        return LevelBaseSeekSpeed;
    }
    //GameSettings for now
    public void iSet_LevelGunType(GunType argLevelGun_MAIN, GunType argLevelGun_SCONDARY)
    {
        LevelGun_MAIN = argLevelGun_MAIN;
        LevelGun_SCONDARY = argLevelGun_SCONDARY;
    }
    public GunType Get_LevelGunType(LeftMidRight argLMR)
    {
        if (argLMR == LeftMidRight.MID) { return LevelGun_MAIN; } else { return LevelGun_SCONDARY; }
    }

    public void iSet_listSs(List<int> argSpawnpointIDs, List<int> argGravepointIDS)
    {
        ListSpawnPointIds = argSpawnpointIDs;
        ListGravePointIds = argGravepointIDS;
    }


    public void iIncrease_LevelSeekSpeed()
    {
        if (LevelBaseSeekSpeed == SeekSpeed.walk)
        {
            LevelBaseSeekSpeed = SeekSpeed.run;
            Debug.Log("wavelevel set base seek to runs");
        }
        else

         if (LevelBaseSeekSpeed == SeekSpeed.run)
        {

            LevelBaseSeekSpeed = SeekSpeed.sprint;
            Debug.Log("wave agro up to sprint");
        }




    }

    // public void iSet_listSpawnpoints() { }


    public List<int> Get_ListSpawnPointIds()
    {
        return ListSpawnPointIds;
    }

    public List<int> Get_ListGravePointIds()
    {
        return ListGravePointIds;
    }
    public List<int> Get_ListTunnelSpawnPointIds()
    {
        return ListTunnelSpawnPointIds;
    }
    #endregion

    #region Ilevel

    public void StartLevel_WAVEPLAY()
    {
        Resources.UnloadUnusedAssets();
        GameEventsManager.Instance.Call_WaveStartedOrReset_DEO(this);
        GameManager.Instance.KngGameState = ARZState.WavePlay;
        GameManager.Instance.LiftupSign();
        GameEventsManager.Call_GunSetChangeTo(Get_LevelGunType(LeftMidRight.MID));
        GameEventsManager.CALL_ToggleStemInput(true);
        BaseStartLevel_WAVEPLAY();
    }
    public virtual void BaseStartLevel_WAVEPLAY()
    {
        //Wavelevel ob\nly runs StartLevel_WAVEPLAY(), derived objs will overrided this to run when StartLevel_WAVEPLAY is called . by wave manager usually
    }
    public void FinishLevel()
    {
        throw new System.NotImplementedException();
    }

    public void ReloadLevel()
    {
        throw new System.NotImplementedException();
    }

    public void On_TickerTime(int argSecondTicked)
    {
        Debug.Log("timer " + argSecondTicked.ToString());
    }

    public void ControllTunnel(LeftMidRight argLeftMdRight, bool argOenClose)
    {
        throw new System.NotImplementedException();
    }
    #endregion


    //#region MonoMethods
    void Awake()
    {
        Debug.Log("wavelevel Awake()");
    }
    //void OnEnable()
    //{
    //    GameEventsManager.On_PredatorPointed += HEardPredatorPointed;
    //    GameEventsManager.On_WaveMiniBossDied += HEardBossDied;
    //    GameEventsManager.On_WaveMiniAXMANDied += HEardAXMANDied;
    //    //  GameEventsManager.On_WaveGRAVERDied += HEardGRAVERied;
    //}
    //void OnDisable()
    //{
    //    GameEventsManager.On_PredatorPointed -= HEardPredatorPointed;
    //    GameEventsManager.On_WaveMiniBossDied -= HEardBossDied;
    //    GameEventsManager.On_WaveMiniAXMANDied -= HEardAXMANDied;
    //    // GameEventsManager.On_WaveGRAVERDied -= HEardGRAVERied;
    //}
    public virtual void HEardPredatorPointed()
    {
        Debug.Log("wavelevel heardpointed");
    }

    public virtual void One_Boss_Died()
    {
        Debug.Log("wavelevel heardpointed");
    }
    public virtual void On_Axeman_Died()
    {
        Debug.Log("wavelevel heardpointed");
    }

    public virtual void On_Graver_Died()
    {
        Debug.Log("wavelevel heardpointed");
    }
    public virtual void On_Standard_Died()
    {
        Debug.Log("GRaveDied");

    }
    public virtual void On_Sprinter_Died()
    {
        Debug.Log("GRaveDied");

    }

    protected virtual void Start()
    {
        Debug.Log("wavelevel Start()");
    }





    //#endregion

}


/*  
public void ControllTunnel(LeftMidRight argLeftMdRight, bool argOenClose)
{

#if ENABLE_DEBUGLOG
        Debug.Log("not implemented"); 
        #endif
}

public void FinishLevel()
{
#if ENABLE_DEBUGLOG
        Debug.Log("not implemented");
        #endif
}

public void ReloadLevel()
{
#if ENABLE_DEBUGLOG
        Debug.Log("not implemented");
        #endif
}

public virtual void StartLevel_WAVEPLAY()
{

Resources.UnloadUnusedAssets();
GameEventsManager.Instance.Call_WaveStartedOrReset_DEO(this);
GameManager.Instance.KngGameState = ARZState.WavePlay;
GameManager.Instance.LiftupSign();
StemKitMNGR.Call_GunSetChangeTo(Get_LevelGunType(LeftMidRight.MID));
StemKitMNGR.CALL_ToggleStemInput(true);

}

    */


/*
public bool SelfTick;
public bool hasMiniBoss;
public bool MiniBossDied;
public MidNearNone TunelLevel;
public bool PlayerSideTunnelOn;
public bool OtherSideTunnelOn;

public FarMidNearNone GraveLevel;
//public int WaveLevel_Number;
public int _waveLevel_Number1Based;
public int _time_waveLevel;
public int TimeCoolDown_wl;
public int _initial_HP;
public int MaxLiveEnemies;
int CashedMaxLiveEnemies;
//public int SpawnerLevelTrio;
public EnemyAnimatorState InitialZstate;
public int FlyPeriod;
// public GunType InitialGun_Left;
public GunType InitialGun;
public GunType InitialGun_Right;

public List<KngEnemyName> LevelZombies;

public List<GameObject> OptimalEnemyPool = new List<GameObject>(); //this will ruin eneies manager ... do later

public int TimeIncreaseAgro;
public int TimeGraveStart;
public int TimeGraveEnds;

public int TimeGasMAskStart;
public int TimeGasMAskEnds;

public int TimeBigMutantStart;
public int TimeBigMutantkEnds;

public int TimeFighterStart;
public int TimeFighterEnds;

public int TimeTeethStart;
public int TimeTeethEnds;


public int TimePumpkinStart;
public int TimePumpkinEnds;

public int TimeClawStart;
public int TimeClawEnds;

void OnEnable()
{
    GameEventsManager.On_WaveMiniBossDied += HeadrMiniBossDied;
    GameEventsManager.On_PredatorPointed += HEardPredatorPointed;
}
void OnDisable()
{
    GameEventsManager.On_WaveMiniBossDied -= HeadrMiniBossDied;
    GameEventsManager.On_PredatorPointed -= HEardPredatorPointed;
}

public virtual void HEardPredatorPointed() {
    //only scripted level spawns predator
}

void HeadrMiniBossDied()
{
    MiniBossDied = true;
}

public ScriptWaveScenario LevelScriptedScenario;
KNodeManager _knodemanager; //for controlling and getting list of available spawn using knodeIDs
                            //can also ask the knodemanager to set itselfe ie:bottom floor position. 
                            //the level will then calculate propper spawns and propper isfree paterns

//bool LevelTimeEnded = false;
public ScriptWaveScenario GetScenarioObject()
{
    return this.LevelScriptedScenario;
}

public void ReloadLevel()
{
#if ENABLE_DEBUGLOG Debug.Log("not implemented"); #endif
}


private List<int> Generated_GravepointsIds;
private List<int> Generated_SpawnPointsIds;
//  private List<int> Generated_TunnelSpawnPointsIds;
protected virtual void Start()
{


    //        CloseBotheDoors();
    // Debug.Log("start() " + gameObject.name + "l " + Get_WaveLevelNumber());
    if (!PlayerSideTunnelOn && !OtherSideTunnelOn)
    {
        TunelLevel = MidNearNone.NONE;
        hasMiniBoss = false;
    }
    if (TunelLevel != MidNearNone.NONE)
    {
        hasMiniBoss = true;
    }

    MiniBossDied = false;

    if (TimeGraveStart >= _time_waveLevel) { TimeGraveStart = 4; }
    if (TimeGraveEnds >= _time_waveLevel) { TimeGraveStart = _time_waveLevel - 1; }
    if (TimeIncreaseAgro >= _time_waveLevel) { TimeIncreaseAgro = 10; }

    CashedMaxLiveEnemies = MaxLiveEnemies;
    if (GameSettings.Instance.IsKidsModeOn)
    {
        if (MaxLiveEnemies > 1)
        {
            CashedMaxLiveEnemies--;
        }
    }

    calc_Gavespawns();
    calc_spawns();
   // calc_TunnelPoints();

}

void OPenplayersidedoors()
{

    if (GameSettings.Instance.GameMode == ARZGameModes.GameLeft_Alpha)
    {
        GameEventsManager.Instance.CAll_SpotLightTarget(0, KnodeProvider.Instance.GEt_DoorAlphaTrans());
        GameEventsManager.Instance.CAll_SpotLightOnOff(0, true);
        GameEventsManager.Instance.CAll_DoorsControle('l', 'o');
    }
    else
    {
        GameEventsManager.Instance.CAll_SpotLightTarget(1, KnodeProvider.Instance.GEt_DoorBravoTrans());
        GameEventsManager.Instance.CAll_SpotLightOnOff(1, true);
        GameEventsManager.Instance.CAll_DoorsControle('r', 'o');
    }


}
void ClosePlayerSideDoor()
{
    if (GameSettings.Instance.GameMode == ARZGameModes.GameLeft_Alpha)
    {
        GameEventsManager.Instance.CAll_SpotLightTarget(0, null);
        GameEventsManager.Instance.CAll_SpotLightOnOff(0, false);
        GameEventsManager.Instance.CAll_DoorsControle('l', 'c');
    }
    else
    {
        GameEventsManager.Instance.CAll_SpotLightTarget(1, null);
        GameEventsManager.Instance.CAll_SpotLightOnOff(1, false);
        GameEventsManager.Instance.CAll_DoorsControle('r', 'c');
    }

}

void OPenOthersidedoors()
{

    if (GameSettings.Instance.GameMode == ARZGameModes.GameLeft_Alpha)
    {
        GameEventsManager.Instance.CAll_SpotLightOnOff(1, true);
        GameEventsManager.Instance.CAll_DoorsControle('r', 'o');
        GameEventsManager.Instance.CAll_SpotLightTarget(1, KnodeProvider.Instance.GEt_DoorBravoTrans());
    }
    else
    {
        GameEventsManager.Instance.CAll_SpotLightOnOff(0, true);
        GameEventsManager.Instance.CAll_DoorsControle('l', 'o');
        GameEventsManager.Instance.CAll_SpotLightTarget(0, KnodeProvider.Instance.GEt_DoorAlphaTrans());
    }
}
void CloseOtherideDoor()
{
    if (GameSettings.Instance.GameMode == ARZGameModes.GameLeft_Alpha)
    {
        GameEventsManager.Instance.CAll_SpotLightOnOff(1, false);
        GameEventsManager.Instance.CAll_DoorsControle('r', 'c');
        GameEventsManager.Instance.CAll_SpotLightTarget(1, null);
    }
    else
    {
        GameEventsManager.Instance.CAll_SpotLightOnOff(0, false);
        GameEventsManager.Instance.CAll_DoorsControle('l', 'c');
        GameEventsManager.Instance.CAll_SpotLightTarget(0, null);
    }
}
//void OpenBothDoors()
//{
//    OPenplayersidedoors();
//    OPenOthersidedoors();



//}
public void CloseBotheDoors()
{
    ClosePlayerSideDoor();
    CloseOtherideDoor();
}

void calc_spawns()
{
    //List<int> IndecesOfSpawnNodesForTHisWave = new List<int>();
    //Spawn ids go from 0 -n in groops of 8 
    int lastSpawnId_exclussive = (Mathf.CeilToInt(MaxLiveEnemies / 8f)) * 8;

    Generated_SpawnPointsIds = new List<int>();

    for (int x = 0; x < lastSpawnId_exclussive; x++)
    {
        Generated_SpawnPointsIds.Add(x);
    }
}

void calc_Gavespawns()
{



    Generated_GravepointsIds = new List<int>();

    switch (GraveLevel)
    {
        case FarMidNearNone.FAR:
            Generated_GravepointsIds.Add(26);
            Generated_GravepointsIds.Add(35);
            Generated_GravepointsIds.Add(28);
            Generated_GravepointsIds.Add(37);
            break;
        case FarMidNearNone.MID:
            Generated_GravepointsIds.Add(34);
            Generated_GravepointsIds.Add(44);
            Generated_GravepointsIds.Add(37);

            break;
        case FarMidNearNone.NEAR:
            Generated_GravepointsIds.Add(43);
            Generated_GravepointsIds.Add(50);
            Generated_GravepointsIds.Add(52);
            Generated_GravepointsIds.Add(53);
            Generated_GravepointsIds.Add(51);
            break;
    }

}

//void calc_TunnelPoints()
//{
//    Generated_TunnelSpawnPointsIds = new List<int>();

//    if (TunelLevel != MidNearNone.NONE)
//    {
//        if (PlayerSideTunnelOn)
//        {
//            if (GameSettings.Instance.GameMode == ARZGameModes.GameLeft_Alpha)
//            {
//                Generated_TunnelSpawnPointsIds.Add(0);
//            }
//            else
//            {
//                Generated_TunnelSpawnPointsIds.Add(1);
//            }

//        }
//        if (OtherSideTunnelOn)
//        {
//            if (GameSettings.Instance.GameMode == ARZGameModes.GameLeft_Alpha)
//            {
//                Generated_TunnelSpawnPointsIds.Add(1);
//            }
//            else
//            {
//                Generated_TunnelSpawnPointsIds.Add(0);
//            }

//        }


//    }

//}
#region Interface
public void AssignWaveNumber_byWavemanager(int x)
{
    _waveLevel_Number1Based = x;
}
public void SetLevelHP(int x)
{
    _initial_HP = x;
}

public void StartLevel_WAVEPLAY()
{
    // Debug.Log("start level " + gameObject.name + "level " + Get_WaveLevelNumber());
    Resources.UnloadUnusedAssets();
    GameEventsManager.Instance.Call_WaveStartedOrReset_DEO(this);
    GameManager.Instance.KngGameState = ARZState.WavePlay;
    GameManager.Instance.LiftupSign();
    StemKitMNGR.Call_GunSetChangeTo(Get_GunType_M());
    StemKitMNGR.CALL_ToggleStemInput(true);

    UnCommonCommonWaveStart();

}

public virtual void UnCommonCommonWaveStart()
{
    StartGameForRegularWave();
}

public void FinishLevel() //get event from timer that will tick and copare with its copy of curlevellevel
{
    if (GameManager.Instance.KngGameState != ARZState.ReachedAllowedTime)
    {
        if (GameManager.Instance.ENEMYMNGER_getter().liveenemies.Count > 0)
        {
            GameManager.Instance.KngGameState = ARZState.WaveOverTime;
        }
        else
        {

            GameManager.Instance.KngGameState = ARZState.WaveEnded;
            GameManager.Instance.GM_Handle_WaveCompleteByPoppingNUMplusplus();
        }
    }
}



public GunType Get_GunType_M()
{
    return this.InitialGun;
}

public GunType Get_GunType_R()
{
    return this.InitialGun_Right;
}

public int Get_LevelMaxEnemiesOnScreen()
{
    return this.CashedMaxLiveEnemies;
}


public int Get_LevelNumber()
{
    return this._waveLevel_Number1Based;
}

public void Set_LevelTime(int argLeveltime)
{
    _time_waveLevel = argLeveltime;
}
public int Get_LevelTime()
{
    return _time_waveLevel;
}

public int Get_TickTime()
{
    return this.TimeCoolDown_wl;
}
public int Get_LevelHP()
{
    return this._initial_HP;
}
public void Increase_Initial_State()
{
    if (InitialZstate == EnemyAnimatorState.IDLE)
    {
        InitialZstate = EnemyAnimatorState.WALKING;
    }
    else
   if (InitialZstate == EnemyAnimatorState.WALKING)
    {
        InitialZstate = EnemyAnimatorState.CHASING;

    }
    else if (InitialZstate == EnemyAnimatorState.CHASING)
    {
        InitialZstate = EnemyAnimatorState.HYPERCHASING;
    }
    else
        InitialZstate = EnemyAnimatorState.CHASING;


}
public EnemyAnimatorState Get_Level_AnimState()
{
    return this.InitialZstate;
}


public List<int> Get_LevelSpawnPointIds()
{
    return Generated_SpawnPointsIds;
}

public List<int> Get_LevelGravePointIds()
{
    return Generated_GravepointsIds;
}

public bool NoTicks()
{
    return SelfTick;
}
#endregion

void StartGameForRegularWave() {

    if (TunelLevel != MidNearNone.NONE)
    {

        if (PlayerSideTunnelOn)
        {
            OPenplayersidedoors();
        }
        if (OtherSideTunnelOn)
        {
            OPenOthersidedoors();
        }


    }

    if (hasMiniBoss)
    {
        GameManager.Instance.Spawn_PumpkinBossInProperTunnel();
    }
}

public void Set_LevelNumber(int num)
{
#if ENABLE_DEBUGLOG Debug.Log("not implemented"); #endif
}

public void Set_LevelMaxEnemiesOnScreen(int maxenemies)
{
#if ENABLE_DEBUGLOG Debug.Log("not implemented"); #endif
}

public void Set_LevelSpawnPointIds(List<int> spawnids)
{
#if ENABLE_DEBUGLOG Debug.Log("not implemented"); #endif
}

public void Set_LevelGravePointIds(List<int> graveids)
{
#if ENABLE_DEBUGLOG Debug.Log("not implemented"); #endif
}

public void Set_TunnelSpaenPointIds(List<int> tunnelspawnids)
{
#if ENABLE_DEBUGLOG Debug.Log("not implemented"); #endif
}

public List<int> Get_TunnelSpawnPointIds()
{
#if ENABLE_DEBUGLOG Debug.Log("not implemented"); #endif
}
//public List<int> Get_LevelTunnelPointIds()
//{
//    return Generated_TunnelSpawnPointsIds;
//}


#if ENABLE_KEYBORADINPUTS // Skip Don't Destroy On Load when editor isn't playing so test runner passes.


void Update()
{
     if (Input.GetKeyDown(KeyCode.Alpha1))
    {
        OPenplayersidedoors();
    }
    if (Input.GetKeyDown(KeyCode.Alpha2))
    {
        OpenBothDoors();
    }

    if (Input.GetKeyDown(KeyCode.Alpha3))
    {
        ClosePlaresideDoor();
    }
    if (Input.GetKeyDown(KeyCode.Alpha4))
    {
        CloseBotheDoors();
    }
}
#endif

*/
