////#define  ENABLE_DEBUGLOG
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EB_SeekTarget : IEnemyBehaviorObj, IDisposable
{
#region Private vars
    IEnemyEntityComp m_ieec;
    IEnemyMoverComp m_Imover;
    ModesEnum _modeenum;
    float MaxTimePAss = 3f;
    float CurTime = 0f;
    int _headShot_NonLethal = 50; //used to be 80
    int _bodyShot_NonLethal = 15;
    int _limbShot_NonLethal = 5;
    Vector3 TargetToSeek;
    #endregion

    #region Constructor Destructor
    public EB_SeekTarget(IEnemyEntityComp argIentity, Vector3 Target)
    {
        _modeenum = ModesEnum.SEEKTARGET;
#if ENABLE_DEBUGLOG
        Debug.Log("Constructor" + _modeenum.ToString());
#endif
        m_ieec = argIentity;
        m_Imover = argIentity.Get_Mover();
 
        TargetToSeek = Target;
       
        StartBehavior();
    }

    ~EB_SeekTarget()
    {
#if ENABLE_DEBUGLOG
#endif
        Debug.Log("Destructor" + _modeenum.ToString());
        Dispose();
    }
#endregion
    public void Dispose()
    {
        Debug.Log("disposing Seektarg");
        GC.SuppressFinalize(this);
    }

#region Interface

    public ModesEnum GET_MyModeEnum()
    {
        return _modeenum;
    }

    public void ReadBullet(Bullet abulletfromDamageBehComp)
    {
    }

    public void StartBehavior()
    {
  
    }
    public void RunBehavior()
    {
        Debug.Log("running seek target beh");
    }
    public void EndBehavior( )
    {
        Dispose();
    }
    #endregion

    #region Private Methods
    void Update_bodyPartShot_nonLEthalPoints(Bullet bullet, int PointsNonLethal)
    {
        //int ExraLethalPoins = 0;
        //int newHP = m_ieec.Get_HP(); 
        //newHP -= bullet.damage;
        //if (newHP < 0)
        //{
        //    newHP = 0;
        //    ExraLethalPoins = PointsNonLethal;
        //}
        //m_ieec.Set_HP(bullet, PointsNonLethal + ExraLethalPoins, newHP);
    }
    public void CompleteAnyAnim(int argCombatanim)
    {

    }
    #endregion
}

