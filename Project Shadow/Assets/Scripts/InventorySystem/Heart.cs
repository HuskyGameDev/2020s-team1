using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heart : MonoBehaviour
{
    private float pickupRate = .0000000000001f;
    private static float nextPickup = 0f;

    public string itemName = "Health";
    public Sprite icon;
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
        if (Time.time > nextPickup) 
        {
            nextPickup = Time.time + pickupRate;
            for (int index = 0; index < inventoryController.InventorySlot.Length; index++)
            {
                if (inventoryController.isOccupied[index] != true)
                {
                    inventoryController.InventorySlot[index].GetComponent<InventorySlot>().Add(icon, index, itemName);
                    inventoryController.isOccupied[index] = true;
                    Destroy(gameObject);
                    break;
                }
            }
        }
    }
}
