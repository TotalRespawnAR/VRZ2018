using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointsStructWtf
{
    //   name_Row_Col    farthest row from player being row0  
    //
    //    SpawnPoint SP
    //    GravePoint GP
    //    FlyPoint   FP
    //    Waypoint   WP
    //    HotSpot    HP
    //
    // 
    //
    //       |       |       |FP_0_2 |FP_0_3 |       |       |
    //       |SP_0_0 |SP_0_1 |SP_0_2 |SP_0_3 |SP_0_4 |SP_0_5 |
    //       |       |       |       |       |       |       |
    //  _____|WP_0_0 |WP_0_1 |WP_0_2 |WP_0_3 |WP_0_4 |WP_0_5 |________
    //
    //                              BW_0_0_0

    //     SP_1_0     GP_1_1                  GP_1_4      SP_1_5
    // ______                                                _________
    //       |WP_1_0 |WP_1_1 |WP_1_2|X|WP_1_3 |WP_1_4 |WP_1_5|     <---------X is the location of the physical Room Column
    //       |                                               | 
    //       |                                               | 
    //       |                                               | 
    //       |                                               | 
    //       |sp2N/A |sp2N/A |sp2N/A |sp2N/A |sp2N/A |sp2N/A |
    //       |        GP_2_1  GP_2_2  GP_2_3  GP_2_4         |   
    //       |                                               |
    //  _____|WP_2_0 |WP_2_1 |WP_2_2 |WP_2_3 |WP_2_4 |WP_2_5 |_________
    //
    //
    //
    //     SP_3_0     GP_3_1  GP_3_2  GP_3_3  GP_3_4      SP_3_5
    // ______                                                _________
    //       |WP_3_0 |WP_3_1 |WP_3_2 |WP_3_3 |WP_3_4 |WP_3_5 |     
    //       |                                               |     
    //       |                                               |     
    //       |               |HP_4_0 |HP_4_1 |               |   <--- hotspots o and 1 do not follow the patern (2d arraw indexing) therow is 4 , but the cols just represent player alpfa and bravo
    //
    //
    //here i just want to use a 2d matrix to store and access the spawn points;
    //the anchors are named <anchorshortname>_Id_row_column
    //eg:  GravePoint row 2 column 3  = GP_someID_2_3
    // this will make it very easy to use indecies to create a path for any spawnpoint using a simple formula:
    // spawnpoint SP_1_1  can generate simple path using index values . very easy to modify and experiemnt with different logic 
    //  PATH = SP_!_1 WP_1__1  WP 2_1 WP_3_1 It s a straight lin on column 1 
    // PAH2 = SP_!_1 WP_1__1  WP 2_0 WP_3_2 It s a ZigZag line  ...
    //
    //  
     const int MaxRows = 4;
    public int GetMaxRow() {
        return MaxRows;
    }

     const int MaxCols = 6;
    public int GetMaxCol() {
        return MaxCols;
    }
    GameObject[,] SP_2D ;
    GameObject[,] GP_2D ;
    GameObject[,] WP_2D;

    GameObject[] FP_1D;
    GameObject[] HP_1D;
    GameObject[] SB_1D;
    GameObject Arena_Obj;
    GameObject WB_Obj;
    public WayPointsStructWtf() {
        SP_2D = new GameObject[MaxRows, MaxCols];
        GP_2D = new GameObject[MaxRows, MaxCols];
        WP_2D = new GameObject[MaxRows, MaxCols];
        FP_1D = new GameObject[2];
        HP_1D = new GameObject[2];
        SB_1D = new GameObject[2];
    }

    /// <summary>
    ///  has same indexing confiuraion as 2d arrays [R,C]
    /// </summary>
    /// <param name="argGO"></param>
    /// <param name="argRow"></param>  
    /// <param name="argCol"></param>
    /// 

    // 2D
    public void AddSpawnPoint(GameObject argGO, int argRow, int argCol)  { SP_2D[argRow, argCol] = argGO; SP_2D[argRow, argCol].AddComponent<KngSpawnPointsCTRL>(); SP_2D[argRow, argCol].GetComponent<KngSpawnPointsCTRL>().ColumnNumber=argCol; }//delete colnumber not needed
    public void AddGravePoint(GameObject argGO, int argRow, int argCol) {  GP_2D[argRow, argCol] = argGO; GP_2D[argRow, argCol].AddComponent<KngSpawnPointsCTRL>(); GP_2D[argRow, argCol].GetComponent<KngSpawnPointsCTRL>().ColumnNumber = argCol; }
    public void AddWayPoint(GameObject argGO, int argRow, int argCol) {    WP_2D[argRow, argCol] = argGO; }
    // 1D
    public void AddFlyPoint(GameObject argGO, int argIndex) {  FP_1D[argIndex] = argGO; }
    public void AddHotSpot(GameObject argGO, int argIndex) { HP_1D[argIndex] = argGO; }
    public void AddScoreBoards(GameObject argGO, int argIndex) { SB_1D[argIndex] = argGO; }
    // obj
    public void AddWallPlace(GameObject argGO) { WB_Obj = argGO; }
    public void AddArenaPlace(GameObject argGO) { Arena_Obj = argGO; }
    //*******************************************************************************************************

    public KngNode ClosesNodeToSpawnColumn(int argRow,int argSpawnCol, bool isgraver) {
        int OffsetRow = argRow;
        if (isgraver) OffsetRow++;

        return WP_2D[OffsetRow, argSpawnCol].GetComponent<KngNode>();
    }

    //2D
    public GameObject GetSpawnPoint( int argRow, int argCol) {
        if (argRow >= MaxRows) { argRow = MaxRows - 1; }
        if (argRow <= 0) { argRow = 0; }
        if (argCol >= MaxCols) { argCol = MaxCols - 1; }
        if (argCol <= 0) { argCol = 0; }
        return SP_2D[argRow, argCol] ; }
    public GameObject GetGravePoint( int argRow, int argCol) {
        if (argRow == 2) argRow = 3;
        if (argRow >= MaxRows) { argRow = MaxRows - 1; }
        if (argRow <= 0) { argRow = 0; }
        if (argCol >= MaxCols) { argCol = MaxCols - 1; }
        if (argCol <= 0) { argCol = 0; }
        return GP_2D[argRow, argCol]; }
    public GameObject GetWayPoint( int argRow, int argCol) { return  WP_2D[argRow, argCol] ; }
    public GameObject GetWayPoint(Vector2 argv2) {
        if (argv2.x >= MaxRows) { argv2.x = MaxRows - 1; }
        if (argv2.x <= 0) { argv2.x = 0; }
        if (argv2.y >= MaxCols) { argv2.y = MaxCols - 1; }
        if (argv2.y <= 0) { argv2.y = 0; }

        return WP_2D[(int)argv2.x, (int)argv2.y]; }
    //1D
    public GameObject GetFlyPoint( int argIndex) {return FP_1D[argIndex] ; }
    public GameObject GetHotSpot(int argIndex) { return HP_1D[argIndex] ; }
    public GameObject GetScoreBoards( int argIndex) { return SB_1D[argIndex]; }
    //obj
    public GameObject GetWallPlace() { return WB_Obj; }
    public GameObject GetArenaPlace() { return Arena_Obj ; }
    //*******************************************************************************************************

    public GameObject[,] GetWayPoints( ) {
        return WP_2D;
    }

    public List<Transform> GetSpawnPoitGroupByRow(int argRow)
    {
    
        List<Transform> OutSpawnGroup = new List<Transform>();
        if (argRow == 0)
        {
            for (int c = 0; c < MaxCols; c++)
            {

                OutSpawnGroup.Add(SP_2D[0,c].transform);
            }
        }else
        if (argRow == 1)
        {
            

                OutSpawnGroup.Add(SP_2D[1, 0].transform);
                OutSpawnGroup.Add(SP_2D[1, 5].transform);

        }
        else
        
        {
            OutSpawnGroup.Add(SP_2D[3, 0].transform);
            OutSpawnGroup.Add(SP_2D[3, 5].transform);
        }
         

            return OutSpawnGroup;
    }
    public List<Transform> GetGravePoitGroupByRow(int argRow)
    {
        //GP_1_1 GP_1_4

        List<Transform> OutSpawnGroup = new List<Transform>();
        if (argRow == 1)
        {
            OutSpawnGroup.Add(GP_2D[1, 1].transform);
            OutSpawnGroup.Add(GP_2D[1, 4].transform);
        }
        else
        if (argRow == 2)
        {

           OutSpawnGroup.Add(GP_2D[2, 1].transform);
            OutSpawnGroup.Add(GP_2D[2, 2].transform);

            OutSpawnGroup.Add(GP_2D[2, 3].transform);

            OutSpawnGroup.Add(GP_2D[2, 4].transform);



        }
        else

        {
            OutSpawnGroup.Add(GP_2D[3, 1].transform);
            OutSpawnGroup.Add(GP_2D[3, 2].transform);
            OutSpawnGroup.Add(GP_2D[3, 3].transform);
            OutSpawnGroup.Add(GP_2D[3, 4].transform);
        }
        return OutSpawnGroup;
    }


    public List<Vector3> GetweightedPath(int argFromRow, int argFromCol, int Weight=0) {
        if (argFromRow >= MaxRows) { argFromRow = MaxRows - 1; }
        if (argFromRow <= 0) { argFromRow =  0; }
        if (argFromCol >= MaxCols) { argFromCol = MaxCols - 1; }
        if (argFromCol <= 0) { argFromCol =0; }

        int cashedColumn = argFromCol;
        List<Vector3> OutPath = new List<Vector3>();
        for (int r = argFromRow; r < MaxRows; r++) {
            OutPath.Add(WP_2D[r, cashedColumn].transform.position);
        }
        OutPath.Add(HP_1D[0].transform.position); //hotspot o 
        return OutPath;
    }


    public void SetSpawPaths() {

        SP_2D[0, 0].GetComponent<KngSpawnPointsCTRL>().SetPathsFromHere(SP00Paths(), SP_2D[0, 0].name);

        SP_2D[0, 1].GetComponent<KngSpawnPointsCTRL>().SetPathsFromHere(SP01Paths(), SP_2D[0, 1].name);
        SP_2D[0, 2].GetComponent<KngSpawnPointsCTRL>().SetPathsFromHere(SP02Paths(), SP_2D[0, 2].name);
        SP_2D[0, 3].GetComponent<KngSpawnPointsCTRL>().SetPathsFromHere(SP03Paths(), SP_2D[0, 3].name);
        SP_2D[0, 4].GetComponent<KngSpawnPointsCTRL>().SetPathsFromHere(SP04Paths(), SP_2D[0, 4].name);
        SP_2D[0, 5].GetComponent<KngSpawnPointsCTRL>().SetPathsFromHere(SP04Paths(), SP_2D[0, 5].name);
        SP_2D[1, 0].GetComponent<KngSpawnPointsCTRL>().SetPathsFromHere(SP10Paths(), SP_2D[1, 0].name);
        SP_2D[1, 5].GetComponent<KngSpawnPointsCTRL>().SetPathsFromHere(SP15Paths(), SP_2D[1, 5].name);
        SP_2D[3, 0].GetComponent<KngSpawnPointsCTRL>().SetPathsFromHere(SP30Paths(), SP_2D[3, 0].name);
        SP_2D[3, 5].GetComponent<KngSpawnPointsCTRL>().SetPathsFromHere(SP35Paths(), SP_2D[3, 5].name);


        GP_2D[1, 1].GetComponent<KngSpawnPointsCTRL>().SetPathsFromHere(GP11Paths(), GP_2D[1, 1].name);
        GP_2D[1, 4].GetComponent<KngSpawnPointsCTRL>().SetPathsFromHere(GP14Paths(), GP_2D[1, 4].name);
        GP_2D[2, 1].GetComponent<KngSpawnPointsCTRL>().SetPathsFromHere(GP21Paths(), GP_2D[2, 1].name);
        GP_2D[2, 2].GetComponent<KngSpawnPointsCTRL>().SetPathsFromHere(GP22Paths(), GP_2D[2, 2].name);
        GP_2D[2, 3].GetComponent<KngSpawnPointsCTRL>().SetPathsFromHere(GP23Paths(), GP_2D[2, 3].name);
        GP_2D[2, 4].GetComponent<KngSpawnPointsCTRL>().SetPathsFromHere(GP24Paths(), GP_2D[2, 4].name);
        GP_2D[3, 1].GetComponent<KngSpawnPointsCTRL>().SetPathsFromHere(GP31Paths(), GP_2D[3, 1].name);
        GP_2D[3, 2].GetComponent<KngSpawnPointsCTRL>().SetPathsFromHere(GP32Paths(), GP_2D[3, 2].name);
        GP_2D[3, 3].GetComponent<KngSpawnPointsCTRL>().SetPathsFromHere(GP33Paths(), GP_2D[3, 3].name);
        GP_2D[3, 4].GetComponent<KngSpawnPointsCTRL>().SetPathsFromHere(GP34Paths(), GP_2D[3, 4].name);

    }

    public void BuildTheNextNodes() {
        AddKngNodeScript();

        for (int row = 0; row < MaxRows-1; row++) //last row is the endpoint , after that , the next node is either hotspot A or hotspotB
        {
            for (int col = 0; col < MaxCols; col++)
            {
  
               
                KngNode kngForNEighborsetup = WP_2D[row, col].GetComponent<KngNode>();
                for (int ColnextRowDown = 0; ColnextRowDown < MaxCols; ColnextRowDown++)
                {
                    //each of the nodes in the next row
                   // Debug.Log(" " + row + " " + col + " procexxing");
                   // Debug.Log(" " + row+1 + " " + col + " try add");
                    kngForNEighborsetup.NextNodes.Add(WP_2D[row + 1, ColnextRowDown].GetComponent<KngNode>());
                   
                }
                kngForNEighborsetup.OrderNextNodsAround(col);
            }
        }


        HP_1D[0].AddComponent<KngNode>();
        HP_1D[0].GetComponent<KngNode>().Row = 4;
        HP_1D[0].GetComponent<KngNode>().Col = 1;
        HP_1D[0].GetComponent<KngNode>().pos = HP_1D[0].transform.position;
        HP_1D[1].AddComponent<KngNode>();
        HP_1D[1].GetComponent<KngNode>().Row = 4;
        HP_1D[1].GetComponent<KngNode>().Col = 3;
        HP_1D[1].GetComponent<KngNode>().pos = HP_1D[1].transform.position;

        for (int LastRowc = 0; LastRowc < MaxCols; LastRowc++)
        {
            if (WP_2D[3, LastRowc] != null)
            {
                if (GameSettings.Instance.GameMode == ARZGameModes.GameLeft_Alpha)
                    WP_2D[3, LastRowc].GetComponent<KngNode>().NextNodes.Add(HP_1D[0].GetComponent<KngNode>());
                else
                {
                    WP_2D[3, LastRowc].GetComponent<KngNode>().NextNodes.Add(HP_1D[1].GetComponent<KngNode>());
                }
            }
        }



    }

    void AddKngNodeScript() {
        for (int c = 0; c < MaxCols; c++) {
            for (int r = 0; r < MaxRows; r++) {
                WP_2D[r,c].AddComponent<KngNode>();
                WP_2D[r, c].GetComponent<KngNode>().Row = r;
                WP_2D[r,c].GetComponent<KngNode>().Col = c;
            }
        }

    }

    List<List<Vector3>> SP00Paths()
    {
        List<List<Vector3>> OutPaths = new List<List<Vector3>>();
        List<Vector3> p1 = new List<Vector3>();
        p1.Add(GetSpawnPoint(0, 0).transform.position);
        p1.Add(GetWayPoint(0, 0).transform.position);
        p1.Add(GetWayPoint(1, 0).transform.position);
        p1.Add(GetWayPoint(2, 0).transform.position);
        p1.Add(GetWayPoint(3, 2).transform.position);



        List<Vector3> p2 = new List<Vector3>();
        p2.Add(GetSpawnPoint(0, 0).transform.position);
        p2.Add(GetWayPoint(0, 0).transform.position);
        p2.Add(GetWayPoint(1, 1).transform.position);
        p2.Add(GetWayPoint(2, 1).transform.position);
        p2.Add(GetWayPoint(3, 2).transform.position);

        List<Vector3> p3 = new List<Vector3>();
        p3.Add(GetSpawnPoint(0, 0).transform.position);

        p3.Add(GetWayPoint(0, 0).transform.position);
        p3.Add(GetWayPoint(1, 0).transform.position);
        p3.Add(GetWayPoint(2, 2).transform.position);
        p3.Add(GetWayPoint(3, 2).transform.position);
        OutPaths.Add(p1);
        OutPaths.Add(p2);
        OutPaths.Add(p3);

        foreach (List<Vector3> vl in OutPaths)
        {
            if (GameSettings.Instance._gameMode == ARZGameModes.GameLeft_Alpha)
            {
                vl.Add(HP_1D[0].transform.position);
            }
            else
                vl.Add(HP_1D[1].transform.position);
        }

        return OutPaths;
    }

    List<List<Vector3>> SP01Paths()
    {
        List<List<Vector3>> OutPaths = new List<List<Vector3>>();
        List<Vector3> p1 = new List<Vector3>();
        p1.Add(GetSpawnPoint(0, 1).transform.position);

        p1.Add(GetWayPoint(0, 1).transform.position);
        p1.Add(GetWayPoint(1, 1).transform.position);
        p1.Add(GetWayPoint(2, 1).transform.position);
        p1.Add(GetWayPoint(3, 2).transform.position);

        List<Vector3> p2 = new List<Vector3>();
        p2.Add(GetSpawnPoint(0, 1).transform.position);
        p2.Add(GetWayPoint(0, 1).transform.position);
        p2.Add(GetWayPoint(1, 2).transform.position);
        p2.Add(GetWayPoint(2, 1).transform.position);
        p2.Add(GetWayPoint(3, 2).transform.position);

        List<Vector3> p3 = new List<Vector3>();
        p3.Add(GetSpawnPoint(0, 1).transform.position);
        p3.Add(GetWayPoint(0, 1).transform.position);
        p3.Add(GetWayPoint(1, 2).transform.position);
        p3.Add(GetWayPoint(2, 1).transform.position);
        p3.Add(GetWayPoint(3, 1).transform.position);
        OutPaths.Add(p1);
        OutPaths.Add(p2);
        OutPaths.Add(p3);
        foreach (List<Vector3> vl in OutPaths)
        {
            if (GameSettings.Instance._gameMode == ARZGameModes.GameLeft_Alpha)
            {
                vl.Add(HP_1D[0].transform.position);
            }
            else
                vl.Add(HP_1D[1].transform.position);
        }
        return OutPaths;
    }

    List<List<Vector3>> SP02Paths()
    {
        List<List<Vector3>> OutPaths = new List<List<Vector3>>();
        List<Vector3> p1 = new List<Vector3>();
        p1.Add(GetSpawnPoint(0, 2).transform.position);
        p1.Add(GetWayPoint(0, 2).transform.position);
        p1.Add(GetWayPoint(1, 2).transform.position);
        p1.Add(GetWayPoint(2, 2).transform.position);
        p1.Add(GetWayPoint(3, 2).transform.position);

        List<Vector3> p2 = new List<Vector3>();
        p2.Add(GetSpawnPoint(0, 2).transform.position);
        p2.Add(GetWayPoint(0, 2).transform.position);
        p2.Add(GetWayPoint(1, 3).transform.position);
        p2.Add(GetWayPoint(2, 2).transform.position);
        p2.Add(GetWayPoint(3, 2).transform.position);

        List<Vector3> p3 = new List<Vector3>();
        p3.Add(GetSpawnPoint(0, 2).transform.position);
        p3.Add(GetWayPoint(0, 2).transform.position);
        p3.Add(GetWayPoint(1, 2).transform.position);
        p3.Add(GetWayPoint(2, 3).transform.position);
        p3.Add(GetWayPoint(3, 2).transform.position);
        OutPaths.Add(p1);
        OutPaths.Add(p2);
        OutPaths.Add(p3);
        foreach (List<Vector3> vl in OutPaths)
        {
            if (GameSettings.Instance._gameMode == ARZGameModes.GameLeft_Alpha)
            {
                vl.Add(HP_1D[0].transform.position);
            }
            else
                vl.Add(HP_1D[1].transform.position);
        }
        return OutPaths;
    }


    List<List<Vector3>> SP03Paths()
    {
        List<List<Vector3>> OutPaths = new List<List<Vector3>>();
        List<Vector3> p1 = new List<Vector3>();
        p1.Add(GetSpawnPoint(0, 3).transform.position);
        p1.Add(GetWayPoint(0, 3).transform.position);
        p1.Add(GetWayPoint(1, 3).transform.position);
        p1.Add(GetWayPoint(2, 3).transform.position);
        p1.Add(GetWayPoint(3, 3).transform.position);

        List<Vector3> p2 = new List<Vector3>();
        p2.Add(GetSpawnPoint(0, 3).transform.position);

        p2.Add(GetWayPoint(0, 3).transform.position);
        p2.Add(GetWayPoint(1, 3).transform.position);
        p2.Add(GetWayPoint(2, 2).transform.position);
        p2.Add(GetWayPoint(3, 2).transform.position);

        List<Vector3> p3 = new List<Vector3>();
        p3.Add(GetSpawnPoint(0, 3).transform.position);
        p3.Add(GetWayPoint(0, 3).transform.position);
        p3.Add(GetWayPoint(1, 3).transform.position);
        p3.Add(GetWayPoint(2, 2).transform.position);
        p3.Add(GetWayPoint(3, 3).transform.position);
        OutPaths.Add(p1);
        OutPaths.Add(p2);
        OutPaths.Add(p3);
        foreach (List<Vector3> vl in OutPaths)
        {
            if (GameSettings.Instance._gameMode == ARZGameModes.GameLeft_Alpha)
            {
                vl.Add(HP_1D[0].transform.position);
            }
            else
                vl.Add(HP_1D[1].transform.position);
        }
        return OutPaths;
    }


    List<List<Vector3>> SP04Paths()
    {
        List<List<Vector3>> OutPaths = new List<List<Vector3>>();
        List<Vector3> p1 = new List<Vector3>();
        p1.Add(GetSpawnPoint(0, 4).transform.position);

        p1.Add(GetWayPoint(0, 4).transform.position);
        p1.Add(GetWayPoint(1, 4).transform.position);
        p1.Add(GetWayPoint(2, 4).transform.position);
        p1.Add(GetWayPoint(3, 4).transform.position);

        List<Vector3> p2 = new List<Vector3>();
        p2.Add(GetSpawnPoint(0, 4).transform.position);
        p2.Add(GetWayPoint(0, 4).transform.position);
        p2.Add(GetWayPoint(1, 3).transform.position);
        p2.Add(GetWayPoint(2, 2).transform.position);
        p2.Add(GetWayPoint(3, 3).transform.position);

        List<Vector3> p3 = new List<Vector3>();
        p3.Add(GetSpawnPoint(0, 4).transform.position);
        p3.Add(GetWayPoint(0, 4).transform.position);
        p3.Add(GetWayPoint(1, 4).transform.position);
        p3.Add(GetWayPoint(2, 3).transform.position);
        p3.Add(GetWayPoint(3, 2).transform.position);
        OutPaths.Add(p1);
        OutPaths.Add(p2);
        OutPaths.Add(p3);
        foreach (List<Vector3> vl in OutPaths)
        {
            if (GameSettings.Instance._gameMode == ARZGameModes.GameLeft_Alpha)
            {
                vl.Add(HP_1D[0].transform.position);
            }
            else
                vl.Add(HP_1D[1].transform.position);
        }
        return OutPaths;
    }



    List<List<Vector3>> SP05Paths()
    {
        List<List<Vector3>> OutPaths = new List<List<Vector3>>();
        List<Vector3> p1 = new List<Vector3>();
        p1.Add(GetSpawnPoint(0, 5).transform.position);
        p1.Add(GetWayPoint(0, 5).transform.position);

        p1.Add(GetWayPoint(1, 5).transform.position);
        p1.Add(GetWayPoint(2, 5).transform.position);
        p1.Add(GetWayPoint(3, 4).transform.position);

        List<Vector3> p2 = new List<Vector3>();
        p2.Add(GetWayPoint(0, 5).transform.position);
        p2.Add(GetWayPoint(1, 5).transform.position);
        p2.Add(GetWayPoint(2, 4).transform.position);
        p2.Add(GetWayPoint(3, 3).transform.position);

        List<Vector3> p3 = new List<Vector3>();
        p3.Add(GetWayPoint(0, 5).transform.position);
        p3.Add(GetWayPoint(1, 4).transform.position);
        p3.Add(GetWayPoint(2, 5).transform.position);
        p3.Add(GetWayPoint(3, 3).transform.position);
        OutPaths.Add(p1);
        OutPaths.Add(p2);
        OutPaths.Add(p3);
        foreach (List<Vector3> vl in OutPaths)
        {
            if (GameSettings.Instance._gameMode == ARZGameModes.GameLeft_Alpha)
            {
                vl.Add(HP_1D[0].transform.position);
            }
            else
                vl.Add(HP_1D[1].transform.position);
        }
        return OutPaths;
    }

    List<List<Vector3>> SP10Paths()
    {
        List<List<Vector3>> OutPaths = new List<List<Vector3>>();
        List<Vector3> p1 = new List<Vector3>();
        p1.Add(GetSpawnPoint(1, 0).transform.position);

        p1.Add(GetWayPoint(1, 0).transform.position);
        p1.Add(GetWayPoint(2, 0).transform.position);
        p1.Add(GetWayPoint(3, 2).transform.position);

        List<Vector3> p2 = new List<Vector3>();
        p2.Add(GetSpawnPoint(1, 0).transform.position);

        p2.Add(GetWayPoint(1, 0).transform.position);
        p2.Add(GetWayPoint(2, 2).transform.position);
        p2.Add(GetWayPoint(3, 2).transform.position);

        List<Vector3> p3 = new List<Vector3>();
        p3.Add(GetSpawnPoint(1, 0).transform.position);

        p3.Add(GetWayPoint(1, 0).transform.position);
        p3.Add(GetWayPoint(2, 2).transform.position);
        p3.Add(GetWayPoint(3, 3).transform.position);
        OutPaths.Add(p1);
        OutPaths.Add(p2);
        OutPaths.Add(p3);
        foreach (List<Vector3> vl in OutPaths)
        {
            if (GameSettings.Instance._gameMode == ARZGameModes.GameLeft_Alpha)
            {
                vl.Add(HP_1D[0].transform.position);
            }
            else
                vl.Add(HP_1D[1].transform.position);
        }
        return OutPaths;
    }

    List<List<Vector3>> SP15Paths()
    {
        List<List<Vector3>> OutPaths = new List<List<Vector3>>();
        List<Vector3> p1 = new List<Vector3>();
        p1.Add(GetSpawnPoint(1, 5).transform.position);

        p1.Add(GetWayPoint(1, 3).transform.position);
        p1.Add(GetWayPoint(2, 4).transform.position);
        p1.Add(GetWayPoint(3, 3).transform.position);

        List<Vector3> p2 = new List<Vector3>();
        p2.Add(GetSpawnPoint(1, 5).transform.position);

        p2.Add(GetWayPoint(1, 3).transform.position);
        p2.Add(GetWayPoint(2, 3).transform.position);
        p2.Add(GetWayPoint(3, 2).transform.position);

        List<Vector3> p3 = new List<Vector3>();
        p3.Add(GetSpawnPoint(1, 5).transform.position);

        p3.Add(GetWayPoint(1, 3).transform.position);
        p3.Add(GetWayPoint(2, 5).transform.position);
        p3.Add(GetWayPoint(3, 3).transform.position);
        OutPaths.Add(p1);
        OutPaths.Add(p2);
        OutPaths.Add(p3);
        foreach (List<Vector3> vl in OutPaths)
        {
            if (GameSettings.Instance._gameMode == ARZGameModes.GameLeft_Alpha)
            {
                vl.Add(HP_1D[0].transform.position);
            }
            else
                vl.Add(HP_1D[1].transform.position);
        }
        return OutPaths;
    }

    List<List<Vector3>> SP30Paths()
    {
        List<List<Vector3>> OutPaths = new List<List<Vector3>>();
        List<Vector3> p1 = new List<Vector3>();
        p1.Add(GetSpawnPoint(3, 0).transform.position);

        p1.Add(GetWayPoint(3, 0).transform.position);

        List<Vector3> p2 = new List<Vector3>();
        p2.Add(GetSpawnPoint(3, 0).transform.position);

        p2.Add(GetWayPoint(3, 1).transform.position);

        List<Vector3> p3 = new List<Vector3>();
        p3.Add(GetSpawnPoint(3, 0).transform.position);

        p3.Add(GetWayPoint(3, 2).transform.position);
        OutPaths.Add(p1);
        OutPaths.Add(p2);
        OutPaths.Add(p3);
        foreach (List<Vector3> vl in OutPaths)
        {
            if (GameSettings.Instance._gameMode == ARZGameModes.GameLeft_Alpha)
            {
                vl.Add(HP_1D[0].transform.position);
            }
            else
                vl.Add(HP_1D[1].transform.position);
        }
        return OutPaths;
    }

    List<List<Vector3>> SP35Paths()
    {
        List<List<Vector3>> OutPaths = new List<List<Vector3>>();
        List<Vector3> p1 = new List<Vector3>();
        p1.Add(GetSpawnPoint(3, 5).transform.position);

        p1.Add(GetWayPoint(3, 5).transform.position);

        List<Vector3> p2 = new List<Vector3>();
        p2.Add(GetSpawnPoint(3, 5).transform.position);

        p2.Add(GetWayPoint(3, 3).transform.position);

        List<Vector3> p3 = new List<Vector3>();
        p3.Add(GetSpawnPoint(3, 5).transform.position);

        p3.Add(GetWayPoint(3, 2).transform.position);
        OutPaths.Add(p1);
        OutPaths.Add(p2);
        OutPaths.Add(p3);
        foreach (List<Vector3> vl in OutPaths)
        {
            if (GameSettings.Instance._gameMode == ARZGameModes.GameLeft_Alpha)
            {
                vl.Add(HP_1D[0].transform.position);
            }
            else
                vl.Add(HP_1D[1].transform.position);
        }
        return OutPaths;
    }



    List<List<Vector3>> GP11Paths()
    {
        List<List<Vector3>> OutPaths = new List<List<Vector3>>();
        List<Vector3> p1 = new List<Vector3>();
        p1.Add(GetGravePoint(1, 1).transform.position);

        p1.Add(GetWayPoint(1, 1).transform.position);
        p1.Add(GetWayPoint(2, 1).transform.position);
        p1.Add(GetWayPoint(3, 1).transform.position);

        List<Vector3> p2 = new List<Vector3>();
        p2.Add(GetGravePoint(1, 1).transform.position);

        p2.Add(GetWayPoint(1, 1).transform.position);
        p2.Add(GetWayPoint(2, 2).transform.position);
        p2.Add(GetWayPoint(3, 1).transform.position);

        List<Vector3> p3 = new List<Vector3>();
        p3.Add(GetGravePoint(1, 1).transform.position);

        p3.Add(GetWayPoint(1, 2).transform.position);
        p3.Add(GetWayPoint(2, 1).transform.position);
        p3.Add(GetWayPoint(3, 2).transform.position);
        OutPaths.Add(p1);
        OutPaths.Add(p2);
        OutPaths.Add(p3);
        foreach (List<Vector3> vl in OutPaths)
        {
            if (GameSettings.Instance._gameMode == ARZGameModes.GameLeft_Alpha)
            {
                vl.Add(HP_1D[0].transform.position);
            }
            else
                vl.Add(HP_1D[1].transform.position);
        }
        return OutPaths;
    }



    List<List<Vector3>> GP14Paths()
    {
        List<List<Vector3>> OutPaths = new List<List<Vector3>>();
        List<Vector3> p1 = new List<Vector3>();
        p1.Add(GetGravePoint(1, 4).transform.position);

        p1.Add(GetWayPoint(1, 4).transform.position);
        p1.Add(GetWayPoint(2, 4).transform.position);
        p1.Add(GetWayPoint(3, 4).transform.position);

        List<Vector3> p2 = new List<Vector3>();
        p2.Add(GetGravePoint(1, 4).transform.position);

        p2.Add(GetWayPoint(1, 4).transform.position);
        p2.Add(GetWayPoint(2, 4).transform.position);
        p2.Add(GetWayPoint(3, 3).transform.position);

        List<Vector3> p3 = new List<Vector3>();
        p3.Add(GetGravePoint(1, 4).transform.position);

        p3.Add(GetWayPoint(1, 4).transform.position);
        p3.Add(GetWayPoint(2, 3).transform.position);
        p3.Add(GetWayPoint(3, 3).transform.position);
        OutPaths.Add(p1);
        OutPaths.Add(p2);
        OutPaths.Add(p3);
        foreach (List<Vector3> vl in OutPaths)
        {
            if (GameSettings.Instance._gameMode == ARZGameModes.GameLeft_Alpha)
            {
                vl.Add(HP_1D[0].transform.position);
            }
            else
                vl.Add(HP_1D[1].transform.position);
        }
        return OutPaths;
    }
    List<List<Vector3>> GP21Paths()
    {
        List<List<Vector3>> OutPaths = new List<List<Vector3>>();
        List<Vector3> p1 = new List<Vector3>();
        p1.Add(GetGravePoint(2, 1).transform.position);
        //p1.Add(GetWayPoint(2, 1).transform.position);
        p1.Add(GetWayPoint(3, 1).transform.position);

        List<Vector3> p2 = new List<Vector3>();
        p2.Add(GetGravePoint(2, 1).transform.position);
       // p2.Add(GetWayPoint(2, 1).transform.position);
        p2.Add(GetWayPoint(3, 1).transform.position);

        List<Vector3> p3 = new List<Vector3>();
        p3.Add(GetGravePoint(2, 1).transform.position);
       // p3.Add(GetWayPoint(2, 1).transform.position);
        p3.Add(GetWayPoint(3, 1).transform.position);

        OutPaths.Add(p1);
        OutPaths.Add(p2);
        OutPaths.Add(p3);
        foreach (List<Vector3> vl in OutPaths)
        {
            if (GameSettings.Instance._gameMode == ARZGameModes.GameLeft_Alpha)
            {
                vl.Add(HP_1D[0].transform.position);
            }
            else
                vl.Add(HP_1D[1].transform.position);
        }
        return OutPaths;
    }

    List<List<Vector3>> GP22Paths()
    {
        List<List<Vector3>> OutPaths = new List<List<Vector3>>();
        List<Vector3> p1 = new List<Vector3>();
        p1.Add(GetGravePoint(2, 2).transform.position);

      //  p1.Add(GetWayPoint(2, 2).transform.position);
        p1.Add(GetWayPoint(3, 1).transform.position);

        List<Vector3> p2 = new List<Vector3>();
        p2.Add(GetGravePoint(2, 2).transform.position);

      //  p2.Add(GetWayPoint(2, 2).transform.position);
        p2.Add(GetWayPoint(3, 2).transform.position);

        List<Vector3> p3 = new List<Vector3>();
        p3.Add(GetGravePoint(2, 2).transform.position);

      //  p3.Add(GetWayPoint(2, 3).transform.position);
        p3.Add(GetWayPoint(3, 3).transform.position);
        OutPaths.Add(p1);
        OutPaths.Add(p2);
        OutPaths.Add(p3);

        foreach (List<Vector3> vl in OutPaths)
        {
            if (GameSettings.Instance._gameMode == ARZGameModes.GameLeft_Alpha)
            {
                vl.Add(HP_1D[0].transform.position);
            }
            else
                vl.Add(HP_1D[1].transform.position);
        }
        return OutPaths;
    }

    List<List<Vector3>> GP23Paths()
    {
        List<List<Vector3>> OutPaths = new List<List<Vector3>>();
        List<Vector3> p1 = new List<Vector3>();
        p1.Add(GetGravePoint(2, 3).transform.position);

      //  p1.Add(GetWayPoint(2, 3).transform.position);
        p1.Add(GetWayPoint(3, 3).transform.position);

        List<Vector3> p2 = new List<Vector3>();
        p2.Add(GetGravePoint(2, 3).transform.position);

       // p2.Add(GetWayPoint(2, 3).transform.position);
        p2.Add(GetWayPoint(3, 2).transform.position);

        List<Vector3> p3 = new List<Vector3>();
        p3.Add(GetGravePoint(2, 3).transform.position);

       // p3.Add(GetWayPoint(2, 3).transform.position);
        p3.Add(GetWayPoint(3, 3).transform.position);
        OutPaths.Add(p1);
        OutPaths.Add(p2);
        OutPaths.Add(p3);
        foreach (List<Vector3> vl in OutPaths)
        {
            if (GameSettings.Instance._gameMode == ARZGameModes.GameLeft_Alpha)
            {
                vl.Add(HP_1D[0].transform.position);
            }
            else
                vl.Add(HP_1D[1].transform.position);
        }
        return OutPaths;
    }
    List<List<Vector3>> GP24Paths()
    {
        List<List<Vector3>> OutPaths = new List<List<Vector3>>();
        List<Vector3> p1 = new List<Vector3>();

        p1.Add(GetGravePoint(2, 4).transform.position);

       // p1.Add(GetWayPoint(2, 4).transform.position);
        p1.Add(GetWayPoint(3, 4).transform.position);

        List<Vector3> p2 = new List<Vector3>();
        p2.Add(GetGravePoint(2, 4).transform.position);

      //  p2.Add(GetWayPoint(2, 4).transform.position);
        p2.Add(GetWayPoint(3, 3).transform.position);

        List<Vector3> p3 = new List<Vector3>();
        p3.Add(GetGravePoint(2, 4).transform.position);

       // p3.Add(GetWayPoint(2, 4).transform.position);
        p3.Add(GetWayPoint(3, 2).transform.position);
        OutPaths.Add(p1);
        OutPaths.Add(p2);
        OutPaths.Add(p3);

        foreach (List<Vector3> vl in OutPaths)
        {
            if (GameSettings.Instance._gameMode == ARZGameModes.GameLeft_Alpha)
            {
                vl.Add(HP_1D[0].transform.position);
            }
            else
                vl.Add(HP_1D[1].transform.position);
        }
        return OutPaths;
    }
    List<List<Vector3>> GP31Paths()
    {
        List<List<Vector3>> OutPaths = new List<List<Vector3>>();
        List<Vector3> p1 = new List<Vector3>();
        p1.Add(GetGravePoint(3, 1).transform.position);


        p1.Add(GetWayPoint(3, 2).transform.position);

        List<Vector3> p2 = new List<Vector3>();
        p2.Add(GetGravePoint(3, 1).transform.position);
      //  p2.Add(GetWayPoint(3, 2).transform.position);

        List<Vector3> p3 = new List<Vector3>();

        p3.Add(GetGravePoint(3, 1).transform.position);
        p3.Add(GetWayPoint(3, 3).transform.position);
        OutPaths.Add(p1);
        OutPaths.Add(p2);
        OutPaths.Add(p3);
        foreach (List<Vector3> vl in OutPaths)
        {
            if (GameSettings.Instance._gameMode == ARZGameModes.GameLeft_Alpha)
            {
                vl.Add(HP_1D[0].transform.position);
            }
            else
                vl.Add(HP_1D[1].transform.position);
        }
        return OutPaths;
    }
    List<List<Vector3>> GP32Paths()
    {
        List<List<Vector3>> OutPaths = new List<List<Vector3>>();
        List<Vector3> p1 = new List<Vector3>();
        p1.Add(GetGravePoint(3, 2).transform.position);

        p1.Add(GetWayPoint(3, 1).transform.position);

        List<Vector3> p2 = new List<Vector3>();
        p2.Add(GetGravePoint(3, 2).transform.position);
        p2.Add(GetWayPoint(3, 2).transform.position);

        List<Vector3> p3 = new List<Vector3>();
        p3.Add(GetGravePoint(3, 2).transform.position);
        p3.Add(GetWayPoint(3, 3).transform.position);
        OutPaths.Add(p1);
        OutPaths.Add(p2);
        OutPaths.Add(p3);
        foreach (List<Vector3> vl in OutPaths)
        {
            if (GameSettings.Instance._gameMode == ARZGameModes.GameLeft_Alpha)
            {
                vl.Add(HP_1D[0].transform.position);
            }
            else
                vl.Add(HP_1D[1].transform.position);
        }
        return OutPaths;
    }

    List<List<Vector3>> GP33Paths()
    {
        List<List<Vector3>> OutPaths = new List<List<Vector3>>();
        List<Vector3> p1 = new List<Vector3>();
        p1.Add(GetGravePoint(3, 3).transform.position);

        p1.Add(GetWayPoint(3, 3).transform.position);

        List<Vector3> p2 = new List<Vector3>();
        p2.Add(GetGravePoint(3, 3).transform.position);
        p2.Add(GetWayPoint(3, 2).transform.position);

        List<Vector3> p3 = new List<Vector3>();

        p3.Add(GetGravePoint(3, 3).transform.position);
        p3.Add(GetWayPoint(3, 4).transform.position);
        OutPaths.Add(p1);
        OutPaths.Add(p2);
        OutPaths.Add(p3);
        foreach (List<Vector3> vl in OutPaths)
        {
            if (GameSettings.Instance._gameMode == ARZGameModes.GameLeft_Alpha)
            {
                vl.Add(HP_1D[0].transform.position);
            }
            else
                vl.Add(HP_1D[1].transform.position);
        }
        return OutPaths;
    }

    List<List<Vector3>> GP34Paths()
    {
        List<List<Vector3>> OutPaths = new List<List<Vector3>>();
        List<Vector3> p1 = new List<Vector3>();
        p1.Add(GetGravePoint(3, 4).transform.position);

        p1.Add(GetWayPoint(3, 4).transform.position);

        List<Vector3> p2 = new List<Vector3>();
        p2.Add(GetGravePoint(3, 4).transform.position);
        p2.Add(GetWayPoint(3, 3).transform.position);

        List<Vector3> p3 = new List<Vector3>();

        p3.Add(GetGravePoint(3, 4).transform.position);
        p3.Add(GetWayPoint(3, 4).transform.position);
        OutPaths.Add(p1);
        OutPaths.Add(p2);
        OutPaths.Add(p3);
        foreach (List<Vector3> vl in OutPaths)
        {
            if (GameSettings.Instance._gameMode == ARZGameModes.GameLeft_Alpha)
            {
                vl.Add(HP_1D[0].transform.position);
            }
            else
                vl.Add(HP_1D[1].transform.position);
        }
        return OutPaths;
    }

    public void InitAllImmediates_andHotspotsNodes() {
        for (int r = 0; r < MaxRows; r++) {
            for (int c = 0; c < MaxCols; c++) {
                if (SP_2D[r, c] != null) {
                    if (r==1 && c == 0) { //spetial case , the top most spawns are used for special zombies . s
                        SP_2D[r, c].GetComponent<KngSpawnPointsCTRL>().ImmediatNextNode = WP_2D[0, 1].GetComponent<KngNode>();
                    }else
                    if (r == 3 && c == 0)
                    {  
                        SP_2D[r, c].GetComponent<KngSpawnPointsCTRL>().ImmediatNextNode = WP_2D[0, 2].GetComponent<KngNode>();
                    }else
                    if (r == 3 && c == 5)
                    {
                        SP_2D[r, c].GetComponent<KngSpawnPointsCTRL>().ImmediatNextNode = WP_2D[0, 3].GetComponent<KngNode>();
                    }else
                    if (r == 1 && c == 5)
                    {
                        SP_2D[r, c].GetComponent<KngSpawnPointsCTRL>().ImmediatNextNode = WP_2D[0, 4].GetComponent<KngNode>();
                    }
                    else //the rest are standard , if keep you spawn column and go down , the first row is never crated
                    {
                        SP_2D[r, c].GetComponent<KngSpawnPointsCTRL>().ImmediatNextNode = WP_2D[0, c].GetComponent<KngNode>();
                    }
                  
                }
            }
        }
   

            GP_2D[1,1].GetComponent<KngSpawnPointsCTRL>().ImmediatNextNode = FindOpenNodeOnNextRow(1, 1);
            GP_2D[1, 4].GetComponent<KngSpawnPointsCTRL>().ImmediatNextNode = FindOpenNodeOnNextRow(1, 4);
        GP_2D[2, 1].GetComponent<KngSpawnPointsCTRL>().ImmediatNextNode = FindOpenNodeOnNextRow(2, 1);
        GP_2D[2, 2].GetComponent<KngSpawnPointsCTRL>().ImmediatNextNode = FindOpenNodeOnNextRow(2, 2);
        GP_2D[2, 3].GetComponent<KngSpawnPointsCTRL>().ImmediatNextNode = FindOpenNodeOnNextRow(2, 3);
        GP_2D[2, 4].GetComponent<KngSpawnPointsCTRL>().ImmediatNextNode = FindOpenNodeOnNextRow(2, 4);
        GP_2D[3, 1].GetComponent<KngSpawnPointsCTRL>().ImmediatNextNode = FindOpenNodeOnNextRow(3, 1);
        GP_2D[3, 2].GetComponent<KngSpawnPointsCTRL>().ImmediatNextNode = FindOpenNodeOnNextRow(3, 2);
        GP_2D[3, 3].GetComponent<KngSpawnPointsCTRL>().ImmediatNextNode = FindOpenNodeOnNextRow(3, 3);
        GP_2D[3, 4].GetComponent<KngSpawnPointsCTRL>().ImmediatNextNode = FindOpenNodeOnNextRow(3, 4);

    }

    KngNode FindOpenNodeOnNextRow(int myRow, int mycol) {
        if (WP_2D[myRow , mycol].GetComponent<KngNode>().IsOpen)
        {
            return WP_2D[myRow, mycol].GetComponent<KngNode>();
        }
        else
         {
            for (int c = 0; c < MaxCols; c++)
            {
                if(WP_2D[myRow , c].GetComponent<KngNode>().IsOpen)
                {
                    return WP_2D[myRow , c].GetComponent<KngNode>();
                 }
            }
        }
        return WP_2D[myRow , mycol].GetComponent<KngNode>();





    }

    //public List<Vector3> GetweightedPath_1(int argFromRow, int argFromCol, int Desinanion, int Weight = 0)
    //{
    //    if (argFromRow >= MaxRows) { argFromRow = MaxRows - 1; }
    //    if (argFromRow <= 0) { argFromRow = 0; }
    //    if (argFromCol >= MaxCols) { argFromCol = MaxCols - 1; }
    //    if (argFromCol <= 0) { argFromCol = 0; }

    //    int cashedColumn = argFromCol;
    //    List<Vector3> OutPath = new List<Vector3>();


    //        OutPath.Add(WP_2D[argFromRow, argFromRow].transform.position);
    //    OutPath.Add(WP_2D[1, 1].transform.position);
    //    OutPath.Add(WP_2D[1, 5].transform.position);
    //    OutPath.Add(WP_2D[2, 4].transform.position);
    //    OutPath.Add(WP_2D[3, 1].transform.position);
    //    OutPath.Add(WP_2D[3, 0].transform.position);
    //    OutPath.Add(HP_1D[Desinanion].transform.position);

    //    return OutPath;
    //}

    public void ClearAllRenderes(bool argClear) {
        //for (int r = 0; r < MaxRows; r++) {
        //    for (int c = 0; c < MaxCols; c++) {
        //        if(SP_2D[r,c] != null) {
        //            SP_2D[r, c].GetComponent<Renderer>().enabled = argClear;
        //            SP_2D[r, c].GetComponentInChildren<TextMesh>().gameObject.SetActive(argClear);

        //            GP_2D[r, c].GetComponent<Renderer>().enabled = argClear;
        //            GP_2D[r, c].GetComponentInChildren<TextMesh>().gameObject.SetActive(argClear);

        //            WP_2D[r, c].GetComponent<Renderer>().enabled = argClear;
        //            WP_2D[r, c].GetComponentInChildren<TextMesh>().gameObject.SetActive(argClear);
        //        }
        //    }
        //}
 

        foreach (GameObject go in SP_2D)
        {


            if (go != null)
            {
                if (go.GetComponent<BoxCollider>() != null)
                    go.GetComponent<BoxCollider>().enabled = false;

                ToggleRenderersOnObjectancTextchild(go, argClear);
            }

        }


        foreach (GameObject go in GP_2D)
        {
            if (go != null)
            {
                if (go.GetComponent<BoxCollider>() != null)
                    go.GetComponent<BoxCollider>().enabled = false;
                ToggleRenderersOnObjectancTextchild(go, argClear);
            }
        }
        foreach (GameObject go in WP_2D)
        {
            if (go != null)
            {
                if (go.GetComponent<BoxCollider>() != null)
                    go.GetComponent<BoxCollider>().enabled = false;
                ToggleRenderersOnObjectancTextchild(go, argClear);
            }
        }

        foreach (GameObject go in FP_1D)
        {
            if (go != null)
            {
                if (go.GetComponent<BoxCollider>() != null)
                    go.GetComponent<BoxCollider>().enabled = false;
                ToggleRenderersOnObjectancTextchild(go, argClear);
            }

        }
        foreach (GameObject go in HP_1D)
        {
            if (go != null)
            {
                if (go.GetComponent<BoxCollider>() != null)
                    go.GetComponent<BoxCollider>().enabled = false;
                ToggleRenderersOnObjectancTextchild(go, argClear);
            }

        }
        foreach (GameObject go in SB_1D)
        {
            if (go != null)
            {
                if (go.GetComponent<BoxCollider>() != null)
                    go.GetComponent<BoxCollider>().enabled = false;
                ToggleRenderersOnObjectancTextchild(go, argClear);
            }

        }

        if (Arena_Obj != null)
        {

            ToggleRenderersOnObjectancTextchild(Arena_Obj, argClear);
        }


        if (WB_Obj != null)
        {
            ToggleRenderersOnObjectancTextchild(WB_Obj, argClear);
        }

    }





    void ToggleRenderersOnObjectancTextchild(GameObject argGo, bool argClear)
    {
        argGo.GetComponent<MeshRenderer>().enabled = argClear;
        TextMesh tm = argGo.GetComponentInChildren<TextMesh>();
         if(tm != null)
        argGo.GetComponentInChildren<TextMesh>().gameObject.GetComponent<MeshRenderer>().enabled = argClear;
    }

}
