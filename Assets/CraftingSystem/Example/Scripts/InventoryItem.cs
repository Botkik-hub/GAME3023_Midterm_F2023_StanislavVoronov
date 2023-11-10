using UnityEngine;
using UnityEngine.EventSystems;
using Image = UnityEngine.UI.Image;

namespace CraftingSystem.Example
{
    public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
    {
        [SerializeField] private UseableItem _itemInfo;
        
        public UseableItem ItemInfo => _itemInfo;

        private Image _itemIcon;

        public void Use()
        {
            _itemInfo.Use();
        }
        
        public bool CanUse()
        {
            return true;
        }
        
        protected void Awake()
        {
            _itemIcon = GetComponentInChildren<Image>();
            if (_itemInfo != null)
                SetUp(_itemInfo);
        }

        public void SetUp(UseableItem itemInfo)
        {
            _itemInfo = itemInfo;
            _itemIcon.sprite = _itemInfo.icon;  
        }
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("Begin Drag");
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log("End Drag");
        }

        public void OnDrop(PointerEventData eventData)
        {
            Debug.Log("Drop");
        }
    }
}