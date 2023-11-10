using CraftingSystem.Example;
using TMPro;

namespace CraftingSystem
{
    public class InventoryStackedItem : InventoryItem
    {
        public int _count;
        private TMP_Text _itemCountText;
        
        
        private new void Awake()
        {
            base.Awake();
            _itemCountText = GetComponentInChildren<TMP_Text>();

            _itemCountText.text = _count.ToString();
        }
    }
}