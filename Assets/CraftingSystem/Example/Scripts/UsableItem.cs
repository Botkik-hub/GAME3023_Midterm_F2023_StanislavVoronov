using CraftingSystem.Core;
using UnityEngine;

//Attribute which allows right click->Create
namespace CraftingSystem.Example
{
    [CreateAssetMenu(fileName = "New Usable Item", menuName = "Items/New Usable Item")]
    public class UsableItem : Item
    {
        public bool isConsumable = false;
    
        [TextArea]
        public string description = "";
    
        public void Use()
        {
            Debug.Log("This is the Use() function of item: " + name + " - " + description);
        }
    }
}
