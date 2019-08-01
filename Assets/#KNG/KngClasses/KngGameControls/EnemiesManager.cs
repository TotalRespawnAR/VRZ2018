//#define ENABLE_KEYBORADINPUTS
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    void OnEnable()
    {
        //GameEventsManager.OnShooterMissed += SpeedUpLiveZombies;
        // GameEventsManager.OnShooterFired += IHeardAshot;

        // GameManager.OnShooterHitted += AWAKEALLandWALK;
        //  GameManager.OnRzExperienceStarted += AWAKEALLandWALK;
        // GameEventsManager.OnGunSetChanged += UnAimAllIfShotGun;
    }

    private void OnDisable()
    {
        // GameEventsManager.OnShooterMissed -= SpeedUpLiveZombies;
        // GameEventsManager.OnShooterFired -= IHeardAshot;
        //GameManager.OnShooterHitted -= AWAKEALLandWALK;
        // GameManager.OnRzExperienceStarted -= AWAKEALLandWALK;
        //    GameEventsManager.OnGunSetChanged -= UnAimAllIfShotGun;
    }
    int UniqueEnemyID = 0;
    int UniqueEnemyAXEID = 0;

    List<IEnemyEntityComp> IENEMIES = new List<IEnemyEntityComp>();

    public void Add_IENEMY(IEnemyEntityComp argIbeh) { }
























































    public List<GameObject> liveenemies = new List<GameObject>();
    public List<GameObject> Flyenemies = new List<GameObject>();
    public List<GameObject> Axenemies = new List<GameObject>();
    public List<GameObject> EnemyProjectiles = new List<GameObject>();

    public int GetNEWEnemyId()
    {
        return ++UniqueEnemyID;
    }
    public int GetNewAxeID()
    {
        return ++UniqueEnemyAXEID;
    }

    public void AddLiveZombie(GameObject argZombj)
    {
        liveenemies.Add(argZombj);
    }
    public void AddLiveFly(GameObject argFlypobj)
    {
        Flyenemies.Add(argFlypobj);
    }
    public void AddLiveAxeEnemy(GameObject argAxeZombie)
    {
        Axenemies.Add(argAxeZombie);
    }
    public void AddLiveEnemyProjectile(GameObject argAxe)
    {
        EnemyProjectiles.Add(argAxe);
    }


    public void Un_Aim_All_Live_Enemies()
    {
        foreach (GameObject g in liveenemies)
        {
            if (g.GetComponent<IEnemyEntityComp>().GetMainEnemyIsAimedAt())
            {
                g.GetComponent<IEnemyEntityComp>().SetMainEnemyIsAmedAt(false);
            }
        }
    }

    public void RemoveLiveZombie(int argZombieTOkill_ID)
    {


        liveenemies.RemoveAll(p => p.GetComponent<IEnemyEntityComp>().Get_ID() == argZombieTOkill_ID);
        if (liveenemies.Count == 0)
        {
            GameEventsManager.Instance.Call_AllLiveZombiesDied();
        }
        //if (liveenemies.Count == 0 && Axenemies.Count == 0)
        //{
        //    GameEventsManager.Instance.Call_AllLiveZombiesDied();
        //}
    }
    public void RemoveFlyZombie(int argFlyOkill_ID)
    {
        //Flyenemies.RemoveAll(p => p.GetComponent<FlyBehavior>().GetFlyId() == argFlyOkill_ID);
    }
    public void RemoveEnemyProjectile(int argEPkill_ID)
    {
        EnemyProjectiles.RemoveAll(p => p.GetComponent<EnemySpinnerProjectile>().GetAxeId() == argEPkill_ID);
    }
    public void RemoveAxeZombie(int argAxeZ)
    {
        //  Axenemies.RemoveAll(p => p.GetComponent<IEnemyBehaviourComp>().GetZombieID() == argAxeZ);
        if (liveenemies.Count == 0 && Axenemies.Count == 0)
        {
            GameEventsManager.Instance.Call_AllLiveZombiesDied();
        }
        // Debug.LogError("YO make a axezombiebeh");
        // EnemyProjectiles.RemoveAll(p => p.GetComponent<AxeBehavior>().GetAxeId() == argAxeZ);
    }




    public void PausLiveZombies(bool argonoff)
    {
        foreach (GameObject g in liveenemies)
        {
            g.GetComponent<IEnemyEntityComp>().PauseEnemy(argonoff);
        }
        //foreach (GameObject g in Axenemies)
        //{
        //   // g.GetComponent<IEnemyBehaviourComp>().Zbeh_PauseZombieAnimation();
        //}
        //foreach (GameObject g in Flyenemies)
        //{
        //    g.GetComponent<FlyBehavior>().StopFly();
        //}
    }
    public void RESETLiveZombies()
    {
        Debug.Log("RESETLiveZombies");
        foreach (GameObject g in liveenemies)
        {
            //Animator a = g.GetComponent<Animator>();

            //foreach (var anim in a)
            //{
            //    anim.Stop();
            //}

            g.GetComponent<IEnemyEntityComp>().Kill_CurrMODE(); //this hapens when zombie dies by the gun. not by reset, so use this method to kill cur mode
            Destroy(g, 1);
        }
        liveenemies = new List<GameObject>();

        foreach (GameObject g in Axenemies)
        {
            Destroy(g);
        }
        Axenemies = new List<GameObject>();


        foreach (GameObject g in Flyenemies)
        {
            Destroy(g);
        }
        Flyenemies = new List<GameObject>();




    }
    public void CallZombieDiedOnEachZombieAndResetList()
    {
        foreach (GameObject enemy in liveenemies)
        {
            //enemy.GetComponent<IEnemyBehaviourComp>().ImmediateSelfDestructt();
        }
        liveenemies.Clear();

    }

    public GameObject GetLiveZombieByID(int argid)
    {
        GameObject ZombieToReturn = null;
        foreach (GameObject go in liveenemies)
        {
            //if (go.GetComponent<IEnemyBehaviourComp>().GetZombieID() == argid)
            //{
            //    ZombieToReturn = go;
            //}

        }
        return ZombieToReturn;
    }


    public GameObject GetLiveEnemyProjectilesByID(int argID)
    {
        GameObject EnemyPRojectileToReturn = null;
        foreach (GameObject go in EnemyProjectiles)
        {
            if (go.GetComponent<EnemySpinnerProjectile>().GetAxeId() == argID)
            {
                EnemyPRojectileToReturn = go;
            }

        }
        return EnemyPRojectileToReturn;

    }

    public GameObject GetLiveAXEMANByID(int argID)
    {
        GameObject AxmanToReturn = null;
        foreach (GameObject go in Axenemies)
        {
            //if (go.GetComponent<IEnemyBehaviourComp>().GetZombieID() == argID)
            //{
            //    AxmanToReturn = go;
            //}

        }
        return AxmanToReturn;

    }


    float ShortestDistToPlayer = float.MaxValue;
    int indexOfClossesZombie = 0;
    public IEnemyEntityComp ClosesZombieTOPlayer()
    {
        indexOfClossesZombie = 0;
        for (int zcnt = 0; zcnt < liveenemies.Count; zcnt++)
        {
            float tempDist = Vector3.Distance(liveenemies[zcnt].transform.position, GameManager.Instance.GetStaticPLayerPosition());
            if (tempDist < ShortestDistToPlayer)
            {
                ShortestDistToPlayer = tempDist;
                indexOfClossesZombie = zcnt;
            }
        }
        return liveenemies[indexOfClossesZombie].GetComponent<IEnemyEntityComp>();
    }

    void SpeedUpLiveZombies()
    {

        foreach (GameObject liveZombie in liveenemies)
        {
        }
    }
    void IHeardAshot()
    {
        if (GameManager.Instance.KngGameState == ARZState.ReachedAllowedTime)
        {
            return;
        }

        if (GameManager.Instance.KngGameState == ARZState.WaveBuffer)
        {
            return;
        }

        if (GameManager.Instance.KngGameState == ARZState.WaveEnded)
        {
            return;
        }


    }

    //void SetState(EnemyAnimatorState argZstate)
    //{
    //    Debug.LogError("use agro up down now");
    //    //foreach (GameObject liveZombie in liveenemies)
    //    //{
    //    //    if (!liveZombie.GetComponent<ZombieBehavior>().IsBossLevel_1 && !liveZombie.GetComponent<ZombieBehavior>().IsBossLevel_2 && !liveZombie.GetComponent<ZombieBehavior>().IsBossLevel_3)
    //    //    {
    //    //        // GameManager.Instance.DO_OVERRIDE_THISZOMB_PATH(liveZombie.GetComponent<ZombieBehavior>());
    //    //        liveZombie.GetComponent<ZombieBehavior>().CurZombieState = argZstate;

    //    //    }
    //    //}
    //}



    //public void AWAKEALLandWALK()
    //{
    //    if (GameManager.Instance.KngGameState == ARZState.Pregame)
    //    {
    //        SetState(EnemyAnimatorState.WALKING);
    //    }
    //}

    public void SetIncreaseagro()
    {
        foreach (GameObject liveZombie in liveenemies)
        {
            liveZombie.GetComponent<IEnemyEntityComp>().SPEEDUPENEMY_LIVE();
        }
    }

    public void DisolveAllZombiesOnPlayerDeath()
    {
        Debug.Log("DisolveAllZombiesOnPlayerDeath");
        PausLiveZombies(true);
        //  ZombieIndicatorManager.Instance.RemoveAllIndicators();
        StartCoroutine(Wait3secondsRemoveAll());

        foreach (GameObject liveZombie in liveenemies)
        {
            liveZombie.GetComponent<IEnemyEntityComp>().Shutthefuckup();
            liveZombie.GetComponent<IEnemyEntityComp>().Kill_CurrMODE(); //this hapens when zombie dies by the gun. not by reset, so use this method to kill cur mode
            liveZombie.GetComponent<IEnemyMeshComp>().MeshDisolveToNothing();
        }
    }
    void ResolveAllZombies() { }

#if ENABLE_KEYBORADINPUTS // Skip Don't Destroy On Load when editor isn't playing so test runner passes.
    bool lightonoff;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (liveenemies.Count > 1)
            {
                int rnd = Random.Range(0, liveenemies.Count);
                Transform targ = liveenemies[rnd].transform;
                GameEventsManager.Instance.CAll_SpotLightTarget(0, targ);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            lightonoff = !lightonoff;
            GameEventsManager.Instance.CAll_SpotLightOnOff(0, lightonoff);
        }


    }
#endif


    IEnumerator Wait3secondsRemoveAll()
    {
        //liveZombie.GetComponent<IEnemyEntityComp>().Kill_CurrMODE(); //this hapens when zombie dies by the gun. not by reset, so use this method to kill cur mode
        yield return new WaitForSeconds(3);
        RESETLiveZombies();

    }
}
