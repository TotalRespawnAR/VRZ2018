// @Author Nabil Lamriben ©2018
////#define ENABLE_DEBUGLOG
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ZombieDamage : MonoBehaviour 
{
    /*

    #region PublicVars
    // colt bullet  -> 50 damage
    // m1911 bullet -> 25 damage
    // mac11 bullet -> 25 damage
    //but we should have static hitpoints OFF by default , so this will not matter
    //wave 1 zombie hp -> 20  - 40 
    //wave 2 zombie hp -> 80  - 120
    //wave 3 zombie hp -> 100 - 160
    #endregion

    #region PrivateVars
    public int PublicDamageHP;
    private int _computedHEadshotValue = 0;
    private int _hp;
    bool _beenHitOnce = false;
    public int OriginalHP;

    public bool BeenHitOnce() { return _beenHitOnce; }

    public int MyHP
    {
        get { return _hp; }
        set
        {
            _hp = value;
            _ZBEH.Call_ZombieHpChanged(_hp);
        }
    }

    int _headShot_Lethal = 0;    //used to be 100
    int _headShot_NonLethal = 0; //used to be 80
    int _bodyShot_Lethal = 30;
    int _bodyShot_NonLethal = 5;
    int _limbShot_Lethal = 20;
    int _limbShot_NonLethal = 5;


    public int HEADSHOTCOUNT_TODEATH;
    // the array of headshot sounds
    //  private AudioClip[] headShotClips;
    // the audiosource
    ////private AudioSource aS;
    #endregion

    #region Dependencies
    ZombieAnimState _zStateAnim_needed_forTrigHEadsot;
    ZombieEffects _zEffects;

    ZombieBehavior _ZBEH;
    ZombieCollisionCTRL _ZCOLLI;
    #endregion

    #region INIT

    void Awake()
    {
        _ZBEH = GetComponent<ZombieBehavior>();
        _ZCOLLI = GetComponent<ZombieCollisionCTRL>();
        // ZombieInfo = GetComponentInChildren<TextMesh>();
        _zStateAnim_needed_forTrigHEadsot = GetComponent<ZombieAnimState>();
        _zEffects = GetComponent<ZombieEffects>();
        // MyHP = 20;

        if (_ZBEH.IsBossLevel_1) { HEADSHOTCOUNT_TODEATH = 5;
           
        }
        else
            if (_ZBEH.IsBossLevel_2) { HEADSHOTCOUNT_TODEATH = 10; }
        else if (_ZBEH.IsBossLevel_3) { HEADSHOTCOUNT_TODEATH = 50; }
        else
        {
            HEADSHOTCOUNT_TODEATH = 0;
        }

        if (GameSettings.Instance.IsKidsModeOn) {
            if (HEADSHOTCOUNT_TODEATH > 0) {
                HEADSHOTCOUNT_TODEATH--;
            }
        }
    }


    #endregion
    #region PublicMethods
    public void SetHP(int value)
    {
        
        MyHP = value; //to shrtcut the event . we don t need to broadcast an HP change when we init a zombie
        OriginalHP = value;
        _zEffects.SetHEalthBarHealthPoints(value);
        _computedHEadshotValue = MyHP / 3;

        if (GameSettings.Instance.IsKidsModeOn) {
            _computedHEadshotValue = MyHP / 2;
        }
    }


    public void SetHP(int value, int BossHeadShots)
    {
        MyHP = value; //to shrtcut the event . we don t need to broadcast an HP change when we init a zombie
        OriginalHP = value;
        _zEffects.SetHEalthBarHealthPoints(value, BossHeadShots);
    }

    public void SendBulletTORaggdoll(Bullet bullet)
    {
        ZombieRagDoll zrg = GetComponent<ZombieRagDoll>();
        if (zrg != null)
        {
            if (bullet.hitInfo.rigidbody != null)
            {
                zrg.PUSH_DOLL(bullet, bullet.hitInfo.rigidbody);
            }
        }
        else
        {
            Logger.Debug("we hit a zombie but not ragdoll");
        }
    }


    public void TakeHit_fromZbehavior(Bullet bullet)
    {
        if (!_beenHitOnce) { _beenHitOnce = true; }

  //      if (_ZBEH.CurZombieState == EBSTATE.BURNING) { return; }
        if (bullet.MyType == GunType.HELL)
        {
            DoBurnMe();
            return;
        }
        TakeHitWhileInWaveNotpregame(bullet);

        //if (_ZBEH.IsBossLevel_1 || _ZBEH.IsBossLevel_2 || _ZBEH.IsBossLevel_3)
        //{
        //    _zEffects.UpdatedHp(HEADSHOTCOUNT_TODEATH);
        //}else
        //_zEffects.UpdatedHp(MyHP);
       
        _zEffects.UpdatedHp(MyHP, _beenHitOnce);
    }
    public void DoBurnMe() {
        if ( !_ZBEH.IsBossLevel_1 && !_ZBEH.IsBossLevel_2 && !_ZBEH.IsBossLevel_3){
            //_ZBEH.CurZombieState = EBSTATE.BURNING;
            _zEffects.SetFire();
            StartCoroutine(WaitToFinishBurning());
        }
    }

    IEnumerator WaitToFinishBurning()
    {
        yield return new WaitForSeconds(10);
        Kill();
    }


    #region FakeReactionNoBullet
    public void Fake_TakeSelfHit_fromZbehavior() {
       // if (_ZBEH.CurZombieState == EBSTATE.BURNING) { return; } //it should never be burning but what ever
        Fake_TakeHitWhileInWaveNotpregame();
    }

    void Fake_TakeHitWhileInWaveNotpregame() {
     
        if (_ZBEH.CurZombieState == EBSTATE.EBDEAD_4)
            return;
         FakeRgister_HeadShot();
    }
    void FakeRgister_HeadShot() {
        if (_zEffects != null)
        {
            // WEll, the only time i can be here is by having GodModeOn
            //if (!GameSettings.Instance.IsIsGodModeON){_zEffects.Boold_On_Head(bullet);}
            //else
            //{
                _zEffects.Blood_On_HEadCheat(_ZBEH.GetZombieHeadTrans());
          //  }
        }
        PlayHeadShotSound.Instance.PlaySplatSound("_Splat");

        Fake_TakeHeadDamage_computedamageForRegularORboss();
    }

    void Fake_TakeHeadDamage_computedamageForRegularORboss() {

        //HEADSHOTCOUNT_TODEATH--;

        if (_ZBEH.IsBossLevel_1 || _ZBEH.IsBossLevel_2 || _ZBEH.IsBossLevel_3)
        {
            Fake_Compute_for_Boss();
        }
        else
        {
            Fake_Compute_for_Regular();

        }
    }
    void Fake_Compute_for_Boss()
    {
        HEADSHOTCOUNT_TODEATH--;
        if (HEADSHOTCOUNT_TODEATH <= 0)
        {
            MyHP = -1;
            Kill();
        }
        else
        {
            if (_zStateAnim_needed_forTrigHEadsot != null)
            {
                _zStateAnim_needed_forTrigHEadsot.Trigger_HeadShotAnim();
            }
            else
            {
                Debug.LogWarning("YO No animator on this BOSS" + _ZBEH.GetZombieID() + " " + gameObject.name);

            }
            AddSCorePoints(_headShot_NonLethal);
        }
    }
    void Fake_Compute_for_Regular()
    {

        MyHP = -1;
        AddSCorePoints(_headShot_Lethal);
        Add_HeadshotKillCNT();
        Kill();
    }
    #endregion

    void TakeHitWhileInWaveNotpregame(Bullet bullet)
    {
        if (_ZBEH.CurZombieState == EBSTATE.EBDEAD_4)
            return;


        string tag = bullet.hitInfo.collider.gameObject.tag;

        switch (tag)
        {
            case "ZHeadTag":
              //  Debug.Log("head");
                Rgister_HeadShot(bullet);
                break;
            case "ZChestTag":
                //Debug.Log("chest");
                UpdateBloodandScore_TorsoShot(bullet);
                break;
            case "ZPelvisTag":
               // Debug.Log("hips");
                UpdateBloodandScore_PelvisShot(bullet);
                break;
            case "ZArmTag":
                //Debug.Log("arm");
                UpdateBloodandScore_LimbShot(bullet);
                break;
            case "ZLegTag":
                //Debug.Log("leg");
                UpdateBloodandScore_LimbShot(bullet);
                break;
            default:
                UpdateBloodandScore_LimbShot(bullet);
                break;
        }
    }

    public void Kill()
    {

      //  if (_ZBEH.CurZombieState == EBSTATE.DEAD) return;
//
    //    if (_ZBEH.CurZombieState != EBSTATE.BURNING)
      //  {
           // _ZBEH.CurZombieState = EBSTATE.DEAD;
            // stop attacking
            if (_zStateAnim_needed_forTrigHEadsot != null)
            {
                if (_ZBEH.CurZombieState == EBSTATE.REACHING__8)
                {
                    _zStateAnim_needed_forTrigHEadsot.StopAttackTimer();
                }

            }
            else
            {
                Debug.LogWarning("YO No animator on this" + _ZBEH.GetZombieID() + " " + gameObject.name);

            }
      //  }
       //
        _ZBEH.Call_ZombieDied();//----> cals global evnet zombie ded 
        //FindAllMaterials_Filter_andPrint();
        _zEffects.HideBar();

    }

    ////string[] MaterialNamesToExcludeFromCount = new string[] { "", "", "" };
    ////bool CheckIsNotEditorMaterial(string argObjName)
    ////{
    ////    if (argObjName == "SH" || argObjName == "InactiveGUI" || argObjName == "LightProbeLines")
    ////        return false;
    ////    if (argObjName.Contains("Hidden") ||
    ////        argObjName.Contains("FrameDebugger") ||
    ////        argObjName.Contains("Default") ||
    ////        argObjName.Contains("andle") ||
    ////        argObjName.Contains("Grid") ||
    ////        argObjName.Contains("PreviewEncodedNormal") ||
    ////        argObjName.Contains("SceneViewGrayscale") ||
    ////        argObjName.Contains("ont Materia"))
    ////        return false;
    ////    return true;
    ////}
    ////void FindAllMaterials_Filter_andPrint()
    ////{
    ////    Object[] AllObjectsMaterials = Resources.FindObjectsOfTypeAll(typeof(Material));
    ////    //  print("Materials " + AllObjectsMaterials.Length);
    ////    List<Object> RelevantObjectMaterials = new List<Object>();
    ////    for (int x = 0; x < AllObjectsMaterials.Length; x++)
    ////    {
    ////        if (CheckIsNotEditorMaterial(AllObjectsMaterials[x].name))
    ////        {
    ////            RelevantObjectMaterials.Add(AllObjectsMaterials[x]);
    ////        }
    ////    }
    ////   print("Relevant Mats " + RelevantObjectMaterials.Count);

    //// }

    #endregion

    #region PrivateMethods
    void TakeHeadDamage_computedamageForRegularORboss(Bullet bullet)
    {

        //HEADSHOTCOUNT_TODEATH--;

        ComputeHEadShot_for_RegularAndBoss(bullet);
        return;

        if (_ZBEH.IsBossLevel_1 || _ZBEH.IsBossLevel_2 || _ZBEH.IsBossLevel_3)
        {
            Compute_for_Boss(bullet);
        }
        else
        {
            Compute_for_Regular(bullet);

        }

    }


    void ComputeHEadShot_for_RegularAndBoss(Bullet bullet)
    {

        MyHP -= _computedHEadshotValue;
        AddSCorePoints(_headShot_Lethal);
        Add_HeadshotKillCNT();




        if (MyHP <= 2)
        {
            Kill();
        }
        else
        {

            if (_zStateAnim_needed_forTrigHEadsot != null)
            {
                _zStateAnim_needed_forTrigHEadsot.Trigger_HeadShotAnim();
            }
            else
            {
                Debug.LogWarning("YO No animator on this BOSS" + _ZBEH.GetZombieID() + " " + gameObject.name);

            }
        }


        // Kill();
        // _ZBEH.Flip_Just_got_HeadShotted();
    }

        void Compute_for_Regular(Bullet bullet)
    {

        MyHP = -1;
        AddSCorePoints(_headShot_Lethal);
        Add_HeadshotKillCNT();
        Kill();

        // _ZBEH.Flip_Just_got_HeadShotted();
    }

    void Compute_for_Boss(Bullet bullet)
    {
        HEADSHOTCOUNT_TODEATH--;
        if (HEADSHOTCOUNT_TODEATH <= 0)
        {
            MyHP = -1;
            Kill();
        }
        else
        {
            if (_zStateAnim_needed_forTrigHEadsot != null)
            {
                _zStateAnim_needed_forTrigHEadsot.Trigger_HeadShotAnim();
            }
            else
            {
                Debug.LogWarning("YO No animator on this BOSS" + _ZBEH.GetZombieID() + " " + gameObject.name);

            }
            // _ZBEH.Flip_Just_got_HeadShotted();
            AddSCorePoints(_headShot_NonLethal);
            // SendBulletTORaggdoll(bullet);
        }
    }

    //for cheating from zombie beh
    public void Rgister_HeadShot(Bullet bullet)
    {
        if (bullet == null) return;
        if (_zEffects != null)
        {
            if (!GameSettings.Instance.IsIsGodModeON)
            {
                _zEffects.Boold_On_Head(bullet);
            }
            else
            {
                _zEffects.Blood_On_HEadCheat(_ZBEH.GetZombieHeadTrans());
            }
        }
        PlayHeadShotSound.Instance.PlaySplatSound("_Splat");

        TakeHeadDamage_computedamageForRegularORboss(bullet);

    }

    //  AnalyzetheHitTakenReacts(argBullet);
    // transform.root.GetComponent<Animator>().SetLayerWeight(1, 0.9f);
    //  GetComponent<Animator>().SetTrigger("trigHitLeft");

    
    void UpdateBloodandScore_PelvisShot(Bullet bullet)
    {
        if (bullet == null) return;

        // instantiate blood effect
        if (_zEffects != null)
            _zEffects.Boold_On_Pelvis(bullet);

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
            if (_ZBEH.IsBossLevel_1 || _ZBEH.IsBossLevel_2 || _ZBEH.IsBossLevel_3)
            {
                return;
            }
            AddSCorePoints(_bodyShot_NonLethal);
            _zStateAnim_needed_forTrigHEadsot.Trigger_HitGUtAnim();
            
            //  SendBulletTORaggdoll(bullet);
        }
    }
    void UpdateBloodandScore_TorsoShot(Bullet bullet)
    {
        if (bullet == null) return;

        // instantiate blood effect
        if (_zEffects != null)
            _zEffects.Boold_On_Torso(bullet);

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
            if (_ZBEH.IsBossLevel_1 || _ZBEH.IsBossLevel_2 || _ZBEH.IsBossLevel_3)
            {
                return;
            }
            AddSCorePoints(_bodyShot_NonLethal);
            _zStateAnim_needed_forTrigHEadsot.Trigger_HitLeftRightAnim();
        
            //  SendBulletTORaggdoll(bullet);
        }
    }

    void UpdateBloodandScore_LimbShot(Bullet bullet)
    {
        if (bullet == null) return;

        // instantiate blood effect
        if (_zEffects != null)
            _zEffects.Boold_On_Limb(bullet);

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
            if (_ZBEH.IsBossLevel_1 || _ZBEH.IsBossLevel_2 || _ZBEH.IsBossLevel_3)
            {
                return;
            }
            AddSCorePoints(_limbShot_NonLethal);
            _zStateAnim_needed_forTrigHEadsot.Trigger_HitLeftRightAnim();
          

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

    #endregion
    */

}
