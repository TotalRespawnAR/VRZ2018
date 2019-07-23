using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMyBeh
{

    event EventHandler<DerivedEventArg> ShapeChanged;
    int GetMyBehId();
}
