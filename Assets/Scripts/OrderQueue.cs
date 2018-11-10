using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderQueue : MonoBehaviour
{
    private readonly Vector3 InitialPosition = Vector3.zero;

    private float orderInterval = 3f;

    private List<GameObject> currentLevelOrders;

    private void Start()
    {
        SetCurrentOrderList();
    }

    private void Update()
    {
        orderInterval -= Time.deltaTime;

        if (orderInterval <= 0f)
        {
            ReceiveOrder();

            orderInterval = Random.Range(1f, 5f);
        }
    }

    private void SetCurrentOrderList()
    {
        currentLevelOrders = GameManager.GetCurrentRecipesList();
    }

    public void ReceiveOrder()
    {
        int index = Random.Range(0, currentLevelOrders.Count);

        var order = Instantiate(currentLevelOrders[index], InitialPosition, Quaternion.identity);

        var recipe = order.GetComponent<Recipe>();

        recipe.Initialize();
    }
}
