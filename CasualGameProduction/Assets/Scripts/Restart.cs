using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    private BallsRemaining ballsRemaining;
    private VirusRemaining virusRemaining;

    void Start() {
        GameObject ballsTextObject = GameObject.FindWithTag("ballsText");
        GameObject virusTextObject = GameObject.FindWithTag("virusText");

        if (ballsTextObject != null) {
            ballsRemaining = ballsTextObject.GetComponent<BallsRemaining>();
        }

        if (virusTextObject != null) {
            virusRemaining = virusTextObject.GetComponent<VirusRemaining>();
        }
    }

    void Update()
    {
        // If game is over, start timer
        //Debug.Log(ballsRemaining.getNumBallsRemaining());

        if (virusRemaining.getNumVirusRemaining() == 0 || ballsRemaining.getNumBallsRemaining() == 0) {
            StartCoroutine(WaitTime());
        }
    }

    public IEnumerator WaitTime() {
        // Starts timer, then restarts scene
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
