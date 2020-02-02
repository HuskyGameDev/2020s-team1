using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
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
        canMove = false;
        Hide();
    }
    void Show()
    {
        SpikeTrap1A.GetComponent<Renderer>().enabled = true;
    }
    void Hide()
    {
        SpikeTrap1A.GetComponent<Renderer>().enabled = false;
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
                second++;
                time = 0.0f;
                Debug.Log(second);
            }
            if (second < 5 && second >= 2)
            {
                Show();
                Debug.Log("here");
            }
            else if (second > 5)
            {
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
