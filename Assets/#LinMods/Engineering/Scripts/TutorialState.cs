//#define ENABLE_KEYBORADINPUTS
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialState: MonoBehaviour
{
 ComPuckObject CommunicatorPuck;
 
 public AudioClip Commander1_Voice;
 public AudioClip Computer1_Voice;
 public AudioClip Commander2_Voice;
 public AudioClip Computer2_Voice;
 public AudioClip Commander3_Voice;
 
 public AudioClip ComputerReloadHelp_Voice;
 
 public ComPuckObject ComPuckObj;
 
 public GameObject TargetStyle1;
 public GameObject BarnTarget;
 public GameObject HeadshotTarget;
 
 public Transform   FirstTargetsSpawn;
 public Transform[] SecondTargetsSpawns;
 
 List<TutorialTargetObj> ActiveTargets;
 ////float CurrentTime = 0.0f;
 
 delegate void ActiveStateFunc(/*void*/);
 ActiveStateFunc CurrentState = null;
 int CurrentSubstate = 0;
 
 // Use this for initialization
 void Start(/*void*/)
 {
  StartIdleState();
 }

 // Update is called once per frame
#if ENABLE_KEYBORADINPUTS
 void Update(/*void*/)
 {
  if (CurrentState != null)
    CurrentState();
    
  // Handle test target shooting
  if (ActiveTargets != null)
    if (Input.GetKeyDown(KeyCode.L))
      for (int i=0; i<ActiveTargets.Count; ++i)
        if (ActiveTargets[i] != null)
          if (ActiveTargets[i].CanBeShot())
            if (!ActiveTargets[i].HasBeenHit())
              {
               ActiveTargets[i].NotifyHit();
               break;
              }
 }
#endif
 
 
 void IdleState(/*void*/)
 {
#if ENABLE_KEYBORADINPUTS
  if (Input.GetKeyDown(KeyCode.L))
    BeginTutorialExecution();
#endif
 }
 
 
 void Tutorial1State(/*void*/)
 {
  // Handle the substate
  switch (CurrentSubstate)
    {
     case 0:
        ComPuckObj.StartSwapHologramToGeneralState();
        ++CurrentSubstate;
        break;
        
     case 1:
        if (!ComPuckObj.IsSwapping())
          {
           ComPuckObj.PlayAudio(Commander1_Voice);
           ++CurrentSubstate;
          }
        break;
     
     case 2:
        if (!ComPuckObj.IsPlayingAudio())
          {
           ComPuckObj.StartSwapHologramToInfoBoxState();
           ++CurrentSubstate;
           
           if (ActiveTargets == null)
             ActiveTargets = new List<TutorialTargetObj>();
           ActiveTargets.Clear();
           
           GameObject NewTarget = GameObject.Instantiate<GameObject>(TargetStyle1);
           NewTarget.transform.position = FirstTargetsSpawn.transform.position;
           NewTarget.transform.rotation = FirstTargetsSpawn.transform.rotation;
           ActiveTargets.Add(NewTarget.transform.GetComponentInChildren<TutorialTargetObj>());
          }
        break;
        
     case 3:
        if (!ComPuckObj.IsSwapping())
          {
           ComPuckObj.PlayAudio(Computer1_Voice);
           ++CurrentSubstate;
          }
        break;
     
     case 4:
        if (!ComPuckObj.IsPlayingAudio())
          {
           // Target testing
           bool TargetsStillExist = false;
           for (int i=0; i<ActiveTargets.Count; ++i)
             TargetsStillExist |= !ActiveTargets[i].HasBeenHit();
           if (!TargetsStillExist)
             {
              for (int i=0; i<ActiveTargets.Count; ++i)
                ActiveTargets[i].Retract();
              ActiveTargets.Clear();
             }
           
           // Tutorial execution flow
           if (!TargetsStillExist) 
             StartTutorial2State();
           else
             {
              ComPuckObj.StartSwapHologramToQuestionBoxState();
              ++CurrentSubstate;
             }
          }
        break;
     
     case 5:
        {
#if ENABLE_KEYBORADINPUTS
         // Info box replay test 
         if (!ComPuckObj.IsSwapping())
           if (Input.GetKeyDown(KeyCode.A))
             ReplayComputerMessage();
#endif
                    // Tutorial target shot?
                    bool TargetsStillExist = false;
         for (int i=0; i<ActiveTargets.Count; ++i)
           TargetsStillExist |= !ActiveTargets[i].HasBeenHit();
         if (!TargetsStillExist)
           {
            for (int i=0; i<ActiveTargets.Count; ++i)
              ActiveTargets[i].Retract();
            ActiveTargets.Clear();
           }
         
         // Tutorial execution flow
         if (!TargetsStillExist)
           StartTutorial2State();
         break;
        }  
     };
 }
 
 
 void Tutorial2State(/*void*/)
 {
  // Handle the substate
  switch (CurrentSubstate)
    {
     case 0:
        ComPuckObj.StartSwapHologramToGeneralState();
        ++CurrentSubstate;
        break;
        
     case 1:
        if (!ComPuckObj.IsSwapping())
          {
           ComPuckObj.PlayAudio(Commander2_Voice);
           ++CurrentSubstate;
          }
        break;
     
     case 2:
        if (!ComPuckObj.IsPlayingAudio())
          {
           ComPuckObj.StartSwapHologramToInfoBoxState();
           ++CurrentSubstate;
           
           if (ActiveTargets == null)
             ActiveTargets = new List<TutorialTargetObj>();
           ActiveTargets.Clear();
           
           for (int i=0; i<SecondTargetsSpawns.Length; ++i)
             {
              GameObject NewTarget = GameObject.Instantiate<GameObject>(HeadshotTarget);
              NewTarget.transform.position = SecondTargetsSpawns[i].transform.position;
              NewTarget.transform.rotation = SecondTargetsSpawns[i].transform.rotation;
              ActiveTargets.Add(NewTarget.transform.GetComponentInChildren<TutorialTargetObj>());
             }
          }
        break;
        
     case 3:
        if (!ComPuckObj.IsSwapping())
          {
           ComPuckObj.PlayAudio(Computer2_Voice);
           ++CurrentSubstate;
          }
        break;
     
     case 4:
        if (!ComPuckObj.IsPlayingAudio())
          {
           // Target testing
           bool TargetsStillExist = false;
           for (int i=0; i<ActiveTargets.Count; ++i)
             TargetsStillExist |= !ActiveTargets[i].HasBeenHit();
           if (!TargetsStillExist)
             {
              for (int i=0; i<ActiveTargets.Count; ++i)
                ActiveTargets[i].Retract();
              ActiveTargets.Clear();
             }
           
           // Tutorial execution flow
           if (!TargetsStillExist) 
             StartTutorial3State();
           else
             {
              ComPuckObj.StartSwapHologramToQuestionBoxState();
              ++CurrentSubstate;
             }
          }
        break;
     
     case 5:
          {
#if ENABLE_KEYBORADINPUTS
         // Info box replay test 
         if (!ComPuckObj.IsSwapping())
           if (Input.GetKeyDown(KeyCode.A))
             ReplayComputerMessage();
#endif
                    // Target testing
                    bool TargetsStillExist = false;
           for (int i=0; i<ActiveTargets.Count; ++i)
             TargetsStillExist |= !ActiveTargets[i].HasBeenHit();
           if (!TargetsStillExist)
             {
              for (int i=0; i<ActiveTargets.Count; ++i)
                ActiveTargets[i].Retract();
              ActiveTargets.Clear();
             }
           
           // Tutorial execution flow
           if (!TargetsStillExist) 
             StartTutorial3State();
          }
        break;
    };
 }
 
 
 void Tutorial3State(/*void*/)
 {
  // Handle the substate
  switch (CurrentSubstate)
    {
     case 0:
        ComPuckObj.StartSwapHologramToGeneralState();
        ++CurrentSubstate;
        break;
        
     case 1:
        if (!ComPuckObj.IsSwapping())
          {
           ComPuckObj.PlayAudio(Commander3_Voice);
           ++CurrentSubstate;
          }
        break;
     
     case 2:
        if (!ComPuckObj.IsPlayingAudio())
          {
           ComPuckObj.StartSwapHologramToNothingState();
           ++CurrentSubstate;
          }
        break;
        
     case 3:
                if (!ComPuckObj.IsSwapping()) {

                    ++CurrentSubstate;
                  //  GameManagerOld.Instance.CheckWav1Started(); //LIN TUTO WHEN DONE
                }
//          StartIdleState();
        break;
    };
 }
 
 
 void StartIdleState(/*void*/)
 {
  // Reset the time
  ////CurrentTime = 0.0f;

  // Reset the substate
  CurrentSubstate = 0;
    
  // Set the state
  CurrentState = IdleState;
 }
 
 void StartTutorial1State(/*void*/)
 {
  // Reset the time
  ////CurrentTime = 0.0f;

  // Reset the substate
  CurrentSubstate = 0;
    
  // Set the state
  CurrentState = Tutorial1State;
 }
 
 void StartTutorial2State(/*void*/)
 {
  // Reset the time
  ////CurrentTime = 0.0f;

  // Reset the substate
  CurrentSubstate = 0;
    
  // Set the state
  CurrentState = Tutorial2State;
 }
 
 void StartTutorial3State(/*void*/)
 {
  // Reset the time
  ////CurrentTime = 0.0f;

  // Reset the substate
  CurrentSubstate = 0;
    
  // Set the state
  CurrentState = Tutorial3State;
 }
 
 public void BeginTutorialExecution(/*void*/)
 {
      //  if (GameManagerOld.Instance.DidRzEperienceStartYet()) return;
  // Only "begin" if this is idle
  if (CurrentState != IdleState)
    return;
  
  // Get all hologram renderers for param adjustments
  List<Renderer> HologramRenderers = new List<Renderer>();
  HologramRenderers.AddRange(ComPuckObj.GeneralHologram.GetComponentsInChildren<Renderer>());
  HologramRenderers.AddRange(ComPuckObj.ComputerHelpHologramObj.GetComponentsInChildren<Renderer>());
  HologramRenderers.AddRange(ComPuckObj.IdleHelpHologramObj.GetComponentsInChildren<Renderer>());
  
  // Set shader params for hologram object zero-offsets
  float BasePositionY = transform.position.y;
  foreach (Renderer r in HologramRenderers)
    r.material.SetFloat("_FloorY", BasePositionY);
    
  // Start the tutorial execution  
  StartTutorial1State();
 }
 
 public void ReplayComputerMessage(/*void*/)
 {
  // Swap to the computer info box
  ComPuckObj.StartSwapHologramToInfoBoxState();
  
  // State 3 is generally when the info box speaks
  CurrentSubstate = 3;
 }
}
