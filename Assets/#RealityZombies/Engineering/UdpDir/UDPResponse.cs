using System;
using UnityEngine;

public class UDPResponse : MonoBehaviour
{


    public TextMesh tm = null;
    // public gunhelper gunHelp; Now using events and gunhelper listens

    public void ResponseToUDPPacket(string incomingIP, string incomingPort, byte[] data)
    {
        string messageSTRreceived1 = System.Text.Encoding.UTF8.GetString(data);
        RzPlayerComponent.Instance.PlayHud_Debug_Vertical(messageSTRreceived1);


        if (GameSettings.Instance.GAmeSessionType == ARZGameSessionType.MULTI)
        {
            if (UDPcommMNGR.Instance != null)
            {
                string messageSTRreceived = System.Text.Encoding.UTF8.GetString(data);

                if (messageSTRreceived[0] == '#')
                {
                    string OtherScoreStr = messageSTRreceived.Substring(1);
                    GameEventsManager.Instance.Call_OnUpdateScoreFromOtherHL(OtherScoreStr);
                }
                else
                if (messageSTRreceived.Contains(GameSettings.Instance.GetMSG_smallAttack()))
                {
                    GameEventsManager.Instance.Call_OtherHLSmallAttack();
                }
                else
                if (messageSTRreceived.Contains(GameSettings.Instance.GetMSG_StartGame()))
                {
                    GameEventsManager.Instance.Call_OtherHlStartedGame();
                }

                else
                if (messageSTRreceived.Contains(GameSettings.Instance.GetMSG_crateUpdate()))
                {
                    // crate#1#3
                    string[] splitmessage = messageSTRreceived.Split('#');
                    int crateId = Int32.Parse(splitmessage[1]);

                    int crateste = Int32.Parse(splitmessage[2]);
                    GameEventsManager.Instance.Call_REMOTECrateChangedState(crateId, crateste);
                }



                //else
                //if (messageSTRreceived[0] == '$'){}
            }

        }

        if (GameSettings.Instance.Controlertype == ARZControlerType.StrikerControlSystem)
        {
            if (UDPcommMNGR.Instance != null)
            {
                string messageSTRreceived = System.Text.Encoding.UTF8.GetString(data);
                if (messageSTRreceived.Length > 0)
                {
                    if (messageSTRreceived.Contains("shooting"))
                    {
                        GameEventsManager.Instance.Call_StrikerShoot();
#if !UNITY_EDITOR
                        UDPcommMNGR.Instance.HelpSendMEssage(GameSettings.Instance.Ip_External_AudioServer, GameSettings.Instance.Port_External_AudioServer, "5555Aa9x");
#endif
                    }
                    else
                    if (messageSTRreceived.Contains("reloadin"))
                    {
                        GameEventsManager.Instance.Call_StrikerReload();
#if !UNITY_EDITOR
                        UDPcommMNGR.Instance.HelpSendMEssage(GameSettings.Instance.Ip_External_AudioServer, GameSettings.Instance.Port_External_AudioServer, "0123Cc9x");
#endif
                    }
                    else
                    if (messageSTRreceived.Contains("outofamm"))
                    {
                        GameEventsManager.Instance.Call_StrikerDryFire();
#if !UNITY_EDITOR
                        UDPcommMNGR.Instance.HelpSendMEssage(GameSettings.Instance.Ip_External_AudioServer, GameSettings.Instance.Port_External_AudioServer, "0123Ba9x");
#endif
                    }
                }

            }
        }

    }
}

