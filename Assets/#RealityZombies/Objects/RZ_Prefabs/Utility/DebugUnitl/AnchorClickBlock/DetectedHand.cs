using UnityEngine;
#if !UNITY_EDITOR && UNITY_WSA
         
using UnityEngine.XR.WSA.Input;
#endif

public class DetectedHand : MonoBehaviour
{

    public DetectedHandSubject ObjInHand;
    public bool isHoldingSmthin;
    private void OnEnable()
    {
#if !UNITY_EDITOR && UNITY_WSA
         
        InteractionManager.InteractionSourcePressed += GetPressPos;
        InteractionManager.InteractionSourceDetected += GetDetectedPos;
        InteractionManager.InteractionSourceUpdated += GetUpdateddPos;
#endif
    }
    private void OnDisable()
    {
#if !UNITY_EDITOR && UNITY_WSA
         
        InteractionManager.InteractionSourcePressed -= GetPressPos;
        InteractionManager.InteractionSourceDetected -= GetDetectedPos;
        InteractionManager.InteractionSourceUpdated -= GetUpdateddPos;
#endif
    }

#if !UNITY_EDITOR && UNITY_WSA
         
    void GetPressPos(InteractionSourcePressedEventArgs arg)
    {
        //Vector3 pos;
        //if (arg.state.sourcePose.TryGetPosition(out pos))
        //{
        //    box.transform.position = pos;
        //}
        // arg.state.headPose
        Debug.Log("Pressed");
        if (ObjInHand != null)
        {
            ObjInHand.DetachFromHand();
            ObjInHand = null;
            isHoldingSmthin = false;
        }
    }


    void GetDetectedPos(InteractionSourceDetectedEventArgs arg)
    {
        //Vector3 pos;
        //if (arg.state.sourcePose.TryGetPosition(out pos))
        //{
        //    box.transform.position = pos;
        //}
        // arg.state.headPose
        Debug.Log("det");
    }



    void GetUpdateddPos(InteractionSourceUpdatedEventArgs arg)
    {
        Vector3 pos;
        if (arg.state.sourcePose.TryGetPosition(out pos))
        {
            this.transform.position = pos;
        }
        // arg.state.headPose
    }
#endif

}
