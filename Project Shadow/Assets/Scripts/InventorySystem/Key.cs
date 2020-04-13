using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Key : MonoBehaviour
{
    public string itemName;
    public Image iteamImage;
    public int Amount;

    public void destroy()
    {
        Destroy(gameObject);
    }
}
