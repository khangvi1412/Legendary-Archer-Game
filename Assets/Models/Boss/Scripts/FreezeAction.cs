using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FreezeAction : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] clipsHit;
    public AudioClip WeaponHit;

    private AudioSource audioSource;
    private Rigidbody rigidBody;

    private bool isHit;

    public float timeLife = 2;
        
    public GameObject freeze;      
    
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
            AudioClip clip = GetRandomClip();
            audioSource.PlayOneShot(clip);
            
            audioSource.PlayOneShot(WeaponHit);
            Debug.Log("hit player");

            isHit = true;

            GameObject freezeP = Instantiate(freeze, collision.transform.position + new Vector3(0, -1f, 0), Quaternion.identity) as GameObject;
            Destroy(freezeP, 3);

            collision.gameObject.GetComponent<PlayerInput>().enabled = false;

            rigidBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
        }       
    }

    private void Update()
    {
        if (isHit)
        {
            Destroy(gameObject, 3);
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
