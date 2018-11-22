using System.Collections.Generic;
using UnityEngine;

public class OrderQueue : SlotBehaviour
{
    private readonly Vector3 StartPosition = new Vector3(-2f, 3.4f, 0f);

    public RecipePanel Panel;

    public Slot[] SlotsArray = new Slot[7];

    public List<Recipe> OrderList = new List<Recipe>();

    private float orderInterval = 3f;

    private List<GameObject> currentLevelOrders;

    private void Start()
    {
        currentLevelOrders = GameManager.GetCurrentOrdersList();

        PrepareSlots(StartPosition, SlotsArray, 2f);
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

    public void ReceiveOrder()
    {
        int index = Random.Range(0, currentLevelOrders.Count);

        var order = Instantiate(currentLevelOrders[index], StartPosition, Quaternion.identity);

        var recipe = order.GetComponent<Recipe>();

        recipe.Initialize();

        OrderList.Add(recipe);

        ScrollOrders();
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

    public void EmptySlot(Recipe recipe)
    {
        SlotsArray[recipe.CurrentSlot].CurrentState = Slot.State.Empty;

        OrderList.Remove(recipe);

        if(recipe.IsSelected)
            Panel.ResetInfo();

        Destroy(recipe.gameObject);
    }
}
