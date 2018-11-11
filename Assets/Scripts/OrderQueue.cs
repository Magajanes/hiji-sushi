using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderQueue : MonoBehaviour
{
    private readonly Vector3 StartPosition = new Vector3(-5.5f, 3.4f, 0f);

    private float orderInterval = 3f;
    private List<GameObject> currentLevelOrders;

    public Slot[] Slots = new Slot[6];
    public List<Recipe> OrderList = new List<Recipe>();

    private void Start()
    {
        currentLevelOrders = GameManager.GetCurrentRecipesList();

        PrepareSlots();
    }

    private void Update()
    {
        orderInterval -= Time.deltaTime;

        if (orderInterval <= 0f)
        {
            if(OrderList.Count < 6)
                ReceiveOrder();

            orderInterval = Random.Range(1f, 5f);
        }
    }

    public void PrepareSlots()
    {
        var nextPosition = StartPosition + 2f * Vector3.right;

        for (int i = 0; i < Slots.Length; i++)
        {
            Slots[i] = new Slot();

            Slots[i].CurrentState = Slot.State.Empty;

            Slots[i].SlotPosition = nextPosition;

            Debug.Log("Slot " + i + " created at position " + nextPosition + " and it's state is " + Slots[i].CurrentState + "!");

            nextPosition += 2f * Vector3.right;
        }
    }

    public void ReceiveOrder()
    {
        int index = Random.Range(0, currentLevelOrders.Count);

        var order = Instantiate(currentLevelOrders[index], StartPosition, Quaternion.identity);

        var recipe = order.GetComponent<Recipe>();

        OrderList.Add(recipe);

        ScrollOrders(OrderList);

        recipe.Initialize();
    }

    private void ScrollOrders(List<Recipe> orders)
    {
        foreach (Recipe recipe in orders)
        {
            var currentPos = recipe.transform.position;
            var newPos = currentPos + 2f * Vector3.right;

            if (recipe.CurrentSlot < 5 && Slots[recipe.CurrentSlot + 1].CurrentState == Slot.State.Empty)
            {
                Slots[recipe.CurrentSlot].CurrentState = Slot.State.Empty;

                recipe.CurrentSlot++;

                Slots[recipe.CurrentSlot].CurrentState = Slot.State.Occupied;

                iTween.MoveTo(recipe.gameObject, iTween.Hash("position", newPos,
                                                             "easetype", iTween.EaseType.easeOutExpo,
                                                             "time", 1f));
            }
        }
    }
}
