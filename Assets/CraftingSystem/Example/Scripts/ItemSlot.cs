using System;
using CraftingSystem.Example;
using UnityEngine;
using UnityEngine.EventSystems;

//Holds reference and count of items, manages their visibility in the Inventory panel
public class ItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IItemSlot, IDragHandler
{
    private InventoryItem _item = null;
    public InventoryItem Item => _item;
    public bool SetItem(InventoryItem item)
    {
        if (_item != null)
        {
            return false;
        }

        if (item == null)
        {
            throw new ArgumentException("Item cannot be null. Use Clear() instead.");
        }
        
        _item = item;
        var rectTransform = _item.GetComponent<RectTransform>();        
        rectTransform.SetParent(transform);
        rectTransform.localPosition = Vector3.zero;
        rectTransform.localScale = Vector3.one;
        _item.SetSlot(this);
        return true;
    }

    public void Clear()
    {
        _item = null;
    }

    private UseableItem useableItem => _item.ItemInfo;
    
    [SerializeField]
    private TMPro.TextMeshProUGUI descriptionText;
    [SerializeField]
    private TMPro.TextMeshProUGUI nameText;

    public void UseItemInSlot()
    {
        if (CanUseItem())
        {
            _item.Use();
        }
    }

    private bool CanUseItem()
    {
        return (_item != null && _item.CanUse());
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_item != null)
        {
            descriptionText.text = useableItem.description;
            nameText.text = useableItem.name;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(_item != null)
        {
            descriptionText.text = "";
            nameText.text = "";
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_item != null) return;
        
        var item = eventData.pointerDrag.GetComponent<InventoryItem>();
        if (item == null) return;

        SetItem(item);
    }

    private void OnValidate()
    {
        var boxCollider2D = GetComponent<BoxCollider2D>();
        var rectTransform = GetComponent<RectTransform>();
        boxCollider2D.size = rectTransform.sizeDelta;
    }
}
