using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollider : MonoBehaviour
{
    public bool hitWall = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.tag);
        if(other.gameObject.CompareTag("Wall")) {
            hitWall = true;
        }
        
    }
    void OnTriggerExit2D(Collider2D col)
    {
        Debug.Log("no collide!!");
        hitWall = false;
    }
}
