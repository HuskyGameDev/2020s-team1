using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;
    void Awake()
    {
        instance = this;
    }
    #endregion
    public delegate void OnItemChanged();
    //public OnItemChanged onItemChangedCallback();
    public int space = 10;
    public List<Item> items = new List<Item>();
    public void Add(Item item) 
    {
        if (item.showInInventory) 
        {
            if (items.Count >= space) 
            {
                Debug.Log("Not Enough Space");
                return;
            }
        }
        items.Add(item);
        //if (onItemChangedCallback != null)
        //   onItemChangedCallback.Invoke();
    }
    public void Remove(Item item) 
    {
        items.Remove(item);
        //if (OnItemChangedCallBack != null)
         //   onItemChangedCallback.Invoke();
    }


}
