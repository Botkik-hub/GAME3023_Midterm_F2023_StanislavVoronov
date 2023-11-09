using System.Collections.Generic;
using UnityEngine;

namespace CraftingSystem.Core
{
    public class GridState
    {
        private List<RecipeItem> _recipeItems = new List<RecipeItem>();
        public IReadOnlyList<RecipeItem> RecipeItems => _recipeItems;
        
        public int Count => _recipeItems.Count;
        public GridState(Item[] ingredients, Vector2Int gridSize)
        {
            Vector2Int firstItemPosition = new Vector2Int(-1, -1);
            Vector2Int actualSize = new Vector2Int(0, 0);

            for (int y = 0; y < gridSize.y; y++)
            {
                for (int x = 0; x < gridSize.x; x++)
                {
                    var index = x + y * gridSize.x;
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
        }
        
        public bool CanCraft(GridState item)
        {
            if (item.Count != _recipeItems.Count)
            {
                return false;
            }
            for (int i = 0; i < item.Count; i++)
            {
                if (item.RecipeItems[i] != _recipeItems[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}