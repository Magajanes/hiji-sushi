using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MedalsPanel : MonoBehaviour
{
    [SerializeField]
    private Image[] Medals;

    public void SetMedal(int level, Sprite medal)
    {
        Medals[level - 1].sprite = medal;
        Medals[level - 1].preserveAspect = true;
        Medals[level - 1].gameObject.SetActive(true);
    }
}
