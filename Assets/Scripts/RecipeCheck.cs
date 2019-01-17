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
            source.Play();

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

        ClientsManager.Instance.RemoveRandomClient();
    }

    public bool NormalCheckRecipe(IngredientMixer mixer)
    {
        var steps = CurrentRecipe.PrepareSteps;

        int correctSteps = 0;

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

                    source.Play();
                }
            }
            else
            {
                correctSteps = correctSteps <= 0 ? 0 : correctSteps - 1;

                source.Play();
            }
        }

        return correctSteps == steps.Count;
    }

    private IEnumerator PrepareDish(IngredientMixer mixer, Recipe recipe)
    {
        mixer.ShrinkIngredients(recipe.PrepareInstructions.TimeToPrepare);

        yield return new WaitForSeconds(recipe.PrepareInstructions.TimeToPrepare);

        mixer.EmptyMixer();

        Orders.EmptySlot(recipe);

        mixer.DeliverDish(recipe);

        PrepareCoroutine = null;
    }
}
