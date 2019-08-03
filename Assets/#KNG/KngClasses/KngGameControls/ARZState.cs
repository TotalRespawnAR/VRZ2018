// @Author Nabil Lamriben ©2017




public enum EnemySpeakerTypes
{
    Male,
    Female,
    Creature,
    Giant,
    Bone


}

public enum AudioClipType
{
    FootStep,
    HeadShot,
    BodyShot,
    MoanSmall,
    MoanBig,
    GrowlSmall,
    GrowlBig,
    HurtSmall,
    HurtBig,
    AttackSmall,
    AttackBig,
    DeathSmall,
    DeathBig,
    ArmorMetalDing,


}
public enum TriggersDamageEffects
{
    NoneIkHandReach,
    BearClawLR,
    BearClawRL,
    ScratchUpdown,
    Scratch2XUpdpwn,
    ScratchLR,
    ScratchRL,
    RightHookLR, //hook me up
    MelePunchRL,

    AxeHit,
    GuyleSlash,
    GlassBreak1,
    GlassBreak2,

    BloodDencity1,
    BloodDencity2,
    BloodDencity3,
    BloodDencity4,
    BloodState1,
    BloodState2,
    BloodState3,
    BloodState4,
    BloodCurtain,
    //hookmeup

    //     trigBearClaw,
    //trigBloodDencity,
    //trigBloodState,
    //trigBloodCurtain
}
public enum EnemyTaskEneum
{
    EndFirstAnimation,
    EMERGED,
    SWING,   //C_SWING_type and A_SWING
    ReachedKnodeSaught,
    ReachedPlayer,
    ReachedTarget,
    ReachedEndOfKnodes,
    CombatMove,
    HandReachedGrabbTarget,
    CompletedRaizedPrey,
    LaunchRocket,
    TriggerThrow,
    GrabbedEnemy,
    Pointing,
    Roaring,
    KillmeBeforeILayEggs,
    Grounding,
    Rolling,
    HurlingDone,
    punching,
    Punching,
    Crawling,
    Dancing,
    Burning
}


public enum Ezbezzststzzz
{
    EBSTART_0,
    EBSEEK_1,
    EBECOMBAT_2,
    EBREACH_3,
    EBDEAD_4,
}

public enum SeekSpeed
{
    injuredwalk,
    walk,
    run,
    sprint

}
public enum StartAnimatorStates
{
    GRAVING,
    IDLE,

}
public enum EBSTATE
{
    START_eb0 = 0,
    //WALKING,
    //CHASING,
    //HYPERCHASING,
    SEEKING_eb1 = 1,
    COMBAT_eb2 = 2,
    REACHING_eb3 = 3,
    EBDEAD_eb4 = 4,
    EBDBURNING_eb5,
    PAUSED = 99,


};

//to be turned to string 
public enum EBSTATE_PropertyNames
{
    TrigEBstateTransition,
    IntEBSTATE,
    IntSEEKSPEED,
    IntWeakness,
    FloatMultiplier,
    IntfallDirection,
    IntDeathType,
    TrigCombatMove,
    IntCombatMoveType,
    BoolStartUnderground,
    BoolCrouching,
    BoolIsBlocking,
    TrigReact,
    TrigReachSwing,
    IntReactEnumVal,
    IntSwingEnumVal,
    TrigPickUpP1,
    TrigPickUpP2,
    TrigHurling,
    IntWalkType, //  1 for normal 0 for injured 
    BoolCrawl,
    TrigThrowAxe,
    BoolCanRect, //to avoid Reactionshots set on off during axethrow for example
    BoolisMir,
    BoolAllowBlocking,
}

//keep if wanna try triggers to force state change
public enum EBSTATECHANGETRIGGERS
{
    trig_to_EBSTART_0,
    trig_to_EBSEEK_1,
    trig_to_EBCOMBAT_2,
    trig_to_EBREACH_3,
    trig_to_EBDEAD_4,
}

public enum ARZState
{
    Pregame,
    WaveBuffer,
    WavePlay,
    WaveEnded,
    WaveOverTime,
    EndGame,
    ReachedAllowedTime

}

public enum ARZRagdollState
{
    ANIMATED,
    RAGGEDOLLED,
    BLENDTOANIMATED  //Mecanim in control, but LateUpdate() is used to partially blend in the last ragdolled pose
}

public enum ARZGameModes
{
    GameNoStem,
    GameLeft_Alpha,
    GameRight_Bravo,
    GameTest
}

public enum ARZControlerType
{
    StemControlSystem,
    StrikerControlSystem,
    None
}


public enum ModesEnum
{
    STARTIDLE,
    STARTGRAVE,
    STARTGRAVENODEATH,
    SEEKTARGET,
    PROJECTILE,
    GRABBB,
    HURLEOBJ,
    POINTFINGER,
    KSEEK,
    TARGETPLAYER,
    ATTACK,
    DEAD,
    WAITPREY,
    RAGDOLL,
    COMBAT,
    BURNING
}


public enum EnemyReachApexPlayerPerspective
{
    Apex_Rarm_TopDown,
    Apex_Larm_TopDown,
    Apex_Botharms_TopDown,
    Apex_Rarm_Right2Left,
    Apex_Larm_Left2Right,
    Apex_Botharms_Right2Left,
    Apex_Botharms_Left2Right
}


public enum EnemyAudioEvents
{
    _step, //AnimationDriven
    _stepDrag,//AnimationDriven
    _floorHit,//AnimationDriven
    _dirtDig,
    _headPain,
    _gutPain,
    _deagroPain,
    _blockPain,
    _reachGrunt1, //also axe throw
    _reachGrunt2, //also combat
    _reachGrunt3, //also combat
    _reachGrunt4, //also combat
    _dyingBreath1,
    _dyingBreath2,
    _chaseMoan0,
    _chaseMoan1,
    _chaseMoan2,
    _chaseMoan3,
    _roar,       //AnimationDriven
    _thump,  //AnimationDriven
    _thumpLow, //AnimDriven
    _armording



}
public enum AnimatorStates_STDv1 //to be used with new animator
{


    START,

    IDLE,
    GRAVING,   //trig OnEmerged
    DROPPING,  //trig OnLanded

    CRAWLING,  //
    WALKING,
    CHASING,
    HYPERCHASING,

    ATTACKING,
    REACHING,
    DYING,
    PAUSING, // 11 state 11 is thriller dance lol .. wtf?

    MELTING,
    EATING,
    LAYING,
    STUNNED,
    BURNING,
    FALLING,
};

//int animator these are the values for IntCombatMoveType
public enum CombatActions
{
    idle,//haslistener
    walkleft,//haslistener
    walkright,//haslistener
    walkbackwards,//haslistener
    walkforward,//haslistener
    punchattack1,//haslistener
    punchattack2,
    punchattack3,
    punchattack4,
    kickattack1,
    kickattack2,
    kickattack3,
    crouch,//haslistener
    uncrouch,//haslistener
    tauntchest,//haslistener  
    tauntpoint,
    tauntroar,
    righthook,//haslistener
    melepunch,//haslistener
    walkloop,
    runloop,
    walkbackloop,
    hadooken,
    jumpattack, //haslistener

}

public enum AxmanActions
{
    Grabbed,
    Throw
}

public enum ReactEnumVal
{
    Headshot = 0,
    Gutshot = 1,
    headknockLeft = 2,
    headknockRight = 3,
    shoulderknockLeft = 4,
    shoulderknockRight = 5,
    hipknock,
    backknock,
}


public enum TriggersEnemyAnimator
{
    trigTriggeredAnim, //fir all reactions, the enumtype for chosing animation

    trigHeadShot,
    trigHeadShot_L,
    trigHeadShot_R,
    trigLeftLegOff,
    trigRightLegOff,
    trigPushBack,
    ShotReact,
    ShotReact_R,
    ShotReact_M,
    ShotReact_L,
    trigPushFront,
    trigLanding,
    trigDashing,
    trigRoaring,
    trigHitGut,
    trigHitLeft,
    trigHitRight,
    trigPickUpP1,
    trigPickUpP2,
    trigPointing,
    trigHurling,
    trigThrowAxe,
    trigSpringRoll,
    trigCombatMove, //alongwith a combatmovetype 0-> idlecombat, 1->walkleft, 2->walkright, 3-> walkback 4-> walkforward 5-> attack1 6->attack2
    trigCombatHeadShot,
    trigCombatHitBlock,
    trigCombatHitBlockBreak,
    trigOutOfCombat,
    trigStandPunch,
    trigPunch,
    trigReachSwing
}

public enum BoolsEnemyAnimator
{
    noHead,
    crouching,
    dead,
    reaching,
    crawling,
    noLegs,
    IsBlocking

}


public enum TriggersTestAnimator
{
    trigStartState0,
    trigSeekState1,
    trigCombatState2,
    trigDieState3,
    trigSpecialState4
}

public enum LeftMidRight
{
    LEFT,
    MID,
    RIGHT
}

public enum FarMidNearNone
{
    FAR,
    MID,
    NEAR,
    NONE
}

public enum MidNearNone
{
    MID,
    NEAR,
    NONE
}

//public enum HitType
//{
//    None,
//    ZombieScratch,
//    ZombieScratchRL,
//    ZombieScratchLR,
//    ZobieScratch2handupdown,
//    AxeSplit,
//    GuleSlash,
//    FlySplat,
//    ZombiePunchLR,
//    ZombiePunchRL

//}

public enum GunType
{
    NONE,
    MAGNUM,
    PISTOL,
    UZI,
    SHOTGUN,
    P90,
    MG61,
    HELL,
    GRENADELAUNCHER,
    STRIKERVR
}

public enum buletcolor
{
    yellow,
    orange,
    red
}

public enum PowerUpType
{
    DoublePoints,
    SlowMo,
    InVinsible,

}

public enum Ammunition
{
    NONE,
    MAGNUM,
    PISTOL,
    UZI,
    SHOTGUN,
    P90,
    MG61,
    HELL,
    GRENADELAUNCHER,
};


public enum GunState
{
    // CLIP_IN, no need, we have gun.iscliploaded
    // CLIP_OUT, no need, we have gun.iscliploaded
    MAGizIN,
    CLIP_FULL,
    CANSHOOT,

    CLIP_EMPTY,

    MAGizOUT,
    RELOADING,
    JAMMED
};


public enum WeaponContext
{

    Colt_GUN_MainHand,
    M1911_GUN_MainHand,
    Mac11_GUN_MainHand,
    SOShotgun_GUN_MainHand,

    Colt_MAG_OffHand,
    M1911_MAG_OffHand,
    Mac11_MAG_OffHand,
    SOShotgun_MAG_OffHand,

    Colt_MAG_inGun,
    M1911_MAG_inGun,
    Mac11_MAG_inGun,
    SOShotgun_MAG_inGun,

    Colt_GUN_onRack,
    M1911_GUN_onRack,
    Mac11_GUN_onRack,
    SOShotgun_GUN_onRack,

    Colt_MAG_onRack,
    M1911_MAG_onRack,
    Mac11_MAG_onRack,
    SOShotgun_MAG_onRack
}

public enum GunScopes
{

    Lazor,
    Sniper,
    Kin,
    RedDot,
    NONE,
}

public enum ARZHandType
{
    HandGun, HandMag
}

public enum ARZPlayerLeftyRighty
{
    LeftyPlayer, RightyPlayer
}

public enum CellState
{
    Empty,
    Started,
    Running,
    Ended

}

public enum GunReloadState
{
    A_LOCKEDANDLOAEDED,
    B_EJECTINGMAG,
    C_NOMAG,
    D_INSERTINGMAG

}

//keep in this order cuz i use them as indecies in reload meter image cell array 
public enum ReloadMeterState
{
    SEGMENT_0,
    SEGMENT_1,
    SEGMENT_2,
    Anim0,
    Anim1,
    Anim2,
    Sleeping
}


//esp 12 e
/// points = 0;
/// wavePoints = 0;
/// shotCount = 0;
/// headShotCount = 0;
/// torsoShotCount = 0;
/// limbShotCount = 0;
/// targetHitCount = 0;
/// killCount = 0;
/// enemiesCreatedCount = 0;
/// 


public enum ScoreType
{
    points_0,
    wavePoints_1,
    shotCount_2,
    headShotCount_3,
    torsoShotCount_4,
    limbShotCount_5,
    targetHitCount_6,
    killCount_7,
    enemiesCreatedCount_8,
    timesReloaded_9,
    timesDied_10
}

public enum ARZReloadLevel
{
    NOOB,
    EASY,
    MEDIUM,
    HARD,

}


public enum ARZGameSessionType
{
    SINGLE,
    MULTI,
    UDP
}



//public enum ARZMMeshBone
//{
//    Head_M,
//    Wrist_L, Wrist_R,
//    Knee_L, Knee_R,
//C:\NabilDir\TotalRespawn\ARZ_Single\Assets\Scripts\Behavior\PlayerBehavior.cs    Hip_L, Hip_R,
//    Elbow_L, Elbow_R,
//    Shoulder_L, Shoulder_R,
//    DEFAULT
//}
public enum ARZMMeshBone
{
    //no child
    Head_M,                 // 0
    Knee_L, Knee_R,         //1 2
    Wrist_L, Wrist_R,       //3 4
    //1 child
    Hip_L, Hip_R,           //5 6
    Elbow_L, Elbow_R,       //7 8
    //2 children
    Shoulder_L, Shoulder_R, //9 10
    DEFAULT
}

public enum BulletPointsType
{
    Head,
    Torso,
    Hips,
    Limbs,
    Special
}


public enum ARZPregameType
{
    BRICKWALL,
    OPENBACKROOM,
    GRAVEZOMBIES,
    EATING,
}


public enum ARZombieypes
{
    STANDARD,
    GRAVESKELETON,

    PREYgrave,
    PREDATOR,

    MiniBoss,
    LAYER,
    EATER,
    STD_BOSS,
    BOSS_1,
    BOSS_2,
    AXEMAN,
    BIGBOSS,
    FLY1,
    FLY2,
    TankFighter,
    Sprinter,

}

public enum ARZroom
{
    HOSPITAL,
    DESERT,
    FOREST
}

public enum ARZSpawnType
{
    BACKROOM,
    SPAWN,
    FRONTROOM
}

public enum KngEnemyName
{
    NONE,
    BASIC,
    SPINAL,
    SHITFACE,
    HOODIE,
    PAUL,
    NAKEDPAUL,
    ABOMINATION,
    SARAHCONNOR,
    CAROL,
    MUMMY,
    SKELETON,
    HILLBILLY,
    SWEATER,
    GASMASK,
    BIGMUTANT,
    KNIFEFIGHTER,
    CHECKER,
    MEATHEADWHITE,
    MEATHEADBLACK,
    LADYPANTS,
    MAW,
    PUMPKIN,
    BEAR,
    MUTANTBLUE,
    PARASITE,
    AXEMAN,
    FLY1,
    FLY2,
    Blank,
    Box,
    axvmanv2,
    PumpkinShort,
    HOLOBONES

}

public enum ARZPowerups
{
    NONE,
    SLOWTIME,
    DOUBLEPOINTS,
    GRENADE
}

public enum ARZTutorialStates
{
    StartTutorial,
    LearnShoot,
    LearnReload,
    LearnChangeWeapon,
    LearnDoge,
    LearnDogeInAction,
    EndTutorial
}

public enum AgroUpDown
{
    Up,
    Down
}

public enum AgroLevel
{
    Low,
    Med,
    High

}

public enum KNodeNExtDir
{
    ToPlayer,
    AwayFromPlayer,
    Player_s_Right,
    Player_s_Left
}