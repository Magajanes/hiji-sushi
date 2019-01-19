using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dirt : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer dirtRenderer;
    [SerializeField]
    private Sprite[] dirtSprites;

    private void OnEnable()
    {
        dirtRenderer.sprite = dirtSprites[Random.Range(0, dirtSprites.Length)];
    }
}
