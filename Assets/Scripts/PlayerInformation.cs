using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerInformation : MonoBehaviour
{
    // Start is called before the first frame update
    public int health;
    public int maxHealth = 1000;
    public bool isDead = false;
    public float offsetTime = 2f;
    private float timer = 0f;
    Animator animator;
    int deadParameterId;
    public int damage;
    public GameObject damageScreen;
    public GameObject dieScreen;
    public GameObject dieCanvas;
    public GameObject healthCanvas;
    public GameObject aim;
    public GameObject backgroundEnv;
    public Text countEnemyText;
    public GameObject[] countEnemy;
    public GameObject portal;
    public HealthBar healthBar;
    private bool isLoad = false;
    Cinemachine.CinemachineImpulseSource source;

    void Start()
    {
        isLoad = false;
        health = PlayerPrefs.GetInt("health",1000);
        maxHealth = PlayerPrefs.GetInt("maxHealth",1000);
        damage =  PlayerPrefs.GetInt("damage", 100);
        gameObject.GetComponent<PlayerController>().playerSpeed = PlayerPrefs.GetFloat("speed",3.0f);
        gameObject.GetComponent<PlayerController>().reloadTime = PlayerPrefs.GetFloat("attackSpeed",1.65f);
        PlayerPrefs.SetInt("sceneIndex",SceneManager.GetActiveScene().buildIndex);
        animator = GetComponent<Animator>();
        deadParameterId = Animator.StringToHash("isDead");
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHeath(health);
    }

    // Update is called once per frame
    private void FixedUpdate() {
        //count down screen damage
        timer += Time.deltaTime;
        if(timer > offsetTime)
        {
            timer = 0f;
            damageScreen.SetActive(false);
        }

        countEnemy = GameObject.FindGameObjectsWithTag("Enemy");

        countEnemyText.text = countEnemy.Length.ToString();
        if(!isLoad){
            if(countEnemy.Length == 0){
                portal.SetActive(true);
                isLoad = true;
            }
        }
    }

    public void playerTakeDamage(int damage){
        health = health - damage;
        healthBar.SetHeath(health);
        //screen damage
        if(!damageScreen.active)
        {
            source = GetComponent<Cinemachine.CinemachineImpulseSource>();
            source.GenerateImpulse();
            damageScreen.SetActive(true);

        }
        if(health <= 0 && isDead == false){
            isDead = true;
            //update animator dead
            animator.SetTrigger(deadParameterId);
            //update UI gameover
            backgroundEnv.GetComponent<AudioSource>().enabled = false;
            gameObject.tag = "Untagged";
            Invoke("Die", .5f);
        }
    }

    void Die()
    {
        PlayerPrefs.DeleteAll();
        Cursor.lockState = CursorLockMode.None;
        dieScreen.SetActive(true);
        Invoke("openDieMenu", 7f);
    }

    public void playerHeal(int healths){
        health = health + healths;
        if(health > maxHealth)
            health = maxHealth;
        healthBar.SetHeath(health);
        //update UI
    }

    public void playerDamage(int damages){
        damage = damage + damages;
        //update UI
    }

    public void openDieMenu()
    {
        healthCanvas.SetActive(false);
        aim.SetActive(false);
        dieCanvas.SetActive(true);
        dieScreen.SetActive(false);
    }
}
