﻿using System.Collections.Generic;
using UnityEngine;

namespace CraftingSystem.Core
{
    public class RecipeBook : MonoBehaviour
    {
        // TODO add suggestion to use this script only one time on a scene/project  
        
        // cannot contain item with 0 ingredients
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
                _recipes[itemsCount - 1].Add(recipe.Recipe);
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
            print("start search");
            //TODO figure out this part
            var craftingItems = new GridState(items, gridSize);
            var count = craftingItems.Count;
            
            if (count == 0)
            {
                return null;
            }
            if (count >= _recipes.Count)
            { 
                return null;
            }
            
            var recipes = _recipes[items.Length - 1];
            if (recipes == null || recipes.Count == 0)
            {
                return null;
            }
            
            
            foreach (var recipe in recipes)
            {
                if (recipe.CanCraft(craftingItems))
                {
                    print ($"found + {recipe.Result.name}");
                    return recipe.Result;
                }
            }
            return null;
        }
    }
}