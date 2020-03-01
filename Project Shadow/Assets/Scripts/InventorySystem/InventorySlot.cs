using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    Item item;
    public void addItem(Item newItem) 
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
    }
    public void ClearSlot() 
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
    }
    public void RemoveItemFromInventory() 
    {
        Inventory.instance.Remove(item);
    }
    public void UseItem() 
    {
        if (item != null) 
        {
            item.Use();
        }
    }
}
