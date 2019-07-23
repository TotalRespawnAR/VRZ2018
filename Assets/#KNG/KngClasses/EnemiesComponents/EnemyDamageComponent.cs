//#define  ENABLE_DEBUGLOG
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageComponent : MonoBehaviour, IEnemyDamageComp
{
#region Private vars
    IEnemyEntityComp m_ieec;
    bool hitonce = false;
#endregion
#region MonoMethods
    void Awake()
    {
        m_ieec = GetComponent<IEnemyEntityComp>();
    }
    void OnEnable()
    {
    }
    void OnDisable()
    {

    }

    void Start()
    {
#if ENABLE_DEBUGLOG
        if (m_ieec == null) { Debug.LogError("Damagecomp Didnot find IEBC!!!" + gameObject.name); }
#endif
    }
    //Event Handeler ONLY! EnemyBehavior will update this
    void OnCheckIfListenerWorks(object o, ArgsEnemyMode e)
    {
#if ENABLE_DEBUGLOG
        Debug.Log("damage: heard modeChange to ");
#endif 
        if (e.Mode == ModesEnum.DEAD)
        {
#if ENABLE_DEBUGLOG
            Debug.Log(" dead");
#endif
        }
    }
#endregion
 #region Intertface
    public void TakeHit(Bullet argBullet)
    {
        if (argBullet == null) {
#if ENABLE_DEBUGLOG
#endif
            Debug.LogError("nullbullet on " + gameObject.name);
        }
        if (m_ieec.Get_Cur_EB_Mode() == null ) {
            Debug.LogError("null EB_ on " + gameObject.name);
        }else
        m_ieec.Get_Cur_EB_Mode().ReadBullet(argBullet);
    }

    public void TakeHits(Bullet[] bullets)
    {
        throw new System.NotImplementedException();
    }
    #endregion

}
