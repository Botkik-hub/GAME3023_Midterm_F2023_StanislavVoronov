using UnityEngine;
using UnityEngine.UI;

namespace CraftingSystem.Example
{
    public class ResultSlot : MonoBehaviour, IItemSlot
    {
        public InventoryItem Item => _item;

        private InventoryItem _item;
        private UseableItem _previewItem;
        
        [SerializeField] private Image _itemIcon; 
        
        [SerializeField] private InventoryItem _itemPrefab;

        private Inventory _inventory;
        
        
        private void Awake()
        {
            _inventory = FindObjectOfType<Inventory>();
        }

            
        
        public void SetItem(UseableItem preview)
        {
            _previewItem = preview;
            _itemIcon.gameObject.SetActive(false);
            if (_previewItem == null) return;
            _itemIcon.sprite = preview.icon;
            _itemIcon.gameObject.SetActive(true);
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
            _itemIcon.gameObject.SetActive(false);
            _item = null;
        }

        public void CreateItem()
        {
            if (_previewItem == null) return;
            
            _item = Instantiate(_itemPrefab, transform.position, Quaternion.identity);
            _item.SetUp(_previewItem, 1);
            _inventory.AddItem(_item);
        }
    }
}