using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientsManager : SlotBehaviour
{
    public static Vector3 SpawnPos = new Vector3(-1.5f, 1.8f, 0);
    public static Vector3 ExitPos = new Vector3(20, 1.8f, 0);
    public static Vector3 IllPos = new Vector3(10, 1.8f, 0);
    public static Vector3 IllExitPos = new Vector3(10, -10, 0);

    public static ClientsManager Instance;

    public List<Client> ClientsInRestaurant = new List<Client>();
    public Slot[] SlotsArray = new Slot[6];
    public GameObject[] clientPrefabs;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        PrepareSlots(SpawnPos, SlotsArray, 1.8f);
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

        var clientObject = Instantiate(clientPrefabs[Random.Range(0, clientPrefabs.Length)], SpawnPos, Quaternion.identity);
        var client = clientObject.GetComponent<Client>();

        client.CurrentSlotIndex = index;
        ClientsInRestaurant.Add(client);

        iTween.MoveTo(clientObject, iTween.Hash("position", SlotsArray[index].SlotPosition,
                                                "easetype", iTween.EaseType.easeOutExpo,
                                                "time", 1.5f));

        SlotsArray[index].CurrentState = Slot.State.Occupied;
    }

    public void RemoveRandomClient()
    {
        if (ClientsInRestaurant.Count == 0)
            return;

        var client = RandomClient();
        client.Complain();

        iTween.MoveTo(client.gameObject, iTween.Hash("position", ExitPos,
                                                     "easetype", iTween.EaseType.easeInExpo,
                                                     "time", 3f));

        ClientsInRestaurant.Remove(client);
        SlotsArray[client.CurrentSlotIndex].CurrentState = Slot.State.Empty;
        Destroy(client.gameObject, 3f);
    }

    public void RemoveClient(Client client)
    {
        if (ClientsInRestaurant.Count == 0)
            return;

        iTween.MoveTo(client.gameObject, iTween.Hash("position", ExitPos,
                                                     "easetype", iTween.EaseType.easeInExpo,
                                                     "time", 3f));

        ClientsInRestaurant.Remove(client);
        SlotsArray[client.CurrentSlotIndex].CurrentState = Slot.State.Empty;
        Destroy(client.gameObject, 3f);
    }

    public void RemoveIllClient(Client client)
    {
        if (ClientsInRestaurant.Count == 0)
            return;

        iTween.MoveTo(client.gameObject, iTween.Hash("position", IllExitPos,
                                                     "easetype", iTween.EaseType.easeInExpo,
                                                     "time", 3f));

        ClientsInRestaurant.Remove(client);
        SlotsArray[client.CurrentSlotIndex].CurrentState = Slot.State.Empty;
        Destroy(client.gameObject, 3f);
    }

    public Client RandomClient()
    {
        int index = Random.Range(0, ClientsInRestaurant.Count);
        return ClientsInRestaurant[index];
    }
}
