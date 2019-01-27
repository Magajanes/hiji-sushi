using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sink : MonoBehaviour
{
    [SerializeField]
    private WashManager washManager;
    [SerializeField]
    private AudioSource source;
    [SerializeField]
    private Animator waterAnimator;

    public void OpenWater()
    {
        washManager.WashStarted = true;

        if (GameManager.Instance.SoundFXOn)
            source.Play();

        waterAnimator.SetBool("Open", true);
    }

    public void CloseWater()
    {
        washManager.WashStarted = false;

        waterAnimator.SetBool("Open", false);

        if (GameManager.Instance.SoundFXOn)
            source.Stop();
    }
}
