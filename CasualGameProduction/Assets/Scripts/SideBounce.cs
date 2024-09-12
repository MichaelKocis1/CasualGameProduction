using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideBounce : MonoBehaviour
{
    // This function adds a small force to the player's ball
    // in the event that it lands directly on top of a peg.
    // This is done to avoid the ball getting stuck on top of the peg.

    float playerZPos;
    float pegZPos;

    public float sideZForce;
    Vector3 sideForce;

    private void OnCollisionEnter(Collision other)
    {
        // If player ball has collision with a Clean or Virus peg
        if ((other.gameObject.tag == "Clean" || other.gameObject.tag == "Virus")) 
        {
            // Calculates the current Z position of the player ball and the peg
            // Rounds it to 2 decimal places in order to avoid issues with floats
            playerZPos = Mathf.Round(transform.position.z * 10.0f) * 0.1f;
            pegZPos = Mathf.Round(other.transform.position.z * 10.0f) * 0.1f;

            if (playerZPos == pegZPos)
            {
                // Will randomly set sideZForce to be positive or negative
                if (Random.Range(0, 2) == 1)
                {
                    sideZForce *= -1;
                }
                sideForce = new Vector3(0.0f, 0.0f, sideZForce);

                // Applies the force
                GetComponent<Rigidbody>().AddForce(sideForce, ForceMode.Impulse);
            }
        }
    }
}