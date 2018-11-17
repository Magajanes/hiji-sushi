using System.Collections;
using UnityEngine;

public class RecipeCheck : MonoBehaviour
{
    public bool OrderMatters = false;

    public Recipe CurrentRecipe;

    public IngredientMixer Mixer;
    public OrderQueue Orders;

    public Coroutine PrepareCoroutine;

    public delegate void PrepareAction(IngredientMixer mixer);
    public PrepareAction Prepare;

    public void StartProcess()
    {
        Prepare = PrepareRecipe;

        Prepare(Mixer);
    }

    public void PrepareRecipe(IngredientMixer mixer)
    {
        if (CurrentRecipe == null)
        {
            Debug.Log("Nenhum pedido selecionado!");

            return;
        }

        if (CheckRecipe(mixer))
        {
            PrepareCoroutine = StartCoroutine(PrepareDish(mixer));

            return;
        }

        Debug.Log(CurrentRecipe.DishName + " não foi entregue!");
    }

    public void OrderFailed(Recipe order)
    {
        if (PrepareCoroutine != null)
            StopCoroutine(PrepareCoroutine);

        Orders.EmptySlot(order);

        Debug.Log(order.DishName + " demorou demais a ser entregue!");
    }

    public bool CheckRecipe(IngredientMixer mixer)
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

                    Debug.Log("Medida incorreta!");
                }
            }
            else
            {
                correctSteps = correctSteps <= 0 ? 0 : correctSteps - 1;

                Debug.Log("Ingrediente incorreto!");
            }
        }

        return correctSteps == steps.Count;
    }

    private IEnumerator PrepareDish(IngredientMixer mixer)
    {
        Debug.Log("Dish is correct!");

        yield return null;
    }
}
