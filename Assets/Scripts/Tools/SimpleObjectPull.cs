using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleObjectPull : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField]
    private GameObject PullAim;

    [SerializeField]
    private float PullStrength;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddForce(Vector3.Normalize(PullAim.transform.position - rb.position) * PullStrength);
    }
}
