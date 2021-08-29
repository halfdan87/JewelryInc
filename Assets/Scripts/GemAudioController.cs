using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemAudioController : MonoBehaviour
{

    private AudioSource audioSource;

    public AudioClip gemOnGemSound;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Gem") || collision.gameObject.CompareTag("Jewelry"))
        {
            audioSource.PlayOneShot(gemOnGemSound);
        } 
    }

}
