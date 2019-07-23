using UnityEngine;

public class DistanceToChecker : MonoBehaviour
{


    public Transform Targ;
    public float DistNoX;
    public float DistNoY;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Targ != null)
        {
            DistNoX = Vector3.Distance(this.transform.position, new Vector3(this.transform.position.x, Targ.position.y, Targ.position.z));
            DistNoY = Vector3.Distance(this.transform.position, new Vector3(Targ.position.x, this.transform.position.y, Targ.position.z));
        }
    }
}
