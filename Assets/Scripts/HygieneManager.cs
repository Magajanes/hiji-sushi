using UnityEngine;
using UnityEngine.UI;

public class HygieneManager : MonoBehaviour
{
    public Slider HygieneBar;

    public float HygieneCounter;

    private void Start()
    {
        HygieneBar.maxValue = 100f;

        HygieneCounter = HygieneBar.maxValue;
    }

    private void Update()
    {
        HygieneCounter -= Time.deltaTime * GameManager.Instance.DirtRate;

        HygieneBar.value = HygieneCounter;

        HygieneCounter = Mathf.Clamp(HygieneCounter, 0f, 100f);
    }

    public bool HygieneCheck()
    {
        float chance;

        if (HygieneBar.value > 30f)
            return true;

        if (HygieneBar.value > 15f)
        {
            chance = Random.Range(0, 100f);

            return chance > 30f;
        }

        if (HygieneBar.value > 0f)
        {
            chance = Random.Range(0, 100f);

            return chance > 60f;
        }

        return false;
    }
}
