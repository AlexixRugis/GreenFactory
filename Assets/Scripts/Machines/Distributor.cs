using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distributor : MachineObject
{
    private Vector3Int output;

    [SerializeField] private GameObject objectPrefab = null;
    [SerializeField] private float objectTime = 0;
    private float timer = 0;
    private Item item = null;

    private void Awake()
    {
        output = Vector3Int.RoundToInt(transform.position + transform.up);
    }
    void Update()
    {
        if (timer < objectTime)
        {
            timer += Time.deltaTime;
        }
        else {
            MachineObject next;
            if (FactoryManager.instance.machines.TryGetValue(output, out next))
            {
                if (item == null)
                {
                    item = Instantiate(objectPrefab, new Vector3(0, 0, 0), Quaternion.identity).GetComponent<Item>();
                    item.SetVisibility(false);
                }
                if (next.PutItem(Vector3Int.RoundToInt(transform.position), item))
                {
                    Conveyor isConveyor = next.GetComponent<Conveyor>();
                    if (isConveyor != null && next.transform.rotation.z != transform.rotation.z) isConveyor.SetProcess(0.5f);
                    item = null;
                    timer = 0;
                }
            }
        }
    }
}
