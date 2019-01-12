using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Client : MonoBehaviour
{
    public int CurrentSlotIndex;

    [SerializeField]
    private Animator clientAnimator;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip[] audioClips;

    public void Complain()
    {
        audioSource.clip = audioClips[0];
        audioSource.Play();
    }

    public void Eat()
    {
        audioSource.clip = audioClips[1];
        audioSource.Play();

        clientAnimator.SetTrigger("Eat");
    }

    public void Vomit()
    {
        audioSource.clip = audioClips[2];
        audioSource.Play();

        clientAnimator.SetTrigger("Vomit");
    }
}
