using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTargetHitNotifierObj: MonoBehaviour
{
 public TutorialTargetObj OwnerTarget;
 public string HitMessage = "NotifyHit";
 
 // Use this for initialization
 void Start(/*void*/)
 {
 }

 // Update is called once per frame
 void Update(/*void*/)
 {
 }
    void OnEnable()
    {
        GameEventsManager.OnTakeHit += TakeHit;
    }

    private void OnDisable()
    {
        GameEventsManager.OnTakeHit -= TakeHit;
    }


    public void TakeHit(Bullet bullet, int argunused)
 {
  if (string.IsNullOrEmpty(HitMessage))
    return;
  
  if (OwnerTarget != null)
    if (OwnerTarget.CanBeShot())
      if (!OwnerTarget.HasBeenHit())
        OwnerTarget.SendMessage(HitMessage);
 }
}
