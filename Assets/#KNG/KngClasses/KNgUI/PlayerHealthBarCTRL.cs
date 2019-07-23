using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthBarCTRL : MonoBehaviour {

    public LineRenderer Player_HealthBar;
    public LineRenderer Player_SlowTimeBar;
    
    float InitialLength_Health = 1.0f;
    float InitialLength_SlowTime = 0.0f;
    float _curHealthLength;
    float _curSlowTimeLength;
    public void ResetBArs()
    {
        Player_HealthBar.SetPosition(0, new Vector3(0, InitialLength_Health, 0));
        Player_SlowTimeBar.SetPosition(0, new Vector3(0, InitialLength_SlowTime, 0));
    }

  //  public static PlayerHealthBarCTRL Instance = null;

    private void Awake()
    {
        //if (Instance == null)
        //{
        //    Instance = this;
        //    ResetBArs();

        //}
        //else
        //    Destroy(gameObject);
    }
 
 
    public void XUpdated_Percent_PlayerHEalthBAr(float argPercentLeft_0_1)
    {
       
         
        float newLen = argPercentLeft_0_1 * InitialLength_Health;

        
        SetLengthHealthBar(newLen);
               
    }
 

    void SetLengthHealthBar(float argLen)
    {
        Player_HealthBar.SetPosition(0, new Vector3(0,argLen, 0));
    }


    public void XUpdated_Percent_TimeBAr(float argPercentLeft_0_1)
    {


        float newLen = argPercentLeft_0_1;


        SetLengthTimeBar(newLen);

    }

    void SetLengthTimeBar(float argLen)
    {
        if (argLen <= 0.02) argLen = 0f;
        Player_SlowTimeBar.SetPosition(0, new Vector3(0, argLen, 0));

    }
}
