// @Author Nabil Lamriben ©2018
using UnityEngine;

public class MainHandScript : MonoBehaviour
{
    public Transform MyBundleBone;
    //----------------------------------------------InitializedThisHand

    //**************************************
    //    Gun or Mag bundel
    //*************************************
    public IBundle MyBun;

    //**************************************
    //     Ainimatior for each hand object
    //*************************************
    public HandAnimatorCTRL _AnmController;


    //----------------------------------------------X InitializedThisHand

    public void InitializedThisHand(IBundle argIBun)
    {

        MyBun = argIBun;
        _AnmController = this.gameObject.GetComponent<HandAnimatorCTRL>();

        //if (Mytype == ARZHandType.HandGun)
        //{
        _AnmController.SetHAndType(1); //old way use to set animator depending on had being main or off hand so that the hand can animate holing different clips and guns

        //}
        // else { _AnmController.SetHAndType(2); }
    }
    // public ARZHandType GetHandType() { return this.Mytype; }

    public void Equip_byGunType(GunType argguntype)
    {

        MyBun.SetMyCurrBunThing(argguntype);
        MyBun.ShowMyCurrBunThing();
        _AnmController.MainHandAnimateHoldGun(argguntype);
    }




}
