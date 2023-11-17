using CraftingSystem.Core;
using CraftingSystem.Example.Slots2;
using UnityEngine;
using UnityEngine.UI;

namespace CraftingSystem.Example
{
    public class CraftGridExample : MonoBehaviour  
    {
        [SerializeField] private CraftingSlot[] craftingSlots;
        
        private ResultSlot _resultSlot;
        
        
        private RecipeBook _recipeBook;

        private const int GridSize = 3;        
        
        private Button _craftButton;

        private bool _hasItem;
        
        private void Awake()
        {
            _resultSlot = GetComponentInChildren<ResultSlot>();
            _recipeBook = FindObjectOfType<RecipeBook>();
            _craftButton = GetComponentInChildren<Button>();
            _craftButton.interactable = false;
        }

        private void Start()
        {
            foreach (var craftingSlot in craftingSlots)
            {
                craftingSlot.OnItemChanged += OnItemChanged;
            }
        }

        private void OnEnable()
        {
            _craftButton.onClick.AddListener(CreateItem);
        }

        private void OnDisable()
        {
            _craftButton.onClick.RemoveListener(CreateItem);
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
            
            _resultSlot.SetItem((UseableItem)craftedItem, resultCount);
            var hasItem = craftedItem != null;
            _hasItem = hasItem;
            _craftButton.interactable = hasItem;
        }

        private void CreateItem()
        {
            if (!_hasItem) return;
            
            _resultSlot.CreateItem();
            
            foreach (var slot in craftingSlots)
            {
                if (slot.Item == null) continue;
                
                slot.Item.Count -=1;
            }
        }
    }
}