using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 5;
    public float runSpeed = 7;
    private bool isRunning = false;
    public Rigidbody2D rb;
    Vector2 movement;
    public Transform characterTrnsform;
    //public float fuelType1Amount;
    public LightSourceControl lightSourceControl;
    public Text txtKey;

    //Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            if (isRunning)
            {
                isRunning = false;
            }
            else
            {
                isRunning = true;
            }
        }

        float hInput = Input.GetAxisRaw("Horizontal");
        movement.x = hInput;
        if(hInput < 0)
        {
            characterTrnsform.eulerAngles = new Vector3(0, 0, -90);
        }
        else if(hInput > 0)
        {
            characterTrnsform.eulerAngles = new Vector3(0, 0, 90);
        }


        float vInput = Input.GetAxisRaw("Vertical");
        movement.y = vInput;

        if (vInput < 0)
        {
            characterTrnsform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (vInput > 0)
        {
            characterTrnsform.eulerAngles = new Vector3(0, 0, 180);
        }

    }

    private void FixedUpdate()
    {
        if (isRunning)
        {
            rb.MovePosition(rb.position + movement * runSpeed * Time.fixedDeltaTime);
        }
        else
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        FuelType1 fuelType1 = collision.GetComponent<FuelType1>();
        if (fuelType1 != null)
        {
            lightSourceControl.fuelIncrease(fuelType1.fuelAmount);
            fuelType1.destroy();
        }

        Key k = collision.GetComponent<Key>();
        if (k != null)
        {
            txtKey.text = Convert.ToString(Convert.ToInt32(txtKey.text) + 1);
            k.destroy();
        }
    }
}
