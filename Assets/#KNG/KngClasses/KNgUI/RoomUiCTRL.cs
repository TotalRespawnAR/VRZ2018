// @Author Nabil Lamriben ©2018
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomUiCTRL : MonoBehaviour {
    #region dependencies
    RoomCanvasCTRL _roomCanvasCTRL;
    Room3DTextCTRL _room3dTextCTRL;
    #endregion

    #region PublicVars
    #endregion

    #region PrivateVars
    #endregion

    #region INIT
    void OnEnable()
    {

    }

    private void OnDisable()
    {

    }

    private void Awake()
    {
        _roomCanvasCTRL = GetComponent<RoomCanvasCTRL>();
        _room3dTextCTRL = GetComponent<Room3DTextCTRL>();
    }

    private void Start()
    {

    }
    #endregion

    #region PublicMethods
    public void ShowTextOnCanvas_thenFadeit_andPut3DTitle(string str, bool isFinal) {

        _roomCanvasCTRL.SetCanvasText_AndFadeInOut(str,isFinal);
    }
    public void Set_3D_Title(string arg3DTite, bool isFinal) {
        _room3dTextCTRL.BuildAndSet3dTitle(arg3DTite, isFinal);
    }
    #endregion

    #region PrivateMethods
 
    private void OnDestroy()
    {

    }
    #endregion

}
