Crafting system package 1.1

Created by Stanislav Voronov

This package contains an abstract crafting system for you to use and my implementation of it with inventory and crafting table.

I recommend you to use only the system and make your own inventory that suits your needs.

If you want to use my implementation of inventory, you can use it as a reference or just use it as it is.

If you only want to use the system, you need Core folder, everything else can be deleted

More on my implementation of the inventory you can find in Readme.txt in the Example folder

System is not limited by the size of the grid. (My example has only 3x3 or 4x4 field)


## How to use

create your items by inheriting the Item class and creating a new scriptable objects from it

you can create your own recipes just by creating a new scriptable object from the RecipeScriptable class or you can inherit from it and add your specifics

All recipes have to be in the Resources folder, you can specify subfolders in the RecipeBook instances

RecipeBook has to be on the scene to work, you can choose when to load your recipes by calling LoadRecipes() method

# Recipe Editor window

I have made custom inspector for RecipeScriptable, so you can see crafting grid in there, If you want to use other recipe class, you can make your own custom inspector for it using my example

For an editor to be able to show the icon of a Recipe, you need to make it readable (Read/Write check in the import settings)