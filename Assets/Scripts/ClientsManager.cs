using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientsManager : SlotBehaviour
{
    private readonly Vector3 SpawnPosition = new Vector3(-2f, 2f, 0f);
    private readonly Vector3 ExitPosition = new Vector3(20f, 2f, 0f);

    public static ClientsManager Instance;

    public List<Client> ClientsInRestaurant = new List<Client>();

    public Slot[] SlotsArray = new Slot[6];

    public GameObject clientPrefab;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        PrepareSlots(SpawnPosition, SlotsArray, 3f);
    }
    
    public void ReceiveClient()
    {
        if (ClientsInRestaurant.Count >= 6)
            return;

        int index;

        do
        {
            index = Random.Range(0, SlotsArray.Length);
        }
        while (SlotsArray[index].CurrentState == Slot.State.Occupied);

        var clientObject = Instantiate(clientPrefab, SpawnPosition, Quaternion.identity);
        var client = clientObject.GetComponent<Client>();

        client.CurrentSlotIndex = index;

        ClientsInRestaurant.Add(client);

        iTween.MoveTo(clientObject, iTween.Hash("position", SlotsArray[index].SlotPosition,
                                                "easetype", iTween.EaseType.easeOutExpo,
                                                "time", 1f));

        SlotsArray[index].CurrentState = Slot.State.Occupied;
    }

    public void RemoveRandomClient()
    {
        if (ClientsInRestaurant.Count == 0)
            return;

        var client = RandomClient();
        var clientObject = client.gameObject;

        client.Complain();

        iTween.MoveTo(clientObject, iTween.Hash("position", ExitPosition,
                                                "easetype", iTween.EaseType.easeInExpo,
                                                "time", 3f));

        ClientsInRestaurant.Remove(client);

        SlotsArray[client.CurrentSlotIndex].CurrentState = Slot.State.Empty;

        Destroy(clientObject, 3f);
    }

    public void RemoveClient(Client client)
    {
        if (ClientsInRestaurant.Count == 0)
            return;

        var clientObject = client.gameObject;

        iTween.MoveTo(clientObject, iTween.Hash("position", ExitPosition,
                                                "easetype", iTween.EaseType.easeInExpo,
                                                "time", 3f));

        ClientsInRestaurant.Remove(client);

        SlotsArray[client.CurrentSlotIndex].CurrentState = Slot.State.Empty;

        Destroy(clientObject, 3f);
    }

    public Client RandomClient()
    {
        int index = Random.Range(0, ClientsInRestaurant.Count);

        return ClientsInRestaurant[index];
    }
}
