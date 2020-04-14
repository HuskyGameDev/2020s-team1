using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterMovement : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite moveUpSprite;
    public Sprite moveDownSprite;
    public Sprite moveLeftSprite;
    public Sprite moveRightSprite;
    public int keyCounter;
    public float moveSpeed = 5;
    public float runSpeed = 7;
    private bool isRunning = false;
    public Rigidbody2D rb;
    Vector2 movement;
    public Transform characterTrnsform;
    //public float fuelType1Amount;
    public LightSourceControl lightSourceControl;
    public AudioManager audioManager;
    private float timeSinceLastPlay = 0.3f;

    public static CharacterMovement instance;
    public int currentHealth, maxHealth;
    private float damageTime = 1.5f;
    private float canDamage = -1f;
    public ParticleSystem particle;
    public bool flameDama = false;
    public int flameCount = 0;

    CanvasController canvasController;

    InventorySlot inventorySlot;

    public GameObject inventorySlot1;
    public GameObject inventorySlot2;
    public GameObject inventorySlot3;
    public GameObject inventorySlot4;
    public GameObject inventorySlot5;
    public GameObject inventorySlot6;
    public GameObject inventorySlot7;
    public GameObject inventorySlot8;
    public GameObject inventorySlot9;
    public GameObject inventorySlot10;



    private void Awake() 
    {
        instance = this;
    }

    //Start is called before the first frame update
    void Start()
    {
        keyCounter = 0;
        canvasController = GameObject.FindGameObjectWithTag("Canvas").GetComponent<CanvasController>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) 
        {
            inventorySlot1.GetComponent<InventorySlot>().Use(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            inventorySlot2.GetComponent<InventorySlot>().Use(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            inventorySlot3.GetComponent<InventorySlot>().Use(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            inventorySlot4.GetComponent<InventorySlot>().Use(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            inventorySlot5.GetComponent<InventorySlot>().Use(4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            inventorySlot6.GetComponent<InventorySlot>().Use(5);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            inventorySlot7.GetComponent<InventorySlot>().Use(6);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            inventorySlot8.GetComponent<InventorySlot>().Use(7);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            inventorySlot9.GetComponent<InventorySlot>().Use(8);
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            inventorySlot10.GetComponent<InventorySlot>().Use(9);
        }

        timeSinceLastPlay += Time.deltaTime;
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
        if (Input.GetKeyDown(KeyCode.R))
        {
            canvasController.fuelLamp();
        }


        float hInput = 0;
        hInput = Input.GetAxisRaw("Horizontal");
        movement.x = hInput;
        float vInput = 0;
        vInput = Input.GetAxisRaw("Vertical");
        movement.y = vInput;

        if(hInput > 0)
        {
            spriteRenderer.sprite = moveRightSprite;
        }
        else if(hInput < 0)
        {
            spriteRenderer.sprite = moveLeftSprite;
        }
        else if(vInput > 0)
        {
            spriteRenderer.sprite = moveUpSprite;
        }
        else if(vInput < 0)
        {
            spriteRenderer.sprite = moveDownSprite;
        }

    }

    private void FixedUpdate()
    {

        if (isRunning)
        {
            rb.MovePosition(rb.position + movement * runSpeed * Time.fixedDeltaTime);
            if(timeSinceLastPlay > 0.3f && (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0))
            {
                //audioManager.Play("RunStep");
                AudioManager.instance.Play("RunStep");
                    timeSinceLastPlay = 0.1f;
            }
            else if(Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
            {
                isRunning = false;
            }
            
        }
        else
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
            if(timeSinceLastPlay > 0.3f && (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0))
            {
                //audioManager.Play("WalkStep");
                AudioManager.instance.Play("WalkStep");
                timeSinceLastPlay = 0f;
            }
            
        }

        if (flameDama == true )
        {
            flameCount++;
        }

        if (flameCount >= 20)
        {
            damagePlayer();
            flameCount = 0;
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        FireTrap flames = collision.GetComponent<FireTrap>();
        bool temp = flames.returnMove();
        if (flames != null && temp == true)
        {
            flameDama = true;
        }
        else
        {
            flameDama = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Heart heart = collision.GetComponent<Heart>();
        if (heart != null) 
        {
            instance.HealPlayer();
        }
        SpikeTrapAS sp = collision.GetComponent<SpikeTrapAS>();
        if (sp != null)
        {
            particle.GetComponent<ParticleSystem>().Emit(1);
            instance.damagePlayer();
/*            AudioManager.instance.Play("GameOverMusic");
            SceneManager.LoadScene("Death");
            AudioManager.instance.SwitchMusic("Theme1", "MenuBGM");*/
            return;
        }


        EnemyAI enemy = collision.GetComponent<EnemyAI>();
        if(enemy != null)
        {
            AudioManager.instance.Play("GameOverMusic");
            SceneManager.LoadScene("Death");
            AudioManager.instance.SwitchMusic("Theme1", "MenuBGM");
        }
    }

    public void damagePlayer()
    {
        if (Time.time > canDamage)
        {
            currentHealth--;
            canDamage = Time.time + damageTime;
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                AudioManager.instance.Play("GameOverMusic");
                SceneManager.LoadScene("Death");
                AudioManager.instance.SwitchMusic("Theme1", "MenuBGM");
            }
        }
        canvasController.HealthDisplay(currentHealth);
    }
    public void HealPlayer() 
    {
        currentHealth++;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        canvasController.HealthDisplay(currentHealth);
    }

}
