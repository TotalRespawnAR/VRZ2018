// @Author Nabil Lamriben ©2018
using UnityEngine;

public class SliderFrontCollider : MonoBehaviour {


    ////Color ColorONSuccess = Color.green;
    ////Color ColorONFail = Color.red;
    ////private Material material;

    public SliderBackCollider MyBACKCOLLIDER;
    ////private Color originalColor;

    private void Start()
    {
        ////if (GameSettings.Instance == null) { GetComponent<Renderer>().enabled = false; return; }

        ////if (!GameSettings.Instance.IsGunTestModeON) { GetComponent<Renderer>().enabled = false; }
        ////else
        ////{
        ////    material = GetComponent<Renderer>().material;
        ////    originalColor = material.color;
        ////}
    }

    ////private void OnTriggerEnter(Collider other)
    ////{
    ////    if (other.gameObject.CompareTag("LoadyHand"))
    ////    {
    ////         MyBACKCOLLIDER.Trig_Front(true);
    ////        if (GameSettings.Instance.IsGunTestModeON)
    ////        {
    ////            material.color = ColorONSuccess;
    ////        }
    ////    }

    ////}


    ////private void OnTriggerExit(Collider other)
    ////{
    ////    if (other.gameObject.CompareTag("LoadyHand"))
    ////    {
    ////        if (GameSettings.Instance.IsGunTestModeON)
    ////        {
    ////            material.color = originalColor;
    ////        }
    ////        MyBACKCOLLIDER.Trig_Front(false);
    ////    }

    ////}
}
