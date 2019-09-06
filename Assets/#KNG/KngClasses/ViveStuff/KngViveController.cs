using UnityEngine;
using Valve.VR;
public class KngViveController : MonoBehaviour
{
    public SteamVR_Behaviour_Pose pose;
    public SteamVR_ActionSet m_actionSet;
    public SteamVR_Action_Boolean m_SHOOTboolAction = null;
    public SteamVR_Action_Boolean m_NextGunboolAction = null;
    public SteamVR_Action_Boolean m_NextScopeboolAction = null;
    public SteamVR_Action_Boolean m_PrevGunboolAction = null;
    public SteamVR_Action_Boolean m_PrevScopeboolAction = null;
    public ViveGunBundle RightHandBundle;
    private void OnEnable()
    {

        // m_boolAction = SteamVR_Actions._default.GrabPinch;

        m_SHOOTboolAction[SteamVR_Input_Sources.RightHand].onStateDown += OnShootGunClicked;
        m_SHOOTboolAction[SteamVR_Input_Sources.RightHand].onStateUp += OnStopShootGunClicked;

        m_NextGunboolAction[SteamVR_Input_Sources.RightHand].onStateDown += OnNextGunClicked;
        m_NextScopeboolAction[SteamVR_Input_Sources.RightHand].onStateDown += OnNextScopeClicked;
        m_PrevGunboolAction[SteamVR_Input_Sources.RightHand].onStateDown += OnPrevGunClicked;
        m_PrevScopeboolAction[SteamVR_Input_Sources.RightHand].onStateDown += OnPrevScopeClicked;

    }
    private void OnDisable()
    {
        m_SHOOTboolAction[SteamVR_Input_Sources.RightHand].onStateDown -= OnShootGunClicked;
        m_SHOOTboolAction[SteamVR_Input_Sources.RightHand].onStateUp -= OnStopShootGunClicked;

        m_NextGunboolAction[SteamVR_Input_Sources.RightHand].onStateDown -= OnNextGunClicked;
        m_NextScopeboolAction[SteamVR_Input_Sources.RightHand].onStateDown -= OnNextScopeClicked;
        m_PrevGunboolAction[SteamVR_Input_Sources.RightHand].onStateDown -= OnPrevGunClicked;
        m_PrevScopeboolAction[SteamVR_Input_Sources.RightHand].onStateDown -= OnPrevScopeClicked;

    }

    // Use this for initialization
    void Start()
    {
        m_actionSet.Activate(SteamVR_Input_Sources.Any, 0, false);
        if (pose == null)
        {
            pose = this.GetComponent<SteamVR_Behaviour_Pose>();
        }

        if (pose == null)
        {
            Debug.LogError("No SteamVR_Behaviour_Pose component found on this object");
        }

        if (m_SHOOTboolAction == null)
        {
            Debug.LogError("No ui interaction action has been set on this component.");
        }

        if (RzPlayerComponent.Instance != null)
        {
            RzPlayerComponent.Instance.SetMyHandPLZ(this.transform);
        }
        else
        {
            Debug.Log("NoRZplayer");
        }
    }

    //private void Update()
    //{
    //    if (m_SHOOTboolAction != null && m_SHOOTboolAction.GetStateDown(pose.inputSource))
    //    {
    //        print("PEWPEW");
    //    }


    //}
    private void OnShootGunClicked(SteamVR_Action_Boolean action, SteamVR_Input_Sources source) { RightHandBundle.GetActiveGunScript().GUN_FIRE(); }
    private void OnStopShootGunClicked(SteamVR_Action_Boolean action, SteamVR_Input_Sources source) { RightHandBundle.GetActiveGunScript().GUN_STOP_FIRE(); }



    private void OnNextGunClicked(SteamVR_Action_Boolean action, SteamVR_Input_Sources source)
    {

        if (GameSettings.Instance.ISimpleGunSwapn)
        {
            RightHandBundle.SwapToOtherGun();
        }
        else
        {
            //print("right");
            RightHandBundle.PlayerHand_Main_gun();
        }
    }

    private void OnPrevGunClicked(SteamVR_Action_Boolean action, SteamVR_Input_Sources source)
    {
        if (GameSettings.Instance.ISimpleGunSwapn)
        {
            RightHandBundle.SwapToOtherGun();
        }
        else
        {
            //print("left");
            RightHandBundle.PlayerHand_Secondary_gun();
        }

    }

    private void OnNextScopeClicked(SteamVR_Action_Boolean action, SteamVR_Input_Sources source)
    {
        //print("up");
        // RightHandBundle.SwitchScopesRoundRobbin();
    }

    private void OnPrevScopeClicked(SteamVR_Action_Boolean action, SteamVR_Input_Sources source)
    {
        //print("down");
        //RightHandBundle.SwitchScopesRoundRobbin();
    }

}
