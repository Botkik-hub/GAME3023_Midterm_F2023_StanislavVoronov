using System;
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
}