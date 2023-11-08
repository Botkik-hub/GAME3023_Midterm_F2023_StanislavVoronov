using System;
using System.Collections.Generic;
using UnityEngine;

namespace CraftingSystem.Core
{
    [Serializable]
    public struct RecipeItem
    {
        
        public Item item;
        // Position is relative to the left corner of the first recipe item
        public Vector2Int position;

        public static bool operator ==(RecipeItem a, RecipeItem b)
        {
            return  a.item == b.item && a.position == b.position;
        }

        public static bool operator !=(RecipeItem a, RecipeItem b)
        {
            return !(a == b);
        }
        
        public bool Equals(RecipeItem other)
        {
            return this == other;
        }

        public override bool Equals(object obj)
        {
            return obj is RecipeItem other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(item, position);
        }
    }

    [Serializable]
    public class Recipe
    {
        private Item _result;
        private Vector2Int _size;
        private List<RecipeItem> _recipeItems = new List<RecipeItem>();
        
        public Item Result => _result;
        public Vector2Int Size => _size;
        public IReadOnlyList<RecipeItem> RecipeItems => _recipeItems;
        
        
        public Recipe(Vector2Int size, Item[] recipeItems, Item result)
        {
            _result = result;
            _size = size;
            SetUp(recipeItems);
        }

        private void SetUp(Item[] ingredients)
        { 
            Vector2Int firstItemPosition = new Vector2Int(-1, -1);
            Vector2Int actualSize = new Vector2Int(0, 0);
            
            for (int y = 0; y < _size.y; y++)
            { 
                for (int x = 0; x < _size.x; x++)
                {
                    var index = x + y * _size.x;
                    // skip null items
                    if (ingredients[index] == null)
                    {
                        continue;
                    }
                    //find position of the top left item
                    if (firstItemPosition == new Vector2Int(-1, -1))
                    {
                        firstItemPosition = new Vector2Int(x, y);
                    }
                    var item = ingredients[index]; 
                    var distanceX = x - firstItemPosition.x;
                    var distanceY = y - firstItemPosition.y;
                    
                    if (distanceX > actualSize.x)
                    {
                        actualSize.x = distanceX;
                    }
                    if (distanceY > actualSize.y)
                    {
                        actualSize.y = distanceY;
                    }
                    _recipeItems.Add(new RecipeItem()
                    {
                        item = item,
                        position = new Vector2Int(distanceX, distanceY)
                    });
                }
            }
            _size = actualSize + new Vector2Int(1, 1);
            
        }
        
        public bool CanCraft(List<RecipeItem> items)
        {
            if (items.Count != _recipeItems.Count)
            {
                return false;
            }
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i] != _recipeItems[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}