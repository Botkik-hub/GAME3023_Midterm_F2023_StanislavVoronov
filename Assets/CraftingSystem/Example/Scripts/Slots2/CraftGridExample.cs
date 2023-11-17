using CraftingSystem.Core;
using UnityEngine;

namespace CraftingSystem.Example.Slots2
{
    public class CraftGridExample : MonoBehaviour  
    {
        [SerializeField] private CraftingSlot[] craftingSlots;
        
        private ResultSlot _resultSlot;
        
        private RecipeBook _recipeBook;

        private int GridSize = 3;        
        
        private void Awake()
        {
            _resultSlot = GetComponentInChildren<ResultSlot>();
            _recipeBook = FindObjectOfType<RecipeBook>();
        }

        private void OnEnable()
        {
            foreach (var craftingSlot in craftingSlots)
            {
                craftingSlot.OnItemChanged += OnItemChanged;
            }
        }
        
        private void OnDisable()
        {
            foreach (var craftingSlot in craftingSlots)
            {
                craftingSlot.OnItemChanged -= OnItemChanged;
            }
        }

        private void OnItemChanged()
        {
            var items = new Item[GridSize * GridSize];
            for (int i = 0; i < craftingSlots.Length; i++)
            {
                if (craftingSlots[i].Item == null)
                {
                    items[i] = null;
                    continue;
                }
                items[i] = craftingSlots[i].Item.ItemInfo;
            }
            
            var craftedItem = _recipeBook.CheckGridState(items, new Vector2Int(GridSize, GridSize), out int resultCount);
            
            _resultSlot.SetPreview((UseableItem)craftedItem, resultCount);
        }

        public void SpendMaterials()
        {
            foreach (var slot in craftingSlots)
            {
                if (slot.Item == null) continue;
                
                slot.Item.Count -=1;
            }
            OnItemChanged();
        }
    }
}