using UnityEngine;

public class FlockingFlyBehabior : MonoBehaviour
{
    Animator m_anim;
    Rigidbody rb;
    SphereCollider sc;
    bool shot;
    void Awake()
    {
        sc = GetComponent<SphereCollider>();
        rb = GetComponent<Rigidbody>();
        m_anim = GetComponent<Animator>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Shot(Bullet argBullet)
    {
        shot = true;
        m_anim.SetTrigger("FlyDeathTrig");
        if (this.transform.parent != null)
        {

            this.transform.parent = null;
        }
        rb.isKinematic = false;
        rb.useGravity = true;
        sc.isTrigger = true;

        Destroy(gameObject, 5f);
    }



    public void aimed(bool argTF)
    {
        Debug.Log("AIMED");
    }
}
