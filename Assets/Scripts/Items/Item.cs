using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Item : MonoBehaviour
{
    public string itemName = "";
    public int price = 0;

    private SpriteRenderer _spriteRenderer;
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }


    public void SetVisibility(bool isVisible)
    {
        _spriteRenderer.enabled = isVisible;
    }
}
