using UnityEngine;

public class PlayerScaledBuilder : MonoBehaviour
{

    public GameObject Head;
    public GameObject Gun;
    public float HeadElevation;
    public float HeadRayDownDist;

    public GameObject PlayerCharacter;

    bool hasBeenInstanciated;

    void poof()
    {
        PlayerCharacter.transform.localScale = new Vector3(1f, 1f, 1f) * 1f;

        PlayerCharacter.SetActive(true);
    }


    // Update is called once per frame
    void Update()
    {
        HeadElevation = Head.transform.position.y;
        Vector3 forward = Head.transform.TransformDirection(Vector3.forward) * 10;
        Debug.DrawRay(Head.transform.position, forward, Color.blue);
        Vector3 down = Head.transform.TransformDirection(Vector3.down) * 10;
        Debug.DrawRay(Head.transform.position, down, Color.green);




        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 9; //vrutils for the ground

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        //layerMask = ~layerMask;

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(Head.transform.position, Head.transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(Head.transform.position, Head.transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
            HeadRayDownDist = hit.distance;
            // Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(Head.transform.position, Head.transform.TransformDirection(Vector3.down) * 1000, Color.white);
            //  Debug.Log("Did not Hit");
        }

        if (hasBeenInstanciated) return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            poof();
            hasBeenInstanciated = true;
        }
    }
}
