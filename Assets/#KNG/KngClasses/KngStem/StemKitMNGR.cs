//#define STRIKER_TEST
// @Author Nabil Lamriben ©2018
//using SixenseCore;
using UnityEngine;
public class StemKitMNGR : MonoBehaviour
{
    // .0028 .073 -.14
    Transform A_CtrlPos;
    Transform B_CtrlPos;


    GunsBundle MNGR_GunsBundle;
    MagsBundle MNGR_MagsBundle;

    GameObject MNGR_MAIN_Hand;
    GameObject MNGR_OtherPlayer_Hand;
    //GameObject MNGR_OFF_Hand;
    //  public bool useStem;




    IPlayerHandsCTRL _playerHandsController;
    StemHandsFactory _myHandsFactory;


    #region Events
    //public delegate void STEMCONNECTED(TrackerID argid);
    //public static event STEMCONNECTED OnSTEMCONNECTED;
    //public static void StemObjectConnected(TrackerID argid)
    //{
    //    if (OnSTEMCONNECTED != null) OnSTEMCONNECTED(argid);
    //}

    //public delegate void STEMDISCONNECTED(TrackerID argid);
    //public static event STEMDISCONNECTED OnSTEM_DIS_CONNECTED;
    //public static void StemObject_dis_Connected(TrackerID argid)
    //{
    //    if (OnSTEM_DIS_CONNECTED != null) OnSTEM_DIS_CONNECTED(argid);
    //}








    //======================================================================================
    //public delegate void EVENT_UIcell_FILLED(int id);
    //public static event EVENT_UIcell_FILLED OnUICellFilled;
    //public static void CALL_UICELLFilled(int id)
    //{
    //    if (OnUICellFilled != null) OnUICellFilled(id);
    //}

    //public delegate void EVENT_START_UIcell(int id);
    //public static event EVENT_START_UIcell On_START_Uicell;
    //public static void CALL_Start_UIcell(int id)
    //{
    //    if (On_START_Uicell != null) On_START_Uicell(id);
    //}


    //public delegate void EVENT_Override_UICELLID(int id);
    //public static event EVENT_Override_UICELLID On_Override_UICellid;
    //public static void Call_OVR_Cell_ID(int id)
    //{
    //    if (On_Override_UICellid != null) On_Override_UICellid(id);
    //}
    //======================================================================================


    //****************************************************************************
    // Use this for initialization
    //****************************************************************************


    public delegate void EVENTVIBRATE(int x, float zeroone);
    public static event EVENTVIBRATE OnVibrate;
    public static void CALL_VIBRATECONTROLLERG(int x, float zeroone)
    {
        if (OnVibrate != null) OnVibrate(x, zeroone);
    }


    //public delegate void EVENTALLOWEDGUNINDEX(int x);
    //public static event EVENTALLOWEDGUNINDEX OnUpdateAvailableGUnIndex;
    //public static void CALL_UpdateAvailableGUnIndex(int x)
    //{
    //    if (OnUpdateAvailableGUnIndex != null) OnUpdateAvailableGUnIndex(x);
    //}

    //public delegate void EVENTRESTGUNANDMETER();
    //public static event EVENTRESTGUNANDMETER OnResetGunAndMeter;
    //public static void CALL_ResetGunAndMeter()
    //{
    //    if (OnResetGunAndMeter != null) OnResetGunAndMeter();
    //}



    public delegate void EVENTTRACKERSLOCATED(bool argonoff);
    public static event EVENTTRACKERSLOCATED OnStartFindingTrackers;
    public static void CALL_TrackersTransformFoundAndBundlePlaced(bool argonoff)
    {
        if (OnStartFindingTrackers != null) OnStartFindingTrackers(argonoff);
    }

    //public delegate void EVENTALLOWEXTRABUTTONS(bool argonoff);
    //public static event EVENTALLOWEXTRABUTTONS OnToggleONOFFExtraButtons;
    //public static void CALL_ToggleAllowExtraButtons(bool argonoff)
    //{
    //    if (OnToggleONOFFExtraButtons != null) OnToggleONOFFExtraButtons(argonoff);
    //}

    #endregion


    public GameObject StrikerModel;


    void Start()
    {

        _myHandsFactory = GetComponent<StemHandsFactory>();
        _playerHandsController = GetComponent<IPlayerHandsCTRL>();
        //#if STRIKER_TEST
        //        useStem = true;
        //        StrikerModel.SetActive(true);

        //#else
        //        useStem = GameSettings.Instance.IsUseHololens(); //settings must be in secene before load
        //#endif

        if (GameSettings.Instance._controlertype == ARZControlerType.StemControlSystem)
        {
            StrikerModel.SetActive(false);
        }
        InitHandsBundles();
        //if (!useStem)
        //{
        //    // this.transform.parent = Camera.main.transform;
        //    // this.transform.localPosition -= new Vector3(2.1f, -2.6f, 2.20f);
        //}
    }


    void FullINit()
    {

        Find_StemTrackerandControllerPositions();
#if !STRIKER_TEST

        FindBundles();


        if (GameSettings.Instance.GameMode == ARZGameModes.GameLeft_Alpha)
        {
            // Debug.Log("full initleftalpha");
            if (GameSettings.Instance.PlayerLeftyRight == ARZPlayerLeftyRighty.RightyPlayer)
            {

                SetupAlphaSide(true);
            }
            else
            {
                SetupAlphaSide(false);
            }
        }
        if (GameSettings.Instance.GameMode == ARZGameModes.GameRight_Bravo)
        {
            //Debug.Log("full init RightBravo");
            if (GameSettings.Instance.PlayerLeftyRight == ARZPlayerLeftyRighty.RightyPlayer)
            {
                SetupBravoSide(true);
            }
            else
            {
                SetupBravoSide(false);
            }
        }
        if (GameSettings.Instance.Controlertype == ARZControlerType.StemControlSystem)
        {
        }
        _playerHandsController.InitPlayerHandScripts(MNGR_MAIN_Hand.GetComponent<MainHandScript>());
        //DissAllowPlayerFromUseingStemInputs();
        GameEventsManager.CALL_ToggleStemInput(false);
#endif

    }


    void InitHandsBundles()
    {
        FullINit();
        CALL_TrackersTransformFoundAndBundlePlaced(true);
    }

    //****************************************************************************
    Transform DeepSearch(Transform parent, string val)
    {
        foreach (Transform c in parent)
        {
            if (c.name == val) { return c; }
            var result = DeepSearch(c, val);
            if (result != null)
                return result;
        }
        return null;
    }

    void Find_StemTrackerandControllerPositions()
    {
        if (GameSettings.Instance.Controlertype != ARZControlerType.StemControlSystem) return;
        A_CtrlPos = DeepSearch(this.transform, "AlphaCTRL_HandPos");
        if (A_CtrlPos == null)
        {
            //Debug.Log("did not find hand pos" + "AlphaCTRL_HandPos");
        }
        B_CtrlPos = DeepSearch(this.transform, "BravoCTRL_HandPos");
        if (B_CtrlPos == null)
        {
            //  Debug.Log("did not find hand pos" + "BravoCTRL_HandPos");
        }
    }


    void FindBundles()
    {
        if (GameSettings.Instance.Controlertype != ARZControlerType.StemControlSystem) return;

        MNGR_GunsBundle = GetComponentInChildren<GunsBundle>();
        MNGR_MagsBundle = GetComponentInChildren<MagsBundle>();

        if (MNGR_MagsBundle == null)
        {
            // Debug.Log("no mags bundur found");
        }
        if (MNGR_GunsBundle == null)
        {
            //  Debug.Log("no guns bundur found");
        }

    }

    void SetupAlphaSide(bool argRighty)
    {
        SetupSide(argRighty, A_CtrlPos, B_CtrlPos);
    }

    void SetupBravoSide(bool argRighty)
    {
        SetupSide(argRighty, B_CtrlPos, A_CtrlPos);
    }

    void SetupSide(bool argRighty, Transform argCTRL, Transform argOtherCTRL)
    {
        if (GameSettings.Instance.Controlertype != ARZControlerType.StemControlSystem) return;

        MNGR_MAIN_Hand = _myHandsFactory.FactoryBuild_MainHand(argRighty, argCTRL, MNGR_GunsBundle);
        if (GameSettings.Instance.GAmeSessionType == ARZGameSessionType.UDP)
        {
            MNGR_OtherPlayer_Hand = _myHandsFactory.FactoryBuild_OtherPlayer_Hand(argRighty, argOtherCTRL);
        }
        //  MNGR_OFF_Hand = _myHandsFactory.FactoryBuild_OffHand(argRighty, argTRK,MNGR_MagsBundle);
        RzPlayerComponent.Instance.SetMyHandPLZ(MNGR_MAIN_Hand.transform);
    }

    //  public Transform GetPlayerMainHandTrans() { return MNGR_MAIN_Hand.transform; }
    //void SetupSide(bool argRighty, Transform argCTRL, Transform argOtherCTRL)
    //{
    //    //if (GameSettings.Instance.Controlertype != ARZControlerType.StemControlSystem) return;
    //    MNGR_OtherPlayer_Hand = _myHandsFactory.FactoryBuild_OtherPlayer_Hand(argRighty, argOtherCTRL);
    //}

}
