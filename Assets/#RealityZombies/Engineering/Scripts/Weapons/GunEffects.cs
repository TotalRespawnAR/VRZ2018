using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class GunEffects : MonoBehaviour
{


    public FlashScript myflash;
    public CasingEjector Ejector;
    GunAttachementsManager gattm;
    GunType myType;
    AudioSource audioSource;
    AudioClip Clip_Fire;
    AudioClip Clip_MagOut;
    AudioClip Clip_fullreload;

    AudioClip Clip_MagIn;
    AudioClip Clip_Dry;

    public void FlashEffect()
    {
        myflash.Flash();
        // _uaudio.ModulateVolume("_Fire", this.gameObject, 1f);
        // _uaudio.PlayEvent("_Fire");
        audioSource.PlayOneShot(Clip_Fire);
        StemKitMNGR.CALL_VIBRATECONTROLLERG(100, 1f);
    }

    public void CasingEjectEffect()
    {
        //  print("casin");
        Ejector.EjectCasing();
    }



    public void AUDIO_PopMagOut()
    {
        audioSource.PlayOneShot(Clip_MagOut);
    }
    public void AUDIO_PushMagIn()
    {
        audioSource.PlayOneShot(Clip_MagIn);
    }
    public void AUDIO_Chamber()
    { //_uaudio.PlayEvent("_Hammer"); StemKitMNGR.CALL_VIBRATECONTROLLERG(200, 1f);
        audioSource.PlayOneShot(Clip_MagIn);
    }
    public void AUDIO_Dry()
    {
        audioSource.PlayOneShot(Clip_Dry);
    }
    public void AUDIO_FullReload()
    {// _uaudio.PlayEvent("_FullReload"); StemKitMNGR.CALL_VIBRATECONTROLLERG(500, 0.5f);
        audioSource.PlayOneShot(Clip_fullreload);
    }
    public void AUDIO_CancelALL()
    {
        if (audioSource != null)
        {
            //    _uaudio.CancelInvoke();
            audioSource.Stop();
        }
    }
    //   UAudioManager _uaudio;
    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        gattm = GetComponent<GunAttachementsManager>();
        myType = GetComponent<IGun>().GetGunType();
        // Resources.Load<AudioClip>("AudioClips/Guns/ColtAudio/PushMagIn_Colt");
        // Resources.Load<AudioClip>("AudioClips/Guns/ColtAudio/ClickRelease_Colt");
        // Resources.Load<AudioClip>("AudioClips/Guns/ColtAudio/Bang_Colt");
        // Resources.Load<AudioClip>("AudioClips/Guns/ColtAudio/Bang2_Colt");
        // Resources.Load<AudioClip>("AudioClips/Guns/ColtAudio/Bang3_Colt");
        // Resources.Load<AudioClip>("AudioClips/Guns/ColtAudio/Bang4_Colt");
        // Resources.Load<AudioClip>("AudioClips/Guns/ColtAudio/PushMagIn_Colt");
        // Resources.Load<AudioClip>("AudioClips/Guns/ColtAudio/SlideChamber_Colt");

        //  Resources.Load<AudioClip>("AudioClips/Guns/M4Audio/M4_mid");
        //  Resources.Load<AudioClip>("AudioClips/Guns/M4Audio/M4_mid1sec");
        //  Resources.Load<AudioClip>("AudioClips/Guns/M4Audio/M4_start");
        //  Resources.Load<AudioClip>("AudioClips/Guns/M4Audio/single__schots__gun-shot");

        // Resources.Load<AudioClip>("AudioClips/Guns/M1911Audio/Bang_m1911");
        // Resources.Load<AudioClip>("AudioClips/Guns/M1911Audio/Bang_m1911_original");
        // Resources.Load<AudioClip>("AudioClips/Guns/M1911Audio/ClickRelease_m1911");
        // Resources.Load<AudioClip>("AudioClips/Guns/M1911Audio/PushMagIn_m1911");
        // Resources.Load<AudioClip>("AudioClips/Guns/M1911Audio/SlideChamer_m1911");

        // Resources.Load<AudioClip>("AudioClips/Guns/mac11Audio/Bang_mac11");
        // Resources.Load<AudioClip>("AudioClips/Guns/mac11Audio/Bang_mac11_SharpShortmp3");
        // Resources.Load<AudioClip>("AudioClips/Guns/mac11Audio/Bang_mac11Shortenedmp3");
        // Resources.Load<AudioClip>("AudioClips/Guns/mac11Audio/ClickRelease_mac11");
        // Resources.Load<AudioClip>("AudioClips/Guns/mac11Audio/New_Mac_11_May_2018");
        // Resources.Load<AudioClip>("AudioClips/Guns/mac11Audio/PushMagIn_mac11");
        // Resources.Load<AudioClip>("AudioClips/Guns/mac11Audio/SlideChamer_mac11");

        // Resources.Load<AudioClip>("AudioClips/Guns/Shared/chamberingRounCHACHIKACLIKLA");
        // Resources.Load<AudioClip>("AudioClips/Guns/Shared/DRYFIRE_ALL");
        // Resources.Load<AudioClip>(" AudioClips/Guns/Shared/");

        // Resources.Load<AudioClip>(" AudioClips/Guns/ShotgunAudio/ShotgunBang");
        // Resources.Load<AudioClip>(" AudioClips/Guns/ShotgunAudio/shotgun_abrupt");
        // Resources.Load<AudioClip>(" AudioClips/Guns/ShotgunAudio/Bang_ScorpionAug1");
        // Resources.Load<AudioClip>(" AudioClips/Guns/ShotgunAudio/impacter");


        if (myType == GunType.PISTOL) { Clip_Fire = Resources.Load<AudioClip>("AudioClips/Guns/M1911Audio/9_mm"); }
        else if (myType == GunType.MAGNUM) { Clip_Fire = Resources.Load<AudioClip>("AudioClips/Guns/ColtAudio/Bang_Colt"); }
        else if (myType == GunType.P90) { Clip_Fire = Resources.Load<AudioClip>("AudioClips/Guns/ShotgunAudio/impacter"); }
        else if (myType == GunType.UZI) { Clip_Fire = Resources.Load<AudioClip>("AudioClips/Guns/mac11Audio/Bang_mac11"); }
        else if (myType == GunType.SHOTGUN) { Clip_Fire = Resources.Load<AudioClip>("AudioClips/Guns/ShotgunAudio/shotgun_abrupt"); }
        else if (myType == GunType.MG61) { Clip_Fire = Resources.Load<AudioClip>("AudioClips/Guns/mac11Audio/Bang_ScorpionAug1"); }

        else { Clip_Fire = Resources.Load<AudioClip>("AudioClips/Guns/M1911Audio/Bang_m1911"); }

        Clip_MagOut = Resources.Load<AudioClip>("AudioClips/Guns/mac11Audio/ClickRelease_mac11");

        Clip_fullreload = Resources.Load<AudioClip>("AudioClips/Guns/Shared/chamberingRounCHACHIKACLIKLA");

        Clip_MagIn = Resources.Load<AudioClip>("AudioClips/Guns/mac11Audio/PushMagIn_mac11");
        Clip_Dry = Resources.Load<AudioClip>("AudioClips/Guns/Shared/DRYFIRE_ALL");

        //_uaudio = GetComponent<UAudioManager>();
        //  InitBarelTrans();
    }

    // public Transform barrelTran;


    //void InitBarelTrans()
    //{
    //    barrelTran = gunhelper.DeepSearch(this.transform, "BarrelObj");
    //    if (barrelTran == null)
    //    {
    //        Debug.LogError("barel not found ");
    //        barrelTran = this.transform;
    //    }

    //    if (barrelTran == null)
    //    {
    //        barrelTran = gattm.BulletSpawnerMovableTransform;
    //    }
    //}

    // public void initBareltrans(Transform ArgbarrelTran) { Transform barrelTran = ArgbarrelTran; }

    //void doLaserfrom() { gunhelper.DrawStaticLaserPointer(barrelTran.transform.position, barrelTran.transform.position + (barrelTran.transform.forward * - 8 )); }





    // Update is called once per frame
    //void Update () {
    //    //if (laserOn) { doLaserfrom(); }
    //}
}
