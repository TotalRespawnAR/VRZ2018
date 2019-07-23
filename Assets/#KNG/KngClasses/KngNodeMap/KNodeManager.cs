//#define ENABLE_DEBUGLOG
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KNodeManager : MonoBehaviour
{

    private void OnEnable()
    {
        GameEventsManager.OnGameObjectAnchoredPlaced += ListenTo_AnchoresPlaced;
        // GameEventsManager.OnWaveStartedOrReset_DEO += StartDectivating_onHEardWaveStarted;

    }
    private void OnDisable()
    {
        GameEventsManager.OnGameObjectAnchoredPlaced -= ListenTo_AnchoresPlaced;
        //GameEventsManager.OnWaveStartedOrReset_DEO -= StartDectivating_onHEardWaveStarted;

    }


    #region Check_2_Ancs_Placed
    bool RoomWasAnchored = false;
    bool StemWasAnchored = false;
    void ListenTo_AnchoresPlaced(string argPlacedAnhorName)
    {
        if (argPlacedAnhorName.Contains(GameSettings.Instance.AncName_WindowBasedLand()))
        {
            RoomWasAnchored = true;

        }
        if (argPlacedAnhorName == GameSettings.Instance.AncName_ArenaStemBase())
        {
            StemWasAnchored = true;

        }

        if (RoomWasAnchored && StemWasAnchored)
        {
            Init_mustWaitForWACplaced();
            ////get the level to do this so that it can also initialize spawn lists 
            KnodeProvider.Instance.SetReadyForRequests(this);
        }
    }
    #endregion

    //row0 is the further row back . should be usd as spawnpoint
    const int TotalRows = 8;
    const int TotalCols = 8;
    int IndexLastRow = TotalRows - 1;
    int IndexLastCol = TotalCols - 1;
    Color FinalColorOrange;
    [SerializeField]
    public Dictionary<int, KNode> KnodeDICT;
    [SerializeField]
    public KNode[,] Knodes2D; //this 2D array is only used to figure linking nodes with their neighbors

    public KNode RandomeNode;
    public KNode ExplicitRootNode;
    public KNode ExplicitNodeAlpha;
    public KNode ExplicitNodeBravo;
    public KNode[] UpstairsNodes = new KNode[8];
    public KNode[] FlyNodes0 = new KNode[5];
    public KNode[] FlyNodes1 = new KNode[5];
    public KNode[] TunnelLeft = new KNode[2];
    public KNode[] TunnelRight = new KNode[2];
    private void InitNodePos()
    {
#if ENABLE_DEBUGLOG
        Debug.Log(" mnger Init_mustWaitForWACplaced");
#endif
        ExplicitRootNode.SetPos();
        ExplicitNodeAlpha.SetPos();
        ExplicitNodeBravo.SetPos();
        RandomeNode.SetPos();
        foreach (KNode n in UpstairsNodes) { n.SetPos(); }
        foreach (KNode n in FlyNodes0) { n.SetPos(); }
        foreach (KNode n in FlyNodes1) { n.SetPos(); }
        foreach (KNode n in TunnelLeft) { n.SetPos(); }
        foreach (KNode n in TunnelRight) { n.SetPos(); }
        for (int r = 0; r < TotalRows; r++)
        {
            for (int c = 0; c < TotalCols; c++)
            {
                Knodes2D[r, c].SetPos();
            }
        }
    }
    public GameObject LandMesh;
    public GameObject UnderLandMesh;
    public GameObject ColesiumMesh;

    public Material OcclusionMAt;



    public GameObject LandFloorColliderMesh;

    public GameObject DoorLeftPos;
    public GameObject DoorRightPos;

    private void Awake()
    {//SceneAncPlace or SceneAnchorsSeriDeci Dont init anythig, you're just here for looks
        if (SceneManager.GetActiveScene().name.Contains("Scene"))
        {
#if ENABLE_DEBUGLOG
            Debug.Log(" Kngg Node manager will not init ");
#endif

            return;
        }
        FinalColorOrange = new Color(0.2F, 0.3F, 0.4F);
        KnodeDICT = new Dictionary<int, KNode>();
        Knodes2D = new KNode[8, 8];
        // Init_mustWaitForWACplaced();//later make sure the land was anchored first. try just getting an event listener here to fire this off
        //just a test
        //RandomeNode = Knodes2D[2, 5];

    }
    private void Start()
    {
        //Init_mustWaitForWACplaced();
    }
    #region Init the2darray

    //gamemanager listens for both placed stem and land and will call this gamemanager.in lat_init
    public void Init_mustWaitForWACplaced()
    {
#if ENABLE_DEBUGLOG
        Debug.Log(" mnger Init_mustWaitForWACplaced");
#endif
        Init_2DArray_WithChildTrans();
        LinKKnodes();//Also needs to wait
        OrderNEighbors();

        Link_UpNodes();
        Init_Knode_DICT();


        // KnodesMeshRenderers(false);
        // FloorMeshRenderers(false);
        InitNodePos();

        KnodeProvider.Instance.SetReadyForRequests(this);
    }

    void Init_2DArray_WithChildTrans()
    {
        for (int k = 0; k < transform.childCount; k++)
        {
            Fill2DArray_SetID_SetTrans(transform.GetChild(k), k);
        }
    }
    void Fill2DArray_SetID_SetTrans(Transform argTran, int argChildNumber)
    {

#if ENABLE_DEBUGLOG
        //  Debug.Log("first 2 letters " + argTran.name.Substring(0, 2));
#endif
        if (argTran.name.Substring(0, 2) == "KN")
        {
            KNode kn = argTran.GetComponent<KNode>();
            kn.KnokeID = argChildNumber;
            kn.KnodeTransform = argTran;
            int Row = Convert.ToInt32(argTran.name.Split('_')[1]);
            int Column = Convert.ToInt32(argTran.name.Split('_')[2]);
            kn.MyColumn = Column;
            kn.MyRow = Row;
            AddKnodeTO2Darray(kn, Row, Column);
        }

    }

    void AddKnodeTO2Darray(KNode argKnode, int argRow, int argCol)
    {
        Knodes2D[argRow, argCol] = argKnode;
    }

    void Init_Knode_DICT()
    {
        for (int r = 0; r < TotalRows; r++)
        {
            for (int c = 0; c < TotalCols; c++)
            {
                KnodeDICT.Add(Knodes2D[r, c].KnokeID, Knodes2D[r, c]);
            }
        }
    }
    #endregion

    #region Linking Nodes
    void Link_LateralsKnodes(int _curNodeRow, int _curNodeColumn)
    {
        KNode _curKn = Knodes2D[_curNodeRow, _curNodeColumn];
        if (_curNodeColumn == 0)
        {
            _curKn.Left_fromPlayersview = _curKn.Right_fromPlayerView = Knodes2D[_curNodeRow, _curNodeColumn + 1]; //if left most , link both right and left to the same node as to not have nulls and keep enemy mooving 
        }
        else
        if (_curNodeColumn == IndexLastCol)
        {
            _curKn.Left_fromPlayersview = _curKn.Right_fromPlayerView = Knodes2D[_curNodeRow, _curNodeColumn - 1];

        }
        else
        {
            _curKn.Right_fromPlayerView = Knodes2D[_curNodeRow, _curNodeColumn + 1];
            _curKn.Left_fromPlayersview = Knodes2D[_curNodeRow, _curNodeColumn - 1];
        }


    }

    void Link_NextKnodes(int _curNodeRow, int _curNodeColumn)
    {
        KNode _cur_kn = Knodes2D[_curNodeRow, _curNodeColumn];
        // if row closest to player 
        if (_curNodeRow >= TotalRows - 1)
            return;

        for (int c = 0; c < TotalCols; c++)
        {
            KNode _nextkn = Knodes2D[_curNodeRow + 1, c];
            _cur_kn.NextNeighbors.Add(_nextkn);
        }



    }

    void Link_LastRowWithAN_BN()
    {

        for (int c = 0; c < TotalCols; c++)
        {
            KNode _lastRowNode = Knodes2D[TotalRows - 1, c];
            KNode _nextkn = ExplicitNodeAlpha;
            if (GameSettings.Instance)
            {
                if (GameSettings.Instance.GameMode == ARZGameModes.GameRight_Bravo)
                    _nextkn = ExplicitNodeBravo;
            }
#if ENABLE_DEBUGLOG
            Debug.Log("linking KN_" + _lastRowNode + "_" + c + " to " + _nextkn.gameObject.name);
#endif
            _lastRowNode.NextNeighbors.Add(_nextkn);
            _nextkn.IsFinal = true;
            _lastRowNode.IsFinal = true;
        }



    }

    void Link_PrevKnodes(int _curNodeRow, int _curNodeColumn)
    {
        KNode _cur_kn = Knodes2D[_curNodeRow, _curNodeColumn];
        // if row closest to player 
        if (_curNodeRow == 0)
        {
            for (int c = 0; c < TotalCols; c++)
            {
                KNode _prevkn = ExplicitRootNode;
                _cur_kn.PrevNeighbors.Add(_prevkn);
            }

        }
        else
        {
            for (int c = 0; c < TotalCols; c++)
            {
                KNode _prevkn = Knodes2D[_curNodeRow - 1, c];
                _cur_kn.PrevNeighbors.Add(_prevkn);
            }

        }

    }

    void Link_UpNodes()
    {
        for (int u = 0; u < TotalCols; u++)
        {
            UpstairsNodes[u].NextNeighbors.Add(Knodes2D[1, u]);
        }
    }

    void LinKKnodes()
    {
        for (int r = 0; r < TotalRows; r++)
        {
            for (int c = 0; c < TotalCols; c++)
            {
                Link_LateralsKnodes(r, c);
                Link_NextKnodes(r, c);
                Link_PrevKnodes(r, c);
            }
        }

        Link_LastRowWithAN_BN();

    }

    void OrderNEighbors()
    {


        for (int r = 0; r < TotalRows; r++)
        {
            for (int c = 0; c < TotalCols; c++)
            {
                OrderNextNodes(c, Knodes2D[r, c].PrevNeighbors);
                OrderNextNodes(c, Knodes2D[r, c].NextNeighbors);
            }
        }


    }

    void OrderNextNodes(int argCol, List<KNode> NextNodes)
    {
        foreach (KNode n in NextNodes)
        {
            n.DistToCol = Math.Abs(n.MyColumn - argCol);
        }
        NextNodes.Sort((x, y) => x.DistToCol.CompareTo(Math.Abs(y.DistToCol)));
    }

    void KnodesMeshRenderers(bool argONOFF)
    {
        for (int r = 0; r < TotalRows; r++)
        {
            for (int c = 0; c < TotalCols; c++)
            {
                Knodes2D[r, c].gameObject.GetComponent<Renderer>().enabled = argONOFF;
            }
            ExplicitNodeAlpha.gameObject.GetComponent<Renderer>().enabled = argONOFF;
            ExplicitNodeBravo.gameObject.GetComponent<Renderer>().enabled = argONOFF;
            ExplicitRootNode.gameObject.GetComponent<Renderer>().enabled = argONOFF;

            for (int c = 0; c < TotalCols; c++)
            {
                UpstairsNodes[c].gameObject.GetComponent<Renderer>().enabled = argONOFF;
            }

            for (int f0 = 0; f0 < FlyNodes0.Length; f0++)
            {
                FlyNodes0[f0].gameObject.GetComponent<Renderer>().enabled = argONOFF;
            }

            for (int f1 = 0; f1 < FlyNodes1.Length; f1++)
            {
                FlyNodes1[f1].gameObject.GetComponent<Renderer>().enabled = argONOFF;
            }
        }

    }

    //void FloorMeshRenderers(bool argONOFF)
    //{ 
    //      FloorMeshRenderersOcclude();

    //   // LandMesh.GetComponent<Renderer>().enabled = argONOFF;
    //   // UnderLandMesh.GetComponent<Renderer>().enabled = argONOFF;
    //}

    //void FloorMeshRenderersOcclude( )
    //{
    //    //Debug.Log("mesh controller does this now");
    //   // LandMesh.GetComponent<Renderer>().material = OcclusionMAt;
    //   // UnderLandMesh.GetComponent<Renderer>().material = OcclusionMAt;
    //}

    #endregion

    #region GetKnodes
    //from KngKnodeSpawnManager
    public KNode GetNode(int argNodeID)
    {
        if (argNodeID >= KnodeDICT.Count) { argNodeID = 0; }
        //   Debug.Log("knode " + argNodeID);
        return KnodeDICT[argNodeID];
    }


    public List<KNode> GetAll_BackRowSpawns()
    {
        List<KNode> ListOut = new List<KNode>();
        for (int c = 0; c < TotalCols; c++)
        {
            ListOut.Add(KnodeDICT[c]);
        }
        return ListOut;
    }

    public List<KNode> GetAll_RowSSpawns(int argMAXRowIndex)
    {
        List<KNode> ListOut = new List<KNode>();
        for (int r = 0; r < argMAXRowIndex; r++)
        {
            for (int c = 0; c < TotalCols; c++)
            {
                ListOut.Add(Knodes2D[r, c]);
            }
        }
        return ListOut;
    }

    public List<KNode> GetAll_LandNodes()
    {
        List<KNode> ListOut = new List<KNode>();
        for (int r = 0; r < TotalRows; r++)
        {
            for (int c = 0; c < TotalCols; c++)
            {
                ListOut.Add(Knodes2D[r, c]);
            }
        }


        //ListOut.AddRange(UpstairsNodes);
        //ListOut.AddRange(FlyNodes0);
        //ListOut.AddRange(FlyNodes1);
        //ListOut.AddRange(TunnelLeft);
        //ListOut.AddRange(TunnelRight);
        return ListOut;
    }


    public List<KNode> GetAllFreeKnodes()
    {
        List<KNode> ListOut = new List<KNode>();
        for (int r = 0; r < TotalRows; r++)
        {
            for (int c = 0; c < TotalCols; c++)
            {
                if (Knodes2D[r, c].IsFree)
                    ListOut.Add(Knodes2D[r, c]);
            }
        }
        return ListOut;
    }
    #endregion

    // 0 is the clossest 
    // 1 is the only other implemented
    public void PostionTunnels(MidNearNone argDisToPlayer)
    {
        if (argDisToPlayer == MidNearNone.NEAR)
        {
            UnderLandMesh.transform.localPosition = new Vector3(0f, 8.89f, 0f);
        }
        else if (argDisToPlayer == MidNearNone.MID)
        {
            UnderLandMesh.transform.localPosition = new Vector3(0f, 0.0f, 0f);
        }
        else
        {
            UnderLandMesh.transform.localPosition = new Vector3(0f, 0f, 0f);
        }


    }

    public void FreeAllNodes()
    {
        foreach (KNode kn in GetAll_LandNodes())
        {
            kn.IsFree = true;
        }
    }

    public void TogglePointsMeshes(bool argOnOff)
    {
        //  Debug.Log("toogled " + argOnOff.ToString());
        if (ExplicitRootNode.IsFinal) { ExplicitRootNode.SetColor(FinalColorOrange); }
        ExplicitRootNode.gameObject.GetComponent<MeshRenderer>().enabled = argOnOff;

        if (ExplicitNodeAlpha.IsFinal) { ExplicitNodeAlpha.SetColor(FinalColorOrange); }
        ExplicitNodeAlpha.gameObject.GetComponent<MeshRenderer>().enabled = argOnOff;

        if (ExplicitNodeBravo.IsFinal) { ExplicitNodeBravo.SetColor(FinalColorOrange); }
        ExplicitNodeBravo.gameObject.GetComponent<MeshRenderer>().enabled = argOnOff;
        foreach (KNode kn in UpstairsNodes)
        {
            if (kn.IsFinal) { kn.SetColor(FinalColorOrange); }
            kn.gameObject.GetComponent<MeshRenderer>().enabled = argOnOff;
        }
        foreach (KNode kn in FlyNodes0)
        {
            if (kn.IsFinal) { kn.SetColor(FinalColorOrange); }
            kn.gameObject.GetComponent<MeshRenderer>().enabled = argOnOff;
        }
        foreach (KNode kn in FlyNodes1)
        {
            if (kn.IsFinal) { kn.SetColor(FinalColorOrange); }
            kn.gameObject.GetComponent<MeshRenderer>().enabled = argOnOff;
        }
        foreach (KNode kn in Knodes2D)
        {
            if (kn.IsFinal) { kn.SetColor(FinalColorOrange); }
            kn.gameObject.GetComponent<MeshRenderer>().enabled = argOnOff;
            TextMesh tm = kn.gameObject.transform.GetChild(0).gameObject.GetComponent<TextMesh>();
            if (tm != null) { tm.gameObject.SetActive(argOnOff); }
        }
    }
}
