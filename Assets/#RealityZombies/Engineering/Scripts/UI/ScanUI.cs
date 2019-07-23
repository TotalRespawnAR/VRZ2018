using System.Collections.Generic;
using UnityEngine;

public class ScanUI : MonoBehaviour
{

    RoomLoader MyRoomLoader;
    RoomSaver MyRoomSaver;
    public GameObject ArrowDebugger;

    public Material OcclusionMat;
    public Material OcclusionOptimizedMat;
    public Material SpatialMappoingMat;
    public Material FunkySpatialMappoingMat;
    public Material Spatialmappingtest;
    public Material SpatialUnderdtanding;



    void TurnOn(MeshRenderer argMR)
    {
        argMR.material.color = Color.red;
    }
    void TurnOff(MeshRenderer argMR)
    {
        argMR.material.color = Color.black;
    }

    public MeshRenderer BG_Debug;
    bool BOOLVAL_debugCubesPlaced = false;
    void UpdateColor_BG_Debug(bool argBoolBound_Debug)
    {
        if (argBoolBound_Debug) { TurnOn(BG_Debug); } else { TurnOff(BG_Debug); }
    }
    public void DoToggleValueAndColor_Debug()
    {
        BOOLVAL_debugCubesPlaced = !BOOLVAL_debugCubesPlaced;
        UpdateColor_BG_Debug(BOOLVAL_debugCubesPlaced);

        MyRoomLoader.ToggleRoom();
        if (BOOLVAL_debugCubesPlaced) { PointmeTheMeshesAnchors(); } else { RemovePointers(); }

    }


    public MeshRenderer BG_Occlude;
    bool BOOLVAL_Occlude = false;
    void UpdateColor_BG_Occlude(bool argBoolBound_Occlude)
    {
        if (argBoolBound_Occlude)
        {
            TurnOn(BG_Occlude);
            // SpatialMappingManager.Instance.SurfaceMaterial = OcclusionMat;
        }
        else
        {
            TurnOff(BG_Occlude);
            //  SpatialMappingManager.Instance.SurfaceMaterial = FunkySpatialMappoingMat;
        }
    }
    public void DoToggleValueAndColor_Occlude()
    {
        BOOLVAL_Occlude = !BOOLVAL_Occlude;
        UpdateColor_BG_Occlude(BOOLVAL_Occlude);


    }

    void Start()
    {
        MyRoomLoader = gameObject.GetComponent<RoomLoader>();
        Debug.Assert(MyRoomLoader != null, "MyRoomLoader cannot be null.");
        MyRoomSaver = gameObject.GetComponent<RoomSaver>();
        Debug.Assert(MyRoomSaver != null, "MyRoomSaver cannot be null.");

        UpdateColor_BG_Debug(BOOLVAL_debugCubesPlaced);
        UpdateColor_BG_Occlude(BOOLVAL_Occlude);
    }


    public void Observer_Start()
    {
        // SpatialMappingManager.Instance.StartObserver();
    }

    public void Observer_Stop()
    {
        // SpatialMappingManager.Instance.StopObserver();
    }

    public void Observer_Clean()
    {
        //   SpatialMappingManager.Instance.CleanupObserver();
    }


    public void DoSaveRoom()
    {
#if !UNITY_EDITOR && UNITY_WSA
         

        MyRoomSaver.SaveRoom();
#endif

    }

    public void DoLoadRoom()
    {
#if !UNITY_EDITOR && UNITY_WSA
         

        MyRoomLoader.LoadRoom();
#endif

    }

    public GameObject pointer;
    List<GameObject> pointers = new List<GameObject>();
    public void PointmeTheMeshesAnchors()
    {
        if (MyRoomLoader.GetRoomMeshObjs() == null) return;
        if (MyRoomLoader.GetRoomMeshObjs().Count == 0) return;
        foreach (GameObject go in MyRoomLoader.GetRoomMeshObjs())
        {
            GameObject p = Instantiate(pointer, go.transform.position, Quaternion.identity);
            pointers.Add(p);
        }
    }

    public void RemovePointers()
    {
        for (int x = 0; x < pointers.Count; x++)
        {
            Destroy(pointers[x]);
        }
        pointers.Clear();

    }




}
