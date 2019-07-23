//#define ENABLE_KEYBORADINPUTS
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {


    EnemyModelsRepo Repo_enemies;

    GameObject InstantiatedEnemy;
    private void Awake()
    {
        Repo_enemies = GetComponent<EnemyModelsRepo>();
    }
    // Use this for initialization

#if ENABLE_KEYBORADINPUTS
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            KngGameMaster.Instance.PlopAGuy();
        }
    }
#endif

    // Update is called once per frame
    public GameObject InstanTiateEnemy(KngEnemyName argName, KNode argKnode)
    {
        return Instantiate(Repo_enemies.GetEnemyModel_Ref(argName), argKnode.KnodeTransform.position, Quaternion.identity);
    }
}
