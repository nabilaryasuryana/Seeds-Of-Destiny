using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOneShot : MonoBehaviour
{
    public AudioClip audioClip; // Ini bisa di-overwrite dari inspector jika diinginkan

    private AudioSource audioSource;

    void Start()
    {
        // Jika audioClip tidak diatur di inspector, beri nilai default dari Resources
        if (audioClip == null)
        {
            audioClip = Resources.Load<AudioClip>("Click 02_Minimal UI Sounds");
        }

        // Cari AudioSource dengan tag "Audio"
        GameObject audioObject = GameObject.FindWithTag("Audio");
        if (audioObject != null)
        {
            audioSource = audioObject.GetComponent<AudioSource>();
        }
        else
        {
            Debug.LogError("AudioSource dengan tag 'Audio' tidak ditemukan.");
        }
    }

    public void PlaySound()
    {
        if (audioSource != null && audioClip != null)
        {
            audioSource.PlayOneShot(audioClip); // Mainkan AudioClip
        }
        else
        {
            Debug.LogWarning("AudioSource atau AudioClip tidak disetel.");
        }
    }
}
