﻿using System;
using System.IO;
using UnityEngine;

#if !UNITY_EDITOR && UNITY_WSA
         
 

using System.Threading.Tasks;  
#endif

public class TcpClientUwpAndUnity : MonoBehaviour
{

    public TcpMessageDisplay TrackingManager;
    public TextMesh StatusTextMesh;
#if !UNITY_EDITOR && UNITY_WSA
         
 

    private bool _useUWP = true;
    private Windows.Networking.Sockets.StreamSocket socket;
    private Task exchangeTask;
#endif
#if !UNITY_EDITOR && UNITY_WSA
         

 

    private bool _useUWP = false;
    System.Net.Sockets.TcpClient client;
    System.Net.Sockets.NetworkStream stream;
    private Thread exchangeThread;
#endif

    private Byte[] bytes = new Byte[256];
    private StreamWriter writer;
    private StreamReader reader;

    public void Connect(string host, string port)
    {
#if !UNITY_EDITOR && UNITY_WSA
         

        if (_useUWP)
        {
            ConnectUWP(host, port);
        }
        else
        {
            ConnectUnity(host, port);
        }
#endif

    }



#if !UNITY_EDITOR && UNITY_WSA
         
 

    private async void ConnectUWP(string host, string port)
#else
    private void ConnectUWP(string host, string port)
#endif
    {
#if !UNITY_EDITOR && UNITY_WSA
         

 
      //  errorStatus = "UWP TCP client used in Unity!";
 
        try
        {
            if (exchangeTask != null) StopExchange();

            socket = new Windows.Networking.Sockets.StreamSocket();
            Windows.Networking.HostName serverHost = new Windows.Networking.HostName(host);
            await socket.ConnectAsync(serverHost, port);

            Stream streamOut = socket.OutputStream.AsStreamForWrite();
            writer = new StreamWriter(streamOut) { AutoFlush = true };

            Stream streamIn = socket.InputStream.AsStreamForRead();
            reader = new StreamReader(streamIn);

            RestartExchange();
            successStatus = "Connected!";
        }
        catch (Exception e)
        {
            errorStatus = e.ToString();
        }
#endif
    }

    private void ConnectUnity(string host, string port)
    {
#if !UNITY_EDITOR && UNITY_WSA
         

 

        errorStatus = "Unity TCP client used in UWP!";
 
        System.Net.IPAddress MyMSIipAddress;
        System.Net.IPAddress ServerAddress;
        try
        {
            if (exchangeThread != null) StopExchange();

            //if (GameSettings.Instance == null) { OTHERipAddress = System.Net.IPAddress.Parse("192.168.1.3"); }
            //else
            //{

            //    OTHERipAddress = System.Net.IPAddress.Parse(GameSettings.Instance.My_Partner_IP);

            //}
            int portint = Int32.Parse(port);

            MyMSIipAddress = System.Net.IPAddress.Parse("192.168.1.2");
            ServerAddress = System.Net.IPAddress.Parse("192.168.1.3");
            System.Net.IPEndPoint ipLocalEndPoint = new System.Net.IPEndPoint(MyMSIipAddress, portint);
            // TcpClient clientSocket = new TcpClient(ipLocalEndPoint);
            // clientSocket.Connect(remoteHost, remotePort);

            // client = new System.Net.Sockets.TcpClient(host, Int32.Parse(port));
            client = new System.Net.Sockets.TcpClient(ipLocalEndPoint);
            client.Connect(ServerAddress, 50111);
            TrackingManager.DisplayReceivedMessage("yo connecting to port" + port);
            stream = client.GetStream();
            reader = new StreamReader(stream);
            writer = new StreamWriter(stream) { AutoFlush = true };

            RestartExchange();
            successStatus = "Connected!";
        }
        catch (Exception e)
        {
            errorStatus = e.ToString();
        }
#endif
    }

    private bool exchanging = false;
    private bool exchangeStopRequested = false;
    private string lastPacket = null;

    private string errorStatus = null;
    private string warningStatus = null;
    private string successStatus = null;
    private string unknownStatus = null;

    public void RestartExchange()
    {
#if !UNITY_EDITOR && UNITY_WSA
         

 

        if (exchangeThread != null) StopExchange();
        exchangeStopRequested = false;
        exchangeThread = new System.Threading.Thread(ExchangePackets);
        exchangeThread.Start();
 
        if (exchangeTask != null) StopExchange();
        exchangeStopRequested = false;
        exchangeTask = Task.Run(() => ExchangePackets());
#endif
    }

    private void Start()
    {

#if !UNITY_EDITOR && UNITY_WSA
         

 

        if (GameSettings.Instance != null)
        {
            Connect(GameSettings.Instance.Ip_External_OtherHL, GameSettings.Instance.Port_External_OtherHL);//should use same port on server as client    
        }
        else
        {
            Debug.Log("no settings found , assuming server is on jalt");
            Connect("192.168.1.3", "50103");

        }
#else
        if (GameSettings.Instance != null)
        {
            ConnectUWP(GameSettings.Instance.Ip_External_OtherHL, GameSettings.Instance.Port_External_OtherHL);//should use same port on server as client    
        }
        else
        {
            Debug.Log("no settings found , assuming server is on jalt");
            Connect("192.168.1.3", "50103");
        }
#endif

    }

    public void Update()
    {
        //Debug.Log("wtf");
        if (lastPacket != null)
        {
            ReportDataToTrackingManager(lastPacket);
        }

        if (errorStatus != null)
        {
            //StatusTextManager.SetError(errorStatus);
            StatusTextMesh.text += "status: errorStatus";
            errorStatus = null;
        }
        if (warningStatus != null)
        {
            //StatusTextManager.SetWarning(warningStatus);
            StatusTextMesh.text += "status: warningStatus";
            warningStatus = null;
        }
        if (successStatus != null)
        {
            //StatusTextManager.SetSuccess(successStatus);
            StatusTextMesh.text += "status: successStatus";
            successStatus = null;
        }
        if (unknownStatus != null)
        {
            // StatusTextManager.SetUnknown(unknownStatus);
            StatusTextMesh.text += "status: unknownStatus";
            unknownStatus = null;
        }
    }

    public void ExchangePackets()
    {
        while (!exchangeStopRequested)
        {
            if (writer == null || reader == null) continue;
            exchanging = true;

            writer.Write("yo what up server \n");
            Debug.Log("Sent data!");
            string received = null;

#if !UNITY_EDITOR && UNITY_WSA
         
 
            byte[] bytes = new byte[client.SendBufferSize];
            int recv = 0;
            while (true)
            {
                recv = stream.Read(bytes, 0, client.SendBufferSize);
                received += Encoding.UTF8.GetString(bytes, 0, recv);
                if (received.EndsWith("\n")) break;
            }
#else
            received = reader.ReadLine();
#endif

            lastPacket = received;
            Debug.Log("Read data: " + received);

            exchanging = false;
        }
    }

    private void ReportDataToTrackingManager(string data)
    {
        if (data == null)
        {
            Debug.Log("Received a frame but data was null");
            return;
        }

        //var parts = data.Split(';');
        //foreach (var part in parts)
        //{
        //    ReportStringToTrackingManager(part);
        //}

        ReportStringToTrackingManager(data);
    }

    private void ReportStringToTrackingManager(string rigidBodyString)
    {
        //var parts = rigidBodyString.Split(':');
        //var positionData = parts[1].Split(',');
        //var rotationData = parts[2].Split(',');

        //int id = Int32.Parse(parts[0]);
        //float x = float.Parse(positionData[0]);
        //float y = float.Parse(positionData[1]);
        //float z = float.Parse(positionData[2]);
        //float qx = float.Parse(rotationData[0]);
        //float qy = float.Parse(rotationData[1]);
        //float qz = float.Parse(rotationData[2]);
        //float qw = float.Parse(rotationData[3]);

        //Vector3 position = new Vector3(x, y, z);
        //Quaternion rotation = new Quaternion(qx, qy, qz, qw);
        Debug.Log("YOOOO MEssage from server =" + rigidBodyString);
        //    TrackingManager.UpdateRigidBodyData(id, position, rotation);
        TrackingManager.DisplayReceivedMessage(rigidBodyString);
    }

    public void StopExchange()
    {
        exchangeStopRequested = true;

#if !UNITY_EDITOR && UNITY_WSA
         

 
        if (exchangeThread != null)
        {
            exchangeThread.Abort();
            stream.Close();
            client.Close();
            writer.Close();
            reader.Close();

            stream = null;
            exchangeThread = null;
        }
//#else
        if (exchangeTask != null) {
            exchangeTask.Wait();
            socket.Dispose();
            writer.Dispose();
            reader.Dispose();

            socket = null;
            exchangeTask = null;
        }
#endif
        writer = null;
        reader = null;
    }

    public void OnDestroy()
    {
        StopExchange();
    }

}