// @Author Jeffrey M. Paquette ©2016
#define ENABLE_LOGS
//using HoloToolkit.Unity;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameCanvas : MonoBehaviour
{


    /// <summary>
    ///  Noah: I'm adding in references to the new 3D user interface. To find all the references search for 
    ///  my comments note's  "3D UI"
    /// </summary>

    // the ref to the 3D UI prefab
    private GameObject prefab3dUi; //No need for instantiating  it here, it is now  a prt of the fullRoom_GS and is acceessed via DevRoomManager
    private GameObject textRealityZombies;
    private GameObject textFinalScore;
    private GameObject textGameOver;
    private GameObject textRespawningIn;
    private GameObject textWave;
    private GameObject textWaveComplete;
    private GameObject textYouDied;
    // an easy check for if we have the menu in game
    private bool have3dUi = false;


    public GameObject damageCanvas;
    //*****************************************************
    //           has damagetracker.cs   KillPlayer()  and take hit()
    //                Scratch 
    //
    //                 /-------
    //                /       _____________
    //        -------/   ____/
    //    ---/          /
    //
    //*****************************************************



    public GameObject waveCompleteCanvas;


    //*****************************************************
    //                WAVE 1 Complete
    //
    //    WEAPON UPGRADE UNLOCKED
    // 
    //*****************************************************
    public void WaveCompleteWeaponUpgare()
    {
        // set focused group to wave complete group
        focusedGroup = waveCompleteGroup;
        // set focused canvas to wave complete canvas
        focusedCanvas = waveCompleteCanvas;

        StartCoroutine(FadeIn());
    }





    public GameObject waveStartCanvas;
    public Text waveNumText;
    //*****************************************************
    //                WAVE 
    //
    //    
    //             (waveNumText)
    //
    //
    //*****************************************************




    //*****************************************************
    //                GAMEOVER CANVAS is reffed in GaemManager and called from Damagetraker->gamemanager.turnon GameOVER cnvas 
    //
    //
    //*****************************************************
    public Text pointsLostText, finalScoreText;
    //*****************************************************
    //               you died
    //
    //    Total points Lost         :     350
    //  
    //    Final Score               :      10
    //
    //*****************************************************

    public float fadeSpeed;

    CanvasGroup waveStartGroup, waveCompleteGroup;
    // RzPlayerDamageEffectsComponent dmgTracker;
    GameObject focusedCanvas;
    CanvasGroup focusedGroup;
    //UAudioManager audioManager;
    bool firstWave = true;

    // Use this for initialization
    private void Start()
    {
        Debug.LogError("Gamecanvas is on " + gameObject.name);
        waveStartGroup = waveStartCanvas.GetComponent<CanvasGroup>();
        waveCompleteGroup = waveCompleteCanvas.GetComponent<CanvasGroup>();
        // dmgTracker = damageCanvas.GetComponent<RzPlayerDamageEffectsComponent>();
        //audioManager = gameObject.GetComponent<UAudioManager>();





    }// end of start function

    public void SetRefOfNewCanvas(GameObject argplacedSC3D)
    {

        prefab3dUi = argplacedSC3D;

        InitNew3dCanvas();
    }

    void InitNew3dCanvas()
    {
        if (prefab3dUi != null)
        {
            // grab the children refs
            textRealityZombies = prefab3dUi.transform.GetChild(0).gameObject;
            textFinalScore = prefab3dUi.transform.GetChild(1).gameObject;
            textGameOver = prefab3dUi.transform.GetChild(2).gameObject;
            textRespawningIn = prefab3dUi.transform.GetChild(3).gameObject;
            textWave = prefab3dUi.transform.GetChild(4).gameObject;
            textWaveComplete = prefab3dUi.transform.GetChild(5).gameObject;
            textYouDied = prefab3dUi.transform.GetChild(6).gameObject;
        }
        else
        {
            Debug.Log("Yo , No ref to mesh ui3d should have come from gamemanager start ");
        }

    }
    public void PlayGameOverAudio()
    {
        //audioManager.PlayEvent("_Boat");
    }



    public void WaveStarted(string waveNum)
    {
        // set wave number text
        waveNumText.text = waveNum;
        // set focused group to wave start group
        focusedGroup = waveStartGroup;
        // set focused canvas on wave start canvas
        focusedCanvas = waveStartCanvas;

        StartCoroutine(FadeIn());

        // play appropriate audio
        if (firstWave)
        {
            firstWave = false;
            //  PlayHeadShotSound.Instance.PlaySound("_Boat");
        }
        else
        {
            //   PlayHeadShotSound.Instance.PlaySound("_Boom");
        }

        // start of 3D UI in WaveStarted()
        // if we have the 3d menu 
        if (have3dUi == true)
        {
            // set the wave text           
            textWave.transform.GetChild(0).GetComponent<Text>().text = waveNum;
        }// end of have 3dui
        // end of 3D UI in WaveStarted()

    }// end of WaveStarted() funct

    IEnumerator FadeIn()
    {
        // turn alpha to 0 then show canvas
        focusedGroup.alpha = 0.0f;
        focusedCanvas.SetActive(true);

        // start of 3D UI in FadeIn()
        textWave.SetActive(true);
        // end of 3D UI in FadeIn()        


        while (focusedGroup.alpha < 1.0f)
        {
            focusedGroup.alpha += Time.deltaTime;
            yield return null;
        }



        TimerBehavior t = gameObject.AddComponent<TimerBehavior>();
        t.StartTimer(GameSettings.Instance.GET_FadeFinalInStartIn(), StartFadeOut);
    }

    IEnumerator FadeOut()
    {
        while (focusedGroup.alpha > 0.0f)
        {
            focusedGroup.alpha -= Time.deltaTime;
            yield return null;
        }

        // turn off canvas
        focusedCanvas.SetActive(false);

        // start of 3D UI in FadeOut()
        yield return new WaitForSeconds(1);
        textWave.SetActive(false);
        //  end of 3D UI in FadeIn()
    }

    public void StartFadeOut()
    {
        StopAllCoroutines();
        StartCoroutine(FadeOut());
    }

    //not used
    //public void TakeHit()
    //{
    //    //dmgTracker.TakeHit();
    //}

    //public void KillePlayer() {
    //    dmgTracker.KillPlayer();
    //}
    //public void ResetDamage()
    //{
    //    dmgTracker.ResetDamageTrakerProps();
    //}

    public void PointsLost(int value)
    {
        pointsLostText.text = "- " + value;
    }

    public void FinalScore(int value)
    {
        finalScoreText.text = value.ToString();
    }
}
