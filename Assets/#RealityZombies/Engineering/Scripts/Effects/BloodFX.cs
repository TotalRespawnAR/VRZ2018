// @Author Jeffrey M. Paquette ©2016

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BloodFX : MonoBehaviour {

    [Tooltip("List of prefabs randomly generated on head shots.")]
    public GameObject[] HeadShotBloodPrefabs;

    [Tooltip("List of prefabs randomly generated on torso shots.")]
    public GameObject[] TorsoShotBloodPrefabs;

    [Tooltip("List of prefabs randomly generated on limb shots.")]
    public GameObject[] LimbShotBloodPrefabs;


    public GameObject HexBlood;
    public GameObject ShatterEffects;

    [Tooltip("How long the effect will remail active before destroying it.")]
    public float killDelay = 3.0f;
    public void HeadShotFX_CHEAT(Transform rgTransOfHEad)
    {
        // Transform ClosesZombieHEad = Gamem
        GameObject obj = Instantiate(ShatterEffects, rgTransOfHEad.position, Quaternion.FromToRotation(Vector3.forward, rgTransOfHEad.position), rgTransOfHEad) as GameObject;
        KillTimer t = obj.AddComponent<KillTimer>();
        t.StartTimer(killDelay);
    }

    public void HeadShotFX(RaycastHit hitInfo,bool useHex)
    {
        if (useHex)
        {
            Shot_HEX_FX(hitInfo);
        }
        else
        {
            HeadShotBloodFX(hitInfo);
        }
    }

    public void TorsoShotFX(RaycastHit hitInfo, bool useHex)
    {
        if (useHex)
        {
            Shot_HEX_FX(hitInfo);
        }
        else
        {
            TorsoShotBloodFX(hitInfo);
        }
    }

    public void PelvisShotFX(RaycastHit hitInfo, bool useHex)
    {
        if (useHex)
        {
            Shot_HEX_FX(hitInfo);
        }
        else
        {
            PelvisShotBloodFX(hitInfo);
        }
    }

    public void LimbShotFX(RaycastHit hitInfo, bool useHex )
    {
        if (useHex)
        {
            Shot_HEX_FX(hitInfo);
        }
        else
        {
            LimbShotBloodFX(hitInfo);
        }
    }

    void Shot_HEX_FX(RaycastHit hitInfo)
    {
        
           // Transform ClosesZombieHEad = Gamem
           // GameObject obj = Instantiate(ShatterEffects, hitInfo.point, Quaternion.FromToRotation(Vector3.forward, hitInfo.normal), hitInfo.collider.transform) as GameObject;

        GameObject obj = Instantiate(ShatterEffects, hitInfo.point, Quaternion.identity) as GameObject;

        KillTimer t = obj.AddComponent<KillTimer>();
            t.StartTimer(killDelay);
     
       

    }

    void HeadShotBloodFX(RaycastHit hitInfo)
    {
        int index = Random.Range(0, HeadShotBloodPrefabs.Length);
        GameObject obj = Instantiate(HeadShotBloodPrefabs[index], hitInfo.point, Quaternion.FromToRotation(Vector3.forward, hitInfo.normal), hitInfo.collider.transform) as GameObject;
        KillTimer t = obj.AddComponent<KillTimer>();
        t.StartTimer(killDelay);
    }

     void TorsoShotBloodFX(RaycastHit hitInfo)
    {
        int index = Random.Range(0, TorsoShotBloodPrefabs.Length);
        GameObject obj = Instantiate(TorsoShotBloodPrefabs[index], hitInfo.point, Quaternion.FromToRotation(Vector3.forward, hitInfo.normal), hitInfo.collider.transform) as GameObject;
        KillTimer t = obj.AddComponent<KillTimer>();
        t.StartTimer(killDelay);
    }

    void PelvisShotBloodFX(RaycastHit hitInfo)
    {
        int index = Random.Range(0, TorsoShotBloodPrefabs.Length);
        GameObject obj = Instantiate(TorsoShotBloodPrefabs[index], hitInfo.point, Quaternion.FromToRotation(Vector3.forward, hitInfo.normal), hitInfo.collider.transform) as GameObject;
        KillTimer t = obj.AddComponent<KillTimer>();
        t.StartTimer(killDelay);
    }

    void LimbShotBloodFX(RaycastHit hitInfo)
    {
        int index = Random.Range(0, LimbShotBloodPrefabs.Length);
        GameObject obj = Instantiate(LimbShotBloodPrefabs[index], hitInfo.point, Quaternion.FromToRotation(Vector3.forward, hitInfo.normal), hitInfo.collider.transform) as GameObject;
        KillTimer t = obj.AddComponent<KillTimer>();
        t.StartTimer(killDelay);
    }
}
