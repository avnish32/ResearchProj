using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour
{
    [SerializeField]
    private bool juicy;
    
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip clip)
    {
        if (!juicy)
        {
            return;
        }
        audioSource.PlayOneShot(clip);
    }

    public void PlayMusic(AudioClip music)
    {
        if (!juicy)
        {
            return;
        }
        audioSource.volume = 0.7f;
        audioSource.clip = music;
        audioSource.loop = true;
        audioSource.Play();
    }

    public bool IsGameJuicy()
    {
        return juicy;
    }
}
