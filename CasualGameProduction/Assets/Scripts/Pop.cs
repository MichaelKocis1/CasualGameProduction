using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pop : MonoBehaviour
{
    public static int points = 0;


    private void OnCollisionEnter3D(Collision2D other)
    {
        points += 10;
        gameObject.SetActive(false);
    }
}
