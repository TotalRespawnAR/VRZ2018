// @Author Nabil Lamriben ©2018
using UnityEngine;

public class GunsBundle : MonoBehaviour, IBundle
{

    public GameObject gunM1911;
    public GameObject gunMac11;
    public GameObject gunColt;
    public GameObject gunShotgun;
    public GameObject gunP90;
    public GameObject gunMG61;
    public GameObject gunHell;
    public GameObject gunFlareG;
    public GameObject TheCtrl;


    GameObject M1911;
    GameObject Mac11;
    GameObject Colt;
    GameObject Shotgun;
    GameObject P90;
    GameObject MG61;
    GameObject Hell;
    GameObject FlareG;


    bool _isEquipedGunVisible;
    GameObject _curGunObject;
    IGun CurGunScript;

    public IGun GetActiveGunScript() { return CurGunScript; }



    private void Awake()
    {
        M1911 = Instantiate(gunM1911, this.transform.position, this.transform.rotation);
        M1911.name = "Gun_M1911";
        M1911.transform.parent = this.transform;

        Mac11 = Instantiate(gunMac11, this.transform.position, this.transform.rotation);
        Mac11.name = "Gun_Mac11";
        Mac11.transform.parent = this.transform;

        Colt = Instantiate(gunColt, this.transform.position, this.transform.rotation);
        Colt.name = "Gun_Colt";
        Colt.transform.parent = this.transform;

        Shotgun = Instantiate(gunShotgun, this.transform.position, this.transform.rotation);
        Shotgun.name = "Gun_Shotgun";
        Shotgun.transform.parent = this.transform;



        P90 = Instantiate(gunP90, this.transform.position, this.transform.rotation);
        P90.name = "Gun_P90";
        P90.transform.parent = this.transform;


        MG61 = Instantiate(gunMG61, this.transform.position, this.transform.rotation);
        MG61.name = "Gun_MG61";
        MG61.transform.parent = this.transform;

        Hell = Instantiate(gunHell, this.transform.position, this.transform.rotation);
        Hell.name = "Gun_Hell";
        Hell.transform.parent = this.transform;

        FlareG = Instantiate(gunFlareG, this.transform.position, this.transform.rotation);
        FlareG.name = "Gun_FlareG";
        FlareG.transform.parent = this.transform;


        HideAllMyThings();
        SetMyCurrBunThing(GunType.P90);
        ShowMyCurrBunThing();
    }




    #region InterfaceRegion

    public bool IsMyThingShowing()
    {
        if (_curGunObject == null) { return false; }
        if (!_isEquipedGunVisible) { return false; }
        return true;
    }

    public void HideAllMyThings()
    {
        M1911.SetActive(false);
        Colt.SetActive(false);
        Mac11.SetActive(false);
        Shotgun.SetActive(false);
        P90.SetActive(false);
        MG61.SetActive(false);
        Hell.SetActive(false);
        FlareG.SetActive(false);
    }


    // tracking meter //stemplayerctrl.ItPutsGunInHand or maginhand ->  handscript.ANYHAD_EQUIP  
    public void SetMyCurrBunThing(GunType argGuntype)
    {
        //unequip previous weapon
        if (CurGunScript != null)
        {
            CurGunScript.CancelSound();
            _curGunObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("tried to equip but no weapon was found");
        }
        switch (argGuntype)
        {
            case GunType.PISTOL:
                _curGunObject = M1911;
                break;
            case GunType.MAGNUM:
                _curGunObject = Colt;
                break;
            case GunType.UZI:
                _curGunObject = Mac11;
                break;
            case GunType.SHOTGUN:
                _curGunObject = Shotgun;
                break;
            case GunType.P90:
                _curGunObject = P90;
                break;
            case GunType.MG61:
                _curGunObject = MG61;
                break;
            case GunType.HELL:
                _curGunObject = Hell;
                break;
            case GunType.GRENADELAUNCHER:
                _curGunObject = FlareG;
                break;

        }
        // _curGunObject.GetComponent<Gun>().Gun_MAgic_MAg_Refill();

        if (_curGunObject != null)
        {
            CurGunScript = _curGunObject.GetComponent<IGun>();
            //if (CurGunScript.GUN_GET_BOOLS().ThisGunIsReloading) {
            //    CurGunScript.FullReplacementOfMag_NoSOund();
            //};
        }
        else
        {
            Debug.LogWarning("no curr weapon Onject ");
        }
    }


    public void ShowMyCurrBunThing()
    {
        _isEquipedGunVisible = true;
        if (_curGunObject != null)
        {
            _curGunObject.SetActive(true);
        }
        else { Debug.LogWarning("no curr weapon Onject "); }
    }

    public void HideMyCurrBunThing()
    {
        Set_EquipedGunObject_Visible(false);
    }


    #endregion

    ////stemkitmanager.start() -> playerHandsCTRL.INit()
    public virtual void Set_EquipedGunObject_Visible(bool argVisible)
    {        // make equipped clip active
        _isEquipedGunVisible = argVisible;
        if (_curGunObject != null)
        {
            _curGunObject.SetActive(argVisible);
            GameEventsManager.Call_SetCurIgunTo(CurGunScript);
        }
        else { Debug.LogWarning("no curr weapon Onject "); }
    }

}
