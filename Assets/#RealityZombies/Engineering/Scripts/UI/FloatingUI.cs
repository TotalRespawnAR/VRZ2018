using UnityEngine;

public class FloatingUI : MonoBehaviour
{

    public GameObject RoomEs;
    bool RooEsIsVisible = false;

    /// <summary>
    /// OnClicking FloatingUI."ToggleDevRoom BUTTON" in SetupScene
    /// </summary>
    public void Floating_UI_ToggleDevRoom()
    {
        RooEsIsVisible = !RooEsIsVisible;

        if (RooEsIsVisible)
        {
            RoomEs.SetActive(true);
        }
        else
        {
            RoomEs.SetActive(false);
        }
    }


    /// <summary>
    /// OnClicking FloatingUI."GAME BUTTON" in SetupScene
    /// </summary>
    public void Floating_UI_OpenGameScene()
    {
        GameSettings.Instance.Backroom = ARZroom.HOSPITAL;
        GameSettings.Instance.IsTournamentModeOn = false;
        //SingletonKiller.Instance.SingKill_GoToGameScene();
    }

    public void Floating_UI_OpenApocalypseScene()
    {
        GameSettings.Instance.Backroom = ARZroom.HOSPITAL;
        GameSettings.Instance.IsTournamentModeOn = true;
        //SingletonKiller.Instance.SingKill_GoToGameScene();
    }


    public void Floating_UI_GameDesert()
    {
        GameSettings.Instance.Backroom = ARZroom.DESERT;
        GameSettings.Instance.IsTournamentModeOn = false;
        // SingletonKiller.Instance.SingKill_GoToGameScene();
    }

    public void Floating_UI_ApocalypseDEsertr()
    {
        GameSettings.Instance.Backroom = ARZroom.DESERT;
        GameSettings.Instance.IsTournamentModeOn = true;
        //SingletonKiller.Instance.SingKill_GoToGameScene();
    }




    public void Floating_UI_GameForest()
    {
        GameSettings.Instance.Backroom = ARZroom.FOREST;
        GameSettings.Instance.IsTournamentModeOn = false;
        // SingletonKiller.Instance.SingKill_GoToGameScene();
    }

    public void Floating_UI_ApocalypseFOrest()
    {
        GameSettings.Instance.Backroom = ARZroom.FOREST;
        GameSettings.Instance.IsTournamentModeOn = true;
        //  SingletonKiller.Instance.SingKill_GoToGameScene();
    }

    public void Floating_UI_APocalypse_RoomSelection()
    {
        GameSettings.Instance.IsTournamentModeOn = true;
        //SingletonKiller.Instance.SingKill_GoTO_LobbyRoomSelection();
    }

    public void Floating_UI_Arcade_RoomSelection()
    {
        GameSettings.Instance.IsTournamentModeOn = false;
        //SingletonKiller.Instance.SingKill_GoTO_LobbyRoomSelection();
    }

    /// <summary>
    /// OnClicking FloatingUI."CALIB BUTTON" in SetupScene
    /// </summary>
    public void Floating_UI_OpenCalibtaionScene()
    {
        //SingletonKiller.Instance.SingKill_GoTOCalibtaion();
    }


    GameObject _chelper;
    private void Start()
    {
        _chelper = GameObject.FindGameObjectWithTag("CursorTag");
        if (_chelper == null)
        {
            Logger.Debug("XXXXXXXXXXX did not find Cursor in game");
        }
        else
        {
            _chelper.transform.GetChild(0).gameObject.SetActive(true);
        }
    }


}
