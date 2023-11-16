using System;
using UnityEngine;

namespace CraftingSystem.Core
{
    
    [Serializable]
    public class Recipe : GridState
    {
        private Item _result;
        private int _resultCount;
        
        public Item Result => _result;
        public int ResultCount => _resultCount;
        
        public Recipe(Vector2Int gridSize, Item[] recipeItems, Item result, int resultCount) : base(recipeItems, gridSize)
        {
            _resultCount = resultCount;
            _result = result;
        }
        
    }
}