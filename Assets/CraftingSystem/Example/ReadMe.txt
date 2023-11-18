My Example of the inventory

Logic of the example: 

Inventory:
    has a list of slots
    At start it will add starting items in them

My slots:
    1. BaseSlot
        Handles logic of drag and drop, adding/removing items
    2. DescriptionSlot
        +Shows description and name of the item
    3. CraftingSlot
        +Invoke event when item is added/removed from it
    4. ResultSlot
        -Items cannot be droped in
        +Always has item in it to show what will be crafted
        +Remove item would decrease count of items from crafting slots by 1
        
UsableItem:
    Contains logic of using item (just priniting description for now)
    Contains description of the item
    Contains bool IsConsumable
    
    
CraftingGridExample:
    Handles logic of crafting items (finding recipes in the RecipeBook, decreasing count of items in crafting slots)
    
    WARNING: put all crafting slots in the order of first row->second row...

   