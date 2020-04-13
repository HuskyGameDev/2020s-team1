using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heart : MonoBehaviour
{
    public string itemName = "Health";
    public Sprite icon;
    public int healthToRestore;
    public bool showInInventory = true;
    [HideInInspector]
    public UI_InventoryController inventoryController;
    public GameObject inventoryGroup;
    void Start()
    {
        inventoryController = inventoryGroup.GetComponent<UI_InventoryController>();
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        for (int index = 0; index < inventoryController.InventorySlot.Length; index++) 
        {
            if (inventoryController.isOccupied[index] != true) 
            {
                inventoryController.InventorySlot[index].GetComponent<InventorySlot>().AddItem(icon,index);
                inventoryController.isOccupied[index] = true;
                break;
            }
        }
        Destroy(gameObject);
    }
}
