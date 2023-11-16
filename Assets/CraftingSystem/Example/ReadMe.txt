My Example of the inventory

## Items

Items created by the UseableItem class that inherits from the Item class.
InventoryItem is a gameObject that you can interact on the scene. 
It would always be in one of the slots.
It can be dragged and dropped to another slot.
It can be used by clicking on it.
You can stack items with the same info (UseableItem).

There is no split option in the 1.0 version.

## Slots

Items are placed in the slots.
Slots should implement the IItemSlot interface.

## Drag and Drop

Drag and Drop use unity's drag and drop system.

Slots have OnDrop method and Items can be draggable


# Known issues:
    Unity errors when closing inventory window (cannot start couroutine on disabled object)