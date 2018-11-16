using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public string Name;

    public IngredientMixer Mixer;

    public GameObject UsedPrefab;

    public void Select()
    {
        Mixer.AddIngredient(this);
    }
}
