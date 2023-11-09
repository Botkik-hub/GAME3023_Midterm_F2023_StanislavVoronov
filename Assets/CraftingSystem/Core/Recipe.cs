using System;
using UnityEngine;

namespace CraftingSystem.Core
{
    
    [Serializable]
    public class Recipe : GridState
    {
        private Item _result;
        public Item Result => _result;
        
        public Recipe(Vector2Int gridSize, Item[] recipeItems, Item result) : base(recipeItems, gridSize)
        {
            _result = result;
        }
        
    }
}