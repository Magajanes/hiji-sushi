using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Slot
{
    public enum State { Empty, Occupied };
    public State CurrentState;

    public Vector3 SlotPosition;
}
