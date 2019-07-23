using UnityEngine;

#pragma warning disable IDE1006 // Naming Styles
public class gunhelper : MonoBehaviour
{

    //#pragma warning restore IDE1006 // Naming Styles

    //#pragma warning disable IDE1006 // Naming Styles
    //    void putClipin() {
    //#pragma warning restore IDE1006 // Naming Styles

    //    }

    private Gun thegun;

    private void OnEnable()
    {
        GameEventsManager.StrikerShoot_Handeler += Shoot;
        GameEventsManager.StrikerDryFire_Handeler += DrySound;
        GameEventsManager.StrikerReload_Handeler += ReloadSound;
        GameEventsManager.OnToggleInputs += Handle_ToggleAllowStemInputs;
    }
    private void OnDisable()
    {
        GameEventsManager.StrikerShoot_Handeler -= Shoot;
        GameEventsManager.StrikerDryFire_Handeler -= DrySound;
        GameEventsManager.StrikerReload_Handeler -= ReloadSound;
        GameEventsManager.OnToggleInputs += Handle_ToggleAllowStemInputs;
    }
    bool isPlayerAllowedToUseStemInput = false;
    void Handle_ToggleAllowStemInputs(bool argb)
    {

        isPlayerAllowedToUseStemInput = argb;
    }

    public void StartStopAutoShootShowcase(bool argstartstop)
    {
        thegun.StartStopAutoShootShowcase(argstartstop);
        if (!argstartstop)
        {
            thegun.GUN_STOP_FIRE();
        }
    }

    //______________________________________________________should be in ReloadMAnager along with gunreoad stats
    void EjectMag()
    {
        thegun.MANUAL_EJECT_MAG_OUT();
        thegun.AUDIO_PopMagOut();
    }


    void InjectMag()
    {
        thegun.GunInjstantiateMagANDSLIDEINanim();
        thegun.AUDIO_PopMagIn();
    }
    //______________________________________________________should be in ReloadMAnager along with gunreoad stats


    public void Shoot()
    {
        thegun.GUN_FIRE();
    }

    public void DrySound()
    {
        thegun.AUDIO_DryFire();
    }

    public void ReloadSound()
    {
        thegun.AUDIO_PopMagOut();
    }

    void StopShooting()
    {
        thegun.GUN_STOP_FIRE();
    }


    // Use this for initialization
    void Awake()
    {
        // putClipin();
        thegun = GetComponent<Gun>();
    }

    // Update is called once per frame
#if UNITY_EDITOR

    //void Update()
    //{
    //    if (!isPlayerAllowedToUseStemInput) return;

    //    // if (thegun.IsShowcase) { thegun.GUN_FIRE_SHOWCASE(Time.deltaTime); }
    //    if (Input.GetKeyDown(KeyCode.E)) { EjectMag(); }
    //    if (Input.GetKeyDown(KeyCode.I)) { InjectMag(); }
    //    //if (Input.GetKeyDown(KeyCode.R)) { thegun.FullReplacementOfMag(); }
    //    //if (Input.GetKeyDown(KeyCode.LeftControl)) { Shoot(); }
    //    //if (Input.GetKeyUp(KeyCode.LeftControl)) { StopShooting(); }
    //}
#endif




    public static Transform DeepSearch(Transform parent, string val)
    {
        // Debug.Log("on " + parent.gameObject.name);

        foreach (Transform c in parent)
        {
            if (c.name == val) { return c; }
            var result = DeepSearch(c, val);
            if (result != null)
                return result;
        }
        return null;
    }

    public static Transform DeepSearchContain(Transform parent, string val)
    {
        // Debug.Log("on " + parent.gameObject.name);

        foreach (Transform c in parent)
        {
            if (c.name.Contains(val)) { return c; }
            var result = DeepSearchContain(c, val);
            if (result != null)
                return result;
        }
        return null;
    }

}
