using System.Collections.Generic;
using UnityEngine;

namespace CraftingSystem.Core
{
    [CreateAssetMenu(fileName = "New Recipe", menuName = "Items/New Recipe")]
    public class RecipeScriptable : ScriptableObject
    {
        public Item[] ingredients;
        public Vector2Int sizeOfGrid;
        public Item result;

        private Recipe _recipe;        
        
        private void OnValidate()
        {
            _recipe = new Recipe(sizeOfGrid, ingredients, result);
        }
    }
}