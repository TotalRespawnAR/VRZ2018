using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsUI : MonoBehaviour {

    void TurnOn(MeshRenderer argMR)
    {
        argMR.material.color = Color.red;
    }
    void TurnOff(MeshRenderer argMR)
    {
        argMR.material.color = Color.black;
    }


    // Use this for initialization
    void Start()
    {
        UpdateColor_BG_Alpha_BG_Bravo();
        UpdateColor_BG_Stem_BG_Striker();
        UpdateColor_BG_Righty_BG_Lefty();
        UpdateColor_BG_Noob_Easy_BG_Medium_BG_Hard();
    }

    public MeshRenderer BG_Alpha;
    public MeshRenderer BG_Bravo;

    void UpdateColor_BG_Alpha_BG_Bravo()
    {
        if (GameSettings.Instance.GameMode == ARZGameModes.GameLeft_Alpha)
        {
            TurnOn(BG_Alpha);
            TurnOff(BG_Bravo);
        }
        else
        if (GameSettings.Instance.GameMode == ARZGameModes.GameRight_Bravo)
        {
            TurnOn(BG_Bravo);
            TurnOff(BG_Alpha);
        }
        else
        {
            TurnOff(BG_Alpha);
            TurnOff(BG_Bravo);
        }
    }
    public void DoClick_Alpha()
    {
        GameSettings.Instance.GameMode = ARZGameModes.GameLeft_Alpha;
        UpdateColor_BG_Alpha_BG_Bravo();
    }

    public void DoClick_Bavo()
    {
        GameSettings.Instance.GameMode = ARZGameModes.GameRight_Bravo;
        UpdateColor_BG_Alpha_BG_Bravo();
    }



    public MeshRenderer BG_Stem;
    public MeshRenderer BG_Striker;
    void UpdateColor_BG_Stem_BG_Striker()
    {
        if (GameSettings.Instance.Controlertype == ARZControlerType.StemControlSystem)
        {
            TurnOn(BG_Stem);
            TurnOff(BG_Striker);
        }
        else
        if (GameSettings.Instance.Controlertype == ARZControlerType.StrikerControlSystem)
        {
            TurnOn(BG_Striker);
            TurnOff(BG_Stem);
        }
        else
        {
            TurnOff(BG_Stem);
            TurnOff(BG_Striker);
        }
    }
    public void DoClick_Stem()
    {
        GameSettings.Instance.Controlertype = ARZControlerType.StemControlSystem;
        UpdateColor_BG_Stem_BG_Striker();
    }

    public void DoClick_BG_Striker()
    {
        GameSettings.Instance.Controlertype = ARZControlerType.StrikerControlSystem;
        UpdateColor_BG_Stem_BG_Striker();
    }





    public MeshRenderer BG_Righty;
    public MeshRenderer BG_Lefty;
    void UpdateColor_BG_Righty_BG_Lefty()
    {
        if (GameSettings.Instance.PlayerLeftyRight == ARZPlayerLeftyRighty.RightyPlayer)
        {
            TurnOn(BG_Righty);
            TurnOff(BG_Lefty);
        }
        else
        if (GameSettings.Instance.PlayerLeftyRight == ARZPlayerLeftyRighty.LeftyPlayer)
        {
            TurnOn(BG_Lefty);
            TurnOff(BG_Righty);
        }
        else
        {
            TurnOff(BG_Righty);
            TurnOff(BG_Lefty);
        }
    }
    public void DoClick_Righty()
    {
        GameSettings.Instance.PlayerLeftyRight = ARZPlayerLeftyRighty.RightyPlayer;
        UpdateColor_BG_Righty_BG_Lefty();
    }

    public void DoClick_BG_Lefty()
    {
        GameSettings.Instance.PlayerLeftyRight = ARZPlayerLeftyRighty.LeftyPlayer;
        UpdateColor_BG_Righty_BG_Lefty();
    }



    public MeshRenderer BG_Easy;
    public MeshRenderer BG_Medium;
    public MeshRenderer BG_Hard;
    public MeshRenderer BG_Noob;
    void UpdateColor_BG_Noob_Easy_BG_Medium_BG_Hard()
    {
        if (GameSettings.Instance.ReloadDifficulty == ARZReloadLevel.NOOB)
        {
            TurnOff(BG_Easy);
            TurnOff(BG_Medium);
            TurnOff(BG_Hard);
            TurnOn(BG_Noob);
        }
        else
          if (GameSettings.Instance.ReloadDifficulty == ARZReloadLevel.EASY)
        {
            TurnOn(BG_Easy);
            TurnOff(BG_Medium);
            TurnOff(BG_Hard);
            TurnOff(BG_Noob);
        }
        else
        if (GameSettings.Instance.ReloadDifficulty == ARZReloadLevel.MEDIUM)
        {
            TurnOff(BG_Easy);
            TurnOn(BG_Medium);
            TurnOff(BG_Hard);
            TurnOff(BG_Noob);
        }
        else
        if (GameSettings.Instance.ReloadDifficulty == ARZReloadLevel.HARD)
        {
            TurnOff(BG_Easy);
            TurnOff(BG_Medium);
            TurnOn(BG_Hard);
            TurnOff(BG_Noob);
        }
     

    }

    public void DoClick_Noob()
    {
        GameSettings.Instance.ReloadDifficulty = ARZReloadLevel.NOOB;
        UpdateColor_BG_Noob_Easy_BG_Medium_BG_Hard();
    }

    public void DoClick_Easy() {
        GameSettings.Instance.ReloadDifficulty = ARZReloadLevel.EASY;
        UpdateColor_BG_Noob_Easy_BG_Medium_BG_Hard();
    }
    public void DoClick_Medium() {
        GameSettings.Instance.ReloadDifficulty = ARZReloadLevel.MEDIUM;

        UpdateColor_BG_Noob_Easy_BG_Medium_BG_Hard();
    }
    public void DoClick_Hard() {
        GameSettings.Instance.ReloadDifficulty = ARZReloadLevel.HARD;

            UpdateColor_BG_Noob_Easy_BG_Medium_BG_Hard();
    }
}
