

using System;
using UnityEngine.EventSystems;

namespace CraftingSystem.Example.Slots2
{
    public class CraftingSlot : BaseSlot
    {
        public event Action OnItemChanged;

        public override void RemoveItem()
        {
            base.RemoveItem();
            OnItemChanged?.Invoke();
        }

        public override void OnDrop(PointerEventData eventData)
        {
            var item = eventData.pointerDrag.GetComponent<DragableItem>();
            if (item == null) return;
            
            if (AddItem(item))
            {
                item.SetSlot(this);
                OnItemChanged?.Invoke();
            }
        }
    }
}