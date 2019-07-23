using UnityEngine;

public class MagazineMNGR : MonoBehaviour
{



    public Transform GunClipLocation;
    public GameObject FunctionalMagModel;
    //cant unpub , it is used by gun.cs 
    public IMag curMagInGun;
    //unbub
    public GameObject _curActiveMagInstance;
    ////GameObject _ACTIVE_UI = null;
    //unpub
    // public GameObject _CoppyLinkedOfActivereload;//linked when bundel instantiates us 
    ////activereloadUIctrl _ActivreReloadScript;



    // Just refil the mag , not used 
    public void Refill_CurMag()
    {

        curMagInGun.Refill();
    }
    //*******************************************MAG_MANAGEMENT***************************************
    public bool IsMagPlaced()
    {
        if (_curActiveMagInstance == null)
        {
            return false;
        }
        return true;
    }



    //called from GUNFIRE
    public bool CanDecrementCurMagBulletCount(bool argSlowTimeOnOverride)
    {
        if (curMagInGun == null) { return false; }
        // Debug.Log("-1");
        TestCurMagValidity();
        return curMagInGun.TryDecrementBulletCount(argSlowTimeOnOverride);
    }


    //called from GUNFIRE
    public GameObject GetChamberedBullet()
    {
        //if (curMagInGun == null) { return null; }
        TestCurMagValidity();
        return curMagInGun.GetBulletFromMag();
    }

    //caled from anybulletsandcanireload called from gunfire
    public bool IsThereBulletsInCurmag()
    {
        // Debug.Log("-3 checking f bullets");
        // if (curMagInGun == null) { return false; }
        TestCurMagValidity();
        if (curMagInGun == null) return false;

        if (curMagInGun.GetBulletsCount_inMag() > 0) return true;
        else
            return false;
    }


    //gun.start with full clip...
    public void InstantiateMagInPlace(/*bool _argShowcase*/)
    {

        if (_curActiveMagInstance == null)
        {
            _curActiveMagInstance = Instantiate(FunctionalMagModel, GunClipLocation.position, GunClipLocation.rotation);
            _curActiveMagInstance.transform.parent = GunClipLocation;
            curMagInGun = _curActiveMagInstance.GetComponent<Mag>();
            //if (_argShowcase) curMagInGun.DissableMAgRendered();
        }
    }



    //public bool IsThere_ONELAST_BulletsInCurmag()
    //{
    //    // Debug.Log("-3 checking f bullets");
    //    TestCurMagValidity();
    //    if (curMagInGun == null) return false;

    //    if (curMagInGun.GetBulletsCount_inMag() == 1) return true;
    //    else
    //        return false;
    //}


    void TestCurMagValidity()
    {
        if (curMagInGun == null) { Debug.Log("no mag!"); return; }
    }
    //*******************************************xMAG_MANAGEMENT**************************************


    void InitClipInTrans()
    {
        GunClipLocation = gunhelper.DeepSearchContain(this.transform, "_ClipIn");
        if (GunClipLocation == null)
        {
            Logger.Debug("_ClipIn not found ");
            GunClipLocation = this.transform;
        }
    }



    void Awake() { InitClipInTrans(); }


    // tracking meter //stemplayerctrl.ItPutsGunInHand or maginhand ->  handscript.ANYHAD_EQUIP  -> takes curIgun.GUNLINK_meter
    // bundle includes public meterobj -> gun  
    //public void GUNRELOAD_LINK_RELOAD_METER(GameObject go)
    //{
    //    _CoppyLinkedOfActivereload = go;
    //}
    // Use this for initialization


    // Update is called once per frame
    //void Update()
    //{

    //}

    //    |
    //    |
    //    |
    //    V
    //ANIMATIONEVENTLISTENER
    //public void OnSlideOutAnimComplete()
    //{
    //    GunPopClipOut();
    //    Logger.Debug("yo i heard on slid complete and am not on animatorscript");
    //    //ready to accept a clip
    //    //_myGunBools.BmagIn = false; _myGunBools.BHazBullets = false; _myGunBools.BisReloading = false; //_myGunBools.CanAcceptNewClip = true;
    //}
    public void MAGmngerDropRigidMAg()
    {
        if (_curActiveMagInstance != null)
        {
            GameObject rigidclip = _curActiveMagInstance;
            curMagInGun = null;
            _curActiveMagInstance = null;

            rigidclip.transform.parent = null;
            DestroyObject(_curActiveMagInstance);
            rigidclip.AddComponent<Rigidbody>();
            rigidclip.GetComponent<Rigidbody>().AddForce(GunClipLocation.forward * -2, ForceMode.Impulse);
            //rigidclip.GetComponent<Mag>().InitCanPlayCollisionSound();
            KillTimer t = rigidclip.AddComponent<KillTimer>();
            t.StartTimer(5.0f);
        }

        //_myGunBools.BmagIn = false;

        ////        _myGunBools.BmagIn = false; _myGunBools.BHazBullets = false; _myGunBools.BisReloading = false;
        //_myGunBools.CanAcceptNewClip = true;
    }

    //
    //    |
    //    |
    //    |
    //    V
    // IF COLLISION AND HAD IS FULL OF AMMO 

    public void InstantiatFunctionalMagObjOnBone()
    {
        if (_curActiveMagInstance == null)
        {
            //  Debug.Log("NOW i got a mag");
            _curActiveMagInstance = Instantiate(FunctionalMagModel, GunClipLocation.position, GunClipLocation.rotation);
            _curActiveMagInstance.transform.parent = GunClipLocation;
            curMagInGun = _curActiveMagInstance.GetComponent<Mag>();
        }
        else
        {
            Debug.Log("Wait whaaa, i already have a mag?");
        }
    }





}
