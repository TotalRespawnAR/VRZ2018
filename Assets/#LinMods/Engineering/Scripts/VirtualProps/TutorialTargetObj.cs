using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTargetObj: MonoBehaviour
{
 public GameObject RootObj;
 public GameObject TriggerObj;
 public GameObject TargetObj;
 public GameObject ImageObj;
 public GameObject ParticleEffectPref;
 
 public AnimationCurve MovementCurve;
 
 public Renderer[] FadingRenderers;

 public Vector3 MovementSpeed;

 float CurrentTime;
 float TimeVelocity = 0.0f;
 
 float ImageWiggleTime = 0.0f;
 float ImageWigglePower = 1.0f;
 
 // Use this for initialization
 void Start(/*void*/)
 {
  CurrentTime = 0.0f;
  UpdateColours();
  
  Appear();
 }
	
 
 
 // Update is called once per frame
 const float MOVEMENT_MAX = 3.0f;
 void Update(/*void*/)
 {
  // Update the timer/position
  CurrentTime += Time.deltaTime*TimeVelocity;
  
  if (ImageWigglePower > 0.0f)
    {
     ImageWigglePower -= Time.deltaTime;
     if (ImageWigglePower < 0.0f)
       ImageWigglePower = 0.0f;
       
     ImageWiggleTime += Time.deltaTime;
     ImageObj.transform.localRotation = Quaternion.Euler(-Mathf.Sin(ImageWiggleTime*10.0f)*ImageWigglePower*10.0f, 0.0f, 0.0f);
    }
    
  // Past 1.0? Stop moving forwards
  if (CurrentTime > 1.0f)
    {
     CurrentTime = 1.0f;
     TimeVelocity = 0.0f;
    }
  
  // Past 0.0? Stop moving backwards
  else if (CurrentTime < 0.0f)
    {
     CurrentTime = 0.0f;
     TimeVelocity = 0.0f;
     if (RootObj != null)
       Destroy(RootObj);
     else
       Destroy(gameObject);
    }
  
  // The velocity isn't 0.0? The target is moving so update the colours
  if (TimeVelocity != 0.0f)
    UpdateColours();
    
  // Figure out the position based on the movement curve specified
  float CurrentPosition = MovementCurve.Evaluate(CurrentTime)*MOVEMENT_MAX;
  transform.localPosition = CurrentPosition*MovementSpeed - MOVEMENT_MAX*MovementSpeed;
 }
 
 public void Appear(/*void*/)
 {
  // Wiggle a little bit while lowering
  ImageWiggleTime = 0.0f;
  ImageWigglePower = 1.0f;
 
  // Time goes forwards... ie target moves towards the player
  TimeVelocity = 1.0f;
 
  // If we're fully hidden, reset the target objects
  if (CurrentTime <= 0.0f)
    {
     // Update all material colours first
     UpdateColours();
     
     // Show the target elements
     if (TargetObj != null)
       TargetObj.SetActive(true);
     if (TriggerObj != null)
       TriggerObj.SetActive(true);
     if (ImageObj != null)
       ImageObj.SetActive(true);
    }
 }
 
 
 public bool HasBeenHit(/*void*/)
 {
  return !TriggerObj.activeInHierarchy;
 }
 
 public bool CanBeShot(/*void*/)
 {
  return CurrentTime > 0.9f;
 }
 
 public void Retract(/*void*/)
 {
  // Time goes backwards... ie target moves away from the player
  TimeVelocity = -1.0f;
 }
 
 
 public void NotifySlightlyMissed(/*void*/)
 {
  ImageWiggleTime = 0.0f;
  ImageWigglePower = 1.0f;
 }
 
 
 public void NotifyHit(/*void*/)
 {
  // Do nothing if already dead
  if (!TriggerObj.activeInHierarchy)
    return;
  
  // Hide the target elements
  if (TargetObj != null)
    TargetObj.SetActive(false);
  if (TriggerObj != null)
    TriggerObj.SetActive(false);
  if (ImageObj != null)
    ImageObj.SetActive(false);
       
  // Spawn a particle effect
  if (ParticleEffectPref != null)
    {
     GameObject NewParticleEffect = GameObject.Instantiate(ParticleEffectPref) as GameObject;
     NewParticleEffect.transform.position = TargetObj.transform.position;
     NewParticleEffect.transform.rotation = TargetObj.transform.rotation;
     NewParticleEffect.GetComponent<ParticleSystem>().Emit(40);
     Destroy(NewParticleEffect, 2.0f);
    }
 }
 
 void UpdateColours(/*void*/)
 {
  // Make sure we have renderers
  if (FadingRenderers == null)
    return;
  
  //   The brightness/fade is equal to the current movement spot... though
  // add a little more black and a little more light to the two sides of
  // the spectrum.
  float CurrentFade = Mathf.Clamp((CurrentTime*1.5f)-0.2f, 0.0f, 1.0f);
  Color NewFadeColour = new Color(CurrentFade, CurrentFade, CurrentFade, 1.0f);
  
  // Update all colours
  for (int i=0; i<FadingRenderers.Length; ++i)
    FadingRenderers[i].material.color = NewFadeColour;
 }
}
