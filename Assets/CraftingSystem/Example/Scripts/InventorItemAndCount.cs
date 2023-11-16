using System;

namespace CraftingSystem.Example
{
    [Serializable]
    public struct InventorItemAndCount
    {
        public UseableItem itemInfo;
        public int count;
        
        public InventorItemAndCount(UseableItem itemInfo, int count)
        {
            this.itemInfo = itemInfo;
            this.count = count;
        }
    }
}