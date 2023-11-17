using CraftingSystem.Core;
using UnityEngine;

//Attribute which allows right click->Create
[CreateAssetMenu(fileName = "New Usable Item", menuName = "Items/New Usable Item")]
public class UseableItem : Item
{
    public bool isConsumable = false;
    
    [TextArea]
    public string description = "";
    
    public void Use()
    {
        Debug.Log("This is the Use() function of item: " + name + " - " + description);
    }
}
