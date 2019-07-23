using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArgsEnemyMode : EventArgs
{


    ModesEnum _mode;

    public ArgsEnemyMode(ModesEnum mode)
    {
        _mode = mode;
    }

    public ModesEnum Mode
    {
        get
        {
            return _mode;
        }

        set
        {
            _mode = value;
        }
    }
}
