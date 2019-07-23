// @Author Nabil Lamriben ©2018
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

#if !UNITY_EDITOR  && UNITY_WSA
using Windows.Networking.Sockets;
using Windows.Networking.Connectivity;
using Windows.Networking;
#endif

[System.Serializable]
public class UDPMessageEvent : UnityEvent<string, string, byte[]>
{

}

public class UDPCommunication : MonoBehaviour
{
    //show to screen 
    public TextMesh MyInfo;
    private string internalPort = "";
    private string externalIP = "";//ip of the sender . for now it's the msi
    private string externalPort = "";
    static int Cake = 0;
    public static UDPCommunication Instance = null;

    [Tooltip("Function to invoke at incoming packet")]
    public UDPMessageEvent udpEvent = null;

    private readonly Queue<Action> ExecuteOnMainThread = new Queue<Action>();

    void Awake()
    {

        Application.targetFrameRate = 60;

        if (Instance == null)
        {
            //FindWavveSettinggsFile();
            //if (has_waveLevelSettingsFile) Load(Kng_wavesettingsFilePATH + "/" + WaveLevelSetttingsFileName + ".txt");

            DontDestroyOnLoad(this.gameObject);
            Instance = this;

        }
    }

    //***********************************************************************************************************
    //   
    //

    //  
    //
    //***********************************************************************************************************

    void UWPStart()
    {
        //  Init_myInfo_and_Displayit();
        InitAdioSeverAsUdpClientProps();
        GetUnknownsFromGameSettings_andDisplay();
    }
    private void InitAdioSeverAsUdpClientProps()
    {
        if (GameSettings.Instance != null)
        {
            internalPort = GameSettings.Instance.Port_Internal;
            externalIP = GameSettings.Instance.Ip_External_AudioServer;
            externalPort = GameSettings.Instance.Port_External_AudioServer;
        }
        else
        {
            Debug.Log("No game settings");
        }
    }
    void GetUnknownsFromGameSettings_andDisplay()
    {


        string _info = "  ";
        if (GameSettings.Instance == null)
        {
            Debug.Log("no settings");
            _info = " i haz no settings !!! Will FAAAILLL ";
            _info += "my ip is na ";
            _info += "\n enemy ip > " + externalIP;
            _info += "\n  enemy port" + externalPort;
            _info += "\n  listening port " + internalPort;
        }
        else

        {
            _info += "my ip is na ";
            _info += "\n enemy ip > " + externalIP;
            _info += "\n  enemy port" + externalPort;
            _info += "\n  listening port " + internalPort;
        }
        // MyInfo.text = _info;
    }


    void OnApplicationQuit()
    {

        //if (ExecuteOnMainThread.IsAlive)
        //{
        //    ExecuteOnMainThread.Abort();
        //}
        //receiver.Close();
        Destroy(this.gameObject); //let see if this solves the grey screen problem
    }
#if !UNITY_EDITOR && UNITY_WSA
 
 

    private void OnEnable()
    {
        if (udpEvent == null)
        {
            udpEvent = new UDPMessageEvent();
            udpEvent.AddListener(UDPMessageReceived);
        }
    }

    private void OnDisable()
    {
        if (udpEvent != null)
        {
            
            udpEvent.RemoveAllListeners();
        }
    }



    //we've got a message (data[]) from (host) in case of not assigned an event
    void UDPMessageReceived(string host, string port, byte[] data)
    {
    //    Debug.Log("udpMessageReceived: " + host + " on port " + port + " " + data.Length.ToString() + " bytes ");
    //    // Console3D.Instance.LOGit("GOT MESSAGE FROM: " + host + " on port " + port + " " + data.Length.ToString() + " bytes ");
    //    ++Cake;
    //    string MsgId = string.Format("{0:0000}", Cake);
    //   // string FullMessage = MsgId + "Aa9x";
    // string FullMessage = MsgId + "Cakeyum";
     
    //SendUDPMessage(externalIP, externalPort, Encoding.UTF8.GetBytes(FullMessage));
    }

    //Send an UDP-Packet
    public async void SendUDPMessage(string HostIP, string HostPort, byte[] data)
    {
        await _SendUDPMessage(HostIP, HostPort, data);
    }



    DatagramSocket socket;

    async void Start()
    {
        UWPStart();



        Debug.Log("Waiting for a connection...");

        socket = new DatagramSocket();
        socket.MessageReceived += Socket_MessageReceived;

        HostName IP = null;
        try
        {
            var icp = NetworkInformation.GetInternetConnectionProfile();

         //Console3D.Instance.LOGit("icp.netadaptor id = " + icp.NetworkAdapter.NetworkAdapterId.ToString());

            IP = Windows.Networking.Connectivity.NetworkInformation.GetHostNames()
            .SingleOrDefault(
                hn =>
                    hn.IPInformation?.NetworkAdapter != null && hn.IPInformation.NetworkAdapter.NetworkAdapterId
                    == icp.NetworkAdapter.NetworkAdapterId);
       //  Console3D.Instance.LOGit("not even using gamesettings -> my socket is = " + IP.ToString() + " " + internalPort);
            await socket.BindEndpointAsync(IP, internalPort);
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
            Debug.Log(SocketError.GetStatus(e.HResult).ToString());
            return;
        }
       //had worked SendUDPMessage("192.168.1.14", "7791", Encoding.UTF8.GetBytes("1234Ab9x"));
    SendUDPMessage(externalIP, externalPort, Encoding.UTF8.GetBytes("hadworked"));
    }




    private async System.Threading.Tasks.Task _SendUDPMessage(string argexternalIP, string argexternalPort, byte[] data)
    {
        using (var stream = await socket.GetOutputStreamAsync(new Windows.Networking.HostName(argexternalIP), argexternalPort))
        {
            using (var writer = new Windows.Storage.Streams.DataWriter(stream))
            {
                writer.WriteBytes(data);
                await writer.StoreAsync();

            }
        }
    }
 
#endif

    // to make Unity-Editor happy :-)
    void Start()
    {
        InitAdioSeverAsUdpClientProps();
    }

    public void SendUDPMessage(string HostIP, string HostPort, byte[] data)
    {

    }



    static MemoryStream ToMemoryStream(Stream input)
    {
        try
        {                                         // Read and write in
            byte[] block = new byte[0x1000];       // blocks of 4K. 1000
            MemoryStream ms = new MemoryStream();
            while (true)
            {
                int bytesRead = input.Read(block, 0, block.Length);
                if (bytesRead == 0) return ms;
                ms.Write(block, 0, bytesRead);
            }
        }
        finally { }
    }

    // Update is called once per frame
    void Update()
    {
        while (ExecuteOnMainThread.Count > 0)
        {
            ExecuteOnMainThread.Dequeue().Invoke();

        }
    }
#if !UNITY_EDITOR && UNITY_WSA
 
    private void Socket_MessageReceived(Windows.Networking.Sockets.DatagramSocket sender,
        Windows.Networking.Sockets.DatagramSocketMessageReceivedEventArgs args)
    {
        //Console3D.Instance.LOGit("GOT MESSAGE FROM: " + args.RemoteAddress.DisplayName);
        //Read the message that was received from the UDP  client.
        Stream streamIn = args.GetDataStream().AsStreamForRead();
        MemoryStream ms = ToMemoryStream(streamIn);
        byte[] msgData = ms.ToArray();


        if (ExecuteOnMainThread.Count == 0)
        {
            ExecuteOnMainThread.Enqueue(() =>
            {
                if (udpEvent != null)
                    udpEvent.Invoke(args.RemoteAddress.DisplayName, internalPort, msgData);
            });
        }
    }


#endif
}
