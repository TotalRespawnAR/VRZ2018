// @Author Nabil Lamriben ©2018
#define ENABLE_LOGS
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RoomCanvasCTRL : MonoBehaviour
{

    //*****************************************************
    //                WAVE 
    //             (waveNumText)
    //*****************************************************

    //*****************************************************
    //                GAMEOVER CANVAS     used to be reffed in GaemManager and called from Damagetraker->gamemanager.turnon GameOVER cnvas 
    //*****************************************************

    //*****************************************************
    //               you died
    //
    //    Total points Lost         :     350
    //  
    //    Final Score               :      10
    //*****************************************************


    #region dependencies
    #endregion

    #region PublicVars
    public GameObject PubInGameCanvasObjRef;
    public Text CanvasTextBox;
    #endregion

    #region PrivateVars
    CanvasGroup InGameCanvasGroup;
    GameObject TheCanvasInfoccussGameObj;
    CanvasGroup focusedGroup;
    #endregion

    #region INIT
    private void Start()
    {
        InGameCanvasGroup = PubInGameCanvasObjRef.GetComponent<CanvasGroup>();
    }
    #endregion

    #region PublicMethods
    public void SetCanvasText_AndFadeInOut(string argFormatedTExtToDisplay, bool IsFinal)
    {
        CanvasTextBox.color = Color.blue;
        if (IsFinal)
        {
            CanvasTextBox.color = Color.red;
        }
        CanvasTextBox.text = argFormatedTExtToDisplay;
        focusedGroup = InGameCanvasGroup;
        TheCanvasInfoccussGameObj = PubInGameCanvasObjRef;
        StartCoroutine(FadeIn(IsFinal));
    }
    #endregion

    #region PrivateMethods


    IEnumerator FadeIn(bool isFinalScore)
    {
        // turn alpha to 0 then show canvas
        focusedGroup.alpha = 0.0f;
        TheCanvasInfoccussGameObj.SetActive(true);


        while (focusedGroup.alpha < 1.0)
        {
            focusedGroup.alpha += Time.deltaTime;
            yield return null;
        }

        TimerBehavior t = gameObject.AddComponent<TimerBehavior>();
        if (isFinalScore)
        {
            t.StartTimer(10f, StartFadeOut);
        }
        else
        {
            t.StartTimer(GameSettings.Instance.GET_FadeFinalInStartIn(), StartFadeOut);
        }
    }

    IEnumerator FadeOut()
    {
        while (focusedGroup.alpha > 0.0f)
        {
            focusedGroup.alpha -= Time.deltaTime;
            yield return null;
        }

        // turn off canvas
        TheCanvasInfoccussGameObj.SetActive(false);
    }

    void StartFadeOut()
    {
        StopAllCoroutines();
        StartCoroutine(FadeOut());
    }
    private void OnDestroy()
    {

    }
    #endregion
}
