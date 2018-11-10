using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe : MonoBehaviour
{
    public string DishName;

    public Dictionary<Ingredient, int> PrepareSteps;

    public Instructions PrepareInstructions;
}
