using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewsPanel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI[] statsInfo;

    public void ShowStats()
    {
        statsInfo[0].text = GameManager.Instance.PerfectDishes.ToString();

        statsInfo[1].text = GameManager.Instance.DelayedDishes.ToString();

        statsInfo[2].text = GameManager.Instance.RottenDishes.ToString();
    }
}
