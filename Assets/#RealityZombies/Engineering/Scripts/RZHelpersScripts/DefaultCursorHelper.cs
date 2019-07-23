using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultCursorHelper : MonoBehaviour {

    

    public SkinnedMeshRenderer top;
    public SkinnedMeshRenderer bot;
    public SkinnedMeshRenderer rig;
    public SkinnedMeshRenderer lef;
    // Use this for initialization
    public void HideMesh() { top.enabled = false; bot.enabled = false; rig.enabled = false; lef.enabled = false; }
    public void ShowMesh() { top.enabled = true; bot.enabled = true; rig.enabled = true; lef.enabled = true; }
}
