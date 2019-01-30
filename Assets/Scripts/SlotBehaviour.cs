using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SlotBehaviour : MonoBehaviour
{
    public virtual void PrepareSlots(Vector3 start, Slot[] slots, float delta)
    {
        var nextPosition = start;

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = new Slot();

            slots[i].CurrentState = Slot.State.Empty;

            slots[i].SlotPosition = nextPosition;

            nextPosition += delta * Vector3.right;
        }
    }
}
