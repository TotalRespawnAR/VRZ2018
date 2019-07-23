#define ENABLE_LOGS
using UnityEngine;

public class Points3dUi : MonoBehaviour
{

    /// <summary>
    ///  this script is to move the 3d ui points up in the air and destroy them after x seconds
    /// </summary>

    // how long it will live
    private int lifeTime;
    // how fast it will move upwards
    private float moveSpeed;
    public Renderer ren100;
    public Renderer ren125;
    public Renderer ren150;
    public Renderer ren175;
    public Renderer ren200;
    public Renderer rens1;
    public Renderer rens2;
    public Renderer rens5;

    // start
    private void Start()
    {
        // set move speed
        moveSpeed = 0.8f;
        // set lifetime
        lifeTime = 3;
        // destroy in that time
        Destroy(gameObject, lifeTime);


    }// end of start

    // Update
    private void Update()
    {
        // move by moveSpeed
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);

    }// end of Update

    private void OnDestroy()
    {
        DestroyImmediate(ren100);
        DestroyImmediate(ren125);
        DestroyImmediate(ren150);
        DestroyImmediate(ren175);
        DestroyImmediate(ren200);
        DestroyImmediate(rens1);
        DestroyImmediate(rens2);
        DestroyImmediate(rens5);
    }
}// end of points3dUi script
