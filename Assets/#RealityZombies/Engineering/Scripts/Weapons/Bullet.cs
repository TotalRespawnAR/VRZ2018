//#define ENABLE_DEBUGLOG
// @Author Jeffrey M. Paquette ©2016

using UnityEngine;

public class Bullet : MonoBehaviour
{

    public GunType MyType;
    buletcolor bc;
    public bool Is_CountsTowardsPoints = true;
    [Tooltip("Damage this bullet causes")]
    public int damage;
    public bool HitBoss;
    public bool HitLegs;
    public bool dudeisblocking;
    public bool hitenemy;
    [Tooltip("Raycast layer of all objects this bullet should collide with")]
    public LayerMask RaycastLayerMask = Physics.DefaultRaycastLayers;

    [Tooltip("The number of the enemy layer")]
    int TargetLAyer = 17;
    int enemyLayer = 9;
    int GridmapLayer = 8; //forwall
    int layer_mask;//= LayerMask.GetMask("Player");
    [Tooltip("Pickup layer")]
    public int pickupLayer;

    [HideInInspector]
    public RaycastHit hitInfo;

    private void Awake()
    {
        damage = GameSettings.Instance.GetBulletDamage(MyType);
    }

    // bullet vapor trail obj
    public GameObject bulletVaporTrail;

    BulletPointsType _bulletPointsType;

    // void Record_LINE_ZombieHit(Vector3 start, Vector3 end) { if (GameManager.Instance != null) { if (!GameManager.Instance.IsGameStarted()) { return; };  GameManager.Instance.AddZombieHitRelativeToPAthfinder(start, end); } else { Logger.Debug("sorry no gamemanager"); } }
    // void Record_LINE_Miss(Vector3 start, Vector3 end) { if (GameManager.Instance != null) { if (!GameManager.Instance.IsGameStarted()) { return; }; GameManager.Instance.Add_FailedShots_RelativeToPAthfinder(start, end); } }

    void BulletMissed()
    {
        if (!Is_CountsTowardsPoints)
        {
            return;
        }

        if (GameManager.Instance != null)
        {
            if (!GameManager.Instance.DidRzEperienceStartYet())
            {
                return;
            };
            GameManager.Instance.GetScoreMAnager().Update_IncrementBullets_Missed_ZombieCNT();
        }
        else
        {
            Logger.Debug("sorry no gamemanager");
        }
    }

    void BulletHitZombie()
    {
        bool test = Is_CountsTowardsPoints;
        if (!Is_CountsTowardsPoints)
        {
            return;
        }

        if (GameManager.Instance != null)
        {
            if (!GameManager.Instance.DidRzEperienceStartYet()) { return; };
            GameManager.Instance.GetScoreMAnager().Update_IncrementBullet_Hit_ZombieCNT();
        }
        else { Logger.Debug("sorry no gamemanager"); }
    }

    void BulletsFiredCountUp()
    {
        if (!Is_CountsTowardsPoints)
        {
            return;
        }

        if (GameManager.Instance != null)
        {
            if (!GameManager.Instance.DidRzEperienceStartYet())
            {
                return;
            };
            GameManager.Instance.GetScoreMAnager().Update_IncrementBulletsShotCNT();
            // Logger.Debug("bulletfired");
        }
        else
        {
            Logger.Debug("sorry no gamemanager");
        }
    }

    //void Bullet_HIts_ZHead()
    //{
    //    bool test = Is_CountsTowardsPoints;

    //    //if (!Is_CountsTowardsPoints) return;
    //    if (GameManager.Instance != null)
    //    {
    //        if (!GameManager.Instance.DidRzEperienceStartYet()) { return; };
    //        GameManager.Instance.GetScoreMAnager().Update_headShotCNT();
    //    }
    //    else { Logger.Debug("sorry no gamemanager"); }
    //}

    //void Bullet_HIts_ZTorso() { if (!Is_CountsTowardsPoints) return; if (GameManager.Instance != null) { if (!GameManager.Instance.DidRzEperienceStartYet()) { return; }; GameManager.Instance.GetScoreMAnager().Update_torsoShotCNT(); } else { Logger.Debug("sorry no gamemanager"); } }

    //void Bullet_HItsZlimb() { if (!Is_CountsTowardsPoints) return; if (GameManager.Instance != null) { if (!GameManager.Instance.DidRzEperienceStartYet()) { return; }; GameManager.Instance.GetScoreMAnager().Update_limbShotCNT(); } else { Logger.Debug("sorry no gamemanager"); } }

    void UpdateStreaks_Miss()
    {
        if (!Is_CountsTowardsPoints)
        {
            return;
        }

        if (GameManager.Instance != null)
        {
            GameManager.Instance.GetStreakManager().Set_StreakBreake();
        }
        else { Logger.Debug("sorry no gamemanager"); }
    }


    //void UpdateStreak_Hit(Vector3 hitLocatoin)
    //{
    //    if (!Is_CountsTowardsPoints) return;
    //    if (GameManager.Instance != null)
    //    {
    //        GameManager.Instance.GetStreakManager().CreateCappedStreakObject(hitLocatoin);
    //    }
    //    else { Logger.Debug("sorry no gamemanager"); }
    //}
    Transform CashedVaperPoint;

    public BulletPointsType BulletPointsType
    {
        get
        {
            return _bulletPointsType;
        }

        set
        {
            _bulletPointsType = value;
        }
    }

    //public void SetVaporSpawn(Transform hereAtFLashPoint)
    //{
    //    CashedVaperPoint = hereAtFLashPoint;
    //}


    int numcolor;
    public void NewTraileAngled(Quaternion therot, Vector3 argVAporPlace)
    {

        if (GameSettings.Instance.UseNab)
        {
            Debug.Log("not 0 " + numcolor);

            if (hitenemy)
            {
                if (BulletPointsType == BulletPointsType.Head)
                {
                    if (dudeisblocking)
                    {
                        numcolor = 2;
                    }
                    else
                    {
                        numcolor = 3;
                    }
                }
                else
                {

                    numcolor = 1;

                }
            }
            else
            {
                numcolor = 1;
            }
        }
        else
        {
            numcolor = 1;
        }


        GameManager.Instance.MkeBT(argVAporPlace, 100f, therot, numcolor);
        // GameObject bvpr_clone = Instantiate(bulletVaporTrail, argVAporPlace, therot);
        // bvpr_clone.GetComponent<BulletVaporTrail>().ChangeParticleEffect(0.25f, new Color32(21, 148, 255, 128), 0.5f);  // glow orange = 255, 152, 21, 128 || glow blue = 21, 148, 255, 128 || glow red = 255, 25, 21, 128


    }

    public void NewStartWithAngle()
    {
        layer_mask = LayerMask.GetMask("Enemy", "Target"); //, "GridMap"
        if (GameManager.Instance != null)
        {
            if (GameManager.Instance.KngGameState == ARZState.WavePlay)
            {
                BulletsFiredCountUp();
            }
        }

        // if (Physics.Raycast(transform.position, transform.forward * -1, out hitInfo, 80.0f, RaycastLayerMask))
        if (Physics.Raycast(transform.position, transform.forward * -1, out hitInfo, 80.0f, layer_mask))
        {
            CheckWhatWasHit();// Debug.Log("bullet his "+hitInfo.collider.gameObject.name + " layer =" + hitInfo.collider.gameObject.layer.ToString());
        }
        else
        {
            if (GameManager.Instance != null)
            {
                BulletMissed();
                BreakTheStreak();
            }
        }
        Destroy(gameObject);
    }

    void CheckWhatWasHit()
    {
#if ENABLE_DEBUGLOG
        Debug.Log("bullet his " + hitInfo.collider.gameObject.name + " layer =" + hitInfo.collider.gameObject.layer.ToString());
#endif
        if (hitInfo.collider.gameObject.layer == enemyLayer)
        {
            IEnemyEntityComp EntityHit = hitInfo.collider.gameObject.GetComponentInParent<IEnemyEntityComp>();
            if (EntityHit != null)
            {

                hitenemy = true;
                if (EntityHit.Get_Animer().iGet_BlockingBool()) { dudeisblocking = true; }
                if (EntityHit.GetMyType() == ARZombieypes.STD_BOSS)
                {
                    HitBoss = true;
                }


                if (hitInfo.collider.gameObject.CompareTag("ZHeadTag"))
                {
#if ENABLE_DEBUGLOG
                //   Debug.Log("a bullet in the head");
#endif
                    if (EntityHit.GetMyType() == ARZombieypes.STD_BOSS)
                    {
                        //  PlayHeadShotSound.Instance.PlaySound_2d_HeadShot();
                        int potentialdamage = (EntityHit.Get_OriginalHP() / 20);
                        // if (potentialdamage > damage) damage = potentialdamage;
                    }
                    else
                    {
                        // PlayHeadShotSound.Instance.PlaySound_2d_HeadShot();
                        int potentialdamage = (EntityHit.Get_OriginalHP() / 3) + 3;
                        if (potentialdamage > damage)
                        {
                            damage = potentialdamage;
                        }
                    }
                    BulletPointsType = BulletPointsType.Head;
                }
                else

                if (hitInfo.collider.gameObject.CompareTag("ZChestTag"))
                {
#if ENABLE_DEBUGLOG
                //   Debug.Log("a bullet in the chest");
#endif
                    BulletPointsType = BulletPointsType.Torso;
                    BreakTheStreak();
                }
                else
                if (hitInfo.collider.gameObject.CompareTag("ZPelvisTag"))
                {
#if ENABLE_DEBUGLOG
                //  Debug.Log("a bullet in the hip");
#endif
                    BulletPointsType = BulletPointsType.Hips;
                    BreakTheStreak();
                }
                else
                if (hitInfo.collider.gameObject.CompareTag("ZLegTag"))
                {
#if ENABLE_DEBUGLOG
                //    Debug.Log("a bullet in the leg");
#endif
                    BulletPointsType = BulletPointsType.Limbs;
                    HitLegs = true;
                    BreakTheStreak();
                }
                else
                if (hitInfo.collider.gameObject.CompareTag("ZArmTag"))
                {
#if ENABLE_DEBUGLOG
                // Debug.Log("a bullet in the arm");
#endif
                    BulletPointsType = BulletPointsType.Hips;
                    BreakTheStreak();
                }
                else
                if (hitInfo.collider.gameObject.CompareTag("ZNoneTag"))
                {
#if ENABLE_DEBUGLOG
                Debug.Log("a bullet in the NONE");
#endif
                    BulletPointsType = BulletPointsType.Limbs;
                    BreakTheStreak();
                }




                if (BulletPointsType == BulletPointsType.Head)
                {
                    PlayHeadShotSound.Instance.PlayImpBig();
                }
                else
                {
                    PlayHeadShotSound.Instance.PlayIMPsmall();
                }



                IEnemyDamageComp Idamage = hitInfo.collider.gameObject.GetComponentInParent<IEnemyDamageComp>();

                if (Idamage == null)
                { Debug.LogError("FOUND YOU " + hitInfo.collider.gameObject.name); }
                else
                {
                    // Idamage = hitInfo.collider.gameObject.GetComponentInParent<IEnemyDamageComp>();
                    Idamage.TakeHit(this);
                }







            }
            else
            {

                if (hitInfo.collider.gameObject.CompareTag("EnemyProjectile"))
                {
#if ENABLE_DEBUGLOG
                Debug.Log("a bullet in the NONE");
#endif

                    BreakTheStreak();

                    IShootable Ishot = hitInfo.collider.gameObject.GetComponentInParent<IShootable>();
                    if (Ishot == null)
                    { Debug.LogError("oh must be an axe " + hitInfo.collider.gameObject.name); }
                    else
                    {
                        Ishot.Shot(this);
                        return;
                    }


                }
                Debug.Log("This " + hitInfo.collider.gameObject.name + " was not an enemy despide being on enemylayer");
                return;
            }





        }
        else
        if (hitInfo.collider.gameObject.layer == TargetLAyer)
        {

            BreakTheStreak();

            IShootable Ishot = hitInfo.collider.gameObject.GetComponentInParent<IShootable>();
            if (Ishot == null)
            {
                //  Debug.LogError("FOUND YOU " + hitInfo.collider.gameObject.name);

                Ishot = hitInfo.collider.gameObject.GetComponent<IShootable>();
                if (Ishot == null)
                {

                    Ishot.Shot(this);

                }
            }
            else
            {
                //  Debug.LogError("FOUND YOU " + hitInfo.collider.gameObject.name);
                //  Ishot = hitInfo.collider.gameObject.GetComponentInParent<IShootable>();
                Ishot.Shot(this);
            }
        }
    }
    //void Olcheck()
    //{


    //    //should be start button only
    //    if (hitInfo.collider.CompareTag("Interactive"))
    //    {
    //        GameEventsManager.Instance.Call_TakebulletHit(this, 0);
    //    }


    //    //Logger.Debug("we hit smthin->");
    //    if (hitInfo.collider.gameObject.layer == enemyLayer)
    //    {
    //        ShotObject so = hitInfo.collider.gameObject.GetComponent<ShotObject>();
    //        if (so != null)
    //        {
    //            //Logger.Debug("<-hitshootable->");
    //            //	Debug.Log("<-hitshootable->");
    //            so.Shot();
    //            if (so.givesScore)
    //                BulletHitZombie();// give score
    //            return;
    //        }
    //        if (hitInfo.collider.CompareTag("EnemyProjectile"))
    //        {
    //            // Debug.Log("BOOOOOODETECT AXE");
    //            //IShootable iso = hitInfo.collider.gameObject.transform.GetChild(0).gameObject.GetComponent<IShootable>();
    //            IShootable iso = hitInfo.collider.gameObject.GetComponent<IShootable>();
    //            if (iso != null)
    //            {
    //                iso.Shot(this);
    //            }
    //        }
    //        else
    //        {

    //            IShootable iso = hitInfo.collider.gameObject.transform.parent.gameObject.GetComponent<IShootable>();
    //            if (iso != null)
    //            {
    //                iso.Shot(this);
    //            }
    //            else
    //            {
    //                // Logger.Debug("<-hitzombie->");
    //                // Record_LINE_ZombieHit(transform.position, hitInfo.point);
    //                // hitInfo.collider.gameObject.SendMessageUpwards("TakeHit", this); //will send to zombiebehavior which will assign scores depending on wether the bullet was a lethal or nonlethal shot
    //                GameEventsManager.Instance.Call_TakebulletHit(this, hitInfo.collider.gameObject.GetComponentInParent<IZBehavior>().GetZombieID());
    //            }


    //        }

    //        BulletHitZombie();//the bool justgotheadshotted was flipped : above Call_TakeBullethiit -> event ontakehit -> zdammage()takes damageg registers head shot , sets the bool, now line 196 check if you shoould or not do a 3d 100 125

    //        if (hitInfo.collider.gameObject.CompareTag("ZHeadTag"))
    //        {

    //            // Logger.Debug("<-HEADDD->");
    //            if (GameSettings.Instance.curHeadShotNum < GameSettings.Instance.maxHeadShotNum)
    //            {
    //                GameSettings.Instance.curHeadShotNum++;
    //            }

    //            Bullet_HIts_ZHead();
    //            bool didZombiethatwasshotintheheadAlreadyGotShot = hitInfo.collider.gameObject.GetComponentInParent<IZBehavior>().Get_Did_alreadyGotHeadshotted();
    //            if (didZombiethatwasshotintheheadAlreadyGotShot == false)
    //            {

    //                UpdateStreak_Hit(hitInfo.point);
    //                hitInfo.collider.gameObject.GetComponentInParent<IZBehavior>().Flip_Just_got_HeadShotted();
    //            }
    //            //else
    //            //{
    //            //    Logger.Debug("another headshot , but nostreak , problany a boss");
    //            //}
    //        }
    //        else
    //        {
    //            // Logger.Debug("<-NOTHEAD->");
    //            BreakTheStreak();
    //        }

    //    }
    //    else
    //    {
    //        if (GameManager.Instance != null)
    //        {                    // Record_LINE_Miss(transform.position, transform.position + transform.forward * 20);
    //            if (GameManager.Instance.KngGameState == ARZState.WavePlay ||
    //            GameManager.Instance.KngGameState == ARZState.Pregame ||
    //            GameManager.Instance.KngGameState == ARZState.Pregame)
    //            {
    //                BreakTheStreak();
    //            }
    //        }

    //    }
    //}

    /*
         public void NewStartWithAngle()
    {
        if (GameManager.Instance != null)
        {
            if (GameManager.Instance.KngGameState == ARZState.WavePlay)
            {
                BulletFired();
            }
        }
        // on start raycast from bullet position to hit enemies, pickups, or walls
        if (Physics.Raycast(transform.position, transform.forward * -1, out hitInfo, 50.0f, RaycastLayerMask))
        {
            //should be start button only
            if (hitInfo.collider.CompareTag("Interactive")) {
                GameEventsManager.Instance.Call_TakebulletHit(this, 0);
            }

           
            //Logger.Debug("we hit smthin->");
            if (hitInfo.collider.gameObject.layer == enemyLayer)
            {
                ShotObject so = hitInfo.collider.gameObject.GetComponent<ShotObject>();
                if (so != null)
                {
                    //Logger.Debug("<-hitshootable->");
                //	Debug.Log("<-hitshootable->");
                    so.Shot();
                    if(so.givesScore)
                        BulletHitZombie();// give score
                    return;
                }
                if (hitInfo.collider.CompareTag("EnemyProjectile"))
                {
                   // Debug.Log("BOOOOOODETECT AXE");
                    //IShootable iso = hitInfo.collider.gameObject.transform.GetChild(0).gameObject.GetComponent<IShootable>();
                    IShootable iso = hitInfo.collider.gameObject.GetComponent<IShootable>();
                    if (iso != null)
                    {
                        iso.Shot(this);
                    }
                }
                else {

                    IShootable iso = hitInfo.collider.gameObject.transform.parent.gameObject.GetComponent<IShootable>();
                    if (iso != null)
                    {
                        iso.Shot(this);
                    }
                    else
                    {
                       // Logger.Debug("<-hitzombie->");
                        // Record_LINE_ZombieHit(transform.position, hitInfo.point);
                        // hitInfo.collider.gameObject.SendMessageUpwards("TakeHit", this); //will send to zombiebehavior which will assign scores depending on wether the bullet was a lethal or nonlethal shot
                        GameEventsManager.Instance.Call_TakebulletHit(this, hitInfo.collider.gameObject.GetComponentInParent<IZBehavior>().GetZombieID());
                    }
            

                }

                BulletHitZombie();//the bool justgotheadshotted was flipped : above Call_TakeBullethiit -> event ontakehit -> zdammage()takes damageg registers head shot , sets the bool, now line 196 check if you shoould or not do a 3d 100 125

                if (hitInfo.collider.gameObject.CompareTag("ZHeadTag"))
                {

                   // Logger.Debug("<-HEADDD->");
                    if (GameManager.Instance.curHeadShotNum < GameManager.Instance.maxHeadShotNum)
                    {
                        GameManager.Instance.curHeadShotNum++;
                    }

                    Bullet_HIts_ZHead();
                    bool didZombiethatwasshotintheheadAlreadyGotShot = hitInfo.collider.gameObject.GetComponentInParent<IZBehavior>().Get_Did_alreadyGotHeadshotted();
                    if (didZombiethatwasshotintheheadAlreadyGotShot == false)
                    {

                        UpdateStreak_Hit(hitInfo.point);
                        hitInfo.collider.gameObject.GetComponentInParent<IZBehavior>().Flip_Just_got_HeadShotted();
                    }
                    //else
                    //{
                    //    Logger.Debug("another headshot , but nostreak , problany a boss");
                    //}
                }
                else
                {
                   // Logger.Debug("<-NOTHEAD->");
                    BreakTheStreak();
                }

            }
            else
            {
                if (GameManager.Instance != null)
                {                    // Record_LINE_Miss(transform.position, transform.position + transform.forward * 20);
                    if (GameManager.Instance.KngGameState == ARZState.WavePlay ||
                    GameManager.Instance.KngGameState == ARZState.Pregame ||
                    GameManager.Instance.KngGameState == ARZState.Pregame)
                    {
                        BreakTheStreak();
                    }
                }

            }
        }
        else
        {
            if (GameManager.Instance != null)
            {
              //  Logger.Debug("nothing but air");
                BreakTheStreak();
            }
        }
        Destroy(gameObject);
    }

     */



    void BreakTheStreak()
    {
        if (GameSettings.Instance != null)
        {
            GameSettings.Instance.curHeadShotNum = 0;
        }

        UpdateStreaks_Miss();
    }

    // a function for the bullet effects | takes a string (hit tag name) and transform (hit transform)
    private void BulletEffects(string tag, Vector3 hitSpot)
    {

        //if (audioSource != null)
        //{
        //    // get the audiosource
        //    //aS = GetComponent<AudioSource>();

        //    // for each clip in the bullet clips
        //    foreach (AudioClip bS in bulletSounds)
        //    {
        //        // if the name of the sound contains the tag
        //        if (bS.name.Contains(tag))
        //        {
        //            //display message
        //            Logger.Debug("Audio Clip " + bS.name + " contains: " + tag);
        //            // set audioclip
        //            audioSource.clip = bS;
        //            // play the sound
        //            audioSource.Play();
        //        }// end of name contains tag
        //        else
        //        // if it doesnt not contain tag in name
        //        {
        //            //display message
        //            Logger.Debug("Audio Clip " + bS.name + " does not contain: " + tag);
        //        }// end of tag not in name

        //    }// end of for each bullet sound

        //}// end of have audiosource
        //else
        //// if we have no audioSource attached to this object
        //{
        //    // display message
        //    Logger.Debug("Please add an audioSource to " + transform.name);
        //}// end of no audioSource

        ///// 2nd ________________________________ 2nd grab the bullet mark

    }// end of bullet effects function

    void PlaceBulletHoleWood(RaycastHit hitInfo)
    {

    }

    void PlaceBulletHoleMetal(RaycastHit hitInfo)
    {

    }
}
