using System;
using System.Collections.Generic;
using CraftingSystem.Example;
using UnityEditor.UIElements;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    List<ItemSlot> itemSlots = new List<ItemSlot>();
    [SerializeField]
    GameObject inventoryPanel;
    [Space] 
    [SerializeField] private GameObject GameItemPrefab;
    [SerializeField] private List<InventorItemAndCount> startItems;

    private void Awake()
    {
        //Read all itemSlots as children of inventory panel
        itemSlots = new List<ItemSlot>(
            inventoryPanel.transform.GetComponentsInChildren<ItemSlot>()
        );
    }

    void Start()
    {
        CreateItems();
    }

    private void CreateItems()
    {
        var slotIndex = 0;
        foreach (var item in startItems)
        {
            if (slotIndex >= itemSlots.Count)
            {
                // Edit starting configuration of inventory
                print("Inventory is full");
                break;
            }
            
            var gameItem = Instantiate(GameItemPrefab);
            var inventoryItem = gameItem.GetComponent<InventoryItem>();
            inventoryItem.SetUp(item.itemInfo, item.count);
            itemSlots[slotIndex].SetItem(inventoryItem);
            slotIndex++;
        }
    }
    
    public bool AddItem(InventoryItem item)
    {
        foreach (var slot in itemSlots)
        {
            // Here can be handled situation when inventory is full
            if (slot.SetItem(item))
            {
                return true;
            }
        }
        return false;
    }

    private void OnDisable()
    {
        startItems.Clear();
        foreach (var itemSlot in itemSlots)
        {
            var item = itemSlot.Item;   
            if (item != null)
            {
                item.ClearSlot();
                startItems.Add(new InventorItemAndCount(item.ItemInfo, item.Count));
                // Can be made more efficient by pooling items
                Destroy(item);
            }
        }
    }
}
