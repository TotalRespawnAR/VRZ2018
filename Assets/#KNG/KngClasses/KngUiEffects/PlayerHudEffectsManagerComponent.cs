using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHudEffectsManagerComponent : MonoBehaviour
{

    public CanvasHudEffects CanHudEffects;

    //triger: trigHudAnim
    public Animator AnimTorOVR_BearClawRL;
    public Animator AnimTorOVR_BearClawLR;
    public Animator AnimTorOVR_AxeHit;
    public Animator AnimTorOVR_GuyleSlash;
    public Animator AnimTorOVR_ScratchUpdown;
    public Animator AnimTorOVR_Scratch2XUpdpwn;
    public Animator AnimTorOVR_RightHookLR;
    public Animator AnimTorOVR_MelePunchRL;
    public Animator AnimTorOVR_ScratchLR;
    public Animator AnimTorOVR_ScratchRL;
    public Animator AnimTorOVR_GlassBreak1;
    public Animator AnimTorOVR_GlassBreak2;
    public Animator AnimTorOVR_BloodDencity1; //these still legacy
    public Animator AnimTorOVR_BloodDencity2;//these still legacy
    public Animator AnimTorOVR_BloodDencity3;//these still legacy
    public Animator AnimTorOVR_BloodDencity4;//these still legacy
    public Animator AnimTorOVR_BloodState1;//these still legacy
    public Animator AnimTorOVR_BloodState2;//these still legacy
    public Animator AnimTorOVR_BloodState3;//these still legacy
    public Animator AnimTorOVR_BloodState4;//these still legacy
    public Animator AnimTorOVR_BloodCurtain;//these still legacy


    public Text txRealTime;
    public Text txLive;
    public Text txEvent;
    public Text txtVrt;



    int _curLineNum = -1;
    public int _MaxLines = 38;
    Queue<string> _MyDebugQueue = new Queue<string>();
    public void UpdateRealTime(string str)
    {
        txRealTime.text = str;
    }

    public void UpdateLive(string str)
    {
        txLive.text = str;
    }


    public void UpdateEvent(string str)
    {
        txEvent.text = str;
    }

    public void UpdateVert(string str)
    {
        LogToQueue(str);
    }
    void LogToQueue(string str)
    {
        AddLineToDebugQueue(str);
        RefreshDebugPanel();
    }
    void AddLineToDebugQueue(string str)
    {
        _curLineNum++;
        CheckOverflow2();
        str = _curLineNum + " " + str;
        _MyDebugQueue.Enqueue(str);
    }
    void CheckOverflow2()
    {
        if (_MyDebugQueue.Count >= _MaxLines)
        {
            _MyDebugQueue.Dequeue();
        }
    }


    void RefreshDebugPanel()
    {
        StringBuilder sb = new StringBuilder();

        foreach (string str in _MyDebugQueue)
        {
            sb.Append(str);
            sb.Append(Environment.NewLine);
        }

        txtVrt.text = "";
        txtVrt.text = sb.ToString();
    }

    // Use this for initialization
    void Start()
    {

    }
    int hudeffectnum = 12;
    // Update is called once per frame

    void DoTrig(Animator arg_animor)
    {
        // Debug.Log("triggering " + arg_animor.name);
        arg_animor.SetTrigger("trigHudAnim");
        // arg_animor.ResetTrigger("trigHudAnim");
    }
    void TriggerAnimatorByEnum(TriggersDamageEffects argTrigDamEff)
    {

        switch (argTrigDamEff)
        {

            case TriggersDamageEffects.BearClawLR:
                DoTrig(AnimTorOVR_BearClawLR);
                break;

            case TriggersDamageEffects.BearClawRL:
                DoTrig(AnimTorOVR_BearClawRL);
                break;

            case TriggersDamageEffects.AxeHit:
                DoTrig(AnimTorOVR_AxeHit);
                break;

            case TriggersDamageEffects.GuyleSlash:
                DoTrig(AnimTorOVR_GuyleSlash);
                break;

            case TriggersDamageEffects.ScratchUpdown:
                DoTrig(AnimTorOVR_ScratchUpdown);
                break;

            case TriggersDamageEffects.Scratch2XUpdpwn:
                DoTrig(AnimTorOVR_Scratch2XUpdpwn);
                break;

            case TriggersDamageEffects.RightHookLR:
                DoTrig(AnimTorOVR_RightHookLR);
                break;
            case TriggersDamageEffects.MelePunchRL:
                DoTrig(AnimTorOVR_MelePunchRL);
                break;

            case TriggersDamageEffects.ScratchLR:
                DoTrig(AnimTorOVR_ScratchLR);
                break;
            case TriggersDamageEffects.ScratchRL:
                DoTrig(AnimTorOVR_ScratchRL);
                break;
            case TriggersDamageEffects.GlassBreak1:
                DoTrig(AnimTorOVR_GlassBreak1);
                break;
            case TriggersDamageEffects.GlassBreak2:
                DoTrig(AnimTorOVR_GlassBreak2);
                break;

            case TriggersDamageEffects.BloodDencity1:
                DoTrig(AnimTorOVR_BloodDencity1);
                break;
            case TriggersDamageEffects.BloodDencity2:
                DoTrig(AnimTorOVR_BloodDencity2);
                break;
            case TriggersDamageEffects.BloodDencity3:
                DoTrig(AnimTorOVR_BloodDencity3);
                break;
            case TriggersDamageEffects.BloodDencity4:
                DoTrig(AnimTorOVR_BloodDencity4);
                break;

            case TriggersDamageEffects.BloodState1:
                DoTrig(AnimTorOVR_BloodState1);
                break;
            case TriggersDamageEffects.BloodState2:
                DoTrig(AnimTorOVR_BloodState2);
                break;
            case TriggersDamageEffects.BloodState3:
                DoTrig(AnimTorOVR_BloodState3);
                break;
            case TriggersDamageEffects.BloodState4:
                DoTrig(AnimTorOVR_BloodState4);
                break;
            case TriggersDamageEffects.BloodCurtain:
                DoTrig(AnimTorOVR_BloodCurtain);
                break;


        }
    }



    public void Trig_DamageEffect(TriggersDamageEffects argDamageTrig)
    {
        TriggerAnimatorByEnum(argDamageTrig);

    }
}
