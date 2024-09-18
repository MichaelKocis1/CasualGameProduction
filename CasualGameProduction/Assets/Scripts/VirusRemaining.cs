using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VirusRemaining : MonoBehaviour
{
    public TextMeshProUGUI virusText;

    GameObject[] virusPegs;
    public int numVirusRemaining;

    // Start is called before the first frame update
    void Start()
    {
        virusPegs = GameObject.FindGameObjectsWithTag("Virus");
        numVirusRemaining = virusPegs.Length;
        ChangeVirus(0);
    }

    // Call this function whenever a virus peg is removed to
    // update the UI text for number of virus pegs remaining
    public void ChangeVirus(int change) {
        numVirusRemaining = numVirusRemaining + change;
        virusText.text = "Virus Pegs Remaining: " + numVirusRemaining.ToString();
    }

    // Returns value for numVirusRemaining
    public int getNumVirusRemaining() {
        return numVirusRemaining;
    }
}
