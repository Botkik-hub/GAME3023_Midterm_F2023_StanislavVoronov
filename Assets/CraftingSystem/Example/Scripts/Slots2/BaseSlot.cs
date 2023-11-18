using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CraftingSystem.Example.Slots2
{
    public class BaseSlot : MonoBehaviour, IDropHandler
    {
        protected DragableItem _item = null;
        public UsableItem ItemInfo => _item.ItemInfo;
        public DragableItem Item => _item;
        
        public virtual bool AddItem(DragableItem item)
        {
            if (item == null)
            {
                throw new ArgumentException("Item cannot be null. Use RemoveItem() instead.");
            }
        
            if (_item != null && _item.ItemInfo == item.ItemInfo)
            {
                _item.StackItems(item);
                return true;
            }
            
            if (_item != null)
            {
                return false;
            }
            
            _item = item;
            _item.SetSlot(this);
            _item.GoToSlot();
            return true;
        }

        public virtual void RemoveItem()
        {
            _item = null;
        }
        
        public virtual void OnDrop(PointerEventData eventData)
        {
            var item = eventData.pointerDrag.GetComponent<DragableItem>();
            if (item == null) return;
            
            if (AddItem(item))
            {
            }
        }

        
    }
}