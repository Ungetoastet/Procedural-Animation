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
            //Get all enemys and disable their movement script to turn them into passive ragdolls
            if (Physics.OverlapSphere(transform.position, range)[i].GetComponent<AcR_Body>() != null)
            {
                Physics.OverlapSphere(transform.position, range)[i].GetComponent<AcR_Body>().enabled = false;
            }
            //Add Explosion Force to all Rigidbodys nearby.
            if (Physics.OverlapSphere(transform.position, range)[i].GetComponent<Rigidbody>() != null)
            {
                Physics.OverlapSphere(transform.position, range)[i].GetComponent<Rigidbody>().AddExplosionForce(force, transform.position, range);
            }
        }
        //Delete the granade after the explosion
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        //Visualise the explosion radius in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
