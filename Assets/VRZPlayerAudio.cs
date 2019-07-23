using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRZPlayerAudio : MonoBehaviour
{


    public List<AudioClip> One2Nine;
    public AudioClip Womp;
    public AudioClip Womp2;
    public AudioClip Gong;
    public AudioClip Gong2;
    public AudioClip Slow;
    public AudioClip SlowOff;
    public AudioClip HeartBeat;
    public AudioClip BodyImpact;
    public AudioClip HeadshotImpace;
    public AudioClip Cheer200;

    public AudioClip Sudden;

    public List<AudioClip> PainSounds;
    public AudioClip GlassBreak;

    // public AudioSource PlayerSource;

    public AudioSource PainSource2D;
    public AudioSource GAmeEventSource2D;
    public AudioSource ImpactSource2D;


    //// Use this for initialization
    //void Start()
    //{

    //    //PlayCountDOwnGameOver();
    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}

    public void PlayImpactHead()
    {
        ImpactSource2D.Stop();
        ImpactSource2D.clip = HeadshotImpace;
        ImpactSource2D.Play(); ;
    }
    public void PlayImpactBody()
    {
        ImpactSource2D.Stop();
        ImpactSource2D.clip = BodyImpact;
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
        PainSource2D.clip = Gong;
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
        PainSource2D.clip = GlassBreak;
        PainSource2D.Play();
    }

    public void PlayCountDOwnGameOver()
    {
        StartCoroutine(playSound());
    }
    public void PlaySlowTime()
    {
        GAmeEventSource2D.Stop();
        GAmeEventSource2D.clip = Slow;
        GAmeEventSource2D.Play();

    }

    public void PlaySlowTimeOff()
    {
        GAmeEventSource2D.Stop();
        GAmeEventSource2D.clip = SlowOff;
        GAmeEventSource2D.Play();

    }
    public void PlaySuddenDeath()
    {
        GAmeEventSource2D.Stop();
        GAmeEventSource2D.clip = Sudden;
        GAmeEventSource2D.Play();
    }

    public void Play200Cheer()
    {
        GAmeEventSource2D.Stop();
        GAmeEventSource2D.clip = Cheer200;
        GAmeEventSource2D.Play();
    }

    public void PlayWomp()
    {
        GAmeEventSource2D.Stop();
        GAmeEventSource2D.clip = Womp;
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
