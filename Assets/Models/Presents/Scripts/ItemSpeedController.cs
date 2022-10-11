using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpeedController : MonoBehaviour
{
    public float timeAbility = 30;
    public float addSpeed = 3;
    PlayerController player;
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player") {
             Debug.Log("Item speed");
            player = other.gameObject.GetComponent<PlayerController>();
            gameObject.transform.position = new Vector3(0,-150,0);
            StartCoroutine(timeAbilityCount(player));
        }
    }

    private IEnumerator timeAbilityCount(PlayerController player) {
        player.playerSpeed += addSpeed;
        yield return new WaitForSeconds(timeAbility);
        player.playerSpeed -= addSpeed;
        Destroy(gameObject);
    }
}
