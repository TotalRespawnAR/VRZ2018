using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRZPlayerAudio : MonoBehaviour
{


    List<AudioClip> One2Nine;


    List<AudioClip> PainSounds;


    // public AudioSource PlayerSource;

    AudioSource PainSource2D;
    AudioSource GAmeEventSource2D;
    AudioSource ImpactSource2D;


    //// Use this for initialization
    //void Start()
    //{

    //    //PlayCountDOwnGameOver();
    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}

    AudioSource[] SourcesOnMe;

    private void Awake()
    {

        SourcesOnMe = GetComponents<AudioSource>();
        Debug.Log("found " + SourcesOnMe.Length);
        if (SourcesOnMe.Length > 2)
        {
            PainSource2D = SourcesOnMe[0];
            // PainSource2D.clip = GameSettings.Instance.Squish0;
            GAmeEventSource2D = SourcesOnMe[1];
            //  GAmeEventSource2D.clip = GameSettings.Instance.Sudden;
            ImpactSource2D = SourcesOnMe[2];
            // ImpactSource2D.clip = GameSettings.Instance.HeadShotCrushclip;
        }
        One2Nine = new List<AudioClip>();
        PainSounds = new List<AudioClip>();

        One2Nine.Add(GameSettings.Instance.A_10);
        One2Nine.Add(GameSettings.Instance.A_9);
        One2Nine.Add(GameSettings.Instance.A_8);
        One2Nine.Add(GameSettings.Instance.A_7);
        One2Nine.Add(GameSettings.Instance.A_6);
        One2Nine.Add(GameSettings.Instance.A_5);
        One2Nine.Add(GameSettings.Instance.A_4);
        One2Nine.Add(GameSettings.Instance.A_3);
        One2Nine.Add(GameSettings.Instance.A_2);
        One2Nine.Add(GameSettings.Instance.A_1);


        PainSounds.Add(GameSettings.Instance.Squish0);
        PainSounds.Add(GameSettings.Instance.Squish1);
        PainSounds.Add(GameSettings.Instance.Squish2);

    }

    public void PlayImpactHead()
    {
        ImpactSource2D.Stop();
        ImpactSource2D.clip = GameSettings.Instance.HeadImpact2;
        ImpactSource2D.Play(); ;
    }
    public void PlayImpactBody()
    {
        ImpactSource2D.Stop();
        ImpactSource2D.clip = GameSettings.Instance.BodyImpact;
        ImpactSource2D.Play(); ;
    }



    public void PlayHeart()
    {
        //PainSource2D.Stop();
        //PainSource2D.clip = HeartBeat;
        //PainSource2D.Play(); ;
    }

    public void PlayGong()
    {
        PainSource2D.Stop();
        PainSource2D.clip = GameSettings.Instance.Gong;
        PainSource2D.Play(); ;
    }

    int curPainSoundIndex = 0;
    public void PlayHurts()
    {
        PainSource2D.Stop();
        PainSource2D.clip = PainSounds[curPainSoundIndex];
        PainSource2D.Play();

        curPainSoundIndex++;
        if (curPainSoundIndex >= PainSounds.Count)
        {
            curPainSoundIndex--;
        }
    }

    public void PlayGlassBreak()
    {
        PainSource2D.Stop();
        PainSource2D.clip = GameSettings.Instance.GlassBreakSmall;
        PainSource2D.Play();
    }

    public void PlayCountDOwnGameOver()
    {
        StartCoroutine(playSound());
    }
    public void PlaySlowTime()
    {
        GAmeEventSource2D.Stop();
        GAmeEventSource2D.clip = GameSettings.Instance.Slow;
        GAmeEventSource2D.Play();

    }

    public void PlaySlowTimeOff()
    {
        GAmeEventSource2D.Stop();
        GAmeEventSource2D.clip = GameSettings.Instance.Slow;
        GAmeEventSource2D.Play();

    }
    public void PlaySuddenDeath()
    {
        GAmeEventSource2D.Stop();
        GAmeEventSource2D.clip = GameSettings.Instance.Sudden;
        GAmeEventSource2D.Play();
    }

    public void Play200Cheer()
    {
        GAmeEventSource2D.Stop();
        GAmeEventSource2D.clip = GameSettings.Instance.Buzz200Cheer;
        GAmeEventSource2D.Play();
    }

    public void PlayWomp()
    {
        GAmeEventSource2D.Stop();
        GAmeEventSource2D.clip = GameSettings.Instance.Womp;
        GAmeEventSource2D.Play();
    }



    IEnumerator playSound()
    {
        GAmeEventSource2D.clip = One2Nine[0];
        GAmeEventSource2D.Play();
        yield return new WaitForSeconds(1);
        GAmeEventSource2D.clip = One2Nine[1];
        GAmeEventSource2D.Play();
        yield return new WaitForSeconds(1);
        GAmeEventSource2D.clip = One2Nine[2];
        GAmeEventSource2D.Play();
        yield return new WaitForSeconds(1);
        GAmeEventSource2D.clip = One2Nine[3];
        GAmeEventSource2D.Play();
        yield return new WaitForSeconds(1);
        GAmeEventSource2D.clip = One2Nine[4];
        GAmeEventSource2D.Play();
        yield return new WaitForSeconds(1);
        GAmeEventSource2D.clip = One2Nine[5];
        GAmeEventSource2D.Play();
        yield return new WaitForSeconds(1);
        GAmeEventSource2D.clip = One2Nine[6];
        GAmeEventSource2D.Play();
        yield return new WaitForSeconds(1);
        GAmeEventSource2D.clip = One2Nine[7];
        GAmeEventSource2D.Play();
        yield return new WaitForSeconds(1);
        GAmeEventSource2D.clip = One2Nine[8];
        GAmeEventSource2D.Play();
        yield return new WaitForSeconds(1);
        GAmeEventSource2D.clip = One2Nine[9];
        GAmeEventSource2D.Play();
        yield return new WaitForSeconds(1.4f);
        GAmeEventSource2D.Stop();
        //PlayerSource.clip = One2Nine[10];
        GAmeEventSource2D.PlayOneShot(One2Nine[10]);


    }
}
