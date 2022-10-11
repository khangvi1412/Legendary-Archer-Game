using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NightShade : MonoBehaviour
{
    NavMeshAgent agent;

    public GameObject playerObject;
    Transform player;
    Animator animator;

    public float detectRange = 30;
    public float chaseRange = 20;
    public float attackRange = 15;

    bool isFollow = false;

    public float speed = 6f;

    public GameObject shuriken;    
    public Transform shurikenPoint;

    float distance;

    public float transForward = 120f;
    //public float transDown = 8f;


    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();

        agent = this.GetComponent<NavMeshAgent>();
        agent.speed = speed;

        player = playerObject.transform;

        
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(animator.transform.position, player.position);
        if (distance < detectRange)
        {
            animator.SetBool("isStanding", true);
        }
        if(distance < chaseRange)
        {
            isFollow = true;
        }
        if (isFollow == true)
        {
            animator.SetBool("isChasing", true);
            animator.transform.LookAt(player);
            agent.SetDestination(player.position);
            if (distance < attackRange)
            {
                animator.SetBool("isAttacking", true);
                animator.transform.LookAt(player);
                agent.SetDestination(agent.transform.position);
            }
            else if (distance > attackRange)
            {
                animator.SetBool("isAttacking", false);
                animator.SetBool("isChasing", true);
                agent.SetDestination(player.position);
            }
        }
        
    }

    public void ThrowShuriken()
    {
        Vector3 dir = player.position - shurikenPoint.position;
        dir = dir + new Vector3(0, 2.5f, 0);
        Rigidbody rb = Instantiate(shuriken, shurikenPoint.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce(dir * distance * transForward / distance, ForceMode.Force);        
        //rb.AddRelativeTorque(new Vector3(0, 10000000000f *Time.deltaTime, 0));        
        //rb.AddForce(-transform.up * transDown, ForceMode.Impulse);
        /*
        Vector3 dir2 = player.position - shurikenPoint.position;
        dir2 = dir2 + new Vector3(5f, 0.5f, 0);
        Rigidbody rb2 = Instantiate(shuriken, shurikenPoint.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb2.AddForce(dir2 * distance * (transForward-3), ForceMode.Force);

        Vector3 dir3 = player.position - shurikenPoint.position;
        dir3 = dir3 + new Vector3(-5f, 0.5f, 0);
        Rigidbody rb3 = Instantiate(shuriken, shurikenPoint.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb3.AddForce(dir3 * distance * (transForward + 3), ForceMode.Force);
        */
    }
}
