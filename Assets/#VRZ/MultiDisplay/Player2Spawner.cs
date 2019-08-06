using System.Collections;
using UnityEngine;

public class Player2Spawner : MonoBehaviour
{
    GameObject _cameraPlayer2;
    GameObject _player2CampPos;
    GameObject _Player2;
    SimpleDpadPlayerCTRL dpd;

    private void OnEnable()
    {
        GameEventsManager.OnPlayer2Died += HandlePlayer2Died;
    }

    private void OnDisable()
    {
        GameEventsManager.OnPlayer2Died -= HandlePlayer2Died;
    }
    void Start()
    {
        if (GameSettings.Instance.UsePlayer2)
        {

            _cameraPlayer2 = GameObject.FindGameObjectWithTag("Player2Cam");
            _player2CampPos = GameObject.FindGameObjectWithTag("Player2CamPos");
            _Player2 = GameObject.FindGameObjectWithTag("Player2");
        }
    }


    public void DoSpawnPlayer2()
    {
        if (GameSettings.Instance.UsePlayer2)
        {

            dpd = _Player2.GetComponent<SimpleDpadPlayerCTRL>();
            dpd.SetVisiblePlz(true);
            dpd.AllowInputs(true);
            _cameraPlayer2.transform.parent = _player2CampPos.transform;
            _cameraPlayer2.transform.localPosition = Vector3.zero;
            _cameraPlayer2.transform.localEulerAngles = Vector3.zero;

            _Player2.transform.position = this.transform.position;
        }
    }

    public void HandlePlayer2Died()
    {
        dpd.SetVisiblePlz(false);
        dpd.AllowInputs(false);
        StartCoroutine(WaitAfew());
    }

    IEnumerator WaitAfew()
    {
        yield return new WaitForSeconds(5);
        DoSpawnPlayer2();
        dpd.ResetShotCount();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
