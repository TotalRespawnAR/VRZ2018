using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room3DTextCTRL : MonoBehaviour {


    public Text3DContructor Text3Dctor;

    #region dependencies
    #endregion

    #region PublicVars
    #endregion

    #region PrivateVars
    #endregion

 

    #region PublicMethods
    public void BuildAndSet3dTitle(string argTitleTOBuild, bool isFinal3d) {
        Text3Dctor.Build_NEW_3DWord(argTitleTOBuild, isFinal3d);
    }
    #endregion

    #region PrivateMethods
 
    private void OnDestroy()
    {

    }
    #endregion
}
