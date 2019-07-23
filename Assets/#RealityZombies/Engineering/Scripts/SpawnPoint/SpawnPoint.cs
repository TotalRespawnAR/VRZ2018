using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour
{
    private int _spawnId;
    public int Spawn_ID
    {
        get { return _spawnId; }
        set { _spawnId = value; }
    }

    //private void Awake()
    //{
    //    _myrenderer = GetComponent<Renderer>();
    //    _myMat = _myrenderer.material;
    //}

    //Renderer _myrenderer;
    //Material _myMat;
    //private float m_SpawnDelay;

    //// private WaveStandard m_Wave;

    //public void Init_inactive_and_coolingdown(float spawnDelay)
    //{

    //    m_SpawnDelay = spawnDelay;
    //    // m_Wave = wave;   
    //    IsCoolingDown = true;
    //    Is_OFF = true;
    //  //  StartCoolingDown();
    //}

    //public bool Is_OFF;

    //public bool IsCoolingDown = true;

    ////public void StartCoolingDown() { StartCoroutine(CoolDownCoroutine()); }
    //public void StartCoolingDown() { if (!Is_OFF) StartCoroutine(CoolDownCoroutine()); }

    //private IEnumerator CoolDownCoroutine()
    //{

    //    IsCoolingDown = true;
    //    if (GameSettings.Instance != null)
    //    {
    //        if (GameSettings.Instance.IsTestModeON)
    //        {
    //            _myMat.color = Color.red;
    //        }
    //    }

    //    yield return new WaitForSeconds(m_SpawnDelay);
    //    IsCoolingDown = false;
    //    if (GameSettings.Instance != null)
    //    {
    //        if (GameSettings.Instance.IsTestModeON)
    //        {
    //            _myMat.color = Color.blue;
    //        }
    //    }
    //    //ToSayImFreeNowStartYourMethodOffindingwhosfreeItcouldbemeifyouneedittospawn
    //    // m_Wave.RequestAvailableSP();
    //    GameManager.Instance.SignalSpawnReady(this);
    //}

    //public void ResetMe()
    //{
    //    Is_OFF = true;

    //    if (IsCoolingDown)
    //    {
    //        StopCoroutine(CoolDownCoroutine());
    //    }
    //    IsCoolingDown = false;
    //}

    //public void TurnOnAndStartCoolingdown()
    //{
    //    Is_OFF = false;

    //    StartCoroutine(CoolDownCoroutine());

    //    IsCoolingDown = true;
    //}


    //public void StopMe()
    //{
    //    IsCoolingDown = false;
    //    StopAllCoroutines();
    //}

    //private void OnDestroy()
    //{
    //    Destroy(_myMat);
    //}

}
