using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WashManager : MonoBehaviour
{
    public HygieneManager Manager;

    public void WashHands()
    {
        Manager.HygieneCounter = 100f;
    }
}
