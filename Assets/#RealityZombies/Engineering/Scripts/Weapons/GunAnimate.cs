using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAnimate : MonoBehaviour {
    Animator g_animator;
    Gun _gbad2wayflow;
    private void Awake()
    {
        g_animator = gameObject.GetComponent<Animator>();
        _gbad2wayflow = GetComponent<Gun>();
     }

    public void PlayFast() {
      //  if (_gbad2wayflow.GUN_GET_BOOLS().ThisGunIsReloading) return;
        g_animator.speed = 1.5f;
    }
    public void PlayFastest() {
       // if (_gbad2wayflow.GUN_GET_BOOLS().ThisGunIsReloading) return;
        g_animator.speed = 2.0f;
    }
    public void PlayNormal() {
     //   if (_gbad2wayflow.GUN_GET_BOOLS().ThisGunIsReloading) return;
        g_animator.speed = 1.0f;
    }

    public void SetSpeed(float argspeed) { g_animator.speed = argspeed; }
    public void Gunimate_FIRE()
    {
        if (g_animator != null)
        {
           // g_animator.Play("FIREFLAT");
            g_animator.Play("FIREFLAT");
           
        }
        else
        {
            Debug.LogError("no animator for this gun");
        }
    }
    //**************************** move to gunAnimation.cs
    public void Gunimate_FIREFLAT()
    {
        if (g_animator != null)
        {
            g_animator.Play("FIREFLAT");
            //g_animator.Play("FIREROT");
        }
        else
        {
            Debug.LogError("no animator for this gun");
        }
    }
    public void Gunimate_FIREtrigger()
    {
        if (g_animator != null)
        {
            g_animator.SetTrigger("TrigFireRot");
        }
        else
        {
            Debug.LogError("no animator for this gun");
        }
    }

    public void Gunimate_OPENSLIDER() {
        if (GameManager.Instance != null)
        {
            if (GameManager.Instance.KngGameState == ARZState.WaveBuffer) { return; }
        }
        g_animator.Play("OPENSLIDER"); // OPENSLIDER >then goes to> SLIDEOUT-> triggers OnSlideOutAnimComplete (animator takes care of this)
    }
    public void Gunimate_SLIDEIN()
    {
        if (GameManager.Instance != null)
        {
            if (GameManager.Instance.KngGameState == ARZState.WaveBuffer) { return; }
        }
        //   g_animator.SetTrigger("TrigMagSlide"); // --> SLIDEIN -> CLOSESLIDER
        g_animator.Play("SLIDEIN"); // 
    }


    public void Gunimate_HAMMERDOWN() {
        g_animator.Play("HAMMERDOWN");

    }


}
