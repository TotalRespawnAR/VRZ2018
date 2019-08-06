using UnityEngine;

public class GameEventsManager : MonoBehaviour
{
    public static GameEventsManager Instance = null;
    private void Awake()
    {
        // Debug.Log("GAME eventMANAGER Awake");

        //  FindObjectOfType<cursorkiller>().ShouldIkillCursor();
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(gameObject);
    }

    #region Events
    public delegate void EVENTTOGGOLESTEMINPUT(bool argonoff);
    public static event EVENTTOGGOLESTEMINPUT OnToggleInputs;
    public static void CALL_ToggleStemInput(bool argonoff)
    {
        if (OnToggleInputs != null) OnToggleInputs(argonoff);
    }


    public delegate void GunFlavorChanged(GunType argGunType);
    public static event GunFlavorChanged OnGunSetChanged;
    public static void Call_GunSetChangeTo(GunType argGunType)
    {
        if (OnGunSetChanged != null) OnGunSetChanged(argGunType);
    }


    public delegate void UPDATECURIGUN(IGun argIgun);
    public static event UPDATECURIGUN OnUpdateCurIGun;
    public static void Call_SetCurIgunTo(IGun argIgun)
    {
        if (OnUpdateCurIGun != null) OnUpdateCurIGun(argIgun);
    }









    public delegate void TriggerHandler(int argNum);
    public static TriggerHandler CountDownHandler;
    public void call_CountDownAudioVideo(int argNum) { if (NewMethod()) { CountDownHandler(argNum); } }
    private bool NewMethod()
    {
        return CountDownHandler != null;
    }


    public delegate void GgamePausedEvent(bool argonoff);
    public static GgamePausedEvent OnGamePaused;
    public void SignalGamePause(bool argonoff)
    {
        if (OnGamePaused != null) OnGamePaused(argonoff);
    }

    public delegate void GgameContinuedEvent(bool argonoff);
    public static GgameContinuedEvent OnGameContinue;
    public void SignalGameContinue(bool argonoff)
    {
        if (OnGameContinue != null) OnGameContinue(argonoff);
    }

    public delegate void SpawnPointIDreadyEvent(SpawnPoint argSpawnPoint);
    public static SpawnPointIDreadyEvent OnSpawnIdReady;
    public void SignalSpawnReady(SpawnPoint argSpawnPoint)
    {
        if (OnSpawnIdReady != null) OnSpawnIdReady(argSpawnPoint);
    }

    public delegate void EventOtherPlayerStreak();
    public static EventOtherPlayerStreak OtherPlayerStreak_Handeler;
    public void Call_IHeardOtherPlayerTriggerStreakMax()
    {
        if (IsHandlerAvailable()) { OtherPlayerStreak_Handeler(); }
    }
    public bool IsHandlerAvailable()
    {
        return OtherPlayerStreak_Handeler != null;
    }

    public delegate void Event30SecLeft();
    public static Event30SecLeft OnThirtySecLeft;
    public void Call_ThirtySecLeft()
    {
        if (OnThirtySecLeft != null) OnThirtySecLeft();
    }

    public delegate void EventWallBreaks();
    public static EventWallBreaks OnWallBrak;
    public void Call_WallBroke()
    {
        if (OnWallBrak != null) OnWallBrak();
    }

    public delegate void EventShakeCam();
    public static EventShakeCam OnTRyShake;
    public void CAll_TryShakeCam()
    {
        if (OnTRyShake != null) OnTRyShake();
    }

    public delegate void EventWomp();
    public static EventWomp OnWomp;
    public void Call_OnWomp()
    {
        if (OnWomp != null) OnWomp();
    }


    public delegate void EventTakeHit(Bullet argBullet, int srgId); //zid is dissregarded unless the zombie registered to it
    public static EventTakeHit OnTakeHit;
    public void Call_TakebulletHit(Bullet argBullet, int argZid)
    {
        //Debug.Log("bullet zid=" + argZid);
        if (OnTakeHit != null) OnTakeHit(argBullet, argZid);
    }

    public delegate void EventSuddenDeath();
    public static EventSuddenDeath OnSuddenDeath;
    public void Call_SuddenDeath()
    {
        if (OnSuddenDeath != null) OnSuddenDeath();
    }


    public delegate void EventTargExplode();
    public static EventTargExplode OnTargExplode;
    public void Call_targExplode()
    {
        if (OnTargExplode != null) OnTargExplode();
    }


    public delegate void EventTimeToSpawnSpecialZomb(int x);
    public static EventTimeToSpawnSpecialZomb OnTimeToSpawnSpecialZomb;
    public void Call_TimeTospawnSpcialZomb(int x)
    {
        if (OnTimeToSpawnSpecialZomb != null) OnTimeToSpawnSpecialZomb(x);
    }

    //forgot to note this event in last commit
    public delegate void EventAnchoredObjPlaced(string argAncName);
    public static EventAnchoredObjPlaced OnGameObjectAnchoredPlaced;
    public void Call_AnchoredGoPlaced(string argAncName)
    {
        if (OnGameObjectAnchoredPlaced != null) OnGameObjectAnchoredPlaced(argAncName);
    }


    public delegate void EventRZexperiencStarted();
    public static EventRZexperiencStarted OnRzExperienceStarted;
    public void Call_RzExperienceStarted()
    {
        if (OnRzExperienceStarted != null) OnRzExperienceStarted();
    }

    //public delegate void EventWaveStartedOrReset(int argWavNumber);
    //public static EventWaveStartedOrReset OnWaveStartedOrReset;
    //public void Call_WaveStartedOrReset(int argWavNumber)
    //{
    //    if (OnWaveStartedOrReset != null) OnWaveStartedOrReset(argWavNumber);
    //}

    public delegate void EventLEvelEded();
    public static EventLEvelEded OnLevelEnded;
    public void Call_LevelEnded()
    {
        if (OnLevelEnded != null) OnLevelEnded();
    }

    public delegate void EventWaveStartedOrReset_DEO(WaveLevel argCurWaveLevel);
    public static EventWaveStartedOrReset_DEO OnWaveStartedOrReset_DEO;
    public void Call_WaveStartedOrReset_DEO(WaveLevel argCurWaveLevel)
    {

        if (OnWaveStartedOrReset_DEO != null) OnWaveStartedOrReset_DEO(argCurWaveLevel);
    }


    public delegate void EventShooterFired();
    public static EventShooterFired OnShooterFired;
    public void Call_ShooterFired()
    {
        if (OnShooterFired != null) OnShooterFired();
    }



    public delegate void PLAYER2DIED();
    public static event PLAYER2DIED OnPlayer2Died;
    public void CALL_Player2Died()
    {
        if (OnPlayer2Died != null) OnPlayer2Died();
    }
    public delegate void EventShooterMissedt();
    public static EventShooterMissedt OnShooterMissed;
    public void Call_ShooterMissed()
    {
        if (OnShooterMissed != null) OnShooterMissed();
    }


    public delegate void EventShooterHitted();
    public static EventShooterHitted OnShooterHitted;
    public void Call_ShooterHitted()
    {
        if (OnShooterHitted != null) OnShooterHitted();
    }

    public delegate void EventNoLiveZombies();
    public static EventNoLiveZombies OnNoLiveZombiesLeft;
    public void Call_AllLiveZombiesDied()
    {
        if (OnNoLiveZombiesLeft != null) OnNoLiveZombiesLeft();
    }


    public delegate void EventLevelTicker(TickBools tb);
    public static EventLevelTicker OnLevelTicked;
    public void CAll_LevelTicked(TickBools tb)
    {
        if (OnLevelTicked != null) OnLevelTicked(tb);
    }

    public delegate void EventLevelTickerFly();
    public static EventLevelTickerFly OnLevelFlyTicked;
    public void CAll_LevelFlyTicked()
    {
        if (OnLevelFlyTicked != null) OnLevelFlyTicked();
    }

    public delegate void EventAddToZQ(Data_Enemy de);
    public static EventAddToZQ OnaddToZQ;
    public void Call_AzzRoZQ(Data_Enemy de)
    {
        if (OnaddToZQ != null) OnaddToZQ(de);
    }

    public delegate void EventStartStop_AUTOSHOOT(bool argstst);
    public static EventStartStop_AUTOSHOOT Onstst;
    public void Call_StartstopAutoshoot(bool argstst)
    {
        if (Onstst != null) Onstst(argstst);
    }

    public delegate void EventOnboardDisplayScore(int points, bool argaddsubstract);
    public static EventOnboardDisplayScore OnOnBoardDisplay;
    public void Call_OnOnBoardDisplay(int points, bool argaddsubstrac)
    {
        if (OnOnBoardDisplay != null) OnOnBoardDisplay(points, argaddsubstrac);
    }

    //public delegate void EventZombieDied(int argZid);
    //public static EventZombieDied OnZombieDied;

    public delegate void EventStrikerShoot();
    public static EventStrikerShoot StrikerShoot_Handeler;
    public void Call_StrikerShoot()
    {
        if (StrikerShoot_Handeler != null) StrikerShoot_Handeler();
    }

    public delegate void EventStrikerReload();
    public static EventStrikerReload StrikerReload_Handeler;
    public void Call_StrikerReload()
    {
        if (StrikerReload_Handeler != null) StrikerReload_Handeler();
    }

    public delegate void EventStrikerDryFire();
    public static EventStrikerDryFire StrikerDryFire_Handeler;
    public void Call_StrikerDryFire()
    {
        if (StrikerDryFire_Handeler != null) StrikerDryFire_Handeler();
    }


    //when receiving a udp message starting with # , the udprespnce handels it and raises this event.
    // a ui component could listen to this
    public delegate void EventUpdateScoreFromOtherHL(string pointsStr);
    public static EventUpdateScoreFromOtherHL OnUpdateScoreFromOtherHL;
    public void Call_OnUpdateScoreFromOtherHL(string pointsStr)
    {
        if (OnUpdateScoreFromOtherHL != null) OnUpdateScoreFromOtherHL(pointsStr);
    }

    //raised when having registered a smal lattck from other hl  
    // poten
    public delegate void EventOtherHLSmallAttack();
    public static EventOtherHLSmallAttack OnOtherHLSmallAttack;
    public void Call_OtherHLSmallAttack()
    {
        if (OnOtherHLSmallAttack != null) OnOtherHLSmallAttack();
    }


    public delegate void EventOtherHLStartsGame();
    public static EventOtherHLStartsGame OnOtherHLStartsGame;
    public void Call_OtherHlStartedGame()
    {
        if (OnOtherHLStartsGame != null) OnOtherHLStartsGame();
    }



    public delegate void EventLocalCrateChangedState(int argId, int ArgState);
    public static EventLocalCrateChangedState OnLocalCrateChangedState;
    public void Call_LocalCrateChangedState(int argId, int ArgState)
    {
        if (OnLocalCrateChangedState != null) OnLocalCrateChangedState(argId, ArgState);
    }


    public delegate void EventRemoteCrateReceided(int argId, int ArgState);
    public static EventRemoteCrateReceided OnRemoteCrateReceived;
    public void Call_REMOTECrateChangedState(int argId, int ArgState)
    {
        if (OnRemoteCrateReceived != null) OnRemoteCrateReceived(argId, ArgState);
    }


    public delegate void EventSlowTimeOn();
    public static EventSlowTimeOn OnSlowTimeOn;
    public void Call_SlowTimeOn()
    {
        if (OnSlowTimeOn != null) OnSlowTimeOn();
    }
    public delegate void EventSlowTimeOff();
    public static EventSlowTimeOff OnSlowTimeOff;
    public void Call_SlowTimeOff()
    {
        if (OnSlowTimeOff != null) OnSlowTimeOff();
    }

    //public delegate void EVENTALLOWEDGUNINDEX(int x);
    //public static event EVENTALLOWEDGUNINDEX OnUpdateAvailableGUnIndex;
    //public static void CALL_UpdateAvailableGUnIndex(int x)
    //{
    //    if (OnUpdateAvailableGUnIndex != null) OnUpdateAvailableGUnIndex(x);
    //}


    //***************************************************************Tuto
    public delegate void EventTutorialTrigger();
    public static EventTutorialTrigger OnTutoTriggerPulled;
    public void CallTutoPlayerPulledTrigger()
    {
        if (OnTutoTriggerPulled != null) OnTutoTriggerPulled();
    }

    public delegate void EventTutorialAutoReload();
    public static EventTutorialAutoReload OnTutoAutoReload;
    public void CallTutoAutoReload()
    {
        if (OnTutoAutoReload != null) OnTutoAutoReload();
    }

    public delegate void EventPlayerCanReload(bool argCanReload);
    public static EventPlayerCanReload OnCAnReloadReload;
    public void Call_PlayerCanReload(bool argCanReload)
    {
        if (OnCAnReloadReload != null) OnCAnReloadReload(argCanReload);
    }


    public delegate void EventPlayerCanSwitch(bool argCanReload);
    public static EventPlayerCanSwitch OnCAnSwitchWeapons;
    public void Call_PlayerCanSwitchWeapons(bool argCanReload)
    {
        if (OnCAnSwitchWeapons != null) OnCAnSwitchWeapons(argCanReload);
    }

    public delegate void EventPlayerCanShoot(bool argCanReload);
    public static EventPlayerCanShoot OnCAnShoot;
    public void Call_PlayerCanShoot(bool argCanReload)
    {
        if (OnCAnShoot != null) OnCAnShoot(argCanReload);
    }
    //CallTutoPlayerrCANReloaded

    public delegate void EventTutorialReload();
    public static EventTutorialReload OnTutoReload;
    public void CallTutoPlayerreloaded()
    {
        if (OnTutoReload != null) OnTutoReload();
    }

    public delegate void EventTutorialWeaponchange();
    public static EventTutorialWeaponchange OnTutoWeaponChange;
    public void CallTutoPlayerChangedWeapon()
    {
        if (OnTutoWeaponChange != null) OnTutoWeaponChange();
    }

    public delegate void EventTutorialPlayerKilledZombie();
    public static EventTutorialPlayerKilledZombie OnPlayerKilledZombie;
    public void CallTutoPlayerKilledZombie()
    {
        if (OnPlayerKilledZombie != null) OnPlayerKilledZombie();
    }

    public delegate void EventTutorialPlayerAvoidedAxe();
    public static EventTutorialPlayerAvoidedAxe OnTutoPlayerAoidedAxe;
    public void CallTutoPlayerAoidedAxe()
    {
        if (OnTutoPlayerAoidedAxe != null) OnTutoPlayerAoidedAxe();
    }

    public delegate void EventTutorialSkip();
    public static EventTutorialSkip OnSkipTuto;
    public void CAll_SKIPtuto()
    {
        if (OnSkipTuto != null) OnSkipTuto();
    }
    //***************************************************************Tuto



    public delegate void EventDoors(char lr, char oc);
    public static EventDoors On_Doors;
    public void CAll_DoorsControle(char lr, char oc)
    {
        if (On_Doors != null) On_Doors(lr, oc);
    }

    public delegate void EventMATchange(int argx);
    public static EventMATchange On_MatChange;
    public void CAll_Matchange(int argx)
    {
        if (On_MatChange != null) On_MatChange(argx);
    }

    public delegate void EVENTSpotLightTarget(int arglightID, Transform targ);
    public static EVENTSpotLightTarget On_SpotLightTarget;
    public void CAll_SpotLightTarget(int arglightID, Transform targ)
    {
        if (On_SpotLightTarget != null) On_SpotLightTarget(arglightID, targ);
    }


    public delegate void EVENTSpotLightOnOff(int arglightID, bool argOnOff);
    public static EVENTSpotLightOnOff On_SpotLightOnOff;
    public void CAll_SpotLightOnOff(int arglightID, bool argOnOff)
    {
        if (On_SpotLightOnOff != null) On_SpotLightOnOff(arglightID, argOnOff);
    }

    public delegate void EVENTWaveMiniBossDied();
    public static EVENTWaveMiniBossDied On_WaveMiniBossDied;
    public void CAll_WaveMiniBossDied()
    {
        if (On_WaveMiniBossDied != null) On_WaveMiniBossDied();
    }

    public delegate void EVENTWaveAXEMANied();
    public static EVENTWaveAXEMANied On_WaveMiniAXMANDied;
    public void CAll_WaveAXMANDied()
    {
        if (On_WaveMiniAXMANDied != null) On_WaveMiniAXMANDied();
    }

    public delegate void EVENTWaveSPRINTERied();
    public static EVENTWaveSPRINTERied On_WaveSPrinterEnemyDied;
    public void CAll_WavSPRINTERDied()
    {
        if (On_WaveSPrinterEnemyDied != null) On_WaveSPrinterEnemyDied();
    }



    public delegate void EVENTWaveGRAVERDied();
    public static EVENTWaveGRAVERDied On_WaveGRAVERDied;
    public void CAll_WaveGRAVERDied()
    {
        if (On_WaveGRAVERDied != null) On_WaveGRAVERDied();
    }

    public delegate void EVENTWaveSTANDARDied();
    public static EVENTWaveSTANDARDied On_WaveStandardEnemyDied;
    public void CAll_WaveSTANDARDDied()
    {
        if (On_WaveStandardEnemyDied != null) On_WaveStandardEnemyDied();
    }



    public delegate void EVENT_PreyReady(IEnemyEntityComp argIPrey);
    public static EVENT_PreyReady On_PreyIsReady;
    public void CallPreyIsReadyForPickup(IEnemyEntityComp argIPrey)
    {
        if (On_PreyIsReady != null) On_PreyIsReady(argIPrey);
    }

    public delegate void EVENTPredatorPointed();
    public static EVENTPredatorPointed On_PredatorPointed;
    public void CallPredatorPointed()
    {
        if (On_PredatorPointed != null) On_PredatorPointed();
    }

    public delegate void EVENTEnemyIsAimed(int argID, bool argOnOff);
    public static EVENTEnemyIsAimed On_EnemyIsAimed;
    public void CallEnemyIsAmied(int argID, bool argOnOff)
    {
        if (On_EnemyIsAimed != null) On_EnemyIsAimed(argID, argOnOff);
    }

    public delegate void EVENTEnemyCompletedCombatAnim(int argID, int combatanimtype);
    public static EVENTEnemyCompletedCombatAnim On_EnemyCompletedCombatAnim;
    public void Call_EnemyCompletedCombatAnim(int argID, int combatanimtype)
    {
        if (On_EnemyCompletedCombatAnim != null) On_EnemyCompletedCombatAnim(argID, combatanimtype);
    }


    #endregion

}
