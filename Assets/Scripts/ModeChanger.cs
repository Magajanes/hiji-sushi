using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeChanger : MonoBehaviour
{
    [SerializeField]
    private bool isDispenser = false;

    private void Awake()
    {
        CameraMove.OnModeChange += SetButtonPosition;
    }

    private void Start()
    {
        SetButtonPosition(false);
    }

    public void SetButtonPosition(bool cookMode)
    {
        if (isDispenser)
            gameObject.SetActive(!cookMode);
        else
            gameObject.SetActive(cookMode);
    }

    private void OnDestroy()
    {
        CameraMove.OnModeChange -= SetButtonPosition;
    }
}
