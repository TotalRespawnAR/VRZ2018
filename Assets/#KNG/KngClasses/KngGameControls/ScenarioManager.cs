using System.Collections;
using UnityEngine;

public class ScenarioManager : MonoBehaviour
{

    public GameObject TutoCtrlObj;
    StemTutoController _stemTutoCtrl;

    ARZTutorialStates _curTutoState;
    private void Start()
    {
        _curTutoState = ARZTutorialStates.StartTutorial;
        _stemTutoCtrl = TutoCtrlObj.GetComponent<StemTutoController>();
        GameManager.Instance.Position_StemTuto();
        _stemTutoCtrl.HideAll();
    }
    void OnEnable()
    {
        GameEventsManager.OnShooterFired += IHeardAshot;
        GameEventsManager.OnNoLiveZombiesLeft += NoMoreZombies_original3;
        GameEventsManager.OnTutoReload += IHeardManualReload;
        GameEventsManager.OnTutoWeaponChange += WeaponSwitched;
        GameEventsManager.OnTutoPlayerAoidedAxe += IAvoidedTheAxe;
        GameEventsManager.OnSkipTuto += SkipTuorial;
    }
    private void OnDisable()
    {
        GameEventsManager.OnShooterFired -= IHeardAshot;
        GameEventsManager.OnNoLiveZombiesLeft -= NoMoreZombies_original3;
        GameEventsManager.OnTutoReload -= IHeardManualReload;
        GameEventsManager.OnTutoWeaponChange -= WeaponSwitched;
        GameEventsManager.OnTutoPlayerAoidedAxe -= IAvoidedTheAxe;
        GameEventsManager.OnSkipTuto -= SkipTuorial;

    }

    bool IsSkipped = false;
    int ShotFired = 0;
    int MaxShotsFired_Action1 = 1;
    void IHeardAshot()
    {
        ShotFired++;
        if (ShotFired > MaxShotsFired_Action1) return;
        else
        if (ShotFired == MaxShotsFired_Action1) Action1__hideTuto_wait_showMAintextAUtoreload();
    }



    public void RunScenarioTutorial()
    {
        //Data_Enemy EP1 = new Data_Enemy(0,1, KngEnemyName.BASIC, 40, ARZombieypes.STANDARD, EBSTATE.IDLE, EBSTATE.IDLE, new Vector3(0, 180, 0), GameManager.Instance.Get_SceneObjectsManager().KnodesMNGR.GetNode(50));
        //GameManager.Instance.Spawn_Enemy(EP1);
        //if (GameManager.Instance.KngGameState == ARZState.Pregame)
        //{
        //    StartCoroutine(WaitStartTuto(2));
        //}
    }

    IEnumerator WaitStartTuto(int argWaitStartTuto)
    {
        yield return new WaitForSeconds(argWaitStartTuto);
        _curTutoState = ARZTutorialStates.LearnShoot;
        GameEventsManager.CALL_ToggleStemInput(true);
        _stemTutoCtrl.ShowObjTrigger();
        PlayHeadShotSound.Instance.PlaySound("_tutoShoot");
    }

    // Bang once
    void Action1__hideTuto_wait_showMAintextAUtoreload()
    {
        if (IsSkipped)
        {
            _stemTutoCtrl.HideAll();
            _stemTutoCtrl.UpdateMainTExt("");
            return;
        }

        _stemTutoCtrl.HideAll();
        StartCoroutine(WaitHideTutoTrigger(2));
    }


    IEnumerator WaitHideTutoTrigger(int argSecToHideTutuoTriger)
    {
        yield return new WaitForSeconds(argSecToHideTutuoTriger);
        if (GameSettings.Instance.IsSkipPregameOn)
        {
            _stemTutoCtrl.HideAll();
            _stemTutoCtrl.UpdateMainTExt("");

        }
        else
        {
            _stemTutoCtrl.UpdateMainTExt("AUTO \n RELOADING");
            PlayHeadShotSound.Instance.PlaySound("_tutoAutoReload");
            StartCoroutine(WaitShowTutoReload(3));
        }
    }

    IEnumerator WaitShowTutoReload(int argSecToHideTutuoTriger)
    {
        yield return new WaitForSeconds(argSecToHideTutuoTriger);
        if (IsSkipped)
        {
            _stemTutoCtrl.HideAll();
            _stemTutoCtrl.UpdateMainTExt("");

        }
        else
        {
            _curTutoState = ARZTutorialStates.LearnReload;
            _stemTutoCtrl.UpdateMainTExt("");
            _stemTutoCtrl.ShowObjReload();
            PlayHeadShotSound.Instance.PlaySound("_tutoManualReload");
            GameEventsManager.Instance.Call_PlayerCanReload(true);
        }
    }


    // ReloadOnce
    void IHeardManualReload()
    {
        if (IsSkipped)
        {
            _stemTutoCtrl.HideAll();
            _stemTutoCtrl.UpdateMainTExt("");
            return;
        }
        else
        if (_curTutoState == ARZTutorialStates.LearnReload)
        {

            _stemTutoCtrl.HideAll();
            StartCoroutine(WaitShowSwitchWeapon(2));
        }
    }
    IEnumerator WaitShowSwitchWeapon(int argSecToHideTutuoTriger)
    {
        yield return new WaitForSeconds(argSecToHideTutuoTriger);
        if (IsSkipped)
        {
            _stemTutoCtrl.HideAll();
            _stemTutoCtrl.UpdateMainTExt("");

        }
        else
        {

            _stemTutoCtrl.ShowObjJoy();
            PlayHeadShotSound.Instance.PlaySound("_tutoSwitchweapon");
            GameEventsManager.Instance.Call_PlayerCanSwitchWeapons(true);
            _curTutoState = ARZTutorialStates.LearnChangeWeapon;
        }
    }



    //Switched once
    void WeaponSwitched()
    {
        if (IsSkipped)
        {
            _stemTutoCtrl.HideAll();
            _stemTutoCtrl.UpdateMainTExt("");
            return;
        }
        else
        if (_curTutoState == ARZTutorialStates.LearnChangeWeapon)
        {
            _stemTutoCtrl.HideAll();
            StartCoroutine(WaitShowAxeZombie(5));
        }
    }

    bool AxeZombieCreated = false;
    IEnumerator WaitShowAxeZombie(int argSecToHideTutuoTriger)
    {
        yield return new WaitForSeconds(argSecToHideTutuoTriger);

        if (IsSkipped)
        {
            _stemTutoCtrl.HideAll();
            _stemTutoCtrl.UpdateMainTExt("");
        }
        else
        if (_curTutoState == ARZTutorialStates.LearnChangeWeapon)
        {
            // StemKitMNGR.CALL_ToggleStemInput(false);
            GameEventsManager.Instance.Call_PlayerCanShoot(false);
            GameEventsManager.Instance.Call_PlayerCanReload(false);
            _curTutoState = ARZTutorialStates.LearnDoge;
            _stemTutoCtrl.UpdateMainTExt("YOU NEED TO \n DODGE THE AXE");
            PlayHeadShotSound.Instance.PlaySound("_tutoDodge");

            if (AxeZombieCreated == false)
            {
                // GameManager.Instance.Req_axeguyOnANY(2, 2);
                Debug.Log("BROKEN \n probably the first... axezombie create bool but nothig to flip it ");
                AxeZombieCreated = true;
            }


            //EnemyProps EP2 = new EnemyProps(0, KngEnemyName.CHECKER, 40, ARZombieypes.STANDARD, ZombieState.IDLE, new Vector3(0, 0, 0), GameManager.Instance.Get_SceneObjectsManager().KnodesMNGR.GetNode(55));
            //GameManager.Instance.Spawn_Enemy(EP2);
        }

    }

    int axesAvoided = 10;
    //avoided once
    void IAvoidedTheAxe()
    {
        if (GameManager.Instance.KngGameState == ARZState.Pregame)
        {
            if (IsSkipped)
            {
                _stemTutoCtrl.HideAll();
                _stemTutoCtrl.UpdateMainTExt("");
                GameEventsManager.Instance.Call_PlayerCanShoot(true);
                GameEventsManager.Instance.Call_PlayerCanReload(true);
                return;
            }
            else

            if (_curTutoState == ARZTutorialStates.LearnDoge)
            {
                axesAvoided++;
                if (axesAvoided < 1)
                {
                    _stemTutoCtrl.HideAll();
                    _stemTutoCtrl.UpdateMainTExt("GOOD JOB  ");
                    //  PlayHeadShotSound.Instance.PlaySound("_tutoGoodJob");
                    StartCoroutine(WaiteHIdeMAinTExt(2));
                }
                else
                {
                    _stemTutoCtrl.UpdateMainTExt("KILL ZOMBIES IN \n SLOW TIME");

                    PlayHeadShotSound.Instance.PlaySound("_tutoKill");
                    GameEventsManager.CALL_ToggleStemInput(true);
                    GameEventsManager.Instance.Call_PlayerCanShoot(true);
                    GameEventsManager.Instance.Call_PlayerCanReload(true);

                    StartCoroutine(WaiteHIdeMAinTExt(2));

                }
            }
        }
    }

    IEnumerator WaiteHIdeMAinTExt(int argSecToHideTutuoTriger)
    {
        if (IsSkipped)
        {
            _stemTutoCtrl.HideAll();
            _stemTutoCtrl.UpdateMainTExt("");

        }
        yield return new WaitForSeconds(argSecToHideTutuoTriger);
        _stemTutoCtrl.UpdateMainTExt("");
    }



    IEnumerator WaiteHIdeMGET_READY(int argSecToHideTutuoTriger)
    {
        if (IsSkipped)
        {
            _stemTutoCtrl.HideAll();
            _stemTutoCtrl.UpdateMainTExt(" SHOOT THE \n START BUTTON ");
            GameEventsManager.Instance.CallTutoPlayerKilledZombie(); //start button will set itself active
            GameManager.Instance.Position_StartButton();
        }
        else
        {
            _stemTutoCtrl.UpdateMainTExt(" SHOOT THE \n START BUTTON ");
            yield return new WaitForSeconds(argSecToHideTutuoTriger);

            if (IsSkipped)
            {
                _stemTutoCtrl.HideAll();
                _stemTutoCtrl.UpdateMainTExt("");
            }
            else
            {
                GameEventsManager.Instance.CallTutoPlayerKilledZombie(); //start button will set itself active
                GameManager.Instance.Position_StartButton();
                _stemTutoCtrl.UpdateMainTExt("");
                _stemTutoCtrl.HideAll();
            }
        }
    }

    // axe giy dies
    void NoMoreZombies_original3()
    {
        if (GameManager.Instance.KngGameState == ARZState.Pregame)
        {
            _stemTutoCtrl.HideAll();
            StartCoroutine(WaiteHIdeMGET_READY(3));
        }
    }

    void SkipTuorial()
    {

        if (_curTutoState == ARZTutorialStates.LearnShoot || _curTutoState == ARZTutorialStates.LearnReload || _curTutoState == ARZTutorialStates.LearnChangeWeapon)
        {
            IsSkipped = true;
            GameEventsManager.Instance.Call_PlayerCanShoot(true);
            GameEventsManager.Instance.Call_PlayerCanReload(true);
            GameEventsManager.Instance.Call_PlayerCanSwitchWeapons(true);
            NoMoreZombies_original3();
        }
    }












    int step = 0;
    void PromptNextStep()
    {
        step++;

        if (isScenario2_IdlePatroles_WallGraver_Dropper)
        {
            do_scenario2_Steps();
        }

        if (isScenario_3_bossRunWall)
        {
            do_scenario3_Steps();
        }

        if (isScenario_4_Wlled_eaterLayer_TimedGrave_Pumpkin)
        {
            do_scenario4_Steps();
        }



    }


    void do_scenario2_Steps()
    {
        if (step == 1)
        {
            //  GameManagerOld.Instance.Place_The_BrickWall_inWolrd();
            //  GameManagerOld.Instance.Place_TheGraver();
        }
        else
        if (step == 2)
        {
            Drop_Paraite();
        }
        else
        if (step == 3)
        {
            GameManager.Instance.CheckWav1Started(); //ScenarioMNGR scenario2 
        }
    }
    void do_scenario3_Steps()
    {
        if (step == 1)
        {
            GameManager.Instance.CheckWav1Started();//scenario 3
        }

    }
    void do_scenario4_Steps()
    {

        //if (step == 1) {
        //    shotsHEard = 0;
        //   // GameManager.Instance.Place_theBigMutant();
        //   // GameManager.Instance.Place_SOME_IDELING_Zombies(6);
        //}
        //else
        //    if (step==2) {
        //    GameManagerOld.Instance.CheckWav1Started(); //scenario4
        //}

    }




    void Drop_Paraite()
    {
        //  GameManagerOld.Instance.Place_TheDroppingPumpkin();
    }





    public void RunScenarioIdlesPatrolers()
    {
        //isScenario2_IdlePatroles_WallGraver_Dropper = true;

        //GameManagerOld.Instance.Place_SOME_IDELING_Zombies(3);
        //GameManagerOld.Instance.REQ_Z_allPatroles();

    }
    public void RunScenario3()
    {

        //isScenario_3_bossRunWall = true;
        //GameManagerOld.Instance.Place_The_BrickWall_inWolrd();
        //GameManagerOld.Instance.Place_theBigMutant().GetComponent<ZombieBehavior>();

    }
    public void RunScenario4()
    {

        // isScenario_4_Wlled_eaterLayer_TimedGrave_Pumpkin = true;
        // GameManagerOld.Instance.Place_TheEATER();//.GetComponent<ZombieBehavior>();
        // GameManagerOld.Instance.Place_TheLayer();//.GetComponent<ZombieBehavior>();
        // GameManagerOld.Instance.Place_TheWatcher();//.GetComponent<ZombieBehavior>();
        //// GameManager.Instance.Place_TheGraver();
        //// GameManager.Instance.Place_TheHider();
        // GameManagerOld.Instance.Place_The_BrickWall_inWolrd();

    }


    #region oldScenarionEaterLayerGraver
    bool isScenario0_autostart = false;

    bool isScenario1_eaterLayer_graver = false;
    bool isScenario2_IdlePatroles_WallGraver_Dropper = false;
    bool isScenario_3_bossRunWall = false;
    bool isScenario_4_Wlled_eaterLayer_TimedGrave_Pumpkin = false;

    public void RunScenario0_autostart()
    {
        isScenario0_autostart = true;
        _stemTutoCtrl.HideAll();
        GameManager.Instance.CheckWav1Started(); //senario 0 quick start


    }

    #endregion
}
