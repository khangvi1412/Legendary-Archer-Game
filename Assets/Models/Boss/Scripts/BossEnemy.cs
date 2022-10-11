using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossEnemy : MonoBehaviour
{
    NavMeshAgent agent;

    public GameObject playerObject;

    Transform player;
    Animator animator;

    private AudioSource audioSource;

    float distance;
    float chaseRange = 10000;

    float timer; 
    float minorTime;

    bool isFollow = false;

    public float speed = 6f;

    public GameObject skill1Weapon;
    public Transform skill1Point;
    public GameObject skill1Target;
    public AudioClip Skill1Activate;

    public GameObject skill2Weapon;
    public Transform skill2Point1;
    public Transform skill2Point2;
    public AudioClip Skill2Activate;

    public GameObject skill3Weapon;
    public Transform skill3Point;
    public AudioClip Skill3Activate;
    public AudioClip Skill3Voice;

    public GameObject skill4Weapon1;
    public GameObject skill4Weapon2;
    public GameObject skill4Weapon3;
    public AudioClip Skill4Activate1;
    public AudioClip Skill4Activate2;
    public AudioClip Skill4Activate3;
    public AudioClip Skill4Voice;

    public GameObject skill5Weapon;
    public Transform skill5Point;
    public AudioClip Skill5Activate;
    public AudioClip Skill5Voice;
    // Start is called before the first frame update
    void Awake()
    {
        timer = 0;
        minorTime = 0;

        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();        

        agent = this.GetComponent<NavMeshAgent>();
        agent.speed = speed;

        player = playerObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
        timer += Time.deltaTime;        
        
        distance = Vector3.Distance(animator.transform.position, player.position);

        if (timer > 9)
        {
            if (distance < chaseRange)
            {
                isFollow = true;
            }
        }

        if (isFollow == true)
        {
            if(timer < 10 && timer > minorTime)
            {
                animator.SetBool("isChasing", true);
                animator.transform.LookAt(player);
                agent.SetDestination(player.position);
            }            
            else if (timer > 10)
            {
                int skillChoose = UnityEngine.Random.Range(0, 5);
                //int skillChoose = 3;
                if (skillChoose == 0)
                {
                    animator.SetBool("skill1Activate", true);
                    animator.SetBool("isChasing", false);
                    animator.transform.LookAt(player);
                    agent.SetDestination(agent.transform.position);
                    timer = 0;
                    minorTime = 4;
                    StartCoroutine(checkEndState(minorTime,"Skill1", () =>
                    {
                        Debug.Log("Skill 1 end");                        
                        animator.SetBool("skill1Activate", false);
                        animator.SetBool("isChasing", true);    
                        
                    }));
                }
                else if (skillChoose == 1)
                {
                    animator.SetBool("skill2Activate", true);
                    animator.SetBool("isChasing", false);
                    animator.transform.LookAt(player);
                    agent.SetDestination(agent.transform.position);
                    timer = 0;
                    minorTime = 4;
                    StartCoroutine(checkEndState(minorTime, "Skill2", () =>
                    {
                        Debug.Log("Skill 2 end");                        
                        animator.SetBool("skill2Activate", false);
                        animator.SetBool("isChasing", true);                        
                    }));
                }
                else if (skillChoose == 2)
                {
                    animator.SetBool("skill3Activate", true);
                    animator.SetBool("isChasing", false);
                    animator.transform.LookAt(player);
                    agent.SetDestination(agent.transform.position);
                    timer = 0;
                    minorTime = 2;
                    StartCoroutine(checkEndState(minorTime, "Skill3", () =>
                    {
                        Debug.Log("Skill 3 end");
                        animator.SetBool("skill3Activate", false);
                        animator.SetBool("isChasing", true);
                    }));
                }
                else if (skillChoose == 3)
                {
                    animator.SetBool("skill4Activate", true);
                    animator.SetBool("isChasing", false);
                    animator.transform.LookAt(player);
                    agent.SetDestination(agent.transform.position);
                    timer = 0;
                    minorTime = 9;
                    StartCoroutine(checkEndState(minorTime, "Skill4", () =>
                    {
                        Debug.Log("Skill 4 end");
                        animator.SetBool("skill4Activate", false);
                        animator.SetBool("isChasing", true);
                    }));
                }
                else if (skillChoose == 4)
                {
                    animator.SetBool("skill5Activate", true);
                    animator.SetBool("isChasing", false);
                    animator.transform.LookAt(player);
                    agent.SetDestination(agent.transform.position);
                    timer = 0;
                    minorTime = 3;
                    StartCoroutine(checkEndState(minorTime, "Skill5", () =>
                    {
                        Debug.Log("Skill 5 end");
                        animator.SetBool("skill5Activate", false);
                        animator.SetBool("isChasing", true);
                    }));
                }
            }
        }
    }

    private IEnumerator checkEndState(float time, string nameState, Action Oncomplete)
    {
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName(nameState))
            yield return null;
        if (Oncomplete != null)
        {
            yield return new WaitForSeconds(time);
            Oncomplete();
        }
            
    }    

    public void ActivateSkill1()
    {
        Vector3 dir = player.position - skill1Point.position;
        dir = dir + new Vector3(0, 2.4f, 0);

        Rigidbody rb1 = Instantiate(skill1Target, player.position + new Vector3(0, 0.2f, 0), Quaternion.identity).GetComponent<Rigidbody>();

        Rigidbody rb = Instantiate(skill1Weapon, skill1Point.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce(dir * distance * 120f/distance, ForceMode.Force);
    }

    public void ActivateSkill1Sound()
    {
        audioSource.PlayOneShot(Skill1Activate);
    }

    public void ActivateSkill2()
    {
        preSkill2Point1();
        preSkill2Point2();
    }

    private void preSkill2Point1()
    {
        Vector3 dir = player.position - skill1Point.position;
        dir = dir + new Vector3(0, -0.5f, 0);
        Rigidbody rb = Instantiate(skill2Weapon, skill2Point1.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce(dir * distance * 120f / distance, ForceMode.Force);
    }

    private void preSkill2Point2()
    {
        Vector3 dir = player.position - skill1Point.position;
        dir = dir + new Vector3(0, -0.5f, 0);
        Rigidbody rb = Instantiate(skill2Weapon, skill2Point2.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce(dir * distance * 120f / distance, ForceMode.Force);        
    }

    public void ActivateSkill2Sound()
    {
        audioSource.PlayOneShot(Skill2Activate);
    }


    public void ActivateSkill3()
    {
        Vector3 dir = player.position - skill3Point.position;
        dir = dir + new Vector3(0, 3.5f, 0);
        Rigidbody rb = Instantiate(skill3Weapon, skill3Point.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce(dir * distance * 100f / distance, ForceMode.Force);

        audioSource.PlayOneShot(Skill3Activate);
        audioSource.PlayOneShot(Skill3Voice);
    }    

    public void ActivateSkill4()
    {
        
        Vector3 dir = player.position - skill1Point.position;    

        dir = dir + new Vector3(0, 2.5f, 0);
        GameObject weapon;
        AudioClip audio;
        GetRandomSkill4Weapon(out weapon, out audio);
        Rigidbody rb = Instantiate(weapon, skill1Point.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce(dir * distance * 160f / distance, ForceMode.Force);

        audioSource.PlayOneShot(audio);
    }

    public void ActivateSkill4Sound()
    {
        audioSource.PlayOneShot(Skill4Voice);
    }

    private void GetRandomSkill4Weapon(out GameObject weapon, out AudioClip audio)
    {
        int weaponChoose = UnityEngine.Random.Range(0, 3);
        //int weaponChoose = 0;
        if (weaponChoose == 0)
        {
            weapon = skill4Weapon1;
            audio = Skill4Activate1;
        }
        else if(weaponChoose == 1)
        {
            weapon = skill4Weapon2;
            audio = Skill4Activate2;
        }
        else
        {
            weapon = skill4Weapon3;
            audio = Skill4Activate3;
        }
    }

    public void ActivateSkill5()
    {
        Vector3 dir = player.position - skill3Point.position;
        dir = dir + new Vector3(0, 2.5f, 0);
        Rigidbody rb = Instantiate(skill5Weapon, skill5Point.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce(dir * distance * 100f / distance, ForceMode.Force);
    }

    public void ActivateSkill5Sound()
    {
        audioSource.PlayOneShot(Skill5Activate);
    }
    public void ActivateSkill5Voice()
    {
        audioSource.PlayOneShot(Skill5Voice);
    }
}
