using UnityEngine;
using UnityEngine.EventSystems;

namespace CraftingSystem.Example.Slots2
{
    public class ResultSlot : BaseSlot
    {
        [SerializeField] private DragableItem _itemPrefab;
        
        private CraftGridExample _craftGrid;
        
        private UseableItem _previewItem;
        
        private Inventory _inventory;
        
        
        private void Awake()
        {
            _craftGrid = FindObjectOfType<CraftGridExample>();
            _inventory = FindObjectOfType<Inventory>();
        }

        private void Start()
        {
            CreatePreviewItem();            
        }

        private void CreatePreviewItem()
        {
            _item = Instantiate(_inventory.ItemPrefab).GetComponent<DragableItem>();
            _item.SetSlot(this);
            _item.GoToSlot();
            _item.IsUsable = false;
            _item.gameObject.SetActive(false);
        }
        
        public void SetPreview(UseableItem preview, int count)
        {
            _previewItem = preview;
            if (_previewItem == null) 
            {
                _item.gameObject.SetActive(false);
                return;
            } 
            
            _item.SetUp(_previewItem, count);
            _item.gameObject.SetActive(true);
        }

        public override void RemoveItem()
        {
            if (_item != null)
                _item.IsUsable = true;
            
            _previewItem = null;
            _item = null;

            CreatePreviewItem();
            _craftGrid.SpendMaterials();
        }

        public override void OnDrop(PointerEventData eventData)
        { 
            // Cannot drop item here
            return;
        }
    }
}