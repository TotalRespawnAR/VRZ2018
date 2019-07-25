﻿using UnityEngine;

public class PlayerCharacterHands : MonoBehaviour
{
    public Transform HandTargetLeft;
    public Transform HandTargetRight;

    public Transform ElbowTargetLeft;
    public Transform ElbowTargetRight;

    public Transform ChestBone;
    public Transform ChestTarget;
    Transform _headTransTarg;

    public Animator animator;
    public Transform StemHead;
    public Transform SphereHead;
    public bool ikActive = true;

    float originalHeightWithoffset;
    float offest = 1.071649f;
    float curHeadHeightWithoffset;

    // Start is called before the first frame update
    void Start()
    {
        if (useStemHead) { _headTransTarg = StemHead; } else { _headTransTarg = SphereHead; }
        Angle = 90;
        animator = GetComponent<Animator>();
        ChestBone = animator.GetBoneTransform(HumanBodyBones.Spine);
        originalHeightWithoffset = _headTransTarg.transform.position.y - offest;
        curHeadHeightWithoffset = originalHeightWithoffset;
    }

    //a callback for calculating IK
    //void OnAnimatorIK(int layerIndex)
    //{
    //    if (animator)
    //    {

    //        //if the IK is active, set the position and rotation directly to the goal. 
    //        if (ikActive)
    //        {

    //            // Set the look target position, if one has been assigned
    //            //if (lookObj != null)
    //            //{
    //            //    animator.SetLookAtWeight(1);
    //            //    animator.SetLookAtPosition(lookObj.position);
    //            //}

    //            // Set the right hand target position and rotation, if one has been assigned
    //            if (HandTargetRight != null)
    //            {
    //                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
    //                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
    //                animator.SetIKPosition(AvatarIKGoal.RightHand, HandTargetRight.position);
    //                animator.SetIKRotation(AvatarIKGoal.RightHand, HandTargetRight.rotation);
    //            }

    //            if (HandTargetLeft != null)
    //            {
    //                animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
    //                animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
    //                animator.SetIKPosition(AvatarIKGoal.LeftHand, HandTargetLeft.position);
    //                animator.SetIKRotation(AvatarIKGoal.LeftHand, HandTargetLeft.rotation);
    //            }

    //        }

    //        //if the IK is not active, set the position and rotation of the hand and head back to the original position
    //        else
    //        {
    //            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
    //            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
    //            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
    //            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);
    //            animator.SetLookAtWeight(0);
    //        }
    //    }
    //}

    void OnAnimatorIK(int layerIndex)
    {
        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, Magnetizm);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, Magnetizm);
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, MagnetizmR);
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, MagnetizmR);




        animator.SetIKPosition(AvatarIKGoal.LeftHand, HandTargetLeft.position);
        animator.SetIKRotation(AvatarIKGoal.LeftHand, HandTargetLeft.rotation);


        animator.SetIKPosition(AvatarIKGoal.RightHand, HandTargetRight.position);
        animator.SetIKRotation(AvatarIKGoal.RightHand, HandTargetRight.rotation);
    }
    public int Xyz;
    public bool neg = true;
    public bool uselookat = true;
    float multiplyer = 1f;
    public float Angle = 90;
    public float HeadHeightCheck;
    public bool useStemHead;
    float curdistToGun = 10f;
    public float ShowCurDist;
    public float Magnetizm = 0;
    public float MagnetizmR = 0;
    private void Update()
    {
        curdistToGun = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(HandTargetRight.position.x, 0, HandTargetRight.position.z));
        ShowCurDist = curdistToGun;

        if (curdistToGun > 2.5f)
        {
            Magnetizm = 0;
        }
        else if (curdistToGun < 2.5f && curdistToGun > .5f)
        {
            Magnetizm = ((2f - (curdistToGun - 2f))) - 2f;

        }
        else
      if (curdistToGun < 0.5f)
        {
            Magnetizm = 1f;
        }

        MagnetizmR = Magnetizm;
        if (MagnetizmR > 0.6f) MagnetizmR = 0.6f;

        // Magnetizm = 0.5f;

    }
    // Update is called once per frame
    void LateUpdate()
    {
        curHeadHeightWithoffset = _headTransTarg.transform.position.y - offest;


        HeadHeightCheck = curHeadHeightWithoffset;
        //ChestBone.localRotation = ChestTarget.rotation;
        animator.SetFloat("HeadHeight", curHeadHeightWithoffset);
        animator.SetFloat("Xval", _headTransTarg.position.x);
        animator.SetFloat("Yval", _headTransTarg.position.z);

        transform.position = new Vector3(_headTransTarg.position.x, 0, _headTransTarg.position.z);
        //ChestBone.Rotate(0, -90, 0);
        if (uselookat)
        {
            ChestBone.LookAt(ChestTarget);
            if (neg) { multiplyer = -1f; }
            else { multiplyer = 1f; }

            if (Xyz < 0 || Xyz > 3)
            {

                return;
            }

            if (Xyz == 0)
            {
                ChestBone.Rotate(Angle * multiplyer, 0, 0);
            }
            else
                if (Xyz == 1)
            {
                ChestBone.Rotate(0, Angle * multiplyer, 0);
            }
            else

            if (Xyz == 2)
            {
                ChestBone.Rotate(0, 0, Angle * multiplyer);
            }


        }
        else
        {
            ChestBone.localRotation = ChestTarget.rotation;

        }

    }
}
