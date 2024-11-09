using System.Collections;
using UnityEngine;

public class Restart : MonoBehaviour
{
    private BallsRemaining ballsRemaining;
    private VirusRemaining virusRemaining;

    private GameObject[] ballsOnScreen;
    private const float stuckCheckInterval = 1f; // How often to check if balls are stuck
    private const float stuckTimeThreshold = 0.4f; // Velocity threshold for determining stuck status
    private bool gameOverTriggered = false; // Prevents multiple game over triggers

    [SerializeField] private GameOverManager gameOverManager;
    [SerializeField] private WinManager winManager;

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

        StartCoroutine(CheckBallsStuckRoutine()); // Start the coroutine here
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

                // Check if any ball is moving above the stuck threshold
                if (rb != null && rb.velocity.magnitude > stuckTimeThreshold)
                {
                    ballsAreStuck = false;
                    break;
                }
            }

            if (ballsAreStuck && ballsOnScreen.Length > 0 && !gameOverTriggered)
            {
                TriggerGameOver();
                yield break; // Stop checking if game over is triggered
            }
        }
    }

    private void TriggerGameOver()
    {
        gameOverTriggered = true;
        gameOverManager.SetGameOver();
        // No scene reload here, game over UI will stay on screen
    }
}
