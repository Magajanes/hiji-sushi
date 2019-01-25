using UnityEngine;

public class StatsPanel : MonoBehaviour
{
    [SerializeField]
    private MedalsPanel medalsPanel;

    [SerializeField]
    private Sprite[] medalSprites;

    [SerializeField]
    private NewsPanel[] newsPanels;

    private void Awake()
    {
        GameManager.OnNewLevelReached += ShowStatsPanel;

        gameObject.SetActive(false);
    }

    private void ShowStatsPanel(int level)
    {
        gameObject.SetActive(true);

        Time.timeScale = 0f;

        GameManager.Instance.PlayMusic(1);


        if (GameManager.Instance.GoldResults())
        {
            foreach (NewsPanel news in newsPanels)
                news.gameObject.SetActive(false);

            newsPanels[0].gameObject.SetActive(true);
            newsPanels[0].MedalImage.sprite = medalSprites[0];
            newsPanels[0].MedalImage.preserveAspect = true;
            newsPanels[0].ShowStats();

            medalsPanel.SetMedal(level, medalSprites[0]);

            return;
        }

        if (GameManager.Instance.DirtyResults())
        {
            foreach (NewsPanel news in newsPanels)
                news.gameObject.SetActive(false);

            newsPanels[1].gameObject.SetActive(true);
            newsPanels[1].MedalImage.sprite = medalSprites[1];
            newsPanels[1].MedalImage.preserveAspect = true;
            newsPanels[1].ShowStats();

            medalsPanel.SetMedal(level, medalSprites[1]);

            return;
        }

        if (GameManager.Instance.SlowResults())
        {
            foreach (NewsPanel news in newsPanels)
                news.gameObject.SetActive(false);

            newsPanels[2].gameObject.SetActive(true);
            newsPanels[2].MedalImage.sprite = medalSprites[1];
            newsPanels[2].MedalImage.preserveAspect = true;
            newsPanels[2].ShowStats();

            medalsPanel.SetMedal(level, medalSprites[1]);

            return;
        }

        if (GameManager.Instance.DirtyAndSlowResults())
        {
            foreach (NewsPanel news in newsPanels)
                news.gameObject.SetActive(false);

            newsPanels[3].gameObject.SetActive(true);
            newsPanels[3].MedalImage.sprite = medalSprites[2];
            newsPanels[3].MedalImage.preserveAspect = true;
            newsPanels[3].ShowStats();

            medalsPanel.SetMedal(level, medalSprites[2]);

            return;
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
