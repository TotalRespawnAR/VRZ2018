using UnityEngine;
using Valve.VR;

public class KngViveInputs : MonoBehaviour
{
    public SteamVR_ActionSet m_actionSet;
    public SteamVR_Action_Boolean m_SHOOTboolAction = null;
    public SteamVR_Action_Boolean m_TouchedPAdAction = null;
    public SteamVR_Action_Boolean m_SwitchGunboolAction = null;
    public SteamVR_Action_Vector2 m_DoadAction = null;
    private void OnEnable()
    {

        // m_boolAction = SteamVR_Actions._default.GrabPinch;

        m_SHOOTboolAction[SteamVR_Input_Sources.Any].onStateDown += OnShootGunClicked;
        m_TouchedPAdAction[SteamVR_Input_Sources.Any].onStateDown += OnPadTouched;
        m_TouchedPAdAction[SteamVR_Input_Sources.Any].onStateUp += OnPadUnTouched;
        m_SwitchGunboolAction[SteamVR_Input_Sources.Any].onStateDown += OnReloadGunClicked;
        m_DoadAction[SteamVR_Input_Sources.Any].onAxis += AxiserTest;
    }

    private void OnDisable()
    {
        m_SHOOTboolAction[SteamVR_Input_Sources.Any].onStateDown -= OnShootGunClicked;
        m_TouchedPAdAction[SteamVR_Input_Sources.Any].onStateDown -= OnPadTouched;
        m_TouchedPAdAction[SteamVR_Input_Sources.Any].onStateUp -= OnPadUnTouched;

        m_SwitchGunboolAction[SteamVR_Input_Sources.Any].onStateDown -= OnReloadGunClicked;
        m_DoadAction[SteamVR_Input_Sources.Any].onAxis -= AxiserTest;
    }
    // Use this for initialization
    void Start()
    {
        m_actionSet.Activate(SteamVR_Input_Sources.Any, 1, true);
    }

    // Update is called once per frame
    //void Update()
    //{
    //    ////works when Public m_SHOOTboolAction is set to knggun1\in\ShootGunClickAction
    //    //if (m_SHOOTboolAction.GetStateDown(SteamVR_Input_Sources.Any))
    //    //{
    //    //    print("111");
    //    //}
    //    ////works when Public m_SHOOTboolAction is set to knggun1\in\ShootGunClickAction
    //    //if (m_SHOOTboolAction[SteamVR_Input_Sources.Any].stateDown)
    //    //{
    //    //    print("2222");
    //    //}




    //    ////doesnt care about any public settings. this is the trigger getting pulled 
    //    //if (SteamVR_Actions.KngGuns1.ShootGunClickAction.GetStateDown(SteamVR_Input_Sources.Any))
    //    //{
    //    //    print("444");
    //    //}
    //}
    //works when Public m_SHOOTboolAction is set to knggun1\in\ShootGunClickAction

    bool isTouching;
    private void OnShootGunClicked(SteamVR_Action_Boolean action, SteamVR_Input_Sources source) { print("pew"); }
    private void OnPadTouched(SteamVR_Action_Boolean action, SteamVR_Input_Sources source) { isTouching = true; }
    private void OnPadUnTouched(SteamVR_Action_Boolean action, SteamVR_Input_Sources source) { isTouching = false; }

    private void OnReloadGunClicked(SteamVR_Action_Boolean action, SteamVR_Input_Sources source) { print("switch"); }

    int GunNumber = 0;

    private void AxiserTest(SteamVR_Action_Vector2 action, SteamVR_Input_Sources source, Vector2 axis, Vector2 delta)
    {
        // print(action.axis.ToString());

        if (isTouching)
        {
            if (delta.x < -0.1f) { GunNumber = 1; }
            else
                   if (delta.x > 0.1f) { GunNumber = 2; }

        }
        else
        {
            print("selected " + GunNumber);
        }
    }

}
