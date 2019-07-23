using UnityEngine;
using System.Collections;

public class RagdollPartScript : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
	
	}
	void OnCollisionEnter(Collision collision)
	{
		//Increase score if this ragdoll part collides with something else
		//than another ragdoll part with sufficient velocity. 
		//If the colliding object is another ragdoll part, it will have the same root, hence the inequality check.
		if (transform.root != collision.transform.root)
		{			
			//Check that we are colliding with sufficient velocity
			if (collision.relativeVelocity.magnitude > 4.0f){
				//compute score
				int score=100*Mathf.RoundToInt(collision.relativeVelocity.magnitude);
				print (gameObject.name + " collided with " + collision.gameObject.name + ", giving score " + score);
				
				
				
			}
		}
	}
	// Update is called once per frame
	void Update () {
	
	}
}
