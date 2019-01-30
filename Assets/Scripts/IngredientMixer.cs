using System.Collections;
using UnityEngine;

public class IngredientMixer : SlotBehaviour
{
    public readonly Vector3 StartPosition = new Vector3(2.75f, -3.2f, 0f);

    public Slot[] SlotsArray = new Slot[5];

    public Used[] UsedIngredients = new Used[5];
    public int[] Measures = new int[5];

    public HygieneManager Manager;

    private Client _client;
    private Vector3 _clientPosition;

    [SerializeField]
    private AudioSource source;

    private void Start()
    {
        PrepareSlots(StartPosition, SlotsArray, 1.5f);
    }

    public void AddIngredient(Ingredient ingredient)
    {
        GameObject ing;
        Used used;

        for (int i = 0; i < SlotsArray.Length; i++)
        {
            if (SlotsArray[i].CurrentState == Slot.State.Empty)
            {
                ing = Instantiate(ingredient.UsedPrefab, ingredient.transform.position, Quaternion.identity);
                used = ing.GetComponent<Used>();

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
                ing = Instantiate(ingredient.UsedPrefab, ingredient.transform.position, Quaternion.identity);

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

    public void Meow()
    {
        if (GameManager.Instance.SoundFXOn)
            source.Play();
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

    public void DeliverDish(Recipe recipe, GameObject dish)
    {
        StartCoroutine(Deliver(recipe, dish));
    }

    private IEnumerator Deliver(Recipe recipe, GameObject dish)
    {
        _client = ClientsManager.Instance.RandomClient();
        _clientPosition = ClientsManager.Instance.SlotsArray[_client.CurrentSlotIndex].SlotPosition;

        iTween.MoveTo(dish, iTween.Hash("position", new Vector3(_clientPosition.x, 0.9f, 0f),
                                        "easetype", iTween.EaseType.easeOutExpo,
                                        "time", 0.5f));

        if (Manager.HygieneCheck())
        {
            _client.Eat();
            GameManager.Instance.PerfectDishes++;
            recipe.GivePoints();
        }
        else
        {
            _client.Vomit();
            GameManager.Instance.RottenDishes++;
            recipe.Penalize();
            GameManager.Instance.CheckNotification();
        }

        GameManager.Instance.CheckGameEnd();
        ClientsManager.Instance.RemoveClient(_client);

        yield return new WaitForSeconds(0.75f);

        Destroy(dish);
    }
}
