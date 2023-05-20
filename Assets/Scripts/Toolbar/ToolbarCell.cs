using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ToolbarCell : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Machine _machine = null;

    [Header("UI")]
    [SerializeField] private Image _image = null;
    [SerializeField] private Color _standardColor = Color.white;
    [SerializeField] private Color _selectedColor = Color.white;


    private Image _cellImage;

    private void Awake()
    {
        _cellImage = GetComponent<Image>();
    }

    private void Start()
    {
        
        if (_machine != null)
        {
            SetCellSprite(_machine.sprite);
        }
        else
        {
            ClearCell();
        }
    }

    public void SetActivated(bool isActive)
    {
        if(isActive)
        {
            _cellImage.color = _selectedColor;
        }
        else
        {
            _cellImage.color = _standardColor;
        }
    }

    public void SetCellSprite(Sprite sprite)
    {
        _image.enabled = true;
        _image.sprite = sprite;
    }

    public void ClearCell()
    {
        _image.enabled = false;
    }

    public Machine GetMachine()
    {
        return _machine;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Toolbar.instance.SetCellActive(transform.GetSiblingIndex());
    }
}
