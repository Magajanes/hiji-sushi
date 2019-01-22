using UnityEngine;

public class StatsPanel : MonoBehaviour
{
    [SerializeField]
    private NewsPanel[] newsPanels;

    private bool goodResults
    {
        get
        {
            return GameManager.Instance.RottenDishes < 1 && GameManager.Instance.DelayedDishes < 1;
        }
    }

    private void Awake()
    {
        GameManager.OnNewLevelReached += ShowStatsPanel;

        gameObject.SetActive(false);
    }

    private void ShowStatsPanel(int level)
    {
        gameObject.SetActive(true);

        Time.timeScale = 0f;

        if (!goodResults)
        {
            newsPanels[0].gameObject.SetActive(true);
            newsPanels[1].gameObject.SetActive(false);

            newsPanels[0].ShowStats();

            GameManager.Instance.PlayMusic(1);
        }
        else
        {
            newsPanels[0].gameObject.SetActive(false);
            newsPanels[1].gameObject.SetActive(true);

            newsPanels[1].ShowStats();

            GameManager.Instance.PlayMusic(1);
        }
    }

    public void HideStatsPanel()
    {
        GameManager.Instance.ResetStats();

        Time.timeScale = 1;

        gameObject.SetActive(false);

        GameManager.Instance.PlayMusic(0);
    }

    private void OnDestroy()
    {
        GameManager.OnNewLevelReached -= ShowStatsPanel;
    }
}
