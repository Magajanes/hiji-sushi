using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sink : MonoBehaviour
{
    [SerializeField]
    private AudioSource source;
    [SerializeField]
    private Animator waterAnimator;

    public void OpenWater()
    {
        source.Play();

        waterAnimator.SetBool("Open", true);
    }

    public void CloseWater()
    {
        waterAnimator.SetBool("Open", false);

        source.Stop();
    }
}
