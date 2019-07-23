using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TickBools   {

    private int tickTime;
    private bool shouldGrave;
    private bool shouldgassmask;
    private bool shouldbigmutanat;
    private bool shouldteeth;
    private bool shouldfighters;
    private bool shouldpumpkins;
    private bool shouldClaws;
    private bool shouldAxeman;
    private bool shouldFLy1;
    private bool shouldFLy2;


    public bool ShouldAxeman
    {
        get
        {
            return shouldAxeman;
        }

        set
        {
            shouldAxeman = value;
        }
    }
    public bool ShouldFLy1
    {
        get
        {
            return shouldFLy1;
        }

        set
        {
            shouldFLy1 = value;
        }
    }

    public bool ShouldFLy2
    {
        get
        {
            return shouldFLy2;
        }

        set
        {
            shouldFLy2 = value;
        }
    }


    public bool ShouldGrave
    {
        get
        {
            return shouldGrave;
        }

        set
        {
            shouldGrave = value;
        }
    }


    public bool ShouldBigMutant
    {
        get
        {
            return shouldbigmutanat;
        }

        set
        {
            shouldbigmutanat = value;
        }
    }
    public bool ShouldGassmask
    {
        get
        {
            return shouldgassmask;
        }

        set
        {
            shouldgassmask = value;
        }
    }

    public bool Shouldteeth
    {
        get
        {
            return shouldteeth;
        }

        set
        {
            shouldteeth = value;
        }
    }

    public bool Shouldfighters
    {
        get
        {
            return shouldfighters;
        }

        set
        {
            shouldfighters = value;
        }
    }

    public bool Shouldpumpkins
    {
        get
        {
            return shouldpumpkins;
        }

        set
        {
            shouldpumpkins = value;
        }
    }

    public bool ShouldClaws
    {
        get
        {
            return shouldClaws;
        }

        set
        {
            shouldClaws = value;
        }
    }

    public int TickTime
    {
        get
        {
            return tickTime;
        }

        set
        {
            tickTime = value;
        }
    }
}
