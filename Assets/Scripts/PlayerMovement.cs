using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;

    private GameObject Camera;
    bool OnGround;
    int Clamp;

    [SerializeField]
    private float WalkSpeed;

    [SerializeField]
    private float JumpForce;

    [SerializeField]
    private float GroundCheckDistance;

    [SerializeField]
    private float XSensitivity;
    [SerializeField]
    private float YSensitivity;

    // Start is called before the first frame update
    void Start()
    {
        //Get needed Components and steal the mouse >:)
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Camera = GetComponentInChildren<Camera>().gameObject;
    }

    void FixedUpdate()
    {
        Walk();
        CheckGround();
    }

    private void Update()
    {
        Rotate();
        Jump();

        //My poor tries to stop the player from rotating 360 degrees on the x axis.
        if (transform.position.y <= -50) transform.SetPositionAndRotation(Vector3.zero, new Quaternion(0, 0, 0, 0));

        if (Camera.transform.rotation.eulerAngles.x > 160 && Camera.transform.rotation.eulerAngles.x < 90)
            Clamp = 0;
        else if (Camera.transform.rotation.eulerAngles.x < 290 && Camera.transform.rotation.eulerAngles.x > 200)
            Clamp = 2;
        else
            Clamp = 1;
    }

    void Walk()
    {
        //Simply push the player around
        if(OnGround) rb.AddRelativeForce(Input.GetAxisRaw("Horizontal") * WalkSpeed, 0, Input.GetAxisRaw("Vertical") * WalkSpeed);
        else rb.AddRelativeForce(Input.GetAxisRaw("Horizontal") * WalkSpeed / 25, 0, Input.GetAxisRaw("Vertical") * WalkSpeed / 20);
    }

    void CheckGround()
    {
        //Adjust drag if being in the air
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit) && hit.distance < GroundCheckDistance)
        {
            OnGround = true;
            rb.drag = 10;
        }
        else
        {
            OnGround = false;
            rb.drag = 0.5f;
        }
    }

    void Rotate()
    {
        //Rotate the entire player around the y axis
        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * XSensitivity));

        if (Input.GetAxis("Mouse Y") != 0)
        {
            if (Clamp == 0 && Input.GetAxis("Mouse Y") > 0)
            {
                Camera.transform.Rotate(new Vector3(-Input.GetAxis("Mouse Y") * YSensitivity, 0, 0), Space.Self);
            }
            if (Clamp == 2 && Input.GetAxis("Mouse Y") < 0)
            {
                Camera.transform.Rotate(new Vector3(-Input.GetAxis("Mouse Y") * YSensitivity, 0, 0), Space.Self);
            }
            if (Clamp == 1)
            {
                Camera.transform.Rotate(new Vector3(-Input.GetAxis("Mouse Y") * YSensitivity, 0, 0), Space.Self);
            }
        }
    }

    void Jump()
    {
        //Jump.
        if (Input.GetKeyDown(KeyCode.Space) && OnGround)
        {
            rb.AddRelativeForce(new Vector3(0, JumpForce, 0), ForceMode.Impulse);
            OnGround = false;
        }
    }
}
