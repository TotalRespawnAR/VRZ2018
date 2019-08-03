using System.Collections;
using UnityEngine;

public class BoidFlocking : MonoBehaviour
{
    internal BoidController controller;

    IEnumerator Start()
    {
        while (true)
        {
            float waitTime;
            if (controller)
            {
                GetComponent<Rigidbody>().velocity += steer() * Time.deltaTime;



                // enforce minimum and maximum speeds for the boids
                float speed = GetComponent<Rigidbody>().velocity.magnitude;
                if (speed > controller.maxVelocity)
                {
                    waitTime = Random.Range(0.1f, 0.2f);
                    GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * controller.maxVelocity;
                }
                else if (speed < controller.minVelocity)
                {
                    GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * controller.minVelocity;
                    waitTime = Random.Range(0.4f, 0.8f);
                }
                else
                    waitTime = Random.Range(0.3f, 0.5f);
            }
            else
                waitTime = 0.1f;//Random.Range(0.2f, 0.4f);

            yield return new WaitForSeconds(waitTime);
        }
    }

    Vector3 steer()
    {
        Vector3 randomize = new Vector3((Random.value * 2) - 1, (Random.value * 2) - 1, (Random.value * 2) - 1);
        randomize.Normalize();
        randomize *= controller.randomness;

        Vector3 center = controller.flockCenter - transform.localPosition;
        Vector3 velocity = controller.flockVelocity - GetComponent<Rigidbody>().velocity;
        Vector3 follow = controller.target.localPosition - transform.localPosition;

        return (center + velocity + follow * 2 + randomize);
    }

    //private void FixedUpdate()
    //{
    //    Debug.DrawRay(transform.position, GetComponent<Rigidbody>().velocity.normalized, Color.yellow);


    //}
}