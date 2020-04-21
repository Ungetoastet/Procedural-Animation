using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoodManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Different Faces of the Character. Normally: 'Normal','Angry','Dead'")]
    private GameObject[] FaceArray;

    [SerializeField]
    private Joint[] RifleGrabbingPoints;

    [SerializeField]
    private Collider[] RifleColliders;

    private int ActiveMood;

    // Start is called before the first frame update
    void Start()
    {
        /*Deactivate Weapon Colliders whilst holding
        for (int i = 0; i < RifleColliders.Length; i++)
        {
            RifleColliders[i].enabled = false;
        }
        */
    }

    // Update is called once per frame
    public void FaceUpdate(int NewMood)
    {
        //Cycle trough faces and just activate the new one if not already dead
        if (ActiveMood != 2)
        {
            for (int i = 0; i < FaceArray.Length; i++)
            {
                if (i == NewMood)
                    FaceArray[i].SetActive(true);
                else
                    FaceArray[i].SetActive(false);
            }
            ActiveMood = NewMood;
        }
        if (ActiveMood == 1)
        {
            //Enable Angry Mode when hit by ball
            if (GetComponent<AttackScript>() != null) GetComponent<AttackScript>().enabled = true;
            if (GetComponent<RifleAttackScript>() != null) GetComponent<RifleAttackScript>().enabled = true;
            GetComponent<AcR_Body>().enabled = true;
        }

        //Being dead is now a mood lol
        if (ActiveMood == 2)
        {
            //trying my best to reduce errors ("...Has already been destroyed...")
            if (GetComponent<RifleAttackScript>() != null && RifleGrabbingPoints[0].breakForce != 0 && RifleGrabbingPoints[0] != null) 
            {
                //Drop the weapon
                for (int i = 0; i < RifleGrabbingPoints.Length; i++)
                {
                    RifleGrabbingPoints[i].breakForce = 0;
                }
                //Activate Weapon Colliders
                for (int i = 0; i < RifleColliders.Length; i++)
                {
                    RifleColliders[i].enabled = true;
                }
            }
            if (GetComponent<RifleAttackScript>() != null) GetComponent<RifleAttackScript>().enabled = false;
        }
    }
}
