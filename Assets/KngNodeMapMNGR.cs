using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KngNodeMapMNGR : MonoBehaviour {


    private void OnEnable()
    {
        GameEventsManager.OnLocalCrateChangedState += SendCrateUpdate;
        GameEventsManager.OnRemoteCrateReceived += ReceivedCrateUpdate;
    }

    private void OnDisable()
    {
        GameEventsManager.OnLocalCrateChangedState -= SendCrateUpdate;
        GameEventsManager.OnRemoteCrateReceived -= ReceivedCrateUpdate;
    }

    public GameObject CrateCtrlObj;
    Dictionary<int , CrateCtrl> CratesDICT = new Dictionary<int, CrateCtrl>();
    //This will keep track of obstacleboxes and offer zombies the best next node when they requestone 


  

    public void ReceivedCrateUpdate(int CrateId, int CrateState) {
        // message from other hl
        // update local visuals
        CrateCtrl crateToUpdate = CratesDICT[CrateId];
        crateToUpdate.UpdateCrateVisual_fromReceivingUDP(CrateState);

    }

    public void SendCrateUpdate(int CrateId, int CrateState)
    {
        //  local visuals already changed
        // message To other hlz
        string cratemessage = GameSettings.Instance.GetMSG_crateUpdate() + "#" + CrateId + "#" + CrateState;
        print(cratemessage);
#if !UNITY_EDITOR
          if (GameSettings.Instance.GAmeSessionType == ARZGameSessionType.MULTI)
        {
        UDPcommMNGR.Instance.HelpSendMEssage(GameSettings.Instance.Ip_External_OtherHL, GameSettings.Instance.Port_External_OtherHL, cratemessage);
        }
#endif
    }
     
    int crateId = 0;



   
    

}
