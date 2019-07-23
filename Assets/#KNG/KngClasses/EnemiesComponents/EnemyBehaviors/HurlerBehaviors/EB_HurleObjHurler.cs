////#define ENABLE_DEBUGLOG
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EB_HurleObjHurler : IEnemyBehaviorObj, IDisposable
{
    #region Private vars
    IEnemyEntityComp m_ieec;
    IEnemyMoverComp m_iMover;
    ModesEnum _modeenum;
    KNode _CurKnode;
    Rigidbody RBmagnet;
    #endregion

    #region Constructor Destructor
    public EB_HurleObjHurler(IEnemyEntityComp argIbhc)
    {
        _modeenum = ModesEnum.HURLEOBJ;
#if ENABLE_DEBUGLOG
        Debug.Log("Constructor "+ _modeenum.ToString());
#endif
        m_ieec = argIbhc;
        m_iMover = argIbhc.Get_Mover();
       // _CurKnode = argIbhc.Get_CurAndOnlyKnode();
        
        StartBehavior();

    }

    ~EB_HurleObjHurler()
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
        Debug.Log("Dispose()" + _modeenum.ToString());
#endif
        GC.SuppressFinalize(this);
    }
#region Interface


    public void StartBehavior()
    {
        //  bool boolval = false;
#if ENABLE_DEBUGLOG
#endif

        // m_iMover.setAnimTrig(TriggersEnemyAnimator.trigPickUpP2);
        Debug.Log("IMPLEMENTME");
        m_ieec.RunHAndUNtargetting();

    }
    public void RunBehavior()
    {
        m_ieec.PredatorDoPrimitiveFollowHand();
        m_ieec.SetTargPos(KnodeProvider.Instance.GEt_PlayerAlphaBravo().GetPos());
        m_iMover.RunRotateTOTarg();
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

    }
    public void CompleteAnyAnim(int argCombatanim)
    {

    }

    #endregion

    #region Private Methods

    #endregion
}

 