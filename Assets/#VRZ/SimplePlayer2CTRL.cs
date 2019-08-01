using UnityEngine;

public class SimplePlayer2CTRL : MonoBehaviour
{
    public Transform Player1Trans;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        Player1Trans = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    //float xmove;
    //float ymove;
    //void Update()
    //{

    //    float translation = Mathf.Abs(Input.GetAxis("Vertical"));
    //    float rotation = Input.GetAxis("Horizontal");
    //    //print(rotation + " " + translation);
    //    print(this.transform.position.x + "," + this.transform.position.y + " ," + this.transform.position.z);
    //    transform.LookAt(new Vector3(Player1Trans.position.x, this.transform.position.y, Player1Trans.position.z));
    //    //  transform.Translate(new Vector3(xmove, transform.position.y, ymove) * Time.deltaTime);
    //    //  transform.Translate(Time.deltaTime * xmove, this.transform.position.y, Time.deltaTime * xmove, this.transform);
    //    // transform.position =



    //}


    CharacterController characterController;
    public float movementSpeed = 5.0f;
    private Vector3 moveDirection = Vector3.zero;

    void Update()
    {
        transform.LookAt(new Vector3(Player1Trans.position.x, this.transform.position.y, Player1Trans.position.z));
        if (characterController.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            moveDirection = moveDirection * movementSpeed;
        }
        //Gravity
        moveDirection.y -= 10f * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);
    }
}
