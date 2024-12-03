using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restart : MonoBehaviour
{
    private BallsRemaining ballsRemaining;
    private VirusRemaining virusRemaining;

    private GameObject[] ballsOnScreen;
    private const float stuckCheckInterval = 1f; // How often to check if balls are stuck
    private const float stuckTimeThreshold = 0.7f; // Velocity threshold for determining stuck status
    private const float stationaryDurationThreshold = 1.5f; // Time a ball must be stationary to count as stuck
    private bool gameOverTriggered = false;

    [SerializeField] private GameOverManager gameOverManager;
    [SerializeField] private WinManager winManager;

    private Dictionary<GameObject, float> stationaryTimeTracker = new Dictionary<GameObject, float>();

    void Start()
    {
        GameObject ballsTextObject = GameObject.FindWithTag("ballsText");
        GameObject virusTextObject = GameObject.FindWithTag("virusText");

        if (ballsTextObject != null)
        {
            ballsRemaining = ballsTextObject.GetComponent<BallsRemaining>();
        }

        if (virusTextObject != null)
        {
            virusRemaining = virusTextObject.GetComponent<VirusRemaining>();
        }

        StartCoroutine(CheckBallsStuckRoutine());
    }

    void Update()
    {
        // Check if all Virus pegs are removed
        if (virusRemaining.getNumVirusRemaining() == 0 && !gameOverTriggered)
        {
            winManager.SetWin();
            gameOverTriggered = true;
        }

        // Check if no more balls are remaining
        if (ballsRemaining.getNumBallsRemaining() == 0 && !gameOverTriggered)
        {
            ballsOnScreen = GameObject.FindGameObjectsWithTag("Player");
            if (ballsOnScreen.Length == 0)
            {
                TriggerGameOver();
            }
        }
    }

    private IEnumerator CheckBallsStuckRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(stuckCheckInterval);

            ballsOnScreen = GameObject.FindGameObjectsWithTag("Player");
            bool ballsAreStuck = true;

            foreach (GameObject ball in ballsOnScreen)
            {
                Rigidbody rb = ball.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    // Check if the ball is moving
                    if (rb.velocity.magnitude > stuckTimeThreshold)
                    {
                        stationaryTimeTracker[ball] = 0; // Reset stationary timer if moving
                        ballsAreStuck = false;
                    }
                    else
                    {
                        // Increment stationary time
                        if (stationaryTimeTracker.ContainsKey(ball))
                        {
                            stationaryTimeTracker[ball] += stuckCheckInterval;
                        }
                        else
                        {
                            stationaryTimeTracker[ball] = stuckCheckInterval;
                        }

                        // Check if ball has been stationary for too long
                        if (stationaryTimeTracker[ball] < stationaryDurationThreshold)
                        {
                            ballsAreStuck = false;
                        }
                    }
                }
            }

            // Only trigger game over if no balls are remaining and balls are stuck
            if (ballsAreStuck 
                && ballsOnScreen.Length > 0 
                && ballsRemaining.getNumBallsRemaining() == 0 
                && !gameOverTriggered)
            {
                Debug.Log("Game Over: All balls are stuck, and no balls remaining.");
                TriggerGameOver();
                yield break;
            }
        } // <- Closing brace for the while loop and the CheckBallsStuckRoutine method
    }

    private void TriggerGameOver()
    {
        gameOverTriggered = true;
        gameOverManager.SetGameOver();
    }
}

