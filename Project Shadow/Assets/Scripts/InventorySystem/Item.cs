using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public bool Health;
    public bool Oil;
    public int restoreHealth;
    public int amount;

    public Sprite icon;
    public bool showInInventory = true;
    public void Use() 
    {
        //if (Health) 
        //{
        //    GameObject player = GameObject.FindGameObjectWithTag("Player");
        //    player.GetComponent<CharacterMovement>().HealPlayer();
        //}
    }
}
