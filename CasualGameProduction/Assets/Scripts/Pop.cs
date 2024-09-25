using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Pop : MonoBehaviour
{
    private VirusRemaining virusRemaining;

    void Start() {
        GameObject virusTextObject = GameObject.FindWithTag("virusText");

        if (virusTextObject != null) {
            virusRemaining = virusTextObject.GetComponent<VirusRemaining>();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player") {
            virusRemaining.ChangeVirus(-1);
            //gameObject.SetActive(false);
            Destroy(this.gameObject);
        }
    }
}
