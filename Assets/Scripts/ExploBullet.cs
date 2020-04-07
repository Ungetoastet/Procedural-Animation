using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploBullet : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField]
    private float range;

    [SerializeField]
    private float force;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        rb.isKinematic = true;
        Boom();
    }

    void Boom()
    {
        for (int i = 0; i < Physics.OverlapSphere(transform.position, range).Length; i++)
        {
            if (Physics.OverlapSphere(transform.position, range)[i].GetComponent<AcR_Body>() != null)
            {
                Physics.OverlapSphere(transform.position, range)[i].GetComponent<AcR_Body>().enabled = false;
            }
            if (Physics.OverlapSphere(transform.position, range)[i].GetComponent<Rigidbody>() != null)
            {
                Physics.OverlapSphere(transform.position, range)[i].GetComponent<Rigidbody>().AddExplosionForce(force, transform.position, range);
            }
        }
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
