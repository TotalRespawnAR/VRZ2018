//////#define ENABLE_DEBUGLOG
using UnityEngine;

public class ScoreDebugCon : MonoBehaviour
{
    public static ScoreDebugCon Instance = null;
    public TextMesh PointsWaveMesh;
    public TextMesh ShotsMesh;
    public TextMesh HitsMesh;
    public TextMesh MissMesh;
    public TextMesh WasHeadMesh;
    public TextMesh HeadShotMesh;
    public TextMesh TorsoMesh;
    public TextMesh LimbMesh;
    public TextMesh CurStreakMesh;
    public TextMesh MaxStreakMesh;
    public TextMesh CntStreakMesh;

    public TextMesh CntHeadShotKills;
    public TextMesh CntTorssoShotKills;
    public TextMesh CntLimbshotKils;
    private void Awake()
    {
        if (Instance == null)
        {
            //FindWavveSettinggsFile();
            //if (has_waveLevelSettingsFile) Load(Kng_wavesettingsFilePATH + "/" + WaveLevelSetttingsFileName + ".txt");

            DontDestroyOnLoad(this.gameObject);
            Instance = this;

        }

    }
    private void Start()
    {
#if ENABLE_DEBUGLOG
#endif 
        Debug.Log("Do I work2 " + gameObject.name);
        if (GameSettings.Instance != null)
        {
            if (!GameSettings.Instance.IsTestModeON)
            {

                PointsWaveMesh.text = "";
                ShotsMesh.text = "";
                WasHeadMesh.text = "";
                HitsMesh.text = "";
                MissMesh.text = "";
                HeadShotMesh.text = "";
                TorsoMesh.text = "";
                LimbMesh.text = "";
                CurStreakMesh.text = "";
                MaxStreakMesh.text = "";
                CntStreakMesh.text = "";
                CntHeadShotKills.text = "";
                CntTorssoShotKills.text = "";
                CntLimbshotKils.text = "";
            }
        }
    }

    public void Update_WAVEPoints(int argwavepoint)
    {
        if (GameSettings.Instance != null)
        {
            if (GameSettings.Instance.IsTestModeON)
            {
                PointsWaveMesh.text = "wave points=" + argwavepoint.ToString();
            }
        }
    }

    public void Update_shotsfired(int shotsfired)
    {
        if (GameSettings.Instance != null)
        {
            if (GameSettings.Instance.IsTestModeON)
            {
                ShotsMesh.text = "shots fired=" + shotsfired.ToString();
            }
        }
    }

    public void Update_wasHead(bool washead)
    {
        if (GameSettings.Instance != null)
        {
            if (GameSettings.Instance.IsTestModeON)
            {
                WasHeadMesh.text = "ishead?=" + washead.ToString();
            }
        }
    }

    public void Update_hit(int hit)
    {
        if (GameSettings.Instance != null)
        {
            if (GameSettings.Instance.IsTestModeON)
            {
                HitsMesh.text = "hit =" + hit.ToString();
            }
        }
    }

    public void Update_miss(int miss)
    {
        if (GameSettings.Instance != null)
        {
            if (GameSettings.Instance.IsTestModeON)
            {
                MissMesh.text = "miss =" + miss.ToString();
            }
        }
    }

    public void Update_heads(int heads)
    {
        if (GameSettings.Instance != null)
        {
            if (GameSettings.Instance.IsTestModeON)
            {
                HeadShotMesh.text = "head =" + heads.ToString();
            }
        }
    }
    public void Update_torsos(int torsos)
    {
        if (GameSettings.Instance != null)
        {
            if (GameSettings.Instance.IsTestModeON)
            {
                TorsoMesh.text = "torso =" + torsos.ToString();
            }
        }
    }
    public void Update_limb(int limbs)
    {
        if (GameSettings.Instance != null)
        {
            if (GameSettings.Instance.IsTestModeON)
            {
                LimbMesh.text = "limb =" + limbs.ToString();
            }
        }
    }
    public void Update_curStrek(int curStreak)
    {
        if (GameSettings.Instance != null)
        {
            if (GameSettings.Instance.IsTestModeON)
            {
                CurStreakMesh.text = "curstreak =" + curStreak.ToString();
            }
        }
    }

    public void Update_MaxStrek(int maxStreak)
    {
        if (GameSettings.Instance != null)
        {
            if (GameSettings.Instance.IsTestModeON)
            {
                MaxStreakMesh.text = "maxStreak =" + maxStreak.ToString();
            }
        }
    }
    public void Update_CNTStrek(int cntStreak)
    {
        if (GameSettings.Instance != null)
        {
            if (GameSettings.Instance.IsTestModeON)
            {
                CntStreakMesh.text = "CNTStreak =" + cntStreak.ToString();
            }
        }
    }

    public void Update_limbKills(int argLimbKills)
    {
        if (GameSettings.Instance != null)
        {
            if (GameSettings.Instance.IsTestModeON)
            {
                CntLimbshotKils.text = "limb Kills =" + argLimbKills.ToString();
            }
        }
    }

    public void Update_torrsoKills(int argTorssokills)
    {
        if (GameSettings.Instance != null)
        {
            if (GameSettings.Instance.IsTestModeON)
            {
                CntTorssoShotKills.text = "torsso Kills =" + argTorssokills.ToString();
            }
        }
    }


    public void Update_headKills(int argHEadKills)
    {
        if (GameSettings.Instance != null)
        {
            if (GameSettings.Instance.IsTestModeON)
            {
                CntHeadShotKills.text = "head Kills =" + argHEadKills.ToString();
            }
        }
    }


}
