using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class KillZone : MonoBehaviour
{
    [SerializeField] private AudioClip[] killZoneClips;
    private void OnTriggerEnter(Collider other)
    {
        // If player hits kill zone, destroy the player
        if (other.tag == "Player")
        {
            Destroy(other.gameObject);
            SoundFXManager.instance.PlayRandomSoundFXClip(killZoneClips, transform, 0.2f);
        }
    }
}