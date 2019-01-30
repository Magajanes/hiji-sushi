using UnityEngine;
using TMPro;

public class Used : MonoBehaviour
{
    public string Name;

    public TextMeshProUGUI AmountText;

    public GameObject MeasuresPanel;

    public void ActivatePanel()
    {
        MeasuresPanel.SetActive(true);
    }
}
