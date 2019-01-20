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
        if (GameManager.Instance.SoundFXOn)
        {
            audioSource.clip = audioClips[0];
            audioSource.Play();
        }
    }

    public void Eat()
    {
        if (GameManager.Instance.SoundFXOn)
        {
            audioSource.clip = audioClips[1];
            audioSource.Play();
        }

        clientAnimator.SetTrigger("Eat");
    }

    public void Vomit()
    {
        if (GameManager.Instance.SoundFXOn)
        {
            audioSource.clip = audioClips[2];
            audioSource.Play();
        }

        clientAnimator.SetTrigger("Vomit");
    }
}
