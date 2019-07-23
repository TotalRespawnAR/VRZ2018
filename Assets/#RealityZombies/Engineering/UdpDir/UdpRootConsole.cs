// @Author Nabil Lamriben ©2018

using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEngine.VR.WSA.Persistence;

public class UdpRootConsole : MonoBehaviour
{

    public TextMesh tm;


    void Start()
    {
    }



    public void DoSendMessage_1()
    {
        if (UDPcommMNGR.Instance == null)
        {
            Debug.Log("nno udp commm manager");
        }
        else
        {
            tm.text += "\n Sending message 1 to " + GameSettings.Instance.Ip_External_OtherHL + " " + GameSettings.Instance.Port_External_OtherHL; ;
        UDPcommMNGR.Instance.HelpSendMEssage("msg 1");
            UDPcommMNGR.Instance.HelpSendMEssage(GameSettings.Instance.Ip_External_OtherHL, GameSettings.Instance.Port_External_OtherHL, "msg TO OTHERHL");

        }

    }

    public void GoToSetupUDP() {
        SceneManager.LoadScene("UdpPropSetup");
    }
    public void DoSendMessage_2()
    {
        if (UDPcommMNGR.Instance == null) {
            Debug.Log("nno udp commm manager");
        }
        else
        {
            tm.text += "\n Sending message 2 to " + GameSettings.Instance.Ip_External_AudioServer + " " + GameSettings.Instance.Port_External_AudioServer;
            UDPcommMNGR.Instance.HelpSendMEssage(GameSettings.Instance.Ip_External_AudioServer, GameSettings.Instance.Port_External_AudioServer, "msg 2");
        }
    }
    public void CallScreen(object o)
    {
        if (o is string)
        {
            if (o.ToString() == "Button_A_Clicked") { DoSendMessage_1(); }
            else
                 if (o.ToString() == "Button_B_Clicked") { DoSendMessage_2(); }
            else
                if (o.ToString() == "Button_C_Clicked") { GoToSetupUDP(); }
        }
    }
}



