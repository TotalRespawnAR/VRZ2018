using UnityEngine;

public class MetalAudioController : MonoBehaviour
{
    //UAudioManager _uaudio;
    void OnEnable()
    {
        GameEventsManager.OnTakeHit += TakeHit;
    }

    private void OnDisable()
    {
        GameEventsManager.OnTakeHit -= TakeHit;
    }
    // Use this for initialization
    void Start()
    {
        //_uaudio = GetComponent<UAudioManager>();
    }

    public void TakeHit(Bullet argBullet, int argidNotused)
    {
        // _uaudio.PlayEvent("_MetalDing");
    }
}
