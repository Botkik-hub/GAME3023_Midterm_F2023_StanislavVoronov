using CraftingSystem.Core;
using UnityEngine;
using UnityEngine.UI;

namespace CraftingSystem.Example
{
    public class ResultSlot : MonoBehaviour, IItemSlot
    {
        public InventoryItem Item => _item;

        private InventoryItem _item;
        private Item _previewItem;
        private Image _itemIcon; 
        
        
        public void SetItem(Item preview)
        {
            _previewItem = preview;
            _itemIcon.sprite = preview.icon;
        }

        public bool SetItem(InventoryItem item)
        {
            // read only slot, item set by other means
            return false;
        }

        public void Clear()
        {
            _previewItem = null;
            _itemIcon.sprite = null;
            _item = null;
        }
    }
}