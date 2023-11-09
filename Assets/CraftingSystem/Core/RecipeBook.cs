using System.Collections.Generic;
using UnityEngine;

namespace CraftingSystem.Core
{
    public class RecipeBook : MonoBehaviour
    {
        private readonly List<List<Recipe>> _recipes = new List<List<Recipe>>();
        
        private void Start()
        {
            _recipes.Clear();
            var recipes = Resources.LoadAll<RecipeScriptable>("Recipes/");
            foreach (var recipe in recipes)
            {
                var itemsCount = recipe.Recipe.Count;
                while (_recipes.Count < itemsCount)
                {
                    _recipes.Add(new List<Recipe>());
                }
                _recipes[itemsCount].Add(recipe.Recipe);
            }
        }

        /// <summary>
        /// Returns a list of recipes that match the given items
        /// </summary>
        /// <param name="items">Get all items in a grid, null if item not exist at the grid cell</param>
        /// <param name="gridSize">Max dimensions of a grid</param>
        /// <returns>Item that is crafted with this items</returns>
        public Item CraftItem(Item[] items, Vector2Int gridSize)
        {
            var recipes = _recipes[items.Length];
            if (recipes == null)
            {
                return null;
            }
            var craftingItems = new GridState(items, gridSize);
            
            foreach (var recipe in recipes)
            {
                if (recipe.CanCraft(craftingItems))
                {
                    return recipe.Result;
                }
            }
            return null;
        }
    }
}