using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScenesController : MonoBehaviour
{
    public bool SoundFXOn = true;
    public bool MusicOn = true;
    public bool MenuOn = false;
    public bool CreditsOn = false;
    public bool NormalMode = true;
    public bool TutorialMode = false;

    public AudioSource Source;

    [SerializeField]
    private GameObject menu;
    [SerializeField]
    private GameObject credits;
    [SerializeField]
    private GameObject difficultySetup;
    [SerializeField]
    private Button[] soundToggles;
    [SerializeField]
    private Sprite[] musicToggleSprites;
    [SerializeField]
    private Sprite[] soundFXToggleSprites;

    private void Start()
    {
        Source.Play();

        DontDestroyOnLoad(gameObject);
    }

    public void StartGame()
    {
        TutorialMode = false;

        Source.Stop();

        SceneManager.LoadScene("Main");
    }

    public void StartTutorial()
    {
        TutorialMode = true;

        Source.Stop();

        SceneManager.LoadScene("Tutorial");
    }

    public void ToggleMenu()
    {
        MenuOn = !MenuOn;

        menu.SetActive(MenuOn);
    }

    public void ToggleSoundFx()
    {
        SoundFXOn = !SoundFXOn;

        soundToggles[0].image.sprite = SoundFXOn ? soundFXToggleSprites[0] : soundFXToggleSprites[1];
    }

    public void ToggleMusic()
    {
        MusicOn = !MusicOn;

        soundToggles[1].image.sprite = MusicOn ? musicToggleSprites[0] : musicToggleSprites[1];

        Source.volume = MusicOn ? 1f : 0f;
    }

    public void ToggleCredits()
    {
        if (!CreditsOn)
            ToggleMenu();

        CreditsOn = !CreditsOn;

        credits.SetActive(CreditsOn);
    }

    public void ShowDifficultySetup()
    {
        difficultySetup.SetActive(true);
    }

    public void SetDifficultyToNormal()
    {
        NormalMode = true;

        difficultySetup.SetActive(false);
    }

    public void SetDifficultyToHard()
    {
        NormalMode = false;

        difficultySetup.SetActive(false);
    }
}
