// @Author Nabil Lamriben ©2018
using UnityEngine;

public class MagsBundle : MonoBehaviour, IBundle
{

    public GameObject magM1911;
    public GameObject magMac11;
    public GameObject magColt;
    public GameObject magShotgun;
    public GameObject magP90;
    public GameObject magMG61;
    public GameObject magHell;
    public GameObject magTempFlareG;




    GameObject MagazinInstanceM1911;
    GameObject MagazinInstanceMac11;
    GameObject MagazinInstanceColt;
    GameObject MagazinInstanceShotgun;
    GameObject MagazinInstanceP90;
    GameObject MagazinInstanceMG61;
    GameObject MagazinInstanceHell;
    GameObject MagazinInstanceTempFlareG;





    private bool _isCurMagVisible;

    GameObject _curMagObject;

    private void Awake()
    {

        MagazinInstanceM1911 = Instantiate(magM1911, this.transform.position, this.transform.rotation);
        MagazinInstanceM1911.name = "Mag_M1911";
        MagazinInstanceM1911.transform.parent = this.transform;

        MagazinInstanceMac11 = Instantiate(magMac11, this.transform.position, this.transform.rotation);
        MagazinInstanceMac11.name = "Mag_Mac11";
        MagazinInstanceMac11.transform.parent = this.transform;

        MagazinInstanceColt = Instantiate(magColt, this.transform.position, magColt.transform.rotation);
        MagazinInstanceColt.name = "Mag_Colt";
        MagazinInstanceColt.transform.parent = this.transform;

        MagazinInstanceShotgun = Instantiate(magShotgun, this.transform.position, this.transform.rotation);
        MagazinInstanceShotgun.name = "Mag_Shotgun";
        MagazinInstanceShotgun.transform.parent = this.transform;

        MagazinInstanceP90 = Instantiate(magP90, this.transform.position, this.transform.rotation);
        MagazinInstanceP90.name = "Mag_P90";
        MagazinInstanceP90.transform.parent = this.transform;

        MagazinInstanceMG61 = Instantiate(magMG61, this.transform.position, this.transform.rotation);
        MagazinInstanceMG61.name = "Mag_MG61";
        MagazinInstanceMG61.transform.parent = this.transform;

        MagazinInstanceHell = Instantiate(magHell, this.transform.position, this.transform.rotation);
        MagazinInstanceHell.name = "Mag_Hell";
        MagazinInstanceHell.transform.parent = this.transform;

        MagazinInstanceTempFlareG = Instantiate(magTempFlareG, this.transform.position, this.transform.rotation);
        MagazinInstanceTempFlareG.name = "Mag_FlareG";
        MagazinInstanceTempFlareG.transform.parent = this.transform;

        HideAllMyThings();

    }


    ////stemkitmanager.start() -> playerHandsCTRL.INit()
    public void Set_CurrAmmoObject_Visible(bool argVisible)
    {        // make equipped clip active

        if (_curMagObject == null) return;
        _isCurMagVisible = argVisible;
        _curMagObject.SetActive(argVisible);
        if (_curMagObject != null)
        {
            _curMagObject.SetActive(argVisible);
        }
        else { Debug.LogWarning("no curr mag Onject "); }
    }


    #region InterfaceRegion

    public bool IsMyThingShowing()
    {
        if (_curMagObject == null)
        {
            // Debug.Log("problem , no cur");
            return false;
        }
        else
        if (!_isCurMagVisible)
        {
            // Debug.Log("no problem , no vis");
            return false;
        }
        else
            return true;
    }

    public void HideAllMyThings()
    {
        MagazinInstanceM1911.SetActive(false);
        MagazinInstanceMac11.SetActive(false);
        MagazinInstanceColt.SetActive(false);
        MagazinInstanceShotgun.SetActive(false);
        MagazinInstanceP90.SetActive(false);
        MagazinInstanceMG61.SetActive(false);
        MagazinInstanceHell.SetActive(false);
        MagazinInstanceTempFlareG.SetActive(false);


    }

    public void SetMyCurrBunThing(GunType argguntype)
    {
        if (_curMagObject != null)
        {
            _curMagObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("tried to equip but no weapon was found");
        }
        //// equip new weapon
        switch (argguntype)
        {
            case GunType.PISTOL:
                _curMagObject = MagazinInstanceM1911;
                break;
            case GunType.MAGNUM:
                _curMagObject = MagazinInstanceColt;
                break;
            case GunType.UZI:
                _curMagObject = MagazinInstanceMac11;
                break;
            case GunType.SHOTGUN:
                _curMagObject = MagazinInstanceShotgun;
                break;
            case GunType.P90:
                _curMagObject = MagazinInstanceP90;
                break;
            case GunType.MG61:
                _curMagObject = MagazinInstanceMG61;
                break;
            case GunType.HELL:
                _curMagObject = MagazinInstanceHell;
                break;
            case GunType.GRENADELAUNCHER:
                _curMagObject = MagazinInstanceTempFlareG;
                break;
        }
    }


    public void ShowMyCurrBunThing()
    {
        Set_CurrAmmoObject_Visible(true);
    }

    public void HideMyCurrBunThing()
    {
        Set_CurrAmmoObject_Visible(false);
    }


    #endregion


}
