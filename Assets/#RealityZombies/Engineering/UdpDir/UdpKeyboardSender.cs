using UnityEngine;

#if !UNITY_EDITOR && UNITY_WSA
         

using Windows.Networking.Connectivity;
using Windows.Networking;
#endif


public class UdpKeyboardSender : MonoBehaviour
{

    int cnt = 0;


    ////bool toggleAutosend = false;
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            cnt++;
            UDPcommMNGR.Instance.HelpSendMEssage("yo hello there");
        }

    }
}
