using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    public static CharacterMovement instance;
    public int currentHealth, maxHealth;
    private float damageTime = 1.5f;
    private float canDamage = -1f;
    public ParticleSystem particle;

    InventoryController inventoryController;
    public GameObject Inventory;

    private void Awake() 
    {
        instance = this;
    }

    //Start is called before the first frame update
    void Start()
    {
        inventoryController = Inventory.GetComponent<InventoryController>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
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
            inventoryController.fuelLamp();
        }


        float hInput = 0;
        hInput = Input.GetAxisRaw("Horizontal");
        movement.x = hInput;
        float vInput = 0;
        vInput = Input.GetAxisRaw("Vertical");
        movement.y = vInput;

        if(hInput > 0)
        {
            if(vInput > 0)
            {
                
                characterTrnsform.eulerAngles = new Vector3(0, 0, 135);
            }
            else if(vInput < 0)
            {
                
                characterTrnsform.eulerAngles = new Vector3(0, 0, 45);
            }
            else
            {
                characterTrnsform.eulerAngles = new Vector3(0, 0, 90);
            }

            
        }
        else if(hInput < 0)
        {
            if (vInput > 0)
            {
                characterTrnsform.eulerAngles = new Vector3(0, 0, -135);
            }
            else if (vInput < 0)
            {
                characterTrnsform.eulerAngles = new Vector3(0, 0, -45);
            }
            else
            {
                characterTrnsform.eulerAngles = new Vector3(0, 0, -90);
            }
        }
        else
        {
            if (vInput < 0)
            {
                characterTrnsform.eulerAngles = new Vector3(0, 0, 0);
            }
            else if (vInput > 0)
            {
                characterTrnsform.eulerAngles = new Vector3(0, 0, 180);
            }
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


        FuelType1 fuelType1 = collision.GetComponent<FuelType1>();
        if (fuelType1 != null)
        {
            inventoryController.fuelCount++;
            //lightSourceControl.fuelIncrease(fuelType1.fuelAmount);
            //audioManager.Play("Oil");
            //AudioManager.instance.Play("Oil");
            fuelType1.destroy();
            return;
        }

        Key k = collision.GetComponent<Key>();
        if (k != null)
        {
            txtKey.text = Convert.ToString(Convert.ToInt32(txtKey.text) + 1);
            //audioManager.Play("Key");
            AudioManager.instance.Play("Key");
            k.destroy();
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
        HealthSystem.instance.HealthDisplay();
    }
    public void HealPlayer() 
    {
        currentHealth++;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        HealthSystem.instance.HealthDisplay();
    }

}
