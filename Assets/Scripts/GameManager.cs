using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Level CurrentLevel;

    public static RecipeCheck Checker;

    public List<Level> GameLevels;

    public RecipeCheck RecipeChecker;

    private void Awake()
    {
        CurrentLevel = GameLevels[0];

        Checker = RecipeChecker;
    }

    public static List<GameObject> GetCurrentRecipesList()
    {
        return CurrentLevel.LevelRecipesList;
    }
}
