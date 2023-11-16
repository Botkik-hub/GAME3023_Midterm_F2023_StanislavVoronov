using TMPro;
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

        private TMP_Text _itemCountText;   
        
        private Inventory _inventory;

        private int craftCount;
        
        private void Awake()
        {
            _inventory = FindObjectOfType<Inventory>();
            _itemCountText = GetComponentInChildren<TMP_Text>();
            _itemCountText.gameObject.SetActive(false);
        }
        
        public void SetItem(UseableItem preview, int count)
        {
            _previewItem = preview;
            if (_previewItem == null) 
            {
                _itemCountText.gameObject.SetActive(false);
                _itemIcon.gameObject.SetActive(false);
                return;
            }
            
            _itemIcon.sprite = preview.icon;
            _itemIcon.gameObject.SetActive(true);
            craftCount = count;
            _itemCountText.gameObject.SetActive(true);
            _itemCountText.text = craftCount.ToString();
        
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
            _item.SetUp(_previewItem, craftCount);
            _inventory.AddItem(_item);
        }
    }
}