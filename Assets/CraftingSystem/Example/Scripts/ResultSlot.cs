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
        
        [SerializeField] private InventoryItem _itemPrefab;
        
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
        
        public void CreateItem()
        {
            if (_item == null) return;
            
            _item = Instantiate(_itemPrefab, transform.position, Quaternion.identity);
            _item.transform.SetParent(transform);
            _item.transform.localScale = Vector3.one;
            _item.SetSlot(this);
        }
    }
}