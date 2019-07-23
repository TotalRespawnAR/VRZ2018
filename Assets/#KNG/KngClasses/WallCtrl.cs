//#define ENABLE_KEYBORADINPUTS
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCtrl : MonoBehaviour {

    private void OnEnable()
    {
       // GameManager.OnTargExplode += DOIT;
    }
    private void OnDisable()
    {
      //  GameManager.OnTargExplode -= DOIT;
    }

    public List<GameObject> WallParts;
    public Material HoloMat0_Diss_Light;
    public List<Material> PArtsMats;
    public List<MeshRenderer> PArtsREN;


    float _dissolveFactor = 0.0f;
    float _elapsedTime = 0f;

    ////float _lerp_From_value = 0.0f;
    ////float _lerp_To_value = 1.0f;
    private void ResetLerpValues()
    {
        _dissolveFactor = 0.0f;
        _elapsedTime = 0f;

    }

    IEnumerator Lerp_dissolveFactor()
    {
        _elapsedTime = 0f;

        while (_elapsedTime < 4f)
        {
            _dissolveFactor = Mathf.Lerp(0, 1, (_elapsedTime / 4f));
            _elapsedTime += Time.deltaTime;
            yield return null;
        }
    }



    IEnumerator DISSABLEMEINAfew()
    {
       
            yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
      
    }
    public void DISOLVEME()
    {

        ////StartDisolve = true;

        SetMaterial(HoloMat0_Diss_Light);
        StartCoroutine(Lerp_dissolveFactor());

        StartCoroutine(DISSABLEMEINAfew());


    }

    

    void SetMaterial(Material argmat)
    {

        //for (int x = 1; x < this.transform.childCount; x++)
        //{
        //    this.transform.GetChild(x).gameObject.GetComponent<MeshRenderer>().material=argmat;
        //}
        for (int x = 1; x < this.transform.childCount; x++)
        {
            WallParts[x].AddComponent<KillTimer>().StartTimer(4f);

        }
        foreach (MeshRenderer render in PArtsREN)
        {
            render.material = argmat;
            PArtsMats.Add(argmat);
        }

    }
    Vector3 torque;

    // Use this for initialization
    void Start () {
 
        WallParts = new List<GameObject>();
        PArtsMats = new List<Material>();
        PArtsREN = new List<MeshRenderer>();
        for (int x = 0; x < this.transform.childCount; x++) {
            WallParts.Add(this.transform.GetChild(x).gameObject);
        }
        for (int x = 1; x < this.transform.childCount; x++)
        {

            PArtsREN.Add(WallParts[x].GetComponent<MeshRenderer>());

        }
      
    }
    ////bool StartDisolve = false;
    // Update is called once per frame

    void DOIT()
    {
        if (GameSettings.Instance.PregmeType == ARZPregameType.OPENBACKROOM)
            return;
            for (int x = 1; x < this.transform.childCount; x++)
        {
            WallParts[x].AddComponent<ConstantForce>();
            WallParts[x].GetComponent<Rigidbody>().isKinematic = false;
            WallParts[x].GetComponent<Rigidbody>().AddForce(this.transform.up * Random.Range(2, 5) *-1, ForceMode.Impulse);
            torque.x = Random.Range(-100, 100);
            torque.y = Random.Range(-50, 50);
            torque.z = Random.Range(-100, 100);
            WallParts[x].GetComponent<ConstantForce>().torque = torque;
            StartCoroutine(DoDisolvein2sec());
        }
    }
#if ENABLE_KEYBORADINPUTS
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.K))
        {
            DOIT();
        }

        // if (StartDisolve)
        //  {

        foreach (Material m in PArtsMats)
        {
            m.SetFloat("_Dis", _dissolveFactor);
        }
        // }

        if (Input.GetKeyDown(KeyCode.F)) { BreakWallFromZombieRunner(); }
    }
#endif

    IEnumerator DoDisolvein2sec() {
        yield return new WaitForSeconds(1.2f);
        DISOLVEME();
    }

    bool wallisBroken = false;
    void BreakWallFromZombieRunner()
    {
        GameEventsManager.Instance.Call_WallBroke();
        GameManager.Instance.GM_Wake_PregameZombies();

        for (int x = 1; x < this.transform.childCount; x++)
        {
            WallParts[x].AddComponent<ConstantForce>();
            WallParts[x].GetComponent<Rigidbody>().isKinematic = false;
            WallParts[x].GetComponent<Rigidbody>().AddForce(this.transform.up * Random.Range(1.0f, 1.6f) * 0.6f, ForceMode.Impulse);
            //torque.x = Random.Range(-100, 100);
            //torque.y = Random.Range(-50, 50);
            //torque.z = Random.Range(-100, 100);

            torque.x = Random.Range(-10, 10);
            torque.y = Random.Range(-5, 5);
            torque.z = Random.Range(-5, 5);
            WallParts[x].GetComponent<ConstantForce>().torque = torque;
            StartCoroutine(DoDisolvein2sec());
        }
    }


    private void OnTriggerEnter(Collider other)
    {
       // Debug.Log("collided with " + other.tag);

        //if (other.CompareTag("ZombieTag"))
        //{
        //    if (wallisBroken) return;
        //    else
        //    {
        //        other.gameObject.GetComponent<ZombieAnimState>().TriggerDash();
        //        BreakWallFromZombieRunner();
        //        wallisBroken = true;
        //    } //    GrenadeExplode(GameManager.Instance.GEtAllives());
        //    //    Instantiate(Explosion, transform.localPosition, Quaternion.identity);

        //    }
    }

 
}
