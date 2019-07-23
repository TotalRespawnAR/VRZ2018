using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyMeshComp   {

    void ToggleExternalMesh_inTime(bool argOnOff, float argeTimeTotoggle);
    void MeshDisolveToNothing();
    void MeshDisolveFromNothing();
  
}
