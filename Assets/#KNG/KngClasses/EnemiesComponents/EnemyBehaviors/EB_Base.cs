//#define  ENABLE_DEBUGLOG
using System;
using UnityEngine;

public class EB_Base : IEnemyBehaviorObj, IDisposable
{
    IEnemyEntityComp m_ieec;
    IEnemyMoverComp m_imover;
    ModesEnum _modeenum;
    Action _startAction;
    float CurTime = 0f;
    float MaxtimeForthisEB;
    int _headShot_NonLethal = 50; //used to be 80
    int _bodyShot_NonLethal = 5;
    int _limbShot_NonLethal = 1;
    bool damageOn;


    public EB_Base(IEnemyEntityComp argIentity, ModesEnum argmodeenum, float argMAxtime, Action argStartAction, bool argDamageOn)
    {
        MaxtimeForthisEB = argMAxtime;
        _modeenum = argmodeenum;
        _startAction = argStartAction;

        m_ieec = argIentity;
        m_imover = argIentity.Get_Mover();
        damageOn = argDamageOn;
#if ENABLE_DEBUGLOG
        // Debug.Log("eb_base Construct" + _modeenum.ToString());
#endif

        //if (_startAction != null)
        //    _startAction();
        //else Debug.Log("  action null");

    }

    ~EB_Base()
    {
#if ENABLE_DEBUGLOG
        // Debug.Log("eb_base Destructor " + _modeenum.ToString());
#endif
        Dispose();
    }

    public virtual void StartBehavior()
    {
        //Debug.Log("eb_base startbeh override me plz");
        //if(_startAction !=null)
        //_startAction();
        //else Debug.Log("  action null");
    }

    public virtual void RunBehavior()
    {
        // Debug.Log("override me ");
    }

    public virtual void EndBehavior()
    {
        // Debug.LogError("eb_base end this");
    }

    public ModesEnum GET_MyModeEnum()
    {
        return this._modeenum;
    }


    public void Dispose()
    {
#if ENABLE_DEBUGLOG
        //  Debug.Log("eb_base ~ 12?");
#endif
        GC.SuppressFinalize(this);
    }



    public virtual void CompleteAnyAnim(int argCombatanim)
    {
    }



    public virtual void ReadBullet(Bullet abulletfromDamageBehComp)
    {

        //  Debug.Log(" bullet  " + abulletfromDamageBehComp.MyType.ToString() + " " + abulletfromDamageBehComp.damage);
        if (!damageOn)
        {
            Debug.Log("sorry no damage here ");
            return;
        }
        // int d = abulletfromDamageBehComp.damage;
        switch (abulletfromDamageBehComp.BulletPointsType)
        {
            case BulletPointsType.Head:
#if ENABLE_DEBUGLOG
                //  Debug.Log("ebbase_hithed");
#endif
                //if (abulletfromDamageBehComp.HitBoss)
                //{
                //    //regular bullet damage
                //    // Update_bodyPartShot_nonLEthalPoints(abulletfromDamageBehComp, _headShot_NonLethal, 69);
                //    abulletfromDamageBehComp.damage += (d * 1);
                //}
                //else
                //{
                //    abulletfromDamageBehComp.damage += (d * 1);
                //}

                Update_bodyPartShot_nonLEthalPoints(abulletfromDamageBehComp, _headShot_NonLethal, 69);



                if (abulletfromDamageBehComp.hitInfo.point.y < m_ieec.Get_MyHEADtrans().position.y)
                {
                    if (abulletfromDamageBehComp.hitInfo.point.x < m_ieec.Get_MyHEADtrans().position.x)
                    {
                        m_ieec.Get_Animer().DO_HEADSHOT_R_Anim();
#if ENABLE_DEBUGLOG
                        Debug.Log("REACT head L");
#endif
                    }
                    else
                    {

                        m_ieec.Get_Animer().DO_HEADSHOT_L_Anim();
#if ENABLE_DEBUGLOG
                        Debug.Log("REACT head R");
#endif
                    }

                }
                else
                {
                    m_ieec.Get_Animer().DO_HEADSHOT_Anim();
#if ENABLE_DEBUGLOG
                    Debug.Log("REACT HEAD");
#endif
                }




                break;
            case BulletPointsType.Torso:
#if ENABLE_DEBUGLOG
                Debug.Log("hittorso");
#endif

                //if (abulletfromDamageBehComp.HitBoss)
                //{

                //    abulletfromDamageBehComp.damage += (d );
                //}
                //else
                //{

                //    abulletfromDamageBehComp.damage += (d );
                //}

                Update_bodyPartShot_nonLEthalPoints(abulletfromDamageBehComp, _bodyShot_NonLethal, 2);





                //                if (abulletfromDamageBehComp.hitInfo.point.z < m_ieec.Get_MyCHESTtrans().position.z)
                //                {
                //                    if (abulletfromDamageBehComp.hitInfo.point.x < m_ieec.Get_MyCHESTtrans().position.x)
                //                    {
                //                        m_ieec.Get_Animer().Do_GUTSHOT_Anim();
                //#if ENABLE_DEBUGLOG
                //                        Debug.Log("REACT chest L");
                //#endif
                //                    }
                //                    else
                //                    {

                //                        m_ieec.Get_Animer().DO_GUTSHOT_R_Anim();
                //#if ENABLE_DEBUGLOG
                //                        Debug.Log("REACT chest R");
                //#endif
                //                    }

                //                }
                //                else
                //                {
                //                    m_ieec.Get_Animer().DO_GUTSHOT_L_Anim();
                //#if ENABLE_DEBUGLOG
                //                    Debug.Log("REACT chest");
                //#endif
                //                }



                if (abulletfromDamageBehComp.hitInfo.point.x < m_ieec.Get_MyCHESTtrans().position.x)
                {

#if ENABLE_DEBUGLOG
                        Debug.Log("REACT chest L");
#endif
                    m_ieec.Get_Animer().DO_GUTSHOT_R_Anim();
#if ENABLE_DEBUGLOG
                        Debug.Log("REACT chest R");
#endif
                }
                else
                {
                    m_ieec.Get_Animer().DO_GUTSHOT_L_Anim();
#if ENABLE_DEBUGLOG
                    Debug.Log("REACT chest");
#endif
                }




                break;
            case BulletPointsType.Hips:
                //if (abulletfromDamageBehComp.HitBoss)
                //{

                //    abulletfromDamageBehComp.damage += (d);
                //}
                //else
                //{

                //    abulletfromDamageBehComp.damage += (d * 2);
                //}

                Update_bodyPartShot_nonLEthalPoints(abulletfromDamageBehComp, _bodyShot_NonLethal, 2);
                m_ieec.Get_Animer().Do_GUTSHOT_Anim();
#if ENABLE_DEBUGLOG
              
#endif
                //  Debug.Log("REACT hips");



                break;
            case BulletPointsType.Limbs:
                if (abulletfromDamageBehComp.HitBoss)
                {

                    abulletfromDamageBehComp.damage = 1;
                }
                else
                {
                    abulletfromDamageBehComp.damage = 1;
                    //abulletfromDamageBehComp.damage += (d * 2);
                }

                //if (abulletfromDamageBehComp.HitLegs) {


                //}

                Update_bodyPartShot_nonLEthalPoints(abulletfromDamageBehComp, _limbShot_NonLethal, 1);


                break;
            default:
                // UpdateBloodandScore_LimbShot(bullet);
                break;
        }
    }

    #region Private Methods
    void Update_bodyPartShot_nonLEthalPoints(Bullet bullet, int PointsNonLethal, int damageMultiplyer)
    {
        int ExraLethalPoins = 0;
        int newHP = m_ieec.Get_HP();


        if (m_ieec.GetMyType() == ARZombieypes.TankFighter)
        {

            if (m_ieec.Get_Animer().iGet_BlockingBool())
            {

                if (bullet.BulletPointsType == BulletPointsType.Head)
                {
                    newHP -= (bullet.damage) / 2;

#if ENABLE_DEBUGLOG
                    Debug.Log("BOS " + (bullet.damage).ToString() + " H " + ((bullet.damage) / 2).ToString() + " damge");
#endif
                } //should be doing 1/20 original hp
                else
                {
                    newHP -= (bullet.damage / 20);
#if ENABLE_DEBUGLOG
                    Debug.Log("BOS " + (bullet.damage).ToString() + " B" + ((bullet.damage) / 20).ToString() + " damge");
#endif

                    PlayHeadShotSound.Instance.PlaySound_2d_MetalDing();
                }
            }
            else
            {


                if (bullet.BulletPointsType == BulletPointsType.Head)
                {
                    newHP -= (bullet.damage / 12);
#if ENABLE_DEBUGLOG
                    Debug.Log("BOS " + (bullet.damage).ToString() + " h " + ((bullet.damage) / 10).ToString() + " damge");
#endif

                } //should be doing 1/10 original hp
                else
                {
                    newHP -= (bullet.damage / 4);
                    PlayHeadShotSound.Instance.PlaySound_2d_MetalDing();
#if ENABLE_DEBUGLOG
                    Debug.Log("BOS " + (bullet.damage).ToString() + " b " + ((bullet.damage) / 4).ToString() + " damge");
#endif

                }

            }

        }
        else
        {


            if (m_ieec.Get_Animer().iGet_BlockingBool())
            {

                if (bullet.BulletPointsType == BulletPointsType.Head)
                {
                    newHP -= (bullet.damage / 2);
#if ENABLE_DEBUGLOG
                    Debug.Log("std " + (bullet.damage).ToString() + " H " + ((bullet.damage) / 2).ToString() + " damge");
#endif

                } //should be doing 1/10 original hp
                else
                {
                    newHP -= (bullet.damage / 4);
                    //PlayHeadShotSound.Instance.PlaySound_2d_MetalDing();
#if ENABLE_DEBUGLOG
                    Debug.Log("std " + (bullet.damage).ToString() + " B " + ((bullet.damage) / 4).ToString() + " damge");
#endif
                }
            }
            else
            {
                if (bullet.BulletPointsType == BulletPointsType.Head)
                {
                    newHP -= (bullet.damage);
#if ENABLE_DEBUGLOG
                    Debug.Log("std " + (bullet.damage).ToString() + " h " + ((bullet.damage)).ToString() + " damge");
#endif
                } //should be doing 1/10 original hp
                else
                {
                    newHP -= (bullet.damage);
                    //PlayHeadShotSound.Instance.PlaySound_2d_MetalDing();
#if ENABLE_DEBUGLOG
                    Debug.Log("std  " + (bullet.damage).ToString() + " b " + ((bullet.damage)).ToString() + " damge");
#endif

                }
            }








            //newHP -= bullet.damage;
            //Debug.Log("NOT TANK original bullet damage " + (bullet.damage).ToString());
        }


        if (newHP < 0)
        {
            newHP = 0;
            ExraLethalPoins = PointsNonLethal;

            if (bullet.BulletPointsType == BulletPointsType.Head) { PlayHeadShotSound.Instance.PlaySound_2d_HeadShot(); }

        }
        else
        {

            if (bullet.BulletPointsType == BulletPointsType.Head) { PlayHeadShotSound.Instance.PlaySound_2d_HEadShotNonLethalh(); }
        }

        m_ieec.Set_HP(bullet, PointsNonLethal + ExraLethalPoins, newHP);
    }
    #endregion
    public void RunTimer()
    {
        CurTime += Time.deltaTime;
        if (CurTime > MaxtimeForthisEB)
        {
            m_ieec.Trigger_EndBEhaviorTASK(EnemyTaskEneum.EndFirstAnimation);
        }
    }
}
