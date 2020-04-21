using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootScript : MonoBehaviour
{
    [SerializeField]
    private Vector3 ShootOffset;

    [SerializeField]
    private GameObject ProjectilePrefab;

    [SerializeField]
    private Vector3 ShotDirection;

    [SerializeField]
    private float ShotForce;

    [SerializeField]
    private KeyCode keyCode;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Yeet "Projectile Prefab" with "ShotForce" in "ShotDirection" if "KeyCode" is pressed
        if (Input.GetKeyDown(keyCode))
        {
            var Bullet = GameObject.Instantiate(ProjectilePrefab, transform.position + transform.TransformDirection(ShootOffset), transform.rotation);
            Rigidbody rb = Bullet.GetComponent<Rigidbody>();
            rb.AddRelativeForce(ShotDirection * ShotForce, ForceMode.Impulse);
            Destroy(Bullet, 5);
        }
    }
}
