//#define  ENABLE_DEBUGLOG
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EBD_Death : EB_Base
 {

    Action _startAction;
    IEnemyEntityComp m_ieec;
    IEnemyMeshComp m_Imesh;
    float CurTimer = 0f;
    float DisolveTime;
    public EBD_Death(IEnemyEntityComp argIentity, ModesEnum argmodeenum, float argMAxtime, Action argStartAction) : base(argIentity, argmodeenum, argMAxtime, argStartAction, false)
    {
        _startAction = argStartAction;
        DisolveTime = argMAxtime;
        m_ieec = argIentity;
        m_Imesh = m_ieec.GEt_MEsher();
    }

    ~EBD_Death()
    {
#if ENABLE_DEBUGLOG
        Debug.Log("ebd ~ first");
#endif
        base.Dispose();
    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override void ReadBullet(Bullet abulletfromDamageBehComp)
    {

    }

    // animate the game object from -1 to +1 and back
    public float minimum = 0F;
    public float maximum = 1.0F;

    // starting value for the Lerp
    float t = 0.0f;
    int twice = 1;
    int curplay = 0;
    void LerpFloat() {
        if (curplay > twice) { t = 0f; return; }
        // animate the position of the game object...
         Mathf.Lerp(minimum, maximum, t);

        // .. and increase the t interpolater
        t += 0.2f * Time.deltaTime;

        // now check if the interpolator has reached 1.0
        // and swap maximum and minimum so game object moves
        // in the opposite direction.
        if (t > 1.0f)
        {
            float temp = maximum;
            maximum = minimum;
            minimum = temp;
            t = 0.0f;
        }
    }

    public override void RunBehavior()
    {
        //LerpFloat();

        // m_Imesh.RunApplyDissovefactoactor();

    }

    public override void StartBehavior()
    {
      //  m_ieec.Get_Animer().Do_LookPlayer(false);
        //base.StartBehavior();
        if (_startAction != null)
            _startAction();
        else Debug.Log("  action null");
    }

    public override string ToString()
    {
        return GET_MyModeEnum().ToString(); // base.ToString();
    }

    
}
