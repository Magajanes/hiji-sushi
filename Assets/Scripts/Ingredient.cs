using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public string Name;

    public IngredientMixer Mixer;
    public RecipeCheck Check;

    public GameObject UsedPrefab;

    public void Select()
    {
        if (Check.PrepareCoroutine == null)
            Mixer.AddIngredient(this);
    }
}
