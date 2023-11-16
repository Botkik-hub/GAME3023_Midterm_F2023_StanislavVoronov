using UnityEngine;

namespace CraftingSystem.Core
{
    [CreateAssetMenu(fileName = "New Recipe", menuName = "Items/New Recipe")]
    public class RecipeScriptable : ScriptableObject
    {
        //Used in editor
        [SerializeField] private Item[] ingredients;
        [SerializeField] private Vector2Int sizeOfGrid;
        [SerializeField] private Item result;
        [SerializeField] private int resultCount = 1;
        
        private bool _isRecipeValid = false;
        
        //Used in game logic
        private Recipe _recipe;        
        public Recipe Recipe => _recipe;
        public bool IsValid => _isRecipeValid;
        
        private void OnValidate()
        {
            if (sizeOfGrid == Vector2Int.zero)
            {
                Debug.LogError("Size of grid cannot be zero");
                return;
            }

            _isRecipeValid = false;
            // check if at least one item is not null
            foreach (var item in ingredients)
            {
                if (item != null)
                {
                    _recipe = new Recipe(sizeOfGrid, ingredients, result, resultCount);
                    _isRecipeValid = resultCount > 0;
                    return;                     
                }
            }
        }
    }
}