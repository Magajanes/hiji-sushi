using System.Collections.Generic;
using UnityEngine;

public class IngredientMixer : MonoBehaviour
{
    public readonly Vector3 StartPosition = new Vector3(2.75f, -3.2f, 0f);

    public Slot[] Slots = new Slot[5];

    public Ingredient[] UsedIngredients = new Ingredient[5];

    public int[] Measures = new int[5];

    private void Start()
    {
        PrepareSlots();
    }

    public void AddIngredient(Ingredient ingredient)
    {
        for (int i = 0; i < Slots.Length; i++)
        {
            if (Slots[i].CurrentState == Slot.State.Empty)
            {
                var used = Instantiate(ingredient.UsedPrefab, Slots[i].SlotPosition, Quaternion.identity);

                UsedIngredients[i] = ingredient;

                Measures[i]++;

                Slots[i].CurrentState = Slot.State.Occupied;

                break;
            }

            if (ingredient.Name == UsedIngredients[i].Name)
            {
                Measures[i]++;

                break;
            }
        }
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

    public void EmptyMixer()
    {
        for (int i = 0; i < Slots.Length; i++)
            Slots[i].CurrentState = Slot.State.Empty;
    }
}
