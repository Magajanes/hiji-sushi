using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe : MonoBehaviour
{
    public bool IsSelected = false;

    public GameObject Highlight;

    [HideInInspector]
    public int CurrentSlot = 0;

    public string DishName;

    public Instructions PrepareInstructions;

    public Dictionary<Ingredient, int> PrepareSteps = new Dictionary<Ingredient, int>();

    public void Initialize()
    {
        Highlight.SetActive(false);

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
    }

    public void SetHighlight()
    {
        Highlight.SetActive(IsSelected);
    }
}
