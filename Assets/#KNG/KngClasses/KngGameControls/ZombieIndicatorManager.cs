//#define ENABLE_KEYBORADINPUTS
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ZombieIndicatorManager : MonoBehaviour {

    public static ZombieIndicatorManager Instance=null;
    EnemiesManager _nmymngr;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            _nmymngr = GetComponent<EnemiesManager>();
        }
        else
            Destroy(gameObject);
    }
    public  GameObject IndicatorPrefab;
    public GameObject FlyIndicatorPrefab;

    List<GameObject> IndicatorsInGame = new List<GameObject>();
    List<GameObject> IndicatorsAXEmenInGame = new List<GameObject>();
    public  List<GameObject> FlyIndicatorsInGame = new List<GameObject>();
    public List<GameObject> EnemyProjectileIndicatorsInGame = new List<GameObject>();

    // Use this for initialization
    void Start () {
		
	}

   


    public void IndicateAXEMANZombieID(int argID,GameObject AxeHead)
    {
        GameObject Zhead = null;
        GameObject Ztarg = _nmymngr.GetLiveAXEMANByID(argID);
        if (Ztarg != null)
        {
            Zhead = AxeHead;
        }

        if (Zhead != null)
        {
            GameObject _indicator = Instantiate(IndicatorPrefab);
            _indicator.GetComponent<ZombieIndicator>().TargetObject = Zhead;
            _indicator.GetComponent<ZombieIndicator>().Zid = Ztarg.GetComponent<IZBehavior>().GetZombieID();
            IndicatorsAXEmenInGame.Add(_indicator);
        }
    }
 

    public void IndicateEnemyProjectileID(int argID)
    {
        GameObject Ztarg = _nmymngr.GetLiveEnemyProjectilesByID(argID);
        if (Ztarg != null)
        {
            GameObject _EPindicator = Instantiate(FlyIndicatorPrefab);
            _EPindicator.GetComponent<ZombieIndicator>().TargetObject = Ztarg;
            _EPindicator.GetComponent<ZombieIndicator>().Fid = Ztarg.GetComponent<EnemySpinnerProjectile>().GetAxeId();
            EnemyProjectileIndicatorsInGame.Add(_EPindicator);
        }
    }


    //IndicatorsAXEmenInGame


    public void RemoveIndicateZombieID(int argID)
    {
        if (IndicatorsInGame == null) return;
        if (IndicatorsInGame.Count < 1) return;
        GameObject temp = IndicatorsInGame.FirstOrDefault(i => i.GetComponent<ZombieIndicator>().Zid == argID);

        if (temp != null)
        {
            IndicatorsInGame.Remove(IndicatorsInGame.Single(i => i.GetComponent<ZombieIndicator>().Zid == argID));
            Destroy(temp);
        }
    }

    public void RemoveIndicateAXEZombieID(int argID)
    {
        if (IndicatorsInGame == null) return;
        if (IndicatorsInGame.Count < 1) return;
        GameObject temp = IndicatorsInGame.FirstOrDefault(i => i.GetComponent<ZombieIndicator>().Zid == argID);

        if (temp != null)
        {
            IndicatorsInGame.Remove(IndicatorsInGame.Single(i => i.GetComponent<ZombieIndicator>().Zid == argID));
            Destroy(temp);
        }
    }
 
    public void RemoveIndicateEnemyProjectileID(int argID)
    {
        if (EnemyProjectileIndicatorsInGame == null) return;
        if (EnemyProjectileIndicatorsInGame.Count < 1) return;
        GameObject temp = EnemyProjectileIndicatorsInGame.FirstOrDefault(i => i.GetComponent<ZombieIndicator>().Pid == argID);

        if (temp != null)
        {
            EnemyProjectileIndicatorsInGame.Remove(EnemyProjectileIndicatorsInGame.Single(i => i.GetComponent<ZombieIndicator>().Pid == argID));
            Destroy(temp);
        }
    }


    public void RemoveAllIndicators()
    {
        if (IndicatorsInGame == null) return;
        if (IndicatorsInGame.Count < 1) return;
        for (int i = IndicatorsInGame.Count-1; i >=0; i--) {
            GameObject temp = IndicatorsInGame[i];
            if (temp != null)
            {
                IndicatorsInGame.Remove(IndicatorsInGame[i]);
                Destroy(temp);
            }
        }


        if (FlyIndicatorsInGame == null) return;
        if (FlyIndicatorsInGame.Count < 1) return;
        for (int i = FlyIndicatorsInGame.Count - 1; i >= 0; i--)
        {
            GameObject temp = FlyIndicatorsInGame[i];
            if (temp != null)
            {
                FlyIndicatorsInGame.Remove(FlyIndicatorsInGame[i]);
                Destroy(temp);
            }
        }

        if (EnemyProjectileIndicatorsInGame == null) return;
        if (EnemyProjectileIndicatorsInGame.Count < 1) return;
        for (int i = EnemyProjectileIndicatorsInGame.Count - 1; i >= 0; i--)
        {
            GameObject temp = EnemyProjectileIndicatorsInGame[i];
            if (temp != null)
            {
                EnemyProjectileIndicatorsInGame.Remove(EnemyProjectileIndicatorsInGame[i]);
                Destroy(temp);
            }
        }

        GameObject[] LeftoverIndicatore = GameObject.FindGameObjectsWithTag("Indicator");
        for (var i = 0; i < LeftoverIndicatore.Length; i++)
        {
            Destroy(LeftoverIndicatore[i]);
        }

    }

    bool IndicatorAlreadyExists(int argID) {
        foreach (GameObject go in IndicatorsInGame) {
            if (go.GetComponent<ZombieIndicator>().Zid == argID) return true;
        }
        return false;
    }


    int axeid = 0;
#if ENABLE_KEYBORADINPUTS
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            GameEventsManager.Instance.Call_OtherHLSmallAttack();
            GameObject ax = Instantiate(Axe, Here.position, Quaternion.identity);
            ax.GetComponent<AxeBehavior>().SetAxeID(axeid);
            axeid++;
        }

        if (Input.GetKeyDown(KeyCode.Delete))
        {
            if (IndicatorAlreadyExists(2))
                RemoveIndicateZombieID(2);
        }
    }
#endif
    public GameObject Axe;
    public Transform Here;
}
