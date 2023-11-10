using System.Collections.Generic;
using CraftingSystem.Example;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    List<ItemSlot> itemSlots = new List<ItemSlot>();
    [SerializeField]
    GameObject inventoryPanel;
    [Space] 
    [SerializeField] private GameObject GameItemPrefab;
    [SerializeField] private List<UseableItem> startItems;

    void Start()
    {
        //Read all itemSlots as children of inventory panel
        itemSlots = new List<ItemSlot>(
            inventoryPanel.transform.GetComponentsInChildren<ItemSlot>()
            );
        
        var slotIndex = 0;
        foreach (var item in startItems)
        {
            if (slotIndex >= itemSlots.Count)
            {
                print("Inventory is full");
                break;
            }
            
            var gameItem = Instantiate(GameItemPrefab);
            var inventoryItem = gameItem.GetComponent<InventoryItem>();
            inventoryItem.SetUp(item);
            itemSlots[slotIndex].SetItem(inventoryItem);
            slotIndex++;
        }
    }
}
