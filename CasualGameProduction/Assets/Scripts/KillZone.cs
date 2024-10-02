using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("HELLOO");
        if (other.tag == "Player")
        {
            Destroy(other.gameObject);
        }
    }
}