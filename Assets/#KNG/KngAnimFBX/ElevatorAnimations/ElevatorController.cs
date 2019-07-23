using UnityEngine;

public class ElevatorController : MonoBehaviour
{


    public static ElevatorController Instance = null;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(this.gameObject);
    }

    bool DoorStateOpen = false;
    Animator m_anim;
    public Animator m_animElevatorUpDown;
    bool ElevatorStateUP = false;
    // Use this for initialization
    void Start()
    {
        m_anim = GetComponent<Animator>();

    }

    public void PlaceElevatorAtGroundLevel()
    {
        //print("e at ground");
        ElevatorStateUP = true;
        m_animElevatorUpDown.SetBool("BoolGoUp", ElevatorStateUP);
        m_animElevatorUpDown.Play("ElevatorUpState");
    }

    public void ElevateMe()
    {
        if (!ElevatorStateUP)
        {
            ElevatorStateUP = true;
            m_animElevatorUpDown.SetBool("BoolGoUp", ElevatorStateUP);
        }
    }
    public void LowerMe()
    {
        if (ElevatorStateUP)
        {
            ElevatorStateUP = false;
            m_animElevatorUpDown.SetBool("BoolGoUp", ElevatorStateUP);
        }
    }

    public void OpenDoor()
    {

        if (!DoorStateOpen)
        {
            DoorStateOpen = true;
            m_anim.SetBool("BoolOpenElevator", DoorStateOpen);
        }
    }
    public void CloseDoor()
    {

        if (DoorStateOpen)
        {
            DoorStateOpen = false;
            m_anim.SetBool("BoolOpenElevator", DoorStateOpen);
        }
    }
}
