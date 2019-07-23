using System.Collections.Generic;
using UnityEngine;

public class WeaponShowcase : MonoBehaviour
{
    public GameObject Colt;
    public GameObject pistol;
    public GameObject Mac11;
    public GameObject Shotgun;
    public GameObject MG61;
    public GameObject P90;
    public GameObject GrenadeLancher;
    public GameObject HellboyGun;

    public Dictionary<GunType, GameObject> Gunhelpers;

    public void HIdeAll()
    {
        foreach (KeyValuePair<GunType, GameObject> gtg in Gunhelpers)
        {
            gtg.Value.SetActive(false);
            // gtg.Value.GetComponent<Gun>().StartStopAutoShootShowcase(false);
        }
    }

    GameObject TryGEtGO = null;

    public void Activategun(GunType argGUnType)
    {
        TryGEtGO = null;
        Gunhelpers.TryGetValue(argGUnType, out TryGEtGO);

        if (TryGEtGO != null)
        {
            HIdeAll();
            TryGEtGO.SetActive(true);
            GameEventsManager.Instance.Call_StartstopAutoshoot(true);
            // TryGEtGO.GetComponent<Gun>().StartStopAutoShootShowcase(true);
        }
        else
        {

        }
    }

    public int GET_shoedGun_MagCapacity()
    {
        if (TryGEtGO == null)
            return 0;
        else
        {
            MagazineMNGR mm = TryGEtGO.GetComponent<IGun>().GetGunMagMngr();
            return mm.FunctionalMagModel.GetComponent<Mag>().MagSize;
        }
    }
    public int GET_shoedGunBulletDamage()
    {
        if (TryGEtGO == null)
            return 0;
        else
        {
            if (TryGEtGO.GetComponent<Gun>().gunType == GunType.GRENADELAUNCHER)
            {
                return 100;
            }
            MagazineMNGR mm = TryGEtGO.GetComponent<IGun>().GetGunMagMngr();
            return mm.FunctionalMagModel.GetComponent<Mag>().MagBulletlet.GetComponent<Bullet>().damage;
        }
    }

    public string GET_shoedGun_FireType()
    {
        if (TryGEtGO == null)
            return "n/a";
        else
        {
            Gun selction = TryGEtGO.GetComponent<Gun>();
            if (selction.gunType == GunType.UZI || selction.gunType == GunType.MG61 || selction.gunType == GunType.P90)
            {
                return "FULL AUTO";
            }
            else
                return "semi auto";
        }
    }

    public string GET_shoedGun_Name()
    {
        string GunName = "n/a";
        if (TryGEtGO == null)
        {
            return GunName;

        }

        else
        {
            Gun selction = TryGEtGO.GetComponent<Gun>();
            switch (selction.gunType)
            {
                case GunType.MAGNUM:
                    GunName = "Colt Anaconda";
                    break;
                case GunType.PISTOL:
                    GunName = "M-1911 pistol";
                    break;
                case GunType.UZI:
                    GunName = "MAC 11";
                    break;
                case GunType.SHOTGUN:
                    GunName = "Shotgun";
                    break;
                case GunType.MG61:
                    GunName = "MG-61 Scorpion";
                    break;
                case GunType.P90:
                    GunName = "P-90";
                    break;
                case GunType.GRENADELAUNCHER:
                    GunName = "Grenade Launcher";
                    break;
                case GunType.HELL:
                    GunName = "Hellboy gun";
                    break;
            }

            return GunName;
        }
    }


    // Use this for initialization
    void Start()
    {

        GameObject TryGEtGO = null;
        Gunhelpers = new Dictionary<GunType, GameObject>();
        Gunhelpers.Add(GunType.MAGNUM, Colt);
        Gunhelpers.Add(GunType.PISTOL, pistol);
        Gunhelpers.Add(GunType.UZI, Mac11);
        Gunhelpers.Add(GunType.SHOTGUN, Shotgun);
        Gunhelpers.Add(GunType.MG61, MG61);
        Gunhelpers.Add(GunType.P90, P90);
        Gunhelpers.Add(GunType.GRENADELAUNCHER, GrenadeLancher);
        Gunhelpers.Add(GunType.HELL, HellboyGun);

        Gunhelpers.TryGetValue(GunType.MAGNUM, out TryGEtGO);

        foreach (KeyValuePair<GunType, GameObject> gtg in Gunhelpers)
        {
            gtg.Value.GetComponent<IGun>().UseScope(GunScopes.NONE);
        }
        HIdeAll();

    }

    // Update is called once per frame
    void Update()
    {
        //  if (Input.GetKeyDown(KeyCode.Alpha1)) { Activategun(GunType.MAGNUM); }
        // if (Input.GetKeyDown(KeyCode.Alpha2)) { Activategun(GunType.PISTOL); }
        //  if (Input.GetKeyDown(KeyCode.Alpha3)) { Activategun(GunType.UZI); }

        Rortateme();
    }


    void Rortateme()
    {        // Rotate the object around its local X axis at 1 degree per second
        //transform.Rotate(Time.deltaTime, 0, 0);

        // ...also rotate around the World's Y axis
        transform.Rotate(0, Time.deltaTime * 50, 0, Space.World);
    }
}
