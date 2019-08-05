using UnityEngine;

public class Mag : MonoBehaviour, IMag
{
    Renderer _myrenderer;

    AudioSource MagSource;
    bool touchedGround = false;
    // UAudioManager audioM/nager;

    public GameObject MagBulletlet;

    public int MagSize;

    public int MagBulletCount;


    void Awake()
    {
        touchedGround = false;
        MagSource = GetComponent<AudioSource>();
        _myrenderer = GetComponentInChildren<Renderer>();
        MagBulletCount = MagSize;
        //  audioManager = GetComponent<UAudioManager>();
    }


    public bool TryDecrementBulletCount()
    {
        if (GameSettings.Instance.Controlertype == ARZControlerType.StrikerControlSystem)
        {
            return true; // no decrementing bullets and no reloading needed
        }
        if (MagBulletCount <= 0)
        {
            return false;
        }
        else
        {
            if (GameSettings.Instance != null)
            {
                if (GameSettings.Instance.UseBabyGun)
                {
                    return true;
                }
            }
            MagBulletCount--;
            return true;
        }
    }

    public GameObject GetBulletFromMag()
    {
        return this.MagBulletlet;
    }

    public int GetBulletsCount_inMag() { return MagBulletCount; }

    public void DissableMAgRendered() { _myrenderer.enabled = false; }

    public void Refill()
    {
        Logger.Debug("bullets refilled");
        MagBulletCount = MagSize;
    }

    public void InitCanPlayCollisionSound() { }


    private void OnCollisionEnter(Collision collision)
    {
        // if (collision.gameObject.tag == "SpatialMesh")
        // audioManager.PlayEvent("Cling");
    }

    private void OnTriggerEnter(Collider other)
    {
        //   print("other cling");

        if (other.gameObject.tag == "MetalTag")
        {
            // print("other sound");
            MagSource.Play();
        }
    }
}
