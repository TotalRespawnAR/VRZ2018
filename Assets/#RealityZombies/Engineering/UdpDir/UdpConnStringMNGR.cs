// @Author Nabil Lamriben ©2018
using UnityEngine;

#if !UNITY_EDITOR  && UNITY_WSA
using Windows.Networking.Sockets;
using Windows.Networking.Connectivity;
using Windows.Networking;
#endif

public class UdpConnStringMNGR : MonoBehaviour
{


    public TextMesh _myIpText;
    public TextMesh _myInternalListenPortText;
    public TextMesh _myPartenersIPText;
    public TextMesh _myPartenersListenPortText;
    string MeTheHostMyName;
    // Use this for initialization
    void Start()
    {
        Debug.Log("UdpComnnnnnStro");

        if (GameSettings.Instance != null)
        {
            string MyCashedIP = GameSettings.Instance.My_IP;
            _myIpText.text = MyCashedIP;


            _myInternalListenPortText.text = GameSettings.Instance.Port_Internal;
            _myPartenersIPText.text = GameSettings.Instance.Ip_External_OtherHL; ;
            _myPartenersListenPortText.text = GameSettings.Instance.Port_External_OtherHL;

            getmyownHostname();
        }
    }

    void getmyownHostname()
    {
        if (GameSettings.Instance == null)
        {
            Debug.Log("no settings");
        }
        else

        {
            //msipc LAPTOP-SS5QVBK2  Holo-01  Holo-02
            string hostName = "na";
#if !UNITY_EDITOR && UNITY_WSA
         
 


            var hostNames = NetworkInformation.GetHostNames();
              hostName = hostNames.FirstOrDefault(name => name.Type == HostNameType.DomainName)?.DisplayName ?? "???";
#endif
            MeTheHostMyName = hostName;
            Debug.Log("i am the host " + MeTheHostMyName);
        }
    }



    void getmyownIp()
    {
#if !UNITY_EDITOR && UNITY_WSA
         
 

        HostName IP = null;
        var icp = NetworkInformation.GetInternetConnectionProfile();
        
        IP = Windows.Networking.Connectivity.NetworkInformation.GetHostNames()
            .SingleOrDefault(
                hn =>
                    hn.IPInformation?.NetworkAdapter != null && hn.IPInformation.NetworkAdapter.NetworkAdapterId
                    == icp.NetworkAdapter.NetworkAdapterId);    
        

         Console3D.Instance.LOGit("foud IP " + IP.ToString());
#endif

    }




    //to keep track of doubl click problem 
    int click_IP_cnt = 0;
    int click_intPrt_cnt = 0;
    int click_exIP_cnt = 0;
    int click_exPrt_cnt = 0;

    //to display and set udp sockets stuffs 
    void Set_My_IP(string argstr)
    {
        click_IP_cnt++;
        if (GameSettings.Instance == null)
        {
            //Debug.Log("!!!!!!!no gamesettings !!!!!!");
            _myIpText.text = "no settings";
        }
        else
        {
            // Debug.Log("."+click_IP_cnt.ToString()+". gamesttings.My_IP = " + argstr);
            GameSettings.Instance.My_IP = argstr;
            _myIpText.text = GameSettings.Instance.My_IP + "   ." + click_IP_cnt.ToString() + ".";
        }
    }

    void Set_My_Listen_Port(string argstr)
    {
        click_intPrt_cnt++;
        if (GameSettings.Instance == null)
        {
            Debug.Log("!!!!!!!no gamesettings !!!!!!");
            _myInternalListenPortText.text = "no settings";
        }
        else
        {
            Debug.Log("." + click_intPrt_cnt.ToString() + ".gamesttings.My_ListenPort = " + argstr);
            GameSettings.Instance.Port_Internal = argstr;
            _myInternalListenPortText.text = GameSettings.Instance.Port_Internal + "    ." + click_intPrt_cnt.ToString() + ".";
        }
    }

    void Set_My_Partner_IP(string argstr)
    {
        click_exIP_cnt++;
        if (GameSettings.Instance == null)
        {
            Debug.Log("!!!!!!!no gamesettings !!!!!!");
            _myPartenersIPText.text = "no settings";
        }
        else
        {
            Debug.Log("." + click_exIP_cnt.ToString() + ".gamesttings.My_Partner_IP = " + argstr);
            GameSettings.Instance.Ip_External_OtherHL = argstr;
            _myPartenersIPText.text = GameSettings.Instance.Ip_External_OtherHL + "  ." + click_exIP_cnt.ToString() + ".";
        }
    }

    void Set_My_Partner_Listen_Port(string argstr)
    {
        click_exPrt_cnt++;
        if (GameSettings.Instance == null)
        {
            Debug.Log("!!!!!!!no gamesettings !!!!!!");
            _myPartenersListenPortText.text = "no settings";
        }
        else
        {
            Debug.Log("." + click_exPrt_cnt.ToString() + ".gamesttings.My_Partner's Listen_Port = " + argstr);
            GameSettings.Instance.Port_External_OtherHL = argstr;
            _myPartenersListenPortText.text = GameSettings.Instance.Port_External_OtherHL + "  ." + click_exPrt_cnt.ToString() + ".";
        }
    }



}
