using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hands : MonoBehaviour
{
    public float WashAnimationTime;

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

    public void FinishWash()
    {
        HandsAnimator.SetTrigger("Finish");
    }

    private IEnumerator SetWashStep(int step)
    {
        HandsAnimator.SetInteger("StepNumber", step);
        HandsAnimator.SetBool("WashingHands", true);

        yield return new WaitForSeconds(WashAnimationTime);

        washCoroutine = null;

        HandsAnimator.SetBool("WashingHands", false);
    }
}
