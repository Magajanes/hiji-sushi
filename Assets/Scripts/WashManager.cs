using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WashManager : MonoBehaviour
{
    public HygieneManager Manager;

    public Hands HandsObject;

    public void WashHands(int step)
    {
        var willGivePoints = HandsObject.CanWashHands(step);

        if (willGivePoints)
            Manager.HygieneCounter += 10f;
    }
}
