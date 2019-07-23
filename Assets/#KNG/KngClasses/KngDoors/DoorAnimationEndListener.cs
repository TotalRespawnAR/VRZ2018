using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimationEndListener : MonoBehaviour {

    public void OnDoorLeftFinishedOpenAnimation() {
        _Dc.LeftDoorIsOpen = true;
    }
    public void OnDoorLeftFinishedCloseAnimation()
    {
        _Dc.LeftDoorIsOpen = false;
    }
    public void OnDoorRightFinishedOpenAnimation() {
        _Dc.RightDoorIsOpen = true;
    }

    public void OnDoorRightFinishedCloseAnimation()
    {
        _Dc.RightDoorIsOpen = false;
    }

    DoorsController _Dc;
    private void Awake()
    {
        _Dc = GetComponentInParent<DoorsController>();
    }
}
