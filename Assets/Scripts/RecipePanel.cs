using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RecipePanel : MonoBehaviour
{
    private const float SHOWING_POSITION = 2.1f;
    private const float HIDDEN_POSITION = 6.7f;

    public RecipeCheck Checker;

    public Text RecipeTitle;
    public Text[] Instructions;
    public Image[] IngredientIcons;

    private Coroutine hintCoroutine = null;

    private void Start()
    {
        Recipe.OnSelect += SetPanel;

        ResetInfo();
    }

    public void ResetInfo()
    {
        RecipeTitle.text = "RECEITA";

        foreach (Text text in Instructions)
            text.enabled = false;

        foreach (Image image in IngredientIcons)
            image.enabled = false;
    }

    public void SetPanel(object sender, EventArgs args)
    {
        ShowRecipe(Checker.CurrentRecipe);
    }

    public void ShowRecipe(Recipe recipe)
    {
        ResetInfo();

        RecipeTitle.text = recipe.DishName;

        var instructions = recipe.PrepareInstructions;

        for (int i = 0; i < instructions.IngredientsList.Count; i++)
        {
            Instructions[i].enabled = true;

            Instructions[i].text = instructions.MeasuresList[i] + "x";

            IngredientIcons[i].enabled = true;

            IngredientIcons[i].sprite = instructions.IngredientIconsList[i];

            IngredientIcons[i].preserveAspect = true;
        }
    }

    public void RecipeHint()
    {
        if (hintCoroutine == null)
            hintCoroutine = StartCoroutine(ShowHint());
    }

    public void MovePanel(float nextPosition)
    {
        iTween.MoveTo(gameObject, iTween.Hash("y", nextPosition,
                                              "easetype", iTween.EaseType.easeOutExpo,
                                              "time", 1f));
    }

    private IEnumerator ShowHint()
    {
        MovePanel(SHOWING_POSITION);

        yield return new WaitForSeconds(5f);

        MovePanel(HIDDEN_POSITION);

        hintCoroutine = null;
    }

    private void OnDestroy()
    {
        Recipe.OnSelect -= SetPanel;
    }
}