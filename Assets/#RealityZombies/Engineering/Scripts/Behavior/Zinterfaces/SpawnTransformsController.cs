using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTransformsController : MonoBehaviour
{
    /*


    #region PrivateVars

    //groups
    public List<Transform> SpawnsFar;
    public Transform[] activeSpawnPointsForWaveLevel;

    public Transform[] activeThrowerSpawnPointsLevel;
    public Transform[] activeBigBossSpawnPointsLevel;
    public List<Transform> SpawnsOuter; //only for udpBossed
    Transform[] activeGravesPointsForWaveLevel;
    public List<Transform> SpawnsInner;  //only for axtthrowers
    List<Transform> GravePointsFar;
    List<Transform> GravePointsMid;
    List<Transform> GravePointsNear;
    public List<Transform> TwoThrowerSpawnPoints;
    public List<Transform> TwoBigBossSpawnPoints;

    ////Transform[] all_Nears;
    int RRptr_far = 0;
    int GravePTR = 0;
    int ThrowerPointPTR = 0;
    int BigBossPointPTR = 0;
    //  int MAX_farPTR = 0;
    ////int RRptr_near=0;
    WaveLevel _curWave;
        bool MAXOutAxeZombies = false;
    #endregion

    #region INIT

    void OnEnable()
    {
        GameEventsManager.OnWaveStartedOrReset_DEO += SetLoadedLEvelRef;
    }

    private void OnDisable()
    {
        GameEventsManager.OnWaveStartedOrReset_DEO -= SetLoadedLEvelRef;
    }
    public void SetLoadedLEvelRef(WaveLevel argLoadedLevel)
    {
        _curWave = argLoadedLevel;
        CreatTransArrayForRR(_curWave.Get_WaveLevelNumber());
    }
    private void Awake()
    {
        InitPlaces();
    }

    void Start()
    {

    }



    void InitPlaces()
    {
        SpawnsFar = new List<Transform>();
        SpawnsOuter = new List<Transform>();
        SpawnsInner = new List<Transform>();
        TwoThrowerSpawnPoints = new List<Transform>();
        TwoBigBossSpawnPoints = new List<Transform>();
        GravePointsFar = new List<Transform>();
        GravePointsMid = new List<Transform>();
        GravePointsNear = new List<Transform>();
    }
    #endregion

    #region PublicMethods
    //public void Set_FARandNEAR_spawnTransforms(List<Transform> argSpawnFar, List<Transform> argSpawnnear)
    //{
    //    SpawnsFar = argSpawnFar;
    //    SpawnsArchFar = argSpawnnear;

    //}
    KngSpawnPointsCTRL test;

    public void Set_SpawPointGroupsTransforms(WayPointsStruct argSpawnstruct)
    {
        SpawnsFar = argSpawnstruct.GetSpawnPoitGroupByRow(0);
        SpawnsOuter = argSpawnstruct.GetSpawnPoitGroupByRow(1);
        SpawnsInner = argSpawnstruct.GetSpawnPoitGroupByRow(3);
        GravePointsFar = argSpawnstruct.GetGravePoitGroupByRow(1);
        GravePointsMid = argSpawnstruct.GetGravePoitGroupByRow(2);
        GravePointsNear = argSpawnstruct.GetGravePoitGroupByRow(3);

        test = SpawnsFar[0].gameObject.GetComponent<KngSpawnPointsCTRL>();
        //string s = test.GivenName;
        //print(s);
    }

    int maxLiveAxeman = 2;
    int CurCreatedAxeman = 0;

    public void DoWorkWithThisBatch(List<Data_Enemy> argBatch)
    {
        if (MAXOutAxeZombies)
        {
            maxLiveAxeman = 2;
        }
        else {
            maxLiveAxeman = 1;
        }
        CurCreatedAxeman = 0; //reset here when a batch comes

        if (argBatch == null) { return; }
        foreach (Data_Enemy de in argBatch)
        {

            if (de.Ztype_STD == ARZombieypes.Thrower)
            {
                int LiveAxman = GameManager.Instance.ENEMYMNGER_getter().Axenemies.Count;
                if (  LiveAxman >= maxLiveAxeman) {
                    continue;
                }


                Transform HERE = activeThrowerSpawnPointsLevel[ThrowerPointPTR];
                ThrowerPointPTR++;
                if (ThrowerPointPTR >= activeThrowerSpawnPointsLevel.Length)
                {
                    ThrowerPointPTR = 0;
                }
                de.SpawnTrans = HERE;
                GameManager.Instance.REQ_NewAxeGuy(de);
                CurCreatedAxeman++;
            }
            else
            if (de.Ztype_STD == ARZombieypes.STANDARD)
            {
                Transform HERE = activeSpawnPointsForWaveLevel[RRptr_far];
                RRptr_far++;
                if (RRptr_far >= activeSpawnPointsForWaveLevel.Length)
                {
                    RRptr_far = 0;
                }
                // GameManager.Instance.REQ_ZNew(HERE, (int)de.ModelName, de.MaterialNum123_optinal, de.HitPoints, de.Ztype_STD, de.InitialState);
                de.SpawnTrans = HERE;

                GameManager.Instance.REQ_ZNew(de);
                //GameManager.Instance.Req_ZombieOnANY_WAYPOINT(2,0,de); 
            }
            else
            if (de.Ztype_STD == ARZombieypes.GRAVER)
            {

                Transform HERE = activeGravesPointsForWaveLevel[GravePTR];// GameManager.Instance.GET_GravesManager().Graves[GravePTR].transform;
                GravePTR++;
                if (GravePTR >= activeGravesPointsForWaveLevel.Length)
                {
                    GravePTR = 0;
                }
                de.SpawnTrans = HERE;
                //GameManager.Instance.Req_ZombieOnANY_WAYPOINT(2, 0, de);
                GameManager.Instance.REQ_ZNew(de);
            }
            else
            if (de.Ztype_STD == ARZombieypes.BIGBOSS)  //NOT YET IMPLEMENTED 
            {
                Debug.Log("No BIgBoss implementation");
            }
            else
            if (de.Ztype_STD == ARZombieypes.FLY1)  //NOT YET IMPLEMENTED 
            {
                Debug.Log("No FLY1 implementation");
            }
            else
            if (de.Ztype_STD == ARZombieypes.FLY2)  //NOT YET IMPLEMENTED 
            {
                Debug.Log("No FLY2 implementation");
            }
        }
    }

    public void SpawnFly()
    {

        if (GameManager.Instance.KngGameState != ARZState.WavePlay) return;
        GameManager.Instance.REQ_NewFly();

    }

    public void SpawnAxeGuy()
    {
        if (GameManager.Instance.KngGameState != ARZState.WavePlay) return;
        GameManager.Instance.REQ_AxeGuyOnSpawn();
    }



    void CreatTransArrayForRR(int _argSpawnerLEvel)
    {
        if (_argSpawnerLEvel > 2) { MAXOutAxeZombies = true; }
        


        List<Transform> ActivSpawnPoints = new List<Transform>();
        List<Transform> ActivGravePoints = new List<Transform>();

        ActivSpawnPoints.AddRange(SpawnsFar);
        ActivGravePoints.AddRange(GravePointsFar);
        ActivGravePoints.AddRange(GravePointsMid);
        ActivGravePoints.AddRange(GravePointsNear);

        TwoThrowerSpawnPoints.AddRange(SpawnsOuter);
        TwoBigBossSpawnPoints.AddRange(SpawnsInner);

        activeGravesPointsForWaveLevel = ActivGravePoints.ToArray();
        activeSpawnPointsForWaveLevel = ActivSpawnPoints.ToArray();
        activeThrowerSpawnPointsLevel = TwoThrowerSpawnPoints.ToArray();
        activeBigBossSpawnPointsLevel = TwoBigBossSpawnPoints.ToArray();
    }
    #endregion

    #region PrivateMethods
    //void Update()
    //{

    //}

    //private void OnDestroy()
    //{

    //}
    #endregion

    */
}
