using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHealthController : MonoBehaviour
{
    public float timeAbility = 30;
    public int addHeal = 10;
    PlayerInformation player;
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player") {
            Debug.Log("Item health");
            player = other.gameObject.GetComponent<PlayerInformation>();
            gameObject.transform.position = new Vector3(0,-150,0);
            player.playerHeal(addHeal);
        }
    }
}
