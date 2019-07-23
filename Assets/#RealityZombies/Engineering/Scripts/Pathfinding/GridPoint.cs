// @Author Jeffrey M. Paquette ©2016
// @Author Nabil Lamriben ©2017
// @Author Nabil Lamriben ©2018
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridPoint : MonoBehaviour {

    // public GameObject gridPoint;
    public LayerMask layerMask = Physics.DefaultRaycastLayers;
    float segmentDistanceGP; float bottomsegmetdistanceGP;
    public float GetSegmentDist() { return segmentDistanceGP; }
    public GameObject CubeChild;
    public TextMesh GPTxt;
    public TextMesh NodeTxt;
    public string GridpointName = "GP";
    //public Material scannedMaterial;
    public void SetTXTtop(string str) { GPTxt.text = str; }
    public void SetTXTbot(string str) { NodeTxt.text = str; }
    public void SetCOLOR(Color argColor) { //gameObject.GetComponent<Renderer>().material.color = argColor;
    }


    public GameObject forwardGameObject { get; private set; }
    public GameObject leftGameObject { get; private set; }
    public GameObject rightGameObject { get; private set; }
    public GameObject backGameObject { get; private set; }



    [SerializeField]
    GridPoint forwardGridPoint;
    [SerializeField]
    GridPoint leftGridPoint;
    [SerializeField]
    GridPoint rightGridPoint;
    [SerializeField]
    GridPoint backGridPoint;


    public bool GP_Connected { get; private set; }

    public bool wasvisited { get; set; }

    //public GameSettings Settigs;
    // public bool thisGridpointWasUsedinPath1;
    public int TimesUsedInAfinalPath;
    public void Increment_TimesUsedInAfinalPath()
    {

        this.TimesUsedInAfinalPath++;
    }
    void drawAll()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward) * segmentDistanceGP;
        Debug.DrawRay(transform.position, forward, Color.blue);
        Vector3 backward = transform.TransformDirection(Vector3.back) * segmentDistanceGP;
        Debug.DrawRay(transform.position, backward, Color.cyan);
        Vector3 leftward = transform.TransformDirection(Vector3.left) * segmentDistanceGP;
        Debug.DrawRay(transform.position, leftward, Color.red);
        Vector3 rightward = transform.TransformDirection(Vector3.right) * segmentDistanceGP;
        Debug.DrawRay(transform.position, rightward, Color.green);
        Vector3 downward = transform.TransformDirection(Vector3.down) * bottomsegmetdistanceGP;
        Debug.DrawRay(transform.position, downward, Color.black);
    }

    void Update()
    {
        // (GameSettings.Instance.IsShowGridPointMesh)
           // drawAll();
    }
    public float SegmentSizeMaster = .25f;
    public float BottomSegmentSizeMaster = 3f;
    void Awake()
    {
        //the next 2 lines MUST be kept on Awake.  putting them on Start causes meme leak
        segmentDistanceGP = SegmentSizeMaster;// was GameSettings.Instance.SegmentSizeMaster;
        bottomsegmetdistanceGP = BottomSegmentSizeMaster; //was GameSettings.Instance.BottomSegmentSizeMaster;
    }
    public float GetGpSegmentDist() { return segmentDistanceGP; }

    void Start()
    {
        //   Init_Times_Used_in_a_path();
    }

    public void Init_Times_Used_in_a_path()
    {
        TimesUsedInAfinalPath = 0;
        //thisGridpointWasUsedinPath1 = false;
    }


    public void turnCubeMeshOff()
    {
        CubeChild.GetComponent<MeshRenderer>().enabled = false; //used to be just gameobject .getcom   
        GPTxt.gameObject.GetComponent<MeshRenderer>().enabled = false;
        NodeTxt.gameObject.GetComponent<MeshRenderer>().enabled = false;
        //  gameObject.SetActive(false);
    }


    public void Connect()
    {
        GP_Connected = true;
    }

    public bool isValid()
    {
        // if there is floor beneath then this is a valid point
        RaycastHit hitInfo;
        return Physics.Raycast(transform.position, Vector3.down, out hitInfo, 3.0f, layerMask);
    }

    public void SetForwardPoint(GameObject forwardPoint)
    {
        if (forwardPoint == null)
        {
            forwardGameObject = null;
            forwardGridPoint = null;
            return;
        }

        forwardGameObject = forwardPoint;
        forwardGridPoint = forwardGameObject.GetComponent<GridPoint>();
    }

    public void SetLeftPoint(GameObject leftPoint)
    {
        if (leftPoint == null)
        {
            leftGameObject = null;
            leftGridPoint = null;
            return;
        }

        leftGameObject = leftPoint;
        leftGridPoint = leftGameObject.GetComponent<GridPoint>();
    }

    public void SetRightPoint(GameObject rightPoint)
    {
        if (rightPoint == null)
        {
            rightGameObject = null;
            rightGridPoint = null;
            return;
        }

        rightGameObject = rightPoint;
        rightGridPoint = rightGameObject.GetComponent<GridPoint>();
    }

    public void SetBackPoint(GameObject backPoint)
    {
        if (backPoint == null)
        {
            backGameObject = null;
            backGridPoint = null;
            return;
        }

        backGameObject = backPoint;
        backGridPoint = backGameObject.GetComponent<GridPoint>();
    }
    public void Scan(List<GameObject> pointsList, Queue<GameObject> toVisit, Transform argParent)
    {
        /// <summary>
        /// when instantiated, perform raycasts to forward, left, right, and back
        /// if raycast hits spatial map point is set to null
        /// if raycast hits another gridpoint set respective property to that object and tell that object to do the same
        /// else instantiate a new gridpoint and add it to pointList
        /// </summary>
        GP_Raycast_Forward(pointsList, toVisit, argParent);
        GP_Raycast_Rightward(pointsList, toVisit, argParent);
        GP_Raycast_Backward(pointsList, toVisit, argParent);

        GP_Raycast_Leftward(pointsList, toVisit, argParent);

        //raycast back
    }

    /// <summary>
    /// Checks the bounds of this grid point. Returns false if we are removing this point.
    /// </summary>
    /// <returns>
    public bool GP_CheckBounds()
    {
        // if there is not enough room for an agent, then delete point
        Vector3 center = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        Vector3 halfExtents = new Vector3(segmentDistanceGP * 0.5f, 0.5f, segmentDistanceGP * 0.5f);

        if (Physics.CheckBox(center, halfExtents, Quaternion.identity, layerMask, QueryTriggerInteraction.Ignore))
        {
            // GP_RemovePoint();
            return false;
        }

        return true;
    }

    public bool GP_CheckConnectivity()
    {
        if (!GP_Connected)
        {
            GP_RemovePoint();
            return false;
        }

        return true;
    }

    void GP_RemovePoint()
    {
        // Logger.Debug("Removing Point");
        if (forwardGameObject != null)
            forwardGridPoint.SetBackPoint(null);

        if (leftGameObject != null)
            leftGridPoint.SetRightPoint(null);

        if (rightGameObject != null)
            rightGridPoint.SetLeftPoint(null);

        if (backGameObject != null)
            backGridPoint.SetForwardPoint(null);

        Destroy(gameObject);
    }


    void GP_Raycast_Forward(List<GameObject> pointsList, Queue<GameObject> toVisit, Transform argParent)
    {
        RaycastHit hitInfo;

        // raycast forward
        if (Physics.Raycast(transform.position, Vector3.forward, out hitInfo, segmentDistanceGP, layerMask, QueryTriggerInteraction.Collide))
        {
            if (hitInfo.collider.gameObject.CompareTag("GridPoint"))
            {
                SetForwardPoint(hitInfo.collider.gameObject);
                forwardGridPoint.SetBackPoint(gameObject);
            }
            else
            {
                SetForwardPoint(null);
            }
        }
        else
        {
            Vector3 forwardOffset = (Vector3.forward * segmentDistanceGP);
            Vector3 newPosition = forwardOffset + this.transform.position;
            // Logger.Debug(newPosition + "F=" + forwardOffset + " + " + this.transform.position);
            GameObject newGridPointOBJ = Instantiate(this.gameObject, newPosition, argParent.localRotation) as GameObject;
            newGridPointOBJ.name = this.gameObject.name + "_";

            newGridPointOBJ.transform.parent = argParent;
            GridPoint gp = newGridPointOBJ.GetComponent<GridPoint>();
            if (gp.isValid() && gp.GP_CheckBounds())
            {
                SetForwardPoint(newGridPointOBJ);
                newGridPointOBJ.name += pointsList.Count.ToString();
                pointsList.Add(newGridPointOBJ);
                toVisit.Enqueue(newGridPointOBJ);

            }
            else
            {
                SetForwardPoint(null);
                Destroy(newGridPointOBJ);
            }
        }
    }
    void GP_Raycast_Backward(List<GameObject> pointsList, Queue<GameObject> toVisit, Transform argParent)
    {
        RaycastHit hitInfo;
        // raycast back
        if (Physics.Raycast(transform.position, Vector3.back, out hitInfo, segmentDistanceGP, layerMask, QueryTriggerInteraction.Collide))
        {

            if (hitInfo.collider.gameObject.CompareTag("GridPoint"))
            {
                SetBackPoint(hitInfo.collider.gameObject);
                backGridPoint.SetForwardPoint(gameObject);
            }
            else
            {
                SetBackPoint(null);
            }
        }
        else
        {

            Vector3 backtOffset = (Vector3.back * segmentDistanceGP);
            Vector3 newPosition = backtOffset + this.transform.position;
            // Logger.Debug(newPosition + "B=" + backtOffset + " + " + this.transform.position);
            GameObject newGridPointOBJ = Instantiate(this.gameObject, newPosition, argParent.localRotation) as GameObject;
            newGridPointOBJ.name = this.gameObject.name + "_";
            newGridPointOBJ.transform.parent = argParent;

            GridPoint gp = newGridPointOBJ.GetComponent<GridPoint>();
            if (gp.isValid() && gp.GP_CheckBounds())
            {
                newGridPointOBJ.name += pointsList.Count.ToString();
                SetBackPoint(newGridPointOBJ);
                pointsList.Add(newGridPointOBJ);
                toVisit.Enqueue(newGridPointOBJ);

            }
            else
            {
                SetBackPoint(null);
                Destroy(newGridPointOBJ);
            }
        }
    }
    void GP_Raycast_Rightward(List<GameObject> pointsList, Queue<GameObject> toVisit, Transform argParent)
    {
        RaycastHit hitInfo;
        // raycast right
        if (Physics.Raycast(transform.position, Vector3.right, out hitInfo, segmentDistanceGP, layerMask, QueryTriggerInteraction.Collide))
        {

            if (hitInfo.collider.gameObject.CompareTag("GridPoint"))
            {
                SetRightPoint(hitInfo.collider.gameObject);
                rightGridPoint.SetLeftPoint(gameObject);
            }
            else
            {
                SetRightPoint(null);
            }
        }
        else
        {
            Vector3 rightOffset = (Vector3.right * segmentDistanceGP);
            Vector3 newPosition = rightOffset + this.transform.position;
            //Logger.Debug(newPosition + "R=" + rightOffset + " + " + this.transform.position);

            GameObject newGridPointOBJ = Instantiate(this.gameObject, newPosition, argParent.localRotation) as GameObject;
            newGridPointOBJ.name = this.gameObject.name + "_";

            newGridPointOBJ.transform.parent = argParent;
            GridPoint gp = newGridPointOBJ.GetComponent<GridPoint>();
            if (gp.isValid() && gp.GP_CheckBounds())
            {
                newGridPointOBJ.name += pointsList.Count.ToString();
                SetRightPoint(newGridPointOBJ);
                pointsList.Add(newGridPointOBJ);
                toVisit.Enqueue(newGridPointOBJ);

            }
            else
            {
                SetRightPoint(null);
                Destroy(newGridPointOBJ);
            }
        }
    }
    void GP_Raycast_Leftward(List<GameObject> pointsList, Queue<GameObject> toVisit, Transform argParent)
    {
        RaycastHit hitInfo;



        // raycast left
        if (Physics.Raycast(transform.position, Vector3.left, out hitInfo, segmentDistanceGP, layerMask, QueryTriggerInteraction.Collide))
        {
            if (hitInfo.collider.gameObject.CompareTag("GridPoint"))
            {
                SetLeftPoint(hitInfo.collider.gameObject);
                leftGridPoint.SetRightPoint(gameObject);
            }
            else
            {
                SetLeftPoint(null);
            }
        }
        else
        {
            Vector3 leftOffset = (Vector3.left * segmentDistanceGP);
            Vector3 newPosition = leftOffset + this.transform.position;
            // Logger.Debug(newPosition + "L=" + leftOffset + " + " + this.transform.position);
            GameObject newGridPointOBJ = Instantiate(this.gameObject, newPosition, argParent.localRotation) as GameObject;
            newGridPointOBJ.name = this.gameObject.name + "_";
            newGridPointOBJ.transform.parent = argParent;
            GridPoint gp = newGridPointOBJ.GetComponent<GridPoint>();
            if (gp.isValid() && gp.GP_CheckBounds())
            {
                newGridPointOBJ.name += pointsList.Count.ToString();
                SetLeftPoint(newGridPointOBJ);
                pointsList.Add(newGridPointOBJ);
                toVisit.Enqueue(newGridPointOBJ);

            }
            else
            {
                SetLeftPoint(null);
                Destroy(newGridPointOBJ);
            }
        }
    }
    //public GameObject gridPoint;
    //public LayerMask layerMask = Physics.DefaultRaycastLayers;
    //float segmentDistance = 0.25f;
    //public float GetGpSegmentDist() { return segmentDistance; }
    ////public Material scannedMaterial;

    //[HideInInspector]
    //public GameObject forwardGameObject { get; private set; }
    //public GameObject leftGameObject { get; private set; }
    //public GameObject rightGameObject { get; private set; }
    //public GameObject backGameObject { get; private set; }
    //public bool Connected { get; private set; }

    //public bool wasvisited { get;  set; }

    //GridPoint forwardGridPoint;
    //GridPoint leftGridPoint;
    //GridPoint rightGridPoint;
    //GridPoint backGridPoint;

    //void Start()
    //{
    //    Connected = false;
    //    wasvisited = false;
    //}

    //public void turnCubeMeshOff() {
    //    transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().enabled = false;
    //    transform.GetChild(2).gameObject.SetActive(false);
    //}


    //public void Connect()
    //{
    //    Connected = true;
    //}

    //public bool isValid()
    //{
    //    // if there is floor beneath then this is a valid point
    //    RaycastHit hitInfo;
    //    return Physics.Raycast(transform.position, Vector3.down, out hitInfo, 3.0f, layerMask);
    //}

    //public void SetForwardPoint(GameObject forwardPoint)
    //{
    //    if (forwardPoint == null)
    //    {
    //        forwardGameObject = null;
    //        forwardGridPoint = null;
    //        return;
    //    }

    //    forwardGameObject = forwardPoint;
    //    forwardGridPoint = forwardGameObject.GetComponent<GridPoint>();
    //}

    //public void SetLeftPoint(GameObject leftPoint)
    //{
    //    if (leftPoint == null)
    //    {
    //        leftGameObject = null;
    //        leftGridPoint = null;
    //        return;
    //    }

    //    leftGameObject = leftPoint;
    //    leftGridPoint = leftGameObject.GetComponent<GridPoint>();
    //}

    //public void SetRightPoint(GameObject rightPoint)
    //{
    //    if (rightPoint == null)
    //    {
    //        rightGameObject = null;
    //        rightGridPoint = null;
    //        return;
    //    }

    //    rightGameObject = rightPoint;
    //    rightGridPoint = rightGameObject.GetComponent<GridPoint>();
    //}

    //public void SetBackPoint(GameObject backPoint)
    //{
    //    if (backPoint == null)
    //    {
    //        backGameObject = null;
    //        backGridPoint = null;
    //        return;
    //    }

    //    backGameObject = backPoint;
    //    backGridPoint = backGameObject.GetComponent<GridPoint>();
    //}

    //public void Scan(List<GameObject> pointsList, Queue<GameObject> toVisit, GameObject parentMapObject)
    //{
    //    /// <summary>
    //    /// when instantiated, perform raycasts to forward, left, right, and back
    //    /// if raycast hits spatial map point is set to null
    //    /// if raycast hits another gridpoint set respective property to that object and tell that object to do the same
    //    /// else instantiate a new gridpoint and add it to pointList
    //    /// </summary>

    //    RaycastHit hitInfo;

    //    // raycast forward
    //    if (Physics.Raycast(transform.position, Vector3.forward, out hitInfo, segmentDistance, layerMask, QueryTriggerInteraction.Collide))
    //    {
    //        if (hitInfo.collider.gameObject.CompareTag("GridPoint"))
    //        {
    //            SetForwardPoint(hitInfo.collider.gameObject);
    //            forwardGridPoint.SetBackPoint(gameObject);
    //        }
    //        else
    //        {
    //            SetForwardPoint(null);
    //        }
    //    }
    //    else
    //    {
    //        Vector3 newPosition = transform.position + (Vector3.forward * segmentDistance);
    //        GameObject newGridPoint = Instantiate(gridPoint, newPosition, Quaternion.identity) as GameObject;
    //        newGridPoint.transform.parent = parentMapObject.transform;

    //        GridPoint gp = newGridPoint.GetComponent<GridPoint>();
    //        if (gp.isValid() && gp.CheckBounds())
    //        {
    //            pointsList.Add(newGridPoint);
    //            toVisit.Enqueue(newGridPoint);
    //            SetForwardPoint(newGridPoint);
    //        }
    //        else
    //        {
    //            SetForwardPoint(null);
    //            Destroy(newGridPoint);
    //        }
    //    }

    //    // raycast right
    //    if (Physics.Raycast(transform.position, Vector3.right, out hitInfo, segmentDistance, layerMask, QueryTriggerInteraction.Collide))
    //    {
    //        if (hitInfo.collider.gameObject.CompareTag("GridPoint"))
    //        {
    //            SetRightPoint(hitInfo.collider.gameObject);
    //            rightGridPoint.SetLeftPoint(gameObject);
    //        }
    //        else
    //        {
    //            SetRightPoint(null);
    //        }
    //    }
    //    else
    //    {
    //        Vector3 newPosition = transform.position + (Vector3.right * segmentDistance);
    //        GameObject newGridPoint = Instantiate(gridPoint, newPosition, Quaternion.identity) as GameObject;
    //        newGridPoint.transform.parent = parentMapObject.transform;

    //        GridPoint gp = newGridPoint.GetComponent<GridPoint>();
    //        if (gp.isValid() && gp.CheckBounds())
    //        {
    //            pointsList.Add(newGridPoint);
    //            toVisit.Enqueue(newGridPoint);
    //            SetRightPoint(newGridPoint);
    //        }
    //        else
    //        {
    //            SetRightPoint(null);
    //            Destroy(newGridPoint);
    //        }
    //    }

    //    // raycast left
    //    if (Physics.Raycast(transform.position, Vector3.left, out hitInfo, segmentDistance, layerMask, QueryTriggerInteraction.Collide))
    //    {
    //        if (hitInfo.collider.gameObject.CompareTag("GridPoint"))
    //        {
    //            SetLeftPoint(hitInfo.collider.gameObject);
    //            leftGridPoint.SetRightPoint(gameObject);
    //        }
    //        else
    //        {
    //            SetLeftPoint(null);
    //        }
    //    }
    //    else
    //    {
    //        Vector3 newPosition = transform.position + (Vector3.left * segmentDistance);
    //        GameObject newGridPoint = Instantiate(gridPoint, newPosition, Quaternion.identity) as GameObject;
    //        newGridPoint.transform.parent = parentMapObject.transform;

    //        GridPoint gp = newGridPoint.GetComponent<GridPoint>();
    //        if (gp.isValid() && gp.CheckBounds())
    //        {
    //            pointsList.Add(newGridPoint);
    //            toVisit.Enqueue(newGridPoint);
    //            SetLeftPoint(newGridPoint);
    //        }
    //        else
    //        {
    //            SetLeftPoint(null);
    //            Destroy(newGridPoint);
    //        }
    //    }

    //    // raycast back
    //    if (Physics.Raycast(transform.position, Vector3.back, out hitInfo, segmentDistance, layerMask, QueryTriggerInteraction.Collide))
    //    {
    //        if (hitInfo.collider.gameObject.CompareTag("GridPoint"))
    //        {
    //            SetBackPoint(hitInfo.collider.gameObject);
    //            backGridPoint.SetForwardPoint(gameObject);
    //        }
    //        else
    //        {
    //            SetBackPoint(null);
    //        }
    //    }
    //    else
    //    {
    //        Vector3 newPosition = transform.position + (Vector3.back * segmentDistance);
    //        GameObject newGridPoint = Instantiate(gridPoint, newPosition, Quaternion.identity) as GameObject;
    //        newGridPoint.transform.parent = parentMapObject.transform;

    //        GridPoint gp = newGridPoint.GetComponent<GridPoint>();
    //        if (gp.isValid() && gp.CheckBounds())
    //        {
    //            pointsList.Add(newGridPoint);
    //            toVisit.Enqueue(newGridPoint);
    //            SetBackPoint(newGridPoint);
    //        }
    //        else
    //        {
    //            SetBackPoint(null);
    //            Destroy(newGridPoint);
    //        }
    //    }

    //    //gameObject.GetComponentInChildren<MeshRenderer>().material = scannedMaterial;
    //}

    ///// <summary>
    ///// Checks the bounds of this grid point. Returns false if we are removing this point.
    ///// </summary>
    ///// <returns>
    //public bool CheckBounds()
    //{
    //    // if there is not enough room for an agent, then delete point
    //    Vector3 center = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
    //    Vector3 halfExtents = new Vector3(segmentDistance * 0.5f, 0.5f, segmentDistance * 0.5f);

    //    if (Physics.CheckBox(center, halfExtents, Quaternion.identity, layerMask, QueryTriggerInteraction.Ignore))
    //    {
    //        // RemovePoint();
    //        return false;
    //    }

    //    return true;
    //}

    ///// <summary>
    ///// Checks the connectivity of this grid point to the root node. Returns false if we are removing this point.
    ///// </summary>
    ///// <returns>
    //public bool CheckConnectivity()
    //{
    //    if (!Connected)
    //    {
    //        RemovePoint();
    //        return false;
    //    }

    //    return true;
    //}

    //void RemovePoint()
    //{
    //    // Logger.Debug("Removing Point");
    //    if (forwardGameObject != null)
    //        forwardGridPoint.SetBackPoint(null);

    //    if (leftGameObject != null)
    //        leftGridPoint.SetRightPoint(null);

    //    if (rightGameObject != null)
    //        rightGridPoint.SetLeftPoint(null);

    //    if (backGameObject != null)
    //        backGridPoint.SetForwardPoint(null);

    //    Destroy(gameObject);
    //}
}
