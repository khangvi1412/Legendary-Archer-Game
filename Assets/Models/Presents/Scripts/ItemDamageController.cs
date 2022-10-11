using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDamageController : MonoBehaviour
{
    public float timeAbility = 30;
    public int addDamage = 50;
    PlayerInformation player;
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player") {
             Debug.Log("Item damage");
            player = other.gameObject.GetComponent<PlayerInformation>();

            gameObject.transform.position = new Vector3(0,-150,0);
            StartCoroutine(timeAbilityCount(player));
        }
    }

    private IEnumerator timeAbilityCount(PlayerInformation player) {
        player.playerDamage(addDamage);
        yield return new WaitForSeconds(timeAbility);
        player.playerDamage(-addDamage);
        Destroy(gameObject);
    }
}
