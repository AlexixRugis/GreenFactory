using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineObject : MonoBehaviour
{
    public bool isInteractable = true;

    public Machine machine;

    protected Dictionary<Vector3Int, MachineInput> inputs = new Dictionary<Vector3Int, MachineInput>();

    public bool PutItem(Vector3Int position, Item item)
    {
        MachineInput input;

        if(inputs.TryGetValue(position, out input)) {
            if (input.item != null)
            {
                return false;
            }

            input.item = item;
            return true;
        }

        return false;
    }

    public void ClearMachine()
    {
        foreach(MachineInput input in inputs.Values)
        {
            Destroy(input.item.gameObject);
        }
        Destroy(gameObject);
    }

}

[System.Serializable]
public class MachineInput
{
    public Item item = null;
}