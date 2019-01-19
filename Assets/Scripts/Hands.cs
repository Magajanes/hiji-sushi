using System.Collections;
using UnityEngine;

public class Hands : MonoBehaviour
{
    public float WashAnimationTime;

    public Animator HandsAnimator;
    public AudioSource Source;

    public AudioClip[] WashClips;

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
        Source.clip = WashClips[1];
        Source.Play();

        HandsAnimator.SetTrigger("Finish");
    }

    private IEnumerator SetWashStep(int step)
    {
        Source.clip = WashClips[0];
        Source.Play();

        HandsAnimator.SetInteger("StepNumber", step);
        HandsAnimator.SetBool("WashingHands", true);

        yield return new WaitForSeconds(WashAnimationTime);

        Source.Stop();

        washCoroutine = null;

        HandsAnimator.SetBool("WashingHands", false);
    }
}
