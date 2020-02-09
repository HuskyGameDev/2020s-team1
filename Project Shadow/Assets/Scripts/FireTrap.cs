﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    public float moveSpeed = 5;
    public Rigidbody2D rb;
    public int second, minute;
    public bool timerIsOn = false;
    public float millisecond, time;
    public GameObject SpikeTrap1A;
    public static bool canMove;
    public Sprite spriteImage;
    // Start is called before the first frame update
    void Start()
    {
        canMove = false;
        Hide();
    }
    void Show()
    {
        SpikeTrap1A.GetComponent<Renderer>().enabled = true;//Shows the sprite
    }
    void Hide()
    {
        SpikeTrap1A.GetComponent<Renderer>().enabled = false;//hides the sprite
    }
    void HideChildren()
    {
        Renderer[] lChildRenderers = gameObject.GetComponentsInChildren<Renderer>();
        foreach (Renderer lRenderer in lChildRenderers)
        {
            lRenderer.enabled = false;
        }
    }
    void ShowChildren()
    {
        Renderer[] lChildRenderers = gameObject.GetComponentsInChildren<Renderer>();
        foreach (Renderer lRenderer in lChildRenderers)
        {
            lRenderer.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove.Equals(true))
        {
            
            timerIsOn = false;
            time += Time.deltaTime;
            if (time > 1.0f)
            {
                //Basic timmer
                second++;
                time = 0.0f;
            }
            if (second < 5 && second >= 2)
            {
                //shows the spark
                Show();
            }
            else if (second > 5)
            {
                //shows the fire 
                second = 0;
                canMove = false;
                Hide();
            }


        }
    }
    public static void Ativated()
    {
        //the trap is activated
        canMove = true;
    }
}