using UnityEngine;

public class RzPlayerHealthTubeControllerComponent : MonoBehaviour
{
    public static RzPlayerHealthTubeControllerComponent Instance = null;
    public Transform Atrans;
    public Transform Btrans;
    private void Awake()
    {
        Debug.Log("MY HB" + gameObject.name);
        if (Instance == null)
        {
            Instance = this;
            _cur_SIDE_HB_Length = InitialLength_SIDE_HB = SIDE_HB.transform.localScale.y;
            _cur_SIDE_SLOWTIM_Length = InitialLength_SIDE_SLOWTIM = SIDE_SLOWTIME.transform.localScale.y;
            ResetBArs();
        }
        else
            Destroy(gameObject);

    }


    public GameObject FullHealthObj;
    public GameObject FullSLowTimeObj;
    public GameObject Player_HealthBar;
    public Renderer Healthrend;
    public GameObject Player_SlowTimeBar;
    public GameObject SIDE_HB;
    public GameObject SIDE_SLOWTIME;
    float InitialLength_SIDE_HB;
    float InitialLength_SIDE_SLOWTIM;
    float _cur_SIDE_HB_Length;
    float _cur_SIDE_SLOWTIM_Length;


    public Material RedP;
    public Material YellowP;
    public Material GreenP;

    float InitialLength_Health = 1.0f;
    float InitialLength_SlowTime = 0.0f;
    float _curHealthLength;
    float _curSlowTimeLength;

    public void ResetBArs()
    {
        SIDE_HB.transform.localScale = new Vector3(1, InitialLength_SIDE_HB, 1);
        SIDE_SLOWTIME.transform.localScale = new Vector3(1, InitialLength_SIDE_SLOWTIM, 1);
        _cur_SIDE_HB_Length = InitialLength_SIDE_HB;
        _cur_SIDE_SLOWTIM_Length = InitialLength_SIDE_SLOWTIM;


        Player_HealthBar.transform.localScale = new Vector3(1, InitialLength_Health, 1);
        Player_SlowTimeBar.transform.localScale = new Vector3(1, InitialLength_SlowTime, 1);
        Healthrend.material = GreenP;
        SIDE_HB.GetComponent<UnityEngine.UI.Image>().color = Color.green;
    }
    #region Public methods intended to be called by an objects UPDATE() 
    public void Updated_Percent_PlayerHEalthBAr(float argPercentLeft_0_1)
    {
        float newLen = argPercentLeft_0_1 * InitialLength_Health;
        if (argPercentLeft_0_1 < .55f && argPercentLeft_0_1 > .33f)
        {
            Healthrend.material = YellowP;
            SIDE_HB.GetComponent<UnityEngine.UI.Image>().color = Color.yellow;

        }
        else
        if (argPercentLeft_0_1 <= .33f)
        {
            Healthrend.material = RedP;
            SIDE_HB.GetComponent<UnityEngine.UI.Image>().color = Color.red;
        }
        else
        {
            Healthrend.material = GreenP;
            SIDE_HB.GetComponent<UnityEngine.UI.Image>().color = Color.green;
        }
        SetLengthHealthBar(newLen);

    }




    public void Updated_Percent_TimeBAr(float argPercentLeft_0_1)
    {


        float newLen = argPercentLeft_0_1;


        SetLengthTimeBar(newLen);

    }
    #endregion



    void SetLengthHealthBar(float argLen)
    {
        Player_HealthBar.transform.localScale = new Vector3(1, argLen, 1);

        SIDE_HB.transform.localScale = new Vector3(1, argLen * InitialLength_SIDE_HB, 1);
    }



    void SetLengthTimeBar(float argLen)
    {
        if (argLen <= 0.02) argLen = 0f;
        Player_SlowTimeBar.transform.localScale = new Vector3(1, argLen, 1);

        SIDE_SLOWTIME.transform.localScale = new Vector3(1, argLen * InitialLength_SIDE_HB, 1); //use hb cuz initial time y scale is 0

    }
}
