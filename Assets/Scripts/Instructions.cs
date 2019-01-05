using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewInstructions", menuName = "Recipe/Instructions", order = 0)]
public class Instructions : ScriptableObject
{
    public float TimeToPrepare;

    public float Points;

    public GameObject DishPrefab;

    public List<string> IngredientsList;

    public List<Sprite> IngredientIconsList;

    public List<int> MeasuresList;

    public Dictionary<string,int> SetSteps()
    {
        var steps = new Dictionary<string, int>();

        for (int i = 0; i < IngredientsList.Count; i++)
            steps.Add(IngredientsList[i], MeasuresList[i]);

        return steps;
    }
}
