using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScenesController : MonoBehaviour
{
    public bool SoundFXOn = true;
    public bool MusicOn = true;
    public bool MenuOn = false;

    [SerializeField]
    private GameObject menu;
    [SerializeField]
    private Button[] soundToggles;
    [SerializeField]
    private Sprite[] musicToggleSprites;
    [SerializeField]
    private Sprite[] soundFXToggleSprites;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Main");
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
    }
}
