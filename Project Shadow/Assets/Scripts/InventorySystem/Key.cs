using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Key : MonoBehaviour
{
    public Text txtKey;
    CharacterMovement playerController;
    private GameObject player;
    [HideInInspector]
    public static Key instance;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<CharacterMovement>();


    }
    private void Awake()
    {
        instance = this;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            playerController.keyCounter++;
            txtKey.text = Convert.ToString(playerController.keyCounter);
            AudioManager.instance.Play("Key");
            Destroy(gameObject);
        }
    }
}
