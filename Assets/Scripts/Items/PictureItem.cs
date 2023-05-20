using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureItem : Item
{
    [SerializeField] private SpriteRenderer[] peaces;
    private Color[] colors;

    private void Awake()
    {
        peaces = GetComponentsInChildren<SpriteRenderer>();
        colors = new Color[4];
    }


    private void SetFrame(Color color)
    {
        peaces[4].color = color;
    }

    public Color[] GetTop()
    {
        return new Color[4] { colors[0], colors[1], Color.clear, Color.clear };
    }

    public Color[] GetDown()
    {
        return new Color[4] { Color.clear, Color.clear, colors[2], colors[3] };
    }

    public Color[] GetLeft()
    {
        return new Color[4] { colors[0], Color.clear, colors[2], Color.clear };
    }

    public Color[] GetRight()
    {
        return new Color[4] { Color.clear, colors[1], Color.clear, colors[3] };
    }

    public void UpdateColors(Color[] colors)
    {
        this.colors = colors;
        for (int i = 0; i < colors.Length; i++)
        {
            peaces[i].color = colors[i];
        }
    }
}
