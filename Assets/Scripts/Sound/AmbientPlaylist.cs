using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientPlaylist : MonoBehaviour
{
    // Array untuk menyimpan audio clips (lagu)
    public AudioClip[] ambientTracks;

    // AudioSource untuk memutar musik
    private AudioSource audioSource;

    // Flag untuk memastikan pergantian lagu
    private bool isPlaying = false;

    void Start()
    {
        // Ambil AudioSource dari gameObject
        audioSource = GetComponent<AudioSource>();

        // Pastikan ada audio clips dalam playlist
        if (ambientTracks.Length > 0)
        {
            PlayRandomTrack();
        }
    }

    void Update()
    {
        // Jika tidak ada lagu yang sedang diputar dan tidak sedang dalam transisi pergantian
        if (!audioSource.isPlaying && !isPlaying)
        {
            isPlaying = true;
            PlayRandomTrack();
        }
    }

    // Memutar track secara acak
    void PlayRandomTrack()
    {
        if (ambientTracks.Length > 0)
        {
            // Ambil index acak dari 0 sampai panjang array ambientTracks
            int randomTrackIndex = Random.Range(0, ambientTracks.Length);
            
            // Set clip ke track acak yang dipilih dan mainkan
            audioSource.clip = ambientTracks[randomTrackIndex];
            audioSource.Play();
        }

        isPlaying = false;
    }
}