﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Recipe : MonoBehaviour
{
    [HideInInspector]
    public bool IsSelected = false;
    [HideInInspector]
    public int CurrentSlot = 0;

    public string DishName;

    public float TimeToDeliver;
    private float deliveryTime;
    private float points;

    public GameObject Highlight;
    public Slider Timer;

    public Instructions PrepareInstructions;

    private Coroutine shakeCoroutine;

    public Dictionary<string, int> PrepareSteps = new Dictionary<string, int>();

    public static EventHandler OnSelect;

    public void Initialize()
    {
        SetHighlight();

        PrepareSteps = PrepareInstructions.SetSteps();

        deliveryTime = TimeToDeliver;

        Timer.maxValue = deliveryTime;
    }

    private void Update()
    {
        deliveryTime -= Time.deltaTime * GameManager.Instance.WaitRate;

        Timer.value = deliveryTime;

        if (deliveryTime <= 0f)
            GameManager.Instance.Checker.OrderFailed(this);
    }

    public void Select()
    {
        var current = GameManager.Instance.Checker.CurrentRecipe;

        if (current != null)
        {
            current.IsSelected = false;

            current.SetHighlight();
        }

        GameManager.Instance.Checker.CurrentRecipe = this;

        IsSelected = true;

        SetHighlight();

        OnSelect(this, new EventArgs());
    }

    public void SetHighlight()
    {
        Highlight.SetActive(IsSelected);
    }

    public void GivePoints()
    {
        if (deliveryTime >= TimeToDeliver / 2f)
            points = PrepareInstructions.Points * (deliveryTime / TimeToDeliver);
        else
            points = PrepareInstructions.Points / 2f;

        GameManager.Instance.AddPoints(points);
    }

    public void Penalize()
    {
        GameManager.Instance.RemovePoints(PrepareInstructions.Points / 2f);
    }

    public void Shake()
    {
        if (shakeCoroutine == null)
            shakeCoroutine = StartCoroutine(ShakeRecipe());
    }

    private IEnumerator ShakeRecipe()
    {
        float counter = 0.5f;
        Vector3 randomVector;

        var lastPosition = transform.position;

        while (counter > 0)
        {
            randomVector = UnityEngine.Random.insideUnitSphere * 0.2f;
            randomVector.z = 0f;

            transform.position = lastPosition + randomVector;
            counter -= 0.01f;

            yield return null;
        }

        transform.position = lastPosition;

        shakeCoroutine = null;
    }
}
