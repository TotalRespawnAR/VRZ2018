using SixenseCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSelector : MonoBehaviour
{
    public LineRenderer LR;
    public RaycastHit hitInfo;
    public LayerMask RaycastLayerMask = Physics.DefaultRaycastLayers;
    int worldAnchorLayer = 21;
    int GridmapLayer = 8; //forwall
    bool hitsomething = false;
    public GameObject RedDot;
    public TrackerVisual TrackerVis;
    public TextMesh tm;

    public GameObject CurAimedObj;
    public GameObject CurSelectedObj;

    bool SlideForward = true;
    bool SlideLateral = true;
    bool SlideUpDown = false;
    bool Rotates = false;
    void ReadStemInput()
    {
        if (GameSettings.Instance.Controlertype != ARZControlerType.StemControlSystem)
        {
            return;
        }


        var controller = TrackerVis.Input;


        ReadJoystickHorizontalInput(controller);

        //controller.GetAnyButtonDown(
        if (controller.GetButton(Buttons.TRIGGER))
        {
            CurSelectedObj = CurAimedObj;
        }

        if (controller.GetButton(Buttons.BUMPER))
        {
           // Debug.Log("bump");
            SlideForward = false;
            SlideLateral = false;
            SlideUpDown = true;
            Rotates = true;
        }

        if (controller.GetButtonUp(Buttons.BUMPER))
        {
           // Debug.Log("bump");
            SlideForward = true;
            SlideLateral = true;
            SlideUpDown = false;
            Rotates = false;
        }


        //if (controller.GetButtonDown(Buttons.PREV))
        //{

        //    Debug.Log("prev");

        //    SlideForward = false;
        //    SlideLateral = false;
        //    SlideUpDown = true;
        //    Rotates = false;
        //}
        //if (controller.GetButtonDown(Buttons.NEXT))
        //{
        //    Debug.Log("nex");

        //    SlideForward = false;
        //    SlideLateral = false;
        //    SlideUpDown = true;
        //    Rotates = false;
        //}

        //if (controller.GetButtonDown(Buttons.A))
        //{
        //    Debug.Log("A ");

        //    SlideForward = false;
        //    SlideLateral = false;
        //    SlideUpDown = false;
        //    Rotates = true;
        //}

        if (controller.GetButtonDown(Buttons.B))
        {
            Debug.Log("B ");
            CurSelectedObj = null;
        }



    }


    // Update is called once per frame
    private void Update()
    {
        if (CurSelectedObj != null)
        {
            tm.text = CurSelectedObj.name;
        }
        else
            tm.text = "na";

       CheckSight();
        if (!hitsomething)
        {

            LR.SetPosition(1, new Vector3(0, 0, -400));
            RedDot.SetActive(false);
        }

        ReadStemInput();
        if (CurSelectedObj != null)
        {
            RotateThisObject(CurSelectedObj);
            TransateThisObject_Forward(CurSelectedObj);
            TransateThisObject_Laterally(CurSelectedObj);
            TransateThisObject_Vertically(CurSelectedObj);
        }

    }

    float XaxisValue = 0;
    float Yaxisalue = 0;

    void ReadJoystickHorizontalInput(Tracker ArgTracker)
    {
        //    -1                   -0.2             0             0.2                   1
        //  
        XaxisValue = ArgTracker.JoystickX;
        Yaxisalue = ArgTracker.JoystickY;

        if (XaxisValue < 0.1 && XaxisValue > -0.1) { XaxisValue = 0f; }
        if (Yaxisalue < 0.1 && Yaxisalue > -0.1) { Yaxisalue = 0f; }
    }

    void RotateThisObject(GameObject argObjToroatate)
    {
        if (!Rotates) return;
        if (argObjToroatate == null) return;
        // ...also rotate around the World's Y axis
        argObjToroatate.transform.Rotate(Vector3.up * Time.deltaTime * XaxisValue * 4f, Space.World);
    }


    void TransateThisObject_Forward(GameObject argObjToroatate)
    {
        if (!SlideForward) return; 
        if (argObjToroatate == null) return;
        // Move the object forward along its z axis 1 unit/second.
        argObjToroatate.transform.Translate(Vector3.forward * Time.deltaTime * Yaxisalue * 1.2f);

    }



    void TransateThisObject_Laterally(GameObject argObjToroatate)
    {
        if (!SlideLateral) return;
        if (argObjToroatate == null) return;
        // Move the object forward along its z axis 1 unit/second.
        argObjToroatate.transform.Translate(Vector3.right * Time.deltaTime * XaxisValue * 2f);
    }

    void TransateThisObject_Vertically(GameObject argObjToroatate)
    {
        if (!SlideUpDown) return;
        if (argObjToroatate == null) return;
        // Move the object forward along its z axis 1 unit/second.
        argObjToroatate.transform.Translate(Vector3.up * Time.deltaTime * Yaxisalue * 2f);
    }


    void CheckSight()
    {
        if (Physics.Raycast(transform.position, transform.forward * -1, out hitInfo, 100, RaycastLayerMask))
        {
            if (hitInfo.collider.gameObject.layer == worldAnchorLayer)
            {
                hitsomething = true;
                RedDot.SetActive(true);
                RedDot.transform.position = new Vector3(hitInfo.point.x, hitInfo.point.y, hitInfo.point.z - 0.01f);


                float dist = Vector3.Distance(hitInfo.point, this.transform.position);
                // Debug.Log("hitZombie" + dist);
                LR.SetPosition(1, new Vector3(0, 0, dist * -1));

                CurAimedObj = hitInfo.collider.gameObject;
            }

        }
        else
        {
            hitsomething = false;
            CurAimedObj = null;
        }


    }
}
