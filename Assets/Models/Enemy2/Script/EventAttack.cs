using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventAttack : MonoBehaviour
{
    public AudioClip hit_player;
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player")){
            other.gameObject.GetComponent<AudioSource>().PlayOneShot(hit_player,2f);
            other.gameObject.GetComponent<PlayerInformation>().playerTakeDamage(damage);
        }
    }
}
