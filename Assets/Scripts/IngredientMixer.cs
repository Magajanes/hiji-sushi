using System.Collections;
using UnityEngine;

public class IngredientMixer : MonoBehaviour
{
    public readonly Vector3 StartPosition = new Vector3(2.75f, -3.2f, 0f);

    public Slot[] Slots = new Slot[5];

    public Used[] UsedIngredients = new Used[5];

    public int[] Measures = new int[5];

    private void Start()
    {
        PrepareSlots();
    }

    private void PrepareSlots()
    {
        var nextPosition = StartPosition;

        for (int i = 0; i < Slots.Length; i++)
        {
            Slots[i] = new Slot();

            Slots[i].CurrentState = Slot.State.Empty;

            Slots[i].SlotPosition = nextPosition;

            nextPosition += 1.5f * Vector3.right;
        }
    }

    public void AddIngredient(Ingredient ingredient)
    {
        for (int i = 0; i < Slots.Length; i++)
        {
            if (Slots[i].CurrentState == Slot.State.Empty)
            {
                var ing = Instantiate(ingredient.UsedPrefab, Slots[i].SlotPosition, Quaternion.identity);

                var used = ing.GetComponent<Used>();

                UsedIngredients[i] = used;

                Measures[i]++;

                used.AmountText.text = "x" + Measures[i];

                Slots[i].CurrentState = Slot.State.Occupied;

                break;
            }

            if (ingredient.Name == UsedIngredients[i].Name)
            {
                Measures[i]++;

                UsedIngredients[i].AmountText.text = "x" + Measures[i];

                break;
            }
        }
    }

    public void EmptyMixer()
    {
        for (int i = 0; i < Slots.Length; i++)
        {
            if(UsedIngredients[i] != null)
                Destroy(UsedIngredients[i].gameObject);

            Measures[i] = 0;

            Slots[i].CurrentState = Slot.State.Empty;
        }
    }

    public int NumberOfTypes()
    {
        int count = 0;

        for (int i = 0; i < UsedIngredients.Length; i++)
        {
            if (UsedIngredients[i] != null)
                count++;
        }

        return count;
    }

    public void ShrinkIngredients(float time)
    {
        foreach (Used used in UsedIngredients)
        {
            if(used != null)
                iTween.ScaleTo(used.gameObject, Vector3.zero, time);
        }
    }

    public void DeliverDish(Recipe recipe)
    {
        StartCoroutine(Deliver(recipe));
    }

    private IEnumerator Deliver(Recipe recipe)
    {
        var dish = Instantiate(recipe.PrepareInstructions.DishPrefab, new Vector3(5.7f, -3f, 0f), Quaternion.identity);

        dish.transform.localScale = Vector3.zero;

        iTween.ScaleTo(dish, Vector3.one, 1f);

        iTween.MoveTo(dish, iTween.Hash("position", new Vector3(8.9f, 0.9f, 0f),
                                        "easetype", iTween.EaseType.easeOutBounce,
                                        "time", 1f));

        recipe.GivePoints();

        yield return new WaitForSeconds(2.5f);

        Destroy(dish);
    }
}
