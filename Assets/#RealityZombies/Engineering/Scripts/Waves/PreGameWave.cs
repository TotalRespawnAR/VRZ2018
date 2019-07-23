using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreGameWave : MonoBehaviour, IWave
{
    #region Public_props
    [Tooltip("Total time this wave will be active.")]
    // public float WaveTimerInMinutes;
    public float WavetimeinSeconds;

    [Tooltip("Number of living zombies allowed at any one time.")]
    public int maxZombiesOnScreen;

    [Tooltip("Percentage of spawn points used. Range 0.0 - 1.0")]
    public float percentageOfSpawns;

    [Tooltip("Buffer before the same spawn can respawn a zombie")]
    public float CoolDownTime = 3.5f;


    [Tooltip("Hitpoint range of zombies this wave.")]
    public int minZombieHP, maxZombieHP;


    public int WaveNumber = 0;

    public ScriptWaveScenario GetScenarioObject()
    {
        throw new System.NotImplementedException();
    }

    public GunType GetWaveGunType()
    {
        throw new System.NotImplementedException();
    }
    #endregion




    public bool IsZombieCountOnScreenLow()
    {
        throw new System.NotImplementedException();
    }

    public void OnGameOver()
    {
        throw new System.NotImplementedException();
    }

    public void SpawnOneOfMyZombiesAtSpawn(SpawnPoint argHere)
    {
        throw new System.NotImplementedException();
    }

    public void StartWave()
    {
        throw new System.NotImplementedException();
    }

    public void WaveReload()
    {
        throw new System.NotImplementedException();
    }

    public void WaveSTD_Completed_callGM_Handle_Pop_newPlusPLus()
    {
        throw new System.NotImplementedException();
    }

    public void WaveUpdateKilledZombies_and_CheckIfISTImeToSpawnNew()
    {
        throw new System.NotImplementedException();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
