using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryUI;
    public Transform itemsParent;
    Inventory inventory;
    void Start()
    {
        inventory = Inventory.instance;
        //inventory.onItemChangedCallBack += UpdateUI;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B)) 
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
            UpdateUi();
        }
    }
    public void UpdateUi() 
    {
        InventorySlot[] slots = GetComponentsInChildren<InventorySlot>();
        for (int i = 0; i < slots.Length; i++) 
        {
            if (i < inventory.items.Count)
            {
                slots[i].addItem(inventory.items[i]);
            }
            else 
            {
                slots[i].ClearSlot();
            }
        }
    }
}
