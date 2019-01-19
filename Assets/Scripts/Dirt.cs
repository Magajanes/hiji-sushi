using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dirt : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer dirtRenderer;
    [SerializeField]
    private Sprite[] dirtSprites;

    private void Awake()
    {
        CameraMove.OnModeChange += SetRandomSprites;
    }

    public void SetRandomSprites(bool cookMode)
    {
        if (!cookMode)
            dirtRenderer.sprite = dirtSprites[Random.Range(0, dirtSprites.Length)];
    }

    private void OnDestroy()
    {
        CameraMove.OnModeChange -= SetRandomSprites;
    }
}
