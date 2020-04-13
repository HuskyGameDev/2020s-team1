using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_InventoryController : MonoBehaviour
{
    public GameObject[] InventorySlot;
    [HideInInspector]
    public bool[] isOccupied;
    private void Start()
    {
        isOccupied = new bool[InventorySlot.Length];
    }
}
