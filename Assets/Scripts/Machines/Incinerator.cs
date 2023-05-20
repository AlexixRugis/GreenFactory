using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Incinerator : MachineObject
{
    public float timer = 0;
    [SerializeField] private float _burningTime = 0;

    [SerializeField] private MachineInput input;

    private void Awake()
    {
        input = new MachineInput();
        inputs.Add(Vector3Int.RoundToInt(transform.position - transform.up), input);
    }

    void Update()
    {
        if(input.item != null)
        {
            input.item.SetVisibility(false);
            if (timer < _burningTime)
                timer += Time.deltaTime;
            else
            {
                Destroy(input.item.gameObject);
                timer = 0;
            }
        }
    }
}
