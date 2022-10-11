using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UpdateStore : MonoBehaviour
{
    // Start is called before the first frame update
    private int health;
    private int maxHealth;
    private int damage;

    private float speed;
    private float attackSpeed;

    public Text damageText;
    public Text speedText;
    public Text attackSpeedText;
    public Text healthText;
    public Text pointText;
    public Text notify;
    int countPoint;

    private void Start() {
        countPoint = 3;
        health = PlayerPrefs.GetInt("health");
        damage = PlayerPrefs.GetInt("damage");
        speed = PlayerPrefs.GetFloat("speed");
        maxHealth = PlayerPrefs.GetInt("maxHealth");
        attackSpeed = PlayerPrefs.GetFloat("attackSpeed");
        damageText.text = "Damge: " + damage;
        speedText.text = "Speed: " + speed;
        attackSpeedText.text = "Attack Speed: " + attackSpeed;
        healthText.text = "Max Health: " + maxHealth;
        pointText.text = "Point: " + countPoint;
        notify.text = "";

    }
    public void ButtonDone()
    {
        int indexScene = PlayerPrefs.GetInt("sceneIndex") + 1;
        if(countPoint > 0) {
            notify.text = "You must use all points before going to the next level";
            StartCoroutine(showNotify());
            return;
        }
        PlayerPrefs.SetInt("health",health);
        PlayerPrefs.SetInt("damage",damage);
        PlayerPrefs.SetInt("maxHealth",maxHealth);
        PlayerPrefs.SetFloat("speed",speed);
        PlayerPrefs.SetFloat("attackSpeed",attackSpeed);
        SceneManager.LoadScene("GamePlay_"+indexScene.ToString());
    }
    public void ButtonUpdateDamage()
    {
        if(countPoint == 0) {
            return;
        }
        countPoint-=1;
        damage += 25;
        damageText.text = "Damage: "+ damage;
        pointText.text = "Point: " + countPoint;
    }
    public void ButtonUpdateHealth()
    {
        if(countPoint == 0) {
            return;
        }
        countPoint-=1;
        health += 500;
        maxHealth += 500;
        healthText.text = "Max Health: " + maxHealth;
        pointText.text = "Point: " + countPoint;
    }
    public void ButtonUpdateSpeed()
    {
        if(countPoint == 0) {
            return;
        }
        countPoint-=1;
        speed += 1;
        speedText.text = "Speed: " + speed;
        pointText.text = "Point: " + countPoint;
    }
    public void ButtonUpdateActackSpeed()
    {
        if(countPoint == 0) {
            return;
        }
        countPoint-=1;
        attackSpeed -= 0.05f;
        attackSpeedText.text = "Attack Speed: " + attackSpeed;
        pointText.text = "Point: " + countPoint;
    }
    IEnumerator showNotify()
    {
        yield return new WaitForSeconds(1);
        notify.text = "";
    }
}
