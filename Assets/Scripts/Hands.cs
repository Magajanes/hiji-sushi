using System.Collections;
using UnityEngine;

public class Hands : MonoBehaviour
{
    public float WashAnimationTime;

    public HygieneManager Manager;
    public Animator HandsAnimator;
    public AudioSource Source;

    public AudioClip[] WashClips;

    [HideInInspector]
    public Coroutine WashCoroutine;

    public delegate void FinishWashAction();
    public static event FinishWashAction OnWashComplete;

    public bool CanWashHands(int step)
    {
        if (WashCoroutine == null)
        {
            WashCoroutine = StartCoroutine(SetWashStep(step));

            return true;
        }

        return false;
    }

    public void FinishWash()
    {
        if (GameManager.Instance.SoundFXOn)
        {
            Source.clip = WashClips[1];
            Source.Play();
        }

        HandsAnimator.SetTrigger("Finish");

        if (Manager.HygieneCounter >= 100f)
        {
            if (OnWashComplete != null)
                OnWashComplete();
        }
    }

    private IEnumerator SetWashStep(int step)
    {
        if (GameManager.Instance.SoundFXOn)
        {
            Source.clip = WashClips[0];
            Source.Play();
        }

        HandsAnimator.SetInteger("StepNumber", step);
        HandsAnimator.SetBool("WashingHands", true);

        yield return new WaitForSeconds(WashAnimationTime);

        Source.Stop();
        HandsAnimator.SetBool("WashingHands", false);

        WashCoroutine = null;
    }
}
