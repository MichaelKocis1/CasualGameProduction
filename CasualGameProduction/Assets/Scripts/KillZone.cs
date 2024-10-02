using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // If player hits kill zone, destroy the player
        if (other.tag == "Player")
        {
            Destroy(other.gameObject);
        }
    }
}