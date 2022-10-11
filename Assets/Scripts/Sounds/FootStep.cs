using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStep : MonoBehaviour
{
    public AudioSource playerFootAudio;
    public AudioClip footClip;

    // public void PlayFootSound()
    // {
    //     playerFootAudio.clip = footClip;
    //     playerFootAudio.Play();
    //     Debug.Log("PlaySoundStep");
    // }
    public void PlayFootSound(AnimationEvent animationEvent)
{
    if(animationEvent.animatorClipInfo.weight > 0.5) {
        AudioClip clip = footClip;
        playerFootAudio.PlayOneShot(clip);
    }
}
}