using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : MachineObject
{
    private float process = 0;
    [SerializeField] private MachineInput input;
    private Vector3Int output;
    public void Awake()
    {
        input = new MachineInput();
        inputs.Add(Vector3Int.RoundToInt(transform.position - transform.up), input);
        inputs.Add(Vector3Int.RoundToInt(transform.position + transform.right), input);
        inputs.Add(Vector3Int.RoundToInt(transform.position - transform.right), input);

        output = Vector3Int.RoundToInt(transform.position + transform.up);
    }

    public void Update()
    {
        if(input.item != null)
        {
            input.item.SetVisibility(true);
            if(process < 1)
            {
                process += Time.deltaTime;
                input.item.transform.position = Vector3.Lerp(transform.position - transform.up/2, transform.position + transform.up/2, process);
            }
            else
            {
                MachineObject next;
                if(FactoryManager.instance.machines.TryGetValue(output, out next))
                {
                    
                    if (next.PutItem(Vector3Int.RoundToInt(transform.position), input.item))
                    {
                        Conveyor isConveyor = next.GetComponent<Conveyor>();
                        if (isConveyor != null && next.transform.rotation.z != transform.rotation.z) isConveyor.SetProcess(0.5f);
                        input.item = null;
                        process = 0;
                    }
                }
            }
        }
    }

    public void SetProcess(float p)
    {
        process = Mathf.Clamp(p, 0, 1);
    }
}


