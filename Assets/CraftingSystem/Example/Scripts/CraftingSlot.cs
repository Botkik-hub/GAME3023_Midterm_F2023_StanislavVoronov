using System;
using UnityEngine;

namespace CraftingSystem.Example
{
    public class CraftingSlot : MonoBehaviour, IItemSlot
    {
        public event Action OnItemChanged;

        public InventoryItem Item => _item;
        
        private InventoryItem _item;


        public bool SetItem(InventoryItem item)
        {
            if (_item!= null)
            {
                return false;
            }
            
            _item = item;
            _item.transform.position = transform.position;
            OnItemChanged?.Invoke();
            return true;
        }

        public void Clear()
        {
            _item = null;
        }
    }
}