using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] private AudioClip[] gameOverClips;

    public void SetGameOver()
    {
        gameOverScreen.SetActive(true); // Corrected 'setActive' to 'SetActive'
        SoundFXManager.instance.PlayRandomSoundFXClip(gameOverClips, transform, 1.0f);
    } // Ensure this closing brace is present for the method
}