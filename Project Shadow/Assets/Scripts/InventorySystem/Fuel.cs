using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : MonoBehaviour
{
    private float pickupFuelRate = .00000000000000001f;
    private static float nextPickupFuelRate = 0f;

    public Sprite icon;
    public string itemName = "Oil";
    CanvasController canvasController;
    public GameObject inventoryGroup;
    public GameObject Canvas;
    [HideInInspector]
    public UI_InventoryController inventoryController;
    private void Start()
    {
        canvasController = Canvas.GetComponent<CanvasController>();
        inventoryController = inventoryGroup.GetComponent<UI_InventoryController>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (Time.time > nextPickupFuelRate)
            {
                nextPickupFuelRate = Time.time + pickupFuelRate;
                for (int index = 0; index < inventoryController.InventorySlot.Length; index++)
                {
                    if (inventoryController.isOccupied[index] != true)
                    {
                        inventoryController.InventorySlot[index].GetComponent<InventorySlot>().Add(icon, index, itemName);
                        inventoryController.isOccupied[index] = true;
                        AudioManager.instance.Play("Oil"); ;
                        Destroy(gameObject);
                        canvasController.fuelCount++;
                        break;
                    }
                }
            }
        }
    }
}
