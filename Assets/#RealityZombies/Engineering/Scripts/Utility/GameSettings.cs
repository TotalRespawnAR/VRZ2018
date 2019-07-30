// @Author Nabil Lamriben ©2017

using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{

    public static GameSettings Instance = null;

    private bool _isNOTEditor = false;//true;

    bool has_waveLevelSettingsFile;
    public bool HasWaveSettingsFile()
    {
        return has_waveLevelSettingsFile;
    }

    public bool NABILSETTINGSON;

    private void Awake()
    {
        Application.targetFrameRate = 80;

        if (Instance == null)
        {
            //FindWavveSettinggsFile();
            //if (has_waveLevelSettingsFile) Load(Kng_wavesettingsFilePATH + "/" + WaveLevelSetttingsFileName + ".txt");





            DontDestroyOnLoad(this.gameObject);
            Instance = this;
            _gameVersion = Application.version.ToString();
            _gameName = Application.productName.ToString();


#if !UNITY_EDITOR
            _isNOTEditor = true;
#endif


            AttackBigclipMale = Resources.Load<AudioClip>("AudioClips/Enemy/AudioUsed/Male/AttackBigMale");
            AttackSmallclipMale = Resources.Load<AudioClip>("AudioClips/Enemy/AudioUsed/Male/AttackSmallMale");
            DeathBigclipMale = Resources.Load<AudioClip>("AudioClips/Enemy/AudioUsed/Male/DeathBigMale");
            DeathSmallclipMale = Resources.Load<AudioClip>("AudioClips/Enemy/AudioUsed/Male/DeathSmallMale");
            GrowlBigclipMale = Resources.Load<AudioClip>("AudioClips/Enemy/AudioUsed/Male/GrowlBigMale");
            GrowlSmallclipMale = Resources.Load<AudioClip>("AudioClips/Enemy/AudioUsed/Male/GrowlSmallMale");
            HurtBigclipMale = Resources.Load<AudioClip>("AudioClips/Enemy/AudioUsed/Male/HurtBigMale");
            HurtSmallclipMale = Resources.Load<AudioClip>("AudioClips/Enemy/AudioUsed/Male/HurtSmallMale");
            MoanBigclipMale = Resources.Load<AudioClip>("AudioClips/Enemy/AudioUsed/Male/MoanBigMale");
            MoanSmallclipMale = Resources.Load<AudioClip>("AudioClips/Enemy/AudioUsed/Male/MoanSmallMale");



            AttackBigclipFemale = Resources.Load<AudioClip>("AudioClips/Enemy/AudioUsed/Female/AttackBigFemale");
            AttackSmallclipFemale = Resources.Load<AudioClip>("AudioClips/Enemy/AudioUsed/Female/AttackSmallFemale");
            DeathBigclipFemale = Resources.Load<AudioClip>("AudioClips/Enemy/AudioUsed/Female/DeathBigFemale");
            DeathSmallclipFemale = Resources.Load<AudioClip>("AudioClips/Enemy/AudioUsed/Female/DeathSmallFemale");
            GrowlBigclipFemale = Resources.Load<AudioClip>("AudioClips/Enemy/AudioUsed/Female/GrowlBigFemale");
            GrowlSmallclipFemale = Resources.Load<AudioClip>("AudioClips/Enemy/AudioUsed/Female/GrowlSmallFemale");
            HurtBigclipFemale = Resources.Load<AudioClip>("AudioClips/Enemy/AudioUsed/Female/HurtBigFemale");
            HurtSmallclipFemale = Resources.Load<AudioClip>("AudioClips/Enemy/AudioUsed/Female/HurtSmallFemale");
            MoanBigclipFemale = Resources.Load<AudioClip>("AudioClips/Enemy/AudioUsed/Female/MoanBigFemale");
            MoanSmallclipFemale = Resources.Load<AudioClip>("AudioClips/Enemy/AudioUsed/Female/MoanSmallFemale");


            AttackBigclipGiant = Resources.Load<AudioClip>("AudioClips/Enemy/AudioUsed/Giant/AttackBigGiant");
            AttackSmallclipGiant = Resources.Load<AudioClip>("AudioClips/Enemy/AudioUsed/Giant/AttackSmallGiant");
            DeathBigclipGiant = Resources.Load<AudioClip>("AudioClips/Enemy/AudioUsed/Giant/DeathBigGiant");
            DeathSmallclipGiant = Resources.Load<AudioClip>("AudioClips/Enemy/AudioUsed/Giant/DeathSmallGiant");
            GrowlBigclipGiant = Resources.Load<AudioClip>("AudioClips/Enemy/AudioUsed/Giant/GrowlBigGiant");
            GrowlSmallclipGiant = Resources.Load<AudioClip>("AudioClips/Enemy/AudioUsed/Giant/GrowlSmallGiant");
            HurtBigclipGiant = Resources.Load<AudioClip>("AudioClips/Enemy/AudioUsed/Giant/HurtBigGiant");
            HurtSmallclipGiant = Resources.Load<AudioClip>("AudioClips/Enemy/AudioUsed/Giant/HurtSmallGiant");
            MoanBigclipGiant = Resources.Load<AudioClip>("AudioClips/Enemy/AudioUsed/Giant/MoanBigGiant");
            MoanSmallclipGiant = Resources.Load<AudioClip>("AudioClips/Enemy/AudioUsed/Giant/MoanSmallGiant");


            AttackBigclipCreature = Resources.Load<AudioClip>("AudioClips/Enemy/AudioUsed/Creature/AttackBigCreature");
            AttackSmallclipCreature = Resources.Load<AudioClip>("AudioClips/Enemy/AudioUsed/Creature/AttackSmallCreature");
            DeathBigclipCreature = Resources.Load<AudioClip>("AudioClips/Enemy/AudioUsed/Creature/DeathBigCreature");
            DeathSmallclipCreature = Resources.Load<AudioClip>("AudioClips/Enemy/AudioUsed/Creature/DeathSmallCreature");
            GrowlBigclipCreature = Resources.Load<AudioClip>("AudioClips/Enemy/AudioUsed/Creature/GrowlBigCreature");
            GrowlSmallclipCreature = Resources.Load<AudioClip>("AudioClips/Enemy/AudioUsed/Creature/GrowlSmallCreature");
            HurtBigclipCreature = Resources.Load<AudioClip>("AudioClips/Enemy/AudioUsed/Creature/HurtBigCreature");
            HurtSmallclipCreature = Resources.Load<AudioClip>("AudioClips/Enemy/AudioUsed/Creature/HurtSmallCreature");
            MoanBigclipCreature = Resources.Load<AudioClip>("AudioClips/Enemy/AudioUsed/Creature/MoanBigCreature");
            MoanSmallclipCreature = Resources.Load<AudioClip>("AudioClips/Enemy/AudioUsed/Creature/MoanSmallCreature");

            AttackBigclipBone = Resources.Load<AudioClip>("AudioClips/Enemy/AudioUsed/Bone/AttackBigBone");
            AttackSmallclipBone = Resources.Load<AudioClip>("AudioClips/Enemy/AudioUsed/Bone/AttackSmallBone");
            DeathBigclipBone = Resources.Load<AudioClip>("AudioClips/Enemy/AudioUsed/Bone/DeathBigBone");
            DeathSmallclipBone = Resources.Load<AudioClip>("AudioClips/Enemy/AudioUsed/Bone/DeathSmallBone");
            GrowlBigclipBone = Resources.Load<AudioClip>("AudioClips/Enemy/AudioUsed/Bone/GrowlBigBone");
            GrowlSmallclipBone = Resources.Load<AudioClip>("AudioClips/Enemy/AudioUsed/Bone/GrowlSmallBone");
            HurtBigclipBone = Resources.Load<AudioClip>("AudioClips/Enemy/AudioUsed/Bone/HurtBigBone");
            HurtSmallclipBone = Resources.Load<AudioClip>("AudioClips/Enemy/AudioUsed/Bone/HurtSmallBone");
            MoanBigclipBone = Resources.Load<AudioClip>("AudioClips/Enemy/AudioUsed/Bone/MoanBigBone");
            MoanSmallclipBone = Resources.Load<AudioClip>("AudioClips/Enemy/AudioUsed/Bone/MoanSmallBone");

            BoneHeadShotCrushclip = Resources.Load<AudioClip>("AudioClips/Enemy/AudioUsed/SharedAudio/EffectBoneHeadShotCrush");
            Bodyshotclip = Resources.Load<AudioClip>("AudioClips/Enemy/AudioUsed/SharedAudio/EffectBodyShotCrush");
            BoneBodyShotclip = Resources.Load<AudioClip>("AudioClips/Enemy/AudioUsed/SharedAudio/EffectBoneBodyShot");
            BoneSmallBreakclip = Resources.Load<AudioClip>("AudioClips/Enemy/AudioUsed/SharedAudio/EffectBoneSmallBreak");
            HeadShotCrushclip = Resources.Load<AudioClip>("AudioClips/Enemy/AudioUsed/SharedAudio/EffectHeadShotCrush");
            FootStepBasicclip = Resources.Load<AudioClip>("AudioClips/Enemy/AudioUsed/SharedAudio/FootStepBasic");
            FootStepPumpkinclip = Resources.Load<AudioClip>("AudioClips/Enemy/AudioUsed/SharedAudio/FootStepPumpkin");


            Buzz100 = Resources.Load<AudioClip>("AudioClips/HeadshotBuzz/Buzz100");
            Buzz125 = Resources.Load<AudioClip>("AudioClips/HeadshotBuzz/Buzz125");
            Buzz150 = Resources.Load<AudioClip>("AudioClips/HeadshotBuzz/Buzz150");
            Buzz175 = Resources.Load<AudioClip>("AudioClips/HeadshotBuzz/Buzz175");
            Buzz200 = Resources.Load<AudioClip>("AudioClips/200of200GOODwithYAY");
            Buzz200Cheer = Resources.Load<AudioClip>("AudioClips/HeadshotBuzz/Buzz200Cheer");
            GlassBreakSmall = Resources.Load<AudioClip>("AudioClips/HeadshotBuzz/GlassBreakSmall");
            Impact_Metal_0 = Resources.Load<AudioClip>("AudioClips/HeadshotBuzz/Impact_Metal_0");
            Impact_Metal_1 = Resources.Load<AudioClip>("AudioClips/HeadshotBuzz/Impact_Metal_1");
            Impact_Metal_2 = Resources.Load<AudioClip>("AudioClips/HeadshotBuzz/Impact_Metal_2");



            Womp = Resources.Load<AudioClip>("AudioClips/HeadshotBuzz/movieTrailerWomp");
            Womp2 = Resources.Load<AudioClip>("AudioClips/HeadshotBuzz/angryBoat");
            AirHorn = Resources.Load<AudioClip>("AudioClips/AirHorn");
            Gong = Resources.Load<AudioClip>("AudioClips/DeskBell");
            Gong2 = Resources.Load<AudioClip>("AudioClips/HeadshotBuzz/Impact_Metal_2");
            Slow = Resources.Load<AudioClip>("AudioClips/PowerUp_SndFx_SlowTime_End");
            SlowOff = Resources.Load<AudioClip>("AudioClips/SlowTimeStartAndSTop");
            HeartBeat = Resources.Load<AudioClip>("AudioClips/HeavyBreathing");
            BodyImpact = Resources.Load<AudioClip>("AudioClips/HeadshotBuzz/Impact_BodyFlesh_0");
            HeadshotImpace = Resources.Load<AudioClip>("AudioClips/HeadshotBuzz/Impact_Metal_2");

            GameOver = Resources.Load<AudioClip>("AudioClips/HeadshotBuzz/gameOver");
            Sudden = Resources.Load<AudioClip>("AudioClips/HeadshotBuzz/suddendeath");

            A_1 = Resources.Load<AudioClip>("AudioClips/DemonicCountDown/Demonic_1_one");
            A_2 = Resources.Load<AudioClip>("AudioClips/DemonicCountDown/Demonic_2_two");
            A_3 = Resources.Load<AudioClip>("AudioClips/DemonicCountDown/Demonic_3_three");
            A_4 = Resources.Load<AudioClip>("AudioClips/DemonicCountDown/Demonic_4_four");
            A_5 = Resources.Load<AudioClip>("AudioClips/DemonicCountDown/Demonic_5_five");
            A_6 = Resources.Load<AudioClip>("AudioClips/DemonicCountDown/Demonic_6_six");
            A_7 = Resources.Load<AudioClip>("AudioClips/DemonicCountDown/Demonic_7_seven");
            A_8 = Resources.Load<AudioClip>("AudioClips/DemonicCountDown/Demonic_8_eight");
            A_9 = Resources.Load<AudioClip>("AudioClips/DemonicCountDown/Demonic_9_nine");
            A_10 = Resources.Load<AudioClip>("AudioClips/DemonicCountDown/Demonic_10_ten");

            Squish0 = Resources.Load<AudioClip>("AudioClips/HeadshotBuzz/Squish_0");
            Squish1 = Resources.Load<AudioClip>("AudioClips/HeadshotBuzz/Squish_1");
            Squish2 = Resources.Load<AudioClip>("AudioClips/HeadshotBuzz/Squish_2");


            //                             30
            WaveTimes__Sec = new int[8] { 30, 40, 50, 60, 6000, 40, 40, 1000000 };
            WaveSpawnRates = new int[8] { 1, 1, 1, 1, 1, 1, 1, 1 };
            WaveGraveRates = new int[8] { 5, 4, 3, 2, 1, 1, 2, 2 };
            //WaveSpawnRates = new int[8] { 2000, 4, 3, 3, 2, 1, 1, 1 };
            //WaveGraveRates = new int[8] { 6000, 5, 4, 2, 1, 3, 2, 2 };

            Wave_Enemy_HItPoints_HP = new int[8] { 100, 200, 300, 400, 500, 4, 4, 4 };
            WaveMaxe_Standards_OnScreen = new int[8] { 10, 15, 18, 20, 22, 25, 18, 18 };
            WaveMaxe_Graverrrs_OnScreen = new int[8] { 5, 10, 10, 10, 10, 10, 18, 18 };
            WaveMaxe_Sprinters_OnScreen = new int[8] { 5, 4, 7, 8, 10, 16, 18, 18 };
            WaveMaxe_AxeDudess_OnScreen = new int[8] { 1, 3, 4, 5, 6, 0, 0, 0 };

            Wave_Boss____Times = new int[8] { -1, -1, 30, 40, 60, -1, -1, -1 };

            WaveGraveTimeStart = new int[8] { 1, 1, 1, 1, 1, 1, 1, 1 };

            WaveSpeedUpTimeStart = new int[8] { 40, 50, 70, 60, 70, 10, 10, 10 };

            Wave_zombieHIt_Strengthss = new int[8] { 2, 2, 3, 4, 5, 4, 4, 4 };




            WaveSeekSpeeds = new SeekSpeed[8];
            WaveSeekSpeeds[0] = SeekSpeed.walk;
            WaveSeekSpeeds[1] = SeekSpeed.run;
            WaveSeekSpeeds[2] = SeekSpeed.run;
            WaveSeekSpeeds[3] = SeekSpeed.run;
            WaveSeekSpeeds[4] = SeekSpeed.run;
            WaveSeekSpeeds[5] = SeekSpeed.run;
            WaveSeekSpeeds[6] = SeekSpeed.run;
            WaveSeekSpeeds[7] = SeekSpeed.sprint;



            MainGuns = new GunType[8];
            MainGuns[0] = GunType.MAGNUM;
            MainGuns[1] = GunType.PISTOL;
            MainGuns[2] = GunType.MG61;
            MainGuns[3] = GunType.UZI;
            MainGuns[4] = GunType.P90;
            MainGuns[5] = GunType.MG61;
            MainGuns[6] = GunType.MG61;
            MainGuns[7] = GunType.P90;


            SecondaryGuns = new GunType[8];
            SecondaryGuns[0] = GunType.SHOTGUN;
            SecondaryGuns[1] = GunType.SHOTGUN;
            SecondaryGuns[2] = GunType.SHOTGUN;
            SecondaryGuns[3] = GunType.SHOTGUN;
            SecondaryGuns[4] = GunType.SHOTGUN;
            SecondaryGuns[5] = GunType.P90;
            SecondaryGuns[6] = GunType.P90;
            SecondaryGuns[7] = GunType.SHOTGUN;

            WaveHasBos = new bool[8];
            WaveHasBos[0] = true;
            WaveHasBos[1] = true; //but boss time -1 so just do spawning groups , not incremental
            WaveHasBos[2] = false;
            WaveHasBos[3] = true;
            WaveHasBos[4] = true;
            WaveHasBos[5] = false;
            WaveHasBos[6] = false;
            WaveHasBos[7] = false;


            WaveTimesSpawnAxe = new List<int>[8];
            WaveTimesSpawnAxe[0] = new List<int>() { 10, 60, 65, 68 };// { 5, 8, 10, 12, 15, 20, 21, 22, 23, 24, 25, 26, 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90 };//   
            WaveTimesSpawnAxe[1] = new List<int>() { 20, 40, 50 };//{ 5, 8, 10, 12, 15, 20, 21, 22, 23, 24, 25, 26, 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90 };//  
            WaveTimesSpawnAxe[2] = new List<int>() { 15, 25, 35, 50, 65, 70, 80 };
            WaveTimesSpawnAxe[3] = new List<int>() { 15, 30, 45, 60, 70, 80 };
            WaveTimesSpawnAxe[4] = new List<int>() { 15, 25, 35, 50, 65, 70, 80, 100, 120, 130, 140, 150, 160, 170, 180, 190, 200, 220, 230, 240, 250, 260, 270, 280, 290, 300, 310, 320, 330, 340, 350, 360, 370, 380, 390, 400, 410, 420, 430, 440, 450, 460, 470, 480, 490, 500, 510, 520, 530, 540, 550, 560, 570, 580, 590, 600 };
            WaveTimesSpawnAxe[5] = new List<int>() { 1000 };// { 15, 25 };
            WaveTimesSpawnAxe[6] = new List<int>() { 1000 };//{ 15, 25 };
            WaveTimesSpawnAxe[7] = new List<int>() { 1000 };//{ 15, 25 };



            WaveTimesSpawnPrinter = new List<int>[8];
            WaveTimesSpawnPrinter[0] = new List<int>() { 1000 };
            WaveTimesSpawnPrinter[1] = new List<int>() { 55, 58 };// { 10, 15, 20, 25, 30, 40, 50, 60 };  
            WaveTimesSpawnPrinter[2] = new List<int>() { 5, 15, 16, 17, 20, 25, 30, 45, 60 };
            WaveTimesSpawnPrinter[3] = new List<int>() { 10, 15, 20, 25, 26, 30, 35, 36, 40, 45, 46, 47, 50, 55, 56, 57, 60, 65, 66, 70, 75, 76, 80, 81, 82, 83, 84, 85, 86, 87, 88, 89, 90, 100, 110, 120 };
            WaveTimesSpawnPrinter[4] = new List<int>() { 2, 5, 10, 11, 15, 17, 20, 21, 23, 24, 30, 32, 35, 37, 40, 42, 46, 48, 51, 55, 56, 57, 60, 62, 64, 66, 70, 80, 100, 115, 117, 120, 121, 123, 124, 130, 132, 135, 137, 140, 142, 146, 148, 151, 155, 156, 157, 160, 162, 164, 166, 170, 171, 172, 175, 180, 200, 215, 217, 220, 221, 223, 224, 230, 232, 235, 237, 240, 242, 246, 248, 251, 255, 256, 257, 260, 262, 264, 266, 270, 271, 272, 275, 280, 290, 291, 292, 293, 294, 295, 296, 297, 298, 299, 300, 30000 };
            WaveTimesSpawnPrinter[5] = new List<int>() { 1000 };// { 2, 3, 4, 5, 16, 19, 21, 26, 20, 28 };
            WaveTimesSpawnPrinter[6] = new List<int>() { 1000 };// { 2, 3, 4, 5, 16, 19, 21, 26, 20, 21, 26, 29 };
            WaveTimesSpawnPrinter[7] = new List<int>() { 1000 };//{ 2, 3, 4, 5, 16, 19, 21, 26, 20, 21, 25 };


            WaveAvailableSpawnPoints = new List<int>[8];
            WaveAvailableSpawnPoints[0] = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
            WaveAvailableSpawnPoints[1] = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            WaveAvailableSpawnPoints[2] = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            WaveAvailableSpawnPoints[3] = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            WaveAvailableSpawnPoints[4] = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            WaveAvailableSpawnPoints[5] = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            WaveAvailableSpawnPoints[6] = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            WaveAvailableSpawnPoints[7] = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };

            // 16    18 19 20 21    23
            // 24    26 27 28 29    31
            // 32    34 35 36 37    38
            // 40 41 42 43 44 45 46 47
            // 48 49 50 51 52 53 54 55
            WaveAvailableGravePoints = new List<int>[8];
            WaveAvailableGravePoints[0] = new List<int>() { 24, 34, 27, 37, 41 };
            WaveAvailableGravePoints[1] = new List<int>() { 24, 34, 27, 37, 41, 46, 43 };
            WaveAvailableGravePoints[2] = new List<int>() { 34, 27, 37, 41, 46, 43, 49, 52, 54 };
            WaveAvailableGravePoints[3] = new List<int>() { 34, 27, 37, 41, 46, 43, 24, 49, 52, 54 };
            WaveAvailableGravePoints[4] = new List<int>() { 34, 27, 24, 34, 37, 41, 46, 43, 49, 52, 54, 50, 46 };
            WaveAvailableGravePoints[5] = new List<int>() { 34, 27, 37, 41, 46, 43, 49, 52, 54, 50, 46 };
            WaveAvailableGravePoints[6] = new List<int>() { 34, 27, 37, 41, 46, 43, 49, 52, 54, 50, 46 };
            WaveAvailableGravePoints[7] = new List<int>() { 24, 26, };

            WaveEnemyNames = new List<KngEnemyName>[8];
            //WaveEnemyNames[0] = new List<KngEnemyName>() { KngEnemyName.BASIC, KngEnemyName.HOODIE, KngEnemyName.SHITFACE };
            WaveEnemyNames[0] = new List<KngEnemyName>() { KngEnemyName.BASIC, KngEnemyName.HOODIE, KngEnemyName.SARAHCONNOR, KngEnemyName.SHITFACE };
            WaveEnemyNames[1] = new List<KngEnemyName>() { KngEnemyName.BASIC, KngEnemyName.HOODIE, KngEnemyName.SHITFACE };
            WaveEnemyNames[2] = new List<KngEnemyName>() { KngEnemyName.BASIC, KngEnemyName.HOODIE, KngEnemyName.SHITFACE };
            WaveEnemyNames[3] = new List<KngEnemyName>() { KngEnemyName.BASIC, KngEnemyName.HOODIE, KngEnemyName.SHITFACE };
            WaveEnemyNames[4] = new List<KngEnemyName>() { KngEnemyName.BASIC, KngEnemyName.HOODIE, KngEnemyName.SHITFACE };
            WaveEnemyNames[5] = new List<KngEnemyName>() { KngEnemyName.BASIC, KngEnemyName.HOODIE, KngEnemyName.SHITFACE };
            WaveEnemyNames[6] = new List<KngEnemyName>() { KngEnemyName.BASIC, KngEnemyName.HOODIE, KngEnemyName.SHITFACE };
            WaveEnemyNames[7] = new List<KngEnemyName>() { KngEnemyName.BASIC, KngEnemyName.HOODIE, KngEnemyName.SHITFACE };

            // WaveEnemyNames[0].AddRange(new List<KngEnemyName>() { KngEnemyName.NAKEDPAUL, KngEnemyName.NAKEDPAUL, KngEnemyName.HILLBILLY });

            WaveEnemyNames[0].AddRange(new List<KngEnemyName>() { KngEnemyName.PAUL, KngEnemyName.NAKEDPAUL, KngEnemyName.HILLBILLY });

            WaveEnemyNames[1].AddRange(new List<KngEnemyName>() { KngEnemyName.NAKEDPAUL, KngEnemyName.HILLBILLY, KngEnemyName.SARAHCONNOR, KngEnemyName.MEATHEADWHITE });
            WaveEnemyNames[2].AddRange(new List<KngEnemyName>() { KngEnemyName.NAKEDPAUL, KngEnemyName.HILLBILLY, KngEnemyName.SARAHCONNOR, KngEnemyName.MEATHEADWHITE });
            WaveEnemyNames[3].AddRange(new List<KngEnemyName>() { KngEnemyName.NAKEDPAUL, KngEnemyName.HILLBILLY, KngEnemyName.SARAHCONNOR, KngEnemyName.MEATHEADWHITE });

            WaveEnemyNames[4].AddRange(new List<KngEnemyName>() { KngEnemyName.NAKEDPAUL, KngEnemyName.HILLBILLY, KngEnemyName.SARAHCONNOR, KngEnemyName.MEATHEADWHITE });
            WaveEnemyNames[5].AddRange(new List<KngEnemyName>() { KngEnemyName.NAKEDPAUL, KngEnemyName.HILLBILLY, KngEnemyName.SARAHCONNOR, KngEnemyName.MEATHEADWHITE });
            WaveEnemyNames[6].AddRange(new List<KngEnemyName>() { KngEnemyName.NAKEDPAUL, KngEnemyName.HILLBILLY, KngEnemyName.SARAHCONNOR, KngEnemyName.MEATHEADWHITE, KngEnemyName.KNIFEFIGHTER, KngEnemyName.BIGMUTANT });
            WaveEnemyNames[7].AddRange(new List<KngEnemyName>() { KngEnemyName.NAKEDPAUL, KngEnemyName.HILLBILLY, KngEnemyName.SARAHCONNOR, KngEnemyName.MEATHEADWHITE, KngEnemyName.KNIFEFIGHTER, KngEnemyName.BIGMUTANT });

        }
        else
        {
            Destroy(gameObject);
        }
    }
    #region SetILevelProps
    int[] WaveTimes__Sec;
    public int GetHardCodedWaveTime(int argZeroBAsed)
    {
        return WaveTimes__Sec[argZeroBAsed];
    }

    int[] Wave_Enemy_HItPoints_HP;
    public int GetHardCodedWaveHP(int argZeroBAsed)
    {
        return Wave_Enemy_HItPoints_HP[argZeroBAsed];
    }
    SeekSpeed[] WaveSeekSpeeds;
    public SeekSpeed GetHardCodedWaveSpeed(int argZeroBAsed)
    {
        return WaveSeekSpeeds[argZeroBAsed];
    }
    int[] WaveMaxe_Standards_OnScreen;
    public int GetHardCodedMazOnScereen(int argZeroBAsed)
    {
        return WaveMaxe_Standards_OnScreen[argZeroBAsed];
    }

    int[] WaveMaxe_Graverrrs_OnScreen;
    public int GetHardCodedMax_Graves_OnScereen(int argZeroBAsed)
    {
        return WaveMaxe_Graverrrs_OnScreen[argZeroBAsed];
    }

    int[] WaveMaxe_Sprinters_OnScreen;
    public int GetHardCodedMax_Sprinters_OnScereen(int argZeroBAsed)
    {
        return WaveMaxe_Sprinters_OnScreen[argZeroBAsed];
    }


    bool[] WaveHasBos;
    public bool GetHardCodedHasBoss(int argZeroBAsed)
    {
        return WaveHasBos[argZeroBAsed];
    }
    int[] Wave_Boss____Times;
    public int GetHardCodedWaveBossTimes(int argZeroBAsed)
    {
        return Wave_Boss____Times[argZeroBAsed];
    }

    int[] Wave_zombieHIt_Strengthss;
    public int GetHardCodedWaveHitStregths(int argZeroBAsed)
    {
        return Wave_zombieHIt_Strengthss[argZeroBAsed];
    }

    GunType[] MainGuns;
    public GunType GetHardCodedWaveMainGuns(int argZeroBAsed)
    {
        return MainGuns[argZeroBAsed];
    }

    GunType[] SecondaryGuns;
    public GunType GetHardCodedWaveSecondaryGuns(int argZeroBAsed)
    {
        return SecondaryGuns[argZeroBAsed];
    }

    //BasicLevel
    int[] WaveSpawnRates;
    public int GetHardCodedWave_Spawn_Rates(int argZeroBAsed)
    {
        return WaveSpawnRates[argZeroBAsed];
    }

    int[] WaveGraveRates;
    public int GetHardCodedWave_Grave_Rates(int argZeroBAsed)
    {
        return WaveGraveRates[argZeroBAsed];
    }



    int[] WaveGraveTimeStart;
    public int GetHardCodedWave_Grave_TimeStart(int argZeroBAsed)
    {
        return WaveGraveTimeStart[argZeroBAsed];
    }

    int[] WaveSpeedUpTimeStart;
    public int GetHardCodedWave_Speed_IncreaseStart(int argZeroBAsed)
    {
        return WaveSpeedUpTimeStart[argZeroBAsed];
    }

    int[] WaveMaxe_AxeDudess_OnScreen;
    public int GetHardCodedWave_MAx_Axemen(int argZeroBAsed)
    {
        return WaveMaxe_AxeDudess_OnScreen[argZeroBAsed];
    }

    List<int>[] WaveTimesSpawnAxe;
    public List<int> GetHardCodedWave_AxeGue_Times(int argZeroBAsed)
    {
        return WaveTimesSpawnAxe[argZeroBAsed];
    }

    List<int>[] WaveAvailableSpawnPoints;
    public List<int> GetHardCodedWave_Available_SPawnIds(int argZeroBAsed)
    {
        return WaveAvailableSpawnPoints[argZeroBAsed];
    }

    List<int>[] WaveAvailableGravePoints;
    public List<int> GetHardCodedWave_Available_GraveIds(int argZeroBAsed)
    {
        return WaveAvailableGravePoints[argZeroBAsed];
    }

    List<int>[] WaveTimesSpawnPrinter;
    public List<int> Get__SPRINTER_TIMES(int argZeroBAsed)
    {
        return WaveTimesSpawnPrinter[argZeroBAsed];
    }

    List<KngEnemyName>[] WaveEnemyNames;
    public List<KngEnemyName> GetHardCodedWave_Enemy_Names(int argZeroBAsed)
    {
        return WaveEnemyNames[argZeroBAsed];
    }
    #endregion


    #region Audio

    public AudioClip AttackBigclipMale;
    public AudioClip AttackSmallclipMale;
    public AudioClip DeathBigclipMale;
    public AudioClip DeathSmallclipMale;
    public AudioClip GrowlBigclipMale;
    public AudioClip GrowlSmallclipMale;
    public AudioClip HurtBigclipMale;
    public AudioClip HurtSmallclipMale;
    public AudioClip MoanBigclipMale;
    public AudioClip MoanSmallclipMale;



    public AudioClip AttackBigclipFemale;
    public AudioClip AttackSmallclipFemale;
    public AudioClip DeathBigclipFemale;
    public AudioClip DeathSmallclipFemale;
    public AudioClip GrowlBigclipFemale;
    public AudioClip GrowlSmallclipFemale;
    public AudioClip HurtBigclipFemale;
    public AudioClip HurtSmallclipFemale;
    public AudioClip MoanBigclipFemale;
    public AudioClip MoanSmallclipFemale;


    public AudioClip AttackBigclipGiant;
    public AudioClip AttackSmallclipGiant;
    public AudioClip DeathBigclipGiant;
    public AudioClip DeathSmallclipGiant;
    public AudioClip GrowlBigclipGiant;
    public AudioClip GrowlSmallclipGiant;
    public AudioClip HurtBigclipGiant;
    public AudioClip HurtSmallclipGiant;
    public AudioClip MoanBigclipGiant;
    public AudioClip MoanSmallclipGiant;


    public AudioClip AttackBigclipCreature;
    public AudioClip AttackSmallclipCreature;
    public AudioClip DeathBigclipCreature;
    public AudioClip DeathSmallclipCreature;
    public AudioClip GrowlBigclipCreature;
    public AudioClip GrowlSmallclipCreature;
    public AudioClip HurtBigclipCreature;
    public AudioClip HurtSmallclipCreature;
    public AudioClip MoanBigclipCreature;
    public AudioClip MoanSmallclipCreature;

    public AudioClip AttackBigclipBone;
    public AudioClip AttackSmallclipBone;
    public AudioClip DeathBigclipBone;
    public AudioClip DeathSmallclipBone;
    public AudioClip GrowlBigclipBone;
    public AudioClip GrowlSmallclipBone;
    public AudioClip HurtBigclipBone;
    public AudioClip HurtSmallclipBone;
    public AudioClip MoanBigclipBone;
    public AudioClip MoanSmallclipBone;

    public AudioClip BoneHeadShotCrushclip;
    public AudioClip Bodyshotclip;
    public AudioClip BoneBodyShotclip;
    public AudioClip BoneSmallBreakclip;
    public AudioClip HeadShotCrushclip;
    public AudioClip FootStepBasicclip;
    public AudioClip FootStepPumpkinclip;


    public AudioClip Impact_Metal_0;
    public AudioClip Impact_Metal_1;
    public AudioClip Impact_Metal_2;
    public AudioClip GlassBreakSmall;
    public AudioClip Buzz100;
    public AudioClip Buzz125;
    public AudioClip Buzz150;
    public AudioClip Buzz175;
    public AudioClip Buzz200;
    public AudioClip Buzz200Cheer;


    public AudioClip Womp;
    public AudioClip Womp2;
    public AudioClip Gong;
    public AudioClip Gong2;
    public AudioClip Slow;
    public AudioClip SlowOff;
    public AudioClip HeartBeat;
    public AudioClip BodyImpact;
    public AudioClip HeadshotImpace;
    public AudioClip Cheer200;
    public AudioClip GameOver;
    public AudioClip Sudden;
    public AudioClip AirHorn;
    public AudioClip A_1;
    public AudioClip A_2;
    public AudioClip A_3;
    public AudioClip A_4;
    public AudioClip A_5;
    public AudioClip A_6;
    public AudioClip A_7;
    public AudioClip A_8;
    public AudioClip A_9;
    public AudioClip A_10;


    public AudioClip Squish0;
    public AudioClip Squish1;
    public AudioClip Squish2;




    #endregion

    public int GetBulletDamage(GunType argGutype)
    {
        int damage = 1;
        switch (argGutype)
        {


            case GunType.MAGNUM:
                damage = 25;   // 4 kills 100hp in chest
                break;
            case GunType.PISTOL:
                damage = 40;   // 5 kills 200hp in chest
                break;

            case GunType.MG61:
                damage = 50;    //  6 bullets kille 300hp 
                break;

            case GunType.UZI:
                damage = 57;    // 7 bullets kills 400hp
                break;



            case GunType.P90:
                damage = 100;
                break;
            case GunType.SHOTGUN:
                damage = 50;
                break;


        }

        return damage;

    }
    public void Set_GlobalTimer(float argtime) { Global_Time_Apocalypse_GameEnds_600s_10m = argtime; }
    public void Set_GlobalTimer(string argtime)
    {

        float FloatTime = float.Parse(argtime);

        Global_Time_Apocalypse_GameEnds_600s_10m = FloatTime;

    }




    #region GamePlaySettings

    //letsplay one wave simulating wave 4 when suddendeath happens
    public float _global_Time_Tournament_GameEnds = 600f;
    private float _global_Time_KidsGameEnds = 240f;
    private float _global_Time_SuddenDeath = 300f;//65f;  //when suddendeath gest called . should be within the sutdden death buffer

    public float Global_Time_Apocalypse_GameEnds_600s_10m
    {
        get { return _global_Time_Tournament_GameEnds; }
        set { _global_Time_Tournament_GameEnds = value; }
    }
    public float Global_Time_SuddenDeath_300s_5min
    {
        get { return _global_Time_SuddenDeath; }
        set { _global_Time_SuddenDeath = value; }
    }
    public float Global_Time_Arcade_GameEnds_240s_4m
    {
        get { return _global_Time_KidsGameEnds; }
        set { _global_Time_KidsGameEnds = value; }
    }


    private string _gameName;
    public string GameName
    {
        get { return _gameName; }
        //set { _gameName = value; }
    }

    private string _gameVersion;
    public string GameVersion
    {
        get { return _gameVersion; }
        //set { _gameVersion = value; }
    }


    public bool UseVive = true;
    //Not selectable via 3d menu
    public bool IsUseHololens() { return _isNOTEditor; } //in gamemanager, if usedDevroom and useHolo, then do the 2 anchor placement system
    public bool IsTournamentModeOn = false;
    public bool IsKidsModeOn = false;
    public bool IsSkipPregameOn = false;
    public bool IsKingstonOn = true;
    public bool IsConsoleDebugOn = false;
    public bool IsLoggerOn = false;
    //3d menue seletable
    public bool IsBloodOn = false;
    public bool IsCanToggleLazerOn = true;
    public bool IsCanSelectReloadON = false;
    public bool IsCanSelectRoomOn = true;
    public bool IsTestModeON = false;
    public bool IsIsGodModeON = true;

    public bool IsSecurityOn = true;
    public bool IsAllowVibrate = false;
    public bool IsCratesOn = false;

    public int curHeadShotNum = 0;
    public int maxHeadShotNum = 4;




    public ARZPregameType PregmeType = ARZPregameType.BRICKWALL;




    public ARZGameModes _gameMode = ARZGameModes.GameRight_Bravo;
    public ARZGameModes GameMode
    {
        get
        {
            if (PlayerPrefs.HasKey("mode"))
            {
                if (PlayerPrefs.GetInt("mode") == 0)
                {

                    return ARZGameModes.GameLeft_Alpha;
                }
                else
                {
                    return ARZGameModes.GameRight_Bravo;
                }
            }
            else
            {
                return _gameMode;
            }
        }
        set
        {
            _gameMode = value;
            if (_gameMode == ARZGameModes.GameLeft_Alpha)
            {
                PlayerPrefs.SetInt("mode", 0);
            }
            else
            {
                PlayerPrefs.SetInt("mode", 1);
            }
        }
    }


    private ARZPlayerLeftyRighty _playerLeftyRight = ARZPlayerLeftyRighty.RightyPlayer;
    public ARZPlayerLeftyRighty PlayerLeftyRight
    {
        get
        {

            if (PlayerPrefs.HasKey("hand"))
            {
                if (PlayerPrefs.GetInt("hand") == 0)
                {

                    return ARZPlayerLeftyRighty.RightyPlayer;
                }
                else
                {
                    return ARZPlayerLeftyRighty.LeftyPlayer;
                }
            }
            else
            {
                return _playerLeftyRight;
            }
        }
        set
        {
            _playerLeftyRight = value;
            if (_playerLeftyRight == ARZPlayerLeftyRighty.RightyPlayer) { PlayerPrefs.SetInt("hand", 0); }
            else { PlayerPrefs.SetInt("hand", 1); }

        }
    }


    private ARZReloadLevel reloadDifficulty = ARZReloadLevel.MEDIUM;
    public ARZReloadLevel ReloadDifficulty
    {
        get
        {
            return reloadDifficulty;
        }

        set
        {
            reloadDifficulty = value;
        }
    }

    public ARZGameSessionType GAmeSessionType = ARZGameSessionType.SINGLE;

    public ARZControlerType _controlertype = ARZControlerType.StemControlSystem;
    public ARZControlerType Controlertype
    {
        get
        {
            return _controlertype;
        }
        set { _controlertype = value; }
    }






    public ARZroom _backroom = ARZroom.HOSPITAL;
    public ARZroom Backroom
    {
        get
        {

            if (PlayerPrefs.HasKey("backroom"))
            {
                if (PlayerPrefs.GetInt("backroom") == 0)
                {
                    _backroom = ARZroom.HOSPITAL;
                    return _backroom;
                }
                else
                if (PlayerPrefs.GetInt("backroom") == 1)
                {
                    _backroom = ARZroom.DESERT;
                    return _backroom;
                }
                else
                {
                    _backroom = ARZroom.FOREST;
                    return _backroom;
                }
            }
            else
            {
                return _backroom;
            }
        }
        set
        {
            _backroom = value;
            if (_backroom == ARZroom.HOSPITAL) { PlayerPrefs.SetInt("backroom", 0); }
            else if (_backroom == ARZroom.DESERT) { PlayerPrefs.SetInt("backroom", 1); }
            else { PlayerPrefs.SetInt("backroom", 2); }

        }
    }

    public float AUTOSTARTGAMEIN = 600.0f;

    public float targetwaitTime = 500.0f;

    //waveManagerBeginNextWave
    //  public float FirstBuffer = 6.0f;

    // WavetimeinSeconds + 20
    //  public float FadeInStartIn = 1.0f;

    // then it takes 1 second to be completely alpha=1
    //  float FadeFinalInStartIn = 5.0f;

    ////float FadeOutStartIn = 1.0f;

    // then it takes 1 second to be completly alpha=0

    // wave completed , start Grapgics I II III IV  in 
    float StartRomanIn = 2f;
    public float Get_StartGet_roman() { return this.StartRomanIn; }


    // waveManager  WaveCompleted_soPopANewOne ->waveManagerBeginNextWave
    //float NextBuffer = 5.0f;
    //float NextBuffer = 2; float StartWaveAgain = 4.0f;
    float NextBuffer = 5f; //4;
    float StartWaveAgain = 8f; // 4f// 10.0f;

    float FadeFinalInStartIn = 0.6f; //1
    public float GET_FadeFinalInStartIn() { return this.FadeFinalInStartIn; }
    //  float StartWaveAgain = 5.0f;
    public float GET_LONGTIME_10seconds() { return StartWaveAgain; }
    public float GET_SHORTTIME_4seconds() { return NextBuffer; }
    public bool USEAllWEapons;




    #endregion





    #region Anchornames
    //ConsoleObject
    //public GameObject E_ConsoleObject;                   //obj.name=ConsoleObject_ES
    string AnchorName_ConsoleObject = "ARZConsoleObject";
    public string GetAnchorName_ConsoleObject() { return AnchorName_ConsoleObject; }

    //StemBase
    //public GameObject E_StemBase;                        //obj.name=StemBase_ES
    // string AnchorName_StemBase = "ARZStemBase";
    //  public string GetAnchorName_StemBase() { return AnchorName_StemBase; }

    //MetalBarrel
    string AnchorName_MetalBarrel = "ARZMetalBarrel";
    public string GetAnchorName_MetalBarrel() { return AnchorName_MetalBarrel; }

    //MetalBarrel
    string AnchorName_RoomModel = "ARZRoomModel";
    public string GetAnchorName_RoomModel() { return AnchorName_RoomModel; }

    //SpawnPoint
    // public GameObject E_SpawnPoint;                     //obj.name=SpawnPoint_ES
    string AnchorName_SpawnPoint = "ARZSpawnPoint";
    public string GetAnchorName_SpawnPoint() { return AnchorName_SpawnPoint; }



    //SpawnPointDummy
    // public GameObject E_SpawnPointDummy;                     //obj.name=SpawnPointDummy_ES
    string AnchorName_SpawnPointDummy = "ARZSpawnPointDummy";
    public string GetAnchorName_SpawnPointDummy() { return AnchorName_SpawnPointDummy; }


    //Barrier
    //public GameObject E_Barrier;                         //obj.name=Barrier_ES
    string AnchorName_Barrier = "ARZBarrier";
    public string GetAnchorName_Barrier() { return AnchorName_Barrier; }


    //ScoreBord
    //public GameObject E_ScoreBoard;                     //obj.name=ScoreBoard_ES
    string AnchorName_ScoreBoard = "ARZScoreBoard";
    public string GetAnchorName_ScoreBoard() { return AnchorName_ScoreBoard; }


    //WeaponRack
    //public GameObject E_WeaponRack;                     //obj.name=WeaponRack_ES
    string AnchorName_WeaponRack = "ARZWeaponRack";
    public string GetAnchorName_WeaponRack() { return AnchorName_WeaponRack; }


    //PistoleMag
    //public GameObject E_PistoleMag;                     //obj.name=PistoleMag_ES
    string AnchorName_PistoleMag = "ARZPistoleMag";
    public string GetAnchorName_PistoleMag() { return AnchorName_PistoleMag; }


    //AmmoBox
    //public GameObject E_AmmoBox;                           //obj.name=AmmoBox_ES
    string AnchorName_AmmoBox = "ARZAmmoBox";
    public string GetAnchorName_AmmoBox() { return AnchorName_AmmoBox; }


    //AmmoBoxInfinite
    //public GameObject E_AmmoBoxInfinite;                 //obj.name=AmmoBoxInfinite_ES
    string AnchorName_AmmoBoxInfinite = "ARZAmmoBoxInfinite";
    public string GetAnchorName_AmmoBoxInfinite() { return AnchorName_AmmoBoxInfinite; }


    //PathFinder
    //public GameObject E_PathFinder;                    //obj.name=PathFinder_ES
    string AnchorName_PathFinder = "ARZPathFinder";
    public string GetAnchorName_PathFinder() { return AnchorName_PathFinder; }


    //WalkieTalkie
    //public GameObject E_WalkieTalkie;                    //obj.name=WalkieTalkie_ES
    string AnchorName_WalkieTalkie = "ARZWalkieTalkie";
    public string GetAnchorName_WalkieTalkie() { return AnchorName_WalkieTalkie; }


    //MistEmitter
    //public GameObject E_MistEmitter;                     //obj.name=MistEmitter_ES
    string AnchorName_MistEmitter = "ARZMistEmitter";
    public string GetAnchorName_MistEmitter() { return AnchorName_MistEmitter; }


    //mISTeND
    //public GameObject E_MistEnd;                        //obj.name=MistEnd_ES
    string AnchorName_MistEnd = "ARZMistEnd";
    public string GetAnchorName_MistEnd() { return AnchorName_MistEnd; }

    //HotSpot
    //public GameObject E_HotSpot;                        //obj.name=HotSpot_ES
    string AnchorName_HotSpot = "ARZHotSpot";
    public string GetAnchorName_HotSpot() { return AnchorName_HotSpot; }


    //AirStrikeStart
    //public GameObject E_AirStrikeStart;                        //obj.name=AirStrikeStart_ES
    string AnchorName_AirStrikeStart = "ARZAirStrikeStart";

    public string GetAnchorName_AirStrikeStart() { return AnchorName_AirStrikeStart; }

    //AirStrikeEnd
    //public GameObject E_AirStrikeEnd;                        //obj.name=AirStrikeEnd_ES
    string AnchorName_AirStrikeEnd = "ARZAirStrikeEnd";
    public string GetAnchorName_AirStrikeEnd() { return AnchorName_AirStrikeEnd; }

    string AnchorName_Target = "ARZTarget";
    public string GetAnchorName_Target() { return AnchorName_Target; }



    string AnchorName_ZombiePregame = "ARZZombiePregame";
    public string GetAnchorName_ZombiePregame() { return AnchorName_ZombiePregame; }


    string AnchorName_StartButton = "ARZStartButton";
    public string GetAnchorName_StartButton() { return AnchorName_StartButton; }

    string AnchorName_Tutorial1 = "ARZCompuck";
    public string GetAnchorName_Tutorial1() { return AnchorName_Tutorial1; }

    string AnchorName_TutoTargLeft = "ARZTutoTargLeft";
    public string GetAnchorName_TutoTargLeft() { return AnchorName_TutoTargLeft; }

    string AnchorName_TutoTargRight = "ARZTutoTargRight";
    public string GetAnchorName_TutoTargRight() { return AnchorName_TutoTargRight; }

    string AnchorName_Ball = "ARZBall";
    public string GetAnchorName_Ball() { return AnchorName_Ball; }

    string AnchorName_RoomBack = "ARZRoomBack";
    public string GetAnchorName_RoomBack() { return AnchorName_RoomBack; }

    string AnchorName_RoomFront = "ARZRoomFront";
    public string GetAnchorName_RoomFront() { return AnchorName_RoomFront; }



    string _anchorName_ArrowRed = "ancred";
    public string AncName_ArrowRed() { return _anchorName_ArrowRed; }
    string _anchorName_ArrowBlue = "ancblue";
    public string AncName_ArrowBlue() { return _anchorName_ArrowBlue; }
    string _anchorName_ArrowGreen = "ancgreen";
    public string AncName_ArrowGreen() { return _anchorName_ArrowGreen; }
    string _anchorName_ArrowYellow = "ancyellow";
    public string AncName_ArrowYellow() { return _anchorName_ArrowYellow; }
    string _anchorName_ArenaBase = "ancarena";
    public string AncName_ArenaBase() { return _anchorName_ArenaBase; }
    string _anchorName_ArenaHotspot = "hotspot";
    public string AncName_ArenaHotSpotBase() { return _anchorName_ArenaHotspot; }
    string _anchorName_ArenaPillar = "ancpillar";
    public string AncName_ArenaPillar() { return _anchorName_ArenaPillar; }
    string _anchorName_ArenaStemBase = "ancstem";
    public string AncName_ArenaStemBase() { return _anchorName_ArenaStemBase; }
    string _anchorName_ArenaPlayerBase = "ancplayer";
    public string AncName_ArenaPlayerBase() { return _anchorName_ArenaPlayerBase; }
    string _anchorName_ArenaScoreBase = "ancscore";
    public string AncName_ArenaScoreBase() { return _anchorName_ArenaScoreBase; }
    string _anchorName_ArenaZombieSpawnBase = "anczombiespawn";
    public string AncName_ArenaZombieSpawnBase() { return _anchorName_ArenaZombieSpawnBase; }
    string _anchorName_ArenaGraveSpawnBase = "ancgravespawn";
    public string AncName_ArenaGraveSpawnBase() { return _anchorName_ArenaGraveSpawnBase; }
    string _anchorName_ArenaFlySpawnBase = "ancflyspawn";
    public string AncName_ArenaFlySpawnBase() { return _anchorName_ArenaFlySpawnBase; }
    string _anchorName_ArenaRoofSpawnBase = "ancroofpawnbase";
    public string AncName_ArenRoofSpawnBase() { return _anchorName_ArenaRoofSpawnBase; }
    string _anchorName_ArenaWallBase = "ancwall";
    public string AncName_ArenaWallBase() { return _anchorName_ArenaWallBase; }
    string _anchorName_ArenaWayPoints = "ancwaypoint";
    public string AncName_ArenaWayPointBase() { return _anchorName_ArenaWayPoints; }
    string _anchorName_WindowBasedLand = "windowbasedland";
    public string AncName_WindowBasedLand() { return _anchorName_WindowBasedLand; }
    #endregion



    string MSG_crateUpdate = "crate";
    public string GetMSG_crateUpdate() { return MSG_crateUpdate; }

    string MSG_smallAttack = "smallAt0";
    public string GetMSG_smallAttack() { return MSG_smallAttack; }


    string MSG_BigAttack = "bigAtta0";
    public string GetMSG_BigAttack() { return MSG_BigAttack; }


    string MSG_AudioFire = "firebang0";
    public string GetMSG_AudioFire() { return MSG_AudioFire; }


    string MSG_AudioReload = "reloadi0";
    public string GetMSG_AudioReload() { return MSG_AudioReload; }


    string MSG_AudioDry = "dryrelo0";
    public string GetMSG_AudioDry() { return MSG_AudioDry; }

    string MSG_StartGame = "startga0";
    public string GetMSG_StartGame() { return MSG_StartGame; }





    string STR_PortInternal_Alpha = "50201";
    public string GetSTR_PortInternal_Alpha() { return STR_PortInternal_Alpha; }


    string STR_PortInternal_Bravo = "50202";
    public string GetSTR_PortInternal_Bravo() { return STR_PortInternal_Alpha; }

    string STR_Port_Audio = "7791";
    public string GetSTR_STR_Port_Audio() { return STR_Port_Audio; }


    string STR_Port_Server = "50204";
    public string GetSTR_STR_Port_Server() { return STR_Port_Server; }

    string STR_IP_Alpha = "192.168.8.6";//kng store alpha
    public string GetSTR_IP_Alpha() { return STR_IP_Alpha; }

    string STR_IP_Bravo = "192.168.8.8";//kng store bravo
    public string GetSTR_IP_Bravo() { return STR_IP_Bravo; }

    string STR_IP_Audio = "192.168.8.10";
    public string GetSTR_IP_Audio() { return STR_IP_Audio; }

    string STR_IP_Server = "192.168.8.148";//kng store pc
    public string GetSTR_IP_Server() { return STR_IP_Server; }


    #region UDPnetworking



    //********************************************************************************************************
    private string _my_ip;
    public string My_IP
    {
        get { return _my_ip; }
        set { _my_ip = value; }
    }

    private string _internal_Port = "";
    public string Port_Internal
    {
        //get { return _internal_Port; }
        //set { _internal_Port = value; }
        get
        {
            if (PlayerPrefs.HasKey("internal_Port"))
            {
                _internal_Port = PlayerPrefs.GetString("internal_Port");
                return _internal_Port;
            }
            else
            {
                return _internal_Port;
            }
        }
        set
        {
            _internal_Port = value;
            PlayerPrefs.SetString("internal_Port", _internal_Port);
        }
    }

    private string _external_IP_OtherHL = "";
    public string Ip_External_OtherHL
    {
        //get { return _external_IP_OtherHL; }
        //set { _external_IP_OtherHL = value; }
        get
        {
            if (PlayerPrefs.HasKey("external_IP_OtherHL"))
            {
                _external_IP_OtherHL = PlayerPrefs.GetString("external_IP_OtherHL");
                return _external_IP_OtherHL;
            }
            else
            {
                return _external_IP_OtherHL;
            }
        }
        set
        {
            _external_IP_OtherHL = value;
            PlayerPrefs.SetString("external_IP_OtherHL", _external_IP_OtherHL);
        }
    }

    private string _external_Port_OtherHL = "";
    public string Port_External_OtherHL
    {
        //  get { return _external_Port_OtherHL; }
        // set { _external_Port_OtherHL = value; }
        get
        {
            if (PlayerPrefs.HasKey("external_Port_OtherHL"))
            {
                _external_Port_OtherHL = PlayerPrefs.GetString("external_Port_OtherHL");
                return _external_Port_OtherHL;
            }
            else
            {
                return _external_Port_OtherHL;
            }
        }
        set
        {
            _external_Port_OtherHL = value;
            PlayerPrefs.SetString("external_Port_OtherHL", _external_Port_OtherHL);
        }

    }


    private string _external_IP_AudioServer = "";
    public string Ip_External_AudioServer
    {
        //get { return _external_IP_AudoServer; }
        //set { _external_IP_AudoServer = value; }


        get
        {
            if (PlayerPrefs.HasKey("external_IP_AudioServer"))
            {
                _external_IP_AudioServer = PlayerPrefs.GetString("external_IP_AudioServer");
                return _external_IP_AudioServer;
            }
            else
            {
                return _external_IP_AudioServer;
            }
        }
        set
        {
            _external_IP_AudioServer = value;
            PlayerPrefs.SetString("external_IP_AudioServer", _external_IP_AudioServer);
        }
    }

    private string _external_IP_ScoreServer = "";
    public string Ip_External_ScoreServer
    {
        get
        {
            if (PlayerPrefs.HasKey("external_IP_ScoreServer"))
            {
                _external_IP_ScoreServer = PlayerPrefs.GetString("external_IP_ScoreServer");
                return _external_IP_ScoreServer;
            }
            else
            {
                return _external_IP_ScoreServer;
            }
        }
        set
        {
            _external_IP_ScoreServer = value;
            PlayerPrefs.SetString("external_IP_ScoreServer", _external_IP_ScoreServer);
        }
    }

    private string _external_Port_AudioServer = "";
    public string Port_External_AudioServer
    {
        get
        {
            if (PlayerPrefs.HasKey("external_Port_AudioServer"))
            {
                _external_Port_AudioServer = PlayerPrefs.GetString("external_Port_AudioServer");
                return _external_Port_AudioServer;
            }
            else
            {
                return _external_Port_AudioServer;
            }
        }
        set
        {
            _external_Port_AudioServer = value;
            PlayerPrefs.SetString("external_Port_AudioServer", _external_Port_AudioServer);
        }

    }


    private string _external_Port_ScoreServer = "";
    public string Port_External_ScoreServer
    {
        get
        {
            if (PlayerPrefs.HasKey("external_Port_ScoreServer"))
            {
                _external_Port_ScoreServer = PlayerPrefs.GetString("external_Port_ScoreServer");
                return _external_Port_ScoreServer;
            }
            else
            {
                return _external_Port_ScoreServer;
            }
        }
        set
        {
            _external_Port_ScoreServer = value;
            PlayerPrefs.SetString("external_Port_ScoreServer", _external_Port_ScoreServer);
        }

    }

    //********************************************************************************************************
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Tab))
    //    {
    //        if (GameEventsManager.Instance!=null) {
    //            GameEventsManager.Instance.Call_OtherHLSmallAttack();
    //        } }
    //}



    #endregion




    #region ColorUtils
    List<Color> PathColors;
    void InitColors()
    {
        PathColors = new List<Color>();
        ColorIndex = 0;
        PathColors.Add(Color.red);
        PathColors.Add(Color.green);
        PathColors.Add(Color.blue);
        PathColors.Add(Color.yellow);
        PathColors.Add(Color.cyan);
        PathColors.Add(Color.magenta);
        PathColors.Add(Color.gray);
        PathColors.Add(Color.white);
    }
    int ColorIndex = 0;
    public Color GETRand_color()
    {
        Color c = new Color();
        c = UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        return c;
    }
    public Color GetNExtColor()
    {
        ColorIndex++;
        if (ColorIndex >= PathColors.Count)
        {
            ColorIndex = 0;
        }

        return PathColors[ColorIndex];
    }
    #endregion


    #region EnemyVertCount

    public int GetEnemyModel_Verts_FullRez(KngEnemyName _argZname)
    {
        int verts_FULLREZ = 0;
        int verts_lowrez = 0;
        int tris_lowrez = 0;
        int tris_FULLREZ = 0;

        switch (_argZname)
        {
            case KngEnemyName.NONE:
                verts_FULLREZ = 0;
                verts_lowrez = 0;
                tris_lowrez = 0;
                tris_FULLREZ = 0;
                break;
            case KngEnemyName.BASIC:
                verts_FULLREZ = 1736;
                tris_FULLREZ = 2547;
                verts_lowrez = 330;
                tris_lowrez = 294;
                break;
            case KngEnemyName.SPINAL:
                verts_FULLREZ = 2269;
                tris_FULLREZ = 2968;
                verts_lowrez = 539;
                tris_lowrez = 394;
                break;
            case KngEnemyName.SHITFACE:
                verts_FULLREZ = 2032;
                tris_FULLREZ = 3133;
                verts_lowrez = 864;
                tris_lowrez = 999;
                break;
            case KngEnemyName.HOODIE:
                verts_FULLREZ = 1984;
                tris_FULLREZ = 3063;
                verts_lowrez = 249;
                tris_lowrez = 207;
                break;
            case KngEnemyName.PAUL:
                verts_FULLREZ = 4153;
                tris_FULLREZ = 4631;
                verts_lowrez = 390;
                tris_lowrez = 457;
                break;
            case KngEnemyName.NAKEDPAUL:
                verts_FULLREZ = 2232;
                tris_FULLREZ = 2698;
                verts_lowrez = 364;
                tris_lowrez = 217;
                break;
            case KngEnemyName.ABOMINATION:
                int hair_verts = 659;
                int hair_tris = 534;
                verts_FULLREZ = 1084;
                tris_FULLREZ = 1348;
                verts_lowrez = 471;
                tris_lowrez = 453;
                break;
            case KngEnemyName.SARAHCONNOR:
                verts_FULLREZ = 2674;
                tris_FULLREZ = 4126;
                verts_lowrez = 323;
                tris_lowrez = 317;
                break;
            case KngEnemyName.CAROL:
                int hairback_verts = 112;
                int hairback_tris = 107;
                int hairfront_verts = 155;
                int hairfront_tris = 148;
                verts_FULLREZ = 1840;
                tris_FULLREZ = 3015;
                verts_lowrez = 338;
                tris_lowrez = 189;
                break;
            case KngEnemyName.MUMMY:
                verts_FULLREZ = 1925;
                tris_FULLREZ = 2884;
                verts_lowrez = 183;
                tris_lowrez = 172;
                break;
            case KngEnemyName.SKELETON:
                verts_FULLREZ = 909;
                tris_FULLREZ = 1308;
                verts_lowrez = 212;
                tris_lowrez = 222;
                break;
            case KngEnemyName.HILLBILLY:
                verts_FULLREZ = 1693;
                tris_FULLREZ = 2448;
                verts_lowrez = 403;
                tris_lowrez = 376;
                break;
            case KngEnemyName.SWEATER:
                verts_FULLREZ = 2619;
                tris_FULLREZ = 4129;
                verts_lowrez = 268;
                tris_lowrez = 251;
                break;
            case KngEnemyName.GASMASK:
                verts_FULLREZ = 1933;
                tris_FULLREZ = 3018;
                verts_lowrez = 301;
                tris_lowrez = 316;
                break;
            case KngEnemyName.BIGMUTANT:
                verts_FULLREZ = 2150;
                tris_FULLREZ = 3330;
                verts_lowrez = 386;
                tris_lowrez = 392;
                break;
            case KngEnemyName.KNIFEFIGHTER:
                verts_FULLREZ = 1424;
                tris_FULLREZ = 2168;
                verts_lowrez = 301;
                tris_lowrez = 300;
                break;
            case KngEnemyName.CHECKER:
                verts_FULLREZ = 2104;
                tris_FULLREZ = 3161;
                verts_lowrez = 290;
                tris_lowrez = 270;
                break;
            case KngEnemyName.MEATHEADBLACK:
                verts_FULLREZ = 2255;
                tris_FULLREZ = 3717;
                verts_lowrez = 273;
                tris_lowrez = 283;
                break;
            case KngEnemyName.MEATHEADWHITE:
                verts_FULLREZ = 1227;
                tris_FULLREZ = 1888;
                verts_lowrez = 797;
                tris_lowrez = 1107;
                break;
            case KngEnemyName.LADYPANTS:
                int ladypants_hair_verts = 243;
                int ladypants_hair_tris = 196;
                verts_FULLREZ = 1710;
                tris_FULLREZ = 2572;
                verts_lowrez = 362;
                tris_lowrez = 340;
                break;
            case KngEnemyName.MAW:
                verts_FULLREZ = 705;
                tris_FULLREZ = 8620;
                verts_lowrez = 622;
                tris_lowrez = 1864;
                break;
            case KngEnemyName.PUMPKIN:
                verts_FULLREZ = 4858;
                tris_FULLREZ = 8258;
                verts_lowrez = 1553;
                tris_lowrez = 1820;
                break;
            case KngEnemyName.BEAR:
                verts_FULLREZ = 4716;
                tris_FULLREZ = 7025;
                verts_lowrez = 2094;
                tris_lowrez = 2311;
                break;
            case KngEnemyName.MUTANTBLUE:
                verts_FULLREZ = 1691;
                tris_FULLREZ = 8533;
                verts_lowrez = 1821;
                tris_lowrez = 5410;
                break;
            case KngEnemyName.PARASITE:
                verts_FULLREZ = 6047;
                tris_FULLREZ = 9582;
                verts_lowrez = 1879;
                tris_lowrez = 2082;
                break;
            case KngEnemyName.AXEMAN:
                int bigKnife_verts = 77; // x2
                int bigKnife_tris = 100; // x2
                int L_Eye_vert = 182;
                int L_Eye_tris = 66;
                int Eyes_vert = 46;
                int Eyes_tris = 66;
                verts_FULLREZ = 2425; //1736+330
                tris_FULLREZ = 3388;
                verts_lowrez = 333; //not yet but good goal 
                tris_lowrez = 333; //not yet but good goal
                break;
            case KngEnemyName.FLY1:
                int wings_vert = 50;
                int wings_tris = 64;
                int jaw_vert = 177;
                int jaw_tris = 174;
                int eyes_vert = 86;
                int eyes_tris = 140;
                verts_FULLREZ = 433; //been deicmated
                verts_lowrez = 458;
                tris_lowrez = 0;
                tris_FULLREZ = 0;
                break;
            default:
                verts_FULLREZ = 0;
                verts_lowrez = 0;
                tris_lowrez = 0;
                tris_FULLREZ = 0;
                break;
        }

        return verts_FULLREZ + verts_lowrez;
    }
    #endregion

}

//public bool IsZombieSightOn = true;
//public bool IsGameLong = true;
//public bool IsHideBulletsPaths = true;
//public bool IsZombieRootMotionOn = true; //IsZombieSightModeOn
//public bool IsSinglePlayer = true;
//public bool IsUdpGameSession = false;
//public bool IsMultiPlayer = false;
