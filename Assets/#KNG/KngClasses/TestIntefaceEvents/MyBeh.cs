using System;
using UnityEngine;
//using UnityEngine.Events;

public class MyBeh : MonoBehaviour, IMyBeh
{

    public int MyBehID;
    EBSTATE _behState;

    public EBSTATE BehState
    {
        get
        {
            return _behState;
        }

        set
        {
            _behState = value;
            ChangeShape();
        }
    }

    // public event MyEventHandler ShapeChanged;




    public event EventHandler<DerivedEventArg> ShapeChanged;

    public int GetMyBehId()
    {
        return MyBehID;
    }

    void ChangeShape()
    {
        OnShapeChanged(new DerivedEventArg(MyBehID, _behState));

    }

    //void OnShapeChanged(MyEventArgs e)
    //{
    //       if (ShapeChanged != null) ShapeChanged(this,e);
    //}

    void OnShapeChanged(DerivedEventArg derivedEventArg)
    {
        // throw new NotImplementedException();
        if (ShapeChanged != null) ShapeChanged(this, derivedEventArg);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("MyBeh " + MyBehID + " is  sending");

            BehState = (EBSTATE)MyBehID;
        }

    }
}
