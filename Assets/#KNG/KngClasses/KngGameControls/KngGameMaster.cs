using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KngGameMaster : MonoBehaviour {
    public static KngGameMaster Instance = null;

    private KngSceneObjectsManager _SceneObjMNGR;
    private KngKnodeSpawnManager _knodeSpawnMNGR;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            _SceneObjMNGR = GetComponent<KngSceneObjectsManager>();
            _knodeSpawnMNGR = GetComponent<KngKnodeSpawnManager>();
            _knodeSpawnMNGR.GameMasterInit_KnodeManager(_SceneObjMNGR.KnodeManagerRef);
        }
        else
        {
            DestroyImmediate(this.gameObject);
        }
    }


    int Erefcnt = -1;
   public void PlopAGuy() {
        Erefcnt++;
        if (Erefcnt > (int)KngEnemyName.FLY1) Erefcnt = 0;

        KNode spawnnode = _knodeSpawnMNGR.Get_Spawn_Node(Erefcnt);
        GameObject Eref = _SceneObjMNGR.EnemySpanerRef.InstanTiateEnemy((KngEnemyName)Erefcnt, spawnnode);
    }

    
   
}
