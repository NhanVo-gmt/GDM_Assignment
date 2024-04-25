using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private AudioSource source;

    private void Awake()
    {
        Instance = this;
        source = GetComponentInChildren<AudioSource>();
    }

    public void PlayOneShot(AudioClip clip)
    {
        source.PlayOneShot(clip);
    }
}
