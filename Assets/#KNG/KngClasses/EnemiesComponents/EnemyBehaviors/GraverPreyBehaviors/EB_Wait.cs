////#define ENABLE_DEBUGLOG
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EB_Wait : IEnemyBehaviorObj, IDisposable
{

    #region Private vars
    IEnemyEntityComp m_ieec;
    IEnemyMoverComp m_iMover;
    ModesEnum _modeenum;
    int _headShot_NonLethal = 50; //used to be 80
    int _bodyShot_NonLethal = 15;
    int _limbShot_NonLethal = 5;
    #endregion

    #region Constructor Destructor
    public EB_Wait(IEnemyEntityComp argIbhc)
    {
        _modeenum = ModesEnum.WAITPREY;
#if ENABLE_DEBUGLOG
        Debug.Log("Constructor "+ _modeenum.ToString());
#endif
        m_ieec = argIbhc;
        m_iMover = argIbhc.Get_Mover();

        StartBehavior();

    }

    ~EB_Wait()
    {
#if ENABLE_DEBUGLOG
        Debug.Log("Destructor" + _modeenum.ToString());
#endif
        Dispose();
    }
    #endregion
    public void Dispose()
    {
#if ENABLE_DEBUGLOG

        Debug.Log("disposing wait");
#endif
        GC.SuppressFinalize(this);
    }
#region Interface


    public void StartBehavior()
    {
        //  m_iMover.setAnimType(EBSTATE.START);
        Debug.Log("IMPLEMENTME");

    }
    public void RunBehavior()
    {
      
        //nothing just idle
    }
    public void EndBehavior()
    {
        
        Dispose();
    }


    public ModesEnum GET_MyModeEnum()
    {
        return _modeenum;
    }

    public void ReadBullet(Bullet abulletfromDamageBehComp)
    {
        switch (abulletfromDamageBehComp.BulletPointsType)
        {
            case BulletPointsType.Head:
                Update_bodyPartShot_nonLEthalPoints(abulletfromDamageBehComp, _headShot_NonLethal);
                break;
            case BulletPointsType.Torso:
                Update_bodyPartShot_nonLEthalPoints(abulletfromDamageBehComp, _bodyShot_NonLethal);
                break;
            case BulletPointsType.Limbs:
                Update_bodyPartShot_nonLEthalPoints(abulletfromDamageBehComp, _limbShot_NonLethal);
                break;
            default:
                // UpdateBloodandScore_LimbShot(bullet);
                break;
        }
    }
    public void CompleteAnyAnim(int argCombatanim)
    {

    }
    #endregion

    #region Private Methods
    void Update_bodyPartShot_nonLEthalPoints(Bullet bullet, int PointsNonLethal)
    {
        int ExraLethalPoins = 0;
        int newHP =0;
        newHP -= bullet.damage;
        if (newHP < 0)
        {
            newHP = 0;
            ExraLethalPoins = PointsNonLethal;
        }
        m_ieec.Set_HP(bullet, PointsNonLethal + ExraLethalPoins, newHP);
    }
#endregion
}
