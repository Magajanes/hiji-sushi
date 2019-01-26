using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WashManager : MonoBehaviour
{
    public readonly Vector3 WashHandsPosition = new Vector3(-6f, 0f, 0f);
    public readonly Vector3 HideHandsPosition = new Vector3(-6f, -5f - 0f);

    private float hygieneGauge = 0f;

    public float HygieneGauge
    {
        get
        {
            return hygieneGauge;
        }
    }

    public WashStep[] FirstWashSteps = new WashStep[2];
    public WashStep[] ComplexWashSteps = new WashStep[6];

    public HygieneManager Manager;
    public Hands HandsObject;
    public SpriteRenderer WaterRenderer;

    private delegate void WashEvaluationAction(WashStep step);
    private WashEvaluationAction WashEvaluation;

    private delegate void WashButtonAction();
    private WashButtonAction HandsSetup;

    private void Start()
    {
        WashEvaluation = EvaluateBeforeSoap;

        HandsSetup = PrepareHandsToWash;

        HandsSetup();
    }

    public void EvaluateStep(WashStep step)
    {
        WashEvaluation(step);
    }

    public void WashSetup()
    {
        HandsSetup();
    }

    private void EvaluateHygienePoints(WashStep step)
    {
        hygieneGauge += step.PointsToGive;

        if (hygieneGauge >= Manager.HygieneCounter)
            Manager.HygieneCounter = hygieneGauge;
    }

    private bool StepsComplete(WashStep[] steps)
    {
        int count = 0;

        foreach (WashStep step in steps)
        {
            if (step.Done)
                count++;
        }

        return count == steps.Length;
    }

    private void ResetSteps()
    {
        hygieneGauge = 0f;

        WashEvaluation = EvaluateBeforeSoap;

        foreach (WashStep step in FirstWashSteps)
            step.Done = false;

        foreach (WashStep step in ComplexWashSteps)
            step.Done = false;
    }

    private void PrepareHandsToWash()
    {
        iTween.MoveTo(HandsObject.gameObject, iTween.Hash("position", WashHandsPosition,
                                                          "easetype", iTween.EaseType.easeInOutExpo,
                                                          "time", 1f));

        ResetSteps();

        HandsSetup = PrepareHandsToCook;
    }

    private void PrepareHandsToCook()
    {
        iTween.MoveTo(HandsObject.gameObject, iTween.Hash("position", HideHandsPosition,
                                                          "easetype", iTween.EaseType.easeOutExpo,
                                                          "time", 1f));

        HandsSetup = PrepareHandsToWash;
    }

    public bool WashStarted()
    {
        return hygieneGauge > 0f;
    }

    private void EvaluateBeforeSoap(WashStep step)
    {
        WaterRenderer.sortingOrder = 7;

        if (HandsObject.CanWashHands(step.WashStepID))
        {
            if (!step.Done && step.IsInitial)
            {
                EvaluateHygienePoints(step);
                step.Clean();
                step.Done = true;
            }
            else if (step.Done)
                Debug.Log("Step already done!");
            else if (!step.IsInitial)
                Debug.Log("This is not an initial step!");

            if (StepsComplete(FirstWashSteps))
                WashEvaluation = EvaluateAfterSoap;
        }
    }

    private void EvaluateAfterSoap(WashStep step)
    {
        WaterRenderer.sortingOrder = 2;

        if (HandsObject.CanWashHands(step.WashStepID))
        {
            if (!step.Done && !step.IsInitial)
            {
                EvaluateHygienePoints(step);
                step.Clean();
                step.Done = true;
            }
            else if (step.Done)
                Debug.Log("Step already done!");
            else if (step.IsInitial)
                Debug.Log("This is not an initial step!");
        }
    }
}
