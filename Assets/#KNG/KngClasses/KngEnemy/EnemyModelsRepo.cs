using UnityEngine;

public class EnemyModelsRepo : MonoBehaviour
{

    public GameObject EnemyBlank;
    public GameObject EnemyBox;
    public GameObject Enemy1_Basic;
    public GameObject Enemy2_Spinal;
    public GameObject Enemy3_Shitface;
    public GameObject Enemy4_Hoodie;
    public GameObject Enemy5_Paul;
    public GameObject Enemy6_NakedPaul;
    public GameObject Enemy7_Abomination;
    public GameObject Enemy8_SarahConnor;
    public GameObject Enemy9_Carol;
    public GameObject Enemy10_Mummy;
    public GameObject Enemy11_Skeleton;
    public GameObject Enemy12_HillBilly;
    public GameObject Enemy13_Sweater;
    public GameObject Enemy14_GasMask;
    public GameObject Enemy15_BigMutant;
    public GameObject Enemy16_KnifeFighter;
    public GameObject Enemy17_Checker;
    public GameObject Enemy18_BlackMeatHead;
    public GameObject Enemy19_WhiteMeatHead;
    public GameObject Enemy20_LadyPants;
    public GameObject Enemy21_Maw;
    public GameObject Enemy22_Pumpkin;
    public GameObject Enemy23_Bear;
    public GameObject Enemy24_MutantBlue;
    public GameObject Enemy25_Parasite;
    public GameObject Enemy26_AxeGuy;
    public GameObject Enemy27_FlyRed;
    public GameObject Enemy28_AxeGuyv2;
    public GameObject AnyAxeModel;
    public GameObject GuyleDynamic;
    public GameObject AxeStatic;
    public GameObject AxeDynamic;
    public GameObject AxeDynamic2;
    public GameObject FireBall;
    public GameObject Player2Zombie;
    public GameObject Enemy29_PumpkinShort;
    public GameObject EnemyHolobones;

    public GameObject DirtEffectObj;
    public GameObject SharedEffectObj;

    public GameObject BloodEffect1;
    public GameObject BloodEffect2;
    public GameObject BloodEffect3;
    public GameObject BloodEffect4;

    public GameObject GunProp911;
    public GameObject GunPropMAgnum;
    public GameObject GunPropUzi;
    public GameObject GunPropMG61;
    public GameObject GunPropP90;
    public GameObject GunPropShotgun;


    public GameObject Shared_Shotgun_tinyparticules;
    public GameObject Shared_Shotgun_BIGiumparticules;


    public GameObject Shared_Autoguns_tinyparticules;
    public GameObject Shared_Autoguns_BIGiumparticules;


    public GameObject Shared_pistols_tinyparticules;
    public GameObject Shared_pistols_BIGiumparticules;

    public GameObject Bulltrail1;
    public GameObject Bulltrail2;
    public GameObject Bulltrail3;

    public GameObject GetTrail(int _1_2_3)
    {

        GameObject Goref = null;
        switch (_1_2_3)
        {
            case 1:
                Goref = Bulltrail1;
                break;
            case 2:
                Goref = Bulltrail2;
                break;
            case 3:
                Goref = Bulltrail3;
                break;
            default:
                Debug.LogError("no match found");
                break;
        }

        return Goref;
    }

    public GameObject GetShatter_Ref(GunType argGunType, bool isBig)
    {
        // Debug.Log("find shatter " + argGunType.ToString() + " big " + isBig.ToString());
        GameObject Goref = null;
        switch (argGunType)
        {
            case GunType.PISTOL:


                if (isBig) { Goref = Shared_pistols_BIGiumparticules; }
                else
                { Goref = Shared_pistols_tinyparticules; }




                break;
            case GunType.MAGNUM:
                if (isBig) { Goref = Shared_pistols_BIGiumparticules; }
                else
                { Goref = Shared_pistols_tinyparticules; }


                break;
            case GunType.UZI:
                if (isBig) { Goref = Shared_Autoguns_BIGiumparticules; }
                else
                { Goref = Shared_Autoguns_tinyparticules; }


                break;
            case GunType.MG61:
                if (isBig) { Goref = Shared_Autoguns_BIGiumparticules; }
                else
                { Goref = Shared_Autoguns_tinyparticules; }
                break;
            case GunType.P90:
                if (isBig) { Goref = Shared_Autoguns_BIGiumparticules; }
                else
                { Goref = Shared_Autoguns_tinyparticules; }
                break;
            case GunType.SHOTGUN:
                if (isBig) { Goref = Shared_Shotgun_BIGiumparticules; }
                else
                { Goref = Shared_Shotgun_tinyparticules; }
                break;

            default:
                Debug.LogError("no match found");
                break;
        }

        return Goref;
    }

    public GameObject GetBlood_Ref(GunType argGunType, bool isBig)
    {
        // Debug.Log("find shatter " + argGunType.ToString() + " big " + isBig.ToString());
        GameObject Goref = null;
        switch (argGunType)
        {
            case GunType.PISTOL:


                if (isBig) { Goref = BloodEffect1; }
                else
                { Goref = BloodEffect3; }
                break;

            case GunType.MAGNUM:
                if (isBig) { Goref = BloodEffect1; }
                else
                { Goref = BloodEffect4; }


                break;
            case GunType.UZI:
                if (isBig) { Goref = BloodEffect2; }
                else
                { Goref = BloodEffect3; }


                break;
            case GunType.MG61:
                if (isBig) { Goref = BloodEffect2; }
                else
                { Goref = BloodEffect3; }
                break;
            case GunType.P90:
                if (isBig) { Goref = BloodEffect3; }
                else
                { Goref = BloodEffect4; }
                break;
            case GunType.SHOTGUN:
                if (isBig) { Goref = BloodEffect2; }
                else
                { Goref = BloodEffect4; }
                break;

            default:
                Debug.LogError("no match found");
                break;
        }

        return Goref;
    }



    public GameObject GetGunModel_Ref(GunType argGunType)
    {

        GameObject Goref = null;
        switch (argGunType)
        {
            case GunType.PISTOL:
                Goref = GunProp911;
                break;
            case GunType.MAGNUM:
                Goref = GunPropMAgnum;
                break;
            case GunType.UZI:
                Goref = GunPropUzi;
                break;
            case GunType.MG61:
                Goref = GunPropMG61;
                break;
            case GunType.P90:
                Goref = GunPropP90;
                break;
            case GunType.SHOTGUN:
                Goref = GunPropShotgun;
                break;



            default:
                Debug.LogError("no match found");
                break;
        }

        return Goref;
    }
    public GameObject GetEnemyModel_Ref(KngEnemyName _argZname)
    {

        GameObject Goref = null;
        switch (_argZname)
        {
            case KngEnemyName.Blank:
                Goref = EnemyBlank;
                break;
            case KngEnemyName.Box:
                Goref = EnemyBox;
                break;
            case KngEnemyName.BASIC:
                Goref = Enemy1_Basic;
                break;
            case KngEnemyName.SPINAL:
                Goref = Enemy2_Spinal;
                break;
            case KngEnemyName.SHITFACE:
                Goref = Enemy3_Shitface;
                break;
            case KngEnemyName.HOODIE:
                Goref = Enemy4_Hoodie;
                break;
            case KngEnemyName.PAUL:
                Goref = Enemy5_Paul;
                break;
            case KngEnemyName.NAKEDPAUL:
                Goref = Enemy6_NakedPaul;
                break;
            case KngEnemyName.ABOMINATION:
                Goref = Enemy7_Abomination;
                break;
            case KngEnemyName.SARAHCONNOR:
                Goref = Enemy8_SarahConnor;
                break;
            case KngEnemyName.CAROL:
                Goref = Enemy9_Carol;
                break;
            case KngEnemyName.MUMMY:
                Goref = Enemy10_Mummy;
                break;
            case KngEnemyName.SKELETON:
                Goref = Enemy11_Skeleton;
                break;
            case KngEnemyName.HILLBILLY:
                Goref = Enemy12_HillBilly;
                break;
            case KngEnemyName.SWEATER:
                Goref = Enemy13_Sweater;
                break;
            case KngEnemyName.GASMASK:
                Goref = Enemy14_GasMask;
                break;
            case KngEnemyName.BIGMUTANT:
                Goref = Enemy15_BigMutant;
                break;
            case KngEnemyName.KNIFEFIGHTER:
                Goref = Enemy16_KnifeFighter;
                break;
            case KngEnemyName.CHECKER:
                Goref = Enemy17_Checker;
                break;
            case KngEnemyName.MEATHEADBLACK:
                Goref = Enemy18_BlackMeatHead;
                break;
            case KngEnemyName.MEATHEADWHITE:
                Goref = Enemy19_WhiteMeatHead;
                break;
            case KngEnemyName.LADYPANTS:
                Goref = Enemy20_LadyPants;
                break;
            case KngEnemyName.MAW:
                Goref = Enemy21_Maw;
                break;
            case KngEnemyName.PUMPKIN:
                Goref = Enemy22_Pumpkin;
                break;
            case KngEnemyName.BEAR:
                Goref = Enemy23_Bear;
                break;
            case KngEnemyName.MUTANTBLUE:
                Goref = Enemy24_MutantBlue;
                break;
            case KngEnemyName.PARASITE:
                Goref = Enemy25_Parasite;
                break;
            case KngEnemyName.AXEMAN:
                Goref = Enemy26_AxeGuy;
                break;
            case KngEnemyName.FLY1:
                Goref = Enemy27_FlyRed;
                break;
            case KngEnemyName.axvmanv2:
                Goref = Enemy28_AxeGuyv2;
                break;
            case KngEnemyName.PumpkinShort:
                Goref = Enemy29_PumpkinShort;
                break;
            case KngEnemyName.HOLOBONES:
                Goref = EnemyHolobones;
                break;

            default:
                Goref = EnemyBlank;
                break;
        }

        return Goref;
    }
    private void Start()
    {
        //CleanAll();
    }
    //void CleanAll() {

    //    for(int x =1; x< 2; x++) {

    //     GameObject go=   GetEnemyModel_Ref((KngEnemyName)x);
    //        EnemyMeshContorl im = go.GetComponent<EnemyMeshContorl>();
    //        im.DeepSearchRemoveLAyer(go.transform);
    //    }
    //}
}
