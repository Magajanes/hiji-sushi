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
    public float TimeToPrepare;

    public GameObject Highlight;
    public Slider Timer;

    public Instructions PrepareInstructions;

    public Dictionary<Ingredient, int> PrepareSteps = new Dictionary<Ingredient, int>();

    public static EventHandler OnSelect;

    public void Initialize()
    {
        SetHighlight();

        SetSteps(PrepareInstructions.IngredientsList, PrepareInstructions.MeasuresList);
    }

    private void SetSteps(List<Ingredient> ingredients, List<int> measures)
    {
        for (int i = 0; i < ingredients.Count; i++)
            PrepareSteps.Add(ingredients[i], measures[i]);
    }

    public void Select()
    {
        var current = GameManager.Checker.CurrentRecipe;

        if (current != null)
        {
            current.IsSelected = false;

            current.SetHighlight();
        }

        GameManager.Checker.CurrentRecipe = this;

        IsSelected = true;

        SetHighlight();

        OnSelect(this, new EventArgs());
    }

    public void SetHighlight()
    {
        Highlight.SetActive(IsSelected);
    }
}
