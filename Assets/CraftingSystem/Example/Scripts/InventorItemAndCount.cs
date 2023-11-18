using System;

namespace CraftingSystem.Example
{
    [Serializable]
    public class InventorItemAndCount
    {
        public UsableItem itemInfo;
        public int count;
        
        public InventorItemAndCount(UsableItem itemInfo, int count)
        {
            this.itemInfo = itemInfo;
            this.count = count;
        }
    }
}