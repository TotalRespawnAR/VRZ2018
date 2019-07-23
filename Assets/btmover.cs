using UnityEngine;

public class btmover : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Destroy(gameObject, .2f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * 540 * -Time.deltaTime);
    }
}
