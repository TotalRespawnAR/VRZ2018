
//#define  ENABLE_DEBUGLOG
using System;
using UnityEngine;
public class EBD_Burn : EB_Base
{

    Action _startAction;
    IEnemyEntityComp m_ieec;
    IEnemyMeshComp m_Imesh;
    float CurTimer = 0f;
    float DisolveTime;
    public EBD_Burn(IEnemyEntityComp argIentity, ModesEnum argmodeenum, float argMAxtime, Action argStartAction) : base(argIentity, argmodeenum, argMAxtime, argStartAction, false)
    {
        _startAction = argStartAction;
        DisolveTime = argMAxtime;
        m_ieec = argIentity;
        m_Imesh = m_ieec.GEt_MEsher();
    }

    ~EBD_Burn()
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
    bool hasbinishedBurning = false;
    void WaitWhileBurning()
    {
        if (hasbinishedBurning)
        {
            return;
        }
        else
        {
            t += Time.deltaTime;
            if (t > 6f)
            {
                hasbinishedBurning = true;
                Debug.Log("DoneBurnin");
                GameManager.Instance.Zombie_ID_Died(m_ieec.Get_ID());
                m_ieec.KillYourselfandCeanitup();

                m_Imesh.MeshDisolveToNothing();
            }
        }

    }

    public override void RunBehavior()
    {
        WaitWhileBurning();

    }

    public override void StartBehavior()
    {
        //  m_ieec.Get_Animer().Do_LookPlayer(false);
        //base.StartBehavior();
        if (_startAction != null)
            _startAction();
        else Debug.Log("  action null");

        m_ieec.StickFireOnMe();

    }

    public override string ToString()
    {
        return GET_MyModeEnum().ToString(); // base.ToString();
    }


}
