using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsOrientation : MonoBehaviour
{
    void Start()
    {
        // Lock the level to landscape orientation
        Screen.autorotateToPortrait = true;
        Screen.autorotateToPortraitUpsideDown = true;
        Screen.autorotateToLandscapeLeft = false;
        Screen.autorotateToLandscapeRight = false;
        Screen.orientation = ScreenOrientation.AutoRotation;
    }
}
