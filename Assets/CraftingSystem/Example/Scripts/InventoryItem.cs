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
        
        private RectTransform _rectTransform;
        
        protected void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _itemIcon = GetComponentInChildren<Image>();
            if (_itemInfo != null)
                SetUp(_itemInfo);
        }

        public void SetSlot(IItemSlot slot)
        {
            _currentSlot = slot;
        }
        
        
        public void ClearSlot()
        {
            _currentSlot.Clear();
            _currentSlot = null;
        }
        
        public void GoToSlot()
        {
            // BUG: end of the game throws null reference

            if (_currentSlot == null)
                return;

            _rectTransform.SetParent(_currentSlot.transform);
            _rectTransform.anchoredPosition = Vector2.zero;
            _rectTransform.localScale = Vector3.one;
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
            var tempParent = GameObject.FindGameObjectWithTag("TempParent");
            
            if (tempParent == null)
                throw new MissingReferenceException("TempParent not found, please add a GameObject with tag TempParent to the scene");
            
            _rectTransform.SetParent(tempParent.transform);
            _beginDragSlot = _currentSlot;
            _itemIcon.raycastTarget = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _itemIcon.raycastTarget = true;
            if (_currentSlot != _beginDragSlot)
            {
                return;
            }
            GoToSlot();
        }
    }
}