using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CraftingSystem.Example.Slots2
{
    public class ResultSlot : BaseSlot
    {
        [SerializeField] private Image _itemIcon; 
        [SerializeField] private DragableItem _itemPrefab;
        
        private UseableItem _previewItem;
        
        private TMP_Text _itemCountText;   
        
        private Inventory _inventory;
        private int _craftCount;
        
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
            _craftCount = count;
            _itemCountText.gameObject.SetActive(true);
            _itemCountText.text = _craftCount.ToString();
        }

        public override void RemoveItem()
        {
            _previewItem = null;
            _itemIcon.sprite = null;
            _itemIcon.gameObject.SetActive(false);
            _item = null;
        }

        public override void OnDrop(PointerEventData eventData)
        { 
            // Cannot drop item here
            return;
        }
        
        
        public void CreateItem()
        {
            if (_previewItem == null) return;
            
            _item = Instantiate(_itemPrefab, transform.position, Quaternion.identity);
            _item.SetUp(_previewItem, _craftCount);
            _inventory.AddItem(_item);
        }
    }
}