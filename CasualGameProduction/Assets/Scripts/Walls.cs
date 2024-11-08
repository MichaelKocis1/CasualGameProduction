using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walls : MonoBehaviour
{
    [SerializeField] private AudioClip[] bounceSoundClips;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            // Ensure bounceSoundClips is not null and has elements
            if (bounceSoundClips != null && bounceSoundClips.Length > 0)
            {
                // Ensure SoundFXManager.instance is not null
                if (SoundFXManager.instance != null)
                {
                    SoundFXManager.instance.PlayRandomSoundFXClip(bounceSoundClips, transform, 0.8f);
                }
                else
                {
                    Debug.LogError("SoundFXManager instance is null!");
                }
            }
            else
            {
                Debug.LogError("bounceSoundClips is not assigned or empty!");
            }
        }
    }
}
