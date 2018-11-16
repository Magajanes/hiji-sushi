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
        {
            if(UsedIngredients[i] != null)
                Destroy(UsedIngredients[i].gameObject);

            Measures[i] = 0;

            Slots[i].CurrentState = Slot.State.Empty;
        }
    }
}
