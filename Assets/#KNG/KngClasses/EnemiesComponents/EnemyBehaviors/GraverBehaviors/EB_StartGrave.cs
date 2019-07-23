////#define  ENABLE_DEBUGLOG
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EB_StartGrave : IEnemyBehaviorObj, IDisposable
{
    IEnemyEntityComp m_ieec;
    IEnemyMoverComp m_Imover;
    ModesEnum _modeenum;
    int _headShot_NonLethal = 50; //used to be 80
    int _bodyShot_NonLethal = 15;
    int _limbShot_NonLethal = 5;
    public EB_StartGrave(IEnemyEntityComp argIentity)
    {
        _modeenum = ModesEnum.STARTGRAVE;
#if ENABLE_DEBUGLOG
        Debug.Log("Construct" + _modeenum.ToString());
#endif
        m_ieec = argIentity;
        m_Imover = argIentity.Get_Mover();
        StartBehavior();
    }

    ~EB_StartGrave()
    {
#if ENABLE_DEBUGLOG
        Debug.Log("Destructor" + _modeenum.ToString());
#endif
        Dispose();
    }

    public void Dispose()
    {
#if ENABLE_DEBUGLOG
        Debug.Log("disposing STartGRAVE");
#endif
        GC.SuppressFinalize(this);
    }


    #region Interface

    public void StartBehavior()
    {

    }

    public void RunBehavior()
    {
 
    }

    public void EndBehavior( )
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
        int newHP = m_ieec.Get_HP();
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
