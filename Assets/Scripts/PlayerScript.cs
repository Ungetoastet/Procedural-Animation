using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    private float PlayerSpeed = 5;

    [SerializeField]
    private float JumpForce = 20;

    private Rigidbody rb;

    private bool OnGround;

    private bool isMoving;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        int layerMask = 1 << 31;
        layerMask = ~layerMask;

        OnGround = Physics.Raycast(gameObject.transform.position, Vector3.down, 1.1f);
        if (isMoving || !OnGround) rb.drag = 1;
        else rb.drag = 50;
        if (Input.GetKeyDown(KeyCode.Space) && OnGround) rb.AddForce(new Vector3(0, JumpForce, 0), ForceMode.Impulse);
    }

    private void Move()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            isMoving = true;
            rb.AddRelativeForce(Input.GetAxis("Horizontal") * PlayerSpeed, 0, Input.GetAxis("Vertical") * PlayerSpeed);
        }
        else isMoving = false;
    }
}
