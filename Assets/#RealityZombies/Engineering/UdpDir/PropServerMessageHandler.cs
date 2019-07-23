#define ENABLE_LOGS
//#define USE_LIN_LOCAL_TEST_SERVER

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropServerMessageHandler : MonoBehaviour
{
    // Singleton stuff
    static PropServerMessageHandler CurrentPropServerMessageHandlerInstance = null;

    //---------------------------------------------------------
    // Server data
    //---------------------------------------------------------
    public const int SOUND_HOST_PORT = 7791;
    public const string SOUND_HOST_IP = "192.168.8.197";
    string SoundHostIP = "";
    string SoundHostPort = "";


    //---------------------------------------------------------
    // Standard Device IDs
    //---------------------------------------------------------
    const string SOUND_DEVICE_ID__PLAYER_ALHPA = "AlphaGun";
    const string SOUND_DEVICE_ID__PLAYER_BRAVO = "BravoGun";
    const string SOUND_DEVICE_ID__CONTROL_PANEL_COMMON = "CentrePanel";



    //---------------------------------------------------------
    // Handling Data
    //---------------------------------------------------------
    public const string PRIM_DELEM = "<1>";
    public const string SUB_DELEM = "<2>";
    public const string NIBBLE_DELEM = "<3>";
    static int CurrentMessageID = 0;

    // The container for all requested messages to live before they are sent out
    Queue<string> ServerMsgQueue;




    #region Startup and Shutdown Functions
    /********************************************************************************/
    /********************************************************************************/
    /*------------------------------------------------------------------------------*/
    /*-                                                                            -*/
    /*-                                                                            -*/
    /*-                                                                            -*/
    /*-                         Startup/Shutdown Functions                         -*/
    /*-                                                                            -*/
    /*-                                                                            -*/
    /*-                                                                            -*/
    /*------------------------------------------------------------------------------*/
    /********************************************************************************/
    /********************************************************************************/
    // Function 'void Start(void)'
    //
    //-------------------------------------------------------------------------------
    void Awake(/*void*/)
    {
        // Only one instance allowed
        if (CurrentPropServerMessageHandlerInstance != null)
        {
            Destroy(gameObject);
            return;
        }

        // Store this as the singleton
        CurrentPropServerMessageHandlerInstance = this;

        // Create a message queue
        ServerMsgQueue = new Queue<string>();
    }

    void Start(/*void*/)
    {
#if USE_LIN_LOCAL_TEST_SERVER
  SoundHostIP   = SOUND_HOST_IP;
  SoundHostPort = SOUND_HOST_PORT.ToString();
#else
        SoundHostIP = GameSettings.Instance.GetSTR_IP_Audio();
        SoundHostPort = GameSettings.Instance.GetSTR_STR_Port_Audio();
#endif
    }




    /********************************************************************************/
    /********************************************************************************/
    // Function 'void OnDestroy(void)'
    //
    //-------------------------------------------------------------------------------
    void OnDestroy(/*void*/)
    {
        // Clear this instance if this is the valid singleton
        if (CurrentPropServerMessageHandlerInstance == this)
            CurrentPropServerMessageHandlerInstance = null;
    }
    #endregion










    #region General Updating
    /********************************************************************************/
    /********************************************************************************/
    /*------------------------------------------------------------------------------*/
    /*-                                                                            -*/
    /*-                                                                            -*/
    /*-                                                                            -*/
    /*-                         General Updating Functions                         -*/
    /*-                                                                            -*/
    /*-                                                                            -*/
    /*-                                                                            -*/
    /*------------------------------------------------------------------------------*/
    /********************************************************************************/
    /********************************************************************************/
    // Function 'void Start(void)'
    //
    //-------------------------------------------------------------------------------
    void Update(/*void*/)
    {
        // OMGWTF Only one instance allowed!!!!!!!!1111111111111111~~~~~~~~~
        if (CurrentPropServerMessageHandlerInstance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Do nothing if we have no messages
        if (ServerMsgQueue.Count <= 0)
            return;

        // Build the message set
        string ComposedMessage = ServerMsgQueue.Dequeue();
        for (; ServerMsgQueue.Count > 0; ComposedMessage += PRIM_DELEM + ServerMsgQueue.Dequeue()) ;
#if ENABLE_LOGS
        Debug.Log("Attempting to send string '" + ComposedMessage + "' To server.");
#endif

        // Send it!! SEND IT NOW!!
        if (!SendServerMessage(ComposedMessage, SoundHostIP, SoundHostPort))
        {
#if ENABLE_LOGS
            Debug.Log("Failed to send to sound server for some reason!?");
#endif
        }
    }




    /********************************************************************************/
    /********************************************************************************/
    // Function 'void QueueMessage(string NewMessage)'
    //      -Adds any valid or invalid message string into the send queue
    //          *If the message is invalid, then the server will tell you later!
    //-------------------------------------------------------------------------------
    public static void QueueMessage(string NewMessage)
    {
        // Make sure an instance actually exists
        if (CurrentPropServerMessageHandlerInstance == null)
            return;

        // Add the message to the send queue
        CurrentPropServerMessageHandlerInstance.ServerMsgQueue.Enqueue(NewMessage);
    }
    #endregion










    #region UDP Communication
    /********************************************************************************/
    /********************************************************************************/
    /*------------------------------------------------------------------------------*/
    /*-                                                                            -*/
    /*-                                                                            -*/
    /*-                                                                            -*/
    /*-                        UDP Communication Functions                         -*/
    /*-                                                                            -*/
    /*-                                                                            -*/
    /*-                                                                            -*/
    /*------------------------------------------------------------------------------*/
    /********************************************************************************/
    /********************************************************************************/
    // Function 'bool SendServerMessage(string ComposedData, string ServerIp, string ServerPort)'
    //
    //-------------------------------------------------------------------------------
    bool SendServerMessage(string ComposedData, string ServerIp, string ServerPort)
    {
        // Make sure a valid server and port were given
        if (string.IsNullOrEmpty(ServerIp))
            return false;
        if (string.IsNullOrEmpty(ServerPort))
            return false;

        //   Make sure there is a message to send. If the message data is empty,
        // then just return success because sending nothing worked... yeah... :)
        if (string.IsNullOrEmpty(ComposedData))
            return true;

#if USE_LIN_LOCAL_TEST_SERVER

  // Lin's server test code
  UDPRT.CreateInstance(UDPRT.HOST_PORT, ServerIp, ComposedData);

#else

        // Hololens ready code
        UDPcommMNGR comm = UDPcommMNGR.Instance;
        if (comm != null)
            comm.HelpSendMEssage(ServerIp, ServerPort, ComposedData);
        else
        {
#if ENABLE_LOGS
            Debug.Log("No UDPCommunication instance exists!");
#endif
            return false;
        }

#endif

        // Success!
        return true;
    }
    #endregion










    #region Server Message Helpers 
    /********************************************************************************/
    /********************************************************************************/
    /*------------------------------------------------------------------------------*/
    /*-                                                                            -*/
    /*-                                                                            -*/
    /*-                                                                            -*/
    /*-                     Message Creation Helper Functions                      -*/
    /*-                                                                            -*/
    /*-                                                                            -*/
    /*-                                                                            -*/
    /*------------------------------------------------------------------------------*/
    /*-                                                                            -*/
    /*-    Message format: "####<2>t<2>d"                                          -*/
    /*-       -#### is the message index                                           -*/
    /*-       -t    is the type                                                    -*/
    /*-       -d    is the data                                                    -*/
    /*-                                                                            -*/
    /*-    Messages are separated by a primary delimiter  "<1>"                    -*/
    /*-    Message elements are separted by a sub delimiter "<2>"                  -*/
    /*-    Additional message data is separated by a nibble delimiter "<3>"        -*/
    /*-                                                                            -*/
    /********************************************************************************/
    /********************************************************************************/
    // Function 'bool SendServerMessage(string ComposedData, string ServerIp, string ServerPort)'
    //
    //-------------------------------------------------------------------------------
    public static string GetNextMessageID(/*void*/)
    {
        // Increment the message ID. Wrapping at 10,000
        CurrentMessageID = (CurrentMessageID + 1) % 10000;

        // Return the message ID as a 4-glyph string
        return string.Format("{0:0000}", CurrentMessageID);
    }




    /********************************************************************************/
    /********************************************************************************/
    // Function 'void QueueSound(string SoundId, string DeviceId)'
    //
    //-------------------------------------------------------------------------------
    public static void QueueSound(string SoundId, string DeviceId)
    {
        // Message format: "####<2>A<2>s<3>d"
        // #### = Message Index
        // A    = 'A' character to denote as an audio message
        // s    = a sound ID to play
        // d    = The device ID (number or string) that will play the sound
        string ComposedMessage = GetNextMessageID() + SUB_DELEM + "A" + SUB_DELEM + SoundId + NIBBLE_DELEM + DeviceId;

        // Queue it up for sending!
        QueueMessage(ComposedMessage);
    }




    /********************************************************************************/
    /********************************************************************************/
    // Function 'void QueueAlphaSound(string SoundId)'
    //
    //-------------------------------------------------------------------------------
    public static void QueueAlphaSound(string SoundId)
    {
        QueueSound(SoundId, SOUND_DEVICE_ID__PLAYER_ALHPA + "Sfx");
    }




    /********************************************************************************/
    /********************************************************************************/
    // Function 'void QueueBravoSound(string SoundId)'
    //
    //-------------------------------------------------------------------------------
    public static void QueueBravoSound(string SoundId)
    {
        QueueSound(SoundId, SOUND_DEVICE_ID__PLAYER_BRAVO + "Sfx");
    }




    /********************************************************************************/
    /********************************************************************************/
    // Function 'void QueueControlPanelSound(string SoundId)'
    //
    //-------------------------------------------------------------------------------
    public static void QueueControlPanelSound(string SoundId)
    {
        QueueSound(SoundId, SOUND_DEVICE_ID__CONTROL_PANEL_COMMON + "Sfx");
    }



    /********************************************************************************/
    /********************************************************************************/
    // Function 'void QueueLightPulse(string DeviceId)'
    //
    //-------------------------------------------------------------------------------
    public static void QueueLightPulse(string DeviceId)
    {
        // Message format: "####<2>L<2>e<3>d"
        // #### = Message Index
        // L    = 'L' character to denote as an audio message
        // d    = The device ID (number or string) that will play the sound
        string ComposedMessage = GetNextMessageID() + SUB_DELEM + "L" + SUB_DELEM + "PULSE" + NIBBLE_DELEM + DeviceId;

        // Queue it up for sending!
        QueueMessage(ComposedMessage);
    }




    /********************************************************************************/
    /********************************************************************************/
    // Function 'void QueueAlphaSound(void)'
    //
    //-------------------------------------------------------------------------------
    public static void QueueAlphaFlash(/*void*/)
    {
        QueueLightPulse(SOUND_DEVICE_ID__PLAYER_ALHPA + "Flash");
    }




    /********************************************************************************/
    /********************************************************************************/
    // Function 'void QueueBravoSound(void)'
    //
    //-------------------------------------------------------------------------------
    public static void QueueBravoFlash(/*void*/)
    {
        QueueLightPulse(SOUND_DEVICE_ID__PLAYER_BRAVO + "Flash");
    }
    #endregion
}


//#define ENABLE_LOGS
////#define USE_LIN_LOCAL_TEST_SERVER

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PropServerMessageHandler : MonoBehaviour
//{
//    // Singleton stuff
//    static PropServerMessageHandler CurrentPropServerMessageHandlerInstance = null;

//    //---------------------------------------------------------
//    // Server data
//    //---------------------------------------------------------
//    public const int SOUND_HOST_PORT = 7791;
//    public const string SOUND_HOST_IP = "192.168.1.2";



//    //---------------------------------------------------------
//    // Standard Device IDs
//    //---------------------------------------------------------
//    const string SOUND_DEVICE_ID__PLAYER_ALHPA = "AlphaGun";
//    const string SOUND_DEVICE_ID__PLAYER_BRAVO = "BravoGun";
//    const string SOUND_DEVICE_ID__CONTROL_PANEL_COMMON = "CentreGun";



//    //---------------------------------------------------------
//    // Handling Data
//    //---------------------------------------------------------
//    const string PRIM_DELEM = "<1>";
//    const string SUB_DELEM = "<2>";
//    const string NIBBLE_DELEM = "<3>";
//    static int CurrentMessageID = 0;

//    // The container for all requested messages to live before they are sent out
//    Queue<string> ServerMsgQueue;




//    #region Startup and Shutdown Functions
//    /********************************************************************************/
//    /********************************************************************************/
//    /*------------------------------------------------------------------------------*/
//    /*-                                                                            -*/
//    /*-                                                                            -*/
//    /*-                                                                            -*/
//    /*-                         Startup/Shutdown Functions                         -*/
//    /*-                                                                            -*/
//    /*-                                                                            -*/
//    /*-                                                                            -*/
//    /*------------------------------------------------------------------------------*/
//    /********************************************************************************/
//    /********************************************************************************/
//    // Function 'void Start(void)'
//    //
//    //-------------------------------------------------------------------------------
//    void Awake(/*void*/)
//    {
//        // Only one instance allowed
//        if (CurrentPropServerMessageHandlerInstance != null)
//        {
//            Destroy(gameObject);
//            return;
//        }

//        // Store this as the singleton
//        CurrentPropServerMessageHandlerInstance = this;

//        // Create a message queue
//        ServerMsgQueue = new Queue<string>();
//    }




//    /********************************************************************************/
//    /********************************************************************************/
//    // Function 'void OnDestroy(void)'
//    //
//    //-------------------------------------------------------------------------------
//    void OnDestroy(/*void*/)
//    {
//        // Clear this instance if this is the valid singleton
//        if (CurrentPropServerMessageHandlerInstance == this)
//            CurrentPropServerMessageHandlerInstance = null;
//    }
//    #endregion










//    #region General Updating
//    /********************************************************************************/
//    /********************************************************************************/
//    /*------------------------------------------------------------------------------*/
//    /*-                                                                            -*/
//    /*-                                                                            -*/
//    /*-                                                                            -*/
//    /*-                         General Updating Functions                         -*/
//    /*-                                                                            -*/
//    /*-                                                                            -*/
//    /*-                                                                            -*/
//    /*------------------------------------------------------------------------------*/
//    /********************************************************************************/
//    /********************************************************************************/
//    // Function 'void Start(void)'
//    //
//    //-------------------------------------------------------------------------------
//    void Update(/*void*/)
//    {
//        // OMGWTF Only one instance allowed!!!!!!!!1111111111111111~~~~~~~~~
//        if (CurrentPropServerMessageHandlerInstance != this)
//        {
//            Destroy(gameObject);
//            return;
//        }

//        // Do nothing if we have no messages
//        if (ServerMsgQueue.Count <= 0)
//            return;

//        // Build the message set
//        string ComposedMessage = ServerMsgQueue.Dequeue();
//        for (; ServerMsgQueue.Count > 0; ComposedMessage += PRIM_DELEM + ServerMsgQueue.Dequeue()) ;
//#if ENABLE_LOGS
//        Debug.Log("Attempting to send string '" + ComposedMessage + "' To server.");
//#endif

//        // Send it!! SEND IT NOW!!
//        if (!SendServerMessage(ComposedMessage, GameSettings.Instance.GetSTR_IP_Audio(), GameSettings.Instance.GetSTR_STR_Port_Audio()))
//        {
//#if ENABLE_LOGS
//            Debug.Log("Failed to send to sound server for some reason!?");
//#endif
//        }
//    }




//    /********************************************************************************/
//    /********************************************************************************/
//    // Function 'void QueueMessage(string NewMessage)'
//    //      -Adds any valid or invalid message string into the send queue
//    //          *If the message is invalid, then the server will tell you later!
//    //-------------------------------------------------------------------------------
//    public static void QueueMessage(string NewMessage)
//    {
//        // Make sure an instance actually exists
//        if (CurrentPropServerMessageHandlerInstance == null)
//            return;

//        // Add the message to the send queue
//        CurrentPropServerMessageHandlerInstance.ServerMsgQueue.Enqueue(NewMessage);
//    }
//    #endregion










//    #region UDP Communication
//    /********************************************************************************/
//    /********************************************************************************/
//    /*------------------------------------------------------------------------------*/
//    /*-                                                                            -*/
//    /*-                                                                            -*/
//    /*-                                                                            -*/
//    /*-                        UDP Communication Functions                         -*/
//    /*-                                                                            -*/
//    /*-                                                                            -*/
//    /*-                                                                            -*/
//    /*------------------------------------------------------------------------------*/
//    /********************************************************************************/
//    /********************************************************************************/
//    // Function 'bool SendServerMessage(string ComposedData, string ServerIp, string ServerPort)'
//    //
//    //-------------------------------------------------------------------------------
//    bool SendServerMessage(string ComposedData, string ServerIp, string ServerPort)
//    {
//        // Make sure a valid server and port were given
//        if (string.IsNullOrEmpty(ServerIp))
//            return false;
//        if (string.IsNullOrEmpty(ServerPort))
//            return false;

//        //   Make sure there is a message to send. If the message data is empty,
//        // then just return success because sending nothing worked... yeah... :)
//        if (string.IsNullOrEmpty(ComposedData))
//            return true;

//#if USE_LIN_LOCAL_TEST_SERVER

//  // Lin's server test code
//  UDPRT.CreateInstance(UDPRT.HOST_PORT, ServerIp, ComposedData);

//#else

//        // Hololens ready code
//        UDPcommMNGR comm = UDPcommMNGR.Instance;
//        if (comm != null)
//            comm.HelpSendMEssage(ServerIp, ServerPort, ComposedData);
//        else
//        {
//#if ENABLE_LOGS
//            Debug.Log("No UDPCommunication instance exists!");
//#endif
//            return false;
//        }

//#endif

//        // Success!
//        return true;
//    }
//    #endregion










//    #region Server Message Helpers 
//    /********************************************************************************/
//    /********************************************************************************/
//    /*------------------------------------------------------------------------------*/
//    /*-                                                                            -*/
//    /*-                                                                            -*/
//    /*-                                                                            -*/
//    /*-                     Message Creation Helper Functions                      -*/
//    /*-                                                                            -*/
//    /*-                                                                            -*/
//    /*-                                                                            -*/
//    /*------------------------------------------------------------------------------*/
//    /*-                                                                            -*/
//    /*-    Message format: "####<2>t<2>d"                                          -*/
//    /*-       -#### is the message index                                           -*/
//    /*-       -t    is the type                                                    -*/
//    /*-       -d    is the data                                                    -*/
//    /*-                                                                            -*/
//    /*-    Messages are separated by a primary delimiter  "<1>"                    -*/
//    /*-    Message elements are separted by a sub delimiter "<2>"                  -*/
//    /*-    Additional message data is separated by a nibble delimiter "<3>"        -*/
//    /*-                                                                            -*/
//    /********************************************************************************/
//    /********************************************************************************/
//    // Function 'bool SendServerMessage(string ComposedData, string ServerIp, string ServerPort)'
//    //
//    //-------------------------------------------------------------------------------
//    static string GetNextMessageID(/*void*/)
//    {
//        // Increment the message ID. Wrapping at 10,000
//        CurrentMessageID = (CurrentMessageID + 1) % 10000;

//        // Return the message ID as a 4-glyph string
//        return string.Format("{0:0000}", CurrentMessageID);
//    }




//    /********************************************************************************/
//    /********************************************************************************/
//    // Function 'void QueueSound(string SoundId, string DeviceId)'
//    //
//    //-------------------------------------------------------------------------------
//    public static void QueueSound(string SoundId, string DeviceId)
//    {
//        // Message format: "####<2>A<2>s<3>d"
//        // #### = Message Index
//        // A    = 'A' character to denote as an audio message
//        // s    = a sound ID to play
//        // d    = The device ID (number or string) that will play the sound
//        string ComposedMessage = GetNextMessageID() + SUB_DELEM + "A" + SUB_DELEM + SoundId + NIBBLE_DELEM + DeviceId;

//        // Queue it up for sending!
//        QueueMessage(ComposedMessage);
//    }




//    /********************************************************************************/
//    /********************************************************************************/
//    // Function 'void QueueAlphaSound(string SoundId)'
//    //
//    //-------------------------------------------------------------------------------
//    public static void QueueAlphaSound(string SoundId)
//    {
//        QueueSound(SoundId, SOUND_DEVICE_ID__PLAYER_ALHPA);
//    }




//    /********************************************************************************/
//    /********************************************************************************/
//    // Function 'void QueueBravoSound(string SoundId)'
//    //
//    //-------------------------------------------------------------------------------
//    public static void QueueBravoSound(string SoundId)
//    {
//        QueueSound(SoundId, SOUND_DEVICE_ID__PLAYER_BRAVO);
//    }




//    /********************************************************************************/
//    /********************************************************************************/
//    // Function 'void QueueControlPanelSound(string SoundId)'
//    //
//    //-------------------------------------------------------------------------------
//    public static void QueueControlPanelSound(string SoundId)
//    {
//        QueueSound(SoundId, SOUND_DEVICE_ID__CONTROL_PANEL_COMMON);
//    }
//    #endregion
//}