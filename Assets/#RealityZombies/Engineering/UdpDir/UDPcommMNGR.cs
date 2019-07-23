using System.Text;
using UnityEngine;

public class UDPcommMNGR : MonoBehaviour
{
    //may need to be static instance instead of Sigleton 
    public UDPCommunication UdpCommunicator;
    public static UDPcommMNGR Instance = null;
    private void Awake()
    {
        if (Instance == null)
        {

            DontDestroyOnLoad(this.gameObject);
            Instance = this;

        }
    }
    int MessageSentID = -1;
    public void HelpSendMEssage(string argMessage)
    {
        MessageSentID++;
        UdpCommunicator.SendUDPMessage("192.168.1.3", "50202", Encoding.UTF8.GetBytes(argMessage));
    }
    public void HelpSendMEssage(string argIP, string argPort, string argMessage)
    {
        MessageSentID++;
        UdpCommunicator.SendUDPMessage(argIP, argPort, Encoding.UTF8.GetBytes(argMessage));
    }
}
