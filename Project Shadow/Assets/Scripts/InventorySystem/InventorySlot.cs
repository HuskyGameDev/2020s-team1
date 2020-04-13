using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [HideInInspector]
    public Image[] icon;
    [HideInInspector]
    private string[] itemName;
    [HideInInspector]
    public int amountHealthToRestore;
    [HideInInspector]
    public int amountOfItems;
    CharacterMovement playerController;
    CanvasController canvasController;
    [HideInInspector]
    public UI_InventoryController inventoryController;
    // Start is called before the first frame update

    void Start()
    {
        inventoryController = GameObject.FindGameObjectWithTag("Inventory").GetComponent<UI_InventoryController>();
        canvasController = GameObject.FindGameObjectWithTag("Canvas").GetComponent<CanvasController>();
        itemName = new string[10];
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMovement>();
        icon = new Image[10];
        icon[0] = GameObject.Find("Icon1").GetComponent<Image>();
        icon[1] = GameObject.Find("Icon2").GetComponent<Image>();
        icon[2] = GameObject.Find("Icon3").GetComponent<Image>();
        icon[3] = GameObject.Find("Icon4").GetComponent<Image>();
        icon[4] = GameObject.Find("Icon5").GetComponent<Image>();
        icon[5] = GameObject.Find("Icon6").GetComponent<Image>();
        icon[6] = GameObject.Find("Icon7").GetComponent<Image>();
        icon[7] = GameObject.Find("Icon8").GetComponent<Image>();
        icon[8] = GameObject.Find("Icon9").GetComponent<Image>();
        icon[9] = GameObject.Find("Icon10").GetComponent<Image>();


    }
    public void Add(Sprite image,int index, string ItemName) 
    {
        itemName[index] = ItemName;
        icon[index].sprite = image;
        icon[index].enabled = true;
        //Debug.Log("index " + index);
    }
    public void Use(int index)
    {
        if (inventoryController.isOccupied[index]){
            //Debug.Log("use " + index);
            if (itemName[index] == "Health")
            {
                if (playerController.currentHealth < 6)
                {
                    playerController.HealPlayer();
                    itemName[index] = "";
                    icon[index].sprite = null;
                    icon[index].enabled = false;
                    //Debug.Log("use Health from " + index);
                }
            }
            else if (itemName[index] == "Oil")
            {
                canvasController.fuelLamp();
                itemName[index] = "";
                icon[index].sprite = null;
                icon[index].enabled = false;
                //Debug.Log("fuel Oil from " + index);
            }
        }
    }
}
