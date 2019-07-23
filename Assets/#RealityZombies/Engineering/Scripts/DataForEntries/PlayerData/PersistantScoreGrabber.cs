using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistantScoreGrabber : MonoBehaviour {
    public static PersistantScoreGrabber Instance = null;
    private void Awake()
    {

        if (Instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
          
            Instance = this;

        }
        else
            Destroy(gameObject);
    }

    Data_PlayerPoints CurPlayerPoints;

    //to do nabs
    //   UpdatePoints(points, headShotCount, streakCounter, maxHeadshotsinarow, killCount, missedshots, shotCount, deaths, pointslost,numberofReloads,numberofReloads);  
    //void UpdatePoints(int ArgScore , int ArgHeadshots , int ArgStreakcount , int ArgMaxstreak ,  int ArgKills , int ArgMiss , int ArgTotalshots , int ArgDeaths, int ArgPointslost, int ArgReloads, int ArgWavesPlayed) {
    //    CurPlayerPoints = new Data_PlayerPoints();

    //    CurPlayerPoints.score = ArgScore;
    //    CurPlayerPoints.headshots = ArgHeadshots;
    //    CurPlayerPoints.streakcount = ArgStreakcount;
    //    CurPlayerPoints.maxstreak = ArgMaxstreak;
    //    CurPlayerPoints.kills = ArgKills;
    //    CurPlayerPoints.miss = ArgMiss;
    //    CurPlayerPoints.totalshots = ArgTotalshots;
    //    CurPlayerPoints.deaths = ArgDeaths;
    //    CurPlayerPoints.pointslost = ArgPointslost;
    //    CurPlayerPoints.numberofReloads = ArgReloads;
    //    CurPlayerPoints.wavessurvived = ArgWavesPlayed;


    //    CurPlayerPoints.torssoshots = 0;
    //    CurPlayerPoints.limbshots = 0;
    //    CurPlayerPoints.headshotkills = 0;
    //    CurPlayerPoints.torssoshotkills = 0;
    //    CurPlayerPoints.limbshotkills = 0;
    //    Logger.Debug("the scores are " + CurPlayerPoints.ToString());
    //}

    //new overload
    void UpdatePoints(int ArgScore, int ArgHeadshots, int argTorssoShots, int argLimbshots, int argHeadShotKills, int argTorssoshotKills, int argLimbShotKills, int ArgStreakcount, int ArgMaxstreak, int ArgKills, int ArgMiss, int ArgTotalshots, int ArgDeaths, int ArgPointslost, int ArgReloads, int ArgWavesPlayed)
    {
        CurPlayerPoints = new Data_PlayerPoints(ArgScore, ArgHeadshots,argTorssoShots, argLimbshots, argHeadShotKills, argTorssoshotKills, argLimbShotKills, ArgStreakcount, ArgMaxstreak, ArgKills,ArgMiss, ArgTotalshots, ArgDeaths, ArgPointslost, ArgReloads, ArgWavesPlayed);

        //CurPlayerPoints.score = ArgScore;
        //CurPlayerPoints.headshots = ArgHeadshots;
        //CurPlayerPoints.streakcount = ArgStreakcount;
        //CurPlayerPoints.maxstreak = ArgMaxstreak;
        //CurPlayerPoints.kills = ArgKills;
        //CurPlayerPoints.miss = ArgMiss;
        //CurPlayerPoints.totalshots = ArgTotalshots;
        //CurPlayerPoints.deaths = ArgDeaths;
        //CurPlayerPoints.pointslost = ArgPointslost;
        //CurPlayerPoints.numberofReloads = ArgReloads;
        //CurPlayerPoints.wavessurvived = ArgWavesPlayed;

        //CurPlayerPoints.torssoshots = argTorssoShots;
        //CurPlayerPoints.limbshots = argLimbshots;
        //CurPlayerPoints.headshotkills = argHeadShotKills;
        //CurPlayerPoints.torssoshotkills = argTorssoshotKills;
        //CurPlayerPoints.limbshotkills = argLimbShotKills;

       // Logger.Debug("the scores are " + CurPlayerPoints.ToString());
    }


    public void DoGrabScores() {
 
       

        int points = GameManager.Instance.GetScoreMAnager().Get_PointsTotal();
        int streakCounter = GameManager.Instance.GetStreakManager().Get_NumberOfStreaks();//STRAK sssssssssssssssssssssssssssssssssss
        int shotCount = GameManager.Instance.GetScoreMAnager().Get_BulletsFiredCNT();
        int headShotCount = GameManager.Instance.GetScoreMAnager().Get_headShotCNT();
        int torsoShotCount = GameManager.Instance.GetScoreMAnager().Get_torsoShotCNT();
        int limbShotCount = GameManager.Instance.GetScoreMAnager().Get_limbShotCNT();
        int killCount = GameManager.Instance.GetScoreMAnager().Get_ZombiesKilledCNT();
        int maxHeadshotsinarow = GameManager.Instance.GetStreakManager().Get_MaxHEadshotsInARow(); //is number of streak ssssssssssssssssssssssssssssssssssss  

        int missedshots = GameManager.Instance.GetScoreMAnager().Get_Bullets_Missed_ZombieCNT();
        int deaths = GameManager.Instance.GetScoreMAnager().Get_DeathsCNT();
        int pointslost = GameManager.Instance.GetScoreMAnager().Get_PointsTotalLost();
        int numberofReloads = GameManager.Instance.GetScoreMAnager().Get_ReloadsCNT();
        int wavessurvived = GameManager.Instance.GetScoreMAnager().Get_WavesPlayedCNT();

         
        //to do nab plug these in the iverloaded UpdatePoints
        int torssoshots = GameManager.Instance.GetScoreMAnager().Get_torsoShotCNT();
        int limbshots = GameManager.Instance.GetScoreMAnager().Get_limbShotCNT();

        int headshotkills = GameManager.Instance.GetScoreMAnager().Get_headShotKillsCNT();
        int torssoshotkills = GameManager.Instance.GetScoreMAnager().Get_torssoShotKillsCNT();
        int limbshotkills = GameManager.Instance.GetScoreMAnager().Get_limbShotKillsCNT();

        //  UpdatePoints(points, headShotCount, streakCounter, maxHeadshotsinarow, killCount, missedshots, shotCount, deaths, pointslost,numberofReloads, wavessurvived); 
        UpdatePoints(points, headShotCount, torsoShotCount, limbShotCount, headshotkills, torssoshotkills, limbshotkills, streakCounter, maxHeadshotsinarow, killCount, missedshots, shotCount, deaths, pointslost, numberofReloads, wavessurvived);


    }

 
 


    public Data_PlayerPoints Get_Data_Player() { return this.CurPlayerPoints; }

    public void Clear_DataPoints() {
        CurPlayerPoints = null;
    }
}
