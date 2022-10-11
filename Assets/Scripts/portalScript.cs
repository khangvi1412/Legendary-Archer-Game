using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class portalScript : MonoBehaviour
{
    void OnTriggerEnter(Collider collider) {
        if(collider.gameObject.tag == "Player") {
            if (SceneManager.GetActiveScene().buildIndex == 4)
            {
                Cursor.lockState = CursorLockMode.None;
                SceneManager.LoadScene("Victory");
            }
            else
            {
                PlayerPrefs.SetInt("health", collider.gameObject.GetComponent<PlayerInformation>().health);
                PlayerPrefs.SetInt("damage", collider.gameObject.GetComponent<PlayerInformation>().damage);
                PlayerPrefs.SetInt("maxHealth", collider.gameObject.GetComponent<PlayerInformation>().maxHealth);
                PlayerPrefs.SetFloat("speed", collider.gameObject.GetComponent<PlayerController>().playerSpeed);
                PlayerPrefs.SetFloat("attackSpeed", collider.gameObject.GetComponent<PlayerController>().reloadTime);
                Cursor.lockState = CursorLockMode.None;
                SceneManager.LoadScene("StoreUpdate");
            }
           
        }
    }
}
