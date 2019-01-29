using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    private int arrowNumber;
    private int nextStep;

    public GameObject TutorialRecipePrefab;

    public GameObject[] arrows;
    public TutorialStep[] tutorialSteps;

    private void Start()
    {
        ReceiveTutorialOrder();

        arrowNumber = 0;
        nextStep = 1;

        foreach (GameObject arrow in arrows)
            arrow.SetActive(false);

        foreach (TutorialStep step in tutorialSteps)
            step.StepCollider.enabled = false;

        arrows[arrowNumber].SetActive(true);
        tutorialSteps[arrowNumber].StepCollider.enabled = true;
    }

    public void EvaluateStep(TutorialStep step)
    {
        if (step.Done)
            return;

        if (step.StepId == nextStep)
        {
            StepUp();
            nextStep++;
            return;
        }
    }

    private void StepUp()
    {
        arrows[arrowNumber].SetActive(false);
        tutorialSteps[arrowNumber].StepCollider.enabled = false;

        arrowNumber++;

        if (arrowNumber < tutorialSteps.Length)
        {
            arrows[arrowNumber].SetActive(true);
            tutorialSteps[arrowNumber].StepCollider.enabled = true;
        }
    }

    public void ReceiveTutorialOrder()
    {
        var recipe = TutorialRecipePrefab.GetComponent<Recipe>();
        recipe.Initialize();

        ClientsManager.Instance.ReceiveClient();
    }
}