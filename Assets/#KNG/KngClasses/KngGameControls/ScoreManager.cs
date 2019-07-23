//#define ENABLE_LOGS
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    ShotScoreController _scoreCTRL;
    // Use this for initialization

    // a true or false for if doublePoints is on represented by a 1 & 2
    public static int doublePointsIsOn = 1;

    void Start()
    {
        _scoreCTRL = GetComponent<ShotScoreController>();
    }



    int CNT_reloads = 0;
    //PersistantScoreGrabber.DoGrabScores()->
    public int Get_ReloadsCNT() { return CNT_reloads; }
    //Gun.GunHandle_CellFiled() when last cell is filled -> we call this via gamemanager._scoremanager
    public void Increment_ReloadsCNT() { CNT_reloads++; }


    int CNT_Deaths = 0;
    //PersistantScoreGrabber.DoGrabScores()->
    public int Get_DeathsCNT() { return CNT_Deaths; }
    //gamemnager.playerdied()->
    public void Increment_DeathsCNT() { CNT_Deaths++; }


    int CNT_WavesPlayed = 1;
    //PersistantScoreGrabber.DoGrabScores()->
    public int Get_WavesPlayedCNT() { return CNT_WavesPlayed; }
    //gamemanager.GM_Handle_WaveCompleteByPoppingNUMplusplus()->
    public void Increment_WavesPlayedCNT() { CNT_WavesPlayed++; }
    public void AddBonusPointForFinishingWaveNumber(int argwaveNumber)
    {
        //note that the wave numbers here are 0 based 
        int WaveBonus = 0;


        switch (argwaveNumber)
        {  // flatten the triangle
            case 0:
                WaveBonus = 2500;
                break;
            case 1:
                WaveBonus = 5000;
                break;
            case 2:
                WaveBonus = 10000;
                break;
            case 3:
                WaveBonus = 20000;
                break;

            default:
                WaveBonus = 0;
                break;
        }
        if (GameManager.Instance.PlayerHasDiedInThisWave)
        {
            WaveBonus = 0;
        }

        GameEventsManager.Instance.Call_OnOnBoardDisplay(WaveBonus, true);
        Update_Add_PointsTotal(WaveBonus);

        Debug.Log("ended wave" + argwaveNumber + " adding " + WaveBonus);
    }

    int CNT_BulletsFired = 0;
    int CNT_Bullet_Hit_Zombie = 0;
    int CNT_Bullets_Missed_Zombie = 0;
    //not used anymore untill we get pickups 
    //if we fired at a pickup or ammo box to get amo. this shot should not count 
    //public void SKORE_Decrement_ShotsFiredCounter() { if (CNT_BulletsFired > 0) CNT_BulletsFired--; }
    public int Get_BulletsFiredCNT() { return CNT_BulletsFired; }
    public int Get_Bullet_Hit_ZombieCNT() { return CNT_Bullet_Hit_Zombie; }
    public int Get_Bullets_Missed_ZombieCNT() { return CNT_Bullets_Missed_Zombie; }
    public void Update_IncrementBulletsShotCNT()
    {
        if (GameManager.Instance.KngGameState == ARZState.Pregame)
        {
            Logger.Debug("dont count  ");
            return;
        }

        CNT_BulletsFired++;


        //ha, i did fix this on my branch ... keep this comment here 
        //ScoreDebugCon.Instance.Update_shotsfired(CNT_BulletsFired);
    }
    public void Update_IncrementBullet_Hit_ZombieCNT()
    {
        if (GameManager.Instance.KngGameState == ARZState.Pregame)
        {
            Logger.Debug("dont count  ");
            return;
        }
        CNT_Bullet_Hit_Zombie++; ScoreDebugCon.Instance.Update_hit(CNT_Bullet_Hit_Zombie);
    }
    public void Update_IncrementBullets_Missed_ZombieCNT()
    {
        if (GameManager.Instance.KngGameState == ARZState.Pregame)
        {
            Logger.Debug("dont count  ");
            return;
        }
        //if (GameManager.Instance.KngGameState != ARZState.WavePlay)
        //{
        //    Logger.Debug("dont count  ");
        //    return;
        //}
        CNT_Bullets_Missed_Zombie++;
        //ScoreDebugCon.Instance.Update_miss(CNT_Bullets_Missed_Zombie);
    }

    int CNT_ZombiesKilled = 0;
    // gamemanager.PlayerDied_GameManager()->
    public int Get_ZombiesKilledCNT() { return CNT_ZombiesKilled; }
    //gamemanager.KillEnemy_and_handle_streak
    public void Increment_ZombiesKilledCNT()
    {
        GameManager.Instance.TryUpdateSlowTime();
        if (GameManager.Instance.KngGameState == ARZState.Pregame)
        {
            Logger.Debug("dont count  ");
            return;
        }
        CNT_ZombiesKilled++;
    }


    int SKORE_headShotCount = 0;
    public void Update_headShotCNT()
    {
        if (GameManager.Instance.KngGameState == ARZState.Pregame)
        {
            Logger.Debug("dont count  ");
            return;
        }
        SKORE_headShotCount++;
        //ScoreDebugCon.Instance.Update_heads(SKORE_headShotCount);
        RzPlayerComponent.Instance.PlayHud_Debug_Event(SKORE_headShotCount.ToString());

        if (GameSettings.Instance.GAmeSessionType == ARZGameSessionType.MULTI)
        {

            if (UDPcommMNGR.Instance != null)
            {
                if (SKORE_headShotCount > 1)
                {
                    if (SKORE_headShotCount % 3 == 0)
                    {
                        //send message to other holo 
                        UDPcommMNGR.Instance.HelpSendMEssage(GameSettings.Instance.Ip_External_OtherHL, GameSettings.Instance.Port_External_OtherHL, GameSettings.Instance.GetMSG_smallAttack());
                        Debug.Log("test send message to other hl");
                    }
                }
            }
        }


    }
    public int Get_headShotCNT() { return SKORE_headShotCount; }


    int SKORE_torsoShotCount = 0;
    public void Update_torsoShotCNT()
    {
        if (GameManager.Instance.KngGameState == ARZState.Pregame)
        {
            Logger.Debug("dont count  ");
            return;
        }
        SKORE_torsoShotCount++;
        //ScoreDebugCon.Instance.Update_torsos(SKORE_torsoShotCount);
    }
    public int Get_torsoShotCNT() { return SKORE_torsoShotCount; }

    int SKORE_limbShotCount = 0;
    public void Update_limbShotCNT()
    {
        if (GameManager.Instance.KngGameState == ARZState.Pregame)
        {
            Logger.Debug("dont count  ");
            return;
        }
        //SKORE_limbShotCount++; 
        //ScoreDebugCon.Instance.Update_limb(SKORE_limbShotCount);
    }
    public int Get_limbShotCNT() { return SKORE_limbShotCount; }


    //not used in scores , but may be needed to generate more stats
    int CNT_Zombies_created = 0;
    public int Get_ZombiesCreatedCNT() { return CNT_Zombies_created; }
    public void Increment_ZombiesCreated() { CNT_Zombies_created++; }
    //1111111111111111111111111111111111111111111111111111111111111111111111
    int SKORE_headShotKillsCount = 0;
    public void Update_headShotKillsCNT()
    {
        if (GameManager.Instance.KngGameState == ARZState.Pregame)
        {
            Logger.Debug("dont count  ");
            return;
        }
        SKORE_headShotKillsCount++;
        //ScoreDebugCon.Instance.Update_headKills(SKORE_headShotKillsCount);
        // GameManager.Instance.TryUpdateSlowTime();
    }
    public int Get_headShotKillsCNT() { return SKORE_headShotKillsCount; }
    int SKORE_torsoShotKillsCount = 0;
    public void Update_torssoShotKillsCNT()
    {
        if (GameManager.Instance.KngGameState == ARZState.Pregame)
        {
            Logger.Debug("dont count  ");
            return;
        }
        SKORE_torsoShotKillsCount++;
        //ScoreDebugCon.Instance.Update_torrsoKills(SKORE_torsoShotKillsCount);
    }
    public int Get_torssoShotKillsCNT() { return SKORE_torsoShotKillsCount; }
    int SKORE_limbShotKillsCount = 0;
    public void Update_limbShotKillsCNT()
    {
        if (GameManager.Instance.KngGameState == ARZState.Pregame)
        {
            Logger.Debug("dont count  ");
            return;
        }
        SKORE_limbShotKillsCount++;
        //ScoreDebugCon.Instance.Update_limbKills(SKORE_limbShotKillsCount);
    }
    public int Get_limbShotKillsCNT() { return SKORE_limbShotKillsCount; }

    //0000000000000000000000000000000000000000000000000000000000000000000000
    int Points_CurWave = 0;
    // gamemanager.PlayerDied_GameManager()-> we get curwavepoints, and we 
    //1) show them on canvas
    //2) add them to  totalpoints lost
    //3) remove them from curr totalpoints 
    public int Get_PointsCurWave() { return Points_CurWave; }
    public void Update_Add_PointsCurWave(int argpointstoadd)
    {
        if (GameManager.Instance.KngGameState == ARZState.Pregame)
        {
            Logger.Debug("dont count  ");
            return;
        }

        Points_CurWave += argpointstoadd;
        //ScoreDebugCon.Instance.Update_WAVEPoints(Points_CurWave);

        if (GameSettings.Instance.GAmeSessionType == ARZGameSessionType.UDP)
        {
            if (UDPcommMNGR.Instance != null)
            {
                //send score to other holo 
                // if ( == ARZGameModes.) { };


                string ScoreToSendHotherHL = "#" + GameSettings.Instance.GameMode.ToString() + "#" + Points_CurWave.ToString();

                UDPcommMNGR.Instance.HelpSendMEssage(GameSettings.Instance.Ip_External_ScoreServer, GameSettings.Instance.Port_External_ScoreServer, ScoreToSendHotherHL);
            }
        }
        else

        if (GameSettings.Instance.GAmeSessionType == ARZGameSessionType.MULTI)
        {
            if (UDPcommMNGR.Instance != null)
            {
                string ScoreToSendHotherHL = "#" + GameSettings.Instance.GameMode.ToString() + "#" + Points_CurWave.ToString();
                UDPcommMNGR.Instance.HelpSendMEssage(GameSettings.Instance.Ip_External_OtherHL, GameSettings.Instance.Port_External_OtherHL, ScoreToSendHotherHL);
                UDPcommMNGR.Instance.HelpSendMEssage(GameSettings.Instance.Ip_External_ScoreServer, GameSettings.Instance.Port_External_ScoreServer, ScoreToSendHotherHL);
            }
        }





    }
    public void Reset_WavePoints() { Points_CurWave = 0; }


    int Points_TotalLost = 0;
    //PersistantScoreGrabber.DoGrabScores()->
    public int Get_PointsTotalLost() { return Points_TotalLost; }
    //gamemanager.PlayerDied_GameManager()-> we get curwavepoints, and we   add them to  totalpoints lost
    public void Update_Add_PointsTotalLost(int arglostpoints)
    {
        if (GameManager.Instance.KngGameState == ARZState.Pregame)
        {
            Logger.Debug("dont count  ");
            return;
        }
        Points_TotalLost += arglostpoints;
    }


    //do this last 
    int Points_Total = 0;
    //PersistantScoreGrabber.DoGrabScores() 
    // gamemanager.HardStop()
    // gamemanager.PlayerDied_GameManager() if time is up
    // SCOREboard.UpdateUIText()
    public int Get_PointsTotal() { return Points_Total; }
    public void Update_Add_PointsTotal(int argpointstoadd)
    {
        if (GameManager.Instance.KngGameState == ARZState.Pregame)
        {
            Logger.Debug("dont count  ");
            return;
        }
        // displaya message
        Logger.Debug("Noah Added DoublePoints here in the code: " + transform.name);
        Points_Total += (argpointstoadd * doublePointsIsOn);
    }
    public void Update_Remove_PointsTotal(int argpointstoadd)
    {
        Points_Total -= argpointstoadd;
    }

    int bonusPoints = 0;
    public int Get_BonusPoints() { return bonusPoints; }
    public void Update_BonusPoints(int argbonus)
    {
        if (GameManager.Instance.KngGameState == ARZState.Pregame)
        {
            Logger.Debug("dont count  ");
            return;
        }
        bonusPoints += argbonus;
    }

    //0000000000000000000000000000000000000000000000000000000000000000000000

    public void ResetScore()
    {
        CNT_Deaths = 0;
        CNT_WavesPlayed = 0;
        CNT_reloads = 0;

        CNT_BulletsFired = 0;
        CNT_Bullet_Hit_Zombie = 0;
        CNT_Bullets_Missed_Zombie = 0;

        CNT_ZombiesKilled = 0;
        CNT_Zombies_created = 0;

        Points_Total = 0;
        Points_CurWave = 0;
        Points_TotalLost = 0;

        SKORE_headShotCount = 0;
        SKORE_torsoShotCount = 0;
        SKORE_limbShotCount = 0;

        SKORE_headShotKillsCount = 0;
        SKORE_torsoShotKillsCount = 0;
        SKORE_limbShotKillsCount = 0;
    }

}
