using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinManager : MonoBehaviour
{
    [SerializeField] GameObject winScreen;
    [SerializeField] private AudioClip[] winClips;

    public void SetWin()
    {
        winScreen.SetActive(true); // Corrected 'setActive' to 'SetActive'
        SoundFXManager.instance.PlayRandomSoundFXClip(winClips, transform, 1.0f);
    } // Ensure this closing brace is present for the method
}