using UnityEngine;
using UnityEngine.EventSystems;
using Image = UnityEngine.UI.Image;

namespace CraftingSystem.Example
{
    public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private UseableItem _itemInfo;
        
        public UseableItem ItemInfo => _itemInfo;

        private Image _itemIcon;

        private IItemSlot _currentSlot;
        
        private IItemSlot _beginDragSlot;
        
        protected void Awake()
        {
            _itemIcon = GetComponentInChildren<Image>();
            if (_itemInfo != null)
                SetUp(_itemInfo);
        }

        public void SetSlot(IItemSlot slot)
        {
            _currentSlot = slot;
        }
        
        public void Use()
        {
            _itemInfo.Use();
        }
        
        public bool CanUse()
        {
            return true;
        }
        
        public void SetUp(UseableItem itemInfo)
        {
            _itemInfo = itemInfo;
            _itemIcon.sprite = _itemInfo.icon;  
        }
        
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            var rectTransform = GetComponent<RectTransform>();
            var tempParent = GameObject.FindGameObjectWithTag("TempParent");
            
            if (tempParent == null)
                throw new MissingReferenceException("TempParent not found, please add a GameObject with tag TempParent to the scene");
            
            rectTransform.SetParent(tempParent.transform);
            _beginDragSlot = _currentSlot;
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (_currentSlot != _beginDragSlot)
            {
                return;
            }
            
            var rectTransform = GetComponent<RectTransform>();
            rectTransform.SetParent(_currentSlot.transform);
            rectTransform.anchoredPosition = Vector2.zero;
        }
    }
}