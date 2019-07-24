using ArenaLiveAPI.DaemonControlling;
using System.Collections.Generic;
using UnityEngine;

//
// Automated Daemon Controller Example
//
public class BlasterOperator : MonoBehaviour
{

    string PathToDaemon;

    public string[] BlasterHubComPortNames;

    private int GameID;

    private Dictionary<BlasterAddress, int> Pairs;

    DaemonController Controller;

    void Awake()


    {
        PathToDaemon = Application.dataPath + "/../ArenaLiveDaemon_0_14_0/Release/ArenaLiveDaemon.exe";
        Controller = new DaemonController("GDC", PathToDaemon, true);

        Controller.BlasterConnected += OnBlasterConnected;
        Controller.BlasterDisconnected += OnBlasterDisconnected;
        Controller.ErrorOccured += OnErrorOccured;
        Controller.GameConnected += OnGameConnected;
        Controller.GameDisconnected += OnGameDisconnected;
        Controller.ConnectToDaemon();

        Pairs = new Dictionary<BlasterAddress, int>();

        // Connect to one or more base stations
        foreach (string s in BlasterHubComPortNames)
        {
            Controller.ConnectToBlasterHub(s);
        }
    }

    void OnGameDisconnected(int value)
    {
        Debug.LogFormat("GameDisconnected gameID:{0}", value);
    }

    void OnGameConnected(GameState value)
    {
        Debug.LogFormat("GameConnected gameID:{0} name:{1} nrPlayers:{2}", value.GameID, value.Name, value.NumberOfPlayers);

        GameID = value.GameID;
    }

    void OnErrorOccured(System.Exception value)
    {
        Debug.LogFormat("ErrorOccured exception:{0}", value.ToString());
    }

    void OnBlasterDisconnected(BlasterAddress value)
    {
        Debug.LogFormat("BlasterDisconnected  addr:{0}", value.ToString());

        // Here you might remove the blaster from the pairs Dictionary, 
        // or you may leave it so that when the blaster comes back online
        // the same connection is made
    }

    int NextPlayerID = 0;
    void OnBlasterConnected(BlasterState value)
    {
        Debug.LogFormat("BlasterConnected  addr:{0} name:{1} port:{2}", value.Address.ToString(), value.Name, value.BlasterHub);

        if (Pairs.ContainsKey(value.Address))
        {
            Controller.BindAddressToPlayerID(value.Address, GameID, Pairs[value.Address]);
        }

        else
        {
            Pairs.Add(value.Address, NextPlayerID);
            Controller.BindAddressToPlayerID(value.Address, GameID, NextPlayerID++);
        }
    }

    void OnDestroy()
    {
        if (Controller != null)
        {
            Debug.Log("Shutting down connection with Daemon");

            foreach (string s in BlasterHubComPortNames)
            {
                Controller.DisconnectFromBlasterHub(s);
            }

            Controller.DisconnectFromDaemon();
            Controller.Dispose();
        }
    }

    void Update()
    {
        if (Controller != null)
            Controller.Update();
    }

    public void Bind(int playerID, BlasterAddress address)
    {
        Controller.BindAddressToPlayerID(address, GameID, playerID);
    }
}
