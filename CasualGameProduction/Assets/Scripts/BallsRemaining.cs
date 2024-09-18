using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BallsRemaining : MonoBehaviour
{
    public TextMeshProUGUI ballsText;

    public int numBallsRemaining;

    // Start is called before the first frame update
    void Start()
    {
        ChangeBalls(0);
    }

    // Call this function whenever a ball is shot to update
    // the UI text for number of balls remaining
    public void ChangeBalls(int change) {
        numBallsRemaining = numBallsRemaining + change;
        ballsText.text = "Balls Remaining: " + numBallsRemaining.ToString();
    }

    // Returns value for numBallsRemaining
    public int getNumBallsRemaining() {
        return numBallsRemaining;
    }
}
