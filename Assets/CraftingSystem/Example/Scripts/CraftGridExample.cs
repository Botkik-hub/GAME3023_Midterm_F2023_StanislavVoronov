using CraftingSystem.Core;
using UnityEngine;

namespace CraftingSystem.Example
{
    public class CraftGridExample : MonoBehaviour  
    {
        [SerializeField] private RecipeBook recipeBook;
        
        [SerializeField] private CraftingSlot[] craftingSlots;
        
        [SerializeField] private ResultSlot resultSlot;
        
        private RecipeBook _recipeBook;

        private const int GridSize = 3;        
        
        private void Awake()
        {
            _recipeBook = FindObjectOfType<RecipeBook>();
            craftingSlots = GetComponentsInChildren<CraftingSlot>();
            resultSlot = GetComponentInChildren<ResultSlot>();
        }

        private void Start()
        {
            foreach (var craftingSlot in craftingSlots)
            {
                craftingSlot.OnItemChanged += OnItemChanged;
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

            var craftedItem = _recipeBook.CraftItem(items, new Vector2Int(GridSize, GridSize));
            
            resultSlot.SetItem(craftedItem);
        }

        private void Craft()
        {
            // TODO implement crafting
            resultSlot.CreateItem();
        }
    }
}