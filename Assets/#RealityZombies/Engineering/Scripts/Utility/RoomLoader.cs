// @Author Jeffrey M. Paquette ©2016

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomLoader : MonoBehaviour
{

    public GameObject managerObject;            // the room manager for this scene
    public GameObject surfaceObject;            // prefab for surface mesh objects
    public string fileName = "ARZArena";         // name of file used to store mesh
    public string anchorStoreName = "ARZRoomMesh";// name of world anchor for room
#if !UNITY_EDITOR && UNITY_WSA
         

    UnityEngine.XR.WSA.Persistence.WorldAnchorStore anchorStore;               // store of world anchors
#endif

    List<Mesh> roomMeshes;                      // list of room meshes
    List<GameObject> roomObjects;               // list of game objects that hold room meshes
    public List<GameObject> GetRoomMeshObjs() { return roomObjects; }
    // Use this for initialization
    void Start()
    {
        //Logger.Debug("async");
        // get instance of WorldAnchorStore

        //if (gameObject.name.Contains("MenuObj_")) {
        //    return;
        //}

        //NeverUse RoomLoader in game anymore
        //if (SceneManager.GetActiveScene().name.Contains( "Game_"))
        //{
        //    if (GameSettings.Instance.IsUseHololens) {
        //        managerObject.SendMessage("RoomLoaded_copy_forStandalone");
        //        return;
        //    }
        //}

        //otherwise this is the old MultiAnchorGame, therefore load on start

        if (SceneManager.GetActiveScene().name.Contains("Setup"))
        {
            if (GameSettings.Instance.IsUseHololens() == false)
            {
                return;
            }
#if !UNITY_EDITOR && UNITY_WSA
         

            UnityEngine.XR.WSA.Persistence.WorldAnchorStore.GetAsync(AnchorStoreReady);
#endif

        }

    }

    public void LoadRoom()
    {
#if !UNITY_EDITOR && UNITY_WSA
         

        UnityEngine.XR.WSA.Persistence.WorldAnchorStore.GetAsync(AnchorStoreReady_NEWLOAD);
#endif

    }


    public void ToggleRoom()
    {
        foreach (GameObject obj in roomObjects)
        {
            if (obj.activeInHierarchy)
                obj.SetActive(false);
            else
                obj.SetActive(true);
        }
    }
#if !UNITY_EDITOR && UNITY_WSA
         

    void AnchorStoreReady_NEWLOAD(UnityEngine.XR.WSA.Persistence.WorldAnchorStore store)
    {
        // save instance
        anchorStore = store;

        // load room meshesn
        //roomMeshes = MeshSaver.Load(fileName) as List<Mesh>;
        roomMeshes = MeshSaverOld.Load(fileName) as List<Mesh>;
        roomObjects = new List<GameObject>();

        foreach (Mesh surface in roomMeshes)
        {
            GameObject obj = Instantiate(surfaceObject) as GameObject;
            obj.GetComponent<MeshFilter>().mesh = surface;
            obj.GetComponent<MeshCollider>().sharedMesh = surface;
            //  obj.transform.parent = this.transform;
            roomObjects.Add(obj);

            if (!anchorStore.Load(surface.name, obj))
            {
                Console3D.Instance.LOGit("failed load mesh");
                Logger.Debug("WorldAnchor load failed...");
            }
        }

        //if (managerObject != null)
        //    managerObject.SendMessage("RoomLoaded");
    }
#endif


#if !UNITY_EDITOR && UNITY_WSA
         

    void AnchorStoreReady(UnityEngine.XR.WSA.Persistence.WorldAnchorStore store)
    {
        // save instance
        anchorStore = store;

        // load room meshesn
        //roomMeshes = MeshSaver.Load(fileName) as List<Mesh>;
        roomMeshes = MeshSaverOld.Load(fileName) as List<Mesh>;
        roomObjects = new List<GameObject>();

        foreach (Mesh surface in roomMeshes)
        {
            GameObject obj = Instantiate(surfaceObject) as GameObject;
            obj.GetComponent<MeshFilter>().mesh = surface;
            obj.GetComponent<MeshCollider>().sharedMesh = surface;
            obj.transform.parent = this.transform;
            roomObjects.Add(obj);

            if (!anchorStore.Load(surface.name, obj))
            {
                Console3D.Instance.LOGit("failed load mesh");
                Logger.Debug("WorldAnchor load failed...");
            }
        }

        if (managerObject != null)
            managerObject.SendMessage("RoomLoaded");
    }


#endif
    void OnDestroy()
    {


#if !UNITY_EDITOR && UNITY_WSA
         
 

        //if (GameManagerOld.Instance == null) return;
        //else
        // if (GameSettings.Instance.IsUseHololens())
        //{
        //    return;
        //}
        //else {
        //    Logger.Debug(" need to fix this hack. i need a useSigleAnchorWay and useOldMultiAnchorWay");
        //    //return;
        //}
#endif
        //if (GameManagerOld.Instance == null)
        //{
        //    return;
        //}

        if (GameSettings.Instance.IsUseHololens())
        {
            return;
        }
        else
        {
            foreach (Mesh mesh in roomMeshes)
            {
                Destroy(mesh);
            }
            roomMeshes.Clear();
        }

    }
}
