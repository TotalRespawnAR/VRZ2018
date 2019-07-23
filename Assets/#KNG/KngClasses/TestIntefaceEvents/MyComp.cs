using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyComp : MonoBehaviour , IMyComp{
    IMyBeh _imybeh;

    private void OnEnable() {
        _imybeh = GetComponent<IMyBeh>();
        _imybeh.ShapeChanged += HeardImyBEh;
    }
    private void OnDisable(){
        _imybeh.ShapeChanged -= HeardImyBEh;
    }

    void HeardImyBEh( object o, DerivedEventArg e) {
        Debug.Log(" listener "+_imybeh.GetMyBehId() + " heard it from e." +e.Id+" newstate" + e.Zstate);
    }
}
