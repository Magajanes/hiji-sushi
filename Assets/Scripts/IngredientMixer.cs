using System.Collections;
using UnityEngine;

public class IngredientMixer : SlotBehaviour
{
    public readonly Vector3 StartPosition = new Vector3(2.75f, -3.2f, 0f);

    public Slot[] SlotsArray = new Slot[5];

    public Used[] UsedIngredients = new Used[5];
    public int[] Measures = new int[5];

    public HygieneManager Manager;
    public OrderQueue OrderQueue;

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
                var ing = Instantiate(ingredient.UsedPrefab, ingredient.transform.position, Quaternion.identity);

                var used = ing.GetComponent<Used>();

                iTween.MoveTo(ing, iTween.Hash("position", SlotsArray[i].SlotPosition,
                                               "easetype", iTween.EaseType.easeInOutExpo,
                                               "time", 0.25f,
                                               "oncompletetarget", used.gameObject,
                                               "oncomplete", "ActivatePanel"));

                UsedIngredients[i] = used;

                Measures[i]++;

                used.AmountText.text = Measures[i].ToString();

                SlotsArray[i].CurrentState = Slot.State.Occupied;

                break;
            }

            if (ingredient.Name == UsedIngredients[i].Name)
            {
                var ing = Instantiate(ingredient.UsedPrefab, ingredient.transform.position, Quaternion.identity);

                iTween.MoveTo(ing, iTween.Hash("position", SlotsArray[i].SlotPosition,
                                               "easetype", iTween.EaseType.easeInOutExpo,
                                               "time", 0.25f));
                Destroy(ing, 0.25f);

                Measures[i]++;

                UsedIngredients[i].AmountText.text = Measures[i].ToString();

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
                                        "easetype", iTween.EaseType.easeOutExpo,
                                        "time", 0.5f));

        if (Manager.HygieneCheck())
        {
            Debug.Log(recipe.DishName + " perfeito!");

            recipe.GivePoints();

            GameManager.Instance.PerfectDishes++;
        }
        else
        {
            Debug.Log(recipe.DishName + " estragado!");

            recipe.Penalize();

            GameManager.Instance.RottenDishes++;
        }

        yield return new WaitForSeconds(2.5f);

        Destroy(dish);
    }
}
