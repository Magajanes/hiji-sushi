using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public static Level CurrentLevel;

    public List<Level> GameLevels;

    public RecipeCheck Checker;

    public float Score;

    public TextMeshProUGUI ScorePanel;

    private void Awake()
    {
        Instance = this;

        CurrentLevel = GameLevels[0];
    }

    public static List<GameObject> GetCurrentOrdersList()
    {
        return CurrentLevel.LevelOrdersList;
    }

    public void AddPoints(float points)
    {
        Score += points;

        ScorePanel.text = Mathf.Ceil(Score).ToString();
    }

    public void RemovePoints(float points)
    {
        Score -= points;

        Score = Score <= 0f ? 0f : Score;

        ScorePanel.text = Mathf.Ceil(Score).ToString();
    }

    public void LevelUp()
    {
        
    }

    public void LevelDown()
    {

    }
}
