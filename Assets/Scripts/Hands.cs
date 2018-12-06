using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hands : MonoBehaviour
{
    public Animator HandsAnimator;

    private Coroutine washCoroutine = null;

    public bool CanWashHands(int step)
    {
        if (washCoroutine == null)
        {
            washCoroutine = StartCoroutine(SetWashStep(step));

            return true;
        }

        return false;
    }

    private IEnumerator SetWashStep(int step)
    {
        HandsAnimator.SetInteger("StepNumber", step);
        HandsAnimator.SetBool("WashingHands", true);

        yield return new WaitForSeconds(2f);

        washCoroutine = null;

        HandsAnimator.SetBool("WashingHands", false);
    }
}
