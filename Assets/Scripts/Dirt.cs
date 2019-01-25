using UnityEngine;

public class Dirt : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer dirtRenderer;
    [SerializeField]
    private Sprite[] dirtSprites;

    private void Awake()
    {
        Hands.OnWashComplete += SetRandomSprites;
    }

    private void Start()
    {
        SetRandomSprites();
    }

    public void SetRandomSprites()
    {
        dirtRenderer.sprite = dirtSprites[Random.Range(0, dirtSprites.Length)];
    }

    private void OnDestroy()
    {
        Hands.OnWashComplete -= SetRandomSprites;
    }
}
