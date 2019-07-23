// @Author Nabil Lamriben ©2018
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StemHandsFactory : MonoBehaviour
{
    public GameObject MainHandObj_flipped;
    public GameObject MainHandObj;
    public GameObject OtherPlayerHandObj;

    GameObject Factory_MAIN_Hand;
    GameObject Factory_OtherPlayer_Hand;

    public GameObject FactoryBuild_MainHand(bool argIsRightySetup, Transform StemObjTransform, GunsBundle argGunsBun)
    {

        if (argIsRightySetup)
        {
            Factory_MAIN_Hand = Instantiate(MainHandObj, StemObjTransform.position, StemObjTransform.rotation) as GameObject;
        }

        else
        {
            Factory_MAIN_Hand = Instantiate(MainHandObj_flipped, StemObjTransform.position, StemObjTransform.rotation) as GameObject;
            Factory_MAIN_Hand.transform.position = new Vector3(StemObjTransform.parent.position.x, StemObjTransform.position.y, StemObjTransform.position.z);
        }

        Factory_MAIN_Hand.name = "PlayerShootyHand";
        Factory_MAIN_Hand.transform.parent = StemObjTransform.transform;
        Place_GUN_bunObj(argGunsBun);
        Factory_MAIN_Hand.GetComponent<MainHandScript>().InitializedThisHand(argGunsBun);
        return Factory_MAIN_Hand;
    }
    public GameObject FactoryBuild_OtherPlayer_Hand(bool argIsRightySetup, Transform StemObjTransform)
    {

        if (argIsRightySetup)
        {
            Factory_OtherPlayer_Hand = Instantiate(OtherPlayerHandObj, StemObjTransform.position, StemObjTransform.rotation) as GameObject;
        }

        else
        {
            Factory_OtherPlayer_Hand = Instantiate(OtherPlayerHandObj, StemObjTransform.position, StemObjTransform.rotation) as GameObject;
            Factory_OtherPlayer_Hand.transform.position = new Vector3(StemObjTransform.parent.position.x, StemObjTransform.position.y, StemObjTransform.position.z);
        }

        Factory_OtherPlayer_Hand.name = "OtherPlayerHand";
        Factory_OtherPlayer_Hand.transform.parent = StemObjTransform.transform;
        return Factory_OtherPlayer_Hand;
    }
    void Place_GUN_bunObj(GunsBundle _argGunsBun)
    {
        Transform _gungrip = Factory_MAIN_Hand.GetComponent<MainHandScript>().MyBundleBone;
        _argGunsBun.transform.position = _gungrip.position;
        _argGunsBun.transform.rotation = _gungrip.rotation;
        _argGunsBun.transform.parent = _gungrip;
    }


}

