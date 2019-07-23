//////#define ENABLE_DEBUGLOG

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ARZScoreBoard : MonoBehaviour {

    public Text AccuracyText;
    public Text HeadShotCountText;
    public Text KillCountText;
    public Text pointsText;

    GameManager manager;

    // Use this for initialization
    void Start()
    {
        manager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (manager == null) return;
        UpdateUIText();
    }


    void UpdateUIText()
    {
        if (AccuracyText != null)
            AccuracyText.text = CalcAccuracy();
        if (HeadShotCountText != null)
            HeadShotCountText.text =   manager.GetStreakManager().Get_MaxHEadshotsInARow().ToString();
        if (KillCountText != null)
            KillCountText.text =  manager.GetScoreMAnager().Get_ZombiesKilledCNT().ToString();
        if (pointsText != null)
            pointsText.text = manager.GetScoreMAnager().Get_PointsTotal().ToString();
    }
    string CalcAccuracy()
    {
        if (manager == null) return "0";

        if (manager != null)
        {
            float fired = (float)manager.GetScoreMAnager().Get_BulletsFiredCNT();
            float landed = (float)manager.GetScoreMAnager().Get_Bullet_Hit_ZombieCNT();
            float missed = (float)manager.GetScoreMAnager().Get_Bullets_Missed_ZombieCNT();
            if (fired > 0)
            {
                float accuracy = (landed / fired) * 100;
                return accuracy.ToString("0.00") + " %";
            }
        }


        return "0 %";


    }
}
