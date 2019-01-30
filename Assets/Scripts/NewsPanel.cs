using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NewsPanel : MonoBehaviour
{
    public Image MedalImage;

    [SerializeField]
    private TextMeshProUGUI[] statsInfo;

    public void ShowStats()
    {
        statsInfo[0].text = GameManager.Instance.PerfectDishes.ToString();

        statsInfo[1].text = GameManager.Instance.DelayedDishes.ToString();

        statsInfo[2].text = GameManager.Instance.RottenDishes.ToString();
    }
}
