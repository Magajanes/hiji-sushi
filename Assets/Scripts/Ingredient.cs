using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public string Name;

    public IngredientMixer Mixer;
    public RecipeCheck Check;

    public GameObject UsedPrefab;

    [SerializeField]
    private AudioSource source;

    public void Select()
    {
        if (GameManager.Instance.SoundFXOn)
            source.Play();

        if (Check.PrepareCoroutine == null)
            Mixer.AddIngredient(this);
    }
}
