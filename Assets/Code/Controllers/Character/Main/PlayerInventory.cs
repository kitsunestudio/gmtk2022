using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private List<InventoryEntry> inventory = new List<InventoryEntry>();

    public DicePanel dp;
    public InventoryEntry selectedDie;
    public DefaultableText d4Text;
    public DefaultableText d6Text;
    public DefaultableText d8Text;
    public DefaultableText d12Text;
    public DefaultableText d20Text;

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
                            added = true;
                            updateDieAmountText();
                            return;
                        } else if(entry.CurrentAmount + itemInstance.quantity > entry.MyItem.maxStack) {
                            amountLeft = entry.CurrentAmount + itemInstance.quantity - entry.MyItem.maxStack;
                            entry.CurrentAmount = entry.MyItem.maxStack;
                            itemInstance.quantity = amountLeft;
                            addWithLeftover = createEntry(itemInstance);
                            added = true;
                            updateDieAmountText();
                        }
                    }
                } 
            }
            if(!added) {
                inventory.Add(createEntry(itemInstance));
                updateDieAmountText();
            }
            if(addWithLeftover != null) {
                inventory.Add(addWithLeftover);
                updateDieAmountText();
            }
        } else {
            inventory.Add(createEntry(itemInstance));
            updateDieAmountText();
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
        updateDieAmountText();
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

    private void updateDieAmountText() {
        foreach(InventoryEntry entry in inventory) {
            if(entry.MyItem.id == 1) {
                d4Text.updateText(entry.CurrentAmount.ToString());
            } else if(entry.MyItem.id == 2) {
                d6Text.updateText(entry.CurrentAmount.ToString());
            } else if(entry.MyItem.id == 3) {
                d8Text.updateText(entry.CurrentAmount.ToString());
            } else if(entry.MyItem.id == 4) {
                d12Text.updateText(entry.CurrentAmount.ToString());
            } else if(entry.MyItem.id == 5) {
                d20Text.updateText(entry.CurrentAmount.ToString());
            }
        }
    }
}
