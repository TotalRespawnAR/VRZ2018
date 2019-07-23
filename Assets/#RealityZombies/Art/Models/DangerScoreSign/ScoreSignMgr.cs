using UnityEngine;

public class ScoreSignMgr : ShotObject
{
    public Animator anim;
    public TextMesh DisplayScore;
    public TextMesh DisplayWaveNumber;
    protected bool lowered = true;
    private GameManager manager;

    void Awake()
    {
        Lift();
        lowered = false;
    }

    // Use this for initialization
    void Start()
    {
        manager = GameManager.Instance;
        //
        Lower();
    }

    void Update()
    {
        //if (!lowered && Time.time > .5f) {
        //	Lower();
        //	lowered = true;
        //}
        if (!manager.IsPlayerDead)
        {
            SetScoreDisplay(manager.GetScoreMAnager().Get_PointsTotal().ToString());
        }
    }

    public void Lower()
    {
        if (!lowered)
        {
            // Debug.Log("Lowering Sign");
            // PlayHeadShotSound.Instance.PlayScoreSound("_ScoreSignMove");
            anim.SetBool("Lowered", true);
            lowered = true;
        }
    }

    public void Lift()
    {
        if (lowered)
        {
            //   Debug.Log("lifiting Sign");
            // PlayHeadShotSound.Instance.PlaySplatSound("_ScoreSignMove");
            anim.SetBool("Lowered", false);
            lowered = false;
        }
    }

    public override void Shot()
    {
        base.Shot();
        anim.SetTrigger("Shake");
    }

    public void SetScoreDisplay(string argScore)
    {
        DisplayScore.text = argScore;
    }

    public void SetWaveNumberDisplay(string argWaveNum)
    {
        DisplayWaveNumber.text = argWaveNum;
    }


}
