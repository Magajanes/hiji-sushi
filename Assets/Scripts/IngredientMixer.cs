﻿using System.Collections;
using UnityEngine;

public class IngredientMixer : SlotBehaviour
{
    public readonly Vector3 StartPosition = new Vector3(2.75f, -3.2f, 0f);

    public Slot[] SlotsArray = new Slot[5];

    public Used[] UsedIngredients = new Used[5];
    public int[] Measures = new int[5];

    public HygieneManager Manager;

    private void Start()
    {
        PrepareSlots(StartPosition, SlotsArray, 1.5f);
    }

    public void AddIngredient(Ingredient ingredient)
    {
        for (int i = 0; i < SlotsArray.Length; i++)
        {
            if (SlotsArray[i].CurrentState == Slot.State.Empty)
            {
                var ing = Instantiate(ingredient.UsedPrefab, SlotsArray[i].SlotPosition, Quaternion.identity);

                var used = ing.GetComponent<Used>();

                UsedIngredients[i] = used;

                Measures[i]++;

                used.AmountText.text = "x" + Measures[i];

                SlotsArray[i].CurrentState = Slot.State.Occupied;

                break;
            }

            if (ingredient.Name == UsedIngredients[i].Name)
            {
                Measures[i]++;

                UsedIngredients[i].AmountText.text = "x" + Measures[i];

                break;
            }
        }
    }

    public void EmptyMixer()
    {
        for (int i = 0; i < SlotsArray.Length; i++)
        {
            if(UsedIngredients[i] != null)
                Destroy(UsedIngredients[i].gameObject);

            Measures[i] = 0;

            SlotsArray[i].CurrentState = Slot.State.Empty;
        }
    }

    public int NumberOfTypes()
    {
        int count = 0;

        for (int i = 0; i < UsedIngredients.Length; i++)
        {
            if (UsedIngredients[i] != null)
                count++;
        }

        return count;
    }

    public void ShrinkIngredients(float time)
    {
        foreach (Used used in UsedIngredients)
        {
            if(used != null)
                iTween.ScaleTo(used.gameObject, Vector3.zero, time);
        }
    }

    public void DeliverDish(Recipe recipe)
    {
        StartCoroutine(Deliver(recipe));
    }

    private IEnumerator Deliver(Recipe recipe)
    {
        var dish = Instantiate(recipe.PrepareInstructions.DishPrefab, new Vector3(5.7f, -3f, 0f), Quaternion.identity);

        dish.transform.localScale = Vector3.zero;

        iTween.ScaleTo(dish, Vector3.one, 1f);

        iTween.MoveTo(dish, iTween.Hash("position", new Vector3(8.9f, 0.9f, 0f),
                                        "easetype", iTween.EaseType.easeOutBounce,
                                        "time", 1f));

        if (Manager.HygieneCheck())
        {
            Debug.Log(recipe.DishName + " perfeito!");

            recipe.GivePoints();
        }
        else
        {
            Debug.Log(recipe.DishName + " estragado!");

            recipe.Penalize();
        }

        yield return new WaitForSeconds(2.5f);

        Destroy(dish);
    }
}
