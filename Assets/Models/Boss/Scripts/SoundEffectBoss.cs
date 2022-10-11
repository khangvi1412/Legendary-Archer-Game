using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectBoss : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] clipsStep;

    private AudioSource audioSource;

    public float volumeStep = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StepBoss()
    {
        AudioClip clip = GetRandomClip();
        audioSource.PlayOneShot(clip, volumeStep);
    }

    private AudioClip GetRandomClip()
    {
        return clipsStep[UnityEngine.Random.Range(0, clipsStep.Length)];
    }
}
