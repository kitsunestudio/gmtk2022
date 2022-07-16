using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private List<InventoryEntry> inventory = new List<InventoryEntry>();

    public DicePanel dp;
    public InventoryEntry selectedDie;

    public void addItemToInventory(ItemInstance itemInstance) {

        if(!dp.getActive()) {
            dp.setActive();
        }
        
        if(itemInstance.myItem.stackable) {
            bool added = false;
            int amountLeft = 0;
            InventoryEntry addWithLeftover = null;
            foreach(InventoryEntry entry in inventory) {
                if(entry.MyItem.id == itemInstance.myItem.id) {
                    if(entry.CurrentAmount < entry.MyItem.maxStack) {
                        if(entry.CurrentAmount + itemInstance.quantity <= entry.MyItem.maxStack) {
                            entry.CurrentAmount += itemInstance.quantity;
                            //entry.MyInventoryEntry.GetComponent<ItemPanel>().updateText(entry.CurrentAmount);
                            added = true;
                            return;
                        } else if(entry.CurrentAmount + itemInstance.quantity > entry.MyItem.maxStack) {
                            amountLeft = entry.CurrentAmount + itemInstance.quantity - entry.MyItem.maxStack;
                            entry.CurrentAmount = entry.MyItem.maxStack;
                            //entry.MyInventoryEntry.GetComponent<ItemPanel>().updateText(entry.CurrentAmount);
                            itemInstance.quantity = amountLeft;
                            addWithLeftover = createEntry(itemInstance);
                            added = true;
                        }
                    }
                } 
            }
            if(!added) {
                inventory.Add(createEntry(itemInstance));
            }
            if(addWithLeftover != null) {
                inventory.Add(addWithLeftover);
            }
        } else {
            inventory.Add(createEntry(itemInstance));
        }

        if(selectedDie == null) {
            selectDie();
        }
    }

    private InventoryEntry createEntry(ItemInstance itemInstance) {
        InventoryEntry newEntry = new InventoryEntry();
        newEntry.MyItem = itemInstance.myItem;
        newEntry.CurrentAmount = itemInstance.quantity;
        return newEntry;
    }

    public void logInventory() {
        foreach(InventoryEntry item in inventory) {
            Debug.Log("Item: " + item.MyItem.name + " Amount: " + item.CurrentAmount );
        }
    }

    public void removeDie() {
        selectedDie.CurrentAmount -= 1;
    }

    public bool dieAvailable() {
        if(selectedDie.CurrentAmount > 0) {
            return true;
        }
        return false;
    }

    public void selectDie(int numberOfSides = 0) {
        if(numberOfSides == 0) {
            selectedDie = inventory[0];
            dp.highlightActive(selectedDie.MyItem.sides);
        } else {
            foreach(InventoryEntry entry in inventory) {
                if(entry.MyItem.sides == numberOfSides) {
                    selectedDie = entry;
                    dp.highlightActive(numberOfSides);
                    return;
                }
            }
        }
    }
}
