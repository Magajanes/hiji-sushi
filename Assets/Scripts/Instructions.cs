using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewInstructions", menuName = "Recipe/Instructions", order = 0)]
public class Instructions : ScriptableObject
{
    public float TimeToPrepare;

    public List<string> IngredientsList;

    public List<int> MeasuresList;
}
