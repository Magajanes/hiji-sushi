using UnityEngine;

public class IngredientMixer : MonoBehaviour
{
    public readonly Vector3 StartPosition = new Vector3(2.75f, -3.2f, 0f);

    public Slot[] Slots = new Slot[5];

    private void Start()
    {
        PrepareSlots();
    }

    public void AddIngredient(Ingredient ingredient)
    {
        for (int i = 0; i < Slots.Length; i++)
        {
            if(Slots[i].CurrentState == Slot.State.Empty)
            {
                var used = Instantiate(ingredient.UsedPrefab, Slots[i].SlotPosition, Quaternion.identity);

                Slots[i].CurrentState = Slot.State.Occupied;

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

            nextPosition += 1.25f * Vector3.right;
        }
    }

    public void EmptyMixer()
    {
        for (int i = 0; i < Slots.Length; i++)
            Slots[i].CurrentState = Slot.State.Empty;
    }
}
