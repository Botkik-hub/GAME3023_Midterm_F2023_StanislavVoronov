using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CraftingSystem.Example
{
    public class CraftingSlot : MonoBehaviour, IItemSlot, IDropHandler
    {
        public event Action OnItemChanged;

        public InventoryItem Item => _item;
        
        private InventoryItem _item;


        public bool SetItem(InventoryItem item)
        {
            if (_item!= null)
            {
                _item.GoToLastSlot();
            }
            
            
            _item = item;
            var rectTransform = _item.GetComponent<RectTransform>();
            rectTransform.SetParent(transform);
            rectTransform.anchoredPosition = Vector2.zero;
            rectTransform.localScale = Vector3.one;
            
            
            OnItemChanged?.Invoke();
            return true;
        }

        public void Clear()
        {
            if (_item != null)
            {
                _item.GoToLastSlot();
            }
            _item = null;
        }

        private void OnDisable()
        {
            if (_item != null)
            {
                _item.GoToLastSlot();
                _item = null;
            }
        }

        public void OnDrop(PointerEventData eventData)
        {
            var item = eventData.pointerDrag.GetComponent<InventoryItem>();
            if (item == null)
                return;
            StartCoroutine(OnDropRoutine(item));
        }

        private IEnumerator OnDropRoutine(InventoryItem item)
        {
         
            if (_item != null)
            {
                _item.GoToLastSlot();
                _item = null;
            }
            yield return new WaitForEndOfFrame();
            //item.ClearSlot();
            SetItem(item);   
        }
    }
}