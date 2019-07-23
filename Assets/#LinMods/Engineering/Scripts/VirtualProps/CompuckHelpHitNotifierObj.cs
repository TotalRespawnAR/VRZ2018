using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompuckHelpHitNotifierObj: MonoBehaviour
{
 public GameObject HelpTarget;
 public ComPuckObject MyHoloBase;
 
 public string HitMessage = "ReplayComputerMessage";
    void OnEnable()
    {
        GameEventsManager.OnTakeHit += TakeHit;
    }

    private void OnDisable()
    {
        GameEventsManager.OnTakeHit -= TakeHit;
    }
    // Use this for initialization
    void Start(/*void*/)
 {
 }

 // Update is called once per frame
 void Update(/*void*/)
 {
 }
 
 
 public void TakeHit(Bullet bullet, int argid)
 {
  if (string.IsNullOrEmpty(HitMessage))
    return;
  
  if (HelpTarget != null)
    if (MyHoloBase != null)
      if (!MyHoloBase.IsSwapping())
        HelpTarget.SendMessage(HitMessage);
 }
}
