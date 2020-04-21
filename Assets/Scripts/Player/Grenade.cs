using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float Delay;

    [SerializeField]
    private GameObject BoomFX;

    private float Countdown;
    private bool didBoom;

    // Start is called before the first frame update
    void Start()
    {
        Countdown = Delay;
        didBoom = false;
    }

    // Update is called once per frame
    void Update()
    {     
        if (Countdown <= 0 && !didBoom) Boom();
        else Countdown -= Time.deltaTime;
    }

    void Boom()
    {
        didBoom = true;
        GameObject.Instantiate(BoomFX, transform.position, transform.rotation);

        Destroy(gameObject);
    }
}
