using UnityEngine;

namespace CraftingSystem.Core
{
    [CreateAssetMenu(fileName = "New Recipe", menuName = "Items/New Recipe")]
    public class RecipeScriptable : ScriptableObject
    {
        //Used in editor
        public Item[] ingredients;
        public Vector2Int sizeOfGrid;
        public Item result;
       
        //Used in game logic
        private Recipe _recipe;        
        public Recipe Recipe => _recipe;
        
        private void OnValidate()
        {
            _recipe = new Recipe(sizeOfGrid, ingredients, result);
        }
    }
}