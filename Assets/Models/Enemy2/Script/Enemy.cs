using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    Transform target;

    NavMeshAgent agent;
    Animator anim;
    public float distanceEnemy = 50;
    AudioSource audioSource ;

    // Start is called before the first frame update

    void Awake() {
        target =  player.transform;
    }
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        agent = this.GetComponent<NavMeshAgent>();
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
              anim.SetBool("Attack", false);
             return;
        }else if(dis > 5) {
                anim.SetBool("Attack", false);
              anim.SetBool("Run", true);
             Pursue();
             return;
         }else if(dis <= 4){
                anim.transform.LookAt(target);
                agent.SetDestination(target.position);
               anim.SetBool("Attack", true);
               return;
         }
    }
         
}
