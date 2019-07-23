using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeGuyDamage : MonoBehaviour  
{
    /*
    public int PublicDamageHP;
    private int _computedHEadshotValue = 10;
    private int _hp;
    bool _beenHitOnce = false;
    public int OriginalHP;
    public AxeEnemyBehavior _Abeh;
    IZeffects _zeffcts;

    public int MyHP
    {
        get { return _hp; }
        set
        {
            _hp = value;
           // _ZBEH.Call_ZombieHpChanged(_hp);
        }
    }

    int _headShot_Lethal = 0;    //used to be 100
    int _headShot_NonLethal = 0; //used to be 80
    int _bodyShot_Lethal = 30;
    int _bodyShot_NonLethal = 5;
    int _limbShot_Lethal = 20;
    int _limbShot_NonLethal = 5;
    public int HEADSHOTCOUNT_TODEATH;

    public bool BeenHitOnce()
    {
        return _beenHitOnce;
    }

    public void DoBurnMe()
    {
        throw new System.NotImplementedException();
    }

    public void Fake_TakeSelfHit_fromZbehavior()
    {
        throw new System.NotImplementedException();
    }

    public void Kill()
    {
        if (_Abeh.AxeState0Idle1Walk2Run3DeadTrigThrow5Pause5 == 3) return;

         
            _Abeh.UpdateCurAnimState(3);
             
          
        

        _Abeh.Call_ZombieDied();//----> cals global evnet zombie ded 
        //FindAllMaterials_Filter_andPrint();
         _zeffcts.HideBar();

    }

    public void Rgister_HeadShot(Bullet bullet)
    {
        if (bullet == null) return;
        if (_zeffcts != null)
        {
            if (!GameSettings.Instance.IsIsGodModeON)
            {
                _zeffcts.Boold_On_Head(bullet);
            }
            else
            {
                _zeffcts.Blood_On_HEadCheat(_Abeh.GetZombieHeadTrans());
            }
        }
        PlayHeadShotSound.Instance.PlaySplatSound("_Splat");

        ComputeHEadShot_for_axeguy(bullet);
       
    }
    void ComputeHEadShot_for_axeguy(Bullet bullet)
    {

        MyHP -= _computedHEadshotValue;

        //Debug.Log("head damage hp= " + MyHP);
        AddSCorePoints(_headShot_Lethal);
        Add_HeadshotKillCNT();

        if (MyHP <= 2)
        {
            Kill();
        }
       


        // Kill();
        // _ZBEH.Flip_Just_got_HeadShotted();
    }
    public void SetHP(int value)
    {
        MyHP = value; //to shrtcut the event . we don t need to broadcast an HP change when we init a zombie
        OriginalHP = value;
        _zeffcts.SetHEalthBarHealthPoints(value);
    }

    public void SetHP(int value, int argAxeGuyHeadShots)
    {
        MyHP = value; //to shrtcut the event . we don t need to broadcast an HP change when we init a zombie
        OriginalHP = value;
        _zeffcts.SetHEalthBarHealthPoints(value);
    }

    public void TakeHit_fromZbehavior(Bullet bullet)
    {
        if (!_beenHitOnce) { _beenHitOnce = true; }

        
        if (bullet.MyType == GunType.HELL)
        {
            DoBurnMe();
            return;
        }
        TakeHitWhileInWaveNotpregame(bullet);


        _zeffcts.UpdatedHp(MyHP, _beenHitOnce);
    }
    void TakeHitWhileInWaveNotpregame(Bullet argbullet) {
        string tag = argbullet.hitInfo.collider.gameObject.tag;

        switch (tag)
        {
            case "ZHeadTag":
               // Debug.Log("head");
                Rgister_HeadShot(argbullet);
                break;
            case "ZChestTag":
               // Debug.Log("chest");
                UpdateBloodandScore_TorsoShot(argbullet);
                break;
            case "ZPelvisTag":
               // Debug.Log("hips");
                UpdateBloodandScore_PelvisShot(argbullet);
                break;
            case "ZArmTag":
               // Debug.Log("arm");
                UpdateBloodandScore_LimbShot(argbullet);
                break;
            case "ZLegTag":
              //  Debug.Log("leg");
                UpdateBloodandScore_LimbShot(argbullet);
                break;
            default:
                UpdateBloodandScore_LimbShot(argbullet);
                break;
        }
    }

    void UpdateBloodandScore_PelvisShot(Bullet bullet)
    {
        if (bullet == null) return;

        // instantiate blood effect
        if (_zeffcts != null)
            _zeffcts.Boold_On_Pelvis(bullet);

        // take torso damage
        MyHP -= bullet.damage;
        PlayHeadShotSound.Instance.PlaySplatSound("_BodySplat");
        if (MyHP <= 0)
        {
            AddSCorePoints(_bodyShot_Lethal);
            Add_TorssoshotKillCNT();
            Kill();
        }
        else
        {
      
            AddSCorePoints(_bodyShot_NonLethal);
            //_zStateAnim_needed_forTrigHEadsot.Trigger_HitGUtAnim();

            //  SendBulletTORaggdoll(bullet);
        }
    }
    void UpdateBloodandScore_TorsoShot(Bullet bullet)
    {
        if (bullet == null) return;

        // instantiate blood effect
        if (_zeffcts != null)
            _zeffcts.Boold_On_Torso(bullet);
       
        // take torso damage
        MyHP -= bullet.damage;
        PlayHeadShotSound.Instance.PlaySplatSound("_BodySplat");
        if (MyHP <= 0)
        {
            AddSCorePoints(_bodyShot_Lethal);
            Add_TorssoshotKillCNT();
            Kill();
        }
        else
        {
          
            AddSCorePoints(_bodyShot_NonLethal);
//            _zStateAnim_needed_forTrigHEadsot.Trigger_HitLeftRightAnim();

        }
    }

    void UpdateBloodandScore_LimbShot(Bullet bullet)
    {
        if (bullet == null) return;

        // instantiate blood effect
         if (_zeffcts != null)
            _zeffcts.Boold_On_Limb(bullet);

        // take limb damage
        MyHP -= Mathf.CeilToInt(bullet.damage * 0.5f);
        PlayHeadShotSound.Instance.PlaySplatSound("_LimbSplat");
        if (MyHP <= 0)
        {
            Kill();
            AddSCorePoints(_limbShot_Lethal);
            Add_LimbhotKillCNT();
        }
        else
        {
        
            AddSCorePoints(_limbShot_NonLethal);
           // _zStateAnim_needed_forTrigHEadsot.Trigger_HitLeftRightAnim();


            // SendBulletTORaggdoll(bullet); to add force to ragdoll
        }
    }

    void AddSCorePoints(int argZpoints)
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.GetScoreMAnager().Update_Add_PointsTotal(argZpoints);
            GameManager.Instance.GetScoreMAnager().Update_Add_PointsCurWave(argZpoints);
        }
        else { Logger.Debug("no Gm"); }
    }



    void Add_HeadshotKillCNT()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.GetScoreMAnager().Update_headShotKillsCNT();
        }
        else { Logger.Debug("no Gm"); }
    }

    void Add_TorssoshotKillCNT()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.GetScoreMAnager().Update_torssoShotKillsCNT();

        }
        else { Logger.Debug("no Gm"); }
    }

    void Add_LimbhotKillCNT()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.GetScoreMAnager().Update_limbShotKillsCNT();
        }
        else { Logger.Debug("no Gm"); }
    }

    // Use this for initialization
    void Awake () {
        _zeffcts = GetComponent<IZeffects>();
        _Abeh = GetComponent<AxeEnemyBehavior>();
    }
    */
 
}
