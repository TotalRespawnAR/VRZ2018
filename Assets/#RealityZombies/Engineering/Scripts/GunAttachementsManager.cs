using UnityEngine;

public class GunAttachementsManager : MonoBehaviour
{


    public GameObject SniperScope;
    public GameObject KinScope;
    public GameObject RedDotScope;
    public GameObject LaserBox;

    public Laz0rCTRL TheLazor;  //move the whole dang thing , then ask it to manage red dor vs red sphere , or make the red lazor invisible . the ball can remain there as it will only be seen by the redot mask shader

    public Transform BulletSpawnerMovableTransform;



    public Transform Trans_Top_SniperAimLoc;
    public Transform Trans_Kin_scopeAimLoc;
    public Transform Trans_Mid_RedDotAimLoc;
    public Transform Trans_Bot_LaserAimLoc;
    public Transform Trans_IronIronSightsAimLoc;




    public void Setup_Sniper()
    {
        SniperScope.SetActive(true);
        KinScope.SetActive(false);
        RedDotScope.SetActive(false);
        LaserBox.SetActive(false);

        TheLazor.gameObject.transform.position = Trans_Top_SniperAimLoc.position;
        TheLazor.ToggleRedLaserRenderer(false, true);
    }

    public void Setup_KinScope()
    {
        SniperScope.SetActive(false);
        KinScope.SetActive(true);
        RedDotScope.SetActive(false);
        LaserBox.SetActive(false);

        TheLazor.gameObject.transform.position = Trans_Kin_scopeAimLoc.position;
        TheLazor.ToggleRedLaserRenderer(false, false);
    }

    public void Setup_RedDot()
    {
        SniperScope.SetActive(false);
        KinScope.SetActive(false);
        RedDotScope.SetActive(true);
        LaserBox.SetActive(false);

        TheLazor.gameObject.transform.position = Trans_Mid_RedDotAimLoc.position;
        TheLazor.ToggleRedLaserRenderer(false, false);
    }

    public void Setup_Lazor()
    {
        SniperScope.SetActive(false);
        KinScope.SetActive(false);
        RedDotScope.SetActive(false);
        LaserBox.SetActive(true);

        TheLazor.gameObject.transform.position = Trans_Bot_LaserAimLoc.position;
        TheLazor.ToggleRedLaserRenderer(true, true);
    }


    public void Setup_IronSights()
    {
        SniperScope.SetActive(false);
        KinScope.SetActive(false);
        RedDotScope.SetActive(false);
        LaserBox.SetActive(true);
        TheLazor.gameObject.transform.position = Trans_IronIronSightsAimLoc.position;
        TheLazor.ToggleRedLaserRenderer(false, false);
    }



    // Use this for initialization
    void Start()
    {
      //  Setup_IronSights();
    }


}
