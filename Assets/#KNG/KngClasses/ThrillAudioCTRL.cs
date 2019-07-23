using UnityEngine;
//[RequireComponent(typeof(AudioSource))]

public class ThrillAudioCTRL : MonoBehaviour
{

    public AudioSource AudioSource;
    WaveLevel curlrvel;
    float maxz;
    float fractionstep;

    // UAudioManager _uauio;
    //private void OnEnable()
    //{

    //    // GameManager.OnWaveStartedOrReset_DEO += WaveLevelStartedOrReloaded;
    //    GameEventsManager.OnWallBrak += PlayWallBreak;
    //    GameEventsManager.OnWomp += PlayWomp;
    //}

    //private void OnDisable()
    //{

    //    // GameManager.OnWaveStartedOrReset_DEO -= WaveLevelStartedOrReloaded;
    //    GameEventsManager.OnWallBrak -= PlayWallBreak;
    //    GameEventsManager.OnWomp -= PlayWomp;


    //}
    //void WaveLevelStartedOrReloaded(WaveLevel loadedlevel ) {
    //    curlrvel = loadedlevel;

    //    maxz = 10.0f;// (float)curlrvel.Get_LevelMaxEnemiesOnScreen();
    //    fractionstep = 1 / maxz;
    //    CancelInvoke();
    //     InvokeRepeating("KeepAnEyeOnALPHABRAVODIST", 8.0f, 1f);

    //}



    void Start()
    {
        print("ian am o " + gameObject.name);
        // _uauio = GetComponent<UAudioManager>();
        if (AudioSource == null)
        {
            this.gameObject.AddComponent<AudioSource>();
            AudioSource = GetComponent<AudioSource>();
        }
    }

    public void SetvolumeTHrill(float vol)
    {
        AudioSource.volume = vol;
    }


    ////int Vstep = 0;


    void ModulateVolume()
    {

        float volumefloat = (float)ZombiesInDangerZone * fractionstep;
        if (volumefloat > 1.0f)
        {
            volumefloat = 1.0f;
        }

        SetvolumeTHrill(volumefloat);
    }

    int ZombiesInDangerZone = 0;
    //void KeepAnEyeOnALPHABRAVODIST()
    //{
    //    foreach (GameObject lizeZombie in GameManager.Instance.ENEMYMNGER_getter().enemies) {
    //        float dis = Vector3.Distance(lizeZombie.transform.position, GameManager.Instance.GetStaticPLayerPosition());
    //        if (dis < 6f) {
    //            ZombiesInDangerZone++;
    //        }
    //    }
    //    ModulateVolume();
    //    ZombiesInDangerZone = 0;
    //}
}
