using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RecipePanel : MonoBehaviour
{
    private const float SHOWING_POSITION = 2.15f;
    private const float HIDDEN_POSITION = 6.4f;

    public Transform buttonIcon;
    public RecipeCheck Checker;

    public Text RecipeTitle;
    public Text[] Instructions;

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

            Instructions[i].text = instructions.MeasuresList[i] + "x " + instructions.IngredientsList[i];
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
                                      "time", 1f,
                                      "onstart", "FlipButtonSprite"));
    }

    private IEnumerator ShowHint()
    {
        MovePanel(SHOWING_POSITION);

        yield return new WaitForSeconds(5f);

        MovePanel(HIDDEN_POSITION);

        hintCoroutine = null;
    }

    public void FlipButtonSprite()
    {
        var scale = buttonIcon.localScale;

        scale.x = -scale.x;

        buttonIcon.localScale = scale;
    }

    private void OnDestroy()
    {
        Recipe.OnSelect -= SetPanel;
    }
}