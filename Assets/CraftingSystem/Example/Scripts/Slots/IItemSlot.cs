using UnityEngine;

namespace CraftingSystem.Example
{
    public interface IItemSlot
    {
        public Transform transform { get; }
        
        InventoryItem Item { get; }
        bool SetItem(InventoryItem item);
        void Clear();
    }
}