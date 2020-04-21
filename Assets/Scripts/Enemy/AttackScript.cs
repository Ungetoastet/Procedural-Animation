using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    [SerializeField]
    private Rigidbody[] Fists;

    [SerializeField]
    private float PunchIntervall;

    [SerializeField]
    private float PunchStregth;

    private GameObject Player;
    private float TimeTillPunch;
    private int ActiveFistIndex;
    private AcR_Body Movement;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Movement = GetComponent<AcR_Body>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Movement.enabled) gameObject.GetComponent<AttackScript>().enabled = false;
        //Handle Punch Intervalls
        if (TimeTillPunch <= 0) Punch();
        else TimeTillPunch -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        //Move the Enemy towards the Player
        gameObject.GetComponent<Rigidbody>().AddRelativeForce(Player.transform.position - gameObject.transform.position);
    }
    private void Punch()
    {
        //Punch the Player
        TimeTillPunch = PunchIntervall;
        ActiveFistIndex++;
        if (ActiveFistIndex == Fists.Length) ActiveFistIndex = 0;
        Fists[ActiveFistIndex].AddForce(Vector3.Normalize(Player.transform.position - Fists[ActiveFistIndex].gameObject.transform.position) * PunchStregth, ForceMode.Impulse);
    }
}
