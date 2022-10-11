using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectNightShade : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] clipsStep;
    public AudioClip ShurikenThrow;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Step()
    {
        AudioClip clip = GetRandomClip();
        audioSource.PlayOneShot(clip);
    }

    private void ThrowShuriken()
    {        
        audioSource.PlayOneShot(ShurikenThrow);
    }

    private AudioClip GetRandomClip()
    {
        return clipsStep[UnityEngine.Random.Range(0, clipsStep.Length)];
    }
}
