using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toolbar : MonoBehaviour
{
    [SerializeField] private KeyCode[] _keys = new KeyCode[5];
    private ToolbarCell[] _cells;

    public static Toolbar instance;

    private Machine _selectedMachine;
    private SpriteRenderer _selectionHintSprite;
    // Start is called before the first frame update
    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("More one Toolbar on scene");
        }
        instance = this;

        _cells = GetComponentsInChildren<ToolbarCell>();
        _selectionHintSprite = GameObject.FindGameObjectWithTag("Selection Hint").transform.GetChild(0).GetComponent<SpriteRenderer>();

    }

    private void Start()
    {
        SetCellActive(0);
    }
    private void Update()
    {
        for (int i = 0; i < _keys.Length; i++)
        {
            if (Input.GetKeyDown(_keys[i]))
            {
                SetCellActive(i);
            }
        }
    }

    public void SetCellActive(int index)
    {
        for(int i = 0; i < _cells.Length; i++)
        {
            if(i == index)
            {
                _cells[i].SetActivated(true);
                _selectedMachine = _cells[i].GetMachine();
                if (_selectedMachine != null)
                    _selectionHintSprite.sprite = _selectedMachine.sprite;
                else _selectionHintSprite.sprite = null;
            }
            else
            {
                _cells[i].SetActivated(false);
            }
        }
    }

    public Machine GetMachine()
    {
        return _selectedMachine;
    }
}
