using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painter : MachineObject
{
    [SerializeField] private MachineInput inputObject;
    [SerializeField] private MachineInput inputPaint;

    [SerializeField] private float paintingTime = 0;
    private Vector3Int output;
    private float paintingTimer = 0;
    void Awake()
    {
        inputObject = new MachineInput();
        inputPaint = new MachineInput();
        output = Vector3Int.RoundToInt(transform.position + transform.up);

        inputs.Add(Vector3Int.RoundToInt(transform.position - transform.up), inputObject);
        inputs.Add(Vector3Int.RoundToInt(transform.position + transform.right), inputPaint);
    }

    // Update is called once per frame
    void Update()
    {
        if(inputObject.item != null) inputObject.item.SetVisibility(false);
        if (inputPaint.item != null) inputPaint.item.SetVisibility(false);

        if (inputObject.item != null && inputPaint.item != null)
        {

            if(inputPaint.item.GetComponent<ColourItem>())
            {
                paintingTimer += Time.deltaTime;
                if(paintingTimer >= paintingTime)
                {
                    MachineObject next;
                    if (FactoryManager.instance.machines.TryGetValue(output, out next))
                    {
                        
                        if (next.PutItem(Vector3Int.RoundToInt(transform.position), inputObject.item))
                        {
                            Conveyor isConveyor = next.GetComponent<Conveyor>();
                            if (isConveyor != null && next.transform.rotation.z != transform.rotation.z) isConveyor.SetProcess(0.5f);

                            foreach(SpriteRenderer sr in inputObject.item.GetComponentsInChildren<SpriteRenderer>()) {
                                sr.color = inputPaint.item.GetComponent<ColourItem>().color;
                            }

                            inputObject.item = null;
                            Destroy(inputPaint.item.gameObject);
                            paintingTimer = 0;
                        }
                    }
                }
            }
        }
    }
}
