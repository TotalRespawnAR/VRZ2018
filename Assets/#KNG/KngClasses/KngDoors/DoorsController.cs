using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsController : MonoBehaviour
{

    public Animator RightDoorsAnimator;
    public Animator LeftDoorsAnimator;
    public bool RightDoorIsOpen = false;
    public bool LeftDoorIsOpen = false;
   
  //  public bool rightFinishedanim = true;
  //  public bool lefttFinishedanim = true;
    private void OnEnable()
    {
        GameEventsManager.On_Doors += ControleDoorsAnimation;
    }
    private void OnDisable()
    {
        GameEventsManager.On_Doors -= ControleDoorsAnimation;
    }

    //void ControleDoorsAnimation(char lr, char oc)
    //{
    //   // Debug.Log("heard" + " "+ lr + " "+ oc);
    //    if (lr == 'r')
    //    {
    //        if (oc == 'o')
    //        {

    //              RightDoorsAnimator.SetTrigger("TrigOpen");
    //        }
    //        else
    //        {
    //            Debug.Log("TrigClose  R");
    //            RightDoorsAnimator.SetTrigger("TrigClose");

    //        }
    //    }
    //    else
    //    {
    //        if (oc == 'o')
    //        {
    //            Debug.Log("TrigOpen  L");

    //            LeftDoorsAnimator.SetTrigger("TrigOpen");


    //        }
    //        else
    //        {
    //            Debug.Log("TrigClose  L");
    //            LeftDoorsAnimator.SetTrigger("TrigClose");

    //        }
    //    }

    //}

    void TryOpenLeftDoor() {
        if (LeftDoorIsOpen) {
            return;
        }
        LeftDoorsAnimator.SetTrigger("TrigOpen");
    }

    void TryCloseLeftDoor()
    {
        if (!LeftDoorIsOpen)
        {
            return;
        }
        LeftDoorsAnimator.SetTrigger("TrigClose");
    }

    void TryOpenRightDoor()
    {
        if (RightDoorIsOpen)
        {
            return;
        }
        RightDoorsAnimator.SetTrigger("TrigOpen");
    }

    void TryCloseRightDoor()
    {
        if (!RightDoorIsOpen)
        {
            return;
        }
        RightDoorsAnimator.SetTrigger("TrigClose");
    }

    void ControleDoorsAnimation(char lr, char oc)
    {
        if (lr=='l' && oc=='o') { TryOpenLeftDoor(); }
        else
            if (lr == 'l' && oc == 'c') { TryCloseLeftDoor(); }
        else
            if (lr == 'r' && oc == 'o') { TryOpenRightDoor(); }
        else
            if (lr == 'r' && oc == 'c') { TryCloseRightDoor(); }
       




    }

    //void ControleDoorsAnimation(char lr, char oc)
    //{
    //    //string sr = ""+lr+oc;
    //    //switch (sr) {
    //    //    case "ro":
    //    //        break;


    //    //}


    //    if (lr == 'r')
    //    {
    //        if (oc == 'o')
    //        {
    //            if (rightFinishedanim)
    //            {
    //                rightFinishedanim = false;
    //                RightDoorsAnimator.SetTrigger("TrigOpen");
    //            }
    //        }
    //        else
    //        {
    //            if (rightFinishedanim)
    //            {
    //                rightFinishedanim = false;
    //                RightDoorsAnimator.SetTrigger("TrigClose");
    //            }
    //        }
    //    }
    //    else
    //    {
    //        if (oc == 'o')
    //        {
    //            if (lefttFinishedanim)
    //            {
    //                lefttFinishedanim = false;
    //                LeftDoorsAnimator.SetTrigger("TrigOpen");
    //            }

    //        }
    //        else
    //        {
    //            if (lefttFinishedanim)
    //            {
    //                lefttFinishedanim = false;
    //                LeftDoorsAnimator.SetTrigger("TrigClose");
    //            }
    //        }
    //    }

    //}
}
