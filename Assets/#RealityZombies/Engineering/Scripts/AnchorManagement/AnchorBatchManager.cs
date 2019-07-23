using System.Collections.Generic;
using UnityEngine;
#if !UNITY_EDITOR && UNITY_WSA
               
using UnityEngine.XR.WSA;
using UnityEngine.XR.WSA.Persistence;
using UnityEngine.XR.WSA.Sharing;
#endif

public class AnchorBatchManager : MonoBehaviour
{
    public static AnchorBatchManager Instance = null;
#if !UNITY_EDITOR && UNITY_WSA
     
     private WorldAnchorTransferBatch currentAnchorTransferBatch;
#endif
    private List<byte> rawAnchorUploadData = new List<byte>(0);
    private List<byte> temp;
    private byte[] rawAnchorDownloadData;
    private const uint MinTrustworthySerializedAnchorDataSize = 100000;
#if !UNITY_EDITOR && UNITY_WSA
         
    public WorldAnchorStore AnchorStore { get; protected set; }
#endif
    int AnchorCount = 0;
    int _retryCount = 5;

#if !UNITY_EDITOR && UNITY_WSA
         
    void AnchorStoreReady(WorldAnchorStore anchorStore)
    {

        AnchorStore = anchorStore;
    }
#endif

    void Awake()
    {
        if (Instance == null)
        {
            Debug.Log("AncBatcher On " + gameObject.name);
            //DontDestroyOnLoad(this.gameObject);
            Instance = this;
        }
        else
            Destroy(gameObject);
    }
    void Start()
    {
        AnchorCount = 0;
#if !UNITY_EDITOR && UNITY_WSA
         
        WorldAnchorStore.GetAsync(AnchorStoreReady);
#endif
    }

    void AddStoredAncsToTransferBatch()
    {
#if !UNITY_EDITOR && UNITY_WSA
         
        currentAnchorTransferBatch = new WorldAnchorTransferBatch();
        // gather all stored anchors
        string[] ids = AnchorStore.GetAllIds();
        for (int index = 0; index < ids.Length; index++)
        {
            if (ids[index].Contains(GameSettings.Instance.AncName_ArrowRed()))
            {
                GameObject obj = PlacedObjecetsManager.Instance.Repo_InstantiateObjByAncNAme_tackUnderscore(GameSettings.Instance.AncName_ArrowRed());//Instantiate(barrier) as GameObject;
                WorldAnchor anchor = AnchorStore.Load(ids[index], obj);
                currentAnchorTransferBatch.AddWorldAnchor(ids[index], anchor);
            }
            else
                            if (ids[index].Contains(GameSettings.Instance.AncName_ArrowBlue()))
            {
                GameObject obj = PlacedObjecetsManager.Instance.Repo_InstantiateObjByAncNAme_tackUnderscore(GameSettings.Instance.AncName_ArrowBlue());//Instantiate(barrier) as GameObject;
                WorldAnchor anchor = AnchorStore.Load(ids[index], obj);
                currentAnchorTransferBatch.AddWorldAnchor(ids[index], anchor);
            }
            else
                            if (ids[index].Contains(GameSettings.Instance.AncName_ArrowGreen()))
            {
                GameObject obj = PlacedObjecetsManager.Instance.Repo_InstantiateObjByAncNAme_tackUnderscore(GameSettings.Instance.AncName_ArrowGreen());//Instantiate(barrier) as GameObject;
                WorldAnchor anchor = AnchorStore.Load(ids[index], obj);
                currentAnchorTransferBatch.AddWorldAnchor(ids[index], anchor);
            }
            else
                            if (ids[index].Contains(GameSettings.Instance.AncName_ArrowYellow()))
            {
                GameObject obj = PlacedObjecetsManager.Instance.Repo_InstantiateObjByAncNAme_tackUnderscore(GameSettings.Instance.AncName_ArrowYellow());//Instantiate(barrier) as GameObject;
                WorldAnchor anchor = AnchorStore.Load(ids[index], obj);
                currentAnchorTransferBatch.AddWorldAnchor(ids[index], anchor);
            }
            else
                            if (ids[index].Contains(GameSettings.Instance.AncName_ArenaBase()))
            {
                GameObject obj = PlacedObjecetsManager.Instance.Repo_InstantiateObjByAncNAme_tackUnderscore(GameSettings.Instance.AncName_ArenaBase());//Instantiate(barrier) as GameObject;
                WorldAnchor anchor = AnchorStore.Load(ids[index], obj);
                currentAnchorTransferBatch.AddWorldAnchor(ids[index], anchor);
            }
            else
                            if (ids[index].Contains(GameSettings.Instance.AncName_ArenaHotSpotBase()))
            {
                GameObject obj = PlacedObjecetsManager.Instance.Repo_InstantiateObjByAncNAme_tackUnderscore(GameSettings.Instance.AncName_ArenaHotSpotBase());//Instantiate(barrier) as GameObject;
                WorldAnchor anchor = AnchorStore.Load(ids[index], obj);
                currentAnchorTransferBatch.AddWorldAnchor(ids[index], anchor);
            }
            else
                            if (ids[index].Contains(GameSettings.Instance.AncName_ArenaPlayerBase()))
            {
                GameObject obj = PlacedObjecetsManager.Instance.Repo_InstantiateObjByAncNAme_tackUnderscore(GameSettings.Instance.AncName_ArenaPlayerBase());//Instantiate(barrier) as GameObject;
                WorldAnchor anchor = AnchorStore.Load(ids[index], obj);
                currentAnchorTransferBatch.AddWorldAnchor(ids[index], anchor);
            }
            else
                            if (ids[index].Contains(GameSettings.Instance.AncName_ArenaPillar()))
            {
                GameObject obj = PlacedObjecetsManager.Instance.Repo_InstantiateObjByAncNAme_tackUnderscore(GameSettings.Instance.AncName_ArenaPillar());//Instantiate(barrier) as GameObject;
                WorldAnchor anchor = AnchorStore.Load(ids[index], obj);
                currentAnchorTransferBatch.AddWorldAnchor(ids[index], anchor);
            }
            else
                            if (ids[index].Contains(GameSettings.Instance.AncName_ArenaStemBase()))
            {
                GameObject obj = PlacedObjecetsManager.Instance.Repo_InstantiateObjByAncNAme_tackUnderscore(GameSettings.Instance.AncName_ArenaStemBase());//Instantiate(barrier) as GameObject;
                WorldAnchor anchor = AnchorStore.Load(ids[index], obj);
                currentAnchorTransferBatch.AddWorldAnchor(ids[index], anchor);
            }
            else
                            if (ids[index].Contains(GameSettings.Instance.AncName_ArenaWallBase()))
            {
                GameObject obj = PlacedObjecetsManager.Instance.Repo_InstantiateObjByAncNAme_tackUnderscore(GameSettings.Instance.AncName_ArenaWallBase());//Instantiate(barrier) as GameObject;
                WorldAnchor anchor = AnchorStore.Load(ids[index], obj);
                currentAnchorTransferBatch.AddWorldAnchor(ids[index], anchor);
            }
            else
                            if (ids[index].Contains(GameSettings.Instance.AncName_ArenaScoreBase()))
            {
                GameObject obj = PlacedObjecetsManager.Instance.Repo_InstantiateObjByAncNAme_tackUnderscore(GameSettings.Instance.AncName_ArenaScoreBase());//Instantiate(barrier) as GameObject;
                WorldAnchor anchor = AnchorStore.Load(ids[index], obj);
                currentAnchorTransferBatch.AddWorldAnchor(ids[index], anchor);
            }
            else
                            if (ids[index].Contains(GameSettings.Instance.AncName_ArenaZombieSpawnBase()))
            {
                GameObject obj = PlacedObjecetsManager.Instance.Repo_InstantiateObjByAncNAme_tackUnderscore(GameSettings.Instance.AncName_ArenaZombieSpawnBase());//Instantiate(barrier) as GameObject;
                WorldAnchor anchor = AnchorStore.Load(ids[index], obj);
                currentAnchorTransferBatch.AddWorldAnchor(ids[index], anchor);
            }
            else
                            if (ids[index].Contains(GameSettings.Instance.AncName_ArenaGraveSpawnBase()))
            {
                GameObject obj = PlacedObjecetsManager.Instance.Repo_InstantiateObjByAncNAme_tackUnderscore(GameSettings.Instance.AncName_ArenaGraveSpawnBase());//Instantiate(barrier) as GameObject;
                WorldAnchor anchor = AnchorStore.Load(ids[index], obj);
                currentAnchorTransferBatch.AddWorldAnchor(ids[index], anchor);
            }
            else
                            if (ids[index].Contains(GameSettings.Instance.AncName_ArenaFlySpawnBase()))
            {
                GameObject obj = PlacedObjecetsManager.Instance.Repo_InstantiateObjByAncNAme_tackUnderscore(GameSettings.Instance.AncName_ArenaFlySpawnBase());//Instantiate(barrier) as GameObject;
                WorldAnchor anchor = AnchorStore.Load(ids[index], obj);
                currentAnchorTransferBatch.AddWorldAnchor(ids[index], anchor);
            }
            else
            if (ids[index].Contains(GameSettings.Instance.AncName_ArenRoofSpawnBase()))
            {
                GameObject obj = PlacedObjecetsManager.Instance.Repo_InstantiateObjByAncNAme_tackUnderscore(GameSettings.Instance.AncName_ArenRoofSpawnBase());//Instantiate(barrier) as GameObject;
                WorldAnchor anchor = AnchorStore.Load(ids[index], obj);
                currentAnchorTransferBatch.AddWorldAnchor(ids[index], anchor);
            }

            else
            if (ids[index].Contains(GameSettings.Instance.AncName_ArenaWayPointBase()))
            {
                GameObject obj = PlacedObjecetsManager.Instance.Repo_InstantiateObjByAncNAme_tackUnderscore(GameSettings.Instance.AncName_ArenaWayPointBase());//Instantiate(barrier) as GameObject;
                WorldAnchor anchor = AnchorStore.Load(ids[index], obj);
                currentAnchorTransferBatch.AddWorldAnchor(ids[index], anchor);
            }
        }

        PersistantBatchHolder.Instance.BatchCache1 = currentAnchorTransferBatch;
        PersistantBatchHolder.Instance.Debug_Batch1_AncCountAndAncNames();
#endif

    }
    private void WriteBuffer(byte[] data)
    {
        rawAnchorUploadData.AddRange(data);
        //StaticHexLogger.WriteFast_32BitLinesText(rawAnchorUploadData, StaticHexLogger.DirTarget, StaticHexLogger.FileName_BatchSaveHexText);
        StaticHexLogger.SaveWATB(rawAnchorUploadData.ToArray(), StaticHexLogger.DirTarget, StaticHexLogger.FileName_BatchSaveBytesWACt);
    }
    public void SaveToFile()
    {
        AddStoredAncsToTransferBatch();
#if !UNITY_EDITOR && UNITY_WSA
     
         WorldAnchorTransferBatch.ExportAsync(currentAnchorTransferBatch, WriteBuffer, ExportComplete);
#endif
    }
#if !UNITY_EDITOR && UNITY_WSA
     
    private void ExportComplete(SerializationCompletionReason status)
    {
        if (status == SerializationCompletionReason.Succeeded)
        {
            Debug.LogFormat("[wac] Export Success  {0} anchors from batch", currentAnchorTransferBatch.anchorCount.ToString());
            string[] anchorNames = currentAnchorTransferBatch.GetAllIds();
            for (var i = 0; i < anchorNames.Length; i++)
            {
                //XString xstr = new XString(anchorNames[i]);
                //Debug.Log("  " + anchorNames[i] + " " + xstr);

                //SharingStage.Instance.Manager.GetRoomManager().UploadAnchor(
                //    SharingStage.Instance.CurrentRoom,
                //    new XString(anchorNames[i]),
                //    rawAnchorUploadData.ToArray(),
                //    rawAnchorUploadData.Count);
            }
            //  _DATAsaveLoad.Save(rawAnchorUploadData.ToArray());
        }
        else
        {
            Debug.Log(" [wac] Failed to export anchor!");
        }
        temp = new List<byte>();
        foreach (byte bt in rawAnchorUploadData) { temp.Add(bt); }
        rawAnchorUploadData.Clear();
    }
#endif


    //*******************************************************************************************************************************

    public void LoadFromFile()
    {
        rawAnchorDownloadData = null;
        List<byte> LB = StaticHexLogger.LoadWATB(StaticHexLogger.DirTarget, StaticHexLogger.FileName_BatchSaveBytesWACt);
        rawAnchorDownloadData = LB.ToArray();
#if !UNITY_EDITOR && UNITY_WSA
     
         WorldAnchorTransferBatch.ImportAsync(rawAnchorDownloadData, ImportComplete);
#endif
    }
#if !UNITY_EDITOR && UNITY_WSA
     
    private void ImportComplete(SerializationCompletionReason status, WorldAnchorTransferBatch anchorBatch)
    {
        bool successful = status == SerializationCompletionReason.Succeeded;
        if (successful)
        {
            Debug.LogFormat("[wac] Import Success  {0} anchors from batch", anchorBatch.anchorCount.ToString());
            string[] anchorNames = anchorBatch.GetAllIds();
            for (var i = 0; i < anchorNames.Length; i++)
            {
                GameObject objectToAnchor = null;
                string rawname = anchorNames[i].Split('_')[0];
                objectToAnchor = PlacedObjecetsManager.Instance.Repo_InstantiateObjByAncNAme_tackUnderscore(rawname);
                objectToAnchor.name = anchorNames[i];
                objectToAnchor.GetComponentInChildren<TextMesh>().text = anchorNames[i];
#if !UNITY_EDITOR && UNITY_WSA
         
                AnchorStore.Save(anchorNames[i], anchorBatch.LockObject(anchorNames[i], objectToAnchor));
#endif
            }
        }
        else
        {
            Debug.LogError("[wac] Import failed!");
        }

        anchorBatch.Dispose();
        rawAnchorDownloadData = null;
    }
#endif

}





//private void OnDeserializationComplete(SerializationCompletionReason completionreason, WorldAnchorTransferBatch deserializedtransferbatch)
//{
//    if (completionreason != SerializationCompletionReason.Succeeded)
//    {
//        // Failed
//        if (_retryCount > 0)
//        {
//            _retryCount--;
//            WorldAnchorTransferBatch.ImportAsync(rawAnchorDownloadData, OnDeserializationComplete);
//        }
//    }
//    print("[Client]: Deserialization completed with: " + completionreason + " Number of anchors: " + deserializedtransferbatch.anchorCount);
//    if (deserializedtransferbatch.anchorCount > 0)
//    {
//        string[] anchorNames = deserializedtransferbatch.GetAllIds();
//        for (var i = 0; i < anchorNames.Length; i++)
//        {
//            GameObject objectToAnchor = null;
//            string rawname = anchorNames[i].Split('_')[0];
//            objectToAnchor = _Repo.Get_Object_byName(rawname);
//            AnchorStore.Save(anchorNames[i], deserializedtransferbatch.LockObject(anchorNames[i], objectToAnchor));
//        }
//        //  AnchorImportedHandler(deserializedtransferbatch);
//        // HololensClient.Instance.SendToServer(TMsgType.Error, new TMessage() { Description = completionreason + ": " + deserializedtransferbatch.anchorCount });
//    }
//}
