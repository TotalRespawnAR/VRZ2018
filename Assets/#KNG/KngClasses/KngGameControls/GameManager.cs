// @Author Nabil Lamriben ©2017

//******************************
//#define ENABLE_DEBUGLOG
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    private void OnEnable()
    {
        GameEventsManager.OnGameObjectAnchoredPlaced += ListenTo_AnchoresPlaced;

        if (GameSettings.Instance == null)
        {
            Debug.LogError("NO GAMESETTINGS !");
        }
        else

        {
            if (GameSettings.Instance.UseVive)
            {

                FirstPersonObj.SetActive(false);
                ViveArenaObj.SetActive(true);

                if (GameSettings.Instance.UseRoomFlip)
                {
                    ViveArenaObj.transform.localEulerAngles = new Vector3(0, 180, 0);
                }
                else
                {
                    ViveArenaObj.transform.localEulerAngles = new Vector3(0, 0, 0);

                }
            }
            else
            {
                FirstPersonObj.SetActive(true);
                SteamVR_TrackedObject _SteamVR_TrackedObject = FPsGUNS.gameObject.GetComponent<SteamVR_TrackedObject>();
                if (_SteamVR_TrackedObject != null)
                {
                    Destroy(_SteamVR_TrackedObject);
                }

                CameraVive.GetComponent<Camera>().fieldOfView = 40;

                CameraVive.transform.parent = CameraFPS_POS.transform;
                CameraVive.transform.localPosition = new Vector3(0, 0, 0);
                CameraVive.transform.parent = CameraFPS_POS.transform.parent;
                CameraFPS_POS.transform.parent = CameraVive.transform;

                FPsGUNS.transform.parent = CameraVive.transform;

                ControllerR.SetActive(false);
                ControllerL.SetActive(false);


                ViveArenaObj.SetActive(false);
                //  PlayerOneObj.transform.parent = null;
                //  PlayerOneObj.transform.parent = CameraFPS.transform;
                // PlayerOneObj.transform.localPosition = new Vector3(0, 0, 0);
            }

        }
    }
    private void OnDisable()
    {
        GameEventsManager.OnGameObjectAnchoredPlaced -= ListenTo_AnchoresPlaced;
    }

    public GameObject FakeStones;
    public GameObject ViveArenaObj;
    public GameObject FirstPersonObj;
    public GameObject PlayerOneObj;
    public GameObject CameraVive;
    public GameObject CameraFPS_POS;
    public ViveGunBundle FPsGUNS;
    public GameObject ControllerR;
    public GameObject ControllerL;

    public List<GameObject> ElevatorWallsMeshs;
    public void Spawn_GunProp(GunType argGunType, Transform Zhand, Quaternion WorldRotOfOriginalGUn)
    {
        GameObject InstantiatedGUnPropObj = Instantiate(_enemyModelsRepo_Compo.GetGunModel_Ref(argGunType), Zhand.position, WorldRotOfOriginalGUn);
        InstantiatedGUnPropObj.transform.parent = Zhand;

    }


    public void Spawn_Enemy(Data_Enemy argEnemyPrps)
    {
        argEnemyPrps.ID = _enemiesManager_Compo.GetNEWEnemyId();
        GameObject InstantiatedEnemyObj = Instantiate(_enemyModelsRepo_Compo.GetEnemyModel_Ref(argEnemyPrps.ModelName), argEnemyPrps.SpawnKnode.transform.position, Quaternion.Euler(argEnemyPrps.InitialRotEuler));


        DressEnemyCompos(InstantiatedEnemyObj, argEnemyPrps.Ztype_STD);


        IEnemyEntityComp IEBC = InstantiatedEnemyObj.GetComponent<IEnemyEntityComp>();
        //infobox
        //GameObject InfoBox = Instantiate(EnemyInfoBox);
        //InfoBox.transform.parent = InstantiatedEnemyObj.transform;
        //InfoBox.transform.localPosition = new Vector3(0, 1.65f, 0f);
        //IEnemyInfo IEI = InfoBox.GetComponent<IEnemyInfo>();
        // IEI.Link_IEnemyBehaviorComp(IEBC);Noneed to do it here . infocompo only gets ref at start .. might as well let it handle the linking on its own start
        IEBC.InitBehavior(argEnemyPrps);
        _enemiesManager_Compo.AddLiveZombie(InstantiatedEnemyObj);
    }

    #region Zombie Making and Spawning Coordination

    public GameObject GetaStaticAxe() { return _enemyModelsRepo_Compo.AxeStatic; }
    public GameObject GetaDynamicAxe()
    {
        if (GameSettings.Instance != null)
        {

            if (GameSettings.Instance.UseAxe2)
            {
                return _enemyModelsRepo_Compo.AxeDynamic2;
            }
            else
            {
                return _enemyModelsRepo_Compo.AxeDynamic;
            }
        }
        return _enemyModelsRepo_Compo.AxeDynamic;
    }
    public GameObject GetaDynamicGuyle() { return _enemyModelsRepo_Compo.GuyleDynamic; }
    //from Scenariomanager , the node is just given arbitrarely


    void DressEnemyCompos(GameObject argNewEnemyObj, ARZombieypes argZtype)
    {
        if (argZtype == ARZombieypes.PREYgrave)
        {
            argNewEnemyObj.AddComponent<PreyGraver>();
        }
        else
          if (argZtype == ARZombieypes.PREDATOR)
        {
            argNewEnemyObj.AddComponent<PredatorBoss>();
        }
        else
          if (argZtype == ARZombieypes.STD_BOSS)
        {
            argNewEnemyObj.AddComponent<BossPumpKin>();
        }
        else
            if (argZtype == ARZombieypes.AXEMAN)
        {
            argNewEnemyObj.AddComponent<ZombieAxeMan>();
        }
        else
            if (argZtype == ARZombieypes.GRAVESKELETON)
        {
            argNewEnemyObj.AddComponent<ZombieGraver>();
        }
        else
            if (argZtype == ARZombieypes.TankFighter)
        {
            argNewEnemyObj.AddComponent<ZombiePumpkin>();
        }
        else
        {

            argNewEnemyObj.AddComponent<ZombieOld>();
        }
        //argNewEnemyObj.AddComponent<EnemyMoverComponent>();
        argNewEnemyObj.AddComponent<EnemyDamageComponent>();
        argNewEnemyObj.AddComponent<EnemyScoreComponent>();
        argNewEnemyObj.AddComponent<EnemyEffectsComponent>();
    }


    private void Add_EnemyBehavior(GameObject argNewEnemyObj)
    {
        argNewEnemyObj.AddComponent<EnemyBehavior>();
    }


    private void Remove_EnemyBehavior(GameObject argNewEnemyObj)
    {
        EnemyBehavior EnemyBehToRemove = argNewEnemyObj.GetComponent<EnemyBehavior>();
        if (EnemyBehToRemove != null)
        {
            DestroyImmediate(EnemyBehToRemove);
        }
    }


    //public void Spawn_PumpkinBossInProperTunnel()
    //{
    //    Data_Enemy de = new Data_Enemy(1, KngEnemyName.PUMPKIN, 1, ARZombieypes.STANDARD, EnemyAnimatorState.WALKING, EnemyAnimatorState.WALKING);
    //    de.SpawnKnode = KnodeProvider.Instance.Get_RoundRobin_Tunelpoint();
    //    de.ID = _enemiesManager_Compo.GetNEWEnemyId();
    //    GameObject enemyObjec = _enemyModelsRepo_Compo.GetEnemyModel_Ref(de.ModelName);
    //    GameObject NewEnemyObj = Instantiate(enemyObjec, de.SpawnKnode.transform.position, Quaternion.Euler(de.InitialRotEuler));
    //    IEnemyBehavior enemyIbeh = NewEnemyObj.GetComponent<IEnemyBehavior>();
    //    // enemyIbeh.InitBehavior(de);
    //    _enemiesManager_Compo.AddLiveZombie(NewEnemyObj);
    //}



    public void EffectShartd(Vector3 here)
    {
        GameObject blue = Instantiate(_enemyModelsRepo_Compo.SharedEffectObj, here, Quaternion.identity);
        Destroy(blue, 2f);

    }

    public void EffectShartd(Vector3 here, GunType argGunType, bool isBig)
    {
        GameObject blue;
        if (GameSettings.Instance.IsBloodOn)
        {
            blue = Instantiate(_enemyModelsRepo_Compo.GetBlood_Ref(argGunType, isBig), here, Quaternion.identity);
            Destroy(blue, 2f);
        }
        else

        {
            blue = Instantiate(_enemyModelsRepo_Compo.GetShatter_Ref(argGunType, isBig), here, Quaternion.identity);
            Destroy(blue, 2f);
        }
    }


    public void MkeBT(Vector3 here, float speed, Quaternion rot, int yelloy1Orange2Red3)
    {


        //int x = Random.Range(1, 4);


        GameObject bt = Instantiate(_enemyModelsRepo_Compo.GetTrail(yelloy1Orange2Red3), here, rot);

        Destroy(bt, 20f);
    }


    public void EffectDirt(Vector3 here)
    {
        GameObject brown = Instantiate(_enemyModelsRepo_Compo.DirtEffectObj, here, Quaternion.identity);
        Destroy(brown, 6f);
    }


    public void EffectBlood(Vector3 here, int blood123)
    {
        GameObject red;
        if (blood123 < 1)
        {
            blood123 = 1;
            red = Instantiate(_enemyModelsRepo_Compo.BloodEffect1, here, Quaternion.identity);
        }
        else if (blood123 == 2) { red = Instantiate(_enemyModelsRepo_Compo.BloodEffect2, here, Quaternion.identity); }
        else if (blood123 == 3) { red = Instantiate(_enemyModelsRepo_Compo.BloodEffect3, here, Quaternion.identity); }
        else { red = Instantiate(_enemyModelsRepo_Compo.BloodEffect4, here, Quaternion.identity); }

        Destroy(red, 6f);
    }


    #endregion



    int numberofbulbs = 22;
    bool hasplayedhorn = false;
    public void AirHornEffect()
    {
        if (hasplayedhorn)
        {
            return;
        }

        numberofbulbs--;
        if (numberofbulbs <= 0)
        {
            PlayHeadShotSound.Instance.PlayGongSound();
        }
    }









































    #region Check_2_Ancs_Placed
    bool RoomWasAnchored = false;
    bool StemWasAnchored = false;
    public bool UseElevator;
    void ListenTo_AnchoresPlaced(string argPlacedAnhorName)
    {
        if (argPlacedAnhorName.Contains(GameSettings.Instance.AncName_WindowBasedLand()))
        {
            RoomWasAnchored = true;
#if ENABLE_DEBUGLOG
            Debug.Log("GM Room Found");
#endif
        }
        if (argPlacedAnhorName == GameSettings.Instance.AncName_ArenaStemBase())
        {
            StemWasAnchored = true;
#if ENABLE_DEBUGLOG
            Debug.Log("GM stem found");
#endif
        }

        //if (RoomWasAnchored && StemWasAnchored)
        //{
        //    LateInit();
        //}
    }
    #endregion

    public GameObject CubeBillboard;
    public GameObject EnemyInfoBox;
    //  public RzPlayerComponent RzplayerObj;

    #region Public Props
    public bool NODAMAGEON = false;
    public bool TESTON = false;
    public int T_IdleNum = 0;
    public int T_WalkNum = 0;
    public int T_ChaseNum = 0;
    public int T_HyperNum = 0;
    public int T_DirectionNum = 0;
    public int T_DeathNum = 0;
    public int T_ReachNum = 0;

    public bool NODAMAGE = false;
    public ARZState KngGameState;
    public RzPlayerComponent Rzplayer { get; private set; }
    public ARZGameModes GameMode;


    public bool IsPlayerDead { get; private set; }

    public bool pubHardCodedWaves;

    public bool propUSe_hardCodedWaves
    {
        get { return pubHardCodedWaves; }
        set { pubHardCodedWaves = value; }
    }

    private bool _useHardcodedWaveProps;

    public Material HB_Mat_Green;
    public Material HB_Mat_YEllow;
    public Material HB_Mat_Orange;
    public Material HB_Mat_Red;
    //  public bool UseHardcodedWaveProps { get => _useHardcodedWaveProps; set => _useHardcodedWaveProps = value; }

    public bool PlayerHasDiedInThisWave = false;
    public bool isInvincible = false;

    public List<Transform> spawnPoints_GM_FAR = new List<Transform>();
    public List<Transform> spawnPoints_GM_NEAR = new List<Transform>();
    #endregion

    bool _gameStarted = false;
    bool _suddenDeathFlagIsUp = false;
    bool _gameTimeIsUp = false;
    GunType _pregameGun;

    #region ComPonents
    SceneObjectsManager _sceneObjectMnger_Compo;
    ScoreManager _scoreManager_Compo;
    StreaksManager _streaksManager_Compo;
    // ScenarioManager _senarioManager_Compo;
    EnemiesManager _enemiesManager_Compo;
    EnemyBatchCrafter _batchCrafter_Compo;
    LevelManager _levelManager_Compo;
    TimeController _timeCtrl_Compo;
    MasterUiManager m_Master_UI_compo;
    EnemyModelsRepo _enemyModelsRepo_Compo;
    #endregion

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            _pregameGun = GunType.PISTOL;
            Init_PublicProps();
            Init_Components();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    IEnumerator StartElevator()
    {
        yield return new WaitForSeconds(3);
        ElevatorController.Instance.ElevateMe();
    }
    private void Start()
    {
        // _sceneObjectMnger_Compo.Anchor_StemBase();
        // _sceneObjectMnger_Compo.Anchor_Land();
        if (UseElevator)
        {
            StartCoroutine(StartElevator());

            StartCoroutine(WaitAbitForStem());

            FakeStones.SetActive(false);
            foreach (GameObject go in ElevatorWallsMeshs) { go.SetActive(true); }
        }
        else
        {
            FakeStones.SetActive(true);
            foreach (GameObject go in ElevatorWallsMeshs) { go.SetActive(false); }

            ElevatorController.Instance.PlaceElevatorAtGroundLevel();
            StartCoroutine(WaitAbitForStem());
        }
    }
    IEnumerator WaitAbitForStem()
    {
        yield return new WaitForSeconds(1);
        GameEventsManager.Instance.Call_AnchoredGoPlaced(GameSettings.Instance.AncName_ArenaStemBase());
        GameEventsManager.Instance.Call_AnchoredGoPlaced(GameSettings.Instance.AncName_WindowBasedLand());
        LateInit();
    }
    #region Inits
    void Init_Components()
    {
        _sceneObjectMnger_Compo = GetComponent<SceneObjectsManager>();
        _scoreManager_Compo = GetComponent<ScoreManager>();
        _streaksManager_Compo = GetComponent<StreaksManager>();
        // _senarioManager_Compo = GetComponent<ScenarioManager>();
        _enemiesManager_Compo = GetComponent<EnemiesManager>();
        _batchCrafter_Compo = GetComponent<EnemyBatchCrafter>();
        _levelManager_Compo = GetComponent<LevelManager>();
        _timeCtrl_Compo = GetComponent<TimeController>();
        m_Master_UI_compo = GetComponent<MasterUiManager>();
        _enemyModelsRepo_Compo = GetComponent<EnemyModelsRepo>();
    }

    void Init_PublicProps()
    {
        KngGameState = ARZState.Pregame;
        GameMode = GameSettings.Instance.GameMode;
        Rzplayer = RzPlayerComponent.Instance; // Camera.main.GetComponent<RzPlayerComponent>(); //is static now
    }

    void LateInit()
    {
        GameObject LocalBillboard = Instantiate(CubeBillboard, RzPlayerComponent.Instance.transform.position, Quaternion.identity);
        GameEventsManager.Call_GunSetChangeTo(GunType.PISTOL);
        GameEventsManager.CALL_ToggleStemInput(false);
        //Position_ScoreBoard_Mists_Tuto_RoomUI();

        //if (RzPlayerHealthTubeControllerComponent.Instance != null)
        //{
        //    RzPlayerHealthTubeControllerComponent.Instance.ResetBArs();
        //}
        if (true)
        {
#if ENABLE_DEBUGLOG
            Debug.Log("resethealthbas? just innit rzlayer?");
#endif
        }
        else
        {
#if ENABLE_DEBUGLOG
            Debug.Log("No PlayerHealthBAr Found");
#endif
        }

        _sceneObjectMnger_Compo.ScoreSign_MNGR.SetScoreDisplay("626");
        _sceneObjectMnger_Compo.ScoreSign_MNGR.SetWaveNumberDisplay("0");

        //if (GameSettings.Instance.IsSkipPregameOn)
        //{


        //    // _senarioManager_Compo.TutoCtrlObj.GetComponent<StemTutoController>().HideAll();
        //    StartCoroutine(HideAndCheckStart());

        //}
        //else
        //{
        //    PREGAME_StartScenario();
        //}
        StartCoroutine(HideAndCheckStart());


        _sceneObjectMnger_Compo.KnodesMNGR.TogglePointsMeshes(true);
        GameEventsManager.CALL_ToggleStemInput(true);
    }

    #endregion

    #region Position_andControle_InSceneObjects

    public void Position_ScoreBoard_Mists_Tuto_RoomUI()
    {
        _sceneObjectMnger_Compo.Place_ScoreBoard();
        _sceneObjectMnger_Compo.Place_Missts();
    }
    public void Position_StartButton()
    {
        _sceneObjectMnger_Compo.Place_StartButton();
    }
    public void Position_StemTuto()
    {
        _sceneObjectMnger_Compo.Place_StemTuto();
    }
    public void ShowGameMists() { _sceneObjectMnger_Compo.MistsMNGR.YesMists(); }
    public void HIdeGameMists() { _sceneObjectMnger_Compo.MistsMNGR.NOMists(); }

    #endregion

    public SceneObjectsManager Get_SceneObjectsManager() { return _sceneObjectMnger_Compo; }

    IEnumerator HideAndCheckStart()
    {

        Position_StartButton();
        yield return new WaitForSeconds(0.2f);
        _sceneObjectMnger_Compo.KnodesMNGR.TogglePointsMeshes(false);
        //_sceneObjectMnger_Compo.KnodesMNGR.TogglePointsMeshes(false);
        GameEventsManager.CALL_ToggleStemInput(true);
        GameEventsManager.Instance.Call_PlayerCanSwitchWeapons(true);
        GameEventsManager.Instance.Call_PlayerCanReload(true);
        GameEventsManager.Instance.Call_PlayerCanShoot(true);

        // CheckWav1Started();
    }













    public Transform GEtcahedAB()
    {
#if ENABLE_DEBUGLOG
        Debug.Log("YOOO GET HOTSPOT");
#endif
        return this.transform;// (GameSettings.Instance.GameMode == ARZGameModes.GameLeft_Alpha) ? _WPstruct.GetHotSpot(0).transform : _WPstruct.GetHotSpot(1).transform;
    }

    // KngSpawnPointsCTRL TheSpawn , int argPathNumber, Transform argFinalTarget_AB
    //  EnemyBatchCrafter OnTick CraftBatch 
    //  Batch to SpawnTransformController
    // SpawnTransformController gets Batch from EnemyBatchCrafter OnTickFrom Timer  and calls gamemanager.ReQ_Znew passing dataenemy
    public GameObject REQ_ZNew(Data_Enemy argDataEnemy)
    {
        //  GameObject NEwZombie = _zSpawningMNGR.NEWenemy(_thisIdleSpawn, argZnum, argMnum, HPforThisZombie, cahedAB, argZtype, argInitialState, _thisIdleSpawn.GetComponent<TransPawnType>().SpwanPlaceType);
        //  argDataEnemy.HotspotDest1 = GEtcahedAB();
        // argDataEnemy.SpawnerLevel_forGraveLayers = _levelManager_Compo.Get_Cur_iLEvel().Get_WaveLevelNumber();
        //argDataEnemy.InitialRotEuler = new Vector3(0f, 180f, 0f);

        GameObject NEwZombie = new GameObject("very BROKKKEN"); // _spawningMNGR.NEWenemy_willNeed_Datapath(argDataEnemy, _LevelMANAGER.Get_Cur_iLEvel().Get_WaveLevelNumber() - 1, GEtcahedAB());
        /*
        if (argDataEnemy.Ztype_STD == ARZombieypes.GRAVER)
        {
            GameObject dirt = Instantiate(NEwZombie.GetComponent<ZombieEffects>().DirtEffect, NEwZombie.transform.position, Quaternion.identity);
            Destroy(dirt, 5f);
        }

        _enemiesManager.AddLiveZombie(NEwZombie);
        _scoreManager.Increment_ZombiesCreated();*/
        return NEwZombie;
    }
    //percent splay changes on wave ;

    //int r = 0; int c = 0;

    public GameObject Req_ZombieOnANY_WAYPOINT(int row, int col, Data_Enemy argDataEnemy)
    {

        // argDataEnemy.SpawnTrans = _WPstruct.GetWayPoint(row, col).transform;

        //  argDataEnemy.HotspotDest1 = GEtcahedAB();
        // argDataEnemy.SpawnerLevel_forGraveLayers = _levelManager_Compo.Get_Cur_iLEvel().Get_WaveLevelNumber();
        // argDataEnemy.InitialRotEuler = new Vector3(0f, 180f, 0f);

        // GameObject NEwZombie = _spawningMNGR.NEWenemy_OnAny(argDataEnemy, GEtcahedAB());
        GameObject NEwZombie = new GameObject("super BROKKKEN");
        /*
        if (argDataEnemy.Ztype_STD == ARZombieypes.GRAVER)
        {
            GameObject dirt = Instantiate(NEwZombie.GetComponent<ZombieEffects>().DirtEffect, NEwZombie.transform.position, Quaternion.identity);
            Destroy(dirt, 5f);
        }

        _enemiesManager.AddLiveZombie(NEwZombie);
        _scoreManager.Increment_ZombiesCreated();*/
        return NEwZombie;


        //GameObject CreatedAxeGuyOnSpwan = _spawningMNGR.CreateTutoAxeOnANY(new Vector3(0f, 180f, 0f), row, argCol, _WPstruct); //find the kngnode at col row to giv the zombie as a startingpoint
        //_enemiesManager.AddLiveAxeEnemy(CreatedAxeGuyOnSpwan);
        //return CreatedAxeGuyOnSpwan;
    }

    /*  BROOOOKEN
    public GameObject REQ_NewFly()
    {
        int flyid = 1;
        int rannx = Random.RandomRange(0, 100);
        if (rannx < 20) {
            flyid = 2;
        }
        Debug.Log("BROKEN");

       // GameObject CreatedFlyOnSpwan = _spawningMNGR.CreateFlyOnSpwan(flyid, new Vector3(0f, 180f, 0f), _WPstruct);
       // _enemiesManager.AddLiveFly(CreatedFlyOnSpwan);
        // _scoreManager.Increment_ZombiesCreated();
        return CreatedFlyOnSpwan;
    }


    

   //THIS IS THE ONE TO USE FOR REQ AXE GUY in tuto
    public GameObject Req_axeguyOnANY( int row, int argCol)
    {
        GameObject CreatedAxeGuyOnSpwan = _spawningMNGR.CreateTutoAxeOnANY(new Vector3(0f, 180f, 0f), row ,argCol  , _WPstruct ); //find the kngnode at col row to giv the zombie as a startingpoint
        _enemiesManager.AddLiveAxeEnemy(CreatedAxeGuyOnSpwan);
        return CreatedAxeGuyOnSpwan;
    }

    //Spawntransformcontroller.DoWorkWithThisBatch() ->
    public GameObject REQ_NewAxeGuy(Data_Enemy argDataEnemy)
    {
        //return Req_axeguyOnANY(2, 2);
        GameObject CreatedAxeGuyOnSpwan = _spawningMNGR.CreateAxeGuyOnSpawnWithDE(new Vector3(0f, 180f, 0f), _WPstruct, argDataEnemy);
        _enemiesManager.AddLiveAxeEnemy(CreatedAxeGuyOnSpwan);
         _scoreManager.Increment_ZombiesCreated();
        return CreatedAxeGuyOnSpwan;
    }

    //ON RECEIVE BIG ATTACK
    //udp other player should be calling this
    // if this is alpha, spawn at 1,0 else 1,1
    public GameObject REQ_AxeGuyOnSpawn() {
        GameObject CreatedAxeGuyOnSpwan = _spawningMNGR.CreateAxeGuyOnSpawn(new Vector3(0f, 180f, 0f), _WPstruct);
        _enemiesManager.AddLiveAxeEnemy(CreatedAxeGuyOnSpwan);
        return CreatedAxeGuyOnSpwan;
    }
    */
    //udp other player should be calling this
    // if this is alpha, spawn at 3,0 else 3,5
    public GameObject REQ_BigBossOnSpawn()
    {
        GameObject CreatedAxeGuyOnSpwan = new GameObject();
        _enemiesManager_Compo.AddLiveZombie(CreatedAxeGuyOnSpwan);
        return CreatedAxeGuyOnSpwan;
    }






    public void LiftupSign()
    {
        _sceneObjectMnger_Compo.ScoreSign_MNGR.Lift();
    }

    public void Display_CurWave_ScoresignManager(string argWaveNum)
    {
        _sceneObjectMnger_Compo.ScoreSign_MNGR.SetWaveNumberDisplay(argWaveNum);

    }

    public Transform GetArenaLocationAsTransform()
    {
        Debug.Log("BROKEN ragdollneeds this");
        // return _WPstruct.GetArenaPlace().transform; //just the fucking room object in scene
        return this.transform;
    }

    //only this is needed, cuz zombie can query node itself
    //  public KngNode GetHelpFindingFirstNode(int r, int c) { return _nodeMapManager.FirstNodeAfterSpawning(r,c,_WPstruct); }

    public void TryCallSlowTime()
    {
        _timeCtrl_Compo.InitialSlowTimeCall();
    }
    public void TryUpdateSlowTime()
    {
        _timeCtrl_Compo.UpdateSlowTime();
    }

    void Set_activeWayPointsColliders()
    {
        Debug.Log("BROKEN");
        //foreach (GameObject go in _WPstruct.GetWayPoints())
        //{
        //    if (go != null)
        //    {
        //        PlacedObjecetsManager.Instance.PlaceWaypoint_Active(go);
        //    }
        //}

    }

    #region UI
    public void UIzombieScratch()
    {
        //   print("yo uizscratchs");
        _streaksManager_Compo.Set_StreakBreake();

        if (GameSettings.Instance.IsIsGodModeON || KngGameState == ARZState.Pregame)
        {
            return;
        }
        // m_Master_UI_compo.Run_TakeHit(HitType.ZombieScratch);//masteruimanager->hluictrl
    }

    //public void UIAxeSplit()
    //{
    //    _streaksManager_Compo.Set_StreakBreake();

    //    if (GameSettings.Instance.IsIsGodModeON) return;
    //   // m_Master_UI_compo.Run_TakeHit(HitType.AxeSplit);//masteruimanager->hluictrl
    //}

    //public void UIFlySplat()
    //{
    //    _streaksManager_Compo.Set_StreakBreake();

    //    if (GameSettings.Instance.IsIsGodModeON) return;
    //   // m_Master_UI_compo.Run_TakeHit(HitType.FlySplat);//masteruimanager->hluictrl
    //}
    #endregion





    public Vector3 GetFlyPoint1()
    {
        int rand = Random.Range(0, 2);
        Debug.Log("BROKEN");
        // return _WPstruct.GetFlyPoint(rand).transform.position;
        return this.transform.position;
    }

    //IEnumerator Wait5StartTuto(int secTostartTuto)
    //{
    //    yield return new WaitForSeconds(secTostartTuto);
    //    _senarioManager_Compo.RunScenarioTutorial();
    //}
    //void runScenarioIn5(int argsecTostartTuto)
    //{
    //    StartCoroutine(Wait5StartTuto(argsecTostartTuto));
    //}
    void PREGAME_StartScenario()
    {
        print("AM I HERE ");

        CheckWav1Started();


    }

    public void CheckWav1Started()
    {
        if (!_gameStarted)
        {
            GameEventsManager.Instance.Call_OnWomp();
            StartGame();
        }
    }

    private void StartGame()
    {

        //  GameEventsManager.CALL_ToggleStemInput(true);
        // ShowGameMists();

        print("*** 1.0 Gamestarted");
        // _gameStarted = true;
        GameEventsManager.Instance.Call_RzExperienceStarted();
        _levelManager_Compo.Start_LoadedLEvel_Bufftime_W_start_level_ONTIMER(GameSettings.Instance.GET_LONGTIME_10seconds()); //the actual wave.startwave
        WaveStartingGraphics();
        _gameStarted = true;

    }

    //called by LevelManager
    public void HardStop()
    {

        Debug.Log("------------HardStop-----------");

        m_Master_UI_compo.Run_FinalScore_with_3dTitle(_scoreManager_Compo.Get_PointsTotal());
        m_Master_UI_compo.Run_GameOver();
        _levelManager_Compo.StopTheGame();
        GameEventsManager.CALL_ToggleStemInput(false);

        // pause all enemies
        _enemiesManager_Compo.PausLiveZombies(true);
        //gameOverScreen.SetActive(true);
        m_Master_UI_compo.Run_PlayGameOverAudio();
        KngGameState = ARZState.EndGame;
        PersistantScoreGrabber.Instance.DoGrabScores();
        StartCoroutine(AUTOGOTO_DataEntry(10));
        _timeCtrl_Compo.ResetTimeScaleToNormal(false);
    }
    IEnumerator AUTOGOTO_DataEntry(int arg8)
    {
        yield return new WaitForSeconds(arg8);
        _scoreManager_Compo.ResetScore();
        // isLevelLoaded = false;
        // isRoomLoaded = false;
        _gameStarted = false;
        _gameTimeIsUp = false;
        _suddenDeathFlagIsUp = false;
        // Destroy(ScoreBoardRef);
        // SceneManager.LoadScene("KngSetupMenu");
        SceneManager.LoadScene("DataEntry");
    }

    //called by level manager
    public void ResetWave()
    {
        // destroy all enemies
        Debug.Log("gmResetLevel");
        //  _enemiesManager_Compo.RESETLiveZombies();

        //foreach (ZombieBehavior z in GameObject.FindObjectsOfType<ZombieBehavior>())
        //{
        //    Destroy(z.gameObject);
        //}
        // get rid of blood splatters
        // _masterUImngr_Compo.Run_ResetDamage();
        RzPlayerComponent.Instance.ResetRZplayer();
        RzPlayerHealthTubeControllerComponent.Instance.ResetBArs();

        //if no gun->reset gunfor wave
        IsPlayerDead = false;

        //disable game over tag along screen
        // youDiedScreen.SetActive(false);
        m_Master_UI_compo.Run_ResetWave();

        WaveStartingGraphics();

        GameEventsManager.CALL_ToggleStemInput(true);
        _timeCtrl_Compo.ResetTimeScaleToNormal(false);
    }

    //called by wavelevel 
    public EnemiesManager ENEMYMNGER_getter()
    {
        return this._enemiesManager_Compo;
    }
    //called by wavelevel 

    public void GM_Handle_LoadNextLevel()
    {
        if (KngGameState == ARZState.ReachedAllowedTime)
        {
            return;
        }

        Debug.Log("loading next level ");
        _timeCtrl_Compo.ResetTimeScaleToNormal(false);

        _scoreManager_Compo.AddBonusPointForFinishingWaveNumber(_levelManager_Compo.Get_Cure_LoadedLevel_Num());

        _scoreManager_Compo.Increment_WavesPlayedCNT();
        // reset wave points
        _scoreManager_Compo.Reset_WavePoints();

        //could add accuracy bonus here for each wave, if we pas points to this method 

        // activate canvas spash
        m_Master_UI_compo.Run_WaveCompleteWeaponUpgare();

        // load next wave and launch in 10 seconds
        _levelManager_Compo.LevelCompleted_NextLevel();

        // activate next wave splash in 6 seconds
        TimerBehavior t = gameObject.AddComponent<TimerBehavior>();
        t.StartTimer(GameSettings.Instance.Get_StartGet_roman(), WaveStartingGraphics);
        t.StartTimer(2, WaveStartingGraphics);
        PlayerHasDiedInThisWave = false;
        ZombieIndicatorManager.Instance.RemoveAllIndicators();

    }

    public void WaveStartingGraphics()
    {
        //your canvas    


        //if (PlayerHealthBarCTRL.Instance != null)
        //{

        //    PlayerHealthBarCTRL.Instance.ResetBArs();
        //}
        //else
        //{
        //    Debug.Log("No PlayerHealthBAr Found");
        //}



        //if (RzPlayerHealthTubeControllerComponent.Instance != null)
        //{

        //    RzPlayerHealthTubeControllerComponent.Instance.ResetBArs();
        //}
        //else
        //{
        //    Debug.Log("No PlayerHealthBAr Found");
        //}

        if (true)
        {
            Debug.Log(" wave graphic started resethealthbas? just innit rzlayer?");
        }

        string RomanNum = _levelManager_Compo.GetWaveRomanNumeral();
        m_Master_UI_compo.Run_WaveStarted(RomanNum);
        _sceneObjectMnger_Compo.ScoreSign_MNGR.SetWaveNumberDisplay(_levelManager_Compo.Get_Cure_LoadedLevel_NumPliusOne());
        _sceneObjectMnger_Compo.ScoreSign_MNGR.Lower();
    }

    public void SetSuddenDeathRaised()
    {
        print("suddendeath");
        _suddenDeathFlagIsUp = true;
        GameEventsManager.Instance.Call_SuddenDeath();

    }

    bool IS_HELL_TIME = false;

    public bool Get_IsItHellTimeYet() { return IS_HELL_TIME; }
    public void SetPlayerMustDie()
    {

        IS_HELL_TIME = true;

    }

    //for display
    public ScoreManager GetScoreMAnager() { return this._scoreManager_Compo; }
    public StreaksManager GetStreakManager() { return _streaksManager_Compo; }

    public void TimesUp()
    {
        KngGameState = ARZState.EndGame;
        _gameTimeIsUp = true;
        GameEventsManager.CALL_ToggleStemInput(false);
    }
    public void Zombie_ID_Died(int argZid)
    {
        _scoreManager_Compo.Increment_ZombiesKilledCNT();
        _enemiesManager_Compo.RemoveLiveZombie(argZid);
        // _waveManager.RegisterZombieKilled();
        if (KngGameState == ARZState.Pregame)
        {
            Debug.Log("Notify Zombie died to scenario manager");
            // _senarioMNGR.NotifiyZdied(argZid);
        }
        else
           if (KngGameState == ARZState.ReachedAllowedTime)
        {
            return;
        }
        else
        if (KngGameState == ARZState.WaveOverTime)
        {
            if (_enemiesManager_Compo.liveenemies.Count == 0 && _enemiesManager_Compo.Axenemies.Count == 0)
            {
                End_Overtime();
            }
        }

    }
    public void OvertimeThrownCheckIfEndNow()
    {
        if (_enemiesManager_Compo.liveenemies.Count == 0 && _enemiesManager_Compo.Axenemies.Count == 0)
        {
            End_Overtime();
        }
    }
    public void ZombieAXEMAN_ID_Died(int argZid)
    {
        _scoreManager_Compo.Increment_ZombiesKilledCNT();
        _enemiesManager_Compo.RemoveAxeZombie(argZid);
        // _waveManager.RegisterZombieKilled();
        if (KngGameState == ARZState.Pregame)
        {
#if ENABLE_DEBUGLOG
            Debug.Log("should notify scenario manager"); //_senarioMNGR.NotifiyZdied(argZid);
#endif
        }
        else
           if (KngGameState == ARZState.ReachedAllowedTime)
        {
            return;
        }
        else
        if (KngGameState == ARZState.WaveOverTime)
        {
            if (_enemiesManager_Compo.liveenemies.Count == 0 && _enemiesManager_Compo.Axenemies.Count == 0)
            {
                End_Overtime();
            }
        }

    }

    public void RemoveFlyid(int id)
    {
        _enemiesManager_Compo.RemoveFlyZombie(id);
    }
    public void RemoveAxeid(int id)
    {
        _enemiesManager_Compo.RemoveEnemyProjectile(id);
    }
    public void End_Overtime()
    {
        if (KngGameState == ARZState.ReachedAllowedTime)
        {
            return;
        }

        KngGameState = ARZState.WaveEnded;
        GM_Handle_LoadNextLevel();
    }
    public int GET_CURRLEVELnumber()
    {
        return 1;//........................................._levelManager_Compo.Get_Cur_iLEvel().Get_LevelNumber();
    }



    public Vector3 GetStaticPLayerPosition()
    {
        //if (GameSettings.Instance.GameMode == ARZGameModes.GameLeft_Alpha)
        //{
        //   // return _WPstruct.GetHotSpot(0).transform.position;
        //}
        //else
        //{
        //   // return _WPstruct.GetHotSpot(1).transform.position; ;
        //}
        // Debug.Log("BROKEN");
        return this.transform.position;
    }

    public bool DidRzEperienceStartYet()
    {
        return _gameStarted;
    }

    public List<GameObject> GEtAllives()
    {
        return _enemiesManager_Compo.liveenemies;
    }
    public void PlayerDied_GameManager()
    {
#if ENABLE_DEBUGLOG
#endif
        Debug.Log("PlayerDied_GameManager");
        _timeCtrl_Compo.ResetTimeScaleToNormal(false);
        IsPlayerDead = true;
        PlayerHasDiedInThisWave = true;
        GameEventsManager.CALL_ToggleStemInput(false);
        _scoreManager_Compo.Increment_DeathsCNT();

        // pause all enemies
        // _enemiesManager.PausLiveZombies();
        // ZombieIndicatorManager.Instance.RemoveAllIndicators();
        _enemiesManager_Compo.DisolveAllZombiesOnPlayerDeath();  //should paus all , and disolve and remove all enemies flies axeman and indicators

        if (_suddenDeathFlagIsUp) { HardStopSuudendeath(); return; }

        if (_gameTimeIsUp)
        {
#if ENABLE_DEBUGLOG
#endif
            Debug.Log("gameover yo");
            // game over
            m_Master_UI_compo.Run_PlayGameOverAudio();

            m_Master_UI_compo.Run_FinalScore_with_3dTitle(_scoreManager_Compo.Get_PointsTotal());
            m_Master_UI_compo.Run_GameOver();
            _levelManager_Compo.StopTheGame();

        }
        else
        {
            //*******************************************************************************************************************
            //THIS NEVER HAPPENED gametimeIsup is Never set to true bcause nothing ever calst TimeUp() , instead we use HArdStop
            //*******************************************************************************************************************

            int totalpointsToBeLost = _scoreManager_Compo.Get_PointsCurWave();
            int CurWavePoints = totalpointsToBeLost;
            GameEventsManager.Instance.Call_OnOnBoardDisplay(CurWavePoints, false);
            //  _masterUImngr.Run_YouDied(CurWavePoints);

            _scoreManager_Compo.Update_Add_PointsTotalLost(CurWavePoints);
            _scoreManager_Compo.Update_Remove_PointsTotal(CurWavePoints);
            _scoreManager_Compo.Reset_WavePoints();

            //ScoreDebugCon.Instance.Update_WAVEPoints(0);

            // tell wave manager whether or not to reload wave via if time is up
            //OnGameOver_WaveManager(false);
            _levelManager_Compo.PlayerDiedResetLevel();
        }

    }

    private void HardStopSuudendeath()
    {
        // Logger.Debug("gameover yo");
        // game over

#if ENABLE_DEBUGLOG
#endif
        Debug.Log("------------HardStopSuudendeath-----------");
        m_Master_UI_compo.Run_FinalScore_with_3dTitle(_scoreManager_Compo.Get_PointsTotal());
        m_Master_UI_compo.Run_GameOver();
        _levelManager_Compo.StopTheGame();
        GameEventsManager.CALL_ToggleStemInput(false);

        // pause all enemies
        _enemiesManager_Compo.PausLiveZombies(true);
        m_Master_UI_compo.Run_PlayGameOverAudio();
        KngGameState = ARZState.EndGame;
        PersistantScoreGrabber.Instance.DoGrabScores();
        ZombieIndicatorManager.Instance.RemoveAllIndicators();
        StartCoroutine(AUTOGOTO_DataEntry(10));
    }

    public void GM_Wake_PregameZombies()
    {
        //  _enemiesManager_Compo.AWAKEALLandWALK();
    }

    void LOGMESSAGE(string argmessage)
    {
#if ENABLE_DEBUGLOG
        Debug.Log(argmessage);
#endif
    }





    public LevelManager GetLevelManager() { return _levelManager_Compo; }
    //private void Update()
    //{
    //  //  if (Input.GetKey(KeyCode.L)) { PlayerDied_GameManager(); }
    //}

}