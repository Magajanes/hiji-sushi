using System;
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

    public GameObject Highlight;
    public Slider Timer;

    public Instructions PrepareInstructions;

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
        if (GameManager.Instance.Checker.PrepareCoroutine == null)
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
    }

    public void SetHighlight()
    {
        Highlight.SetActive(IsSelected);
    }

    public void GivePoints()
    {
        var points = PrepareInstructions.Points * (deliveryTime / TimeToDeliver);

        GameManager.Instance.AddPoints(points);
    }

    public void Penalize()
    {
        GameManager.Instance.RemovePoints(PrepareInstructions.Points / 2f);
    }
}
