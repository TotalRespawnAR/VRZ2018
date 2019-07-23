using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArgsAnimationTrigger : EventArgs
{

    bool _witForEndAnimEvent;
    string _trigAnimationName;

    public ArgsAnimationTrigger(bool witForEndAnimEvent, string trigAnimationName)
    {
        _witForEndAnimEvent = witForEndAnimEvent;
        _trigAnimationName = trigAnimationName;
    }

    public bool WitForEndAnimEvent
    {
        get
        {
            return _witForEndAnimEvent;
        }

        set
        {
            _witForEndAnimEvent = value;
        }
    }

    public string TrigAnimationName
    {
        get
        {
            return _trigAnimationName;
        }

        set
        {
            _trigAnimationName = value;
        }
    }
}
