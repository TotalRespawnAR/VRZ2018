using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackRoomMNGR : MonoBehaviour {

    public SpawnsMNGR _mySpawnsMngr;
    public PatrolsMNGR _myPatrolsMngr;

    private void Awake()
    {
        _mySpawnsMngr = GetComponentInChildren<SpawnsMNGR>();
        _myPatrolsMngr = GetComponentInChildren<PatrolsMNGR>();
    }

}
