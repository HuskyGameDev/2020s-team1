using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    public int fuelCount;
    public Text fuelText;
    public GameObject fuelSource;
    LampSwitch lightSource;

    InventoryController inventoryController;
    public GameObject Inventory;

    // Start is called before the first frame update
    void Start()
    {
        lightSource = fuelSource.GetComponent<LampSwitch>();
        fuelCount = 0;
    }
    private void Update()
    {
        fuelText.text = fuelCount.ToString();

    }
    public void fuelLamp() 
    {
        if (fuelCount > 0) 
        {
            lightSource.fuelIncrease(2);
            AudioManager.instance.Play("Oil");
            fuelCount--;
        }
        if (fuelCount <= 0) 
        {
            fuelCount = 0;
        }
    }
}
