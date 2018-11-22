using System.Collections;
using UnityEngine;

public class RecipeCheck : MonoBehaviour
{
    public bool OrderMatters = false;

    public Recipe CurrentRecipe;

    public IngredientMixer Mixer;
    public OrderQueue Orders;

    public Coroutine PrepareCoroutine;

    public delegate bool PrepareAction(IngredientMixer mixer);
    public PrepareAction Check;

    public void StartProcess()
    {
        Check = NormalCheckRecipe;

        PrepareRecipe(Mixer);
    }

    public void PrepareRecipe(IngredientMixer mixer)
    {
        if (CurrentRecipe == null)
        {
            Debug.Log("Nenhum pedido selecionado!");

            return;
        }

        if (Check(mixer))
        {
            PrepareCoroutine = StartCoroutine(PrepareDish(mixer));

            return;
        }

        Debug.Log(CurrentRecipe.DishName + " está incorreto e não foi entregue!");
    }

    public void OrderFailed(Recipe order)
    {
        if (CurrentRecipe == order && PrepareCoroutine != null)
        {
            StopCoroutine(PrepareCoroutine);

            PrepareCoroutine = null;
        }

        order.Penalize();

        Orders.EmptySlot(order);

        Debug.Log(order.DishName + " demorou demais a ser entregue!");
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
        mixer.ShrinkIngredients(CurrentRecipe.PrepareInstructions.TimeToPrepare);

        yield return new WaitForSeconds(CurrentRecipe.PrepareInstructions.TimeToPrepare);

        mixer.EmptyMixer();

        Orders.EmptySlot(CurrentRecipe);

        mixer.DeliverDish(CurrentRecipe);

        PrepareCoroutine = null;
    }
}
