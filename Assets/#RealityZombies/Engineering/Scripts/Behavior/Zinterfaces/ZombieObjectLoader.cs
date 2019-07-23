
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieObjectLoader : MonoBehaviour {

#region dependencies
#endregion

#region PublicVars
#endregion

#region PrivateVars
    ////GameObject ZombieFetched;

#endregion

#region INIT
    //void OnEnable()
    //{

    //}

    //private void OnDisable()
    //{

    //}

    //private void Awake()
    //{

    //}

    //private void Start()
    //{

    //}
#endregion

#region PublicMethods
    public GameObject FetchZombie(int argZModelNumer)
    {
        return Resources.Load(ModelNumber_Path(argZModelNumer), typeof(GameObject)) as GameObject;
    }
    public GameObject FetchFly(int argZModelNumer)
    {
        if (argZModelNumer > 2 || argZModelNumer < 1) argZModelNumer = 1;
        return Resources.Load(FlyModelNumber_Path(argZModelNumer), typeof(GameObject)) as GameObject;
    }
    
    #endregion

    #region PrivateMethods
    //void Update()
    //{

    //}

    // string zombie to fecth


    //zombie17 is a 2 mats
    string ModelNumber_Path(int argZmodelNumber) {
 
            return "EnemiesRaw/OneMatRagLow/Zombie" + argZmodelNumber.ToString() + "_OneMatRag";
       
        //else {
        //    return "EnemiesRaw/OneMatRagLow/Zombie" + argZmodelNumber.ToString() + "_TwoMatRag";
        //}
    }
    string FlyModelNumber_Path(int argZmodelNumber)
    {

        return "EnemiesRaw/FlyEnemies/Fly_" + argZmodelNumber.ToString() ;

        //else {
        //    return "EnemiesRaw/OneMatRagLow/Zombie" + argZmodelNumber.ToString() + "_TwoMatRag";
        //}
    }
    //private void OnDestroy()
    //{

    //}
    #endregion
}
