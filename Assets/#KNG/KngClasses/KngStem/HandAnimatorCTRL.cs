using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAnimatorCTRL : MonoBehaviour
{

    Animator _anim;

 
    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    public void SetHAndType(int argIsMainGunHand)
    {
        _anim.SetInteger("AnimHandType", argIsMainGunHand);

    }

    //stemplayerhandctrl will set this when gun state change
    public void MainHandAnimateHoldGun(GunType newguntype)
    {
        int gt = (int)newguntype;

        if (gt > 4) gt = 1;//cuz i dont have any hand poses for set2guns , i default to magnumhold
        _anim.SetInteger("AnimGunType", gt);
    }



    public void AnyHandAnimHOld(int Index)
    {
        MainHandAnimateHoldGun((GunType)Index);
    }

    public void DoFireAnim()
    {
        _anim.SetTrigger("TrigFireRot");
    }
    public void DoOpenHandAnim()
    {
        _anim.Play("aHand_Opened");
    }

    public void DoTrigHoldAmmo()
    {
        _anim.SetTrigger("TrigHoldAmmo");
    }

}
