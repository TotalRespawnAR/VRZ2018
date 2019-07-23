using UnityEngine;

public class GunSlinger : MonoBehaviour
{


    public GameObject myColt;
    public GameObject my1911;
    public GameObject myMac11;
    public GameObject myMg61;
    public GameObject myP90;
    public GameObject mySH;
    public GameObject myStriker;


    IGun _myActiveGun;

    void SwitChGun(GunType argGuntype)
    {
        myColt.SetActive(false);
        my1911.SetActive(false);
        myMac11.SetActive(false);
        myMg61.SetActive(false);
        myP90.SetActive(false);
        mySH.SetActive(false);
        myStriker.SetActive(false);

        switch (argGuntype)
        {
            case GunType.PISTOL:
                my1911.SetActive(true);
                _myActiveGun = my1911.GetComponent<IGun>();
                break;
            case GunType.MAGNUM:
                myColt.SetActive(true);
                _myActiveGun = myColt.GetComponent<IGun>();
                break;

            case GunType.UZI:
                myMac11.SetActive(true);
                _myActiveGun = myMac11.GetComponent<IGun>();
                break;
            case GunType.MG61:
                myMg61.SetActive(true);
                _myActiveGun = myMg61.GetComponent<IGun>();
                break;
            case GunType.P90:
                myP90.SetActive(true);
                _myActiveGun = myP90.GetComponent<IGun>();
                break;
            case GunType.SHOTGUN:
                mySH.SetActive(true);
                _myActiveGun = mySH.GetComponent<IGun>();
                break;
            case GunType.STRIKERVR:
                myStriker.SetActive(true);
                _myActiveGun = myStriker.GetComponent<IGun>();
                break;
            default:

                break;
        }
    }



    void Start()
    {
        SwitChGun(GunType.PISTOL);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1)) { SwitChGun(GunType.PISTOL); }
        if (Input.GetKeyDown(KeyCode.Alpha2)) { SwitChGun(GunType.MAGNUM); }
        if (Input.GetKeyDown(KeyCode.Alpha3)) { SwitChGun(GunType.UZI); }
        if (Input.GetKeyDown(KeyCode.Alpha4)) { SwitChGun(GunType.MG61); }
        if (Input.GetKeyDown(KeyCode.Alpha5)) { SwitChGun(GunType.P90); }
        if (Input.GetKeyDown(KeyCode.Alpha6)) { SwitChGun(GunType.SHOTGUN); }
        if (Input.GetKeyDown(KeyCode.Space)) { _myActiveGun.GUN_FIRE(); }
        if (Input.GetKeyDown(KeyCode.K)) { _myActiveGun.Temp_ScopeCycleForward(); }


    }
}
