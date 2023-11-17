using System.Collections.Generic;
using CraftingSystem.Example;
using CraftingSystem.Example.Slots2;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    List<BaseSlot> itemSlots = new List<BaseSlot>();
    [SerializeField]
    GameObject inventoryPanel;
    [Space] 
    [SerializeField] private GameObject GameItemPrefab;
    [SerializeField] private List<InventorItemAndCount> startItems;

    private void Awake()
    {
        //Read all itemSlots as children of inventory panel
        itemSlots = new List<BaseSlot>(
            inventoryPanel.transform.GetComponentsInChildren<BaseSlot>()
        );
    }

    private void OnEnable()
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
            var inventoryItem = gameItem.GetComponent<DragableItem>();
            inventoryItem.SetUp(item.itemInfo, item.count);
            itemSlots[slotIndex].AddItem(inventoryItem);
            slotIndex++;
        }
    }

    
    public void AddItem(DragableItem item)
    {
        BaseSlot emptySlot = null;
        
        foreach (var slot in itemSlots)
        {
            if (slot.Item == null)
            {
                if (emptySlot == null)
                    emptySlot = slot;
                continue;
            }

            if (slot.Item.ItemInfo == item.ItemInfo)
            {
                   slot.AddItem(item);
                   return;
            }
        }
        
        if (emptySlot != null && emptySlot.AddItem(item))
        {
            return;
        }
        HandleInventoryFull(item);
    }

    private void HandleInventoryFull(DragableItem item)
    {
        // Do whatever with item,
        // for example, destroy it
        
        Destroy(item.gameObject);
    }
    
    private void OnDisable()
    {
        startItems.Clear();
        foreach (var itemSlot in itemSlots)
        {
            var item = itemSlot.Item;   
            if (item != null)
            {
                itemSlot.RemoveItem();
                startItems.Add(new InventorItemAndCount(item.ItemInfo, item.Count));
                // Can be made more efficient by pooling items
                Destroy(item.gameObject);
            }
        }
    }
}
