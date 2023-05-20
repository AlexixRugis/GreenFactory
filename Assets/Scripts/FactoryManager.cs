using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryManager : MonoBehaviour
{
    public static FactoryManager instance;

    public Dictionary<Vector3Int, MachineObject> machines;

    [SerializeField] private int price = 0;
    [SerializeField] private List<MachineObject> standardMachines = new List<MachineObject>();

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More one FactoryManager on scene");
        }
        instance = this;

        machines = new Dictionary<Vector3Int, MachineObject>();
        standardMachines.ForEach(x => machines.Add(Vector3Int.RoundToInt(x.transform.position), x));
    }

    public void RecalculateFactory()
    {
        price = RecalculatePrice();
    }

    private int RecalculatePrice()
    {
        int finalPrice = 0;

        foreach( MachineObject machineObj in machines.Values)
        {
            if(machineObj.machine)
                finalPrice += machineObj.machine.price;
        }

        return finalPrice;
    }

}
