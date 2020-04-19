using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Key : MonoBehaviour
{
    private float pickupKeyRate = .00000000000000001f;
    private static float nextPickupKeyRate = 0f;

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
            if (Time.time > nextPickupKeyRate)
            {
                playerController.keyCounter++;
                txtKey.text = Convert.ToString(playerController.keyCounter);
                AudioManager.instance.Play("Key");
                Destroy(gameObject);
                nextPickupKeyRate = Time.time + pickupKeyRate;
            }
        }
    }
}
