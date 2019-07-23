// @Author Jeffrey M. Paquette ©2016
// @Author Nabil Lamriben ©2017

using UnityEngine;
using System.Collections;

public interface IWave {
    //string GetFileName();
    void StartWave();
    void WaveReload();
     void WaveUpdateKilledZombies_and_CheckIfISTImeToSpawnNew( );
      void OnGameOver();
    void WaveSTD_Completed_callGM_Handle_Pop_newPlusPLus();

    //need to call this from a voice manager ans set the bool back to false localy on waveitself when done spawing the special zpmbie
  //  void SetisspawningSpecialZombie();

    bool IsZombieCountOnScreenLow();

    void SpawnOneOfMyZombiesAtSpawn(SpawnPoint argHere); //should really just be a vect3  refactor
    GunType GetWaveGunType();
    ScriptWaveScenario GetScenarioObject();
}
