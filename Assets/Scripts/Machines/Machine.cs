using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Machine", menuName = "")]
public class Machine : ScriptableObject
{
    [Header("Information")]
    new public string name = "";
    [Range(0,1000)]
    public int energyUse = 0;
    [Range(0, 100)]
    public int ecological = 0;
    public int price = 0;



    [Header("Graphics")]
    public Sprite sprite = null;
    public GameObject prefab = null;
}
