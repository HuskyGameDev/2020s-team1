using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrapAS : MonoBehaviour
{
    public float moveSpeed = 5;
    Vector2 movement;
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
        movement.y = 0;
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
                //basic timmer
                second++;
                time = 0.0f;
            }
            if (second < 1)
            {
                //spike comes out of the wall
                movement.y = moveSpeed;
                rb.MovePosition(rb.position + movement);
                Show();
            }
            else if (second < 2)
            {
                {
                   //spike waites a second before going back in the wall
                }
            }
            else if (second < 3 && second >= 2)
            {
                //spike moves backwords into the wall
                movement.y = moveSpeed;
                rb.MovePosition(rb.position - movement);
            }
            else
            {
                //hides and resets the trap
                second = 0;
                canMove = false;
                Hide();
            }
        }
    }
   public static void Ativated()
    {
        canMove = true;
    }
}
