using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseInteraction : MonoBehaviour
{

    [Header("Selection Hint")]
    [SerializeField] private GameObject _selectionObject = null;
    [SerializeField] private Color _selectionStandard = Color.white;
    [SerializeField] private Color _selectionWrong = Color.white;
    private Transform _selectionTransform;
    private SpriteRenderer _selectionRenderer;
    private UnityEngine.Tilemaps.Tilemap _tilemap;
    private Camera _camera;
    private Vector3Int _pointerPosition = Vector3Int.zero;
    private int objectRotation = 0;

    private void Awake()
    {
        _selectionTransform = _selectionObject.transform;
        _selectionRenderer = _selectionObject.GetComponent<SpriteRenderer>();
        _camera = Camera.main;
    }

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            _selectionObject.SetActive(false);
            return;
        }

        _selectionObject.SetActive(true);
        Vector3Int position = Vector3Int.RoundToInt(_camera.ScreenToWorldPoint(Input.mousePosition)) * new Vector3Int(1, 1, 0);

        if (position != _pointerPosition)
        {
            _pointerPosition = position;
            UpdateHint();
        }

        if(Input.GetButtonDown("Add Machine") && !FactoryManager.instance.machines.ContainsKey(_pointerPosition))
        {
            Machine machine = Toolbar.instance.GetMachine();

            if (machine != null)
            {
                MachineObject machineObject = Instantiate(machine.prefab, _pointerPosition, Quaternion.Euler(0,0, objectRotation), FactoryManager.instance.transform).GetComponent<MachineObject>();
                FactoryManager.instance.machines.Add(_pointerPosition, machineObject);
                FactoryManager.instance.RecalculateFactory();
            }

            UpdateHint();
        }
        else if(Input.GetButtonDown("Delete Machine"))
        {
            MachineObject mo;
            FactoryManager.instance.machines.TryGetValue(_pointerPosition, out mo);
            if(mo != null)
            {
                if (mo.isInteractable)
                {
                    FactoryManager.instance.machines.Remove(_pointerPosition);
                    mo.ClearMachine();
                    FactoryManager.instance.RecalculateFactory();
                    UpdateHint();
                }
            }

        }
        else if(Input.GetButtonDown("Rotate Machine"))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                objectRotation += 90;
            }
            else
            {
                objectRotation -= 90;
            }
            if (objectRotation <= -360 || objectRotation >= 360) objectRotation = 0;
            UpdateHint();
        }



    }

    private void UpdateHint()
    {
        _selectionTransform.position = _pointerPosition;
        _selectionTransform.rotation = Quaternion.Euler(0, 0, objectRotation);
        
        if(FactoryManager.instance.machines.ContainsKey(_pointerPosition))
        {
            _selectionRenderer.color = _selectionWrong;
        }
        else
        {
            _selectionRenderer.color = _selectionStandard;
        }
    }

}
