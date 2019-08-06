using UnityEngine;

public class Player2Spawner : MonoBehaviour
{
    GameObject _cameraPlayer2;
    GameObject _player2CampPos;
    void Start()
    {
        _cameraPlayer2 = GameObject.FindGameObjectWithTag("Player2Cam");
        _player2CampPos = GameObject.FindGameObjectWithTag("Player2CamPos");

        _cameraPlayer2.transform.parent = _player2CampPos.transform;
        _cameraPlayer2.transform.localPosition = Vector3.zero;
        _cameraPlayer2.transform.localEulerAngles = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
