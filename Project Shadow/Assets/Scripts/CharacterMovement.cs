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
    public AudioManager audioManager;
    private float timeSinceLastPlay = 0.3f;
    private bool isMoving = false;

    //Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastPlay += Time.deltaTime;
        isMoving = false;
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
        if(hInput > 0)
        {
            characterTrnsform.eulerAngles = new Vector3(0, 0, -90);
            isMoving = true;
        }
        else if(hInput < 0)
        {
            characterTrnsform.eulerAngles = new Vector3(0, 0, 90);
            isMoving = true;
        }


        float vInput = Input.GetAxisRaw("Vertical");
        movement.y = vInput;

        if (vInput > 0)
        {
            characterTrnsform.eulerAngles = new Vector3(0, 0, 0);
            isMoving = true;
        }
        else if (vInput < 0)
        {
            characterTrnsform.eulerAngles = new Vector3(0, 0, 180);
            isMoving = true;
        }

    }

    private void FixedUpdate()
    {
        if (isRunning)
        {
            rb.MovePosition(rb.position + movement * runSpeed * Time.fixedDeltaTime);
            if (isMoving)
            {
                if(timeSinceLastPlay > 0.3f)
                {
                    audioManager.Play("RunSound");
                    timeSinceLastPlay = 0f;
                }
            }
            //audioManager.Play("RunSound");
        }
        else
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
            if (isMoving)
            {
                if (timeSinceLastPlay > 0.3f)
                {
                    audioManager.Play("WalkSound");
                    timeSinceLastPlay = 0f;
                }
            }
            //audioManager.Play("WalkSound");
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        FuelType1 fuelType1 = collision.GetComponent<FuelType1>();
        if (fuelType1 != null)
        {
            lightSourceControl.fuelIncrease(fuelType1.fuelAmount);
            audioManager.Play("OilSound");
            fuelType1.destroy();
        }

        Key k = collision.GetComponent<Key>();
        if (k != null)
        {
            txtKey.text = Convert.ToString(Convert.ToInt32(txtKey.text) + 1);
            audioManager.Play("KeySound");
            k.destroy();
        }
    }
}
