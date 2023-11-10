namespace CraftingSystem.Example
{
    public interface IItemSlot
    {
        InventoryItem Item { get; }
        bool SetItem(InventoryItem item);
        void Clear();
    }
}