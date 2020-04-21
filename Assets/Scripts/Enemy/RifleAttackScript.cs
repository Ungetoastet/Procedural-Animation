using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleAttackScript : MonoBehaviour
{
    [SerializeField]
    private Transform PistolMuzzle;

    [SerializeField]
    private Rigidbody PistolRB;

    [SerializeField]
    private float Recoil = 50;

    [SerializeField]
    private AudioClip ShotSFX;

    [SerializeField]
    private ParticleSystem MuzzleFlash;

    [SerializeField]
    private float WalkSpeed;

    [SerializeField]
    private float Stabilisation;

    [SerializeField]
    private SimpleObjectPull[] SecondaryHandStab;

    private GameObject Player;
    private Rigidbody rb;
    private GameObject Pistol;
    private float PistolCooldown = 0;
    private AudioSource Sound;

    // Start is called before the first frame update
    void Start()
    {
        Sound = GetComponent<AudioSource>();
        Pistol = PistolRB.gameObject;
        Player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
        for (int i = 0; i < SecondaryHandStab.Length; i++)
        {
            SecondaryHandStab[i].enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Walk towards the Player
        PistolRB.AddForce(Vector3.Normalize(Player.transform.position - PistolRB.transform.position) * WalkSpeed + Vector3.up * Stabilisation);

        //Shoot if Pistol has the right orientation
        if (CheckPlayer() && PistolCooldown <= 0)
        {
            Shoot();
        }
        if (PistolCooldown > 0) PistolCooldown -= Time.deltaTime;
    }

    void Shoot()
    {
        //Play Sounds
        Sound.clip = ShotSFX;
        Sound.Play();
        //Give 'em some wacky knockback
        PistolRB.AddRelativeForce(Vector3.down * Recoil, ForceMode.Impulse);
        PistolCooldown = 0.5f;
        //Play the ParticleFX
        MuzzleFlash.Play();
    }

    bool CheckPlayer()
    {
        //cast a bunch of arrays to check if the player is in line (why doesn't unity have Physics.drawCone?)
        bool isAimingOnPlayer = false;
        isAimingOnPlayer = 
            Physics.Raycast(PistolMuzzle.position, PistolMuzzle.TransformDirection(Vector3.forward), out RaycastHit hita) && hita.collider.gameObject.CompareTag("Player")||
            Physics.Raycast(PistolMuzzle.position, PistolMuzzle.TransformDirection(Vector3.forward + Vector3.down / 10), out RaycastHit hitb) && hitb.collider.gameObject.CompareTag("Player") ||
            Physics.Raycast(PistolMuzzle.position, PistolMuzzle.TransformDirection(Vector3.forward + Vector3.up / 10), out RaycastHit hitc) && hitc.collider.gameObject.CompareTag("Player") ||
            Physics.Raycast(PistolMuzzle.position, PistolMuzzle.TransformDirection(Vector3.forward + Vector3.right / 10), out RaycastHit hitd) && hitd.collider.gameObject.CompareTag("Player") ||
            Physics.Raycast(PistolMuzzle.position, PistolMuzzle.TransformDirection(Vector3.forward + Vector3.left / 10), out RaycastHit hite) && hite.collider.gameObject.CompareTag("Player");
        return isAimingOnPlayer;
    }
}
