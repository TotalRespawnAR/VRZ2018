// @Author Nabil Lamriben ©2018
#define ENABLE_LOGS
using System.Collections;
using UnityEngine;

public class HoloUiCTRL : MonoBehaviour
{

    /// <summary>
    ///  This is for shwing ui stuff on the hololens screen. 
    ///  this class should only handle the folowwing
    ///  show Damage to player
    ///  Show Countdown to end of game
    ///  Playig Audio to player
    /// </summary>

    #region dependencies
    // UAudioManager audioManager;

    #endregion

    #region PublicVars
    public TextMesh OnboardPointsUI;
    #endregion

    #region PrivateVars

    #endregion

    #region INIT
    void OnEnable()
    {

    }

    private void OnDisable()
    {

    }
    //private void Update()
    //{

    //  //  DebugHP(RzPlayerComponent.Instance.GEt_HEalth_Percent_01());// RzPlayerComponent.Instance.PlayerCurHP);
    //   // CurHealthPercent_0_1 = 
    //}

    private void Awake()
    {

        //audioManager = gameObject.GetComponent<UAudioManager>();
    }

    private void Start()
    {

    }
    #endregion

    #region PublicMethods
    public void DisplayBonusPointsAddedOrLostFor4Seconds(int argBonus, bool IsAdded)
    {
        string StrToDisplay;
        if (IsAdded)
        {
            StrToDisplay = "WAVE BONUS \n " + argBonus.ToString();
            StartCoroutine(ShowFor4SecondsThenClear(StrToDisplay));
        }
        else
        {
            StrToDisplay = "YOU DIED \n  POINTS LOST \n " + argBonus.ToString();
            StartCoroutine(ShowFor4SecondsThenClear(StrToDisplay));
        }
    }

    IEnumerator ShowFor4SecondsThenClear(string argPoints)
    {
        OnboardPointsUI.text = argPoints;
        yield return new WaitForSeconds(6);
        OnboardPointsUI.text = "";
    }

    public void PlayGameOverAudio()
    {
        // audioManager.PlayEvent("_Boat");
    }
    public void PlaySuddenDeathAudio()
    {
        // audioManager.PlayEvent("_SuddenDeath");
    }
    public void WaveStarted(string waveNum)
    {
        //we're getting a roman numeral here watch out 
        if (waveNum == "I")
        {
            //  audioManager.PlayEvent("_Boat");
        }
        else
        {
            //  audioManager.PlayEvent("_Boom");
        }
    }


    public void DebugHP(float HP)
    {
        OnboardPointsUI.text = HP.ToString();
    }
    #endregion

    #region PrivateMethods

    #endregion
}
