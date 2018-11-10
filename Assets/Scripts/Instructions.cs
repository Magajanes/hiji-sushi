using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewInstructions", menuName = "Recipe/Instructions", order = 0)]
public class Instructions : ScriptableObject
{
    public List<Ingredient> IngredientsList;

    public List<int> MeasuresList;
}
