using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeBehaviour : MonoBehaviour {

 //   public GameObject Explosion;
 
 //   void Start () {

 //     //  Rigidbody bulletInstance=this.GetComponent<Rigidbody>();
       

 //     //  bulletInstance.AddForce(transform.forward * bulletSpeed); //ADDING FORWARD FORCE TO THE FLARE PROJECTILE
 //   }
	
	//// Update is called once per frame
	//void Update () {
		
	//}

 //   float RadIusEXPLOSION=5f;
 //   bool hasExploded = false;
 //   public void GrenadeExplode(List<GameObject> argEnemies) {
 //       if (hasExploded) return;

 //       List < GameObject > AllLiveZombies = new List<GameObject>();
 //       foreach (GameObject go in argEnemies)
 //       {
 //           AllLiveZombies.Add(go);
 //       }


 //       foreach (GameObject go in AllLiveZombies) {

 //           float thedist = Vector3.Distance(go.transform.position, this.transform.position);
 //           if (thedist < RadIusEXPLOSION) {

 //               go.GetComponent<ZombieBehavior>().GrenageMe(this.transform);
 //           }
 //       }
 //       Instantiate(Explosion, transform.localPosition, Quaternion.identity);
 //       hasExploded = true;
 //   }

 //   void OnCollisionEnter(Collision collision) {
 //      // Debug.Log("collided with " + collision.gameObject.name);

 //       GrenadeExplode(GameManager.Instance.GEtAllives());
 //       Instantiate(Explosion, transform.localPosition, Quaternion.identity);
 //       Destroy(this.gameObject, 1f);
 //       //if (collision.gameObject.tag == "SpatialMesh")
 //       //{ }

 //   }

    //private void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log("collided with "+ other.tag);
    //    if (other.CompareTag("SpatialMesh")) {

    //        GrenadeExplode(GameManager.Instance.GEtAllives());
    //        Instantiate(Explosion, transform.localPosition, Quaternion.identity);

    //    }
    //}
}
