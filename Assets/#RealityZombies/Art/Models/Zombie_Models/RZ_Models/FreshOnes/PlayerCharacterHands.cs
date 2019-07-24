using UnityEngine;

public class PlayerCharacterHands : MonoBehaviour
{
    public Transform HandTargetLeft;
    public Transform HandTargetRight;

    public Animator animator;

    public bool ikActive = true;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
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
        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1f);
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);


        animator.SetIKPosition(AvatarIKGoal.LeftHand, HandTargetLeft.position);
        animator.SetIKRotation(AvatarIKGoal.LeftHand, HandTargetLeft.rotation);


        animator.SetIKPosition(AvatarIKGoal.RightHand, HandTargetRight.position);
        animator.SetIKRotation(AvatarIKGoal.RightHand, HandTargetRight.rotation);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
