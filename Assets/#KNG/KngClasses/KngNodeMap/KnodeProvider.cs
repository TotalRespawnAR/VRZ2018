//#define ENABLE_DEBUGLOG
//#define ENABLE_KEYBORADINPUTS
using System.Collections.Generic;
using UnityEngine;

public class KnodeProvider : MonoBehaviour
{
    void OnEnable()
    {
        GameEventsManager.OnWaveStartedOrReset_DEO += SetLoadedLEvelRef;
    }

    private void OnDisable()
    {
        GameEventsManager.OnWaveStartedOrReset_DEO -= SetLoadedLEvelRef;
    }
    void SetLoadedLEvelRef(WaveLevel argLoadedlevel)
    {
        Debug.Log("is this called");
        Loaded_Level = null;
        Loaded_Level = argLoadedlevel;
        // ResetAllLandNodeToFree_AndClearLines();

        PlaceUndergtound_AND_SetTunnelInactivesNodes();
        //  Set_SpawnPointsANDGravePointsList( LocalWaveLevel.Get_LevelGravePointIds(), LocalWaveLevel.Get_LevelSpawnPointIds());

        Initialize_SpawnColors(true); //false will color the nodes the turn em offs
    }

    public static KnodeProvider Instance = null;
    KNodeManager m_KnodeManager;
    WaveLevel Loaded_Level;
    KnProvVisualzer m_visualizer;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // m_KnodeManager = GetComponent<KNodeManager>();
            m_visualizer = GetComponent<KnProvVisualzer>();
            // m_visualizer.EraseAll();
        }
        else
            Destroy(gameObject);
    }
    //List<KNode> SpawnNodes;
    //List<KNode> GraveNodes;
    //List<KNode> FlyNodes;
    //List<KNode> UpNodes;
    //List<KNode> DownNodes;

    public bool isReadyForRequests = false;
    Color ColorSpawn = Color.yellow;
    Color ColorGrave = Color.blue;
    Color ColorUp = Color.cyan;
    Color ColorDown = Color.magenta;
    Color ColorFly = Color.green;
    Color ColorFinal = Color.black;
    int kspawn_RoundRobIndex = 0; //knodespawnmanager should 
    int kgravespawn_RoundRobIndex = 0;
    int ktunnelspawn_RoundRobIndex = 0;

    //knodemanager  calls this when is's been initialized after listening to room and stem placed (now all trans are in pace, and refs can be sent)
    public void SetReadyForRequests(KNodeManager readyManager)
    {
#if ENABLE_DEBUGLOG
        Debug.Log("KNP SetReadyForRequests");
#endif
        m_KnodeManager = readyManager;
        isReadyForRequests = true;
        // ColorFinals();

    }

    public KNode GEtClosestKnode(Vector3 argPos)
    {

        indexOfClossesZombie = 0;
        List<KNode> freeknodes = m_KnodeManager.GetAllFreeKnodes();
        for (int zcnt = 0; zcnt < freeknodes.Count; zcnt++)
        {
            float tempDist = Vector3.Distance(freeknodes[zcnt].GetPos(), argPos);
            if (tempDist < ShortestDistToPlayer)
            {
                ShortestDistToPlayer = tempDist;
                indexOfClossesZombie = zcnt;
            }
        }
        return freeknodes[indexOfClossesZombie];
    }

    float ShortestDistToPlayer = float.MaxValue;
    int indexOfClossesZombie = 0;
    public KNode RequestNextKnode(KNode cur)
    {

        if (cur.NextNeighbors == null)
        {
            return m_KnodeManager.ExplicitRootNode;
        }

        KNode Bestnode = null;
        // List<KNode> ListREf = null ;
        for (int neighborIndex = 0; neighborIndex < cur.NextNeighbors.Count; neighborIndex++)
        {
            if (cur.NextNeighbors[neighborIndex].IsFree)
            {
                Bestnode = cur.NextNeighbors[neighborIndex];
                break;
            }
        }
        if (Bestnode == null) Bestnode = cur.NextNeighbors[0];

        //could be null, make sure ther IS a path
        return Bestnode;

        //   return cur.NextNeighbors[0];
    }

    //bool onoff = true;
    //public KNode Get_RoundRobin_Spawnpoint() {
    //    kspawn_RoundRobIndex++;
    //    if (kspawn_RoundRobIndex >= SpawnNodes.Count) {
    //        kspawn_RoundRobIndex = 0;
    //    }
    //    return SpawnNodes[kspawn_RoundRobIndex];
    //}

    //public KNode Get_RoundRobin_Gravepoint()
    //{
    //    //Debug.Log("RR"+ GraveNodes.Count + " grave points ");

    //    kgravespawn_RoundRobIndex++;
    //    if (kgravespawn_RoundRobIndex >= GraveNodes.Count)
    //    {
    //        kgravespawn_RoundRobIndex = 0;
    //    }


    //    return GraveNodes[kspawn_RoundRobIndex];
    //}

    //public KNode Get_RoundRobin_Tunelpoint() 
    //{
    //    ktunnelspawn_RoundRobIndex++;
    //    if (ktunnelspawn_RoundRobIndex >= DownNodes.Count)
    //    {
    //        ktunnelspawn_RoundRobIndex = 0;
    //    }

    //    return DownNodes[ktunnelspawn_RoundRobIndex];
    //}

    void Initialize_SpawnColors(bool argColo)
    {
#if ENABLE_DEBUGLOG
        Debug.Log("KNP Initialize_SpawnColors");
#endif

        if (Loaded_Level.Get_ListSpawnPointIds() != null)
        {
            foreach (int sid in Loaded_Level.Get_ListSpawnPointIds())
            {
                m_KnodeManager.GetNode(sid).Set_Original_Color(ColorSpawn);
            }
        }
        if (Loaded_Level.Get_ListGravePointIds() != null)
        {
            foreach (int gid in Loaded_Level.Get_ListGravePointIds())
            {
                m_KnodeManager.GetNode(gid).Set_Original_Color(ColorGrave);
            }
        }
        if (Loaded_Level.Get_ListTunnelSpawnPointIds() != null)
        {
            foreach (int tid in Loaded_Level.Get_ListTunnelSpawnPointIds())
            {
                m_KnodeManager.GetNode(tid).Set_Original_Color(ColorGrave);
            }
        }

        //  m_KnodeManager.TogglePointsMeshes(GameManager.Instance.TESTON);   //old 
        // m_visualizer.UpdateNExtNeighbors(m_KnodeManager.GetAll_LandNodes());
    }


    /*
public KNode Get_RandNode()
{
    List<KNode> freeknodes = m_KnodeManager.GetAllFreeKnodes();
    int rnd = Random.Range(1, freeknodes.Count-1);
    return freeknodes[rnd];
}
*/

    public KNode GetNodeByID(int argid)
    {
        return m_KnodeManager.GetNode(argid);
    }

    public void ResetAllLandNodeToFree_AndClearLines()
    {
#if ENABLE_DEBUGLOG
        Debug.Log("KNP   ResetAllLandNodeToFree");
#endif
        m_KnodeManager.FreeAllNodes();
        m_visualizer.EraseAll();
    }

    public void PlaceUndergtound_AND_SetTunnelInactivesNodes()
    {
#if ENABLE_DEBUGLOG
        Debug.Log("KNP   PlaceUndergtound_AND_SetTunnelInactivesNodes");
#endif
        //m_KnodeManager.PostionTunnels(LocalWaveLevel.TunelLevel);


        //if (LocalWaveLevel.TunelLevel == MidNearNone.NEAR)
        //{
        //    if (LocalWaveLevel.PlayerSideTunnelOn) {
        //        if (GameSettings.Instance.GameMode == ARZGameModes.GameLeft_Alpha) {
        //        m_KnodeManager.GetNode(25).IsFree = false;
        //        m_KnodeManager.GetNode(33).IsFree = false;
        //        }

        //    }

        //    if (LocalWaveLevel.OtherSideTunnelOn)
        //    {
        //        m_KnodeManager.GetNode(30).IsFree = false;
        //        m_KnodeManager.GetNode(38).IsFree = false;
        //    }
        //}
        //else
        // if (LocalWaveLevel.TunelLevel == MidNearNone.MID)
        //{
        //    if (LocalWaveLevel.PlayerSideTunnelOn)
        //    {
        //        if (GameSettings.Instance.GameMode == ARZGameModes.GameLeft_Alpha)
        //        {
        //            m_KnodeManager.GetNode(17).IsFree = false;
        //            m_KnodeManager.GetNode(25).IsFree = false;
        //        }

        //    }

        //    if (LocalWaveLevel.OtherSideTunnelOn)
        //    {
        //        m_KnodeManager.GetNode(22).IsFree = false;
        //        m_KnodeManager.GetNode(30).IsFree = false;
        //    }


        //}

        //m_visualizer.UpdateNExtNeighborsVisually(m_KnodeManager.GetAll_LandNodes());
    }

    public Transform GEt_DoorAlphaTrans() { return m_KnodeManager.DoorLeftPos.transform; }
    public Transform GEt_DoorBravoTrans() { return m_KnodeManager.DoorRightPos.transform; }
    public KNode GEt_PlayerAlphaBravo()
    {
        if (GameSettings.Instance.GameMode == ARZGameModes.GameLeft_Alpha)
        {
            return m_KnodeManager.ExplicitNodeAlpha;
        }
        else
            return
                m_KnodeManager.ExplicitNodeBravo;
    }
#if ENABLE_KEYBORADINPUTS
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Delete))
        {

            m_visualizer.EraseAll();
        }
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            //  SetTunnelInactives(0);
            m_visualizer.UpdateNExtNeighborsVisually(m_KnodeManager.GetAll_LandNodes());
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            //  SetTunnelInactives(0);
        }

    }

#endif

    public KNode GET_knode(int row, int col)
    {
        return m_KnodeManager.Knodes2D[row, col];
    }
}

























//    void ColorFinals()
//    {

//        if (GraveNodes != null)
//        {
//            foreach (KNode gravenode in GraveNodes)
//            {
//                if (gravenode.IsFinal) { gravenode.Set_Original_Color(ColorFinal); }
//                else
//                    gravenode.Set_Original_Color(ColorGrave);
//            }
//        }
//        else
//        {
//#if ENABLE_DEBUGLOG
//            Debug.LogError("KNP NO GraveNodes");
//#endif
//        }

//        if (SpawnNodes != null)
//        {
//            foreach (KNode spawnNode in SpawnNodes)
//            {
//                if (spawnNode.IsFinal) { spawnNode.Set_Original_Color(ColorFinal); }
//                else
//                    spawnNode.Set_Original_Color(ColorSpawn);
//            }
//        }
//        else
//        {
//#if ENABLE_DEBUGLOG
//            Debug.LogError("KNP NO SpawnNodes");
//#endif
//        }


//        if (UpNodes != null)
//        {
//            foreach (KNode spawnNode in UpNodes)
//            {
//                if (spawnNode.IsFinal) { spawnNode.Set_Original_Color(ColorFinal); }

//            }
//        }
//        else
//        {
//#if ENABLE_DEBUGLOG
//            Debug.LogError("KNP NO UpNodes");
//#endif
//        }


//        if (DownNodes != null)
//        {
//            foreach (KNode spawnNode in DownNodes)
//            {
//                if (spawnNode.IsFinal) { spawnNode.Set_Original_Color(ColorFinal); }

//            }
//        }
//        else
//        {
//#if ENABLE_DEBUGLOG
//            Debug.LogError("KNP NO DownNodes");
//#endif
//        }

//        if (FlyNodes != null)
//        {
//            foreach (KNode spawnNode in FlyNodes)
//            {
//                if (spawnNode.IsFinal) { spawnNode.Set_Original_Color(ColorFinal); }

//            }
//        }
//        else
//        {
//#if ENABLE_DEBUGLOG
//            Debug.LogError("KNP NO FlyNodes");
//#endif
//        }

//        foreach (KNode kn in m_KnodeManager.GetAll_LandNodes())
//        {
//            if (kn.IsFinal) { kn.Set_Original_Color(ColorFinal); }
//        }

//    }


/*
//LEvel will call this on Level.Start()
public void Set_SpawnPointsANDGravePointsList(List<int> LevelGraveIds, List<int> argLEvelSpawnIds)
{

      kspawnPTR = 0; //knodespawnmanager should 
      kgravespawnPTR = 0;
      ktunnelSapwnPTR = 0;

#if ENABLE_DEBUGLOG
    Debug.Log("KNP Set_SpawnPointsANDGravePointsList");
#endif
    if (GraveNodes != null)
    {
        GraveNodes.Clear();
    }
    else {
        GraveNodes = new List<KNode>();
    }
    foreach (int gravenodeID in LevelGraveIds) {
        GraveNodes.Add(m_KnodeManager.KnodeDICT[gravenodeID]);
    }

    if (SpawnNodes != null)
    {
        SpawnNodes.Clear();
    }
    else
    {
        SpawnNodes = new List<KNode>();
    }
    foreach (int spawnnodeids in argLEvelSpawnIds)
    {
        SpawnNodes.Add(m_KnodeManager.KnodeDICT[spawnnodeids]);
    }



    if (FlyNodes != null)
    {
        FlyNodes.Clear();
    }
    else
    {
        FlyNodes = new List<KNode>();
        FlyNodes.Add(m_KnodeManager.FlyNodes0[0]);
        FlyNodes.Add(m_KnodeManager.FlyNodes1[0]);
    }


    if (UpNodes != null)
    {
        UpNodes.Clear();
    }
    else
    {
        UpNodes = new List<KNode>();
        UpNodes.AddRange(m_KnodeManager.UpstairsNodes);

    }



    if (DownNodes == null) {
        DownNodes = new List<KNode>();
    }
    //if(LocalWaveLevel.TunelLevel == MidNearNone.NONE)
    //{
    //    return;
    //}
    //else
    //{

    //    if (LocalWaveLevel.PlayerSideTunnelOn)
    //    {
    //        if (GameSettings.Instance.GameMode == ARZGameModes.GameLeft_Alpha)
    //        {
    //            DownNodes.Add(m_KnodeManager.TunnelLeft[0]);
    //        }
    //        else
    //        {
    //            DownNodes.Add(m_KnodeManager.TunnelRight[0]);
    //        }

    //    }
    //    if (LocalWaveLevel.OtherSideTunnelOn)
    //    {
    //        if (GameSettings.Instance.GameMode == ARZGameModes.GameLeft_Alpha)
    //        {
    //            DownNodes.Add(m_KnodeManager.TunnelRight[0]);
    //        }
    //        else
    //        {
    //            DownNodes.Add(m_KnodeManager.TunnelLeft[0]);
    //        }
    //    }
    //}


}
*/
