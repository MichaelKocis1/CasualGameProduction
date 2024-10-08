using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOneOrientation : MonoBehaviour
{
    void Start()
    {
        // Lock the level to landscape orientation
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;
        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToLandscapeRight = true;
        Screen.orientation = ScreenOrientation.AutoRotation;
    }
}
