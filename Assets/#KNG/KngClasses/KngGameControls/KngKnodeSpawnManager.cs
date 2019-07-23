using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KngKnodeSpawnManager : MonoBehaviour {
    //Depending on wave or time, this will have a list of available spawn points
    // this will also turn the knode available to false on a timer;


    public KNodeManager KnodesManager;
    public List<KNode> AvailableSpawnNodes;

    void Reset_AvailableSpawnNodes() {
        if (AvailableSpawnNodes!=null) {
            AvailableSpawnNodes.Clear();
        }
        AvailableSpawnNodes = new List<KNode>();

    }

    private void Awake()
    {
        Reset_AvailableSpawnNodes();
    }

    public void GameMasterInit_KnodeManager(KNodeManager _argKnodeManager) {
        KnodesManager = _argKnodeManager;
    }
    public List<KNode> Get_SpawnsList_forLevel(int argLevelNumber)
    {
        Reset_AvailableSpawnNodes();
        AvailableSpawnNodes = KnodesManager.GetAll_BackRowSpawns();
        return AvailableSpawnNodes; 
    }

    public KNode Get_Spawn_Node(int argID)
    {
         
          
        return KnodesManager.GetNode(argID);
    }
}
