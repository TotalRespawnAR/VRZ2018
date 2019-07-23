using UnityEngine;

public class CasingScript : MonoBehaviour
{
    AudioSource ShellSource;
    bool touchedGround = false;
    //fires when setactive true
    private void OnEnable()
    {
        touchedGround = false;
        ShellSource = GetComponent<AudioSource>();
        transform.GetComponent<Rigidbody>().WakeUp();
        Invoke("HideCasing", 5.0f);
    }
    //fires when setactive false

    private void OnDisable()
    {
        touchedGround = false;
        transform.GetComponent<Rigidbody>().Sleep();
        CancelInvoke(); //so that hidecasin will not run 2wice
    }

    void HideCasing()
    {
        this.gameObject.SetActive(false);
        // Destroy(gameObject);
    }
    // UAudioManager audioManager;
    // Use this for initialization
    //public float killDelay = 5.0f;
    void Start()
    {
        //audioManager = GetComponent<UAudioManager>();
        // KillTimer t = this.gameObject.AddComponent<KillTimer>();
        //t.StartTimer(killDelay);
    }


    //private void OnCollisionEnter(Collision collision)
    //{
    //    //  print("coollision cling");
    //    //        Debug.Log("collision with " + collision.gameObject.name);
    //    if (collision.gameObject.tag == "MetalTag")
    //    {
    //        print("coollision sound");

    //        ShellSource.Play();
    //    }
    //    // audioManager.PlayEvent("Cling");
    //}
    private void OnTriggerEnter(Collider other)
    {
        //   print("other cling");

        if (other.gameObject.tag == "MetalTag")
        {
            // print("other sound");
            ShellSource.Play();
        }
    }


}
