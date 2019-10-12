using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 5;
    public Rigidbody2D rb;
    Vector2 movement;
    public Transform transform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float hInput = Input.GetAxisRaw("Horizontal");
        movement.x = hInput;
        if(hInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 0, -90);
        }
        else if(hInput > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 90);
        }


        float vInput = Input.GetAxisRaw("Vertical");
        movement.y = vInput;

        if (vInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (vInput > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 180);
        }

    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
