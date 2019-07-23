// @Author Nabil Lamriben ©2018


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieFactory : MonoBehaviour {
/// <summary>
///  Factory will instantiate the zombie , the apply the material to the instance 
/// </summary>

    #region dependencies
    ZombieObjectLoader _ZObjLoader;
    ZombieMaterialLoader _ZMatLoader;
    #endregion

    #region PublicVars
    public GameObject AxeGuy;
    #endregion

    #region PrivateVars
    GameObject EnemyToBuild;
    #endregion

    #region INIT


    public GameObject HealthBarObj;

    private void Awake()
    {
        _ZObjLoader = GetComponent<ZombieObjectLoader>();
        _ZMatLoader = GetComponent<ZombieMaterialLoader>();
        ResetZombieTOBuildRef();
    }

   
    private void ResetZombieTOBuildRef() {
        EnemyToBuild = null;
    }
    #endregion

    #region PublicMethods   

    public GameObject MakeZombie(int argZModelNumbern ,int argMatNumber) {
    ResetZombieTOBuildRef();
    // FetchZombie(); //obj loader
    EnemyToBuild = Instantiate(_ZObjLoader.FetchZombie(argZModelNumbern));
    ApplyMaterial(argZModelNumbern, argMatNumber);
    return EnemyToBuild;
    }
public GameObject MakeZombie(int argZModelNumbern ,int argMatNumber, Vector3 argRot) {
        ResetZombieTOBuildRef();
        // FetchZombie(); //obj loader
        EnemyToBuild = Instantiate( _ZObjLoader.FetchZombie(argZModelNumbern),Vector3.zero,Quaternion.EulerAngles(argRot));
        ApplyMaterial(argZModelNumbern,argMatNumber);
        // componens added here did not work

        

        GameObject HB = Instantiate(HealthBarObj);
        EnemyToBuild.GetComponentInChildren<IZeffects>().SetHealthBar(HB);
        HB.transform.parent =  EnemyToBuild.transform;
       // HB.transform.localPosition =  new Vector3( 0f,0.5f, 0f);

        return EnemyToBuild;
    }

    public GameObject MakeTutoAxeZombie(Transform spawnTrans, Vector3 argRot)
    {
        ResetZombieTOBuildRef();
        // FetchZombie(); //obj loader
        EnemyToBuild = Instantiate(AxeGuy, spawnTrans.position, Quaternion.Euler(argRot)); //Quaternion.identity);// EulerAngles(argRot));

        GameObject HB = Instantiate(HealthBarObj);
        EnemyToBuild.GetComponentInChildren<IZeffects>().SetHealthBar(HB);
        EnemyToBuild.GetComponentInChildren<AxeEnemyBehavior>().AxeState0Idle1Walk2Run3DeadTrigThrow5Pause5 = 0;
        HB.transform.parent = EnemyToBuild.transform;


        return EnemyToBuild;
    }

    /*
 
    //////Spawntransformcontroller.DoWorkWithThisBatch() ->GameManager.REQ_NewAxeGuy() -> ZSpwanManager.CreateAxeGuyOnSpawnWithDE() ->
    public GameObject MakeAxeZombie(  Transform spawnTrans, Vector3 argRot, WayPointsStruct argSpStruct)
    {
        ResetZombieTOBuildRef();
        // FetchZombie(); //obj loader
        EnemyToBuild = Instantiate(AxeGuy, spawnTrans.position, Quaternion.Euler(argRot)); //Quaternion.identity);// EulerAngles(argRot));

        GameObject HB = Instantiate(HealthBarObj);
        EnemyToBuild.GetComponentInChildren<IZeffects>().SetHealthBar(HB);
        HB.transform.parent = EnemyToBuild.transform;

    
        return EnemyToBuild;
    }


    public GameObject MakeAxeZombieDiffrently(Transform spawnTrans, Vector3 argRot, WayPointsStruct argSpStruct)
    {
        ResetZombieTOBuildRef();
        // FetchZombie(); //obj loader
        EnemyToBuild = Instantiate(AxeGuy, spawnTrans.position, Quaternion.Euler(argRot)); //Quaternion.identity);// EulerAngles(argRot));

        GameObject HB = Instantiate(HealthBarObj);
        EnemyToBuild.GetComponentInChildren<IZeffects>().SetHealthBar(HB);
        HB.transform.parent = EnemyToBuild.transform;


        return EnemyToBuild;
    }

    public GameObject MakeAxeZombieVERYDiffrently(Vector3 argpos, Vector3 argRot, WayPointsStruct argSpStruct)
    {
        ResetZombieTOBuildRef();
        // FetchZombie(); //obj loader
        EnemyToBuild = Instantiate(AxeGuy, new Vector3(argpos.x, argpos.y-0.333f, argpos.z) , Quaternion.Euler(argRot)); //Quaternion.identity);// EulerAngles(argRot));

        GameObject HB = Instantiate(HealthBarObj);
        EnemyToBuild.GetComponentInChildren<IZeffects>().SetHealthBar(HB);
        HB.transform.parent = EnemyToBuild.transform;


        return EnemyToBuild;
    }

    public GameObject MakeFlyZombie(int argZModelNumbern, Transform flypoint, Vector3 argRot, WayPointsStruct argSpStruct)
    {
        ResetZombieTOBuildRef();
        // FetchZombie(); //obj loader
        EnemyToBuild = Instantiate(_ZObjLoader.FetchFly(argZModelNumbern), flypoint.position, Quaternion.EulerAngles(argRot));

        FlyBehavior fb = EnemyToBuild.GetComponent<FlyBehavior>();
        if (fb != null)
        {
            fb.SetWayPointstruct(argSpStruct);
            fb.SetMoveToPlayerYOffset(argZModelNumbern); //if 1 , offset y=0.8 else 0 
        }
        else
            Debug.Log("not a flybehavvior... i know .... bad design");

        // componens added here did not work



        GameObject HB = Instantiate(HealthBarObj, EnemyToBuild.transform.position, Quaternion.identity);
        // ZombieToBuild.GetComponentInChildren<ZombieEffects>().SetHealthBar(HB);
        HB.transform.parent = EnemyToBuild.transform;
        // HB.transform.localPosition =  new Vector3( 0f,0.5f, 0f);

        return EnemyToBuild;
    }
    */


    #endregion

    #region PrivateMethods


    private void ApplyMaterial(int argZModelNumber, int argMAtNumber) {

      //  Debug.Log("zid" + argZModelNumber);
       // ZombieToBuild.GetComponent<ZombieMaterialsManager>().SetMaterialExternalandInternal(_ZMatLoader.FetchSingleMaterial(argZModelNumber, argMAtNumber));
       EnemyToBuild.GetComponent<ZombieMaterialsManager>().SetMaterialSarra(_ZMatLoader.FetchSameSetMaterials(argZModelNumber, argMAtNumber), GameObject.Find("CubeShaderRef").GetComponent<MeshRenderer>().materials , argZModelNumber);

    }


  
    #endregion
}
