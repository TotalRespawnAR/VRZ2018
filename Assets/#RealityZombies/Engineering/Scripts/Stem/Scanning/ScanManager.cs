// @Author Jeffrey M. Paquette ©2016

using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEngine.VR.WSA.Persistence;
//using UnityEngine.VR.WSA;

public class ScanManager : MonoBehaviour
{

    RoomSaver roomSaver;

    // Use this for initialization
    void Start()
    {
        roomSaver = gameObject.GetComponent<RoomSaver>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SaveRoom()
    {
        if (roomSaver != null)
        {
            Logger.Debug("Saved in scan manager");
#if !UNITY_EDITOR && UNITY_WSA
         

            roomSaver.SaveRoom();
#endif

        }

    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
