using UnityEngine;

public class FlockingFlyBehabior : MonoBehaviour, IShootable
{
    Animator m_anim;
    Rigidbody rb;
    SphereCollider sc;
    bool shot;
    Transform TargetTrans;
    float rotspeed;
    void Awake()
    {
        sc = GetComponent<SphereCollider>();
        rb = GetComponent<Rigidbody>();
        m_anim = GetComponent<Animator>();
        m_anim.speed = Random.Range(0.75f, 1.1f);
        rotspeed = Random.Range(0.6f, 2.1f);
    }
    void Start()
    {
        TargetTrans = GameObject.FindGameObjectWithTag("TutoTarget1").transform;
    }
    void LateUpdate()
    {
        //  transform.LookAt(TargetTrans);
        Vector3 lTargetDir = TargetTrans.position - transform.position;
        lTargetDir.y = 0.0f;
        // transform.rotation =    Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(lTargetDir), Time.time * rotspeed);
        //transform.rotation = Quaternion.Lerp(transform.rotation, transform.parent.GetComponent<Rigidbody>().velocity.normalized, Time.time * rotspeed);
        // transform.forward = Vector3.Lerp(transform.forward, transform.parent.GetComponent<Rigidbody>().velocity.normalized, Time.time * rotspeed);
        Debug.DrawRay(transform.position, transform.parent.GetComponent<Rigidbody>().velocity.normalized, Color.yellow);


        Vector3 targetDir = transform.parent.GetComponent<Rigidbody>().velocity.normalized;

        // The step size is equal to speed times frame time.
        float step = rotspeed * Time.deltaTime;

        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
        Debug.DrawRay(transform.position, newDir, Color.red);

        // Move our position a step closer to the target.
        transform.rotation = Quaternion.LookRotation(newDir);
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
