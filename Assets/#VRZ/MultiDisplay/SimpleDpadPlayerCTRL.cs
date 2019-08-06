using UnityEngine;

public class SimpleDpadPlayerCTRL : MonoBehaviour, IShootable
{

    private Transform m_Cam;                  // A reference to the main camera in the scenes transform
    private Vector3 m_CamForward;             // The current forward direction of the camera
    private Vector3 m_Move;
    private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.
    public Rigidbody m_Rigidbody;
    public Animator m_Animator;
    public Animator m_AnimatorMesh;
    public float VertValue;
    public float HorValue;
    public bool PressA;
    public bool PressB;
    public bool PressX;
    public bool PressY;
    GameSettings _gs;
    public float jumpSpeed = 5f;
    public bool isGrounded;
    public float Yrbvel;
    RigidbodyConstraints originalConstrains;

    public GameObject FireBAll;
    FireBallProjectile fbp;
    Transform RightHandTrans;
    Transform CamTarget;

    bool IsVisible = false;
    private void Start()
    {
        CamTarget = GameObject.FindGameObjectWithTag("Player").transform;
        _gs = GameSettings.Instance;

        m_Rigidbody = GetComponent<Rigidbody>();
        m_Animator = GetComponent<Animator>();
        originalConstrains = m_Rigidbody.constraints;
        RightHandTrans = m_Animator.GetBoneTransform(HumanBodyBones.RightHand);
    }
    bool highPt = false;


    private void Update()
    {

        UpdateAnimator();
    }


    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {

        if (PressY) { SetVisiblePlz(false); }
        if (PressX) { SetVisiblePlz(true); }

        transform.LookAt(CamTarget);
        Yrbvel = m_Rigidbody.velocity.y;


        ReadPlayer2Inputs();
        if (PressA && isGrounded)
        {
            AnimateJump();
        }
    }

    void ReadPlayer2Inputs()
    {
        if (!canMove) return;
        if (_gs != null)
        {
            if (_gs.UseXboxCTRL) { UseXboxCTRL(); } else { UseKeyboard(); }
        }
    }

    #region keyboard
    void UseKeyboard()
    {
        HorValue = ReadHorizontal();
        VertValue = ReadVertical();
        PressA = Input.GetKey(KeyCode.G);
        PressB = Input.GetKey(KeyCode.H);
        PressX = Input.GetKey(KeyCode.T);
        PressY = Input.GetKey(KeyCode.Y);
    }

    float ReadVertical()
    {
        float Vval = 0f;
        if (Input.GetKey(KeyCode.I)) { Vval = 1.0f; }//up
        if (Input.GetKey(KeyCode.K)) { Vval = -1.0f; }//down

        return Vval;
    }


    float ReadHorizontal()
    {
        float Hval = 0f;
        if (Input.GetKey(KeyCode.J)) { Hval = 1.0f; }
        if (Input.GetKey(KeyCode.L)) { Hval = -1.0f; }

        return Hval;
    }

    #endregion

    void UseXboxCTRL()
    {
        VertValue = Input.GetAxis("xbxP2_JSL_Vert");
        HorValue = Input.GetAxis("xbxP2_JSL_Hor");
        //if (Input.GetButton("xbxP2_A")) { }
        //if (Input.GetButton("xbxP2_B")) { }
        PressA = Input.GetButton("xbxP2_A");
        PressB = Input.GetButton("xbxP2_B");
        PressX = Input.GetButton("xbxP2_X");
        PressY = Input.GetButton("xbxP2_Y");
    }

    void USeArduinoCtrl() { }



    void UpdateAnimator()
    {
        m_Animator.SetFloat("VertFloat", VertValue);
        m_Animator.SetFloat("HoriFloat", HorValue);
        m_Animator.SetBool("GroundedBool", isGrounded);
        m_Animator.SetFloat("RbVertVelo", Yrbvel);
    }

    void RigidBodyJump()
    {
        print("forc up");

        m_Rigidbody.AddForce(new Vector3(0, 2, 0) * jumpSpeed, ForceMode.Impulse);
        isGrounded = false;
        m_Animator.ResetTrigger("TrigJump");
    }

    void AnimateJump()
    {
        if (isGrounded)
        {
            //  m_Rigidbody.isKinematic = false;
            m_Animator.SetTrigger("TrigJump");
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == ("SpatialMesh") && isGrounded == false)
        {
            isGrounded = true;
            m_Rigidbody.constraints = RigidbodyConstraints.FreezePositionY;

            //m_Rigidbody.isKinematic = true;
        }
    }

    private void LateUpdate()
    {
        if (isGrounded) m_Rigidbody.constraints = originalConstrains;
    }
    public void AnimEvent(int argJumpAimFinished)
    {

        if (argJumpAimFinished == 0)
        {
            RigidBodyJump();
            m_Animator.SetTrigger("TrigThrow");
        }
        else
            if (argJumpAimFinished == 1)
        {
            print("time to shoot");
            m_Animator.ResetTrigger("TrigThrow");
            SpawnErekiBall();
        }
        else if (argJumpAimFinished == 2)
        {
            fbp.ReleaseTheProjectile();
        }
    }

    void SpawnErekiBall()
    {
        GameObject ball = Instantiate(FireBAll, RightHandTrans.position, Quaternion.identity);
        fbp = ball.GetComponent<FireBallProjectile>();
        ball.transform.parent = RightHandTrans;
    }

    public void SetVisiblePlz(bool argIsAlive)
    {
        IsVisible = argIsAlive;

        m_AnimatorMesh.SetBool("isVisible", IsVisible);
    }

    bool canMove = false;
    public void AllowInputs(bool argCanMove) { canMove = argCanMove; }

    int Shotsmax = 5;

    public void ResetShotCount()
    {
        Shotsmax = 5;
    }
    public void Shot(Bullet argBullet)
    {
        print("ouch");
        Shotsmax--;
        if (Shotsmax <= 0)
        {
            GameEventsManager.Instance.CALL_Player2Died();
        }
    }

    public void aimed(bool argTF)
    {
        throw new System.NotImplementedException();
    }
}
