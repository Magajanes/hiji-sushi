using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe : MonoBehaviour
{
    public string DishName;

    public Dictionary<Ingredient, int> PrepareSteps;

    public Instructions PrepareInstructions;

    private void Start()
    {
        SetSteps(PrepareInstructions.IngredientsList, PrepareInstructions.MeasuresList);
    }

    private void SetSteps(List<Ingredient> ingredients, List<int> measures)
    {
        for (int i = 0; i < ingredients.Count; i++)
            PrepareSteps.Add(ingredients[i], measures[i]);
    }
}
