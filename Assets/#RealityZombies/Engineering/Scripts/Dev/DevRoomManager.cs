using UnityEngine;

public class DevRoomManager : MonoBehaviour
{

    public BackRoomMNGR _RoomDesert;
    public BackRoomMNGR _RoomCreepy;
    public BackRoomMNGR _RoomForest;

    public DropPointsManager DropPointsmngr;
    public GravesManager Gravesmngr;


    //  public GameObject RoomObjectCambridge;
    public GameObject RoomObjectKingston;
    public Material OcclusionMAt;
    public GameObject devStemStation;
    // public GameObject devStrikerStemStation;
    public GameObject LANESObj;

    public EaterLayerWatcherMNGR EaterLayerWatcher;

    public GameObject devScoreBoard;
    public ComPuckWeapons ComWeapons;

    //public GameObject devSpawnPoint1;
    //public GameObject devSpawnPoint2;
    //public GameObject devSpawnPoint3;
    //public GameObject devSpawnPoint4;
    //public GameObject devSpawnPoint5;
    //public GameObject devSpawnPoint6;
    //public GameObject devSpawnPoint7;
    //public GameObject devSpawnPoint8;
    //public GameObject devSpawnPoint9;
    //public GameObject devSpawnPoint10;
    //public GameObject devSpawnPoint11;


    //ALPHA SIDE OBJECTS
    public GameObject Hotspot_Alpha;
    public Transform A_ScoreBoardPos;


    //BRAVO SIDE OBJECTS
    public GameObject Hotspot_Bravo;
    public Transform B_ScoreBoardPos;
    public Transform Alpha_Point;
    public Transform Brabo_Point;


    public GameObject devBrickWall;
    public GameObject devStartButton;
    //private GameObject _devstation;

    // public GameObject DevStationToUse() { return _devstation; }
    public GameObject devTarget;

    public Transform PlaceHolder_BrickWall;
    public Transform PlaceHolder_Target;
    public Transform PlaceHolder_StartButton;

    Renderer RoomRenderer;
    Material[] originalMaterials;
    ////List<GameObject> RoomElements;
    Material[] occlusionMaterials;



    private void OnEnable()
    {
        GameEventsManager.OnGameObjectAnchoredPlaced += ListenTo_AnchoresPlaced;
    }
    private void OnDisable()
    {
        GameEventsManager.OnGameObjectAnchoredPlaced -= ListenTo_AnchoresPlaced;
    }

    private void Awake()
    {
        Debug.Log("DevRoomManager is on" + gameObject.name);

        //if (GameSettings.Instance.Controlertype == ARZControlerType.StemControlSystem)
        //{
        //    devStrikerStemStation.SetActive(false);
        //    _devstation = devStemStation;
        //}
        //else
        //{
        //    devStemStation.SetActive(false);
        //    _devstation =  devStrikerStemStation;
        //}

        //if (GameSettings.Instance.IsKingstonOn) {

        originalMaterials = new Material[RoomObjectKingston.transform.childCount];
        occlusionMaterials = new Material[RoomObjectKingston.transform.childCount];

        for (int x = 0; x < RoomObjectKingston.transform.childCount; x++)
        {

            originalMaterials[x] = RoomObjectKingston.transform.GetChild(x).gameObject.GetComponent<MeshRenderer>().materials[0];
            occlusionMaterials[x] = OcclusionMAt;
        }

        // RoomObjectCambridge.SetActive(false);
        //}

        //ifOldCambridge
        //else {

        //    originalMaterials = new Material[RoomObjectCambridge.transform.childCount];
        //    occlusionMaterials = new Material[RoomObjectCambridge.transform.childCount];

        //    for (int x = 0; x < RoomObjectCambridge.transform.childCount; x++)
        //    {

        //        originalMaterials[x] = RoomObjectCambridge.transform.GetChild(x).gameObject.GetComponent<MeshRenderer>().materials[0];
        //        occlusionMaterials[x] = OcclusionMAt;
        //    }

        //    RoomObjectKingston.SetActive(false);
        //}


    }
    void Start()
    {
        if (GameSettings.Instance.IsUseHololens() == false)
        {
            Debug.Log("ok yall, this is just in editor, so detupsides on start");
            DoSetupSides_and_PregameObjects(); //lol , used to not be able to call this on ostart, the grid map's recursive node generation would take way long. i had to call a gridmapFinished event to trigget this. otherwise, the brick wall would be placed too fast, and the gridmap would only build half the map... loooooolz
        }
        else
        {
            Debug.Log("yooo kiiid, start sarted , and this IS holomode.. wait for event to setupsides");

        }
        //esle let the event handeler OnAnchoredGoPlaed decide when to fire DoSetupsides. ps:it will also place the wall

    }

    void ListenTo_AnchoresPlaced(string argPlacedAnhorName)
    {
        Debug.Log("heard Anchor" + argPlacedAnhorName + " was just found and placed");
        if (argPlacedAnhorName == GameSettings.Instance.GetAnchorName_RoomModel())
        {
            Debug.Log("eeeekk !!!!!!!!!! i must setupsides");
            DoSetupSides_and_PregameObjects();
        }
    }
    void DoSetupSides_and_PregameObjects()
    {
        if (GameSettings.Instance.GameMode == ARZGameModes.GameLeft_Alpha)
        {
            SetupScoreBoardAlpha();
        }
        else
             if (GameSettings.Instance.GameMode == ARZGameModes.GameRight_Bravo)
        {
            SetupScoreBoardBravo();
        }

        SetupPregameWAll();
    }

    void SetupPregameWAll()
    {
        if (GameSettings.Instance.PregmeType == ARZPregameType.BRICKWALL)
        {

            devBrickWall.transform.position = PlaceHolder_BrickWall.transform.position;
            devTarget.transform.position = PlaceHolder_Target.transform.position;
        }
        //else
        //    devBrickWall.transform.position = PlaceHolder_BrickWall.transform.position;

    }
    public void PLace_THE_BRICKWALL()
    {

        devBrickWall.transform.position = PlaceHolder_BrickWall.transform.position;
    }
    void SetupScoreBoardAlpha()
    {
        devScoreBoard.transform.position = A_ScoreBoardPos.transform.position;
        devScoreBoard.transform.localRotation = A_ScoreBoardPos.transform.localRotation;
    }

    void SetupScoreBoardBravo()
    {
        devScoreBoard.transform.position = B_ScoreBoardPos.transform.position;
        devScoreBoard.transform.localRotation = B_ScoreBoardPos.transform.localRotation;
    }


    public void DoOcclude_CGS_RoomMesh()
    {
        // if (GameSettings.Instance.IsKingstonOn) {
        for (int x = 0; x < RoomObjectKingston.transform.childCount; x++)
        {
            RoomObjectKingston.transform.GetChild(x).gameObject.GetComponent<MeshRenderer>().materials = occlusionMaterials;
        }
        // }

        //else {
        //    for (int x = 0; x < RoomObjectCambridge.transform.childCount; x++)
        //    {
        //        RoomObjectCambridge.transform.GetChild(x).gameObject.GetComponent<MeshRenderer>().materials = occlusionMaterials;
        //    }
        //}

    }

    public void DoShow_CGS_RoomMesh()
    {
        RoomRenderer.materials = originalMaterials;
    }

    public BackRoomMNGR GetActiveBackRoom(string RoomName)
    {
        if (RoomName == "creepy")
        {
            _RoomCreepy.gameObject.SetActive(true);
            _RoomDesert.gameObject.SetActive(false);
            _RoomForest.gameObject.SetActive(false);
            return _RoomCreepy;
        }
        else if (RoomName == "desert")
        {
            _RoomDesert.gameObject.SetActive(true);
            _RoomCreepy.gameObject.SetActive(false);
            _RoomForest.gameObject.SetActive(false);
            return _RoomDesert;
        }
        else
        {
            _RoomForest.gameObject.SetActive(true);
            _RoomCreepy.gameObject.SetActive(false);
            _RoomDesert.gameObject.SetActive(false);

            return _RoomForest;
        }


    }
}
