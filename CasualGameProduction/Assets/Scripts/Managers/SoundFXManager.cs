using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager instance;

    [SerializeField] private AudioSource soundFXObject;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);  
        }
    }

    // Play a specific sound clip
    public void PlaySoundFXClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        if (audioClip == null) return;  // Ensure there's a clip to play

        // Spawn the AudioSource at the given location
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);

        // Assign the clip and volume
        audioSource.clip = audioClip;
        audioSource.volume = volume;

        // Play the sound
        audioSource.Play();

        // Destroy the AudioSource object after the clip finishes playing
        Destroy(audioSource.gameObject, audioSource.clip.length);
    }

    // Play a random sound clip from an array
    public void PlayRandomSoundFXClip(AudioClip[] audioClips, Transform spawnTransform, float volume)
    {

        // Choose a random clip
        int rand = Random.Range(0, audioClips.Length);

        // Spawn the AudioSource at the given location
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);

        // Assign the random clip and volume
        audioSource.clip = audioClips[rand];
        audioSource.volume = volume;

        // Play the sound
        audioSource.Play();

        // Destroy the AudioSource object after the clip finishes playing
        Destroy(audioSource.gameObject, audioSource.clip.length);
    }
}
