using UnityEngine;

public class SceneObjectsManager : MonoBehaviour
{

    public GameObject InScenePlayer;
    public GameObject InSceneStemBase;
    public GameObject InSceneWindowBasedLand;
    public GameObject InSceneScoreSignGroup;
    public GameObject InSceneMists;
    public GameObject InSceneRZwall;
    public GameObject InSceneHOlo_UI_3d;
    public GameObject InSceneStartButton;
    public GameObject InSceneScoreBoardInScene;
    public GameObject InSceneStemTuto;

    public Transform AlphaTutoPlace;//on stem hybrid
    public Transform BravoTutoPlace;//on stem hybrid
    public Transform StartButton_PlaceHolder;
    public Transform Alpha_ScoreSignPlaceHolder;
    public Transform Bravo_ScoreSignPlaceHolder;
    public Transform Mist_PlaceHolder;
    public Transform Wall_PlaceHolder;
    public Transform Colliseum_PlaceHolder;


    public KNodeManager KnodesMNGR;
    //public StemTutoController StemTutoCTRL;
    public StartButtonControle StartButtonCTRL;
    public RoomUiCTRL RoomUI_CTRL;
    public WallCtrl Wall_CTRL;
    public MistManager MistsMNGR;
    public ScoreSignMgr ScoreSign_MNGR;


    public void Anchor_StemBase()
    {
        //if (GameSettings.Instance.IsUseHololens())
        //{
        //    HoloToolkit.Unity.WorldAnchorManager.Instance.AttachAnchor(InSceneStemBase, GameSettings.Instance.AncName_ArenaStemBase());
        //}
        //else
        //{
        //    // InSceneStemBase.transform.position = InScenePlayer.transform.position - new Vector3(0, 0.4f, 0.2f);
        //}
        GameEventsManager.Instance.Call_AnchoredGoPlaced(GameSettings.Instance.AncName_ArenaStemBase());
    }
    public void Anchor_Land()
    {
        //if (GameSettings.Instance.IsUseHololens())
        //{
        //    HoloToolkit.Unity.WorldAnchorManager.Instance.AttachAnchor(InSceneWindowBasedLand, GameSettings.Instance.AncName_WindowBasedLand());

        //}
        //else
        //{
        //    //  InSceneWindowBasedLand.transform.position = InScenePlayer.transform.position - new Vector3(0f, 0.30f, -2.9f);
        //}
        GameEventsManager.Instance.Call_AnchoredGoPlaced(GameSettings.Instance.AncName_WindowBasedLand());
    }

    public void Place_StartButton()
    {
        InSceneStartButton.transform.position = StartButton_PlaceHolder.position;
        InSceneStartButton.transform.parent = StartButton_PlaceHolder;
    }
    public void Place_StemTuto()
    {
        InSceneStemTuto.transform.position = (GameSettings.Instance.GameMode == ARZGameModes.GameLeft_Alpha) ? AlphaTutoPlace.position : BravoTutoPlace.position;
        InSceneStemTuto.transform.parent = (GameSettings.Instance.GameMode == ARZGameModes.GameLeft_Alpha) ? AlphaTutoPlace : BravoTutoPlace;
    }
    public void Place_ScoreBoard()
    {
        //   InSceneScoreSignGroup.transform.position = (GameSettings.Instance.GameMode == ARZGameModes.GameLeft_Alpha) ? Alpha_ScoreSignPlaceHolder.position : Bravo_ScoreSignPlaceHolder.position;
        //  InSceneScoreSignGroup.transform.parent = (GameSettings.Instance.GameMode == ARZGameModes.GameLeft_Alpha) ? Alpha_ScoreSignPlaceHolder : Bravo_ScoreSignPlaceHolder;
    }
    public void Place_Missts()
    {
        //InSceneMists.transform.position = Mist_PlaceHolder.transform.position;
        //InSceneMists.transform.parent = Mist_PlaceHolder.transform;
    }
    public void Place_Wall()
    {
        //InSceneRZwall.transform.position = Wall_PlaceHolder.transform.position;
        //InSceneRZwall.transform.parent = Wall_PlaceHolder.transform;
    }
}
