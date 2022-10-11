using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAttackSpeedController : MonoBehaviour
{
    public float timeAbility = 30;
    public float reloadTime = 0.25f;
    PlayerController player;
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player") {
            Debug.Log("Item attackSpeed");
            player = other.gameObject.GetComponent<PlayerController>();
            gameObject.transform.position = new Vector3(0,-150,0);
            StartCoroutine(timeAbilityCount(player));
        }
    }

    private IEnumerator timeAbilityCount(PlayerController player) {
        player.reloadTime -= reloadTime;
        yield return new WaitForSeconds(timeAbility);
        player.reloadTime += reloadTime;
        Destroy(gameObject);
    }
}
