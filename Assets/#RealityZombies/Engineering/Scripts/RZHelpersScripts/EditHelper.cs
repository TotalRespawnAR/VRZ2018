using UnityEngine;
using UnityEngine.SceneManagement;

public class EditHelper : MonoBehaviour
{

    public DevRoomManager DevRoomManager;

    void Start()
    {

    }
    public void DoHideButtons() { }

    public void DoStartObserver()
    {
        //  SpatialMappingManager.Instance.StartObserver();
    }

    public void DoStoptObserver()
    {
        //SpatialMappingManager.Instance.StopObserver();
        // SpatialMappingManager.Instance.CleanupObserver();

    }

    public void DoPlaceStem()
    {
        // WorldAnchorManager.Instance.AttachAnchor(DevRoomManager.devStemStation, GameSettings.Instance.AncName_ArenaStemBase());
    }


    public void DoObfuscateRoom() { DevRoomManager.DoOcclude_CGS_RoomMesh(); }
    public void DoShowRoom() { DevRoomManager.DoShow_CGS_RoomMesh(); }


    public void DoGoToGameSingle()
    {
        if (GameSettings.Instance.IsKingstonOn)
        {
            SceneManager.LoadScene("GameSoloKingston");
        }
        else
        {
            SceneManager.LoadScene("GameSolo");
        }
    }




}
