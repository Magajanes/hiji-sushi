using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WashManager : MonoBehaviour
{
    private float hygieneGauge = 0f;

    public WashStep[] FirstWashSteps = new WashStep[2];
    public WashStep[] ComplexWashSteps = new WashStep[6];

    public HygieneManager Manager;

    public Hands HandsObject;

    private delegate void WashEvaluationAction(WashStep step);
    private WashEvaluationAction WashEvaluation;

    private void Start()
    {
        WashEvaluation = EvaluateBeforeSoap;
    }

    public void EvaluateStep(WashStep step)
    {
        WashEvaluation(step);
    }

    public void WashHands(WashStep step)
    {
        var willGivePoints = HandsObject.CanWashHands(step.WashStepID);

        if (willGivePoints)
        {
            hygieneGauge += step.PointsToGive;

            if (hygieneGauge >= Manager.HygieneCounter)
                Manager.HygieneCounter = hygieneGauge;
        }
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

    private void EvaluateBeforeSoap(WashStep step)
    {
        if (!step.Done)
        {
            WashHands(step);

            step.Done = true;
        }
        else
            Debug.Log("Step already done!");

        if (StepsComplete(FirstWashSteps))
            WashEvaluation = EvaluateAfterSoap;
    }

    private void EvaluateAfterSoap(WashStep step)
    {
        if (!step.Done)
        {
            WashHands(step);

            step.Done = true;
        }
    }
}
