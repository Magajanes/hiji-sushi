﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Level CurrentLevel;

    public int OrdersLevel = 1;

    public List<Level> GameLevels;

    public RecipeCheck Checker;

    public float Score;

    public TextMeshProUGUI ScorePanel;

    public delegate void LevelUpAction();
    public static event LevelUpAction OnLevelChange;

    private void Awake()
    {
        Instance = this;

        CurrentLevel = GameLevels[OrdersLevel - 1];
    }

    public List<GameObject> GetCurrentOrdersList()
    {
        return CurrentLevel.LevelOrdersList;
    }

    public void AddPoints(float points)
    {
        Score += points;

        ScorePanel.text = Mathf.Ceil(Score).ToString();

        if (OrdersLevel <= 8 && Score >= OrdersLevel * 100)
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

        if (OrdersLevel > 1 && Score < (OrdersLevel - 1) * 100)
        {
            LevelDown();

            if (OnLevelChange != null)
                OnLevelChange();
        }
    }

    public void LevelUp()
    {
        OrdersLevel++;
        CurrentLevel = GameLevels[OrdersLevel - 1];
    }

    public void LevelDown()
    {
        OrdersLevel--;
        CurrentLevel = GameLevels[OrdersLevel - 1];
    }
}
