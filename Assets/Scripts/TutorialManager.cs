using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    private int arrowNumber;
    private int lastStep;

    public GameObject[] arrows;

    private void Start()
    {
        arrowNumber = 0;
        lastStep = 0;

        foreach (GameObject arrow in arrows)
            arrow.SetActive(false);

        arrows[0].SetActive(true);
    }

    public void ShowNextArrow(int step)
    {
        if (lastStep == step)
        {
            arrows[arrowNumber].SetActive(false);
            arrowNumber++;
            lastStep++;
            arrows[arrowNumber].SetActive(true);
        }
    }
}
