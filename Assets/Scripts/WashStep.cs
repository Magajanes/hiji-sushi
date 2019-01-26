using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WashStep : MonoBehaviour
{
    public bool IsInitial;

    public bool Done;

    public int WashStepID;

    public float PointsToGive;

    public GameObject[] DirtObjects;

    public void Clean()
    {
        foreach (GameObject dirt in DirtObjects)
        {
            if (dirt != null)
                dirt.SetActive(false);
        }
    }
}
