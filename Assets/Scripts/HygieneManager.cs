using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HygieneManager : MonoBehaviour
{
    public Slider HygieneBar;

    private float hygieneCounter;

    [SerializeField]
    private float DirtRate = 1f;

    private void Start()
    {
        HygieneBar.maxValue = 100f;

        hygieneCounter = HygieneBar.maxValue;
    }

    private void Update()
    {
        hygieneCounter -= Time.deltaTime * DirtRate;

        HygieneBar.value = hygieneCounter;

        hygieneCounter = Mathf.Clamp(hygieneCounter, 0f, 100f);
    }
}
