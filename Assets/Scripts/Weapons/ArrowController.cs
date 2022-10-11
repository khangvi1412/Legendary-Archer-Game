using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public float timeToDestroy =3f;
    public float timeFloatDestroy = 0.5f;


    public Vector3 target {get; set; }
    public PlayerInformation player {get; set; }
    public bool hit { get; set;} 
    Rigidbody rb;
    BoxCollider bx;
    bool disableRotation;
    public float destroyTime = 10f;
    public float speed = 20f;
    AudioSource arrowAudio;
    public GameObject explosionVFX;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        bx = GetComponent<BoxCollider>();
        //arrowAudio = GetComponent<AudioSource>();
    }
    void Update()
    {
        if(!disableRotation){
            transform.rotation = Quaternion.LookRotation(rb.velocity);
        }
        
        if(!hit){
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            if(Vector3.Distance(transform.position, target) < .01f) {
                Debug.Log("destroy");
                Debug.Log(target);
                Destroy(gameObject);
            }
        }
    }
    private void OnEnable() {
        Destroy(gameObject, timeToDestroy);
    }

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint hitPoint =  collision.GetContact(0);
        if(collision.gameObject.tag != "Player" && collision.gameObject.tag == "Enemy")
        {
//            arrowAudio.Play();
        
            int damage = player.damage;
            collision.gameObject.GetComponent<EnemyInformation>().enemyTakeDamage(damage);
            Debug.Log("hit enemy " + collision.gameObject.name);
            disableRotation = true;
            rb.isKinematic = true;
            bx.isTrigger = true;
            GameObject expl = Instantiate(explosionVFX, hitPoint.point, Quaternion.identity) as GameObject;
            Destroy(expl,timeFloatDestroy);

        }
        if (collision.gameObject.tag != "Player") { 

            Debug.Log("hit other things");
            disableRotation = true;
            rb.isKinematic = true;
            bx.isTrigger = true;
            GameObject expl = Instantiate(explosionVFX, hitPoint.point, Quaternion.identity) as GameObject;
            Destroy(expl,timeFloatDestroy);
        }
    }
}
