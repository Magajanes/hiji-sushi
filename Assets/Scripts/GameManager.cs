using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Level CurrentLevel;

    public List<Level> GameLevels;

    private void Awake()
    {
        CurrentLevel = GameLevels[0];
    }

    public static List<GameObject> GetCurrentRecipesList()
    {
        return CurrentLevel.LevelRecipesList;
    }
}
