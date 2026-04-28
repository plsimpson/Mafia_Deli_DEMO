using UnityEngine;

public class PlayAudioOnStart : MonoBehaviour
{
    [Header("Audio Source Settings")]
    public AudioSource audioSource;

    [Header("Clip To Play On Start")]
    public AudioClip clipToPlay;

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        if (clipToPlay != null)
        {
            audioSource.clip = clipToPlay;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("No audio clip assigned to PlayAudioOnStart.");
        }
    }
}

