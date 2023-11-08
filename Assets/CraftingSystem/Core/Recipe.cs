using UnityEngine;

namespace CraftingSystem.Core
{
    [CreateAssetMenu(fileName = "New Recipe", menuName = "Items/New Recipe")]
    public class Recipe : ScriptableObject
    {
        public Item[] ingredients;
        public Vector2Int sizeOfGrid;
        public Item result;
    }
}