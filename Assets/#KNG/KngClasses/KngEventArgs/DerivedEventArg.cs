using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DerivedEventArg : EventArgs
{
    int _id;
    EBSTATE _zstate;
    public DerivedEventArg(int behid, EBSTATE argnewstate) {
        _id = behid;
        _zstate = argnewstate;
    }
    public int getEventBehId() { return _id; }
    public EBSTATE Zstate
    {
        get
        {

           // Debug.Log(_id + " "+_zstate );
            return _zstate;
        }

        set
        {
            _zstate = value;
        }
    }

    public int Id
    {
        get
        {
            return _id;
        }

        set
        {
            _id = value;
        }
    }
}
