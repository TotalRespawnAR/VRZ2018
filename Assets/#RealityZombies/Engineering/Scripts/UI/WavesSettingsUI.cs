using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesSettingsUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void DoSetGAmeTime() {

    }

    public void DoSetTime_30sec() { GameSettings.Instance.Global_Time_Apocalypse_GameEnds_600s_10m = 30f; }
    public void DoSetTime_60sec() { GameSettings.Instance.Global_Time_Apocalypse_GameEnds_600s_10m = 60f; }
    public void DoSetTime_120sec() { GameSettings.Instance.Global_Time_Apocalypse_GameEnds_600s_10m = 120f; }
    public void DoSetTime_240sec() { GameSettings.Instance.Global_Time_Apocalypse_GameEnds_600s_10m = 240f; }


}
