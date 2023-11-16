﻿using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
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
        
        private int _count = 1;
        private TMP_Text _itemCountText;
        
        private Button _useButton;
        
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

        private void OnDestroy()
        {
            _useButton.onClick.RemoveListener(Use);
            
            if (_currentSlot != null)
                _currentSlot.Clear();
        }

        protected void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _itemIcon = GetComponentInChildren<Image>();
            _itemCountText = GetComponentInChildren<TMP_Text>();
            _useButton = GetComponentInChildren<Button>();
            _useButton.onClick.AddListener(Use);
            _itemCountText.text = _count.ToString();
            
            if (_itemInfo != null)
                SetUp(_itemInfo, _count);
        }

        public void SetSlot(IItemSlot slot)
        {
            _currentSlot = slot;
        }
        
        
        public void ClearSlot()
        {
            if (_currentSlot == null)
                return;
            
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
            if (_itemInfo.isConsumable)
            {
                Count--;
                if (Count <= 0)
                {
                    ClearSlot();
                    //Can be made more efficient by pooling items
                    Destroy(gameObject);
                }    
            }
        }
        
        public bool CanUse()
        {
            return true;
        }
        
        public void SetUp(UseableItem itemInfo, int count)
        {
            _itemInfo = itemInfo;
            _itemIcon.sprite = _itemInfo.icon;  
            Count = count;
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