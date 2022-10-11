using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoblinEnemy : MonoBehaviour
{
    public GameObject player;
    public AudioClip hit_player;
    public AudioClip explosion_enemy;
    public GameObject explosion_effect;
    public int damage;
    Transform target;

    NavMeshAgent agent;
    Animator anim;
    public float distanceEnemy = 50;
    // Start is called before the first frame update
   void Awake() {
        target =  player.transform;
    }
    void Start()
    {
        anim = this.GetComponent<Animator>();
        agent = this.GetComponent<NavMeshAgent>();
        anim.SetBool("Run", false);
    }

    void Seek(Vector3 location)
    {
        agent.SetDestination(location);
    }

    void Pursue()
    {
        Vector3 targetDir = target.position - this.transform.position;

        float relativeHeading = Vector3.Angle(this.transform.forward, this.transform.TransformVector(target.forward));

        float toTarget = Vector3.Angle(this.transform.forward, this.transform.TransformVector(targetDir));

        if ((toTarget > 90 && relativeHeading < 20))
        {
            Seek(target.position);
            return;
        }

        float lookAhead = targetDir.magnitude / (agent.speed+100);
        Seek(target.position + target.transform.forward * lookAhead);
    }

    // Update is called once per frame
    void Update()
    {
         Vector3 targetDir = target.position - this.transform.position;
         float dis = targetDir.magnitude - 1;
         if(dis > distanceEnemy){
              anim.SetBool("Run", false);
         }
         else {
              anim.SetBool("Run", true);
             Pursue();
         }
    }

    private void OnCollisionEnter(Collision other) {

         if(other.gameObject.tag == "Player"){
            this.gameObject.GetComponent<AudioSource>().PlayOneShot(explosion_enemy,10f);
            other.gameObject.GetComponent<AudioSource>().PlayOneShot(hit_player,2f);
            other.gameObject.GetComponent<PlayerInformation>().playerTakeDamage(damage);
            GameObject effect = GameObject.Instantiate(explosion_effect, other.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            Destroy(effect,1);
        }
    }
}
