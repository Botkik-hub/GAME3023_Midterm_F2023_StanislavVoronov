using UnityEngine;
using UnityEngine.EventSystems;

namespace CraftingSystem.Example.Slots2
{
    public class DescriptionSlot : BaseSlot, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private TMPro.TextMeshProUGUI descriptionText;
        [SerializeField]
        private TMPro.TextMeshProUGUI nameText;

                
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_item != null)
            {
                descriptionText.text = ItemInfo.description;
                nameText.text = ItemInfo.name;
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if(_item != null)
            {
                descriptionText.text = "";
                nameText.text = "";
            }
        }
    }
}