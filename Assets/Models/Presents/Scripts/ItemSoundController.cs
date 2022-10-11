using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSoundController : MonoBehaviour
{
    [SerializeField]
    public AudioClip voiceSound;
    public AudioClip itemCollectSound;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }


    public void ItemCollect()
    {        
        audioSource.PlayOneShot(voiceSound, 1.5f);
        audioSource.PlayOneShot(itemCollectSound, 0.7f);
    }
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player") {
            ItemCollect();
        }
    }

}
