using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComPuckObject: MonoBehaviour
{
 public Transform HologramRaysObj;
 public Transform GeneralHologram;
 public Transform ComputerHelpHologramObj;
 public Transform IdleHelpHologramObj;
 
 public AudioSource ComPuckAudioSource;
 
 Transform ActiveHologramRoot;
 
 float HologramScale = 1.0f;
 
 public float SWAP_SPEED = 2.0f;
 float CurrentTime = 0.0f;
 
 delegate void ActiveStateFunc(/*void*/);
 ActiveStateFunc CurrentState = null;
 
 // Use this for initialization
 void Start(/*void*/)
 {
  GeneralHologram.gameObject.SetActive(false);
  ComputerHelpHologramObj.gameObject.SetActive(false);
  IdleHelpHologramObj.gameObject.SetActive(false);
  
  ActiveHologramRoot = GeneralHologram;
  
  HologramScale = 0.05f;
  CurrentTime = 0.0f;
  
  StartSwapHologramToNothingState();
  CurrentTime = 1.0f;
 }
	
 // Update is called once per frame
 void Update(/*void*/)
 {
  if (CurrentState != null)
    CurrentState();
 }
 
 
 public bool IsSwapping(/*void*/)
 {
  return CurrentState != IdleState;
 }
 
 public bool IsPlayingAudio(/*void*/)
 {
  return ComPuckAudioSource.isPlaying;
 }
 
 public void PlayAudio(AudioClip NewClipToPlay)
 {
  ComPuckAudioSource.Stop();
  ComPuckAudioSource.clip = NewClipToPlay;
  if (NewClipToPlay != null)
    ComPuckAudioSource.Play();
 }
 
 void IdleState(/*void*/)
 {
 }
 
 
 
 void SwapHologramToGeneralState(/*void*/)
 {
  if (HologramScale <= 0.05f)
    ActiveHologramRoot = GeneralHologram;
    
  SwapHologramState_COMMON();
 }
 
 
 
 void SwapHologramToInfoBoxState(/*void*/)
 {
  if (HologramScale <= 0.05f)
    ActiveHologramRoot = ComputerHelpHologramObj;
    
  SwapHologramState_COMMON();
 }
 
 
 
 void SwapHologramToQuestionBoxState(/*void*/)
 {
  if (HologramScale <= 0.05f)
    ActiveHologramRoot = IdleHelpHologramObj;
    
  SwapHologramState_COMMON();
 }
 
 
 
 void SwapHologramToNothingState(/*void*/)
 {
  if (HologramScale <= 0.05f)
    ActiveHologramRoot = null;
    
  SwapHologramState_COMMON();
 }
 
 
 
 void SwapHologramState_COMMON(/*void*/)
 {
  CurrentTime += Time.deltaTime*SWAP_SPEED;
  HologramScale = Mathf.Clamp(1.0f-Mathf.Sin(CurrentTime*Mathf.PI/2.0f), 0.05f, 1.0f);

  if (ActiveHologramRoot != null)
    {
     ActiveHologramRoot.gameObject.SetActive(HologramScale > 0.05f);  
     ActiveHologramRoot.transform.localScale = HologramScale*Vector3.one;
    }
      
  Vector3 RaysScale = HologramRaysObj.transform.localScale;
  RaysScale.x = HologramScale;
  RaysScale.z = HologramScale;
  if (HologramRaysObj != null)
    {
     HologramRaysObj.transform.localScale = RaysScale;
     HologramRaysObj.gameObject.SetActive(ActiveHologramRoot != null);
    }
    
  if (CurrentTime >= 2.0f)
    StartIdleState();
 }
 
 
 
 void StartIdleState(/*void*/)
 {
  // Reset the time
  CurrentTime = 0.0f;
  
  // Set the state
  CurrentState = IdleState;
 }
 
 
 
 public void StartSwapHologramToGeneralState(/*void*/)
 {
  // Reset the time
  CurrentTime = 0.0f;
  
  // Set the state
  CurrentState = SwapHologramToGeneralState;
 }
 
 
 
 public void StartSwapHologramToInfoBoxState(/*void*/)
 {
  // Reset the time
  CurrentTime = 0.0f;
  
  // Set the state
  CurrentState = SwapHologramToInfoBoxState;
 }
 
 
 
 public void StartSwapHologramToQuestionBoxState(/*void*/)
 {
  // Reset the time
  CurrentTime = 0.0f;
  
  // Set the state
  CurrentState = SwapHologramToQuestionBoxState;
 }
 
 
 
 public void StartSwapHologramToNothingState(/*void*/)
 {
  // Reset the time
  CurrentTime = 0.0f;
  
  // Set the state
  CurrentState = SwapHologramToNothingState;
 }
}
