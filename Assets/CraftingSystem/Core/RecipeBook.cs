using System;
using System.Collections.Generic;
using UnityEngine;

namespace CraftingSystem.Core
{
    public class RecipeBook : MonoBehaviour
    {
        // TODO add suggestion to use this script only one time on a scene/project  
        [Header("Settings")]
        [SerializeField] private bool loadOnStart = true;
        
        [Header("Path to recipes")]
        [Tooltip("Recipes folder must be located in Resources folder")]
        [SerializeField] private string[] recipePath = new string[] { "Recipes/"};
        
        
        // cannot contain item with 0 ingredients
        private readonly List<List<Recipe>> _recipes = new List<List<Recipe>>();
        
        private bool _isLoaded = false;
        
        private void Start()
        {
            if (loadOnStart)
                LoadRecipes();
        }

        
        /// <summary>
        /// Load all recipes from Resources folder to this object memory
        /// </summary>
        public void LoadRecipes()
        {
            _recipes.Clear();
            foreach (var path in recipePath)
            {
                var recipes = Resources.LoadAll<RecipeScriptable>(path);
                foreach (var recipe in recipes)
                {
                    var itemsCount = recipe.Recipe.Count;
                    while (_recipes.Count < itemsCount)
                    {
                        _recipes.Add(new List<Recipe>());
                    }
                    // no items with 0 ingredients
                    // shift all items by 1 to the less side 
                    // [0] has items with 1 ingredient
                    _recipes[itemsCount - 1].Add(recipe.Recipe);
                }   
            }

            _isLoaded = true;
        }
        
        /// <summary>
        /// Unload all recipes from this object memory
        /// </summary>
        public void UnloadRecipes()
        {
            _recipes.Clear();
            _isLoaded = false;
        }
        
        /// <summary>
        /// Returns a list of recipes that match the given items
        /// </summary>
        /// <param name="items">Get all items in a grid, null if item not exist at the grid cell</param>
        /// <param name="gridSize">Max dimensions of a grid</param>
        /// <returns>Item that is crafted with this items</returns>
        public Item CraftItem(Item[] items, Vector2Int gridSize)
        {
            if (!_isLoaded) throw new Exception("Recipes are not loaded");
            
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
            
            var recipes = _recipes[count - 1];
            if (recipes == null)
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