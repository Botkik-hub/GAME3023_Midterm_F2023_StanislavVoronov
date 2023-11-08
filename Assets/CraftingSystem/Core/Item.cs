using UnityEngine;

namespace CraftingSystem.Core
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Items/New Item")]
    public class Item : ScriptableObject
    {
        public Sprite icon;
        
        [TextArea]
        public string description = "";
    }
}