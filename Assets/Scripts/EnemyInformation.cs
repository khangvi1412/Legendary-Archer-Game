using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using System;

public class EnemyInformation : MonoBehaviour
{
    
    public int health;
    public int maxHealth = 100;
    public bool isDead = false;
    public GameObject damageText;
    public HealthBar healthBar;
    public GameObject goblinExplosion;

    NavMeshAgent agent;

    Animator animator;

    private AudioSource audioSource;

    public AudioClip deadSound;
    void Start()
    {
        health = maxHealth;
        healthBar.SetMaxHealth(health);
        agent = this.GetComponent<NavMeshAgent>();

        animator = GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void enemyTakeDamage(int damage){
        health = health - damage;
        healthBar.SetHeath(health);
        DamageIndicator indicator = Instantiate(damageText, transform.position, Quaternion.identity).GetComponent<DamageIndicator>();
        indicator.SetDamageText(damage);
        Debug.Log(health);
        if(health <= 0 && isDead == false){
            isDead = true;
            //update animator dead

            if(this.name.Contains("Goblin")) {
                GameObject effect = GameObject.Instantiate(goblinExplosion, this.transform.position, Quaternion.identity);
                Destroy(this.gameObject);
                Destroy(effect, 1);
                audioSource.PlayOneShot(deadSound);
            }
            else{
                animator.SetTrigger("isDead");
                audioSource.PlayOneShot(deadSound);
                agent.speed = 0;
                agent.SetDestination(agent.transform.position);
                Destroy(gameObject, 3);
            }
            
        }
    }
}
