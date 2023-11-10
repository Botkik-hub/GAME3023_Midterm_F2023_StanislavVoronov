using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CraftingSystem.Example
{
    public class CraftingSlot : MonoBehaviour, IItemSlot, IDropHandler
    {
        public event Action OnItemChanged;

        public InventoryItem Item => _item;
        
        private InventoryItem _item;

        private Inventory _inventory;

        private void Awake()
        {
            _inventory = FindObjectOfType<Inventory>();
        }


        public bool SetItem(InventoryItem item)
        {
            if (_item!= null)
            {
                var oldItem = _item;
                oldItem.ClearSlot();
                _inventory.AddItem(oldItem);
            }
            
            
            _item = item;
            var rectTransform = _item.GetComponent<RectTransform>();
            rectTransform.SetParent(transform);
            rectTransform.anchoredPosition = Vector2.zero;
            rectTransform.localScale = Vector3.one;
            _item.SetSlot(this);
            
            OnItemChanged?.Invoke();
            return true;
        }

        public void Clear()
        {
            _item = null;
            OnItemChanged?.Invoke();
        }

        private void OnDisable()
        {
            if (_item != null)
            {
                _item.GoToSlot();
                _item = null;
            }
        }

        public void OnDrop(PointerEventData eventData)
        {
            var item = eventData.pointerDrag.GetComponent<InventoryItem>();
            if (item == null) return;
            
            if (_item != null)
            {
                var oldItem = _item;
                oldItem.ClearSlot();
                _inventory.AddItem(oldItem);
            }
            
            item.ClearSlot();
            SetItem(item);
        }
    }
}