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
            // Play a rand sound when ball collides with wall
            if (bounceSoundClips.Length > 0)
            {
                SoundFXManager.instance.PlayRandomSoundFXClip(bounceSoundClips, transform, 1f);
            }
        }
    }
}
