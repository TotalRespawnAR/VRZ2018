using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UdpIpPortSetup : MonoBehaviour
{
    public Text PortInt;
    public Text PortExtAudio;
    public Text PortOtherHL;
    public Text PortScoreserver;
    public Text IpAudio;
    public Text IpOtherHl;
    public Text IpScoreServer;
    string Port_Internalsetup = "";
    string Port_External_Audiosetup = "";
    string Port_External_OtherHLsetup = "";
    string Port_External_ScoreServersetup = "";
    string Ip_External_Audiosetup = "";
    string Ip_External_OtherHLsetup = "";
    string Ip_External_ScoreServersetup = "";

    public Text DefaultPortInt;
    public Text DefaultPortExtAudio;
    public Text DefaultPortOtherHL;
    public Text DefaultPortScoreserver;
    public Text DefaultIpAudio;
    public Text DefaultIpOtherHl;
    public Text DefaultIpScoreServer;

    //string DefaultPortIntStr = "50201";
    //string DefaultPortExtAudioStr = "7791";
    //string DefaultPortOtherHLStr = "50202";
    //string DefaultPortScoreserverStr = "50204";
    //string DefaultIpAudioStr = "192.168.0.100";
    //string DefaultIpOtherHlStr = "192.168.0.131";
    //string DefaultIpScoreServerStr = "192.168.0.132";

    string DefaultPortIntStr;
    string DefaultPortExtAudioStr;
    string DefaultPortOtherHLStr;
    string DefaultPortScoreserverStr;
    string DefaultIpAudioStr;
    string DefaultIpOtherHlStr;
    string DefaultIpScoreServerStr;

    public void SetupPort_Internal(string arggPortInternal)
    {
        Port_Internalsetup = arggPortInternal;
    }
    public void SetupPort_External_Audio(string argPorttAudio)
    {
        Port_External_Audiosetup = argPorttAudio;
    }
    public void SetupPort_External_OtherHL(string argPortOtherHl)
    {
        Port_External_OtherHLsetup = argPortOtherHl;
    }

    public void SetupPort_External_ScoreServer(string argPortScoreServer)
    {
        Port_External_ScoreServersetup = argPortScoreServer;
    }

    public void SetupIP_External_Audio(string argIPaudio)
    {
        Ip_External_Audiosetup = argIPaudio;
    }
    public void SetupIP_External_OtherHL(string argIPOtherHl)
    {
        Ip_External_OtherHLsetup = argIPOtherHl;
    }

    public void SetupIP_External_ScoreServer(string argIPScoreServer)
    {
        Ip_External_ScoreServersetup = argIPScoreServer;
    }

    public void Default_Internal() { Port_Internalsetup = DefaultPortIntStr; }
    public void Default_PortAudio() { Port_External_Audiosetup = DefaultPortExtAudioStr; }
    public void Default_PorOtherHl() { Port_External_OtherHLsetup = DefaultPortOtherHLStr; }
    public void Default_IPAudio() { Ip_External_Audiosetup = DefaultIpAudioStr; }
    public void Default_IPOtherHl() { Ip_External_OtherHLsetup = DefaultIpOtherHlStr; }
    public void Default_IPScoreServer() { Ip_External_ScoreServersetup = DefaultIpScoreServerStr; }
    public void Default_PortScoreServer() { Port_External_ScoreServersetup = DefaultPortScoreserverStr; }

    public void SaveSettings()
    {

        if (GameSettings.Instance != null)
        {
            GameSettings.Instance.Port_Internal = Port_Internalsetup;
            GameSettings.Instance.Port_External_OtherHL = Port_External_OtherHLsetup;
            GameSettings.Instance.Port_External_AudioServer = Port_External_Audiosetup;
            GameSettings.Instance.Ip_External_OtherHL = Ip_External_OtherHLsetup;
            GameSettings.Instance.Ip_External_AudioServer = Ip_External_Audiosetup;
            GameSettings.Instance.Ip_External_ScoreServer = Ip_External_ScoreServersetup;
            GameSettings.Instance.Port_External_ScoreServer = Port_External_ScoreServersetup;
            SceneManager.LoadScene("KngSetupMenu");
            //SceneManager.LoadScene("kngUdpTest");
        }
        else
            Debug.Log("no ame settings");
    }

    private void Awake()
    {
        //DefaultPortIntStr=GameSettings.Instance.GetSTR_PortInternal_Alpha();
        //DefaultPortExtAudioStr= GameSettings.Instance.GetSTR_STR_Port_Audio();
        //DefaultPortOtherHLStr = GameSettings.Instance.GetSTR_PortInternal_Bravo();
        //DefaultPortScoreserverStr = GameSettings.Instance.GetSTR_STR_Port_Server();
        //DefaultIpAudioStr= GameSettings.Instance.GetSTR_IP_Audio();
        //DefaultIpOtherHlStr= GameSettings.Instance.GetSTR_IP_Bravo();
        //DefaultIpScoreServerStr= GameSettings.Instance.GetSTR_IP_Server();
    }

    // Use this for initialization
    void Start()
    {
        if (GameSettings.Instance != null)
        {

            DefaultPortIntStr = GameSettings.Instance.GetSTR_PortInternal_Alpha();
            DefaultPortExtAudioStr = GameSettings.Instance.GetSTR_STR_Port_Audio();
            DefaultPortOtherHLStr = GameSettings.Instance.GetSTR_PortInternal_Bravo();
            DefaultPortScoreserverStr = GameSettings.Instance.GetSTR_STR_Port_Server();
            DefaultIpAudioStr = GameSettings.Instance.GetSTR_IP_Audio();
            DefaultIpOtherHlStr = GameSettings.Instance.GetSTR_IP_Bravo();
            DefaultIpScoreServerStr = GameSettings.Instance.GetSTR_IP_Server();


            if (GameSettings.Instance.GameMode == ARZGameModes.GameRight_Bravo)
            {

                DefaultPortIntStr = GameSettings.Instance.GetSTR_PortInternal_Bravo();
                DefaultPortOtherHLStr = GameSettings.Instance.GetSTR_PortInternal_Alpha();
                DefaultIpOtherHlStr = GameSettings.Instance.GetSTR_IP_Alpha();
            }

            DefaultPortInt.text = DefaultPortIntStr;
            DefaultPortExtAudio.text = DefaultPortExtAudioStr;
            DefaultPortOtherHL.text = DefaultPortOtherHLStr;
            DefaultPortScoreserver.text = DefaultPortScoreserverStr;
            DefaultIpAudio.text = DefaultIpAudioStr;
            DefaultIpOtherHl.text = DefaultIpOtherHlStr;
            DefaultIpScoreServer.text = DefaultIpScoreServerStr;


            PortInt.text = Port_Internalsetup = GameSettings.Instance.Port_Internal;
            PortExtAudio.text = Port_External_Audiosetup = GameSettings.Instance.Port_External_AudioServer;
            PortOtherHL.text = Port_External_OtherHLsetup = GameSettings.Instance.Port_External_OtherHL;
            PortScoreserver.text = Port_External_ScoreServersetup = GameSettings.Instance.Port_External_ScoreServer;
            IpAudio.text = Ip_External_Audiosetup = GameSettings.Instance.Ip_External_AudioServer;
            IpOtherHl.text = Ip_External_OtherHLsetup = GameSettings.Instance.Ip_External_OtherHL;
            IpScoreServer.text = Ip_External_ScoreServersetup = GameSettings.Instance.Ip_External_ScoreServer;


        }
        else
            Debug.Log("no have settings");
    }
}
