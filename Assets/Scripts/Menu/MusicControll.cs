using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicControll : MonoBehaviour
{
    [SerializeField] private AudioSource music;
    private float musicVolume = 1f;

    private void Start()
    {
        music.Play();
    }

    private void Update()
    {
        music.volume = musicVolume;
    }

    public void setVolume(float newVolume)
    {
        musicVolume = newVolume;
    }
}
