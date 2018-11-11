using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe : MonoBehaviour
{
    [HideInInspector]
    public int CurrentSlot = 0;

    public string DishName;

    public Instructions PrepareInstructions;

    public Dictionary<Ingredient, int> PrepareSteps = new Dictionary<Ingredient, int>();

    public void Initialize()
    {
        SetSteps(PrepareInstructions.IngredientsList, PrepareInstructions.MeasuresList);
    }

    private void SetSteps(List<Ingredient> ingredients, List<int> measures)
    {
        for (int i = 0; i < ingredients.Count; i++)
            PrepareSteps.Add(ingredients[i], measures[i]);
    }
}
