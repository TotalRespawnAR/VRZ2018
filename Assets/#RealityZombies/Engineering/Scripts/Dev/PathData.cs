using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathData : MonoBehaviour {


   
    public bool isClosedLoop;


    /// <summary>
    /// VERY IMPORTANT to note:
    ///   
    ///        spawnpoint or present location of zombie ---------------> X
    ///        
    ///        the first pont in this structure must be far    --------> P[0]
    ///                                                                    |
    ///                                                                  P[1]
    ///                                                                    |
    ///                                                                   ...
    ///                                                                    |
    ///                                                                   P[last]     ---- the place of last target depends on ALpha / Bravo  (so if closed loop , do not return this last guy 
    ///                                                                   
    ///                                                                     Also:
    ///                                                                     the last point should be tagged HotSpotTag or smthn cuz zombies just ask what is the active waypoint?
    ///                                                                                                                            this object gives it to the zombie , and increments the next index
    ///                                                                                                                            the zombie has no idea that last nod is the last node , easiest fix is to thag the node and have the zombie checkit .
    ///                                                                                                                            the zombie will then head to it and do REACHING when it's .7 away 
    /// </summary>
    /// <returns></returns>
    /// 
    public Vector3 GET_ActiveWaypoint() {

        if (isClosedLoop) {
            return Get_ActiveTarget_Waypoint_LOOP();
        }
        else
        { return Get_ActiveTarget_Waypoint_LANE(); }
    } 

      Vector3 Get_ActiveTarget_Waypoint_LOOP() {
      //  if (Path_TargetWayPoints.Count <1 ) { Debug.Log("i'm empty dude"); return null; }

        if (_activeTargetWaypoint_index >= Path_TargetWayPoints.Count)
        {
            _activeTargetWaypoint_index = 0;
        }
        
        Vector3 activeTarget = Path_TargetWayPoints[_activeTargetWaypoint_index];
        _activeTargetWaypoint_index++;
        return activeTarget;
    }


      Vector3 Get_ActiveTarget_Waypoint_LANE()
    {

        if (_activeTargetWaypoint_index >= Path_TargetWayPoints.Count)
        {
            _activeTargetWaypoint_index = _activeTargetWaypoint_index= Path_TargetWayPoints.Count-1;  //will keepp return ing the last point on the list . Zombie should have checked for tag . if tag is hotspot, dont ask for a new point 
        }

        Vector3 activeTarget = Path_TargetWayPoints[_activeTargetWaypoint_index];
        _activeTargetWaypoint_index++;
        return activeTarget;
    }


    public int GetActiveIndex() {
        return _activeTargetWaypoint_index ;

    }
    [SerializeField]
   public List<Vector3> Path_TargetWayPoints = new List<Vector3>();

    private int _activeTargetWaypoint_index=0;
    private void Awake()
    {
        _activeTargetWaypoint_index = 0;
    }
    public void ResetMyIndex()
    {
        _activeTargetWaypoint_index = 0;
    }
    public PathData getcopy_with_insertion(Vector3 _argGateway)
    {
        PathData o = gameObject.AddComponent<PathData>();
        o.isClosedLoop = this.isClosedLoop;

        o.Path_TargetWayPoints = new List<Vector3>();

        o.Path_TargetWayPoints.Add(_argGateway);
        for (int x = 0; x < Path_TargetWayPoints.Count; x++) {
            o.Path_TargetWayPoints.Add(this.Path_TargetWayPoints[x]);
        }
        return o;

    }
    public PathData getcopy_with_insertion_LESS_ONE(Vector3 _argGateway)
    {
        PathData o = gameObject.AddComponent<PathData>();
        o.isClosedLoop = this.isClosedLoop;

        o.Path_TargetWayPoints = new List<Vector3>();

        o.Path_TargetWayPoints.Add(_argGateway);
        for (int x = 1; x < Path_TargetWayPoints.Count; x++)
        {
            o.Path_TargetWayPoints.Add(this.Path_TargetWayPoints[x]);
        }
        return o;

    }
    

    public void  InitPathData(Vector3 _alphaBravoHotspot) {
        for (int x = 0; x < this.transform.childCount; x++)
        {
            Path_TargetWayPoints.Add(this.transform.GetChild(x).position);
        }

        //if lane , add the hotspot as last transform in path
        if (!isClosedLoop)
        { 
          Path_TargetWayPoints.Add(_alphaBravoHotspot);       
        }
    }

    public void InitPathData( )
    {
        //for the closed loop , 
        for (int x = 0; x < this.transform.childCount; x++)
        {
            Path_TargetWayPoints.Add(this.transform.GetChild(x).position);
        }

    }

    //public void Insert_gateWay(Vector3 _argGateway) {

    //   // Transform _copofgateway = _argGateway;

    //    //List<Transform> caashedlist = new List<Transform>();
    //    //caashedlist = Path_TargetWayPoints;

    //    //Path_TargetWayPoints.Clear();

    //    //Path_TargetWayPoints.Add(_argGateway);
    //    //foreach (Transform t in caashedlist)
    //    //    Path_TargetWayPoints.Add(t);

    //         Path_TargetWayPoints.Insert(0, _argGateway);
    //}


}
