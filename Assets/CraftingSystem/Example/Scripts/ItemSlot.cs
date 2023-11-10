using System;
using CraftingSystem.Example;
using UnityEngine;
using UnityEngine.EventSystems;

//Holds reference and count of items, manages their visibility in the Inventory panel
public class ItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IItemSlot, IDropHandler
{
    [SerializeField]
    private TMPro.TextMeshProUGUI descriptionText;
    [SerializeField]
    private TMPro.TextMeshProUGUI nameText;
    
    private InventoryItem _item = null;
    public InventoryItem Item => _item;
    private UseableItem useableItem => _item.ItemInfo;

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
        _item.SetSlot(this);
        _item.GoToSlot();
        return true;
    }

    public void Clear()
    {
        _item = null;
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

    public void OnDrop(PointerEventData eventData)
    {
        if (_item != null) return;
        
        var item = eventData.pointerDrag.GetComponent<InventoryItem>();
        if (item == null) return;
        item.ClearSlot();
        SetItem(item);
    }
}
