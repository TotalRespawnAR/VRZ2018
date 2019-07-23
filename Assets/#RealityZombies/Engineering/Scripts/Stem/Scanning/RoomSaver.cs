// @Author Jeffrey M. Paquette ©2016

using System.Collections.Generic;
using UnityEngine;

public class RoomSaver : MonoBehaviour
{
    public static RoomSaver Instance = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    string fileName = "arzroom";             // name of file to store meshes
    string anchorStoreName = "arzroomAnc";      // name of world anchor to store for room
                                                // public string fileName= "ARZArena";             // name of file to store meshes
                                                //  public string anchorStoreName= "ARZRoomMesh";      // name of world anchor to store for room

    List<MeshFilter> roomMeshFilters;
#if !UNITY_EDITOR && UNITY_WSA
         

    UnityEngine.XR.WSA.Persistence.WorldAnchorStore anchorStore;
#endif

    int meshCount = 0;

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
    }
#endif

#if !UNITY_EDITOR && UNITY_WSA
         

    public void SaveRoom()
    {
        // if the anchor store is not ready then we cannot save the room mesh
        if (anchorStore == null)
            return;

        // delete old relevant anchors
        string[] anchorIds = anchorStore.GetAllIds();
        for (int i = 0; i < anchorIds.Length; i++)
        {
            if (anchorIds[i].Contains(anchorStoreName))
            {
                anchorStore.Delete(anchorIds[i]);
            }
        }
        // get all mesh filters used for spatial mapping meshes
        roomMeshFilters = SpatialMappingManager.Instance.GetMeshFilters() as List<MeshFilter>;

        // create new list of room meshes for serialization
        List<Mesh> roomMeshes = new List<Mesh>();

        // cycle through all room mesh filters
        foreach (MeshFilter filter in roomMeshFilters)
        {
            // increase count of meshes in room
            meshCount++;

            // make mesh name = anchor name + mesh count
            string meshName = anchorStoreName + meshCount.ToString();
            filter.mesh.name = meshName;

            // add mesh to room meshes for serialization
            roomMeshes.Add(filter.mesh);

            // save world anchor
            UnityEngine.XR.WSA.WorldAnchor attachingAnchor = filter.gameObject.GetComponent<UnityEngine.XR.WSA.WorldAnchor>();
            if (attachingAnchor == null)
            {
                attachingAnchor = filter.gameObject.AddComponent<UnityEngine.XR.WSA.WorldAnchor>();
            }
            else
            {
                DestroyImmediate(attachingAnchor);
                attachingAnchor = filter.gameObject.AddComponent<UnityEngine.XR.WSA.WorldAnchor>();
            }
            if (attachingAnchor.isLocated)
            {
                if (!anchorStore.Save(meshName, attachingAnchor))
                    Logger.Debug("" + meshName + ": Anchor save failed...");
                else
                    Logger.Debug("" + meshName + ": Anchor SAVED...");
            }
            else
            {
                attachingAnchor.OnTrackingChanged += AttachingAnchor_OnTrackingChanged;
            }
        }

        // serialize and save meshes
        MeshSaverOld.Save(fileName, roomMeshes);
        //MeshSaver.Save(fileName, roomMeshes);
    }
#endif

#if !UNITY_EDITOR && UNITY_WSA
         

    private void AttachingAnchor_OnTrackingChanged(UnityEngine.XR.WSA.WorldAnchor self, bool located)
    {
        if (located)
        {
            string meshName = self.gameObject.GetComponent<MeshFilter>().mesh.name;
            if (!anchorStore.Save(meshName, self))
                Logger.Debug("" + meshName + ": Anchor save failed...");
            else
                Logger.Debug("" + meshName + ": Anchor SAVED...");

            self.OnTrackingChanged -= AttachingAnchor_OnTrackingChanged;
        }
    }
#endif

}
