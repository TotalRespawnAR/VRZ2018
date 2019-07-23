using UnityEngine;

public class PlayerHandsCTRL : MonoBehaviour //, IPlayerHandsCTRL
{
    private void Start()
    {
        Debug.Log("playerhandctrl is on " + gameObject.name);
    }

    //#region PublicProps
    //MainHandScript MAinHandScript;

    //public TextMesh _meshGunStatus;
    //#endregion
    //#region PrivateProps
    //bool isPlayerAllowedToUseStemInput = false;

    //HoloToolkit.Unity.UAudioManager _uaudio;
    //bool _CanStartLocatingTrackers = false;
    //bool _allowExtraButtonUsage = true;



    //private IGun _curEquippedIGun;

    //#endregion


    //public void InitPlayerHandsScripts(MainHandScript argMAIN)
    //{
    //    MAinHandScript = argMAIN;

    //}
    //public void InitPlayerHandScripts(MainHandScript argMAIN)
    //{
    //    MAinHandScript = argMAIN;
    // }
    //#region Events
    //private void OnEnable()
    //{
    //   // StemKitMNGR.OnUpdateCurIGun += Handle_SetCurEquippedGun;
    //    StemKitMNGR.OnGunSetChanged += Handle_GunSetChanged_EQUIP;
    //   // StemKitMNGR.OnUpdateAvailableGUnIndex += Handle_UpdateLatestUnlockedGunEnumIndex;
    //    StemKitMNGR.OnToggleONOFFExtraButtons += Handle_ToggleAllowExtraButtons;
    //}
    //private void OnDisable()
    //{
    //    StemKitMNGR.OnGunSetChanged -= Handle_GunSetChanged_EQUIP;
    //  //  StemKitMNGR.OnUpdateCurIGun -= Handle_SetCurEquippedGun;
    //    //StemKitMNGR.OnUpdateAvailableGUnIndex -= Handle_UpdateLatestUnlockedGunEnumIndex;
    //    StemKitMNGR.OnStartFindingTrackers -= Handle_StartFindingTrackers;
    //    StemKitMNGR.OnToggleONOFFExtraButtons -= Handle_ToggleAllowExtraButtons;
    //    StemKitMNGR.OnToggleInputs -= Handle_ToggleAllowStemInputs;

    //    //GameManager.OnGamePaused -= Handle_ToggleAllowStemInputs;
    //    //GameManager.OnGameContinue -= Handle_ToggleAllowStemInputs;
    //}
    //#endregion

    //#region EventsHandlers

    //void Handle_GunSetChanged_EQUIP(GunType gunType)
    //{
    //    //Debug.Log("C H A N G E ");
    //    Equips_GunInMainHand(gunType);
    //}





    ////StemkitMNGR.Start() -> INitHandsBundles()
    //void Handle_StartFindingTrackers(bool argBool)
    //{
    //    _CanStartLocatingTrackers = argBool;
    //}

    //void Handle_ToggleAllowExtraButtons(bool argOnOff)
    //{
    //    _allowExtraButtonUsage = argOnOff;
    //}


    // void Handle_ToggleAllowStemInputs(bool argb) { isPlayerAllowedToUseStemInput = argb; }


    //#endregion

    //private void Start()
    //{
    //    isPlayerAllowedToUseStemInput = false;
    //    _uaudio = GetComponent<UAudioManager>();
    //}

    //// tracking meter // evrytime i put a gun in my hand(bundle gun enable) -> I endup making gunbundles pass a reff to metergo to the gun activated
    //void Equips_GunInMainHand(GunType gunType)
    //{
    //    MAinHandScript.Equip_byEquipmentIndex((int)gunType);
    //}

}
