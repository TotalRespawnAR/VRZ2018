// @Author Jeffrey M. Paquette ©2016

using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour {

    [Tooltip("What type of gun does this pickup represent?")]
    public GunType type;

    [Tooltip("Time in seconds before object is automatically destroyed.")]
    public float destroyTime;

    [Tooltip("The effect to instantiate upon successful pickup.")]
    public GameObject pickupEffect;

    KillTimer killTimer;
    void OnEnable()
    {
        GameEventsManager.OnTakeHit += TakeHit;
    }

    private void OnDisable()
    {
        GameEventsManager.OnTakeHit -= TakeHit;
    }

    // Use this for initialization
    void Start () {
        killTimer = gameObject.AddComponent<KillTimer>();
        killTimer.StartTimer(destroyTime);
	}
	
	public void TakeHit(Bullet bullet, int argIdNotused)
    {
        // instantiate effect, award weapon, and destroy self
        Instantiate(pickupEffect, transform.position, transform.rotation);
       // GameManager.Instance.AwardWeapon(type);  //never used since the demo
       // GameManager.Instance.DecrementCounter();

        Destroy(gameObject);
    }
}
