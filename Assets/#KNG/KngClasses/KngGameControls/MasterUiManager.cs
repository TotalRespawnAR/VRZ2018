// @Author Nabil Lamriben ©2018
using UnityEngine;

public class MasterUiManager : MonoBehaviour
{
    #region dependencies
    public HoloUiCTRL UI_Holo;
    public RoomUiCTRL UI_Room;

    #endregion

    #region PublicVars
    #endregion

    #region PrivateVars
    #endregion

    #region INIT
    void OnEnable()
    {
        GameEventsManager.OnSuddenDeath += Run_SuddenDeath;

        GameEventsManager.OnOnBoardDisplay += OnblardDisplay_WaveBonus;
    }

    private void OnDisable()
    {
        GameEventsManager.OnSuddenDeath -= Run_SuddenDeath;

        GameEventsManager.OnOnBoardDisplay -= OnblardDisplay_WaveBonus;
    }

    private void Awake()
    {

    }

    private void Start()
    {
        //Debug.Log("masterUI is on " + gameObject.name);
    }
    #endregion

    #region PublicMethods
    public void OnblardDisplay_WaveBonus(int argWaveBonus, bool IsAdded)
    {
        //  UI_Holo.DisplayBonusPointsAddedOrLostFor4Seconds(argWaveBonus, IsAdded);
        UI_Room.Set_3D_Title("wave bonus "+argWaveBonus.ToString(), false);
    }

    //  public void Run_TakeHit(HitType _argHitType) { UI_Holo.TakeHit(_argHitType); }
    //  public void Run_KillPlayer() { UI_Holo.KillePlayer(); }
    public void Run_FinalScore_with_3dTitle(int argFinalPoints)
    {
        UI_Room.ShowTextOnCanvas_thenFadeit_andPut3DTitle("Points: \n" + argFinalPoints.ToString() + "\n", true);
        UI_Room.Set_3D_Title("Final Score", true);

    }
    public void Run_GameOver()
    {
        UI_Room.Set_3D_Title("Game Over", false);
    }
    public void Run_YouDied(int argLostPoints)
    {
        UI_Room.Set_3D_Title("Get Ready", false);
        UI_Room.ShowTextOnCanvas_thenFadeit_andPut3DTitle("Lost Points: " + argLostPoints.ToString() + "\n Respawning again ", false);
    }
    public void Run_PlayGameOverAudio()
    {
        UI_Holo.PlayGameOverAudio();
    }
    // public void Run_ResetDamage() { UI_Holo.ResetDamage(); }
    public void Run_ResetWave() { UI_Room.Set_3D_Title("Respawning", false); }
    public void Run_WaveCompleteWeaponUpgare()
    {
        UI_Room.ShowTextOnCanvas_thenFadeit_andPut3DTitle("Wave Completed", false);
        UI_Room.Set_3D_Title("get ready", false);
        //        GameManager.Instance.MistsHIdeShow(false);
    }
    public void Run_WaveStarted(string argWaveRoman)
    {
        //      GameManager.Instance.MistsHIdeShow(true);

        if (argWaveRoman == "V")
        {
            UI_Room.Set_3D_Title(" Final Wave ", true);
            return;
        }
        string WaveNumberDecimal = ConvertRomanToDecimalNumberString(argWaveRoman);
        UI_Room.Set_3D_Title("wave " + WaveNumberDecimal, (WaveNumberDecimal == "5") ? true : false);
        //UI_Room.ShowTextOnCanvas_thenFadeit_andPut3DTitle("\n Zombie Wave is starting \n",false);
    }



    void Run_SuddenDeath()
    {
        print("sudden death called");
        UI_Holo.PlaySuddenDeathAudio();
        UI_Room.Set_3D_Title("Sudden Death", false);
        UI_Room.ShowTextOnCanvas_thenFadeit_andPut3DTitle(" ", false);
    }
    #endregion

    #region PrivateMethods
    public string ConvertRomanToDecimalNumberString(string argromanNumeral)
    {
        string IntegerOutput = "0";

        switch (argromanNumeral)
        {
            case "I":
                IntegerOutput = "1";
                break;
            case "II":
                IntegerOutput = "2";
                break;
            case "III":
                IntegerOutput = "3";
                break;
            case "IV":
                IntegerOutput = "4";
                break;
            case "V":
                IntegerOutput = "5";
                break;
            case "VI":
                IntegerOutput = "6";
                break;
            case "VII":
                IntegerOutput = "7";
                break;
            case "VIII":
                IntegerOutput = "8";
                break;
            case "IX":
                IntegerOutput = "9";
                break;
            case "X":
                IntegerOutput = "10";
                break;
            case "XI":
                IntegerOutput = "11";
                break;
            default:
                IntegerOutput = "0";
                break;
        }
        return IntegerOutput;
    }
    #endregion
}
