using System.Collections;
using UnityEngine;

public class HealthBarCtrl : MonoBehaviour
{
    IEnemyEntityComp m_ieec;
    public LineRenderer LR_Health;
    public LineRenderer LR_HeadShots;
    public LineRenderer LR_Armor;
    float InitialHP_Health;
    float InitialLength_Health = 0.4f;
    float OriginalHP;
    bool firstdamage = false;
    bool ArglenIsNot0;
    private void Awake()
    {

        // LR_Health.SetPosition(0, new Vector3(InitialLength_Health, 0, 0));
    }
    private void Start()
    {
        m_ieec = GetComponentInParent<IEnemyEntityComp>();
        if (m_ieec != null)
        {
            OriginalHP = (float)m_ieec.Get_HP();
            if (OriginalHP <= 0)
            {
                OriginalHP = 0.5f;
            }

            m_ieec.OnBulletWoundInflicted += SHowHP;
        }
        else
        {
            Debug.LogWarning("No IentityComp");
        }

        SetLength(1f);
        ArglenIsNot0 = true;
        HideAll();
        // StartCoroutine(ShowUp());
        ShowUp();

    }
    void OnDisable()
    {
        if (m_ieec != null)
        {
            m_ieec.OnBulletWoundInflicted -= SHowHP;
        }
        else
        {
            Debug.LogWarning("No IentityComp");
        }

    }
    public void SetInitialHP(float arghp)
    {
        InitialHP_Health = arghp;
    }
    public void SetInitialHP(float arghp, int HeadShotNum)
    {
        InitialHP_Health = (float)HeadShotNum;
    }
    public void ShowMe()
    {
        LR_Health.gameObject.SetActive(true);
        LR_HeadShots.gameObject.SetActive(true);
        LR_Armor.gameObject.SetActive(true);
    }

    public void HideAll()
    {
        LR_Health.gameObject.SetActive(false);
        LR_HeadShots.gameObject.SetActive(false);
        LR_Armor.gameObject.SetActive(false);
    }
    public void UpdatedHP(int argNewHP)
    {
        if (InitialLength_Health <= 0)
        {
            InitialLength_Health = 0.4f;
        }

        if (argNewHP <= 0)
        {
            argNewHP = 0;
        }
        // print("updating hp");
        if (argNewHP > 0)
        {
            ArglenIsNot0 = true;
        }
        else
        {
            ArglenIsNot0 = false;
        }

        float newHP00 = (float)argNewHP * 100.0f;
        float intilen00 = InitialLength_Health / 100f;
        float percentage = newHP00 / InitialHP_Health;

        float newLen = percentage * intilen00;
        SetLength(newLen);


        // InitialHP            100      InitialLength
        //  argNewHP                   
    }


    void SetLength(float argLen)
    {

        _curtime = 0f;
        //print("reset 0");
        duration = Time.time + 2.0f;
        LR_Health.SetPosition(0, new Vector3(argLen, 0, 0));

        if (GameManager.Instance != null)
        {

            if (argLen > 0.75f) { LR_Health.material = GameManager.Instance.HB_Mat_Green; }
            else
            if (argLen > 0.5f && argLen <= 0.75f) { LR_Health.material = GameManager.Instance.HB_Mat_YEllow; }
            else
            if (argLen > 0.25f && argLen <= 0.5f) { LR_Health.material = GameManager.Instance.HB_Mat_Orange; }
            else
            if (argLen <= 0.25f) { LR_Health.material = GameManager.Instance.HB_Mat_Red; }
        }
        else
        {

            Debug.LogWarning("No Game Manager ");
        }

    }

    void SHowHP(object o, ArgsBulletWoond e)
    {
        if (!firstdamage)
        {
            // StartCoroutine(ShowUp());
            firstdamage = true;
            LR_Health.gameObject.SetActive(true);
        }

        if (firstdamage)
        {
            int newHipLeft = e.CurHpforHealthbarAndScoreSender;

            float percent = ((float)newHipLeft / OriginalHP);
            SetLength(percent);
        }


    }
    float secondson = 3;
    float _curtime;



    Coroutine HB_Hide;

    public void ShowUp()
    {
        if (HB_Hide != null)
        {
            StopCoroutine(HB_Hide);
        }
        HB_Hide = StartCoroutine(DoTheCount());
    }
    float duration;
    IEnumerator DoTheCount()
    {
        duration = Time.time + 2.0f;
        while (ArglenIsNot0) //duration > Time.time
        {
            while (duration > Time.time)
            {

                LR_Health.gameObject.SetActive(true);
                yield return null;
            }
            //Debug.Log("not running");
            LR_Health.gameObject.SetActive(false);
            yield return null;
        }
        // print("stopped");
    }

    //causes mem leaks
    //IEnumerator DothCountOld()
    //{

    //    while (ArglenIsNot0)
    //    {
    //        if (_curtime < secondson)
    //        {
    //            LR_Health.gameObject.SetActive(true);
    //            Debug.Log("  running");
    //            _curtime += Time.deltaTime;
    //            print(_curtime);
    //        }
    //        else
    //        {

    //            HideAll();
    //            Debug.Log("not running");
    //        }


    //    }
    //    yield return null;

    //    //do
    //    //{
    //    //    if (_curtime < secondson)
    //    //    {
    //    //        LR_Health.gameObject.SetActive(true);
    //    //        _curtime += Time.deltaTime;
    //    //        print(_curtime);
    //    //    }
    //    //    else
    //    //    {

    //    //        HideAll();
    //    //    }

    //    //} while (ArglenIs0);

    //    //yield return null;

    //}

}
