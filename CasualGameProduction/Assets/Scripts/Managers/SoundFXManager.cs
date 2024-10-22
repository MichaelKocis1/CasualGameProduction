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
        if (audioClips == null || audioClips.Length == 0)
        {
            Debug.LogError("No audio clips provided!");
            return;
        }

        // Choose a random clip safely
        int rand = Random.Range(0, audioClips.Length);
        
        // Get the selected audio clip from the array
        AudioClip selectedClip = audioClips[rand];  // Correct assignment

        // Check if the selected clip is valid
        if (selectedClip == null)
        {
            Debug.LogError("Randomly selected audio clip is null!");
            return;
        }

        // Spawn the AudioSource at the given location
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);

        // Assign the random clip and volume
        audioSource.clip = selectedClip;  // Correct assignment
        audioSource.volume = volume;

        // Play the sound
        audioSource.Play();

        // Destroy the AudioSource object after the clip finishes playing
        Destroy(audioSource.gameObject, audioSource.clip.length);
    }
}

