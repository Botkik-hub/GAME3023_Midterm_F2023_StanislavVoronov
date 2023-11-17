using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CraftingSystem.Example.Slots2
{
    public class DragableItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        private UseableItem _itemInfo;
        
        public UseableItem ItemInfo => _itemInfo;

        private Image _itemIcon;
        private RectTransform _rectTransform;
        
        private int _count = 1;
        private TMP_Text _itemCountText;
        
        private Button _useButton;
        
        private BaseSlot _currentSlot;
        
        private Inventory _inventory;

        public bool IsUsable;
        
        public int Count
        {
            get => _count;
            set
            {
                _count = value;
                if (_count <= 0)
                {
                    Destroy(gameObject);
                    return;
                }
                _itemCountText.text = _count.ToString();
            }
        }
        
        private void Awake()
        {
            _inventory = FindObjectOfType<Inventory>();

            _rectTransform = GetComponent<RectTransform>();
            _itemIcon = GetComponentInChildren<Image>();
            _itemCountText = GetComponentInChildren<TMP_Text>();
            _useButton = GetComponentInChildren<Button>();
            _useButton.onClick.AddListener(Use);
            _itemCountText.text = _count.ToString();
        }
        
        private void OnDestroy()
        {
            _useButton.onClick.RemoveListener(Use);
            
            if (_currentSlot != null)
            {
                _currentSlot.RemoveItem();   
            }
        }

        public void SetSlot(BaseSlot slot)
        {
            _currentSlot = slot;
        }
        
        private void RemoveFromSlot()
        {
            _currentSlot.RemoveItem();
            _currentSlot = null;
        }
        
        public void SetUp(UseableItem itemInfo, int count)
        {
            _itemInfo = itemInfo;
            _itemIcon.sprite = _itemInfo.icon;  
            Count = count;
        }
        
        public void StackItems(DragableItem other)
        {
            if (ItemInfo != other.ItemInfo)
            {
                throw new ArgumentException("Cannot stack different items");
            }
            
            Count += other.Count;
            Destroy(other.gameObject);
        }
        
        
        // Can be made public if needed 
        private void Use()
        {
            if (!IsUsable) return;
            
            _itemInfo.Use();
            if (_itemInfo.isConsumable)
            {
                Count--;
                if (Count <= 0)
                {
                    //Can be made more efficient by pooling items
                    Destroy(gameObject);
                }    
            }
        }

        public void GoToSlot()
        {
            if (_currentSlot == null)
                return;

            _rectTransform.SetParent(_currentSlot.transform);
            _rectTransform.anchoredPosition = Vector2.zero;
            _rectTransform.localScale = Vector3.one;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                var tempParent = GameObject.FindGameObjectWithTag("TempParent");
                if (tempParent == null)
                    throw new MissingReferenceException(
                        "TempParent not found, please add a GameObject with tag TempParent to the scene");

                _rectTransform.SetParent(tempParent.transform);

                RemoveFromSlot();

                _itemIcon.raycastTarget = false;
                return;
            }

            if (eventData.button == PointerEventData.InputButton.Right)
            {
                //todo
                var tempParent = GameObject.FindGameObjectWithTag("TempParent");
                if (tempParent == null)
                    throw new MissingReferenceException(
                        "TempParent not found, please add a GameObject with tag TempParent to the scene");
                _rectTransform.SetParent(tempParent.transform);

                var slot = _currentSlot;
                RemoveFromSlot();
                
                if (_count > 1)
                {
                    
                    var newDraggableItem = Instantiate(_inventory.ItemPrefab, transform.position, Quaternion.identity).GetComponent<DragableItem>();
                    newDraggableItem.SetUp(_itemInfo, _count - 1);
                    slot.AddItem(newDraggableItem);
                    newDraggableItem.GoToSlot();
                    Count = 1;
                }

                _itemIcon.raycastTarget = false;
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _itemIcon.raycastTarget = true;
            if (_currentSlot == null)
            {
                _inventory.AddItem(this);
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.position;
        }
    }
}