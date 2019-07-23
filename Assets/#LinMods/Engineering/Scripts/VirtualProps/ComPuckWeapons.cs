using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ComPuckWeapons : MonoBehaviour
{

    // public TextToSpeech tts;
    private void OnEnable()
    {
        GameEventsManager.OnWaveStartedOrReset_DEO += StartDectivating_onHEardWaveStarted;
    }
    private void OnDisable()
    {
        GameEventsManager.OnWaveStartedOrReset_DEO -= StartDectivating_onHEardWaveStarted;
    }

    void StartDectivating_onHEardWaveStarted(WaveLevel dummy)
    {
        _isDeployin_DOWN = false;
        DEActivete_and_SetWeaponToShow();
    }
    public Transform HologramRaysObj;
    public GameObject WeaponBundle;
    public Text txt;

    WeaponShowcase _WSC;
    float HologramScale = 0.01f;

    public float SWAP_SPEED = 1.0f;
    float CurrentTime = 0.0f;
    // Use this for initialization
    void Start()
    {
        _WSC = WeaponBundle.GetComponent<WeaponShowcase>();
        HologramRaysObj.localScale = HologramScale * Vector3.one;
    }

    bool _isDeployingUp = false;
    bool _isDeployin_DOWN = false;


    void DeployHoloCone2(/*void*/)
    {
        //GameManager.Instance.MistsHIdeShow(false);

        CurrentTime += Time.deltaTime * SWAP_SPEED;
        if (HologramScale < 2.0f)
        {
            HologramScale = CurrentTime / 2f;
            if (HologramRaysObj != null)
            {
                HologramRaysObj.gameObject.SetActive(HologramScale > 0.05f);
                HologramRaysObj.localScale = HologramScale * Vector3.one;
                WeaponBundle.transform.localScale = HologramScale * Vector3.one * 2;
            }

            Vector3 RaysScale = HologramRaysObj.transform.localScale;
            RaysScale.x = HologramScale;
            RaysScale.z = HologramScale;
            if (HologramRaysObj != null)
            {
                HologramRaysObj.transform.localScale = RaysScale;
            }

            //Debug.Log(HologramScale);
        }

        if (HologramScale >= 2.0f)
        {
            //  Debug.Log("REACHED?");
            _isDeployingUp = false;
            return;
        }
    }

    void ImployHoloCone2(/*void*/)
    {
        CurrentTime -= Time.deltaTime * SWAP_SPEED;
        if (HologramScale > 0.01f)
        {
            HologramScale = CurrentTime / 2f;
            if (HologramRaysObj != null)
            {
                HologramRaysObj.gameObject.SetActive(HologramScale > 0.05f);
                HologramRaysObj.localScale = HologramScale * Vector3.one;
                WeaponBundle.transform.localScale = HologramScale * Vector3.one * 2;

            }

            Vector3 RaysScale = HologramRaysObj.transform.localScale;
            RaysScale.x = HologramScale;
            RaysScale.z = HologramScale;
            if (HologramRaysObj != null)
            {
                HologramRaysObj.transform.localScale = RaysScale;
            }

            //Debug.Log(HologramScale);
        }

        if (HologramScale <= 0.01f)
        {
            //Debug.Log("REACHED down?");
            _isDeployin_DOWN = false;
            return;
        }
    }


    // Update is called once per frame
    void Update()
    {

        if (_isDeployingUp) DeployHoloCone2();
        if (_isDeployin_DOWN) ImployHoloCone2();
        CheckwhenToDeactivate();
    }


    public void Activete_and_SetWeaponToShow(GunType _arggun)
    {
        WeaponBundle.gameObject.SetActive(true);
        _WSC.HIdeAll();
        _WSC.Activategun(_arggun);

        _isDeployingUp = true;
        _isDeployin_DOWN = false;
        SetText_forPlayer();
        //  StartCoroutine(FadeOutIn15());
    }

    public void DEActivete_and_SetWeaponToShow()
    {
        txt.text = "";
        _isDeployingUp = false;
        _isDeployin_DOWN = true;

    }
    void CheckwhenToDeactivate()
    {
        if (_isDeployin_DOWN)
        {
            if (HologramScale < 0.2f)
            {
                GameEventsManager.Instance.Call_StartstopAutoshoot(false);

                WeaponBundle.gameObject.SetActive(false);
            }
        }
    }

    void SetText_forPlayer()
    {
        int cap = _WSC.GET_shoedGun_MagCapacity();
        int dam = _WSC.GET_shoedGunBulletDamage();
        string FireType = _WSC.GET_shoedGun_FireType();
        string GunName = _WSC.GET_shoedGun_Name();
        txt.text = "Unlocked: " + GunName + "\n";
        txt.text += ": " + FireType + "\n";
        txt.text += "capacity : " + cap.ToString() + "\n";
        txt.text += "damage : " + dam.ToString() + "\n";

        string Speach = "you have a new weapon, the " + GunName + ".";
        if (FireType.Contains("FULL"))
        {
            Speach += "this gun is fully automatic";
        }
        // StartCoroutine(SpeackIn3seconds(Speach));
    }
    IEnumerator SpeackIn3seconds(string _argTosay)
    {
        yield return new WaitForSeconds(2);
        //    tts.StartSpeaking(_argTosay);
    }

}
