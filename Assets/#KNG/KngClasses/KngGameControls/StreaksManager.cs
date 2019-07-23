// @Author Nabil Lamriben ©2018s
//#define ENABLE_LOGS
using UnityEngine;


public class StreaksManager : MonoBehaviour
{
    #region dependencies
    public GameObject StreakObject;
    public Numbers3DConstructor Numbers3dBuilder;
    #endregion

    #region PrivateVariables
    int ThisBulletHit_Points = 0;
    bool lastShot_wasHEadHit;
    int baseFontSize = 12;

    int curStreakLength;
    int MaxRecordedStreakLength;

    int numberofStreaks;
    int BonusToAwardForThisStreakHit;

    int CappedStreakLength = 5;
    int CappedStreakBonus = 200;
    #endregion

    #region Noahs Vars
    // prefab for 3d text
    public GameObject ui3dPointsPrefab;
    // temp ref to 3d text
    private GameObject spawned3dPoints;
    #endregion

    #region INIT
    void Start()
    {
        lastShot_wasHEadHit = false;
        curStreakLength = 0;
        MaxRecordedStreakLength = 0;
        numberofStreaks = 0;
        BonusToAwardForThisStreakHit = 0;
    }


    #endregion

    #region PublicMethods

    int NumberOfTimesMissed = 0;

    int littleCounterofHeadshotsinarow = 0;
    public void Set_StreakBreake()
    {

        if (curStreakLength > 0)
        {
            numberofStreaks++;
        }

        curStreakLength = 0;
        lastShot_wasHEadHit = false;
        BonusToAwardForThisStreakHit = 0;
        littleCounterofHeadshotsinarow = 0;
        //ScoreDebugCon.Instance.Update_wasHead(lastShot_wasHEadHit);
        //ScoreDebugCon.Instance.Update_CNTStrek(numberofStreaks);

        NumberOfTimesMissed++;
        if (NumberOfTimesMissed % 3 == 0)
        {
            GameEventsManager.Instance.Call_ShooterMissed();
        }
    }



    //only call this when there is a head shot 
    public void CreateCappedStreakObject(Vector3 arghere)
    {
        GameObject so = Instantiate(StreakObject, arghere, Quaternion.identity);
        StreakText st = so.GetComponent<StreakText>();

        CalcPointsForStreak(arghere, st);

        st.SetTextbox("+ " + ThisBulletHit_Points);
        KillTimer t = so.AddComponent<KillTimer>();
        t.StartTimer(2);
        //if we are here , we got a headshot and are trying to see if last shot was a hs. 
        //therefore we must set lastShot_wasHEadHit = true; ... becasue this cur shot will be the 'lastshot' next time we check 
        lastShot_wasHEadHit = true;
        littleCounterofHeadshotsinarow++;
        //if (littleCounterofHeadshotsinarow%4==0) {
        //    GameManager.Instance.TryCallSlowTime();
        //}
        GameManager.Instance.GetScoreMAnager().Update_Add_PointsTotal(ThisBulletHit_Points);
        GameManager.Instance.GetScoreMAnager().Update_Add_PointsCurWave(ThisBulletHit_Points);
        GameManager.Instance.GetScoreMAnager().Update_BonusPoints(BonusToAwardForThisStreakHit);


        //ScoreDebugCon.Instance.Update_wasHead(lastShot_wasHEadHit);
        //ScoreDebugCon.Instance.Update_curStrek(curStreakLength);
        //ScoreDebugCon.Instance.Update_MaxStrek(MaxRecordedStreakLength);
        ThisBulletHit_Points = 0;
    }

    void CalcPointsForStreak(Vector3 here, StreakText argST)
    {
        GameEventsManager.Instance.Call_ShooterHitted();

        curStreakLength++;
        //update max streak reached by player
        if (curStreakLength > MaxRecordedStreakLength)
        {
            MaxRecordedStreakLength = curStreakLength;
        }

        if (curStreakLength >= CappedStreakLength)
        {
            ThisBulletHit_Points = CappedStreakBonus;
            argST.SetColor(Color.green);
            //argST.SetFontSize(baseFontSize+ ((1+CappedStreakLength) *2));
            argST.SetFontSize(baseFontSize + (CappedStreakLength + 2));

        }
        else
        {
            BonusToAwardForThisStreakHit += 25;
            ThisBulletHit_Points = 75;// ((GameSettings.Instance.ReloadDifficulty == ARZReloadLevel.MEDIUM  ||GameSettings.Instance.ReloadDifficulty == ARZReloadLevel.EASY || GameSettings.Instance.ReloadDifficulty == ARZReloadLevel.NOOB) ? 75 : 100);
            ThisBulletHit_Points += BonusToAwardForThisStreakHit;
            argST.SetColor(Color.yellow);

            //argST.SetFontSize(baseFontSize + ((1+curStreakLength) *2));
            argST.SetFontSize(baseFontSize + (curStreakLength + 2));

        }

        // _____________________noah's check / spawn for which 3D text
        // send a message
        Logger.Debug("Noah's Code for StreaksMnger on: " + transform.name);

        // if our prefab is null
        if (ui3dPointsPrefab == null)
        {
            // send a message
            Logger.Debug("No Prefab for 3D Points UI");
            // stop
            return;
        }// end of no prefab for 3d points
        else
        // if there is a prefab for the 3D points
        {
            // send a message
            Logger.Debug("Prefab 3D Points UI Found");
            // spawn one with a ref
            spawned3dPoints = Instantiate(ui3dPointsPrefab, here, ui3dPointsPrefab.transform.rotation);


            if (ThisBulletHit_Points < 2)
            {
                spawned3dPoints.transform.GetChild(0).gameObject.SetActive(false); spawned3dPoints.transform.GetChild(1).gameObject.SetActive(false); spawned3dPoints.transform.GetChild(2).gameObject.SetActive(false); spawned3dPoints.transform.GetChild(3).gameObject.SetActive(false); spawned3dPoints.transform.GetChild(4).gameObject.SetActive(false); spawned3dPoints.transform.GetChild(5).gameObject.SetActive(true); spawned3dPoints.transform.GetChild(6).gameObject.SetActive(false); spawned3dPoints.transform.GetChild(7).gameObject.SetActive(false);
            }
            else
            if (ThisBulletHit_Points >= 2 && ThisBulletHit_Points < 5)
            {
                spawned3dPoints.transform.GetChild(0).gameObject.SetActive(false); spawned3dPoints.transform.GetChild(1).gameObject.SetActive(false); spawned3dPoints.transform.GetChild(2).gameObject.SetActive(false); spawned3dPoints.transform.GetChild(3).gameObject.SetActive(false); spawned3dPoints.transform.GetChild(4).gameObject.SetActive(false); spawned3dPoints.transform.GetChild(5).gameObject.SetActive(false); spawned3dPoints.transform.GetChild(6).gameObject.SetActive(true); spawned3dPoints.transform.GetChild(7).gameObject.SetActive(false);
            }
            else
              if (ThisBulletHit_Points >= 5 && ThisBulletHit_Points < 50)
            {
                spawned3dPoints.transform.GetChild(0).gameObject.SetActive(false); spawned3dPoints.transform.GetChild(1).gameObject.SetActive(false); spawned3dPoints.transform.GetChild(2).gameObject.SetActive(false); spawned3dPoints.transform.GetChild(3).gameObject.SetActive(false); spawned3dPoints.transform.GetChild(4).gameObject.SetActive(false); spawned3dPoints.transform.GetChild(5).gameObject.SetActive(false); spawned3dPoints.transform.GetChild(6).gameObject.SetActive(false); spawned3dPoints.transform.GetChild(7).gameObject.SetActive(true);
            }
            if (ThisBulletHit_Points >= 50 && ThisBulletHit_Points < 120)
            {
                spawned3dPoints.transform.GetChild(0).gameObject.SetActive(true); spawned3dPoints.transform.GetChild(1).gameObject.SetActive(false); spawned3dPoints.transform.GetChild(2).gameObject.SetActive(false); spawned3dPoints.transform.GetChild(3).gameObject.SetActive(false); spawned3dPoints.transform.GetChild(4).gameObject.SetActive(false); spawned3dPoints.transform.GetChild(5).gameObject.SetActive(false); spawned3dPoints.transform.GetChild(6).gameObject.SetActive(false); spawned3dPoints.transform.GetChild(7).gameObject.SetActive(false);
            }
            else if (ThisBulletHit_Points > 120 && ThisBulletHit_Points < 130)
            {
                spawned3dPoints.transform.GetChild(0).gameObject.SetActive(false); spawned3dPoints.transform.GetChild(1).gameObject.SetActive(true); spawned3dPoints.transform.GetChild(2).gameObject.SetActive(false); spawned3dPoints.transform.GetChild(3).gameObject.SetActive(false); spawned3dPoints.transform.GetChild(4).gameObject.SetActive(false); spawned3dPoints.transform.GetChild(5).gameObject.SetActive(false); spawned3dPoints.transform.GetChild(6).gameObject.SetActive(false); spawned3dPoints.transform.GetChild(7).gameObject.SetActive(false);
            }
            else if (ThisBulletHit_Points > 130 && ThisBulletHit_Points < 160)
            {
                spawned3dPoints.transform.GetChild(0).gameObject.SetActive(false); spawned3dPoints.transform.GetChild(1).gameObject.SetActive(false); spawned3dPoints.transform.GetChild(2).gameObject.SetActive(true); spawned3dPoints.transform.GetChild(3).gameObject.SetActive(false); spawned3dPoints.transform.GetChild(4).gameObject.SetActive(false); spawned3dPoints.transform.GetChild(5).gameObject.SetActive(false); spawned3dPoints.transform.GetChild(6).gameObject.SetActive(false); spawned3dPoints.transform.GetChild(7).gameObject.SetActive(false);
            }
            else if (ThisBulletHit_Points > 160 && ThisBulletHit_Points < 180)
            {
                spawned3dPoints.transform.GetChild(0).gameObject.SetActive(false); spawned3dPoints.transform.GetChild(1).gameObject.SetActive(false); spawned3dPoints.transform.GetChild(2).gameObject.SetActive(false); spawned3dPoints.transform.GetChild(3).gameObject.SetActive(true); spawned3dPoints.transform.GetChild(4).gameObject.SetActive(false); spawned3dPoints.transform.GetChild(5).gameObject.SetActive(false); spawned3dPoints.transform.GetChild(6).gameObject.SetActive(false); spawned3dPoints.transform.GetChild(7).gameObject.SetActive(false);
            }
            else if (ThisBulletHit_Points > 180)
            {
                PlayHeadShotSound.Instance.PlaySound_2d_200Cheer();
                spawned3dPoints.transform.GetChild(0).gameObject.SetActive(false); spawned3dPoints.transform.GetChild(1).gameObject.SetActive(false); spawned3dPoints.transform.GetChild(2).gameObject.SetActive(false); spawned3dPoints.transform.GetChild(3).gameObject.SetActive(false); spawned3dPoints.transform.GetChild(4).gameObject.SetActive(true); spawned3dPoints.transform.GetChild(5).gameObject.SetActive(false); spawned3dPoints.transform.GetChild(6).gameObject.SetActive(false); spawned3dPoints.transform.GetChild(7).gameObject.SetActive(false);
            }
            else
            {
            }


            // for each points menu in the prefab
            //foreach (Transform child in spawned3dPoints.transform)
            //{

            //    // check for similarities between converted score to string from the points we got 
            //    if (child.name.Contains((BonusToAwardForThisStreakHit + 75).ToString()))
            //    {                   
            //        // send a message
            //        Logger.Debug("UI Spawning: " + (BonusToAwardForThisStreakHit + 75));                    
            //    }// end of child name does contain points
            //    else
            //    // if it's not the right points
            //    {
            //        // turn that obj off
            //        child.gameObject.SetActive(false);
            //    }// end of not the right points

            //}// end of for each child in 3d points

        }// end of there is a prefabd for 3d points

        // _____________________end of noahs check / spawn
    }

    //unused func
    //void CalcPointsFor_NO_Streak(Vector3 here, StreakText argST)
    //{
    //    ThisBulletHit_Points = ((GameSettings.Instance.ReloadDifficulty == ARZReloadLevel.EASY || GameSettings.Instance.ReloadDifficulty == ARZReloadLevel.NOOB) ? 75 : 100); // BonusToAwardForThisStreakHit;// + ((GameSettings.Instance.ReloadDifficulty == ARZReloadLevel.EASY) ? 100 : 125);
    //    argST.SetColor(Color.blue);
    //}



    public void CreateCappedStreakObject_3D(Vector3 arghere)
    {
        // GameObject so = Instantiate(StreakObject, arghere, Quaternion.identity);

        CalcPointsForStreak_3D();


        //if we are here , we got a headshot and are trying to see if last shot was a hs. 
        //therefore we must set lastShot_wasHEadHit = true; ... becasue this cur shot will be the 'lastshot' next time we check 
        lastShot_wasHEadHit = true;
        GameManager.Instance.GetScoreMAnager().Update_Add_PointsTotal(ThisBulletHit_Points);
        GameManager.Instance.GetScoreMAnager().Update_Add_PointsCurWave(ThisBulletHit_Points);
        GameManager.Instance.GetScoreMAnager().Update_BonusPoints(BonusToAwardForThisStreakHit);


        //ScoreDebugCon.Instance.Update_wasHead(lastShot_wasHEadHit);
        //ScoreDebugCon.Instance.Update_curStrek(curStreakLength);
        //ScoreDebugCon.Instance.Update_MaxStrek(MaxRecordedStreakLength);

        GameObject Obj3dNum = Numbers3dBuilder.Get_Build_NEW_3Dnumber(ThisBulletHit_Points);
        Obj3dNum.transform.parent = null;
        Obj3dNum.transform.position = arghere;
        Obj3dNum.transform.LookAt(Vector3.zero);
        ThisBulletHit_Points = 0;
    }

    void CalcPointsForStreak_3D()
    {
        curStreakLength++;
        //update max streak reached by player
        if (curStreakLength > MaxRecordedStreakLength)
        {
            MaxRecordedStreakLength = curStreakLength;
        }

        if (curStreakLength >= CappedStreakLength)
        {
            ThisBulletHit_Points = CappedStreakBonus;
        }
        else
        {
            BonusToAwardForThisStreakHit += 25;
            ThisBulletHit_Points = 75;// ((GameSettings.Instance.ReloadDifficulty == ARZReloadLevel.EASY || GameSettings.Instance.ReloadDifficulty == ARZReloadLevel.NOOB || GameSettings.Instance.ReloadDifficulty == ARZReloadLevel.MEDIUM) ? 75 : 100);
            ThisBulletHit_Points += BonusToAwardForThisStreakHit;

        }

    }
    public int Get_MaxHEadshotsInARow() { return MaxRecordedStreakLength; }

    public int Get_NumberOfStreaks() { return numberofStreaks; }
    #endregion
}


