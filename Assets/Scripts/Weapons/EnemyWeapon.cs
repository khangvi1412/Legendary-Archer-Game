using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyWeapon : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] clipsHit;
    public AudioClip WeaponHit;

    private AudioSource audioSource;
    private Rigidbody rigidBody;

    private bool isHit;

    public float timeLife = 2;

    public int damage;

    private bool isDame = false;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        isHit = false;

        rigidBody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (isDame == true)
            {
                return;
            }
            AudioClip clip = GetRandomClip();
            audioSource.PlayOneShot(clip);
            
            audioSource.PlayOneShot(WeaponHit);
            Debug.Log("hit player");

            isHit = true;
            isDame = true;            

            collision.gameObject.GetComponent<PlayerInformation>().playerTakeDamage(damage);


            collision.gameObject.GetComponent<PlayerInput>().enabled = true;


            rigidBody.AddForce(0, 0, 0);
        }        
    }

    private void Update()
    {
        if (isHit)
        {
            Destroy(gameObject, 1);
        }
        else
        {
            Destroy(gameObject, timeLife);
        }
    }

    private AudioClip GetRandomClip()
    {
        return clipsHit[UnityEngine.Random.Range(0, clipsHit.Length)];
    }


}
