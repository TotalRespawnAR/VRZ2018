using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// these define the flock's behavior
/// </summary>
public class BoidController : MonoBehaviour
{
    public float minVelocity = 5;
    public float maxVelocity = 20;
    public float randomness = 1;
    public int flockSize = 20;
    public BoidFlocking prefab;
    public Transform target;

    internal Vector3 flockCenter;
    internal Vector3 flockVelocity;

    List<BoidFlocking> boids = new List<BoidFlocking>();
    public BoidWayPoint[] mywaypoints;
    int curWPindex;
    public void ActivateNext()
    {

        return;

        if (mywaypoints.Length < 2) return;
        mywaypoints[curWPindex].gameObject.SetActive(false);
        curWPindex++;
        if (curWPindex >= mywaypoints.Length) curWPindex = 0;

        mywaypoints[curWPindex].gameObject.SetActive(true);
        mywaypoints[curWPindex].ResetTouched();
        target = mywaypoints[curWPindex].transform;


    }
    void Start()
    {
        curWPindex = 0;
        mywaypoints = GetComponentsInChildren<BoidWayPoint>();
        for (int x = 1; x < mywaypoints.Length; x++)
        {
            mywaypoints[x].gameObject.SetActive(false);
        }
        target = mywaypoints[curWPindex].transform;
        for (int i = 0; i < flockSize; i++)
        {
            BoidFlocking boid = Instantiate(prefab, transform.position, transform.rotation) as BoidFlocking;
            boid.transform.parent = transform;
            boid.transform.localPosition = new Vector3(
                            Random.value * GetComponent<Collider>().bounds.size.x,
                            Random.value * GetComponent<Collider>().bounds.size.y,
                            Random.value * GetComponent<Collider>().bounds.size.z) - GetComponent<Collider>().bounds.extents;
            boid.controller = this;
            boids.Add(boid);
        }
    }

    void Update()
    {
        Vector3 center = Vector3.zero;
        Vector3 velocity = Vector3.zero;
        foreach (BoidFlocking boid in boids)
        {
            center += boid.transform.localPosition;

            Vector3 eachVelocity = boid.GetComponent<Rigidbody>().velocity;
            Debug.DrawRay(boid.transform.localPosition, eachVelocity.normalized, Color.red);
            velocity += eachVelocity;


        }
        flockCenter = center / flockSize;
        flockVelocity = velocity / flockSize;
    }

    private void LateUpdate()
    {
        Debug.DrawLine(transform.position, transform.position + flockCenter, Color.blue);
        Debug.DrawLine(transform.position, transform.position + flockVelocity, Color.cyan);

    }
}