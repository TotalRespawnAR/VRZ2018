using UnityEngine;

public class BulletVaporTrail : MonoBehaviour
{

    /// <summary>
    ///  this script is to control the instantiate bullet vapor trail gameObject
    /// </summary>
    /// 
    // private ParticleSystemRenderer _pSysRen;
    private GameObject _particleChildObj;
    private ParticleSystem _Ps;
    private float bulletSpeed = 130f;// 175.0f;// 2f;// 10 tes is prety sweet with size 1.25;// 175.0f; //80

    private void Awake()
    {
        _particleChildObj = transform.GetChild(0).gameObject;
        _Ps = _particleChildObj.GetComponent<ParticleSystem>();

        // _pSysRen = transform.GetChild(0).gameObject.GetComponent<ParticleSystemRenderer>();
    }
    // Update is called once per frame
    private void Update()
    {

        transform.Translate(Vector3.forward * bulletSpeed * -Time.deltaTime);

    }// end of update


    // a function to change the particle effect
    public void ChangeParticleEffect(float size, Color32 color, float lifeTime)
    {

        lifeTime = 0.08f;
        // grab and change the current material       
        // _pSysRen.material.SetColor("_TintColor", Color.yellow);
        // change the size
        _particleChildObj.transform.localScale = new Vector3(size, size + 1, size);
        // change the length of the tail trail
        _Ps.startLifetime = lifeTime;
        // destroy the clone
        Destroy(gameObject, 2);

    }// end of function change particle effect
    private void OnDestroy()
    {
        // DestroyImmediate(_pSysRen.material);//makes a huge difference .now that we cashed our renderer we can destroy its cloned material :D 
    }
}// end of bullet vapor trail script
