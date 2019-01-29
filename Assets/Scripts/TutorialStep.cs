using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialStep : MonoBehaviour
{
    public int StepId;
    public bool Done;
    public Collider2D StepCollider;

    private void Start()
    {
        Done = false;
    }
}
