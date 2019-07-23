using UnityEngine;

public class Laz0rCTRL : MonoBehaviour
{
    //   var layer1 : int = 3;
    //var layer2 : int = 5;
    //var layermask1 : int = 1 << layer1;
    //var layermask2 : int = 1 << layer2;
    //var finalmask : int = layermask1 | layermask2; // Or, (1 << layer1) | (1 << layer2)
    public LineRenderer LR;
    public RaycastHit hitInfo;
    LayerMask RaycastLayerMask = 1 << 9;// Physics.DefaultRaycastLayers;
    int Layer9 = 9;
    int Layer8 = 8;
    int Layer31 = 31;

    LayerMask maskEnemy = 1 << 9;
    LayerMask maskSpatial = 1 << 31;
    LayerMask maskTarget = 1 << 17;

    LayerMask finalmask = (1 << 9) | (1 << 31) | (1 << 17) | (1 << 8);
    float MaxViewDist = 60f;
    float AdjustedSquaredMaxViewDist = 3400f; //a bit less than maxiew * maxview
    bool hitsomething = false;
    public GameObject RedDot;
    GameObject reddotVisibleSpriteObj;
    //  Vector3 RedDot_originalLoc;
    public GameObject RedSphere;
    //    Vector3 RedSphere_originalLoc;
    int randid = 0;
    private void Awake()
    {
        randid = Random.Range(0, 100);
        reddotVisibleSpriteObj = RedDot.transform.GetChild(0).gameObject;
        RedSphere.transform.parent = RedDot.transform;
    }
    public Transform RedDotBase;
    IEnemyEntityComp CURieec;

    //private void Start()
    //{
    //    RedDot_originalLoc = RedDotBase.position;
    //    RedSphere_originalLoc = RedDotBase.position;
    //}

    public void ToggleRedLaserRenderer(bool argOnOff, bool redDotSpriteOnOf)
    {
        LR.gameObject.SetActive(argOnOff);
        reddotVisibleSpriteObj.SetActive(redDotSpriteOnOf);
    }

    void CheckSightEnemyV2()
    {
        if (Physics.Raycast(transform.position, transform.forward * -1, out hitInfo, MaxViewDist, finalmask))
        {

            hitsomething = true;
            //RedDot.SetActive(true);
            RedDot.transform.position = new Vector3(hitInfo.point.x, hitInfo.point.y, hitInfo.point.z);
            float dist = Vector3.Distance(hitInfo.point, this.transform.position);
            // Debug.Log("hit dist" + dist);
            LR.SetPosition(1, new Vector3(0, 0, dist * -1));
        }
        else
        {
            hitsomething = false;
            RedSphere.transform.localScale = new Vector3(.01f, .01f, .01f);
        }
    }

    // int DistFromBase = 0;
    float distPercentage;
    private void Update()
    {
        // Vector3 dist = RedSphere.transform.position - RedDotBase.position;
        //DistFromBase = Mathf.RoundToInt(Vector3.SqrMagnitude(RedSphere.transform.position - RedDotBase.position));

        distPercentage = 1.02f - (AdjustedSquaredMaxViewDist - Vector3.SqrMagnitude(RedSphere.transform.position - RedDotBase.position)) / (AdjustedSquaredMaxViewDist) + 0.01f;

        if (distPercentage < 0.01f) { distPercentage = 0.01f; }
        else
        if (distPercentage > 1.0f) { distPercentage = 1f; }
        RedSphere.transform.localScale = new Vector3(distPercentage, distPercentage, distPercentage);
        // Debug.Log(randid.ToString() + " d= " + distPercentage.ToString());


        CheckSightEnemyV2();
        if (!hitsomething)
        {
            LR.SetPosition(1, new Vector3(0, 0, -400));
            RedDot.transform.position = RedDotBase.position;
            //RedDot.SetActive(false);
            CurEnemy_unAim();
        }
        else
        {
            CheckIfSAMEAimAtEnemy(hitInfo);
        }
    }



    #region AIMING
    void CheckIfSAMEAimAtEnemy(RaycastHit rch)
    {
        if (rch.collider.gameObject.layer == Layer9)
        {

            if (hitInfo.collider.gameObject.CompareTag("EnemyProjectile")) return;




            if (CURieec == null)
            {

                CURieec = hitInfo.collider.gameObject.GetComponentInParent<IEnemyEntityComp>();
                if (CURieec != null)
                {
                    CURieec.SetMainEnemyIsAmedAt(true);
                }


            }
            else
            {
                if (CURieec.Get_ID() != hitInfo.collider.gameObject.GetComponentInParent<IEnemyEntityComp>().Get_ID())
                {
                    CURieec.SetMainEnemyIsAmedAt(false);
                    CURieec = hitInfo.collider.gameObject.GetComponentInParent<IEnemyEntityComp>();
                    CURieec.SetMainEnemyIsAmedAt(true);
                }



            }
        }
    }

    void CurEnemy_unAim()
    {

        if (CURieec == null)
        {
            //DO NOTHING
            // CURieec = hitInfo.collider.gameObject.GetComponentInParent<IEnemyEntityComp>();
            // CurrIECID = CURieec.Get_ID();
        }
        else
        {
            //if (CURieec.GetMainEnemyIsAimedAt())
            // {
            CURieec.SetMainEnemyIsAmedAt(false);
            // CURieec.AimEnemy(false);
            //GameEventsManager.Instance.CallEnemyIsAmied(CURieec.Get_ID(), false);
            //Debug.Log("un aiming");
            //}
            CURieec = null;

        }
    }
    #endregion


    void CurEnemy_aim_thennull()
    {
        if (CURieec == null)
        {
            //DO NOTHING
            // CURieec = hitInfo.collider.gameObject.GetComponentInParent<IEnemyEntityComp>();
            // CurrIECID = CURieec.Get_ID();
        }
        else
        {
            if (!CURieec.GetMainEnemyIsAimedAt())
            {
                CURieec.SetMainEnemyIsAmedAt(true);
                // CURieec.AimEnemy(false);
                //GameEventsManager.Instance.CallEnemyIsAmied(CURieec.Get_ID(), false);
                //Debug.Log("un aiming");
            }
            CURieec = null;

        }

    }
}

/* if (hitInfo.collider.gameObject.CompareTag("EnemyProjectile")) return;
            if (CURieec == null)
            {
                CURieec = hitInfo.collider.gameObject.GetComponentInParent<IEnemyEntityComp>();
                //CURieec.AimEnemy(true);
                if (CURieec == null) { Debug.LogError("badguy->" + hitInfo.collider.gameObject.name); }
                else
                {

                    if (!CURieec.GetMainEnemyIsAimedAt())
                    {

                        CURieec.SetMainEnemyIsAmedAt(true);

                    }

                }
                // Debug.Log("aming new enemy");
            }
            else
            {
                if (rch.collider.gameObject.GetComponentInParent<IEnemyEntityComp>().Get_ID() == CURieec.Get_ID())
                {
                    //still same enemy 
                    //  Debug.Log("aming Same Enemy");
                    if (CURieec == null) Debug.LogError("no ieec aimed lazor");
                }
                else
                {
                    //CURieec.AimEnemy(false);
                    GameEventsManager.Instance.CallEnemyIsAmied(CURieec.Get_ID(), false);



                    CURieec.SetMainEnemyIsAmedAt(false);



                    CURieec = null;
                    CURieec = hitInfo.collider.gameObject.GetComponentInParent<IEnemyEntityComp>();
                    if (CURieec == null) Debug.LogError("no ieec aimed lazor");
                    else
                    {
                        if (!CURieec.GetMainEnemyIsAimedAt())
                        {

                            CURieec.SetMainEnemyIsAmedAt(true);

                        }
                    }

                    //CURieec.AimEnemy(true);
                    //  Debug.Log("aming CHANGED Enemy");
                }

            }*/

/*
     void CheckSightEnemy()
    {
        if (Physics.Raycast(transform.position, transform.forward * -1, out hitInfo, 30, RaycastLayerMask))
        {
             
                hitsomething = true;
                RedDot.SetActive(true);
                RedDot.transform.position = new Vector3(hitInfo.point.x, hitInfo.point.y, hitInfo.point.z - 0.01f);
                // RedDot.transform.position = (hitInfo.point - transform.position).normalized * .99f;


                float dist = Vector3.Distance(hitInfo.point, this.transform.position);
                // Debug.Log("hitZombie" + dist);
                LR.SetPosition(1, new Vector3(0, 0, dist * -1));    
        }
        else
            hitsomething = false;

    }


    void CheckSight()
    {
        if (Physics.Raycast(transform.position, transform.forward * -1, out hitInfo, 100, RaycastLayerMask))
        {
            if (hitInfo.collider.gameObject.layer == enemyLayer)
            {
                hitsomething = true;
                RedDot.SetActive(true);
                RedDot.transform.position = new Vector3(hitInfo.point.x, hitInfo.point.y, hitInfo.point.z - 0.01f);
                // RedDot.transform.position = (hitInfo.point - transform.position).normalized * .99f;


                float dist = Vector3.Distance(hitInfo.point, this.transform.position);
                // Debug.Log("hitZombie" + dist);
                LR.SetPosition(1, new Vector3(0, 0, dist * -1));

                // RedDot.transform.position = (hitInfo.point - transform.position).normalized * (dist - 0.01f);


            }
            //else
            //if (hitInfo.collider.gameObject.layer == SpatialMAp)
            //{
            //    hitsomething = true;
            //    RedDot.SetActive(true);
            //    RedDot.transform.position = new Vector3(hitInfo.point.x, hitInfo.point.y, hitInfo.point.z - 0.01f);

            //    float dist = Vector3.Distance(hitInfo.point, this.transform.position);
            //    Debug.Log("mesh" );
            //    LR.SetPosition(1, new Vector3(0, 0, dist * -1));

            //}


        }
        else
            hitsomething = false;



    }

 */
