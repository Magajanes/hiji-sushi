﻿using System.Collections.Generic;
using UnityEngine;

public class OrderQueue : SlotBehaviour
{
    private readonly Vector3 StartPosition = new Vector3(-2f, 4.2f, 0f);

    public RecipePanel Panel;

    public Slot[] SlotsArray = new Slot[7];

    public List<Recipe> OrderList = new List<Recipe>();

    private float orderInterval;

    private List<GameObject> currentLevelOrders;

    private delegate void OrderAction();
    private OrderAction Order;

    private void Start()
    {
        GameManager.OnLevelChange += UpdateLevels;
        CameraMove.OnFirstWashFinished += StartOrders;

        UpdateLevels();
        PrepareSlots(StartPosition, SlotsArray, 2f);
        Order = WaitFirstWashToOrder;
        orderInterval = Random.Range(3f, 6f);
    }

    private void Update()
    {
        Order();
    }

    private void WaitFirstWashToOrder()
    {
        return;
    }

    private void StartOrders()
    {
        Order = OrderNormally;
    }

    private void OrderNormally()
    {
        orderInterval -= Time.deltaTime;

        if (orderInterval <= 0f)
        {
            if (OrderList.Count < 6)
                ReceiveOrder();

            float chance = Random.Range(0f, 100f);

            orderInterval = chance > 20f ? Random.Range(3f, 6f) : Random.Range(1f, 3f);
        }
    }

    private void UpdateLevels()
    {
        currentLevelOrders = GameManager.Instance.GetCurrentOrdersList();
    }

    public void ReceiveOrder()
    {
        int index = Random.Range(0, currentLevelOrders.Count);

        var order = Instantiate(currentLevelOrders[index], StartPosition, Quaternion.identity);

        var recipe = order.GetComponent<Recipe>();

        recipe.Initialize();

        OrderList.Add(recipe);

        ScrollOrders();

        ClientsManager.Instance.ReceiveClient();
    }

    private void ScrollOrders()
    {
        foreach (Recipe recipe in OrderList)
        {
            var currentPos = recipe.transform.position;

            var newPos = currentPos + 2f * Vector3.right;

            if (recipe.CurrentSlot < 6 && SlotsArray[recipe.CurrentSlot + 1].CurrentState == Slot.State.Empty)
            {
                SlotsArray[recipe.CurrentSlot].CurrentState = Slot.State.Empty;

                recipe.CurrentSlot++;

                SlotsArray[recipe.CurrentSlot].CurrentState = Slot.State.Occupied;

                iTween.MoveTo(recipe.gameObject, iTween.Hash("position", newPos,
                                                             "easetype", iTween.EaseType.easeOutExpo,
                                                             "time", 1f));
            }
        }
    }

    public void ShakeOrders()
    {
        foreach (Recipe recipe in OrderList)
        {
            recipe.Shake();

            recipe.transform.position = SlotsArray[recipe.CurrentSlot].SlotPosition;
        }
    }

    public void EmptySlot(Recipe recipe)
    {
        SlotsArray[recipe.CurrentSlot].CurrentState = Slot.State.Empty;

        OrderList.Remove(recipe);

        if(recipe.IsSelected)
            Panel.ResetInfo();

        Destroy(recipe.gameObject);
    }

    private void OnDestroy()
    {
        GameManager.OnLevelChange -= UpdateLevels;
        CameraMove.OnFirstWashFinished -= StartOrders;
    }
}
