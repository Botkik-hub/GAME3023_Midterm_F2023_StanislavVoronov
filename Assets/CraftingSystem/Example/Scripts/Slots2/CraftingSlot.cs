using System;

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

        public override bool AddItem(DragableItem item)
        {
            var returnValue = base.AddItem(item);
            OnItemChanged?.Invoke();
            return returnValue;
        }
    }
}