using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModesUI : MonoBehaviour {

    void TurnOn(MeshRenderer argMR)
    {
        argMR.material.color = Color.red;
    }
    void TurnOff(MeshRenderer argMR)
    {
        argMR.material.color = Color.black;
    }

    // Use this for initialization
    void Start () {
        UpdateColor_BG_Single_BG_Multi_BG_Udp();
        UpdateColor_BG_Blood(GameSettings.Instance.IsBloodOn);
        UpdateColor_BG_LazerHolo(GameSettings.Instance.IsCanToggleLazerOn);
        UpdateColor_BG_CanSelectReload(GameSettings.Instance.IsCanSelectReloadON);
        UpdateColor_BG_CanSelectRoom(GameSettings.Instance.IsCanSelectRoomOn);
        UpdateColor_BG_IsTestModeOn(GameSettings.Instance.IsTestModeON);
        UpdateColor_BG_IsGodModeOn(GameSettings.Instance.IsIsGodModeON);
        UpdateColor_BG_IsLoggerModeeOn(GameSettings.Instance.IsLoggerOn);
        UpdateColor_BG_IsKingstonModeeOn(GameSettings.Instance.IsKingstonOn);
        UpdateColor_BG_IsConsoleDebugModeeOn(GameSettings.Instance.IsConsoleDebugOn);
        UpdateColor_BG_IsCratesModeOn(GameSettings.Instance.IsCratesOn);
    }


    #region Single_Multi_UDP
    public MeshRenderer BG_Single;
    public MeshRenderer BG_Multi;
    public MeshRenderer BG_Udp;

    void UpdateColor_BG_Single_BG_Multi_BG_Udp()
    {
        if (GameSettings.Instance.GAmeSessionType == ARZGameSessionType.SINGLE)
        {
            TurnOn(BG_Single);
            TurnOff(BG_Multi);
            TurnOff(BG_Udp);

        }
        else
        if (GameSettings.Instance.GAmeSessionType == ARZGameSessionType.MULTI)
        {
            TurnOff(BG_Single);
            TurnOn(BG_Multi);
            TurnOff(BG_Udp);
        }else
        if (GameSettings.Instance.GAmeSessionType == ARZGameSessionType.UDP)
        {
            TurnOff(BG_Single);
            TurnOff(BG_Multi);
            TurnOn(BG_Udp);
        }
        else
        {
            TurnOff(BG_Single);
            TurnOff(BG_Multi);
            TurnOff(BG_Udp);
        }
    }

    public void DoClick_Single()
    {
        GameSettings.Instance.GAmeSessionType = ARZGameSessionType.SINGLE;
        UpdateColor_BG_Single_BG_Multi_BG_Udp();
    }
    public void DoClick_Multi()
    {
        GameSettings.Instance.GAmeSessionType = ARZGameSessionType.MULTI;
        UpdateColor_BG_Single_BG_Multi_BG_Udp();
    }
    public void DoClick_Udp()
    {
        GameSettings.Instance.GAmeSessionType = ARZGameSessionType.UDP;
        UpdateColor_BG_Single_BG_Multi_BG_Udp();
    }
    #endregion

    #region Blood
    public MeshRenderer BG_Blood;
    void UpdateColor_BG_Blood(bool argBoolBound_Blood){if (argBoolBound_Blood) { TurnOn(BG_Blood); } else { TurnOff(BG_Blood); } }
    public void DoToggleValueAndColor_Blood(){GameSettings.Instance.IsBloodOn = !GameSettings.Instance.IsBloodOn;UpdateColor_BG_Blood(GameSettings.Instance.IsBloodOn);}
    #endregion


    #region CAnToggleLazerHolo
    public MeshRenderer BG_LazerHolo;
    void UpdateColor_BG_LazerHolo(bool argBoolBound_LazerHolo) { if (argBoolBound_LazerHolo) { TurnOn(BG_LazerHolo); } else { TurnOff(BG_LazerHolo); } }
    public void DoToggleValueAndColor_LazerHoloToggler() { GameSettings.Instance.IsCanToggleLazerOn = !GameSettings.Instance.IsCanToggleLazerOn; UpdateColor_BG_LazerHolo(GameSettings.Instance.IsCanToggleLazerOn); }
    #endregion


    #region CAnSelectReload
    public MeshRenderer BG_CanSelectReload;
    void UpdateColor_BG_CanSelectReload(bool argBoolBound_CanSelectReload) { if (argBoolBound_CanSelectReload) { TurnOn(BG_CanSelectReload); } else { TurnOff(BG_CanSelectReload); } }
    public void DoToggleValueAndColor_BG_CanSelectReload() { GameSettings.Instance.IsCanSelectReloadON = !GameSettings.Instance.IsCanSelectReloadON; UpdateColor_BG_CanSelectReload(GameSettings.Instance.IsCanSelectReloadON); }
    #endregion


    #region CAnSelectRoom
    public MeshRenderer BG_CanSelectRoom;
    void UpdateColor_BG_CanSelectRoom(bool argBoolBound_CanSelectRoom) { if (argBoolBound_CanSelectRoom) { TurnOn(BG_CanSelectRoom); } else { TurnOff(BG_CanSelectRoom); } }
    public void DoToggleValueAndColor_BG_CanSelectRoom() { GameSettings.Instance.IsCanSelectRoomOn = !GameSettings.Instance.IsCanSelectRoomOn; UpdateColor_BG_CanSelectRoom(GameSettings.Instance.IsCanSelectRoomOn); }
    #endregion



    #region TestMode
    public MeshRenderer BG_IsTestModeOn;
    void UpdateColor_BG_IsTestModeOn(bool argBoolBound_isTestModeOn) { if (argBoolBound_isTestModeOn) { TurnOn(BG_IsTestModeOn); } else { TurnOff(BG_IsTestModeOn); } }
    public void DoToggleValueAndColor_BG_IsTestModeOn() { GameSettings.Instance.IsTestModeON = !GameSettings.Instance.IsTestModeON; UpdateColor_BG_IsTestModeOn(GameSettings.Instance.IsTestModeON); }
    #endregion


    #region GodMode
    public MeshRenderer BG_IsGodModeOn;
    void UpdateColor_BG_IsGodModeOn(bool argBoolBound_isGodModeOn) { if (argBoolBound_isGodModeOn) { TurnOn(BG_IsGodModeOn); } else { TurnOff(BG_IsGodModeOn); } }
    public void DoToggleValueAndColor_BG_IsGodOn() { GameSettings.Instance.IsIsGodModeON = !GameSettings.Instance.IsIsGodModeON; UpdateColor_BG_IsGodModeOn(GameSettings.Instance.IsIsGodModeON); }
    #endregion

    #region LoggerMode
    public MeshRenderer BG_IsLoggerModeOn;
    void UpdateColor_BG_IsLoggerModeeOn(bool argBoolBound_isLoggerModeOn) { if (argBoolBound_isLoggerModeOn) { TurnOn(BG_IsLoggerModeOn); } else { TurnOff(BG_IsLoggerModeOn); } }
    public void DoToggleValueAndColor_BG_IsLoggerOn() { GameSettings.Instance.IsLoggerOn = !GameSettings.Instance.IsLoggerOn; UpdateColor_BG_IsLoggerModeeOn(GameSettings.Instance.IsLoggerOn); }
    #endregion

    #region KingstonMode
    public MeshRenderer BG_IsKingstonModeOn;
    void UpdateColor_BG_IsKingstonModeeOn(bool argBoolBound_isKingstonModeOn) { if (argBoolBound_isKingstonModeOn) { TurnOn(BG_IsKingstonModeOn); } else { TurnOff(BG_IsKingstonModeOn); } }
    public void DoToggleValueAndColor_BG_IsKingstonOn() { GameSettings.Instance.IsKingstonOn = !GameSettings.Instance.IsKingstonOn; UpdateColor_BG_IsKingstonModeeOn(GameSettings.Instance.IsKingstonOn); }
    #endregion

    #region ConsoleDebugMode
    public MeshRenderer BG_IsConsoleDebugModeOn;
    void UpdateColor_BG_IsConsoleDebugModeeOn(bool argBoolBound_isConsoleDebugModeOn) { if (argBoolBound_isConsoleDebugModeOn) { TurnOn(BG_IsConsoleDebugModeOn); } else { TurnOff(BG_IsConsoleDebugModeOn); } }
    public void DoToggleValueAndColor_BG_IsConsoleDebugOn() { GameSettings.Instance.IsConsoleDebugOn = !GameSettings.Instance.IsConsoleDebugOn; UpdateColor_BG_IsConsoleDebugModeeOn(GameSettings.Instance.IsConsoleDebugOn); }
    #endregion

    #region CratesMode
    public MeshRenderer BG_IsCratesModeOn;
    void UpdateColor_BG_IsCratesModeOn(bool argBoolBound_isCratesModeOn) { if (argBoolBound_isCratesModeOn) { TurnOn(BG_IsCratesModeOn); } else { TurnOff(BG_IsCratesModeOn); } }
    public void DoToggleValueAndColor_BG_IsCratesOn() { GameSettings.Instance.IsCratesOn = !GameSettings.Instance.IsCratesOn; UpdateColor_BG_IsCratesModeOn(GameSettings.Instance.IsCratesOn); }
    #endregion
    // UpdateColor_BG_IsConsoleDebugModeeOn(GameSettings.Instance.IsConsoleDebugOn);


}
