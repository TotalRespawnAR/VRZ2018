using System.Collections;
using UnityEngine;


public class PlayHeadShotSound : MonoBehaviour
{
    // UAudioManager _uaudioSplat;
    public VRZPlayerAudio vrzAudio;
    public static PlayHeadShotSound Instance = null;
    private void Awake()
    {
        if (Instance == null)
        {

            // DontDestroyOnLoad(this.gameObject);
            Instance = this;

        }
        else
        {
            Destroy(this);
        }

        print("I Is on " + gameObject.name);
        if (this.gameObject.transform.parent != null)
        {
            print("my dad is" + this.gameObject.transform.parent.name);
        }
    }


    private void OnEnable()
    {

        // GameManager.OnWaveStartedOrReset_DEO += WaveLevelStartedOrReloaded;
        GameEventsManager.OnSuddenDeath += PlaySound_2d_SuddenDeath;

        GameEventsManager.OnWallBrak += PlayWallBreak;
        GameEventsManager.OnWomp += PlayWomp;
    }

    private void OnDisable()
    {

        // GameManager.OnWaveStartedOrReset_DEO -= WaveLevelStartedOrReloaded;
        GameEventsManager.OnSuddenDeath -= PlaySound_2d_SuddenDeath;

        GameEventsManager.OnWallBrak -= PlayWallBreak;
        GameEventsManager.OnWomp -= PlayWomp;


    }


    void PlayWallBreak()
    {
        //_uaudioSplat.PlayEvent("_WallBreak");
    }
    void PlayWomp()
    {
        //   _uaudioSplat.PlayEvent("_Womp");
        vrzAudio.PlayWomp();
    }
    public void PlayGongSound()
    {
        vrzAudio.PlayGong();
    }
    public void PlayDamage()
    {
        //_uaudioSplat.PlayEvent("_damageHit");
        vrzAudio.PlayHurts();
    }

    private void Start()
    {
        //_uaudioSplat = GetComponent<UAudioManager>();
    }

    public void PlayScoreSound(string argEventName)
    {
        SplatSountDelay(argEventName);
    }

    public void PlaySplatSound(string argEventName)
    {
        StartCoroutine(SplatSountDelay(argEventName));
    }

    IEnumerator SplatSountDelay(string argEventName)
    {
        yield return new WaitForSeconds(0.1f);
        //_uaudioSplat.PlayEvent(argEventName);
    }


    public void PlayHeartBeat(float rate)
    {
        // _uaudioSplat.PlayEvent("_HeartBeat");
    }
    public void StopHeartBeat(float rate)
    {
        // _uaudioSplat.StopEvent("_HeartBeat");
        //vrzAudio.PlayHeart();
    }

    public void PlaySound_2d_HeadShot()
    {// _uaudioSplat.PlayEvent("_Splat"); 
    }
    public void PlaySound_2d_HEadShotNonLethalh()
    { //_uaudioSplat.PlayEvent("_SplatNonLethal"); 
    }
    public void PlaySound_2d_MetalDing()
    {// _uaudioSplat.PlayEvent("_MetalDing");
    }
    public void PlaySound_2d_SlowTimeOn()
    {
        print("slow audio on");
        vrzAudio.PlaySlowTime();
    }
    public void PlaySound_2d_SlowTimeOff()
    {
        print("slow audio off");
        vrzAudio.PlaySlowTimeOff();
    }
    public void PlaySound_2d_SuddenDeath()
    {
        vrzAudio.PlaySuddenDeath();
    }
    public void PlaySound_2d_PlayerMustDie()
    {// _uaudioSplat.PlayEvent("_Boat"); 
    }


    public void PlaySound_2d_200Cheer()
    {// _uaudioSplat.PlayEvent("_Boat"); 

        vrzAudio.Play200Cheer();
    }

    public void PlayImpBig()
    { //_uaudioSplat.PlayEvent(argeventname); 
        vrzAudio.PlayImpactHead();
    }
    public void PlaySound(string argeventname)
    { //_uaudioSplat.PlayEvent(argeventname); 
    }

    public void PlayIMPsmall()
    { //_uaudioSplat.PlayEvent(argeventname); 
        vrzAudio.PlayImpactBody();
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.K)) {
    //        _uaudioSplat.PlayEvent("_Splat");
    //    }
    //}


}
