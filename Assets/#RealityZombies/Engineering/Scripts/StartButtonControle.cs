// @Author Nabil Lamriben ©2018
using System.Collections;
using UnityEngine;

public class StartButtonControle : MonoBehaviour, IShootable
{

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            Debug.Log("eeeeeeeeeeeeeeeeeeeeeeeee");
            Debug.Log("int port= " + GameSettings.Instance.Port_Internal);
            Debug.Log("ohl port= " + GameSettings.Instance.Port_External_OtherHL);
            Debug.Log("aud port= " + GameSettings.Instance.Port_External_AudioServer);
            Debug.Log("ohl IP= " + GameSettings.Instance.Ip_External_OtherHL);
            Debug.Log("aud IP = " + GameSettings.Instance.Ip_External_AudioServer);
        }
    }
    void OnEnable()
    {
        GameEventsManager.OnTakeHit += TakeHit;
        GameEventsManager.OnOtherHLStartsGame += StartedRemotely;
        GameEventsManager.OnPlayerKilledZombie += SetCanBeShot;
    }

    private void OnDisable()
    {
        GameEventsManager.OnTakeHit -= TakeHit;
        GameEventsManager.OnOtherHLStartsGame -= StartedRemotely;
        GameEventsManager.OnPlayerKilledZombie -= SetCanBeShot;
    }
    public GameObject EXPLOSION;

    // UAudioManager _uaudio;

    const int maxhitcounts = 3;
    ////int hitsTaken = 0;
    private void Start()
    {
        this.gameObject.SetActive(true);
        //  _uaudio = GetComponent<UAudioManager>();
    }
    public void MakeInvisible()
    {

        this.gameObject.SetActive(false);
    }

    bool isActive = false;
    void SetCanBeShot()
    {
        isActive = true;
    }
    IEnumerator waitWhileCGCplays()
    {

        yield return new WaitForSeconds(0.3f);

        //if (isActive)
        //{

        GameManager.Instance.CheckWav1Started(); //start Button 
        DOExplosion();
        // }
    }
    IEnumerator waitWhileCGCplaysShorter()
    {
        yield return new WaitForSeconds(0.2f);
        GameManager.Instance.CheckWav1Started(); //start Button 
        DOExplosion();
    }

    //when we get udp message"startgame" udpresponce raises event , only on bravo. compensate for potential delay 
    public void StartedRemotely()
    {
        StartCoroutine(waitWhileCGCplaysShorter());
    }

    void DOExplosion()
    {

        Instantiate(EXPLOSION, transform.position, Quaternion.identity);
        MakeInvisible();

    }
    ////bool hasreachedMax = false;
    public void TakeHit(Bullet bullet, int argidNotused)
    {
        if (GameSettings.Instance.GAmeSessionType == ARZGameSessionType.MULTI)
        {
            if (GameSettings.Instance.GameMode == ARZGameModes.GameLeft_Alpha)
            {
                isActive = true; //no scenariomanager to call event to this button, so just set this as true manually
                StartCoroutine(waitWhileCGCplays());
                // UDPcommMNGR.Instance.HelpSendMEssage(GameSettings.Instance.Ip_External_OtherHL, GameSettings.Instance.Port_External_OtherHL, GameSettings.Instance.GetMSG_StartGame());
                // UDPcommMNGR.Instance.HelpSendMEssage(GameSettings.Instance.Ip_External_OtherHL, GameSettings.Instance.Port_External_OtherHL, GameSettings.Instance.GetMSG_StartGame()); //twice just in case. the second message may never be heard 
            }
            else
                if (GameSettings.Instance.GameMode == ARZGameModes.GameRight_Bravo)
            {
                //do nothing , just wait for the udpresponse to invoke otherHlStartedGame event 
            }

            return;
        }
        else
        {
            StartCoroutine(waitWhileCGCplays()); //in udp or single mode, this will be called evrytime a ishootable object takes a hit . this time it will only be valid after isactive has ben set by event call
        }
    }

    public void Shot(Bullet argBullet)
    {

        if (GameSettings.Instance.GAmeSessionType == ARZGameSessionType.MULTI)
        {
            if (GameSettings.Instance.GameMode == ARZGameModes.GameLeft_Alpha)
            {
                isActive = true; //no scenariomanager to call event to this button, so just set this as true manually
                StartCoroutine(waitWhileCGCplays());
                //UDPcommMNGR.Instance.HelpSendMEssage(GameSettings.Instance.Ip_External_OtherHL, GameSettings.Instance.Port_External_OtherHL, GameSettings.Instance.GetMSG_StartGame());
                // UDPcommMNGR.Instance.HelpSendMEssage(GameSettings.Instance.Ip_External_OtherHL, GameSettings.Instance.Port_External_OtherHL, GameSettings.Instance.GetMSG_StartGame()); //twice just in case. the second message may never be heard 
            }
            else
                if (GameSettings.Instance.GameMode == ARZGameModes.GameRight_Bravo)
            {
                //do nothing , just wait for the udpresponse to invoke otherHlStartedGame event 
            }

            return;
        }
        else
        {
            Debug.Log("startshot start corout");

            StartCoroutine(waitWhileCGCplays()); //in udp or single mode, this will be called evrytime a ishootable object takes a hit . this time it will only be valid after isactive has ben set by event call
        }
    }

    public void aimed(bool argTF)
    {
        throw new System.NotImplementedException();
    }
}
