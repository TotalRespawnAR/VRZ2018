using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoloCamPlanes : MonoBehaviour {

    public GameObject PlaneUpObj;
    public GameObject PlaneDownObj;
    public GameObject PlaneLeftObj;
    public GameObject PlaneRightObj;

    public Transform u1;
    public Transform u2;
    public Transform u3;

    public Transform d1;
    public Transform d2;
    public Transform d3;

    public Transform l1;
    public Transform l2;
    public Transform l3;

    public Transform r1;
    public Transform r2;
    public Transform r3;

    public Vector3 u2pos;


    public static HoloCamPlanes Instance = null;
    //void InitPoints() {


    //    u1 = DeepSearchContain(this.transform, "upA");
    //    u2 = DeepSearchContain(this.transform, "upB");
    //    u3 = DeepSearchContain(this.transform, "upC");

    //    d1 = DeepSearchContain(this.transform, "botA");
    //    d2 = DeepSearchContain(this.transform, "botB");
    //    d3 = DeepSearchContain(this.transform, "botC");

    //    l1 = DeepSearchContain(this.transform, "LeftA");
    //    l2 = DeepSearchContain(this.transform, "LeftB");
    //    l3 = DeepSearchContain(this.transform, "LeftC");

    //    r1 = DeepSearchContain(this.transform, "RightA");
    //    r2 = DeepSearchContain(this.transform, "RightB");
    //    r3 = DeepSearchContain(this.transform, "RightC");

    //    PlaneUp = new Plane(u1.position, u2.position, u3.position);
    //    PlaneDown = new Plane(d1.position, d2.position, d3.position);
    //    PlaneLeft = new Plane(l1.position, l2.position, l3.position);
    //    PlaneRight = new Plane(r1.position, r2.position, r3.position);

    //}


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(gameObject);
    }
    void UpdatePlanes()
    {
        PlaneUp = new Plane(u1.position, u2.position, u3.position);
        PlaneDown = new Plane(d1.position, d2.position, d3.position);
        PlaneLeft = new Plane(l1.position, l2.position, l3.position);
        PlaneRight = new Plane(r1.position, r2.position, r3.position);
    }

    Plane PlaneUp;
      Plane PlaneDown;
      Plane PlaneLeft;
      Plane PlaneRight;

    public Transform AnObject;
    public bool IsAbove = false;
    public bool IsBellow = false;
    public bool IsRightside = false;
    public bool IsLeftSide = false;
    bool IsItAbove(Vector3 ballPos) {

        return PlaneUp.GetSide(ballPos);
    }

    bool IsItBellow(Vector3 ballPos)
    {

        return PlaneDown.GetSide(ballPos);
    }

    bool IsItRightSide(Vector3 ballPos)
    {

        return PlaneRight.GetSide(ballPos);
    }

    bool IsItLeftSide(Vector3 ballPos)
    {

        return PlaneLeft.GetSide(ballPos);
    }
  
    public bool CheckIfImOutOfView(Transform argTran) {
       // InitPoints();
        return IsItAbove(argTran.position) || IsItBellow(argTran.position) || IsItRightSide(argTran.position) || IsItLeftSide(argTran.position);

    }
    public bool OutofView;
    private void FixedUpdate()
    {
        UpdatePlanes();
        //OutofView = CheckIfImOutOfView(AnObject);
       
        //if (Input.GetKeyDown(KeyCode.Space)) {
             
        //    IsAbove = IsItAbove(AnObject.position);
        //    IsBellow = IsItBellow(AnObject.position);
        //    IsRightside = IsItRightSide(AnObject.position);
        //    IsLeftSide= IsItLeftSide(AnObject.position);
        //}
 
    }



    public Transform DeepSearchContain(Transform parent, string val)
    {
        // Debug.Log("on " + parent.gameObject.name);

        foreach (Transform c in parent)
        {
            if (c.name.Contains(val)) { return c; }
            var result = DeepSearchContain(c, val);
            if (result != null)
                return result;
        }
        return null;
    }
}
