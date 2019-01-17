using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int PerfectDishes;
    public int DelayedDishes;
    public int RottenDishes;

    public float DirtRate = 1f;
    public float WaitRate = 1f;

    public Level CurrentLevel;
    public int OrdersLevel = 0;

    public List<int> LevelGoalPoints;
    public List<bool> LevelsReached;
    public List<Level> GameLevels;

    public RecipeCheck Checker;

    public float Score;
    public float MaxScore;

    public TextMeshProUGUI ScorePanel;

    public delegate void LevelUpAction();
    public static event LevelUpAction OnLevelChange;

    public delegate void LevelReachAction(int level);
    public static event LevelReachAction OnNewLevelReached;

    private void Awake()
    {
        Instance = this;

        CurrentLevel = GameLevels[OrdersLevel];

        MaxScore = 0f;

        for (int i = 0; i < LevelsReached.Count; i++)
            LevelsReached[i] = false;

        LevelsReached[0] = true;
    }

    public List<GameObject> GetCurrentOrdersList()
    {
        return CurrentLevel.LevelOrdersList;
    }

    private int TotalPrepared()
    {
        return PerfectDishes + DelayedDishes + RottenDishes;
    }

    public void AddPoints(float points)
    {
        Score += points;

        if (Score > MaxScore)
            MaxScore = Score;

        ScorePanel.text = Mathf.Ceil(Score).ToString();

        if (OrdersLevel < 7 && Score >= LevelGoalPoints[OrdersLevel])
        {
            LevelUp();

            if (OnLevelChange != null)
                OnLevelChange();
        }
    }

    public void RemovePoints(float points)
    {
        Score -= points;

        Score = Score <= 0f ? 0f : Score;

        ScorePanel.text = Mathf.Ceil(Score).ToString();

        if(OrdersLevel > 0 && Score < LevelGoalPoints[OrdersLevel - 1])
        {
            LevelDown();

            if (OnLevelChange != null)
                OnLevelChange();
        }
    }

    public void LevelUp()
    {
        OrdersLevel++;

        CurrentLevel = GameLevels[OrdersLevel];

        if (!LevelsReached[OrdersLevel])
        {
            LevelsReached[OrdersLevel] = true;

            if (OnNewLevelReached != null)
                OnNewLevelReached(OrdersLevel);
        }
    }

    public void LevelDown()
    {
        OrdersLevel--;

        CurrentLevel = GameLevels[OrdersLevel];
    }

    public void ResetStats()
    {
        PerfectDishes = 0;
        DelayedDishes = 0;
        RottenDishes = 0;
    }
}
