using UnityEngine;

namespace CraftingSystem.Core
{
    /// <summary>
    /// Simple Item with icon
    /// Inherit from this to make your items with custom logic
    /// </summary>

    public abstract class Item : ScriptableObject
    {
        public Sprite icon;
    }
}