using System.Collections;
using UnityEngine;

public class RecipeCheck : MonoBehaviour
{
    public bool OrderMatters = false;

    public Recipe CurrentRecipe;

    public IngredientMixer Mixer;
    public OrderQueue Orders;

    public Coroutine PrepareCoroutine;

    [SerializeField]
    private RecipePanel recipePanel;
    [SerializeField]
    private GameObject preparingHands;
    [SerializeField]
    private AudioSource source;

    public delegate bool PrepareAction(IngredientMixer mixer);
    public PrepareAction Check;

    public void StartProcess()
    {
        Check = NormalCheckRecipe;
        PrepareRecipe(Mixer, CurrentRecipe);
    }

    public void PrepareRecipe(IngredientMixer mixer, Recipe recipe)
    {
        if (PrepareCoroutine != null)
            return;

        if (recipe == null)
        {
            if (GameManager.Instance.SoundFXOn)
                source.Play();

            Orders.ShakeOrders();

            return;
        }

        if (Check(mixer))
        {
            PrepareCoroutine = StartCoroutine(PrepareDish(mixer, recipe));

            return;
        }
    }

    public void OrderFailed(Recipe order)
    {
        if (CurrentRecipe == order && PrepareCoroutine != null)
        {
            StopCoroutine(PrepareCoroutine);
            PrepareCoroutine = null;

            Mixer.EmptyMixer();
        }

        order.Penalize();
        Orders.EmptySlot(order);

        GameManager.Instance.DelayedDishes++;
        GameManager.Instance.CheckGameEnd();
        ClientsManager.Instance.RemoveRandomClient();

        preparingHands.SetActive(false);
    }

    public bool NormalCheckRecipe(IngredientMixer mixer)
    {
        var steps = CurrentRecipe.PrepareSteps;
        int correctSteps = 0;

        if(mixer.NumberOfTypes() != steps.Count)
        {
            if (GameManager.Instance.SoundFXOn)
                source.Play();

            CurrentRecipe.Shake();

            return false;
        }

        for (int i = 0; i < mixer.NumberOfTypes(); i++)
        {
            if (steps.ContainsKey(mixer.UsedIngredients[i].Name))
            {
                var measure = mixer.Measures[i];

                if (measure == steps[mixer.UsedIngredients[i].Name])
                    correctSteps++;
                else
                {
                    correctSteps = correctSteps <= 0 ? 0 : correctSteps - 1;
                    CurrentRecipe.Shake();

                    if (GameManager.Instance.SoundFXOn)
                        source.Play();
                }
            }
            else
            {
                correctSteps = correctSteps <= 0 ? 0 : correctSteps - 1;
                CurrentRecipe.Shake();

                if (GameManager.Instance.SoundFXOn)
                    source.Play();
            }
        }

        return correctSteps == steps.Count;
    }

    private IEnumerator PrepareDish(IngredientMixer mixer, Recipe recipe)
    {
        preparingHands.SetActive(true);
        recipePanel.HidePanel();
        mixer.ShrinkIngredients(recipe.PrepareInstructions.TimeToPrepare);

        yield return new WaitForSeconds(recipe.PrepareInstructions.TimeToPrepare);

        mixer.EmptyMixer();
        Orders.EmptySlot(recipe);
        mixer.DeliverDish(recipe);
        preparingHands.SetActive(false);

        PrepareCoroutine = null;
    }
}
