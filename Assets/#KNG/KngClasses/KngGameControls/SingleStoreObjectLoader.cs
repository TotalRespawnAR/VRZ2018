using UnityEngine;


public class SingleStoreObjectLoader : MonoBehaviour
{
    public GameObject PathFinderWorldRef;
    string AnchorName_PathFinder;
    GameObject _localPathFindObj;
#if !UNITY_EDITOR && UNITY_WSA
         

    UnityEngine.XR.WSA.Persistence.WorldAnchorStore anchorStore;
#endif



    public Transform getPathfinderWorldReff() { return this.PathFinderWorldRef.transform; }


    void InitAnchorNameVariables()
    {
        InitAnchorNameVariables();
        AnchorName_PathFinder = GameSettings.Instance.GetAnchorName_PathFinder();
    }
    // Use this for initialization
    void Start()
    {
#if !UNITY_EDITOR && UNITY_WSA
         

        UnityEngine.XR.WSA.Persistence.WorldAnchorStore.GetAsync(AnchorStoreReady);
#endif

    }

#if !UNITY_EDITOR && UNITY_WSA
         

    void AnchorStoreReady(UnityEngine.XR.WSA.Persistence.WorldAnchorStore store)
    {
        anchorStore = store;
        LoadObjects();
    }
    
    void LoadObjects()
    {

        // gather all stored anchors
        string[] ids = anchorStore.GetAllIds();
        for (int index = 0; index < ids.Length; index++)
        {

            if (ids[index].Contains("PathFind"))
            {
                _localPathFindObj = Instantiate(PathFinderWorldRef) as GameObject;
                anchorStore.Load(ids[index], _localPathFindObj);
                UnityEngine.XR.WSA.WorldAnchor attachedAnchor = _localPathFindObj.GetComponent<UnityEngine.XR.WSA.WorldAnchor>();
                if (attachedAnchor != null) DestroyImmediate(attachedAnchor);
                _localPathFindObj.name = "myWorldRefObject";
            }
        }


        if (AnchorName_PathFinder == null || AnchorName_PathFinder == "")
        {
            DebugConsole.print(" pathfinder is NULL or empty");
        }

        // LevelLoaded();
        //todonabilsr
        //maketheStartResetBlock();
        // maketheStarBlock();
    }
#endif


}
