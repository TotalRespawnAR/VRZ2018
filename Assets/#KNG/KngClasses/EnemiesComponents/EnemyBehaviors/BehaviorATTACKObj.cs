////#define  ENABLE_DEBUGLOG
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MODE_ATTACK : IEnemyBehaviorObj, IDisposable
{
#region PRivate Vars
    IEnemyEntityComp m_ieec;
    ModesEnum _modeenum;
    float MaxTimePAss = 3f;
    float CurTime = 0f;
    int _headShot_NonLethal = 50; //used to be 80
    int _bodyShot_NonLethal = 15;
    int _limbShot_NonLethal = 5;
    #endregion

#region Construct Destructor
    public MODE_ATTACK(IEnemyEntityComp argIbhc)
    {
        _modeenum = ModesEnum.ATTACK;
        m_ieec = argIbhc;
      
    }
    ~MODE_ATTACK()
    {
        Debug.Log("Destructor" + _modeenum.ToString());
        Dispose();
    }
#endregion
#region IMode and Idisosable
    public void Dispose()
    {
        Debug.Log("disposing BEhAttackObj");
        GC.SuppressFinalize(this);
    }

  
    public void StartBehavior()
    {
        throw new NotImplementedException();
    }
    public void RunBehavior()
    {

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
                //Debug.Log("chest");
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
    #endregion
    public void CompleteAnyAnim(int argCombatanim)
    {

    }
    #region Private Methods
    void Update_bodyPartShot_nonLEthalPoints(Bullet bullet, int PointsNonLethal)
    {
        int ExraLethalPoins = 0;
        int newHP = m_ieec.Get_HP(); ;
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
