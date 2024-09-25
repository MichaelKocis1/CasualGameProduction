using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BallController : MonoBehaviour
{
    private BallsRemaining ballsRemaining;

    void Start() {
        GameObject ballsTextObject = GameObject.FindWithTag("ballsText");

        if (ballsTextObject != null) {
            ballsRemaining = ballsTextObject.GetComponent<BallsRemaining>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -5.0) {
            //gameObject.SetActive(false);
            Destroy(this.gameObject);
        }
    }
}
